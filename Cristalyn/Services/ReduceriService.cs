using System;
using System.Linq;
using Cristalyn.Models;
using Cristalyn.Data;
using Microsoft.EntityFrameworkCore;

namespace Cristalyn.Services
{
    public class ReduceriService
    {
        private readonly CristalynContext _context;
        private const int PUNCTE_PER_LEU = 1; // 1 punct pentru fiecare leu cheltuit
        private const decimal VALOARE_PUNCT = 1.0m; // 1 punct = 1 leu la folosire

        public ReduceriService(CristalynContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calculează prețul final al unui produs, aplicând toate reducerile disponibile
        /// </summary>
        public decimal CalculeazaPretFinal(Produs produs)
        {
            var pret = produs.Pret;
            
            // Verifică reduceri pentru categorie
            var reducereCategorie = _context.ReduceriCategorii
                .FirstOrDefault(r => r.Categorie == produs.Categorie && r.EsteActiva());
            
            if (reducereCategorie != null)
            {
                pret = reducereCategorie.CalculeazaPretRedus(pret);
            }

            return pret;
        }

        /// <summary>
        /// Adaugă puncte de fidelitate pentru o comandă
        /// </summary>
        public void AdaugaPuncteFidelitate(string emailUtilizator, decimal valoareComanda)
        {
            var puncte = (int)(valoareComanda * PUNCTE_PER_LEU);
            var puncteFidelitate = _context.PuncteFidelitate
                .FirstOrDefault(p => p.EmailUtilizator == emailUtilizator);

            if (puncteFidelitate == null)
            {
                puncteFidelitate = new PuncteFidelitate
                {
                    EmailUtilizator = emailUtilizator,
                    PuncteAcumulate = puncte
                };
                _context.PuncteFidelitate.Add(puncteFidelitate);
            }
            else
            {
                puncteFidelitate.PuncteAcumulate += puncte;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Folosește puncte de fidelitate pentru o reducere
        /// </summary>
        public decimal FolosestePuncte(string emailUtilizator, int puncteDorite)
        {
            var puncteFidelitate = _context.PuncteFidelitate
                .FirstOrDefault(p => p.EmailUtilizator == emailUtilizator);

            if (puncteFidelitate == null || puncteFidelitate.PuncteDisponibile < puncteDorite)
            {
                throw new InvalidOperationException("Nu există suficiente puncte disponibile");
            }

            puncteFidelitate.PuncteFolosite += puncteDorite;
            _context.SaveChanges();

            return puncteDorite * VALOARE_PUNCT;
        }

        /// <summary>
        /// Verifică dacă este prima comandă a utilizatorului
        /// </summary>
        public bool EstePrimaComanda(string emailUtilizator)
        {
            return !_context.Comenzi.Any(c => c.EmailUtilizator == emailUtilizator);
        }

        /// <summary>
        /// Obține reducerea pentru prima comandă (10%)
        /// </summary>
        public decimal CalculeazaReducerePrimaComanda(decimal valoareComanda)
        {
            return valoareComanda * 0.10m; // 10% reducere
        }

        /// <summary>
        /// Calculează toate reducerile disponibile pentru o comandă
        /// </summary>
        public (decimal reducereTotala, string detaliiReducere) CalculeazaToateReducerile(
            string emailUtilizator, 
            decimal valoareComanda, 
            string? codCupon = null,
            int puncteFolosite = 0)
        {
            decimal reducereTotala = 0;
            var detalii = new System.Text.StringBuilder();

            // 1. Verifică reducerea pentru prima comandă
            if (EstePrimaComanda(emailUtilizator))
            {
                var reducerePrimaComanda = CalculeazaReducerePrimaComanda(valoareComanda);
                reducereTotala += reducerePrimaComanda;
                detalii.AppendLine($"Reducere prima comandă: {reducerePrimaComanda:N2} MDL");
            }

            // 2. Aplică punctele de fidelitate
            if (puncteFolosite > 0)
            {
                try
                {
                    var reducerePuncte = FolosestePuncte(emailUtilizator, puncteFolosite);
                    reducereTotala += reducerePuncte;
                    detalii.AppendLine($"Reducere puncte fidelitate: {reducerePuncte:N2} MDL");
                }
                catch { /* Ignoră dacă nu sunt suficiente puncte */ }
            }

            // 3. Aplică cuponul
            if (!string.IsNullOrEmpty(codCupon))
            {
                var cupon = _context.Cupoane.FirstOrDefault(c => c.Cod == codCupon && c.EsteValid());
                if (cupon != null)
                {
                    var reducereCupon = cupon.EsteProcentual
                        ? valoareComanda * (cupon.Valoare / 100)
                        : cupon.Valoare;
                    reducereTotala += reducereCupon;
                    detalii.AppendLine($"Reducere cupon: {reducereCupon:N2} MDL");
                }
            }

            return (reducereTotala, detalii.ToString());
        }
    }
} 