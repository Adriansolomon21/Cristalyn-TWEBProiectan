using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    /// <summary>
    /// Represents an item in the shopping cart
    /// </summary>
    public class CosItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product ID associated with this cart item
        /// </summary>
        [Required(ErrorMessage = "ID-ul produsului este obligatoriu")]
        public int ProdusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product
        /// </summary>
        [Required(ErrorMessage = "Numele produsului este obligatoriu")]
        public string Nume { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price per unit of the product
        /// </summary>
        [Required(ErrorMessage = "Prețul produsului este obligatoriu")]
        [Range(0, double.MaxValue, ErrorMessage = "Prețul trebuie să fie mai mare decât 0")]
        public decimal Pret { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in cart
        /// </summary>
        [Required(ErrorMessage = "Cantitatea este obligatorie")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantitatea trebuie să fie cel puțin 1")]
        public int Cantitate { get; set; }

        /// <summary>
        /// Gets the total price for this cart item (Price * Quantity)
        /// </summary>
        public decimal Total => Pret * Cantitate;
    }
}
