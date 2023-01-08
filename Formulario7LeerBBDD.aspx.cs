using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class Formulario7LeerBBDD : System.Web.UI.Page
{
    StringBuilder errorMessages = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Ejemplar = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;

        SqlConnection con = new SqlConnection(Ejemplar);
        SqlDataAdapter da = new SqlDataAdapter("Select * from Empleados", con);

        DataSet ds = new DataSet();
        
        try
        {
            da.Fill(ds);
        }
        catch (SqlException ex)
        {
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages.Append("Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n");
            }
            Console.WriteLine(errorMessages.ToString());
        }

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}