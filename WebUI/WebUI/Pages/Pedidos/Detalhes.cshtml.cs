using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.ViewModel;

namespace WebUI.Pages.Pedidos
{
    public class DetalhesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetalhesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public int PedidoId { get; set; }
        public string NomeCliente { get; set; }
        public List<PedidoItemDetalhado> Itens { get; set; } = new List<PedidoItemDetalhado>();

        public async Task<IActionResult> OnGetAsync(int pedidoId)
        {
            PedidoId = pedidoId;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7213/api/Pedido/{pedidoId}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Error");
            }

            var pedido = await response.Content.ReadFromJsonAsync<PedidoDetalhadoViewModel>(); 
            NomeCliente = pedido.NomeCliente;
            Itens = pedido.Itens;

            return Page();
        }
    }

}
