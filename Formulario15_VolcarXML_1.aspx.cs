using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario15_VolcarXML_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Configuramos la conexión a la BBDD
        string cs = ConfigurationManager.ConnectionStrings["CONEXION0"].ConnectionString;
        using(SqlConnection con = new SqlConnection(cs))
        {
            DataSet ds = new DataSet();
            //Con esto se hace la lectura del xml
            ds.ReadXml(Server.MapPath("~/Datos/XML_Volcar_form15.xml"));
            //Creamos las DataTables que necesitaremos para pasarle al BulkCopy
            DataTable dtProv = ds.Tables["Provincia"];
            DataTable dtPer = ds.Tables["Persona"];

            //Para poder trabajar con el SqlBulkCopy, tenemos que abrir la conexión
            con.Open();

            //Creamos un SqlBulkCopy para insertar en la tabla personas 
            using (SqlBulkCopy bc = new SqlBulkCopy(con))
            {
                //Mapeamos las columnas
                bc.DestinationTableName = "Personas";
                bc.ColumnMappings.Add("ID", "ID");
                bc.ColumnMappings.Add("DNI", "DNI");
                bc.ColumnMappings.Add("Nombre", "Nombre");
                bc.ColumnMappings.Add("Provincia", "provincia");
                //Actualizamos en la BBDD:
                bc.WriteToServer(dtPer);
                
            }

            //Creamos un SqlBulkCopy para insertar en la tabla provincias
            using (SqlBulkCopy bc = new SqlBulkCopy(con))
            {
                //Mapeamos las columnas
                bc.DestinationTableName = "Provincias";
                bc.ColumnMappings.Add("ID", "ID");
                bc.ColumnMappings.Add("nomProvincia", "nomProvincia");
                //Actualizamos en la BBDD:
                bc.WriteToServer(dtProv);
            }
        }
    }
}