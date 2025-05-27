using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă un utilizator al aplicației
    /// </summary>
    public class Utilizator
    {
        /// <summary>
        /// Identificatorul unic al utilizatorului
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numele complet al utilizatorului
        /// </summary>
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Numele trebuie să aibă între 2 și 50 caractere")]
        [DisplayName("Nume complet")]
        [RegularExpression(@"^[a-zA-ZăâîșțĂÂÎȘȚ\s-]+$", ErrorMessage = "Numele poate conține doar litere, spații și cratimă")]
        public string? Nume { get; set; }

        /// <summary>
        /// Adresa de email a utilizatorului
        /// </summary>
        [Required(ErrorMessage = "Adresa de email este obligatorie")]
        [EmailAddress(ErrorMessage = "Vă rugăm să introduceți o adresă de email validă")]
        [DisplayName("Adresă email")]
        public string? Email { get; set; }

        /// <summary>
        /// Parola utilizatorului
        /// </summary>
        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 caractere")]
        [DisplayName("Parolă")]
        public string? Parola { get; set; }

        /// <summary>
        /// Rolul utilizatorului în aplicație (admin sau user)
        /// </summary>
        [DisplayName("Rol utilizator")]
        public string? Rol { get; set; }
    }
}
