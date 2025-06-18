using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă un produs din magazinul de bijuterii
    /// </summary>
    public class Produs
    {
        /// <summary>
        /// Identificatorul unic al produsului
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numele produsului
        /// </summary>
        [Required(ErrorMessage = "Numele produsului este obligatoriu")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 caractere")]
        [DisplayName("Denumire produs")]
        public string Nume { get; set; } = string.Empty;

        /// <summary>
        /// Prețul produsului în MDL
        /// </summary>
        [Required(ErrorMessage = "Prețul produsului este obligatoriu")]
        [Range(0.01, 1000000, ErrorMessage = "Prețul trebuie să fie între 0.01 și 1,000,000 MDL")]
        [DisplayName("Preț (MDL)")]
        [DataType(DataType.Currency)]
        public decimal Pret { get; set; }

        /// <summary>
        /// Descrierea detaliată a produsului
        /// </summary>
        [DisplayName("Descriere")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Descrierea nu poate depăși 1000 de caractere")]
        public string Descriere { get; set; } = string.Empty;

        /// <summary>
        /// URL-ul imaginii produsului
        /// </summary>
        [Required(ErrorMessage = "URL-ul imaginii este obligatoriu")]
        [Url(ErrorMessage = "Vă rugăm să introduceți un URL valid")]
        [DisplayName("URL Imagine")]
        public string ImagineUrl { get; set; } = string.Empty;

        /// <summary>
        /// Categoria din care face parte produsul
        /// </summary>
        [Required(ErrorMessage = "Categoria este obligatorie")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Categoria trebuie să aibă între 2 și 50 caractere")]
        [DisplayName("Categorie")]
        public string Categorie { get; set; } = string.Empty;

        /// <summary>
        /// Cantitatea disponibilă în stoc
        /// </summary>
        [Required(ErrorMessage = "Cantitatea în stoc este obligatorie")]
        [Range(0, 10000, ErrorMessage = "Stocul trebuie să fie între 0 și 10,000")]
        [DisplayName("Stoc")]
        public int Stoc { get; set; } = 0;

        /// <summary>
        /// Data când a fost adăugat produsul
        /// </summary>
        [DisplayName("Data adăugării")]
        [DataType(DataType.DateTime)]
        public DateTime DataAdaugare { get; set; } = DateTime.Now;

        /// <summary>
        /// Indică dacă produsul este activ (vizibil în magazin)
        /// </summary>
        [DisplayName("Activ")]
        public bool EsteActiv { get; set; } = true;
    }
}
