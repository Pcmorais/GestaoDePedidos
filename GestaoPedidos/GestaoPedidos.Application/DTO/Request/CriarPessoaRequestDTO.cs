using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Application.DTO.Request
{
    public class CriarPessoaRequestDTO
    {
        [Required]
        public string NomeCompleto { get; set; } = null!;
        [Required]
        public string CPF { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string? Telefone { get; set; }
        public List<EnderecoRequestDTO> Enderecos { get; set; } = new List<EnderecoRequestDTO>();
    }

}
