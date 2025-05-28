using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă punctele de fidelitate acumulate de un utilizator
    /// </summary>
    public class PuncteFidelitate
    {
        /// <summary>
        /// Identificatorul unic
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email-ul utilizatorului
        /// </summary>
        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este validă")]
        public string EmailUtilizator { get; set; } = string.Empty;

        /// <summary>
        /// Numărul de puncte acumulate
        /// </summary>
        [DisplayName("Puncte Acumulate")]
        public int PuncteAcumulate { get; set; }

        /// <summary>
        /// Numărul de puncte folosite
        /// </summary>
        [DisplayName("Puncte Folosite")]
        public int PuncteFolosite { get; set; }

        /// <summary>
        /// Calculează numărul de puncte disponibile
        /// </summary>
        [DisplayName("Puncte Disponibile")]
        public int PuncteDisponibile => PuncteAcumulate - PuncteFolosite;

        /// <summary>
        /// Calculează valoarea în MDL a punctelor disponibile (1 punct = 1 MDL)
        /// </summary>
        public decimal ValoarePuncte => PuncteDisponibile;
    }
} 