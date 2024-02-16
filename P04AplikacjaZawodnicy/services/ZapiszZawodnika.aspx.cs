using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Services;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P04AplikacjaZawodnicy.services
{
    public partial class ZapiszZawodnika : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idZawodnikaStr = Request["id"];
            if (!string.IsNullOrEmpty(idZawodnikaStr))
            {
                
                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = Convert.ToInt32(idZawodnikaStr);
                z.Imie= Request["imie"];
                z.Nazwisko= Request["nazwisko"];
                z.Kraj= Request["kraj"];
                z.DataUrodzenia = Convert.ToDateTime(Request["dataUr"]);
                z.Wzrost = Convert.ToInt32(Request["wzrost"]);
                z.Waga = Convert.ToInt32(Request["waga"]);
                z.Id_trenera = Convert.ToInt32(Request["idTrenera"]);

                IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                mz.Edytuj(z);
            }
        }
    }
}