using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    public class CosItem
    {
        public int Id { get; set; }

        [Required]
        public int ProdusId { get; set; }

        [Required]
        public string Nume { get; set; } = string.Empty;

        [Required]
        public decimal Pret { get; set; }

        [Required]
        public int Cantitate { get; set; }

        public decimal Total => Pret * Cantitate;
    }
}
