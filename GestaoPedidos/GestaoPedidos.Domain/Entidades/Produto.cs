namespace GestaoPedidos.Domain.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string SKU { get; set; } = null!;
    }

}
