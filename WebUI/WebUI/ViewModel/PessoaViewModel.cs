using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModel
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefone { get; set; } = null!;
        public List<EnderecoViewModel> Enderecos { get; set; } = new List<EnderecoViewModel>();
    }
}
