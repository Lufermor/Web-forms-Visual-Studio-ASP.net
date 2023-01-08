using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formulario2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            deporte.Items.Add("Fútbol");
            deporte.Items.Add("Baloncesto");
        }
        
    }

    protected void deporte_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}