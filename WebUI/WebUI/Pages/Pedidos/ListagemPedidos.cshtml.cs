using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Text.Json;
using WebUI.ViewModel;

namespace WebUI.Pages.Pedidos
{
    public class PedidosModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PedidosModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public List<PedidoIndexViewModel> Pedidos { get; set; } = new();
        public string? FiltroNome { get; set; }
        public string? FiltroDataPedidoInicio { get; set; }
        public string? FiltroDataPedidoFim { get; set; }
        public int? FiltroId { get; set; }

        public async Task OnGetAsync(string? nome, string? dataPedidoInicio, string? dataPedidoFim, int? id)
        {
            var baseUrl = "https://localhost:7213/api/Pedido/Pedidos";

            FiltroNome = nome;
            FiltroDataPedidoInicio = dataPedidoInicio;
            FiltroDataPedidoFim = dataPedidoFim;
            FiltroId = id;

            string url = ObtenhaUrlComQueryParams(nome, dataPedidoInicio, dataPedidoFim, id, baseUrl);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var todosPedidos = JsonSerializer.Deserialize<List<PedidoIndexViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Pedidos = todosPedidos.ToList();
            }
        }

        public async Task<IActionResult> OnPostDownloadExcelAsync(string? nome, string? dataPedidoInicio, string? dataPedidoFim, int? id)
        {
            var baseUrl = "https://localhost:7213/api/Pedido/relatorio/excel";
            string url = ObtenhaUrlComQueryParams(nome, dataPedidoInicio, dataPedidoFim, id, baseUrl);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "pedidos.xlsx");
            }
            else
            {
                return BadRequest("Não foi possível gerar o arquivo Excel.");
            }
        }

        private string ObtenhaUrlComQueryParams(string? nome, string? dataPedidoInicio, string? dataPedidoFim, int? id, string baseUrl)
        {
            var queryParams = new Dictionary<string, string?>()
            {
                { "nomePessoa", nome },
                { "dataPedidoInicio", dataPedidoInicio },
                { "dataPedidoFim", dataPedidoFim },
                { "pedidoId", id?.ToString() }
            };

            var validParams = queryParams.Where(kv => !string.IsNullOrEmpty(kv.Value))
                                         .ToDictionary(kv => kv.Key, kv => kv.Value);

            var url = validParams.Any()
                ? QueryHelpers.AddQueryString(baseUrl, validParams)
                : baseUrl;
            return url;
        }
    }
}
