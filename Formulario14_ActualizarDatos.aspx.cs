using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Formulario14_ActualizarDatos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /**
     * Pre: ---
     * Post: Método que crea una conexión a la base de datos, realiza una consulta y 
     * rellena los textbox con los datos de la consulta
     */
    protected void botonBuscar_Click(object sender, EventArgs e)
    {
        //Configuramos la conexión a la BBDD
        string cs = ConfigurationManager.ConnectionStrings["CONEXION0"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        //Creamos la consulta:
        string sqlQuery = "SELECT * FROM Personas where ID = " + codPersona.Text;
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);

        DataSet ds = new DataSet();
        da.Fill(ds, "Persona");
        //Almacenamos la consulta y el data set en variables globales
        ViewState["consulta"] = sqlQuery;
        ViewState["dataset"] = ds;
        var filas = ds.Tables["Persona"].Rows;
        //Si la consulta fue exitosa, se rellenan los text boxes
        if (filas.Count > 0)
        {
            //Cargamos en un DataRow el registro obtenido en la consulta
            DataRow dr = filas[0];
            dniPersona.Text = dr["DNI"].ToString();
            nomPersona.Text = dr["Nombre"].ToString();
            fnPersona.Text = dr["fecha_nac"].ToString();
            //Cambiamos el color y texto del label mensaje
            Mensaje.ForeColor = System.Drawing.Color.Black;
            Mensaje.Text = "";
        }
        else
        {
            //Si la consulta no es exitosa, se muestra un mensaje de error al usuario
            Mensaje.ForeColor = System.Drawing.Color.Red;
            Mensaje.Text = "Persona no encontrada";
        }
        //Cerramos la conexión a la BBDD
        con.Close();
    }
    /**
     * Pre: ---
     * Post: Crea una conexión a la base de datos, obtiene los datos de una consulta,
     * configura el DataAdapter. Luego, obtiene la fila del dataset creado previamente,
     * modifica sus datos con los datos obtenidos de los TextBoxes y realiza el update.
     */
    protected void BotonActualizar_Click(object sender, EventArgs e)
    {
        //Se configura la conexión y el DataAdapter
        string cs = ConfigurationManager.ConnectionStrings["CONEXION0"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlDataAdapter da = new SqlDataAdapter((string)ViewState["consulta"], con);
        DataSet ds = (DataSet)ViewState["dataset"];

        /*Este CommandBuilder sirve para cablear los comandos necesarios 
         * para poder realizar un update */
        SqlCommandBuilder builder = new SqlCommandBuilder(da);

        //Nos aseguramos de que existe un registro resultado
        if(ds.Tables["Persona"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["Persona"].Rows[0];
            dr["DNI"] = dniPersona.Text;
            dr["Nombre"] = nomPersona.Text;
            dr["fecha_nac"] = fnPersona.Text;
        }
        else
        {
            //Se muestra un mensaje de error al usuario 
            Mensaje.ForeColor = System.Drawing.Color.Red;
            Mensaje.Text = "Persona no encontrada";
        }

        //Se actualiza el registro en la BBDD, esta operación devuelve un entero
        int filasActualizadas = da.Update(ds, "Persona");

        if(filasActualizadas > 0)
        {
            //Se muestra un mensaje al usuario
            Mensaje.ForeColor = System.Drawing.Color.Green;
            Mensaje.Text = "Persona Actualizada";
        }
        else
        {
            Mensaje.ForeColor = System.Drawing.Color.Red;
            Mensaje.Text = "Error en la actualización";
        }
        //Se cierra la conexión con la BBDD
        con.Close();
    }
}