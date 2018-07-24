using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BOL;

namespace DAL
{
   public class WsRecuperaNombrada
    {
        public static String Execute(CredencialesWS cr, Nombrada nom)
        {
            String resp;
            StringBuilder xmlenvia = new StringBuilder("");
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            xmlenvia.Append(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:scc=""https://sccnlp.com/"">");
            xmlenvia.Append(@"<soap:Header>");
            xmlenvia.Append(@"<scc:UserCredentials>");
            xmlenvia.Append(@"<scc:userName>");
            xmlenvia.Append(cr.user);
            xmlenvia.Append(@"</scc:userName>");
            xmlenvia.Append(@"<scc:password>");
            xmlenvia.Append(cr.pass);
            xmlenvia.Append("</scc:password>");
            xmlenvia.Append(@"</scc:UserCredentials>");
            xmlenvia.Append(@"</soap:Header>");
            xmlenvia.Append(@"<soap:Body>");
            xmlenvia.Append(@" <scc:consultarNombradaByConcesionaria>");
            xmlenvia.Append(@"<scc:rutEmpresa>");
            xmlenvia.Append(cr.rutEmpr);
            xmlenvia.Append(@"</scc:rutEmpresa>");
            xmlenvia.Append(@"<scc:filtro>");
            xmlenvia.Append(@"<scc:fechaInicio>");
            xmlenvia.Append(nom.fechaInicioNombrada);
            xmlenvia.Append(@"</scc:fechaInicio>");
            xmlenvia.Append(@"</scc:filtro>");
            xmlenvia.Append(@"</scc:consultarNombradaByConcesionaria>");
            xmlenvia.Append(@"</soap:Body>");
            xmlenvia.Append(@"</soap:Envelope>");
            soapEnvelopeXml.LoadXml(xmlenvia.ToString());



            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();

                    resp = soapResult;
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resp);

            List<Nombrada> nombradas = new List<Nombrada>();

            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("NombradaOutConcesionario");




            for (int i = 0; i < elemList.Count; i++)
            {
                List<TrabajadorNombrada> pers = new List<TrabajadorNombrada>();
                foreach (XmlNode per in elemList[i].ChildNodes[19])
                {
                    pers.Add(new TrabajadorNombrada
                    {
                        idContrato = per.ChildNodes[0].InnerXml,
                        respuesta = per.ChildNodes[9].InnerXml,

                    });
                }


                string fechaaux = elemList[i].ChildNodes[1].InnerXml;
                 string fecha1 = fechaaux.Substring(0, 10); 

                nombradas.Add(new Nombrada
                {
                    
                    fechaInicioNombrada = fecha1,

                    idNave = elemList[i].ChildNodes[3].InnerXml,
                    idLocacion = elemList[i].ChildNodes[5].InnerXml,
                    idPuerto = elemList[i].ChildNodes[7].InnerXml,
                    idTurno = elemList[i].ChildNodes[8].InnerXml,
                    Trabajadores = pers
                });
            }

            List<Nombrada> list = nombradas.Where(x => x.idTurno == nom.idTurno && x.fechaInicioNombrada.Contains(nom.fechaInicioNombrada)).ToList();

            NombradaDal.CargarRespuestaNombrada(list);


            return resp;

        }
        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {

            string ruta_WS = Parameter.LeerRutaws("WNombrada");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ruta_WS);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
