using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidos.Infra.Repositorios
{
    public class RelatorioPedidosRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public RelatorioPedidosRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //public async Task<List<dynamic>> ObterRelatorioCompletoAsync()
        //{
        //    using var dbContext = _appDbContext;

        //    var valores = dbContext.Pedidos
        //                  .Include(p => p.Pessoa)
        //                  .Include(p => p.Itens)
        //                  .ToList()
        //                  .Select(x => new
        //                  {

        //                      Nome = x.Pessoa.NomeCompleto,
        //                      DataPedido = x.DataPedido.ToShortDateString(),
        //                      Valor = x.Itens.Sum(i => i.Quantidade * i.ValorTotal)
        //                  });

        //    return valores;
        //}
    }
}
