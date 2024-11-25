namespace GestaoPedidos.Domain.Entidades
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefone { get; set; }
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }

}
