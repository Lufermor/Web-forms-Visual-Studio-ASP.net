using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public partial class GestorFacturas : System.Web.UI.Page
{
    // Variable con el nombre de la conexión en el Web.config, si se pone
    // CONEXION_LOCAL se cargarán los datos de manera local.
    String conexionBaseDatos = "CONEXION_GOOGLE_CLOUD";

    /**
     * Este método se encarga de cargar todas las facturas que existen en la BD, ya sea
     * en modo local o en la nube.
     */
    private DataSet getAllFacturas()
    {
        // Cogemos la conexión del Web.config
        string ejemplar = ConfigurationManager.ConnectionStrings[conexionBaseDatos].ConnectionString;
        MySqlConnection con = new MySqlConnection(ejemplar);
        // Creamos el data set
        DataSet ds = new DataSet();
        // Creamos la Select para obtener todas las facturas que existen en la BD
        MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM facturas", con);
        // Rellenamos el data set
        da.Fill(ds);
        // Cerramos la conexion
        con.Close();
        // Lo devolvemos
        return ds;
    }

    /**
     * Este método se encarga de obtener de la BD, ya sea en local o en la nube, los diferentes
     * estados de las facturas para poder cargarlos en el DropDown.
     */
    private DataSet getEstadosFactura()
    {
        // Cogemos la conexión del Web.config
        string ejemplar = ConfigurationManager.ConnectionStrings[conexionBaseDatos].ConnectionString;
        MySqlConnection con = new MySqlConnection(ejemplar);
        // Creamos el data set
        DataSet ds = new DataSet();
        // Creamos la Select para obtener todos los estados de factura que existen en la BD
        MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT estado_factura FROM facturas", con);
        // Rellenamos el data set
        da.Fill(ds);
        // Cerramos la conexion
        con.Close();
        // Lo devolvemos
        return ds;
    }

    /**
     * Este método se encarga de obtener de la BD, ya sea en local o en la nube, las diferentes
     * poblaciones de las facturas para poder cargarlos en el DropDown.
     */
    private DataSet getPoblaciones()
    {
        // Cogemos la conexión del Web.config
        string ejemplar = ConfigurationManager.ConnectionStrings[conexionBaseDatos].ConnectionString;
        MySqlConnection con = new MySqlConnection(ejemplar);
        // Creamos el data set
        DataSet ds = new DataSet();
        // Creamos la Select para obtener todas las poblaciones que existen en la BD
        MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT poblacion FROM facturas", con);
        // Rellenamos el data set
        da.Fill(ds);
        // Cerramos la conexion
        con.Close();
        // Lo devolvemos
        return ds;
    }

    /**
     * Este método es el que se inicia cuando se carga la página y se encarga de
     * rellenar los datos de los DropDown y de cargar en el GridView todas las
     * facturas apoyándose en los métodos getEstadosFactura(), getPoblaciones() y
     * getAllFacturas().
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        // Si es la primera vez que se carga la página
        if (!IsPostBack)
        {
            // Rellenamos el DropDownList de estado_factura
            DropDownList1.DataTextField = "estado_factura";
            DropDownList1.DataValueField = "estado_factura";
            DropDownList1.DataSource = getEstadosFactura();
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Filtrar por Estado", "-1"));

            // Rellenamos el DropDownList de poblacion
            DropDownList2.DataTextField = "poblacion";
            DropDownList2.DataValueField = "poblacion";
            DropDownList2.DataSource = getPoblaciones();
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("Filtrar por Población", "-1"));

            // Rellenamos el GridView con los datos de todas las facturas
            GridView1.DataSource = getAllFacturas();
            GridView1.DataBind();
        }
    }

    /**
     * Este método se encarga de aplicar los filtros seleccionados por el usuario.
     * Según los valores introducidos en los DropDown se forma una select diferente.
     */
    protected void aplicarFiltros(object sender, EventArgs e)
    {
        // Formamos el select en función de los filtros que se hayan aplicado
        String selectFiltros = "";
        // Si se seleccionan ambos filtros
        if (DropDownList1.SelectedValue != "-1" && DropDownList2.SelectedValue != "-1")
        {
            selectFiltros = "select * from facturas where facturas.estado_factura='" + DropDownList1.SelectedValue
                + "' and facturas.poblacion='" + DropDownList2.SelectedValue + "'";
        }
        // Si solo se selecciona el filtro de estado_factura
        else if (DropDownList1.SelectedValue != "-1")
        {
            selectFiltros = "select * from facturas where facturas.estado_factura='" + DropDownList1.SelectedValue + "'";
        }
        // Si solo se selecciona el filtro de población
        else if (DropDownList2.SelectedValue != "-1")
        {
            selectFiltros = "select * from facturas where facturas.poblacion='" + DropDownList2.SelectedValue + "'";
        }
        // Si no se selecciona ningún filtro
        else
        {
            selectFiltros = "select * from facturas";
        }
        // Conectamos con la BD
        string conexion = ConfigurationManager.ConnectionStrings[conexionBaseDatos].ConnectionString;
        MySqlConnection con = new MySqlConnection(conexion);
        DataSet ds = new DataSet();
        MySqlDataAdapter da = new MySqlDataAdapter(selectFiltros, con);
        da.Fill(ds);
        GridView1.DataSource = ds;
        // Cargamos la select en el GridView
        GridView1.DataBind();
    }
}
