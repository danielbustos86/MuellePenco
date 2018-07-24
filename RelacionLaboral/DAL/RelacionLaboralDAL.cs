using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using BOL.Helpers;

namespace DAL
{
    public class RelacionLaboralDAL
    {

        public static List<BOL.RelacionLaboral> RecuperaRL()
        {
            List<BOL.RelacionLaboral> ls_relacion = new List<BOL.RelacionLaboral>();
            try

            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RELACION_LABORAL_RECUPERA";

                SqlDataReader rdr = cmd.ExecuteReader();
               
                while (rdr.Read())
                {
                    ls_relacion.Add(new BOL.RelacionLaboral
                    {
                      
                        correlativo  = Int32.Parse(rdr["correlativo"].ToString()),
                        rutEmpresa = rdr["rutEmpresa"].ToString(),
                        idPuerto = rdr["idPuerto"].ToString(),
                        rut = rdr["rut"].ToString(),
                        dv = rdr["dv"].ToString(),
                        pasaporte = rdr["pasaporte"].ToString(),
                        nombres = rdr["nombres"].ToString(),
                        apellidoPaterno = rdr["apellidoPaterno"].ToString(),
                        apellidoMaterno = rdr["apellidoMaterno"].ToString(),
                        idNacionalidad = rdr["idNacionalidad"].ToString(),
                        fechaNacimiento = rdr["fechaNacimiento"].ToString(),
                        idEstadoCivil = rdr["idEstadoCivil"].ToString(),
                        idSexo = rdr["idSexo"].ToString(),
                        idRegion = rdr["idRegion"].ToString(),
                        idComuna = rdr["idComuna"].ToString(),
                        calle = rdr["calle"].ToString(),
                        numero = rdr["numero"].ToString(),
                        depto = rdr["depto"].ToString(),
                        block = rdr["block"].ToString(),
                        email = rdr["email"].ToString(),
                        idIsapre = rdr["idIsapre"].ToString(),
                        idAFP = rdr["idAFP"].ToString(),
                        fechaInicioContrato = rdr["fechaInicioContrato"].ToString(),
                        idTipoContrato = rdr["idTipoContrato"].ToString(),
                        fechaTerminoContrato = rdr["fechaTerminoContrato"].ToString(),
                        idModalidad = rdr["idModalidad"].ToString(),
                        horasExtraAutorizadas = rdr["horasExtraAutorizadas"].ToString(),
                        remuneracionBruta = rdr["remuneracionBruta"].ToString(),
                        idLabor = rdr["idLabor"].ToString(),
                        idFuncion = rdr["idFuncion"].ToString(),
                        idLocacion = rdr["idLocacion"].ToString(),
                        idJornada = rdr["idJornada"].ToString(),
                        dia = rdr["dia"].ToString(),
                        horaDesde = rdr["horaDesde"].ToString(),
                        horaHasta = rdr["horaHasta"].ToString(),
                        horaDescansoDesde = rdr["horaDescansoDesde"].ToString(),
                        horaDescansoHasta = rdr["horaDescansoHasta"].ToString(),
                        horasSemana = rdr["horasSemana"].ToString(),
                        idSindicato = rdr["idSindicato"].ToString(),
                        idContrato = rdr["idContrato"].ToString(),
                        turno = rdr["turno"].ToString(),
                        fecha = rdr["fecha"].ToString()
                    });
                }

                cmd.Connection.Close();
                cmd.Dispose();
                return ls_relacion;
            }
            catch (Exception e)
            {

                string msj = "";
                msj = e.Message.ToString();
                ls_relacion = null;
                return ls_relacion;
            }
        }



        public static string  ActualizRL(RelacionLaboral rl)
        {
            try

            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RELACION_LABORAL_ACTUALIZA";
                cmd.Parameters.AddWithValue("@correlativo", rl.correlativo);
                if (rl.idContrato == null || rl.idContrato == "0")
                {
                    cmd.Parameters.AddWithValue("@idContrato", System.DBNull.Value);

                }
                else {

                    cmd.Parameters.AddWithValue("@idContrato", rl.idContrato);
                }
               
                cmd.Parameters.AddWithValue("respuesta", rl.Respuesta);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (SqlException exp)
            {
                return exp.Message.ToString();
            }

        }
    }
    }
