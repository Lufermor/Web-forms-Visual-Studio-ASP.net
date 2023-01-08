using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario12MayorDeEdad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION0"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlCommand cmd = new SqlCommand("select * from Personas", con);

        DataTable dt = new DataTable();
        dt.Columns.Add("DNI");
        dt.Columns.Add("Nombre");
        dt.Columns.Add("Cod. Provincia");
        dt.Columns.Add("Fecha de nacimiento");
        dt.Columns.Add("Mayor de edad");

        con.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            DataRow dr = dt.NewRow();
            dr["DNI"] = rdr["DNI"];
            dr["Nombre"] = rdr["Nombre"];
            dr["Cod. Provincia"] = rdr["provincia"];
            dr["Fecha de nacimiento"] = ((DateTime)rdr["fecha_nac"]).ToShortDateString();
            dr["Mayor de edad"] = EsMayor((DateTime)rdr["fecha_nac"]);

            dt.Rows.Add(dr);
        }
        con.Close();

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    private string EsMayor(DateTime fNac)
    {
        double diferenciaEnDias = (DateTime.Now - fNac).Days;
        if (diferenciaEnDias >= (22 * 365)) return "Sí";
        else return "No";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "No") 
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
        }
    }
}