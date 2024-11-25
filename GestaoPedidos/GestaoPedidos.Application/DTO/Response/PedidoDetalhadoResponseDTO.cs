namespace GestaoPedidos.Application.DTO.Response
{
    public class PedidoDetalhadoResponseDTO
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public List<PedidoItemResponseDTO> Itens { get; set; } = new List<PedidoItemResponseDTO>();

    }
}
