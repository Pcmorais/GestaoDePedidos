using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infra.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(AppDbContext context) : base(context) { }

        public async Task<Produto?> ObterPeloCodigoProdutoAsync(string sku)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.SKU == sku);
        }
    }
}
