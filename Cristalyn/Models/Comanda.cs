using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cristalyn.Models
{
    public class Comanda
    {
        public int Id { get; set; }

        [Required]
        public string? EmailUtilizator { get; set; }

        public DateTime Data { get; set; }

        [Required]
        public string? Status { get; set; } = "Nouă";

        public decimal Total { get; set; }

        // Pentru a vedea produsele din comandă
        public List<CosItem> Produse { get; set; } = new();
    }
}
