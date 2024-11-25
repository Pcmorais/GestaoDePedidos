using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Application.DTO.Request
{
    public class EnderecoRequestDTO

    {
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string CEP { get; set; } = null!;
        public string TipoEndereco { get; set; } = null!;
        public string Logradouro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string? Complemento { get; set; }
    }

}
