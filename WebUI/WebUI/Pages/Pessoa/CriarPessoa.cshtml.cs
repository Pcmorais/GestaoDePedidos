using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using WebUI.ViewModel;
using Microsoft.AspNetCore.Diagnostics;

namespace WebUI.Pages.Pessoa
{
    public class CreatePessoaModel : PageModel
    {
        [BindProperty]
        public PessoaViewModel Pessoa { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public CreatePessoaModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(PessoaViewModel pessoa)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(pessoa),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("https://localhost:7213/api/Pessoa", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var sucessContent = await response.Content.ReadAsStringAsync();
                TempData["SuccessMessage"] = sucessContent;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync(); // Lê o corpo da resposta
                TempData["ErrorMessage"] = errorContent;
            }

            
            return Page();
        }


    }
}
