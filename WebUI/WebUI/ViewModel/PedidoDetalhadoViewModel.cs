namespace WebUI.ViewModel
{
    public class PedidoDetalhadoViewModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public List<PedidoItemDetalhado> Itens { get; set; } = new List<PedidoItemDetalhado>();
    }
}
