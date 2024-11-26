using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using WebUI.ViewModel;
using System.Net;

namespace WebUI.Pages.Pedidos
{
    public class CreatePedidoModel : PageModel
    {
        private readonly string _baseUrl = "https://localhost:7213";

        [BindProperty]
        public PedidoCadastroViewModel Pedido { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();
        public List<PessoaViewModel> Pessoas { get; set; } = new List<PessoaViewModel>();

        private readonly HttpClient _httpClient;

        public CreatePedidoModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Alterado para ser assíncrono
        public async Task OnGetAsync()
        {
            Pessoas = await GetClientesAsync();
            Produtos = await GetProdutosAsync();
        }

        public async Task<JsonResult> OnPostSubmitAsync([FromBody] PedidoCadastroViewModel pedido)
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(pedido),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:7213/api/Pedido/Criar", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var mensagem = await response.Content.ReadAsStringAsync();
                return new JsonResult(new { ok = true, message = mensagem });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync(); // Lê o corpo da resposta
                return new JsonResult(new { ok = false, message = errorContent });
            }
        }

        public async Task<JsonResult> OnGetBuscarEnderecosAsync(int clienteId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Endereco/{clienteId}");

            if (!response.IsSuccessStatusCode)
            {
                return new JsonResult(new { message = "Erro ao buscar os endereços." });
            }

            var enderecos = await response.Content.ReadFromJsonAsync<List<EnderecoViewModel>>();

            if (enderecos == null || !enderecos.Any())
            {
                return new JsonResult(new { message = "Nenhum endereço encontrado." });
            }

            return new JsonResult(enderecos);
        }

        private async Task<List<PessoaViewModel>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Pessoa");

            if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<PessoaViewModel>();
            }

            response.EnsureSuccessStatusCode(); 

            var clientes = await response.Content.ReadFromJsonAsync<List<PessoaViewModel>>();
            return clientes ?? new List<PessoaViewModel>(); 
        }

        private async Task<List<ProdutoViewModel>> GetProdutosAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Produto");

            if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<ProdutoViewModel>();
            }

            response.EnsureSuccessStatusCode();

            var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoViewModel>>();
            return produtos ?? new List<ProdutoViewModel>();
        }

        private async Task<List<EnderecoViewModel>> GetEnderecosAsync(int clienteId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Pessoa/{clienteId}/enderecos");
            response.EnsureSuccessStatusCode();
            var enderecos = await response.Content.ReadFromJsonAsync<List<EnderecoViewModel>>();
            return enderecos;
        }
    }
}
