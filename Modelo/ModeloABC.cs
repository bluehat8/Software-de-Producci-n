using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Modelo
{
    public class ModeloABC
    {
       public string nombreProducto { get; set; }
       public int demanda { get; set; }
       public double precioUnitario { get; set; }
       public double inversion { get; set; }

       public double invAcumulada { get; set; }
       public double invAcumuladaPrc { get; set; }

       public string zona { get; set; }


        public ModeloABC()
        {

        }


        public ModeloABC(string nombreProducto, int demanda, double precioUnitario,
          double inversion,  double invAcumulada, double invAcumuladaPrc, string zona)
        {
            this.nombreProducto = nombreProducto;
            this.demanda = demanda;
            this.precioUnitario = precioUnitario;
            this.inversion = inversion;
            this.invAcumulada = invAcumulada;
            this.invAcumuladaPrc = invAcumuladaPrc;
            this.zona = zona;
        }


        public List<ModeloABC> calcularABC(List<Producto> lstProductos)
        {
            List<ModeloABC> lstModel = new List<ModeloABC>();
            double inversionAcumulada = 0;
            double porcentaje = 0;

            lstProductos=lstProductos.OrderByDescending(item => item.TotalPrecioCompra).ToList();

            for (int i = 0; i < lstProductos.Count; i++)
            {
                lstModel.Add(new ModeloABC(
                   lstProductos[i].nombre,
                   lstProductos[i].inventario,
                   lstProductos[i].precioDeCompra,
                   lstProductos[i].TotalPrecioCompra,
                   inversionAcumulada += lstProductos[i].TotalPrecioCompra,
                   porcentaje=inversionAcumulada/ lstProductos.Sum(item=> item.TotalPrecioCompra),
                   obtenerClasicacion(porcentaje).ToString()
                    ));
            }
            return lstModel;
        }


        public AbcValues.abcValues obtenerClasicacion(double porcentajeInvAcumulada)
        {
            if (porcentajeInvAcumulada <= 0.8)
            {
                return AbcValues.abcValues.A;
            }
            else if (porcentajeInvAcumulada <= 0.95)
            {
                return AbcValues.abcValues.B;
            }
            else
            {
                return AbcValues.abcValues.C;
            }
        }
    }
}
