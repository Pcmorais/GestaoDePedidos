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

                // Cabeçalhos
                worksheet.Cells[1, 1].Value = "Código do Pedido";
                worksheet.Cells[1, 2].Value = "Nome do Cliente";
                worksheet.Cells[1, 3].Value = "Data do Pedido";
                worksheet.Cells[1, 4].Value = "Valor Total";

                // Estilo dos cabeçalhos
                using (var headerRange = worksheet.Cells[1, 1, 1, 4])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Preenchendo os dados
                for (int i = 0; i < pedidos.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = pedidos[i].Id;
                    worksheet.Cells[i + 2, 2].Value = pedidos[i].Nome;
                    worksheet.Cells[i + 2, 3].Value = pedidos[i].DataPedido;
                    worksheet.Cells[i + 2, 4].Value = pedidos[i].Valor.ToString("C2"); // Formata como valor monetário
                }

                // Estilo das células de dados
                using (var dataRange = worksheet.Cells[2, 1, pedidos.Count + 1, 4])
                {
                    dataRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    dataRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                // Ajustando o tamanho das colunas
                worksheet.Cells.AutoFitColumns();

                // Adicionando bordas
                using (var borderRange = worksheet.Cells[1, 1, pedidos.Count + 1, 4])
                {
                    borderRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    borderRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    borderRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    borderRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                return await Task.FromResult(package.GetAsByteArray());
            }
        }

    }

}
