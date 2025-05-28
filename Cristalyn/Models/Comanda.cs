using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă o comandă plasată în magazinul de bijuterii
    /// </summary>
    public class Comanda
    {
        /// <summary>
        /// Identificatorul unic al comenzii
        /// </summary>
        [DisplayName("Nr. Comandă")]
        public int Id { get; set; }

        /// <summary>
        /// Adresa de email a utilizatorului care a plasat comanda
        /// </summary>
        [Required(ErrorMessage = "Adresa de email este obligatorie")]
        [EmailAddress(ErrorMessage = "Vă rugăm să introduceți o adresă de email validă")]
        [DisplayName("Email Client")]
        public string? EmailUtilizator { get; set; }

        /// <summary>
        /// Data și ora la care a fost plasată comanda
        /// </summary>
        [Required(ErrorMessage = "Data comenzii este obligatorie")]
        [DisplayName("Data Comandă")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        /// <summary>
        /// Statusul curent al comenzii (Nouă, Procesată, Livrată, Anulată)
        /// </summary>
        [Required(ErrorMessage = "Statusul comenzii este obligatoriu")]
        [DisplayName("Status")]
        [RegularExpression("^(Nouă|Procesată|Livrată|Anulată)$", 
            ErrorMessage = "Statusul comenzii trebuie să fie: Nouă, Procesată, Livrată sau Anulată")]
        public string? Status { get; set; } = "Nouă";

        /// <summary>
        /// Valoarea totală a comenzii în MDL
        /// </summary>
        [DisplayName("Total (MDL)")]
        [Range(0, double.MaxValue, ErrorMessage = "Totalul comenzii trebuie să fie mai mare sau egal cu 0")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        /// <summary>
        /// Lista produselor incluse în comandă
        /// </summary>
        [DisplayName("Produse")]
        [MinLength(1, ErrorMessage = "Comanda trebuie să conțină cel puțin un produs")]
        public List<CosItem> Produse { get; set; } = new();

        /// <summary>
        /// ID-ul cuponului aplicat comenzii
        /// </summary>
        [DisplayName("Cupon Aplicat")]
        public int? CuponId { get; set; }

        /// <summary>
        /// Cuponul aplicat comenzii
        /// </summary>
        public Cupon? Cupon { get; set; }

        /// <summary>
        /// Valoarea reducerii aplicate prin cupon
        /// </summary>
        [DisplayName("Reducere Aplicată (MDL)")]
        public decimal Reducere { get; set; }

        /// <summary>
        /// Calculează totalul final după aplicarea reducerii
        /// </summary>
        public decimal TotalFinal => Total - Reducere;
    }
}
