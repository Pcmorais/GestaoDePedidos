using GestaoPedidos.Application.Abstracoes;
using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.DTO.Response;
using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Servicos
{
    public class ServicoProduto
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ServicoProduto(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<Result> CriarProduto(CriarProdutoRequestDTO request)
        {
            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                PrecoUnitario = request.PrecoUnitario,
                SKU = request.SKU
            };

            // Validações
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return Result.Fail("O nome do produto é obrigatório.");

            if (produto.PrecoUnitario <= 0)
                return Result.Fail("O preço do produto deve ser maior que zero.");

            var produtoExistente = await _produtoRepositorio.ObterPeloCodigoProdutoAsync(produto.SKU);
            if (produtoExistente != null)
                return Result.Fail("O SKU informado já está cadastrado.");

            await _produtoRepositorio.AddAsync(produto);

            return Result.Ok("Produto criado com sucesso.");
        }


        public async Task<IEnumerable<ProdutoResponseDTO>> ObtenhaProdutosAsync()
        {
            var produtos = await _produtoRepositorio.GetAllAsync();

            if (produtos != null)
            {
                var response = produtos.Select(p => new ProdutoResponseDTO
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Nome = p.Nome,
                    SKU = p.SKU,
                    PrecoUnitario = p.PrecoUnitario,
                });

                return response;
            }


            return new List<ProdutoResponseDTO>();

        }
    }
}
