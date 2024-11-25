namespace GestaoPedidos.Domain.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
        public string TipoEndereco { get; set; } = null!; // Entrega, Cobrança ou Ambos
        public string Logradouro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string? Complemento { get; set; }
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string CEP { get; set; } = null!;
    }
}
