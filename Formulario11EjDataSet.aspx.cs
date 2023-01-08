using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario11EjDataSet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["CONEXION0"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter("conjunto", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            da.Fill(ds);

            ds.Tables[0].TableName = "Personas";
            ds.Tables[1].TableName = "Provincias";

            GridView1.DataSource = ds.Tables["Personas"];
            GridView2.DataSource = ds.Tables["Provincias"];
            GridView1.DataBind();
            GridView2.DataBind();
        }
    }
}