using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Interfaces.Repositorio
{
    public interface IPedidoRepositorio : IRepositorio<Pedido>
    {
        Task<Pedido?> ObtenhaDadosPedidoPeloId(int id);
        Task<IEnumerable<Pedido>> ObtenhaDadosParaRelatorio(string? nomePessoa, int? pedidoId, DateTime? dataPedidoInicio, DateTime? dataPedidoFim);
        Task<Pedido?> ObtenhaPedidoDetalhado(int pedidoId);
    }
}