using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infra.Repositorios
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<Pedido?> ObtenhaPedidoDetalhado(int pedidoId)
        {
            return await _context.Pedidos
                .Include(p => p.Pessoa)
                .Include(p => p.Itens).ThenInclude(item => item.Produto)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<Pedido?> ObtenhaDadosPedidoPeloId(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens).ThenInclude(item => item.Produto)
                .Include(p => p.EnderecoCobranca)
                .Include(p => p.EnderecoEntrega)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObtenhaDadosParaRelatorio(string? nomePessoa, int? pedidoId, DateTime? dataPedidoInicio, DateTime? dataPedidoFim)
        {
            var query = _context.Pedidos
                        .Include(p => p.Pessoa)
                        .Include(p => p.Itens)
                        .ThenInclude(item => item.Produto)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(nomePessoa))
            {
                query = query.Where(p => p.Pessoa.NomeCompleto.Contains(nomePessoa));
            }

            if (pedidoId.HasValue && pedidoId.Value > 0)
            {
                query = query.Where(p => p.Id == pedidoId.Value);
            }

            if (dataPedidoInicio.HasValue && dataPedidoFim.HasValue)
            {
                query = query.Where(p => p.DataPedido.Date >= dataPedidoInicio.Value.Date && p.DataPedido.Date <= dataPedidoFim.Value.Date);
            }
            else if (dataPedidoInicio.HasValue)
            {
                query = query.Where(p => p.DataPedido.Date >= dataPedidoInicio.Value.Date);
            }
            else if (dataPedidoFim.HasValue)
            {
                query = query.Where(p => p.DataPedido.Date <= dataPedidoFim.Value.Date);
            }

            return await query.ToListAsync();
        }

    }
}
