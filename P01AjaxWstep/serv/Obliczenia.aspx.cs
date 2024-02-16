using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P01AjaxWstep.serv
{
    public partial class Obliczenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string liczba1Str = Request["liczba1"];

            string liczba2Str = Request["liczba2"];

            int wynik =
                Convert.ToInt32(liczba1Str) + Convert.ToInt32(liczba2Str);

            Response.Write(wynik);
        }
    }
}