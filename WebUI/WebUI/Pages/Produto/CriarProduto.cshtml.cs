using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using WebUI.ViewModel;

namespace WebUI.Pages.Produto
{
    public class CriarProdutoModel : PageModel
    {

        [BindProperty]
        public ProdutoViewModel Produto { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public CriarProdutoModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync([FromForm] ProdutoViewModel produto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(produto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("https://localhost:7213/api/produto", jsonContent);

            var dados = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = dados;
            } else
            {
                TempData["ErrorMessage"] = dados;
            }

            return Page();
        }
    }
}
