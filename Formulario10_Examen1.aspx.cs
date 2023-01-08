using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Formulario10_Examen1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.DataTextField = "NameAlumno";
            DropDownList1.DataValueField = "IdAlumno";
            DropDownList1.DataSource = getAlumnosActivos();
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Seleccione un alumno", "-1"));
        }
    }

    private DataTable getAlumnosActivos()
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION_EXAMEN"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getAlumnos", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    private DataTable getAsignaturasAlumnos(string idMyAlumno)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION_EXAMEN"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        SqlDataAdapter da = new SqlDataAdapter("getAsignaturaAlumno", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add(new SqlParameter("@idMyAlumno", idMyAlumno));
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "-1")
        {
            GridView1.DataSource = getAsignaturasAlumnos(DropDownList1.SelectedValue);
            GridView1.DataBind();
        }
    }

}