namespace WebUI.ViewModel
{
    public class PedidoCadastroViewModel
    {
        public int ClienteId { get; set; }
        public int EnderecoEntregaId { get; set; }
        public int EnderecoCobrancaId { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public List<PedidoItemViewModel> Itens { get; set; } = new List<PedidoItemViewModel>();
    }

    public class PedidoItemViewModel
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal => Quantidade * ValorUnitario;
    }

}
