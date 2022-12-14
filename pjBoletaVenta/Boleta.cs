using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjBoletaVenta
{
    public class Boleta
    {
        public int Numero { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Cedula { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Producto { get; set; }
        public int Cantidad { get; set; }

        //metodo para determinar el precio del producto
        public double DeterminaPrecio()
        {
            switch(Producto)
            {
                case "PS5 + 1 MANDO DS5": return 500;
                case "PS4(1TB) + 1 MANDO DS4": return 619;
                case "MANDO PS5/ DS5": return 69;
                case "MANDO PS4/ DS4": return 60;
                default:
                    break;
            }
            return 0;
        }

        //Metodo para determinar el importe 
        public double CalcularImporte()
        {
            return DeterminaPrecio() * Cantidad;
        }

    }
}
