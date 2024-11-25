using GestaoPedidos.Application.DTO.Response;
using GestaoPedidos.Application.Interfaces.Servicos;
using OfficeOpenXml;

namespace GestaoPedidos.Infra.Servicos
{
    public class ServicoPedidosExcel : IServicoExcelPedidos
    {
        public async Task<byte[]> GerarRelatorioPedidosExcelAsync(List<PedidosResponseDTO> pedidos)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatório de Pedidos");

                worksheet.Cells[1, 1].Value = "Código do Pedido";
                worksheet.Cells[1, 2].Value = "Nome do Cliente";
                worksheet.Cells[1, 3].Value = "Data do Pedido";
                worksheet.Cells[1, 4].Value = "Valor Total";

                for (int i = 0; i < pedidos.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = pedidos[i].Id;
                    worksheet.Cells[i + 2, 2].Value = pedidos[i].Nome;
                    worksheet.Cells[i + 2, 3].Value = pedidos[i].DataPedido;
                    worksheet.Cells[i + 2, 4].Value = pedidos[i].Valor;
                }

                worksheet.Cells.AutoFitColumns();

                return await Task.FromResult(package.GetAsByteArray());
            }
        }
    }

}
