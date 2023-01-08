using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario13HorasTrabajadasMes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        string command = "select ID, concat(Nombre, ' ', Apellido) as Nombre_Empleado, fich.mes, fich.Horas_trabajadas " +
            "FROM Empleados " +
            "Join " +
            "(select empleado_ID, MONTH(fecha) as mes, " +
            "sum(DATEDIFF(hour, Hora_entrada, Hora_salida)) as Horas_trabajadas " +
            "FROM Fichajes " +
            "group by MONTH(fecha), empleado_ID) as fich " +
            "on Empleados.ID = fich.empleado_ID ";

        SqlCommand cmd = new SqlCommand(command, con);

        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Nombre");
        dt.Columns.Add("Mes");
        dt.Columns.Add("Horas Trabajadas");

        con.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = rdr["ID"];
            dr["Nombre"] = rdr["Nombre_Empleado"];
            dr["Mes"] = rdr["mes"];
            dr["Horas trabajadas"] = rdr["Horas_trabajadas"];
            //dr["Fecha de nacimiento"] = ((DateTime)rdr["fecha_nac"]).ToShortDateString();
            //dr["Mayor de edad"] = EsMayor((DateTime)rdr["fecha_nac"]);

            dt.Rows.Add(dr);
        }
        con.Close();

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}