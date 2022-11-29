using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDFIREBASE.Models
{
    public class Contacto
    {
        public string IdContacto { get; set; }
        public string Nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string Ciudad { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}