using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario15_VolcarXML_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*
     * Pre: ---
     * Post: Crea una conexión con la BBDD, carga los datos del XML en un DataSet.
     * Luego extrae las tablas del DataSet; crea los objetos SqlBulkCopy, mapea
     * las columnas de las tablas extraidas con las correspondientes en la BBDD, y 
     * finalmente envía y escribe la información en la BBDD
     * */
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Configuramos la conexión a la BBDD
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            DataSet ds = new DataSet();
            //Con esto se hace la lectura del xml
            ds.ReadXml(Server.MapPath("~/Datos/XML_Volcar_form15_2.xml"));
            //Creamos las DataTables que necesitaremos para pasarle al BulkCopy
            DataTable dtFich = ds.Tables["Fichaje"]; 
            DataTable dtEmpl = ds.Tables["Empleado"];

            //Para poder trabajar con el SqlBulkCopy, tenemos que abrir la conexión
            con.Open();

            //Creamos un SqlBulkCopy para insertar en la tabla Empleados
            using (SqlBulkCopy bc = new SqlBulkCopy(con))
            {
                //Mapeamos las columnas
                bc.DestinationTableName = "Empleados";
                bc.ColumnMappings.Add("Nombre", "Nombre");
                bc.ColumnMappings.Add("Apellido", "Apellido");
                bc.ColumnMappings.Add("Puesto", "Puesto");
                bc.ColumnMappings.Add("Fecha_alta", "Fecha_alta");
                bc.ColumnMappings.Add("Salario", "Salario");
                //Actualizamos en la BBDD:
                bc.WriteToServer(dtEmpl);

            }

            //Creamos un SqlBulkCopy para insertar en la tabla fichajes
            using (SqlBulkCopy bc = new SqlBulkCopy(con))
            {
                //Mapeamos las columnas
                bc.DestinationTableName = "Fichajes";
                bc.ColumnMappings.Add("empleado_id", "empleado_ID");
                bc.ColumnMappings.Add("Fecha", "fecha");
                bc.ColumnMappings.Add("Hora_entrada", "Hora_entrada");
                bc.ColumnMappings.Add("Hora_salida", "Hora_salida");
                //Actualizamos en la BBDD:
                bc.WriteToServer(dtFich);
            }
        }
    }
}