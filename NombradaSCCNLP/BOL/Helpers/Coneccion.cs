using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Helpers
{
    public class Coneccion
    {
        public string Servidor { get; set; }
        public string Bd { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string ConString { get; set; }
    }
}
