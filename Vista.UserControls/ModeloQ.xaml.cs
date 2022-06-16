using Software_de_Producción.Modelo;
using Software_de_Producción.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.UI.DataVisualization.Charting;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using OxyPlot.Series;
using DataPoint = OxyPlot.DataPoint;
using Software_de_Producción.Controlador;

namespace Software_de_Producción.Vista.UserControls
{
    /// <summary>
    /// Lógica de interacción para ModeloQ.xaml
    /// </summary>
    public partial class ModeloQ : UserControl
    {
        QModel qo = new QModel();
        ValidaTXT validate = new ValidaTXT();
        Producto objProducto = new Producto();
        RepositorioProductos repo = new RepositorioProductos();

        int demandaAnual;
        double costoPedir;
        double politicaAlmacen;
        double costoProducto;
        double costoAlmacen;
        double qoptimo;
        double NPedidos;
        double tep;
        double rop;
        int plazoEentrega;
        int diasLaborales = 360;
        double z;
        int desv;
        double politicaServicio;


        public ModeloQ()
        {
            InitializeComponent();
            //Graficar();
            datagridP.ItemsSource = repo.Read;
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            calcular();
            desaparecer_Grafico();

        }


        private void desaparecer_Grafico()
        {
            if (txtDesv.Text.Trim() == "")
            {
                oxyTab.Visibility = Visibility.Visible;
                tbcAnalisis.Text = "";
                tbcAnalisis.Margin = new Thickness(20, 281.291, 19.527, -305.291);
                tbcGA.Text = "Gráfica";

            }
            else
            {
                oxyTab.Visibility = Visibility.Collapsed;
                tbcAnalisis.Text = qo.obtenerAnalisis(qoptimo,rop,NPedidos,tep);
                tbcAnalisis.Margin = new Thickness(20, 40, 0, 0);
                tbcGA.Text = "Análisis";
            }
        }

        void calcular()
        {

            
  
            if (rbAnual.IsChecked==true)
            {
                DemandaAnual();
            }

            if (rbMensual.IsChecked == true)
            {
                DemandaMensual();
            }


            if (rbSemanal.IsChecked == true)
            {
                DemandaSemanal();
            }

            if (rbDiaria.IsChecked == true)
            {
                DemandaDiaria();
            }

        }

        private void setCostoAlmacen()
        {
            if (txtCostoAlmacen.Text.Trim() == "")
            {
                costoAlmacen = 0;
            }
            else
            {
                costoAlmacen = double.Parse(txtCostoAlmacen.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
            }
        }


        private bool validarCampos()
        {
            bool flag = true;
            if (txtDemanda.Text == "")
            {
                flag= false;
            }
            if(txtCostoP.Text=="" && txtPolAlmacen.Text.Length > 0 || txtPolAlmacen.Text == "" && txtCostoP.Text.Length > 0)
            {
                flag= false;
            }
            if(txtDesv.Text=="" && txtPolServicio.Text.Length > 0 || txtPolServicio.Text == "" && txtDesv.Text.Length > 0)
            {
                flag= false;
            }

            return flag;
        }



        //NOTA: DARLE MANTENIMIENTO A LA REGIÒN DE TIPOS DE DEMANDAS DE FORMA QUE PUEDAN REDUCIRSE A UNA SOLA FUNCIÒN

        #region TIPOS DE DEMANDAS

        private void DemandaAnual()
        {
             try
                {

                if (validarCampos())
                {

                    if (txtDiasLab.Text == "")
                    {
                        diasLaborales = 360;
                    }
                    else
                    {
                        diasLaborales = int.Parse(txtDiasLab.Text);
                    }

                    if (txtPolAlmacen.Text == "" || txtCostoP.Text=="") { politicaAlmacen = 0; costoProducto = 0; }
                    else  {politicaAlmacen = double.Parse(txtPolAlmacen.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                        costoProducto = double.Parse(txtCostoP.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    }
                    costoPedir = 0;
                    demandaAnual = int.Parse(txtDemanda.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                    costoPedir = double.Parse(txtCostoPedir.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                    plazoEentrega = int.Parse(txtPlazoEntrega.Text);
                  
                    setCostoAlmacen();

                    qoptimo = qo.calcularPedidoOptimoSinFaltante(int.Parse(txtDemanda.Text.ToString()), costoPedir, politicaAlmacen, costoProducto, costoAlmacen);
                    NPedidos = qo.NumeroPedidos(qoptimo,
                        demandaAnual);

                    tbcProducto.Text = txtNombre.Text;
                    tbcQoptimo.Text = qoptimo.ToString();
                    tbcNPedido.Text = NPedidos.ToString();
                    tep = qo.TiempoEntrePedidos(double.Parse(tbcNPedido.Text.ToString()));

                    ////////////////////
                    ///
                    if (txtPolServicio.Text == "")
                    {
                        rop = (qo.ROPSinZ(qo.calcularDemandadiaria(int.Parse(txtDemanda.Text),diasLaborales), int.Parse(txtPlazoEntrega.Text)));

                    }
                    else
                    {
                        z = double.Parse(txtPolServicio.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                        desv = int.Parse(txtDesv.Text);
                        rop = qo.RopConZ(qo.calcularDemandadiaria(int.Parse(txtDemanda.Text),diasLaborales), int.Parse(txtPlazoEntrega.Text),z, desv);

                    }
                    tbcTEP.Text = tep.ToString();
                    tbcROP.Text = rop.ToString();


                    if (costoAlmacen == 0)
                    {
                        //setCostoAlmacen();

                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, qo.calcularCostoMtto(politicaAlmacen, costoProducto), int.Parse(qoptimo.ToString())).ToString();

                    }
                    else if(costoAlmacen!=0)
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, costoAlmacen, int.Parse(qoptimo.ToString())).ToString();

                    }

                    Graficar(Math.Floor(tep), qoptimo, Convert.ToInt32(Math.Floor(NPedidos)),rop);

                    return;
                }
                MessageBox.Show("Faltan datos");

            }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
}







        private void DemandaDiaria()
        {
            try
            {

                if (validarCampos())
                {

                    if (txtDiasLab.Text == "")
                    {
                        diasLaborales = 360;
                    }
                    else
                    {
                        diasLaborales = int.Parse(txtDiasLab.Text);
                    }

                    if (txtPolAlmacen.Text == "" || txtCostoP.Text == "") { politicaAlmacen = 0; costoProducto = 0; }
                    else
                    {
                        politicaAlmacen = double.Parse(txtPolAlmacen.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                        costoProducto = double.Parse(txtCostoP.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    }



                    demandaAnual = int.Parse(txtDemanda.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"))*diasLaborales;
                    costoPedir = double.Parse(txtCostoPedir.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                    
                    plazoEentrega = int.Parse(txtPlazoEntrega.Text);
                    setCostoAlmacen();

                    qoptimo = qo.calcularPedidoOptimoSinFaltante(demandaAnual, costoPedir, politicaAlmacen, costoProducto, costoAlmacen);
                    NPedidos = qo.NumeroPedidos(qoptimo,
                        demandaAnual);

                    tbcProducto.Text = txtNombre.Text;
                    tbcQoptimo.Text = qoptimo.ToString();
                    tbcNPedido.Text = NPedidos.ToString();
                    tep = qo.TiempoEntrePedidos(double.Parse(tbcNPedido.Text.ToString()));

                   

                    if (txtPolServicio.Text == "")
                    {
                        rop = (qo.ROPSinZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text)));

                    }
                    else
                    {
                        z = double.Parse(txtPolServicio.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                        desv = int.Parse(txtDesv.Text);
                        rop = qo.RopConZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text), z, desv);

                    }
                    tbcTEP.Text = tep.ToString();
                    tbcROP.Text = rop.ToString();

                    if (costoAlmacen == 0)
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, qo.calcularCostoMtto(politicaAlmacen, costoProducto), int.Parse(qoptimo.ToString())).ToString();

                    }
                    else
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, costoAlmacen, int.Parse(qoptimo.ToString())).ToString();

                    }
                    Graficar(Math.Floor(tep), qoptimo, Convert.ToInt32(Math.Floor(NPedidos)),rop);

                    return;
                }
                MessageBox.Show("Faltan datos");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }




        private void DemandaMensual()
        {
            try
            {

                if (validarCampos())
                {

                    if (txtDiasLab.Text == "")
                    {
                        diasLaborales = 360;
                    }
                    else
                    {
                        diasLaborales = int.Parse(txtDiasLab.Text);
                    }

                    if (txtPolAlmacen.Text == "" || txtCostoP.Text == "") { politicaAlmacen = 0; costoProducto = 0; }
                    else
                    {
                        politicaAlmacen = double.Parse(txtPolAlmacen.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                        costoProducto = double.Parse(txtCostoP.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    }



                    demandaAnual = int.Parse(txtDemanda.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB")) * 12;
                    costoPedir = double.Parse(txtCostoPedir.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    plazoEentrega = int.Parse(txtPlazoEntrega.Text);
                    setCostoAlmacen();

                    qoptimo = qo.calcularPedidoOptimoSinFaltante(demandaAnual, costoPedir, politicaAlmacen, costoProducto, costoAlmacen);
                    NPedidos = qo.NumeroPedidos(qoptimo,
                        demandaAnual);

                    tbcProducto.Text = txtNombre.Text;
                    tbcQoptimo.Text = qoptimo.ToString();
                    tbcNPedido.Text = NPedidos.ToString();
                    tep = qo.TiempoEntrePedidos(double.Parse(tbcNPedido.Text.ToString()));



                    if (txtPolServicio.Text == "")
                    {
                        rop = (qo.ROPSinZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text)));

                    }
                    else
                    {
                        z = double.Parse(txtPolServicio.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                        desv = int.Parse(txtDesv.Text);
                        rop = qo.RopConZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text), z, desv);

                    }
                    tbcTEP.Text = tep.ToString();
                    tbcROP.Text = rop.ToString();

                    if (costoAlmacen == 0)
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, qo.calcularCostoMtto(politicaAlmacen, costoProducto), int.Parse(qoptimo.ToString())).ToString();

                    }
                    else
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, costoAlmacen, int.Parse(qoptimo.ToString())).ToString();

                    }
                    Graficar(Math.Floor(tep), qoptimo, Convert.ToInt32(Math.Floor(NPedidos)),rop);

                    return;
                }
                MessageBox.Show("Faltan datos");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }




        private void DemandaSemanal()
        {
            try
            {

                if (validarCampos())
                {

                    if (txtDiasLab.Text == "")
                    {
                        diasLaborales = 360;
                    }
                    else
                    {
                        diasLaborales = int.Parse(txtDiasLab.Text);
                    }

                    if (txtPolAlmacen.Text == "" || txtCostoP.Text == "") { politicaAlmacen = 0; costoProducto = 0; }
                    else
                    {
                        politicaAlmacen = double.Parse(txtPolAlmacen.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
                        costoProducto = double.Parse(txtCostoP.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    }


                    demandaAnual = int.Parse(txtDemanda.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB")) * 52;
                    costoPedir = double.Parse(txtCostoPedir.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));

                    plazoEentrega = int.Parse(txtPlazoEntrega.Text);
                    setCostoAlmacen();

                    qoptimo = qo.calcularPedidoOptimoSinFaltante(demandaAnual, costoPedir, politicaAlmacen, costoProducto, costoAlmacen);
                    NPedidos = qo.NumeroPedidos(qoptimo,
                        demandaAnual);

                    tbcProducto.Text = txtNombre.Text;
                    tbcQoptimo.Text = qoptimo.ToString();
                    tbcNPedido.Text = NPedidos.ToString();
                    tep = qo.TiempoEntrePedidos(double.Parse(tbcNPedido.Text.ToString()));



                    if (txtPolServicio.Text == "")
                    {
                        rop = (qo.ROPSinZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text)));

                    }
                    else
                    {
                        z = double.Parse(txtPolServicio.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                        desv = int.Parse(txtDesv.Text);
                        rop = qo.RopConZ(qo.calcularDemandadiaria(demandaAnual, diasLaborales), int.Parse(txtPlazoEntrega.Text), z, desv);

                    }
                    tbcTEP.Text = tep.ToString();
                    tbcROP.Text = rop.ToString();

                    if (costoAlmacen == 0)
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, qo.calcularCostoMtto(politicaAlmacen, costoProducto), int.Parse(qoptimo.ToString())).ToString();

                    }
                    else
                    {
                        tbcCostoTotal.Text = qo.costoTotal(demandaAnual, costoPedir, costoAlmacen, int.Parse(qoptimo.ToString())).ToString();

                    }
                    Graficar(Math.Floor(tep), qoptimo, Convert.ToInt32(Math.Floor(NPedidos)),rop);

                    return;
                }
                MessageBox.Show("Faltan datos");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        #endregion



        #region GRAFICA
        public void Graficar(double tep, double qoptimo, int nPedidos,double rop)
        {
            OxyPlot.Wpf.PlotView pv = new PlotView();
            PlotModel model = new PlotModel();
            LinearAxis exeX = new LinearAxis();
            exeX.Position = AxisPosition.Bottom;
            LinearAxis exeY = new LinearAxis();
            exeX.Position = AxisPosition.Left;
            model.Axes.Add(exeX);
            model.Axes.Add(exeY);
            LineSeries linea = new LineSeries();
            LineSeries linea2 = new LineSeries();
            LineSeries linea3 = new LineSeries();
            LineSeries linea4 = new LineSeries();
            List<LineSeries> listaSeries = new List<LineSeries>();
            listaSeries.Add(linea4);
            
            Generador generador = new Generador();


            ///GRAFICANDO COMPORTAMIENTO DEL INVENTARIO
            generador.GeneradorDatos(0, tep , 0, qoptimo , tep,0, 1);

            foreach (var item2 in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item2.X, item2.Y));
            }

            for (int i =1 ; i<=nPedidos; i++)
            {
             
                if(i>1)
                {
                    generador.Puntos = null;
                    generador.GeneradorDatos(tep*(i-1), tep * i, 0, qoptimo * i, tep * i, 0, 1);
                    foreach (var item2 in generador.Puntos)
                    {
                        linea.Points.Add(new DataPoint(item2.X, item2.Y));
                    }
                }
            }
            linea.Title = "Inventario(tiempo)";


            //GRAFICANDO EL ROP 

            generador.Puntos = null;
            generador.GeneradorDatosRectaHorizontal(0, tep, rop,0, 0, 0, 1);

            foreach (var item2 in generador.Puntos)
            {
               linea2.Points.Add(new DataPoint(item2.X, item2.Y));
            }

            for (int i = 1; i <= nPedidos; i++)
            {

                if (i > 1)
                {
                    generador.Puntos = null;
                    generador.GeneradorDatosRectaHorizontal(tep * (i - 1), tep * i, rop,0,0, 0, 1);
                    foreach (var item2 in generador.Puntos)
                    {
                        linea2.Points.Add(new DataPoint(item2.X, item2.Y));
                    }
                }
            }
            linea2.Title = "Rop(tiempo)";
            linea.Tag = "ROP";

            //GRAFICANDO EL PLAZO DE ENTREGA

            generador.Puntos = null;
            generador.GeneradorDatosRectaVertical(0, rop, 0, tep - plazoEentrega, 0, 0, 1);
            foreach (var item2 in generador.Puntos)
            {
                linea3.Points.Add(new DataPoint(item2.X, item2.Y));
            }

            linea3.Title = "Plazo entrega(ROP)";
            linea.Tag = "Plazo de entrega";


            model.Series.Add(linea);
            model.Series.Add(linea2);
            model.Series.Add(linea3);
            model.Series.Add(linea4);


            oxyTab.Model = model; 

        }

        #endregion


        #region VALIDACIONES DE CAMPOS
        private void txtDemanda_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtDemanda);
        }

        private void txtDemanda_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtCostoP_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoP);
        }

        private void txtCostoP_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtCostoPedir_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoPedir);

        }

        private void txtDesv_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtDesv);

        }

        private void txtPlazoEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtPlazoEntrega);

        }

        private void txtPolServicio_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtPolServicio);

        }

        private void txtCostoAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoAlmacen);

        }

        private void txtDiasLab_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtDiasLab);

        }

        #endregion

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtCostoAlmacen.Clear();
            txtNombre.Clear();
            txtPlazoEntrega.Clear();
            txtDiasLab.Clear();
            txtDesv.Clear();
            txtPolAlmacen.Clear();
            txtPolServicio.Clear();
            txtCostoP.Clear();
            txtCostoPedir.Clear();
            txtDemanda.Clear();
        }

        private void datagridP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridP.SelectedItems.Count == 1)
            {


                Producto a = (Producto)datagridP.SelectedItem;
                objProducto.id = a.id.ToString();
                objProducto.nombre = a.nombre.ToString();
                objProducto.inventario = int.Parse(a.inventario.ToString());
                objProducto.precioDeCompra = double.Parse(a.precioDeCompra.ToString());
                objProducto.precioDeVenta = double.Parse(a.precioDeVenta.ToString());

                txtNombre.Text = objProducto.nombre;
                txtCostoP.Text = objProducto.precioDeCompra.ToString().Replace(",",".");

            }
        }
    }
}