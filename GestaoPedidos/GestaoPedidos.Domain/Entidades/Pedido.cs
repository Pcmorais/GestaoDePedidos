namespace GestaoPedidos.Domain.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
        public int EnderecoEntregaId { get; set; }
        public Endereco EnderecoEntrega { get; set; } = null!;
        public int EnderecoCobrancaId { get; set; }
        public Endereco EnderecoCobranca { get; set; } = null!;
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);
    }


}
