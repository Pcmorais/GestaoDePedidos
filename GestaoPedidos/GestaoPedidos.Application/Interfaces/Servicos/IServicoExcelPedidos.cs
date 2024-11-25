using GestaoPedidos.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidos.Application.Interfaces.Servicos
{
    public interface IServicoExcelPedidos
    {
        Task<byte[]> GerarRelatorioPedidosExcelAsync(List<PedidosResponseDTO> pedidos);
    }
}
