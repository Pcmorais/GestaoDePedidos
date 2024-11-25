namespace GestaoPedidos.Application.DTO.Request
{
    /// <summary>
    /// Representa um item de um pedido, com um produto e a quantidade desejada.
    /// </summary>
    public class PedidoItemRequestDTO
    {
        /// <summary>
        /// ID do produto que será adicionado ao pedido.
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Quantidade do produto solicitada no pedido.
        /// </summary>
        public int Quantidade { get; set; }
    }


}
