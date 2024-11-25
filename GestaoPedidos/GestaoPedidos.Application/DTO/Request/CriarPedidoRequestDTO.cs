namespace GestaoPedidos.Application.DTO.Request
{
    /// <summary>
    /// Objeto que contém os dados necessários para criar um novo pedido.
    /// </summary>
    public class CriarPedidoRequestDTO
    {
        /// <summary>
        /// ID do cliente para o qual o pedido está sendo criado.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// ID do endereço de entrega do pedido.
        /// </summary>
        public int EnderecoEntregaId { get; set; }

        /// <summary>
        /// ID do endereço de cobrança do pedido.
        /// </summary>
        public int EnderecoCobrancaId { get; set; }

        /// <summary>
        /// Lista de itens que fazem parte do pedido. Cada item contém um produto e a quantidade.
        /// </summary>
        public List<PedidoItemRequestDTO> Itens { get; set; } = new List<PedidoItemRequestDTO>();
    }

}
