using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă o reducere aplicată unei categorii de produse
    /// </summary>
    public class ReducereCategorie
    {
        /// <summary>
        /// Identificatorul unic
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numele categoriei
        /// </summary>
        [Required(ErrorMessage = "Categoria este obligatorie")]
        [DisplayName("Categorie")]
        public string Categorie { get; set; } = string.Empty;

        /// <summary>
        /// Procentul de reducere
        /// </summary>
        [Required(ErrorMessage = "Procentul de reducere este obligatoriu")]
        [Range(1, 90, ErrorMessage = "Reducerea trebuie să fie între 1% și 90%")]
        [DisplayName("Procent Reducere")]
        public int ProcentReducere { get; set; }

        /// <summary>
        /// Data de început a reducerii
        /// </summary>
        [Required(ErrorMessage = "Data de început este obligatorie")]
        [DisplayName("Valabil De La")]
        public DateTime ValidDeLa { get; set; }

        /// <summary>
        /// Data de sfârșit a reducerii
        /// </summary>
        [Required(ErrorMessage = "Data de sfârșit este obligatorie")]
        [DisplayName("Valabil Până La")]
        public DateTime ValidPanaLa { get; set; }

        /// <summary>
        /// Descrierea reducerii
        /// </summary>
        [StringLength(200, ErrorMessage = "Descrierea nu poate depăși 200 de caractere")]
        [DisplayName("Descriere")]
        public string? Descriere { get; set; }

        /// <summary>
        /// Verifică dacă reducerea este activă la data curentă
        /// </summary>
        public bool EsteActiva()
        {
            var acum = DateTime.Now;
            return acum >= ValidDeLa && acum <= ValidPanaLa;
        }

        /// <summary>
        /// Calculează prețul redus
        /// </summary>
        public decimal CalculeazaPretRedus(decimal pretOriginal)
        {
            if (!EsteActiva()) return pretOriginal;
            var reducere = pretOriginal * ProcentReducere / 100m;
            return pretOriginal - reducere;
        }
    }
} 