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

/*
 * Esta clase se encarga de introducir los elementos que se cargan en 
 * los DropDownList, realizar el filtrado, obtener los datos del fichero xml 
 * y mostrarlos por pantalla
 */
public partial class Proyecto1_Facturas : System.Web.UI.Page
{
    /*
     * Pre: ---
     * Post: Método que se invoca cuando se carga la página, carga los elementos
     * deseados, lee la información del fichero xml e imprime la información
     * por pantalla
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //Creamos un DataSet
        DataSet ds = new DataSet();
        //Cargamos los datos del XML en el DataSet
        ds.ReadXml(Server.MapPath("~/Datos/DatosFacturas.xml"));
        //Indicamos la fuente de datos al GridView
        GridView1.DataSource = ds;
        //Enlazamos los datos para que se muestren
        GridView1.DataBind();

        if (!IsPostBack) /*Esta línea se usa para evitar que los valores
                en el DropDownList se dupliquen cada vez que ocurra un evento*/
        {
            //Agregamos los elementos necesarios a los DropDownList
            foreach (var factura in ds.Tables[0].AsEnumerable())
            {
                //Comprobamos que la lista de items en el desplegable no está vacía
                if (desEstadoFactura.Items.Count < 1)
                {
                    //Añadimos un item
                    desEstadoFactura.Items.Add(factura.Field<string>("EstadoFactura"));
                }
                else
                {
                    Boolean added = false; //Variable para comprobar si un item ya está añadido
                    foreach (var estado in desEstadoFactura.Items) //Recorremos los items en el desplegable
                    {
                        //Comparamos el item del desplegable con el valor de la fila en la tabla
                        if (factura.Field<string>("EstadoFactura").Equals(estado.ToString()))
                        {
                            /*Si hay una coincidencia, lo marcamos con la variable añadido 
                             * y rompemos el bucle */
                            added = true;
                            break;
                        }
                    }
                    if (!added) //Comprobamos si el item se había añadido o no
                    {
                        //Si el item no se había añadido, entramos en el if, y lo añadimos
                        desEstadoFactura.Items.Add(factura.Field<string>("EstadoFactura"));
                    }
                }
                //Comprobamos que la lista de items en el desplegable no está vacía
                if (desProductos.Items.Count < 1)
                {
                    //Añadimos un item
                    desProductos.Items.Add(factura.Field<string>("Producto"));
                }
                else
                {
                    Boolean added = false;//Variable para comprobar si un item ya está añadido
                    foreach (var estado in desProductos.Items) //Recorremos los items en el desplegable
                    {
                        //Comparamos el item del desplegable con el valor de la fila en la tabla
                        if (factura.Field<string>("Producto").Equals(estado.ToString()))
                        {
                            /*Si hay una coincidencia, lo marcamos con la variable añadido 
                             * y rompemos el bucle */
                            added = true;
                            break;
                        }
                    }
                    if (!added) //Comprobamos si el item se había añadido o no
                    {
                        //Si el item no se había añadido, entramos en el if, y lo añadimos
                        desProductos.Items.Add(factura.Field<string>("Producto"));
                    }
                }
                if (desMoneda.Items.Count < 1)
                {
                    //Añadimos un item
                    desMoneda.Items.Add(factura.Field<string>("Moneda"));
                }
                else
                {
                    Boolean added = false;//Variable para comprobar si un item ya está añadido
                    foreach (var estado in desMoneda.Items) //Recorremos los items en el desplegable
                    {
                        //Comparamos el item del desplegable con el valor de la fila en la tabla
                        if (factura.Field<string>("Moneda").Equals(estado.ToString()))
                        {
                            /*Si hay una coincidencia, lo marcamos con la variable añadido 
                             * y rompemos el bucle */
                            added = true;
                            break;
                        }
                    }
                    if (!added) //Comprobamos si el item se había añadido o no
                    {
                        //Si el item no se había añadido, entramos en el if, y lo añadimos
                        desMoneda.Items.Add(factura.Field<string>("Moneda"));
                    }
                }
            }
            //desEstadoFactura.Items.Add("Pendiente");
            //desEstadoFactura.Items.Add("Pagado");
            //desEstadoFactura.Items.Add("Atrasado");
            //desEstadoFactura.Items.Add("Sobrepagado");

            //desProductos.Items.Add("Software");
            //desProductos.Items.Add("Hardware");
            //desProductos.Items.Add("Servicio al cliente");

            //desMoneda.Items.Add("EUR");
            //desMoneda.Items.Add("USD");
            //desMoneda.Items.Add("GBP");
            //desMoneda.Items.Add("JPY");
            //desMoneda.Items.Add("AUD");
            //desMoneda.Items.Add("CAD");
            //desMoneda.Items.Add("CHF");
            //desMoneda.Items.Add("CNH");
            //desMoneda.Items.Add("BTC");
        }
    }

    /*
     * Pre: ---
     * Post: Este método crea un dataset donde carga los datos del fichero xml,
     * recibe los datos seleccionados en los desplegables, y mediante 
     * expresiones condicionales, realiza el filtrado adecuado a cada caso en los datos.
     * Finalmente guarda los resultados en una nueva tabla y la imprimepor pantalla.
     */
    protected void FiltersChanged(object sender, EventArgs e)
    {
        //Creamos un DataSet
        DataSet ds = new DataSet();
        //Leemos y cargamos el xml en el DataSet
        ds.ReadXml(Server.MapPath("~/Datos/DatosFacturas.xml"));
        // Creamos un objeto de tipo IEnumerable para poder recorrer las filas de la tabla 
        var facturs = ds.Tables[0].AsEnumerable();
        string estado = desEstadoFactura.SelectedItem.Value; // Aquí obtenemos el valor del desplegable.
        string producto = desProductos.SelectedItem.Value;  // Aquí obtenemos el valor del desplegable.
        string moneda = desMoneda.SelectedItem.Value;       // Aquí obtenemos el valor del desplegable.
        if (!estado.Equals("0") && !producto.Equals("0") && !moneda.Equals("0"))
        {

            /* Con la siguiente query obtenemos las filas cuyos valores de 
                EstadoFactura, Producto y Moneda, coinciden con los 
                seleccionados en los DropDownList*/
            var query = from factur in facturs
                        where factur.Field<string>("EstadoFactura") == estado
                        && factur.Field<string>("Producto") == producto
                        && factur.Field<string>("Moneda") == moneda
                        select factur;
            //Creamos una nueva tabla donde copiaremos las filas filtradas
            DataTable newT = ds.Tables[0].Clone();
            //Ya que la tabla la hemos clonado, tiene todos los datos, por tanto tenemos que vaciarla
            newT.Clear();
            //Con el siguiente bucle, copiamos en la nueva tabla, las filas filtradas
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            //Indicamos al GridView que cambiamos la fuente de datos
            GridView1.DataSource = newT;
            //Verificamos si la tabla está vacía, si es así, mostramos un mensaje por pantalla
            if (newT.Rows.Count == 0) Label1.Visible = true; 
            else Label1.Visible = false; 
            //GridView1.DataSource = query.ToList();
            //Enlazamos la fuente de datos al control de GridView 
            GridView1.DataBind();
        }
        else if (!estado.Equals("0") && producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de estado de la factura desEstadoFactura */
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("EstadoFactura") == estado
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        else if (estado.Equals("0") && !producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de producto desProducto */
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("Producto") == producto
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        else if (estado.Equals("0") && producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que sólo se selecciona
              un valor en el DropDownList de moneda desMoneda */
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("Moneda") == moneda
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        else if (!estado.Equals("0") && !producto.Equals("0") && moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de estado de factura y producto*/
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("EstadoFactura") == estado
                        && factur.Field<string>("Producto") == producto
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        else if (!estado.Equals("0") && producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de estado de factura y moneda*/
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("EstadoFactura") == estado
                        && factur.Field<string>("Moneda") == moneda
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        else if (estado.Equals("0") && !producto.Equals("0") && !moneda.Equals("0"))
        {
            /*En esta condición tratamos el caso en que se seleccionan
              valores en los DropDownList de producto y moneda*/
            /*Las siguentes líneas realizan las mismas acciones que en el primer if
              pero con el cambio en las condiciones*/
            var query = from factur in facturs
                        where factur.Field<string>("Moneda") == moneda
                        select factur;
            DataTable newT = ds.Tables[0].Clone();
            newT.Clear();
            foreach (var r in query.ToList())
            {
                newT.ImportRow(r);
            }
            if (newT.Rows.Count == 0) Label1.Visible = true;
            else Label1.Visible = false;
            GridView1.DataSource = newT;
            GridView1.DataBind();
        }
        /* El siguiente else resultó no ser necesario ya que automáticamente se carga 
         el dataset entero cuando no se selecciona ningún valor 
         en ningún desplegable*/
        /*}
        else /*Entramos a este bucle cuando no se selecciona 
              * ningún valor en ninguno de los DropDownList*/
        //{
        //ds.ReadXml(Server.MapPath("~/Datos/DatosFacturas.xml"));
        //GridView1.DataSource = ds;
        //GridView1.DataBind();
    }

    /*
     * Pre: ---
     * Post: Este método se encarga de exportar los datos de un determinado gridview
     * a un fichero xls
     */
    protected void ExportXML(object sender, EventArgs e)
    {
        FiltersChanged(sender, e);
        if (GridView1.Rows.Count == 0) return;
        //Vaciamos el Response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        //Establecemos el nombre del archivo a crear
        string ExportedFileName = "MisFacturas.xls";
        //Creamos objetos htmlTextWritery y stringWriter para crear el archivo xls.
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Especificamos el tipo de archivo y las cabeceras
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + ExportedFileName);
        Response.Charset = "";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        GridView1.RenderControl(htmltextwrtter);
        //Escribimos con el strwritter y finalizamos el Response.
        Response.Write(strwritter.ToString());
        Response.End();
    }

    /*
     * Pre: ---
     * Post: Este método cerifica que el gridView está dentro de un form 
     * antes de crear el archivo excel
     */
    public override void VerifyRenderingInServerForm(Control control) { }
}