using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Interfaces.Repositorio
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<Produto?> ObterPeloCodigoProdutoAsync(string sku);
    }
}
