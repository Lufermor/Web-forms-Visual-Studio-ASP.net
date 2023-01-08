using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Formulario8 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.DataTextField = "TABLE_NAME";
            DropDownList1.DataValueField = "TABLE_NAME";
            DropDownList1.DataSource = getAllTablas();
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Selecciona tabla", "-1"));
        }
    }

    private DataTable getAllTablas()
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getTablas", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    private DataTable getMetadatosTabla(string nomTabla)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getMetaDatos", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add(new SqlParameter("@NomTabla", nomTabla));
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownList1.SelectedValue != "-1")
        {
            GridView1.DataSource = getMetadatosTabla(DropDownList1.SelectedValue);
            GridView1.DataBind();
        }
    }
}