using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Services;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P04AplikacjaZawodnicy.services
{
    public partial class Wyszukiwarka : System.Web.UI.Page
    {
        public Zawodnik[] Zawodnicy { get; set; } = new Zawodnik[0];
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();

            string szukanaFraza = Request["fraza"];

            if (!string.IsNullOrEmpty(szukanaFraza))
                Zawodnicy = mz.PodajZawodnikowFiltr(szukanaFraza);
            else
                Zawodnicy = mz.WczytajZawodnikow();
        }
    }
}