using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formulario16_ActualizarTabla_NewField_11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddl = new DropDownList();
        //ddl.ID = "DropDownList1";
        //List<string> list = new List<string>() { "Seleccionar sexo", "Hombre", "Mujer" };
        //ddl.DataSource = list/* establece el origen de datos para el control DropDownList */;
        ////ddl.DataTextField = /* establece el campo de texto del origen de datos para el control DropDownList */;
        ////ddl.DataValueField = /* establece el campo de valor del origen de datos para el control DropDownList */;
        //ddl.DataBind();
    }

    protected void DropDownList1_PreRender(object sender, EventArgs e)
    {
        DropDownList ddl = (sender as DropDownList);
        if (!ddl.Items.Contains(new ListItem(ddl.SelectedValue)))
        {
            ddl.Items.Add(ddl.SelectedValue);
        }
    }
}