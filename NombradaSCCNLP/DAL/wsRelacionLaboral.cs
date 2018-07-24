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
   public class wsRelacionLaboral
    {
        public static string Contrato(CredencialesWS CR,RelacionLaboral RL)
        {
            string ruta_WS = Parameter.LeerRutaws("RLABORAL");
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
            xmlenvia.Append(@"<scc:password>"+CR.pass+"</scc:password>");
            xmlenvia.Append(@"</scc:UserCredentials>");
            xmlenvia.Append(@"</soapenv:Header>");
            xmlenvia.Append(@"<soapenv:Body>");
            xmlenvia.Append(@"<scc:registrarContratos>");
            xmlenvia.Append(@"<scc:rutEmpresa>"+CR.rutEmpr+"</scc:rutEmpresa>");
            xmlenvia.Append(@"<scc:idPuerto>"+RL.idPuerto+"</scc:idPuerto>");
            xmlenvia.Append(@"<scc:contratos>");
            xmlenvia.Append(@"<scc:Contrato>");
            xmlenvia.Append(@"<scc:trabajador>");
            xmlenvia.Append(@"<scc:rut>"+RL.rut+"</scc:rut>");
            xmlenvia.Append(@"<scc:dv>"+RL.dv+"</scc:dv>");
            xmlenvia.Append(@"<scc:pasaporte></scc:pasaporte>");
            xmlenvia.Append(@"<scc:nombres></scc:nombres>");
            xmlenvia.Append(@"<scc:apellidoPaterno></scc:apellidoPaterno>");
            xmlenvia.Append(@"<scc:apellidoMaterno></scc:apellidoMaterno>");
            xmlenvia.Append(@"<scc:idEstadoCivil>"+RL.idEstadoCivil+"</scc:idEstadoCivil>");
            xmlenvia.Append(@"<scc:idSexo>"+RL.idSexo+"</scc:idSexo>");
            xmlenvia.Append(@"<scc:domicilio>");
            xmlenvia.Append(@"<scc:idRegion>"+RL.idRegion+"</scc:idRegion>");
            xmlenvia.Append(@"<scc:idComuna>"+RL.idComuna+"</scc:idComuna>");
            xmlenvia.Append(@"<scc:calle>"+RL.calle+"</scc:calle>");
            xmlenvia.Append(@"<scc:numero>"+RL.numero+"</scc:numero>");
            xmlenvia.Append(@"<scc:depto></scc:depto>");
            xmlenvia.Append(@"<scc:block></scc:block>");
            xmlenvia.Append(@"</scc:domicilio>");
            xmlenvia.Append(@"<scc:email>"+RL.email+"</scc:email>");
            xmlenvia.Append(@"<scc:idIsapre>"+RL.idIsapre+"</scc:idIsapre>");
            xmlenvia.Append(@"<scc:idAFP>"+RL.idAFP+"</scc:idAFP>");
            xmlenvia.Append(@"</scc:trabajador>");
            xmlenvia.Append(@"<scc:fechaCelebContrato>"+RL.fecha+"</scc:fechaCelebContrato>");
            xmlenvia.Append(@"<scc:idTipoContrato>"+RL.idTipoContrato+"</scc:idTipoContrato>");
            xmlenvia.Append(@"<scc:fechaInicioContrato>"+RL.fechaInicioContrato+"</scc:fechaInicioContrato>");
            xmlenvia.Append(@"<scc:fechaTerminoContrato>"+RL.fechaTerminoContrato+"</scc:fechaTerminoContrato>");
            xmlenvia.Append(@"<scc:idModalidad>"+RL.idModalidad+"</scc:idModalidad>");
            xmlenvia.Append(@"<scc:horasExtraAutorizadas>"+RL.horasExtraAutorizadas+"</scc:horasExtraAutorizadas>");
            xmlenvia.Append(@"<scc:remuneracionBruta>"+RL.remuneracionBruta+"</scc:remuneracionBruta>");
            xmlenvia.Append(@"<scc:labores>");
            xmlenvia.Append(@"<scc:idLabor>"+RL.idLabor+"</scc:idLabor>");
            xmlenvia.Append(@"<scc:idFuncion>"+RL.idFuncion+"</scc:idFuncion>");
            xmlenvia.Append(@"<scc:idLocacion>"+RL.idLocacion+"</scc:idLocacion>");
            xmlenvia.Append(@"<scc:idJornada>"+RL.idJornada+"</scc:idJornada>");
            xmlenvia.Append(@"<scc:horario>");
            xmlenvia.Append(@"<scc:ContratoHorario>");

            if (RL.turno.Equals("3")) { 
            xmlenvia.Append(@"<scc:dia>"+RL.dia+"</scc:dia>");
            xmlenvia.Append(@"<scc:horaDesde>"+RL.horaDesde+"</scc:horaDesde>");
            xmlenvia.Append(@"<scc:horaHasta>23:59</scc:horaHasta>");

                int nextDia = 0;
                int DiaActual = 0;
                DiaActual = int.Parse(RL.dia);
                if (DiaActual == 6)
                {
                    nextDia = 0;
                }
                else
                {
                    nextDia = DiaActual + 1; 

                }

                xmlenvia.Append(@"<scc:dia>" + nextDia.ToString()+ "</scc:dia>");
                xmlenvia.Append(@"<scc:horaDesde>00:00</scc:horaDesde>");
                xmlenvia.Append(@"<scc:horaHasta>" + RL.horaHasta + "</scc:horaHasta>");


            }
            else
            {
                xmlenvia.Append(@"<scc:dia>" + RL.dia + "</scc:dia>");
                xmlenvia.Append(@"<scc:horaDesde>" + RL.horaDesde + "</scc:horaDesde>");
                xmlenvia.Append(@"<scc:horaHasta>" + RL.horaHasta + "</scc:horaHasta>");

            }

            xmlenvia.Append(@"</scc:ContratoHorario>");
            xmlenvia.Append(@"</scc:horario>");
            xmlenvia.Append(@"<scc:acuerdoDescanso>");
            xmlenvia.Append(@"<scc:horaDesde>"+RL.horaDescansoDesde+"</scc:horaDesde>");
            xmlenvia.Append(@"<scc:horaHasta>"+RL.horaDescansoHasta+"</scc:horaHasta>");
            xmlenvia.Append(@"</scc:acuerdoDescanso>");
            xmlenvia.Append(@"</scc:labores>");
            xmlenvia.Append(@"</scc:Contrato>");
            xmlenvia.Append(@"</scc:contratos>");
            xmlenvia.Append(@"</scc:registrarContratos>");
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
            XmlNodeList elemList = root.GetElementsByTagName("registrarContratosResult");

            string Estado = elemList[0].ChildNodes[0].InnerXml;

            XmlElement root1 = doc.DocumentElement;
            XmlNodeList elemList1 = root1.GetElementsByTagName("ContratoCreadoDetalle");


            if (Estado.Equals("0"))
            {

                RL.idContrato = elemList1[0].ChildNodes[5].InnerXml;
                RL.Respuesta = "OK";

            }
            else {
                RL.idContrato = "0";
                RL.Respuesta = elemList1[0].ChildNodes[5].InnerXml;



            }


          return  RelacionLaboralDAL.ActualizRL(RL);

            
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
