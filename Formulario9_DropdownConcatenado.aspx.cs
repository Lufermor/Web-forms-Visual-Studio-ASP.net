using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario9 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.DataTextField = "elEmpleado";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataSource = getAllEmpleados();
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Selecciona un Empleado", "-1"));
        }
    }

    private DataTable getAllEmpleados()
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getEmpleados", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        con.Close();
        return dt;
    }

    private DataTable getFichajesEmpleado(string idEmpleado)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getFichajesEmpleado", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add(new SqlParameter("@EmpleadoID", idEmpleado));
        DataTable dt = new DataTable();
        da.Fill(dt);

        con.Close();
        return dt;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "-1")
        {
            GridView1.DataSource = getFichajesEmpleado(DropDownList1.SelectedValue);
            GridView1.DataBind();
        }
    }
}