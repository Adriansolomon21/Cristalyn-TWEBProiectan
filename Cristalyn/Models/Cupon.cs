using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cristalyn.Models
{
    /// <summary>
    /// Reprezintă un cupon de reducere pentru magazin
    /// </summary>
    public class Cupon
    {
        /// <summary>
        /// Identificatorul unic al cuponului
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Codul cuponului care va fi introdus de utilizator
        /// </summary>
        [Required(ErrorMessage = "Codul cuponului este obligatoriu")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Codul trebuie să aibă între 3 și 20 caractere")]
        [RegularExpression("^[A-Z0-9-]*$", ErrorMessage = "Codul poate conține doar litere mari, cifre și cratimă")]
        [DisplayName("Cod Cupon")]
        public string Cod { get; set; } = string.Empty;

        /// <summary>
        /// Valoarea reducerii (în procente sau sumă fixă)
        /// </summary>
        [Required(ErrorMessage = "Valoarea reducerii este obligatorie")]
        [Range(0.01, 100, ErrorMessage = "Valoarea trebuie să fie între 0.01 și 100")]
        [DisplayName("Valoare Reducere")]
        public decimal Valoare { get; set; }

        /// <summary>
        /// Indică dacă reducerea este în procente sau sumă fixă
        /// </summary>
        [Required(ErrorMessage = "Tipul reducerii este obligatoriu")]
        [DisplayName("Este Procentual")]
        public bool EsteProcentual { get; set; }

        /// <summary>
        /// Data de început a valabilității cuponului
        /// </summary>
        [Required(ErrorMessage = "Data de început este obligatorie")]
        [DataType(DataType.DateTime)]
        [DisplayName("Valid De La")]
        public DateTime ValidDeLa { get; set; }

        /// <summary>
        /// Data de sfârșit a valabilității cuponului
        /// </summary>
        [Required(ErrorMessage = "Data de sfârșit este obligatorie")]
        [DataType(DataType.DateTime)]
        [DisplayName("Valid Până La")]
        public DateTime ValidPanaLa { get; set; }

        /// <summary>
        /// Numărul maxim de utilizări permise pentru acest cupon
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Numărul de utilizări trebuie să fie cel puțin 1")]
        [DisplayName("Număr Maxim Utilizări")]
        public int? NumarMaximUtilizari { get; set; }

        /// <summary>
        /// Numărul de utilizări curente ale cuponului
        /// </summary>
        [DisplayName("Utilizări Curente")]
        public int UtilizariCurente { get; set; }

        /// <summary>
        /// Valoarea minimă a comenzii pentru care se poate aplica cuponul
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Valoarea minimă trebuie să fie mai mare sau egală cu 0")]
        [DisplayName("Valoare Minimă Comandă")]
        public decimal? ValoareMinimaComanda { get; set; }

        /// <summary>
        /// Verifică dacă cuponul este valid la data curentă
        /// </summary>
        public bool EsteValid()
        {
            var acum = DateTime.Now;
            return acum >= ValidDeLa && 
                   acum <= ValidPanaLa && 
                   (NumarMaximUtilizari == null || UtilizariCurente < NumarMaximUtilizari);
        }
    }
} 