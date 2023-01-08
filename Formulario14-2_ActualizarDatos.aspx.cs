using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/* Clase que controla la conexión con la BBDD,  obtiene datos de los registros para 
 * mostrárselos al usuario. También obtiene datos introducidos por el usuario para 
 * modificar la BBDD*/
public partial class Formulario14_2_ActualizarDatos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*Realizamos esto para asegurarnos de que hay un entero por defecto en el textbox,
         * de esta manera evitamos el error en caso de que el usuario no introduzca nada*/
        if (codEmpleado.Text.Trim() == "") codEmpleado.Text = "0";
    }

    /* Pre: --- 
     * Post: Este método crea una conexión con la BBDD, Realiza una consulta, la guarda
     * en una variable global. Si la consutla obtiene un resultado, relleta los 
     * TextBoxes con los resultados de la consulta. En caso contrario muestra
     * un mensaje de error al usuario. */
    protected void botonBuscar_Click(object sender, EventArgs e)
    {
        //Configura la conexión con la BBDD
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        //Establecemos la consulta
        string sqlQuery = "SELECT * FROM Empleados where ID = " + codEmpleado.Text;
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);

        DataSet ds = new DataSet();
        //El resultado de la consulta se almacena en el DataSet.
        da.Fill(ds, "Empleado");
        //Se crean variables globales para guardar datos.
        ViewState["consulta"] = sqlQuery;
        ViewState["dataset"] = ds;

        var filas = ds.Tables["Empleado"].Rows;
        if (filas.Count > 0)
        {
            //Se rellenan los TextBoxes con los resultados de la consulta
            DataRow dr = filas[0];
            nombreEmpleado.Text = dr["Nombre"].ToString();
            apellidoEmpleado.Text = dr["Apellido"].ToString();
            puestoEmpleado.Text = dr["Puesto"].ToString();
            fechaAlta.Text = dr["Fecha_alta"].ToString();
            salario.Text = dr["Salario"].ToString();

            //Se modifica el contenido del label.
            Mensaje.ForeColor = System.Drawing.Color.Black;
            Mensaje.Text = "";
        }
        else
        {
            //Si no se ha obtenido ningún resultado, se muestra un mensaje de error al usuario
            Mensaje.ForeColor = System.Drawing.Color.Red;
            Mensaje.Text = "Empleado no encontrado";
        }
        //Se cierra la conexión con la BBDD
        con.Close();
    }

    /*
     * Pre: ---
     * Post:Se activa cuando el usuario da click en el  BotonActualizar
     * Crea una conexión a la base de datos, obtiene los datos de una consulta,
     * configura el DataAdapter. Luego, obtiene la fila del dataset creado previamente,
     * modifica sus datos con los datos obtenidos de los TextBoxes y realiza el update.
     */
    protected void BotonActualizar_Click(object sender, EventArgs e)
    {
        //Se configura la conexión y el DataAdapter
        string cs = ConfigurationManager.ConnectionStrings["CONEXION1"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlDataAdapter da = new SqlDataAdapter((string)ViewState["consulta"], con);
        DataSet ds = (DataSet)ViewState["dataset"];

        /*Este CommandBuilder sirve para cablear los comandos necesarios 
         * para poder realizar un update */
        SqlCommandBuilder builder = new SqlCommandBuilder(da);

        //Nos aseguramos de que existe un registro resultado
        if (ds.Tables["Empleado"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["Empleado"].Rows[0];
            dr["Nombre"] = nombreEmpleado.Text;
            dr["Apellido"] = apellidoEmpleado.Text;
            dr["Puesto"] = puestoEmpleado.Text;
            dr["Fecha_alta"] = fechaAlta.Text;
            dr["Salario"] = salario.Text;

            Mensaje.ForeColor = System.Drawing.Color.Black;
            Mensaje.Text = "";
        }
        else
        {
            //Se muestra un mensaje de error al usuario 
            Mensaje.ForeColor = System.Drawing.Color.Red;
            Mensaje.Text = "Persona no encontrada";
        }

        //Se actualiza el registro en la BBDD, esta operación devuelve un entero
        int filasActualizadas = da.Update(ds, "Empleado");
        if (filasActualizadas > 0)
        {
            //Se muestra un mensaje al usuario
            Mensaje.ForeColor = System.Drawing.Color.Green;
            Mensaje.Text = "Empleado Actualizado";
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