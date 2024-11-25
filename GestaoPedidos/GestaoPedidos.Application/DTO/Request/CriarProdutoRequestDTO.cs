namespace GestaoPedidos.Application.DTO.Request
{
    public class CriarProdutoRequestDTO
    {
        /// <summary>
        /// Nome do produto (obrigatório).
        /// </summary>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Descrição do produto (opcional).
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Preço unitário do produto (obrigatório).
        /// </summary>
        public decimal PrecoUnitario { get; set; }

        /// <summary>
        /// Código único de identificação do produto (SKU).
        /// </summary>
        public string SKU { get; set; } = null!;
    }

}
