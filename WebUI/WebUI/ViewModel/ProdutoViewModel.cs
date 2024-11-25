using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModel
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string SKU { get; set; } = null!;
    }
}
