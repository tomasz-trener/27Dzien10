using P03Zawodnicy.Shared.Domain;
using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Data;
using P04Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04Zawodnicy.Shared.Services
{
    public class ManagerZawodnikowLINQ : IManagerZawodnikow
    {
        //string connString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=A_Zawodnicy;Integrated Security=True;Encrypt=False";
        string connString;
        public ManagerZawodnikowLINQ()
        {
            connString =ConfigurationManager.ConnectionStrings["A_ZawodnicyConnectionString"].ConnectionString;
        }
        public void Dodaj(Zawodnik z)
        {
            var zd = new ZawodnikDb();
            mapujNaZawodnikaDb(z, zd);

            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            db.ZawodnikDb.InsertOnSubmit(zd);
            db.SubmitChanges();
            z.Id_zawodnika = zd.id_zawodnika;
        }

        public void Edytuj(Zawodnik edytowany)
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            ZawodnikDb zd = db.ZawodnikDb.FirstOrDefault(x => x.id_zawodnika == edytowany.Id_zawodnika);
            mapujNaZawodnikaDb(edytowany, zd);
            db.SubmitChanges();
        }

        public string[] PodajKraje()
        {
            return new ModelBazyDataContext(connString)
                .ZawodnikDb
                .GroupBy(x => x.kraj)
                .Select(x => x.Key)
                .ToArray();
        }

        public double PodajSredniWiekZawodnikow(string kraj)
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);

            double sredniWiek=  db.ZawodnikDb
                .Where(x => x.kraj.Equals(kraj))
                .Select(x => DateTime.Now.Year - x.data_ur.Value.Year)
                .Average();

            return sredniWiek;
        }

        public double PodajSredniWzrost(string kraj)
        {
            return new ModelBazyDataContext(connString)
               .ZawodnikDb
               .Where(x => x.kraj == kraj)
               .Average(x => x.wzrost).Value;
        }

        public Trener[] PodajTrenerow()
        {
            var trenerzyDb = new ModelBazyDataContext(connString).TrenerDb.ToArray();

            var trenerzy = trenerzyDb.Select(x => new Trener
            {
                Id = x.id_trenera,
                Imie = x.imie_t,
                Nazwisko = x.nazwisko_t,
                DataUrodzenia = x.data_ur_t,

            }).ToArray();

            return trenerzy;
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            var zawodnicyDb = new ModelBazyDataContext(connString)
                .ZawodnikDb
                .Where(x => x.kraj == kraj)
                .ToArray();

            return mapujZawodnikow(zawodnicyDb);
        }

        public void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy)
        {
            posortowaniZawodnicy = posortowaniZawodnicy.OrderBy(x=>x.Nazwisko).ToArray();
        }

        public void Usun(int id)
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            var usuwany = db.ZawodnikDb.FirstOrDefault(x=>x.id_zawodnika == id);
            db.ZawodnikDb.DeleteOnSubmit(usuwany);
            db.SubmitChanges();
        }

        public Zawodnik[] WczytajZawodnikow()
        {
            var zawodnicyDb = new ModelBazyDataContext(connString).ZawodnikDb.ToArray();
            return mapujZawodnikow(zawodnicyDb);
        }

        public List<Osoba> WyszukajOsoby(string fragmentNazwy)
        {
            var db = new ModelBazyDataContext(connString);

            var zawodnicy = db.ZawodnikDb
                .Where(x => x.imie.Contains(fragmentNazwy) || x.nazwisko.Contains(fragmentNazwy))
                .Select(x => new Osoba
                {
                    Imie = x.imie,
                    Nazwisko = x.nazwisko,
                    DataUrodzenia = x.data_ur
                }).ToArray();

            var trenerzy  = db.TrenerDb
              .Where(x => x.imie_t.Contains(fragmentNazwy) || x.nazwisko_t.Contains(fragmentNazwy))
              .Select(x => new Osoba
              {
                  Imie = x.imie_t,
                  Nazwisko = x.nazwisko_t,
                  DataUrodzenia = x.data_ur_t
              }).ToArray();

            return zawodnicy.Concat(trenerzy).ToList();

        }

        private Zawodnik[] mapujZawodnikow(params ZawodnikDb[] dane)
        {
            Zawodnik[] tab = new Zawodnik[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                tab[i] = new Zawodnik()
                {
                    Id_zawodnika = dane[i].id_zawodnika,
                    Id_trenera = dane[i].id_trenera,
                    Imie = dane[i].imie,
                    Nazwisko = dane[i].nazwisko,
                    Kraj = dane[i].kraj,
                    DataUrodzenia = dane[i].data_ur,
                    Wzrost = (int)dane[i].wzrost,
                    Waga = (int)dane[i].waga,
                };
            }
            return tab;
        }

        private void mapujNaZawodnikaDb(Zawodnik z, ZawodnikDb zdb)
        {
            zdb.id_zawodnika = z.Id_zawodnika;
            zdb.imie = z.Imie;
            zdb.nazwisko = z.Nazwisko;
            zdb.kraj = z.Kraj;
            zdb.data_ur = z.DataUrodzenia;
            zdb.wzrost = z.Wzrost;
            zdb.waga = z.Waga;
            zdb.id_trenera = z.Id_trenera;
        }

        public GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju()
        {
            return 
                new ModelBazyDataContext(connString)
                .ZawodnikDb
                .GroupBy(x=>x.kraj)
                .Select(x=> new GrupaKraju
                {
                    Kraj = x.Key,
                    SredniWzrost = x.Average(y=>y.wzrost).Value
                }).ToArray();
        }

        public Zawodnik PodajZawodnika(int id)
        {
            var z = new ModelBazyDataContext(connString).ZawodnikDb.FirstOrDefault(x => x.id_zawodnika == id);
            return mapujZawodnikow(z).FirstOrDefault();
        }

        public Zawodnik[] PodajZawodnikowFiltr(string szukanaFraza)
        {
            szukanaFraza = szukanaFraza.ToLower();
            var db = new ModelBazyDataContext(connString);

            var zawodnicy = db.ZawodnikDb
                .Where(x =>
                    x.imie.ToLower().Contains(szukanaFraza) ||
                    x.nazwisko.ToLower().Contains(szukanaFraza) ||
                    x.kraj.ToLower().Contains(szukanaFraza) ||
                    (x.data_ur.HasValue && x.data_ur.Value.Year.ToString().Contains(szukanaFraza)) ||
                    (x.data_ur.HasValue && x.data_ur.Value.Month.ToString().Contains(szukanaFraza)) ||
                    (x.data_ur.HasValue && x.data_ur.Value.Day.ToString().Contains(szukanaFraza)) ||
                    (x.wzrost.HasValue && x.wzrost.ToString().Contains(szukanaFraza)) ||
                    (x.waga.HasValue && x.waga.ToString().Contains(szukanaFraza)))
                .ToArray();


            //teraz działa bo to polecenie wykonuje się lokalnie 
           // zawodnicy = zawodnicy.Where(x => (x.data_ur.HasValue && x.data_ur.Value.ToString("ddMMyyyy").Contains(szukanaFraza))).ToArray();
            // gdy korzystamy z niestandardowej funkcji w c#, która nie ma odpowiednika w sql to możemy to polecenie wykonać lokalnie 
            // to nie zdziała bo ten filtr zadziała jak AND a my chcemy skonstrruować jak operator OR
            return mapujZawodnikow(zawodnicy);
        }
    }
}
