using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Controlador
{
    public interface IRepositorio<T> where T:class
    {

        bool Crear(T entidad);
        bool Editar( T entidadModificada);
        bool Eliminar();
        List<T> Buscar(T nombre);
        List<T> Read { get; }

        bool EliminarPorID( T entidad);

        bool BulkCreate(List<T> entidad);


    }
}
