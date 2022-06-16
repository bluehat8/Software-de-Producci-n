using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Modelo
{
    public class Producto
    {
        public string id { get; set; }
        public string nombre { get; set; }

        public double precioDeVenta { get; set; }

        public double precioDeCompra { get; set; }

        public double TotalPrecioCompra { get; set; }


        public int inventario { get; set; }

        public Producto(){}

        public Producto(string id, string nombre,int inventario, double precioDeVenta, double precioDeCompra, double TotalPrecioCompra){
            this.id = id;
            this.nombre = nombre;
            this.inventario = inventario;
            this.precioDeVenta = precioDeVenta;
            this.precioDeCompra = precioDeCompra;
            this.TotalPrecioCompra = TotalPrecioCompra;
        }



        public double calcularTotalCompra(int inventario, double precioCompra)
        {
            return inventario * precioCompra;
        }


        public bool ValidandoNombres(Producto modelo, List<Producto> Read)
        {

            bool flag = true;
            for (int i = 0; i < Read.Count; i++)
            {
                if (modelo.nombre.Equals(Read[i].nombre))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
}
