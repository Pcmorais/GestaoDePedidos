using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidos.Application.DTO.Response
{
    public class PedidosResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataPedido { get; set; }
        public decimal Valor { get; set; }
    }
}
