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
  public  class wsNombrada
    {
        public static Nombrada CargaNombrada(CredencialesWS CR,Nombrada nombrada)
        {
            string ruta_WS = Parameter.LeerRutaws("WNombrada");
            String resp;
            StringBuilder xmlenvia = new StringBuilder("");
            HttpWebRequest request = CreateWebRequest(ruta_WS);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            xmlenvia.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:scc=""https://sccnlp.com/"">");
            xmlenvia.Append(@"<soapenv:Header>");
            xmlenvia.Append(@"<scc:UserCredentials>");
            xmlenvia.Append(@"<scc:userName>");
            xmlenvia.Append(CR.user);
            xmlenvia.Append(@"</scc:userName>");
            xmlenvia.Append(@"<scc:password>" + CR.pass + "</scc:password>");
            xmlenvia.Append(@"</scc:UserCredentials>");
            xmlenvia.Append(@"</soapenv:Header>");
            xmlenvia.Append(@"<soapenv:Body>");
            xmlenvia.Append(@"<scc:registrarNombradas>");
            xmlenvia.Append(@"<scc:rutEmpresa>"+CR.rutEmpr+"</scc:rutEmpresa>");
            xmlenvia.Append(@"<scc:nombradas>");
            xmlenvia.Append(@"<scc:Nombrada>");
            xmlenvia.Append(@"<scc:fechaInicioNombrada>"+nombrada.fechaInicioNombrada+"</scc:fechaInicioNombrada>");
            xmlenvia.Append(@"<scc:idPuerto>"+nombrada.idPuerto+"</scc:idPuerto>");
            xmlenvia.Append(@"<scc:idTurno>"+nombrada.idTurno+"</scc:idTurno>");
            xmlenvia.Append(@"<scc:idLocacion>"+nombrada.idLocacion+"</scc:idLocacion>");
            if(nombrada.idNave != null && nombrada.idNave != "") { 
            xmlenvia.Append(@"<scc:idNave>"+nombrada.idNave+"</scc:idNave>");

            }



            xmlenvia.Append(@"<scc:trabajadores>");
            
          
            foreach(TrabajadorNombrada tr in nombrada.Trabajadores) { 
            xmlenvia.Append(@"<scc:TrabajadorNombrada>");
            xmlenvia.Append(@"<scc:idContrato>"+tr.idContrato+"</scc:idContrato>");
            xmlenvia.Append(@"<scc:idLabor>"+tr.idLabor+"</scc:idLabor>");
            xmlenvia.Append(@"<scc:idFuncion>"+tr.idFuncion+"</scc:idFuncion>");
            xmlenvia.Append(@"</scc:TrabajadorNombrada>");


            }


            xmlenvia.Append(@"</scc:trabajadores>");
            xmlenvia.Append(@"</scc:Nombrada>");
            xmlenvia.Append(@"</scc:nombradas>");



            xmlenvia.Append(@"</scc:registrarNombradas>");

            xmlenvia.Append(@"</soapenv:Body>");
            xmlenvia.Append(@"</soapenv:Envelope>");

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

            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("registrarNombradasResult");

            string Estado = elemList[0].ChildNodes[0].InnerXml;

            XmlElement root1 = doc.DocumentElement;
            XmlNodeList elemList1 = root1.GetElementsByTagName("NombradaCreadaDetalle");


            if (Estado.Equals("0"))
            {

                nombrada.idNombrada = elemList1[0].ChildNodes[0].InnerXml;
                nombrada.respuesta = "OK";

            }
            else
            {
                nombrada.idNombrada = "0";
                nombrada.respuesta = elemList1[0].ChildNodes[1].InnerXml; ;



            }


            return nombrada;
            //return RelacionLaboralDAL.ActualizRL(RL);


        }

        public static HttpWebRequest CreateWebRequest(string req)
        {

            string ruta_WS = req;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ruta_WS);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
