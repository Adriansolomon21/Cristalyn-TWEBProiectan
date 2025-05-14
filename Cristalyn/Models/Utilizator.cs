using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    public class Utilizator
    {
        public int Id { get; set; }

        [Required]
        public string? Nume { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Parola { get; set; }

        // Nu trebuie completat manual
        public string? Rol { get; set; }
    }
}
