using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Application.DTO.Request
{
    public class CriarPessoaRequestDTO
    {
        public string NomeCompleto { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefone { get; set; }
        public List<EnderecoRequestDTO> Enderecos { get; set; } = new List<EnderecoRequestDTO>();
    }

}
