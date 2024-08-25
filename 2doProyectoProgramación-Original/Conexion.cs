using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2doProyectoProgramación.Properties;
using System.Configuration;

namespace _2doProyectoProgramación
{
    class Conexion
    {
        public string servidor, usuario, clave, db, cadena;

        public static string ObtenerString() {
            return Settings.Default.ProyectoConnectionString;
        }

        public void Fconexion()
        {
            servidor = "LAUTARO";
            db = "Proyecto";
            usuario = "";
            clave = "";
            cadena = ObtenerString();


        }
    }
}
