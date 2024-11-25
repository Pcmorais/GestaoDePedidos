using GestaoPedidos.Application.Abstracoes;
using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.DTO.Response;
using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Application.Interfaces.Servicos;
using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Servicos
{
    public class ServicoPedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IPessoaRepositorio _pessoaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IServicoExcelPedidos _servicoExcelPedidos;

        public ServicoPedido(IPedidoRepositorio pedidoRepositorio, IPessoaRepositorio pessoaRepositorio, IProdutoRepositorio produtoRepositorio, IServicoExcelPedidos servicoExcelPedidos)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _pessoaRepositorio = pessoaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _servicoExcelPedidos = servicoExcelPedidos;
        }

        public async Task<Result> GerarPedido(int clienteId, int enderecoEntregaId, int enderecoCobrancaId, List<PedidoItemRequestDTO> itensDto)
        {
            var cliente = await _pessoaRepositorio.GetByClientIdAsync(clienteId);
            if (cliente == null)
                return Result.Fail("Cliente não encontrado.");

            var enderecoEntrega = cliente.Enderecos.FirstOrDefault(e => e.Id == enderecoEntregaId);
            if (enderecoEntrega == null)
                return Result.Fail("Endereço de entrega inválido.");

            var enderecoCobranca = cliente.Enderecos.FirstOrDefault(e => e.Id == enderecoCobrancaId);
            if (enderecoCobranca == null)
                return Result.Fail("Endereço de cobrança inválido.");

            // Criação do pedido
            var pedido = new Pedido
            {
                PessoaId = clienteId,
                EnderecoEntregaId = enderecoEntregaId,
                EnderecoCobrancaId = enderecoCobrancaId,
                DataPedido = DateTime.UtcNow,
            };

            foreach (var itemDto in itensDto)
            {
                var produto = await _produtoRepositorio.GetByIdAsync(itemDto.ProdutoId);
                if (produto == null)
                    return Result.Fail($"Produto com ID {itemDto.ProdutoId} não encontrado.");

                if (itemDto.Quantidade <= 0)
                    return Result.Fail("A quantidade do produto deve ser maior que zero.");

                var pedidoItem = new PedidoItem
                {
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    ValorUnitario = produto.PrecoUnitario
                };

                pedido.Itens.Add(pedidoItem);
            }

            await _pedidoRepositorio.AddAsync(pedido);

            return Result.Ok("Pedido gerado com sucesso.");
        }


        public async Task<PedidosResponseDTO?> ObtenhaPeloId(int pedidoId) 
        {
            var pedido = await _pedidoRepositorio.ObtenhaDadosPedidoPeloId(pedidoId);

            if (pedido == null) return null;

            var response = new PedidosResponseDTO
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido.ToShortDateString(),
                Nome = pedido.Pessoa.NomeCompleto.ToString(),
                Valor = pedido.ValorTotal
            };

            return response;
        }

        public async Task<PedidoDetalhadoResponseDTO> ObtenhaPedidoDetalhado(int pedidoId)
        {
            var pedido = await _pedidoRepositorio.ObtenhaPedidoDetalhado(pedidoId);

            if (pedido == null) return null;

            var response = new PedidoDetalhadoResponseDTO
            {
                Id = pedido.Id,
                NomeCliente = pedido.Pessoa.NomeCompleto,
                Itens = pedido.Itens.Select(i => new PedidoItemResponseDTO
                {
                    Id = i.Produto.Id,
                    Nome = i.Produto.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.Produto.PrecoUnitario,
                    SKU = i.Produto.SKU,
                }).ToList()
            };

            return response;
        }


        public async Task<List<PedidosResponseDTO>> ObtenhaPedidos(string? nomePessoa, int? pedidoId, DateTime? dataPedidoInicio, DateTime? dataPedidoFim)
        {
            var pedidos = await _pedidoRepositorio.ObtenhaDadosParaRelatorio(nomePessoa, pedidoId, dataPedidoInicio, dataPedidoFim);

            return pedidos.Select(p => new PedidosResponseDTO
            {
                Id = p.Id,
                Nome = p.Pessoa.NomeCompleto,
                DataPedido = p.DataPedido.ToShortDateString(),
                Valor = p.ValorTotal
            }).ToList();
        }

        public async Task<byte[]> GerarRelatorioPedidosExcel(string? nomePessoa, int? pedidoId, DateTime? dataPedidoInicio, DateTime? dataPedidoFim)
        {
            var pedidos = await ObtenhaPedidos(nomePessoa, pedidoId, dataPedidoInicio, dataPedidoFim);
            return await _servicoExcelPedidos.GerarRelatorioPedidosExcelAsync(pedidos);
        }
    }
}
