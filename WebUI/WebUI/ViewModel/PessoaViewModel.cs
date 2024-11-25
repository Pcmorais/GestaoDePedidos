using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModel
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string NomeCompleto { get; set; } = null!;
        [Required]
        public string CPF { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Telefone { get; set; } = null!;
        public List<EnderecoViewModel> Enderecos { get; set; } = new List<EnderecoViewModel>();
    }
}
