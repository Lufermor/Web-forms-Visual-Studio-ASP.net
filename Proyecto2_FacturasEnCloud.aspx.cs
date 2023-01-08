using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;
using System.IO;
using MySql.Data.MySqlClient;

/*
 * Esta clase se encarga de introducir los elementos que se cargan en 
 * los DropDownList, realizar el filtrado, obtener los datos de la BD
 * y mostrarlos por pantalla
 */
public partial class Proyecto2_FacturasEnCloud : System.Web.UI.Page
{
    // Variable con el nombre de la conexión en el Web.config
    //String myConection = "GOOGLE_CLOUD_CONEXION";
    String myConection = "MYSQLCONEXION1";

    /*
    * Pre: ---
    * Post: Este método realiza una conexión con la BD, obtiene los datos de
    * los diferentes elementos a filtrar y los carga en los DropDownLists, además 
    * añade a los DropDownLists valores por defecto.
    */
    private void cargarOpcionesDropDownLists()
    {
        // Se cargan los diferentes estados de las facturas en el DropDownList
        desEstadoFactura.Items.Clear();
        desEstadoFactura.DataTextField = "EstadoFactura";
        desEstadoFactura.DataValueField = "EstadoFactura";
        desEstadoFactura.DataSource = getEstadosFactura();
        desEstadoFactura.DataBind();
        // Se introduce en el DropDownList un valor por defecto
        desEstadoFactura.Items.Insert(0, new ListItem("-- Seleccionar estado de Factura --", "0"));

        // Se cargan los diferentes productos en el DropDownList
        desProductos.Items.Clear();
        desProductos.DataTextField = "Producto";
        desProductos.DataValueField = "Producto";
        desProductos.DataSource = getProductos();
        desProductos.DataBind();
        // Se introduce en el DropDownList un valor por defecto
        desProductos.Items.Insert(0, new ListItem("-- Seleccionar producto --", "0"));

        // Se cargan las diferentes monedas en el DropDownList
        desMoneda.Items.Clear();
        desMoneda.DataTextField = "Moneda";
        desMoneda.DataValueField = "Moneda";
        desMoneda.DataSource = getMonedas();
        desMoneda.DataBind();
        // Se introduce en el DropDownList un valor por defecto
        desMoneda.Items.Insert(0, new ListItem("-- Seleccionar moneda --", "0"));
    }

        /*
         * Pre: ---
         * Post: Este método realiza una conexión con la BD, efectúa la consulta que se le pasa, y 
         * devuelve el resultado en una DataTable
         */
        private DataTable executeConsulta(string query)
    {
        //Creamos la conection string con el nombre de nuestra conexión a la BD
        string cs = ConfigurationManager.ConnectionStrings[myConection].ConnectionString;
        //Establecemos la conexión
        MySqlConnection con = new MySqlConnection(cs);
        //Realizamos la consulta y guardamos el resultado en un DataAdapter
        MySqlDataAdapter da = new MySqlDataAdapter(query, con);
        //Creamos la DataTable
        DataTable dt = new DataTable();
        //Rellenamos el DataTable con los datos del DataAdapter
        da.Fill(dt);
        //Cerramos la conexión con la BD
        con.Close();
        //Devolvemos la DataTable y finalizamos el método.
        return dt;
    }

    /*
     * Pre: ---
     * Post: Este método llama al método executeQuery solicitando todas las facturas, y devuelve
     * la tabla resultado en un aDataTable
     * */
    private DataTable getAllFacturas()
    {
        return executeConsulta("SELECT * FROM facturas");
    }
    /*
     * Este método llama al método executeQuery, solicitando los estados de las facturas, 
     * y devuelve el resultado en una DataTable
     */
    private DataTable getEstadosFactura()
    {
        return executeConsulta("SELECT DISTINCT EstadoFactura FROM facturas");
    }

    /*
     * Este método llama al método executeQuery, solicitando los diferentes productos, 
     * y devuelve el resultado en una DataTable
     */
    private DataTable getProductos()
    {
        return executeConsulta("SELECT DISTINCT Producto FROM facturas");
    }

    /*
     * Este método llama al método executeQuery, solicitando las diferentes divisas, 
     * y devuelve el resultado en una DataTable
     */
    private DataTable getMonedas()
    {
        return executeConsulta("SELECT DISTINCT Moneda FROM facturas");
    }

    /*
     * Pre: ---
     * Post: Método que se invoca cuando se carga la página, carga los elementos
     * deseados, lee la información de la BD, configura los DropDownLists e imprime la información
     * por pantalla
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //Al cargar o refrescar la página, cargamos los datos de todas las facturas en el grid view
        //Indicamos la fuente de datos al GridView
        GridView1.DataSource = getAllFacturas();
        //Enlazamos los datos para que se muestren
        GridView1.DataBind();

        if (!IsPostBack) /*Esta condición se usa para evitar que los valores
                en los DropDownLists se dupliquen cada vez que ocurra un evento*/
        {   
            //Llamamos al método para cargar los DropDownLists
            cargarOpcionesDropDownLists();
        }
    }

    /*
     * Pre: ---
     * Post: Este método recibe los datos seleccionados en los desplegables, y mediante 
     * expresiones condicionales, establece la consulta adecuada a cada caso.
     * Finalmente llama al método para obtener datos de la BD basados en esta consulta, 
     * y carga los resultados en el GridView.
     */
    protected void FiltersChanged(object sender, EventArgs e)
    {
        //Dependiendo de los filtros que se hayan seleccionado, enviaremosuna query u otra
        string query = "";
        string estado = desEstadoFactura.SelectedItem.Value; // Aquí obtenemos el valor del desplegable.
        string producto = desProductos.SelectedItem.Value;  // Aquí obtenemos el valor del desplegable.
        string moneda = desMoneda.SelectedItem.Value;       // Aquí obtenemos el valor del desplegable.
        if (!estado.Equals("0") && !producto.Equals("0") && !moneda.Equals("0"))
        {
            /* Con la siguiente query obtenemos las filas cuyos valores de 
                EstadoFactura, Producto y Moneda, coinciden con los 
                seleccionados en los DropDownList*/
            query = "select * from facturas " +
                "where facturas.EstadoFactura='" + estado
                + "' and facturas.Producto='" + producto 
                + "' and facturas.Moneda='" + moneda + "'";
        }
        else if (!estado.Equals("0") && producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de estado de la factura desEstadoFactura */
            query = "select * from facturas " +
                "where facturas.EstadoFactura='" + estado + "'";
        }
        else if (estado.Equals("0") && !producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de producto desProducto */
            query = "select * from facturas " +
                "where facturas.Producto='" + producto + "'";
        }
        else if (estado.Equals("0") && producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de moneda desMoneda */
            query = "select * from facturas " +
                "where facturas.Moneda='" + moneda + "'";
        }
        else if (!estado.Equals("0") && !producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de estado de factura y producto*/
            query = "select * from facturas " +
                "where facturas.EstadoFactura='" + estado
                + "' and facturas.Producto='" + producto + "'";
        }
        else if (!estado.Equals("0") && producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de estado de factura y moneda*/
            query = "select * from facturas " +
                "where facturas.EstadoFactura='" + estado
                + "' and facturas.Moneda='" + moneda + "'";
        }
        else if (estado.Equals("0") && !producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de producto y moneda*/
            query = "select * from facturas " +
                "where facturas.Producto='" + producto
                + "' and facturas.Moneda='" + moneda + "'";
        }
        else /*Entramos a este else cuando no se selecciona 
              * ningún valor en ninguno de los DropDownList*/
        {
            query = "SELECT * FROM facturas";
        }
        // Llamamos al método para ejecutar la query y guardamos el resultado en el GridView
        GridView1.DataSource = executeConsulta(query);
        GridView1.DataBind();
        // Manejamos el caso de que la query no arroje ningún resultado, para notificarselo al usuario
        if (GridView1.Rows.Count == 0) Label1.Visible = true;
        else Label1.Visible = false;
    }

    /*
     * Pre: ---
     * Post: Este método es un evento que se ejecuta cuando el usuario da click en el 
     * botón modificar, envía los datos obtenidos del text box para modificar
     * la base de datos
     */
    protected void Modificar1_Click(object sender, EventArgs e)
    {
        //Creamos la query
        string query = "show tables";
        string modificar = "ALTER TABLE facturas UPDATE ";
        //Creamos la conectio string con el nombre de nuestra conexión a la BD
        string cs = ConfigurationManager.ConnectionStrings[myConection].ConnectionString;
        //Establecemos la conexión
        MySqlConnection con = new MySqlConnection(cs);
        //Realizamos la consulta y guardamos el resultado en un DataAdapter
        MySqlDataAdapter da = new MySqlDataAdapter(query, con);
        //Creamos la DataTable
        DataTable dt = new DataTable();
        //Rellenamos el DataTable con los datos del DataAdapter
        da.Fill(dt);
        //Cerramos la conexión con la BD
        con.Close();
    }

    /*
     * Pre: ---
     * Post: Este método es un evento que se ejecuta cuando el usuario da click en el 
     * botón añadir, su función es poner visible el div con los text box para escribir 
     * los datos del nuevo registro
     */
    protected void AddRegistro1_Click(object sender, EventArgs e)
    {
        DivAddRegistro1.Visible = true;
        AddRegistro1.Visible = false;
    }

    /*
     * Pre: ---
     * Post: Este método es un evento que se ejecuta cuando el usuario da click en el 
     * botón añadir, su función es crear la query para añadir el nuevo registro y 
     * enviarla a la base de datos, finalmente cambia los valores de visibilidad de
     * DivAddRegistro1 y AddRegistro1
     */
    protected void ButtonAddRegistro2_Click(object sender, EventArgs e)
    {
        //nFactura	nombreMiEmpresa	fechaFactura	CIFCliente	nombreCliente	FechaCobro	EstadoFactura	TelefonoCliente	addressCliente	CPCliente	PaisCliente	CiudadCliente	correoCliente	Producto	ImporteBruto	ImporteIVA	Total	Moneda
        string insertar = "INSERT INTO facturas(nFactura,nombreMiEmpresa,fechaFactura," +
            "CIFCliente,nombreCliente,FechaCobro,EstadoFactura,TelefonoCliente," +
            "addressCliente,CPCliente,PaisCliente,CiudadCliente,correoCliente," +
            "Producto,ImporteBruto,ImporteIVA,Total,Moneda) " +
            "values(@nFactura,@nombreMiEmpresa,@fechaFactura," +
            "@CIFCliente,@nombreCliente,@FechaCobro,@EstadoFactura,@TelefonoCliente," +
            "@addressCliente,@CPCliente,@PaisCliente,@CiudadCliente,@correoCliente," +
            "@Producto,@ImporteBruto,@ImporteIVA,@Total,@Moneda)";
        //Creamos la conectio string con el nombre de nuestra conexión a la BD
        string cs = ConfigurationManager.ConnectionStrings[myConection].ConnectionString;
        //Establecemos la conexión
        MySqlConnection con = new MySqlConnection(cs);
        //Creamos un comando Mysql
        MySqlCommand cmd = new MySqlCommand(insertar, con);
        cmd.Parameters.AddWithValue("@nFactura", TextBoxnFactura2.Text);
        cmd.Parameters.AddWithValue("@nombreMiEmpresa", TextBoxnombreMiEmpresa2.Text);
        cmd.Parameters.AddWithValue("@fechaFactura", TextBoxfechaFactura2.Text);
        cmd.Parameters.AddWithValue("@CIFCliente", TextBoxCIFCliente2.Text);
        cmd.Parameters.AddWithValue("@nombreCliente", TextBoxnombreCliente2.Text);
        cmd.Parameters.AddWithValue("@FechaCobro", TextBoxFechaCobro2.Text);
        cmd.Parameters.AddWithValue("@EstadoFactura", TextBoxEstadoFactura2.Text);
        cmd.Parameters.AddWithValue("@TelefonoCliente", TextBoxTelefonoCliente2.Text);
        cmd.Parameters.AddWithValue("@addressCliente", TextBoxaddressCliente2.Text);
        cmd.Parameters.AddWithValue("@CPCliente", TextBoxCPCliente2.Text);
        cmd.Parameters.AddWithValue("@PaisCliente", TextBoxPaisCliente2.Text);
        cmd.Parameters.AddWithValue("@CiudadCliente", TextBoxCiudadCliente2.Text);
        cmd.Parameters.AddWithValue("@correoCliente", TextBoxcorreoCliente2.Text);
        cmd.Parameters.AddWithValue("@Producto", TextBoxProducto2.Text);
        cmd.Parameters.AddWithValue("@ImporteBruto", TextBoxImporteBruto2.Text);
        cmd.Parameters.AddWithValue("@ImporteIVA", );
        cmd.Parameters.AddWithValue("@Total", );
        cmd.Parameters.AddWithValue("@Moneda", TextBoxMoneda2.Text);
        DivAddRegistro1.Visible = false;
        AddRegistro1.Visible = true;
        cargarOpcionesDropDownLists();
    }

    /*
     * Pre: ---
     * Post: Este método es un evento que se ejecuta cuando el usuario da click en el 
     * botón cancelar, en el formulario de añadir registro, su función es 
     * poner invisible el div con los text box para escribir 
     * los datos del nuevo registro, y poner visible el div de AddRegistro1
     * En caso de que el usuario quiera hacer un nuevo intento de añadir registro.
     */
    protected void ButtonCancelar2_Click(object sender, EventArgs e)
    {
        DivAddRegistro1.Visible = false;
        AddRegistro1.Visible = true;
    }


}