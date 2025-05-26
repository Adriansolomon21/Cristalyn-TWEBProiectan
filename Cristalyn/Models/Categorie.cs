using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        public string Nume { get; set; } = string.Empty;
    }
}
