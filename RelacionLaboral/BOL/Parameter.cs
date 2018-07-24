using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BOL.Helpers;

namespace BOL
{
  public static class Parameter
    {
        public static Coneccion Leer_parametros()
        {
            XDocument Config = new XDocument();
            Config = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "/Parameter.config");

            var Parameter = (from el in Config.Elements("Parameter")
                             select new Coneccion
                             {
                                 Servidor = el.Element("SERVER").Value.ToString(),
                                 Usuario = el.Element("USUARIO").Value.ToString(),
                                 Bd = el.Element("DATABASE").Value.ToString(),
                                 Clave = el.Element("PASSWORD").Value.ToString(),
                                 ConString = "Data source=" + el.Element("SERVER").Value.ToString() + " ; Initial Catalog=" + el.Element("DATABASE").Value.ToString() + "; User ID=" + el.Element("USUARIO").Value.ToString() + "; Password=" + el.Element("PASSWORD").Value.ToString()
                             }).FirstOrDefault();
            return (Coneccion)Parameter;
        }

        public static string LeerLocacion()
        {
            XDocument Config = new XDocument();
            Config = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "/Parameter.config");

            var Parameter = (from el in Config.Elements("Parameter").Elements("APIS")
                             select el.Element("LOCACION").Value.ToString()).FirstOrDefault();
            return Parameter;
        }
        public static string LeerNaves()
        {
            XDocument Config = new XDocument();
            Config = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "/Parameter.config");

            var Parameter = (from el in Config.Elements("Parameter").Elements("APIS")
                             select el.Element("NAVES").Value.ToString()).FirstOrDefault();
            return Parameter;
        }

        public static string LeerRutaws(string ws)
        {
            XDocument Config = new XDocument();
            Config = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "/Parameter.config");

            var Parameter = (from el in Config.Elements("Parameter").Elements("APIS")
                             select el.Element(ws).Value.ToString()).FirstOrDefault();
            return Parameter;
        }


        public static CredencialesWS LeerCredencialesws()
        {

            CredencialesWS cr = new CredencialesWS();

            XDocument Config = new XDocument();
            Config = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "/Parameter.config");

            cr.user = (from el in Config.Elements("Parameter").Elements("APIS")
                       select el.Element("USER").Value.ToString()).FirstOrDefault();

            cr.pass = (from el in Config.Elements("Parameter").Elements("APIS")
                       select el.Element("PASS").Value.ToString()).FirstOrDefault();

            cr.rutEmpr = (from el in Config.Elements("Parameter").Elements("APIS")
                          select el.Element("RUTEM").Value.ToString()).FirstOrDefault();
            //cr.DvEmpr = (from el in Config.Elements("Parameter").Elements("APIS")
            //             select el.Element("DVEMP").Value.ToString()).FirstOrDefault();

            return cr;
        }

    }
}
