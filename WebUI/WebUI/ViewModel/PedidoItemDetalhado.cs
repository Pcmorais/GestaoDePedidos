namespace WebUI.ViewModel
{
    public class PedidoItemDetalhado
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string SKU { get; set; } = null!;
    }
}
