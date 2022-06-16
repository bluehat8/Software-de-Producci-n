using LiteDB;
using Software_de_Producción.Modelo;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Software_de_Producción.Controlador
{
    public class RepositorioProductos : IRepositorio<Producto>
    {

        //BASE DE DATOS NOSQL -> ORIENTADA A DOCUMENTOS->LiteDB

        private string DBName = "Inventario.db";
        private string TableName = "Producto";
        public List<Producto> Read {
            get {

                List<Producto> datos = new List<Producto>();
                using (var db= new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Producto>(TableName).FindAll().ToList();
                }
                return datos;
            }

            

        }

        public List<Producto> Buscar(Producto nombreP)
        {
            List<Producto> datos = new List<Producto>();   
            datos= Read.Where(item => item.nombre.ToUpper().Contains(nombreP.nombre)).ToList();
            return datos;
        }

        public bool Crear(Producto entidad)
        {
            entidad.id = Guid.NewGuid().ToString();
            entidad.TotalPrecioCompra = entidad.calcularTotalCompra(entidad.inventario, entidad.precioDeCompra);

            try
            {
                using (var db= new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar( Producto entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar()
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    coleccion.DeleteAll();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public void calcularDatosRelevantes(TextBlock tbcProd, TextBlock tbtotales, TextBlock tbCostoTC, TextBlock tbMasExistencia)
        {
            if (Read.Count == 0)
            {
                return;
            }

            int maximoInventario = 0;
            tbcProd.Text = Read.Count.ToString();
            tbtotales.Text = Read.Sum(item => item.inventario).ToString();
            tbCostoTC.Text = Read.Sum(item => item.TotalPrecioCompra).ToString();
            maximoInventario = int.Parse(Read.Max(item=> item.inventario).ToString());
            Producto p = new Producto();
            p = Read.Where(item => item.inventario.Equals(maximoInventario)).FirstOrDefault();
            tbMasExistencia.Text = (p.nombre).ToString();
          

        }

        public bool EliminarPorID(Producto entidad)
        {
            try
            {
               
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    coleccion.Delete(entidad.id);
                }
                //si logra eliminar un registro retornara true, de lo contrario retorna false
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BulkCreate(List<Producto> entidad)
        {

            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    /*foreach (var item in entidad)
                    {
                        coleccion.Insert(item);

                    }*/
                  coleccion.InsertBulk(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        public List<Producto> ImportarDatos(string patharchivo)
        {
            Producto objProd;
            SLDocument sl = new SLDocument(patharchivo);
            int irow = 2;
            List<Producto> lstBG = new List<Producto>();

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(irow, 1)))
            {
                objProd= new Producto();
                objProd.id = Guid.NewGuid().ToString();

                objProd.nombre = sl.GetCellValueAsString(irow, 1);
                objProd.inventario = int.Parse(sl.GetCellValueAsString(irow, 2));
                objProd.precioDeVenta = double.Parse(sl.GetCellValueAsString(irow,3));
                objProd.precioDeCompra = double.Parse(sl.GetCellValueAsString(irow, 4));
                objProd.TotalPrecioCompra = objProd.calcularTotalCompra(objProd.inventario,objProd.precioDeCompra);


                //Validando cuentas no repetidas 
                if (!objProd.ValidandoNombres(objProd, lstBG))
                {
                    //MessageBox.Show("Algunos de los productos que intenta importar se encuentran repetidos, ", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    lstBG.Add(new Producto(objProd.id,objProd.nombre,objProd.inventario,objProd.precioDeVenta,objProd.precioDeCompra,objProd.TotalPrecioCompra));

             }

                irow++;

            }
            return lstBG;
        }
    }
}
