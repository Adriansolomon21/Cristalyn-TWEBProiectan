using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    public class Produs
    {
        public int Id { get; set; }

        [Required]
        public string Nume { get; set; } = string.Empty;

        [Required]
        public decimal Pret { get; set; }

        public string Descriere { get; set; } = string.Empty;

        public string ImagineUrl { get; set; } = string.Empty;

        public string Categorie { get; set; } = string.Empty; // ← câmp adăugat pentru clasificare produs

    }
}
