using Cristalyn.Models;

namespace Cristalyn.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CristalynContext context)
        {
            if (context.Produse.Any())
                return;

            var produse = new List<Produs>
            {
                new Produs {
                    Nume = "Medalion inimă argintiu",
                    Pret = 4800,
                    Categorie = "Pandora Moments",
                    ImagineUrl = "/img/produse/img1.jpg"
                },
                new Produs {
                    Nume = "Inel cu diamant rotund",
                    Pret = 12500,
                    Categorie = "Pandora Moments",
                    ImagineUrl = "/img/produse/img2.jpg"
                },
                new Produs {
                    Nume = "Inel floral cu pietre",
                    Pret = 8900,
                    Categorie = "Pandora ME",
                    ImagineUrl = "/img/produse/img3.jpg"
                },
                new Produs {
                    Nume = "Colier cu cercuri duble",
                    Pret = 6200,
                    Categorie = "Pandora ME",
                    ImagineUrl = "/img/produse/img4.jpg"
                },
                new Produs {
                    Nume = "Brățară pătrată minimal",
                    Pret = 3100,
                    Categorie = "Pandora ME",
                    ImagineUrl = "/img/produse/img5.jpeg"
                },
                new Produs {
                    Nume = "Brățară aur roz elegantă",
                    Pret = 7300,
                    Categorie = "Disney x Pandora",
                    ImagineUrl = "/img/produse/img6.jpg"
                },
                new Produs {
                    Nume = "Inel safir albastru",
                    Pret = 8100,
                    Categorie = "Pandora Moments",
                    ImagineUrl = "/img/produse/inel safir.jpg"
                }
            };

            context.Produse.AddRange(produse);
            context.SaveChanges();
        }
    }
}
