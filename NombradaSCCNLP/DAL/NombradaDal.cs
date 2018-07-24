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
 public   class NombradaDal
    {
        public static List<Nombrada> RecuperaRL(string fecha,string turno)
        {
            List<Nombrada> ls_nombrada = new List<Nombrada>();
            try

            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CabeceraNombrada";
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@turno", turno);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ls_nombrada.Add(new Nombrada
                    {
                        fechaInicioNombrada = rdr["nom_fechainicontrato"].ToString(),
                        idTurno = rdr["nom_idturno"].ToString(),
                        idPuerto = rdr["nom_idpuerto"].ToString(),
                        idNave = rdr["nom_idnave"].ToString(),
                        idLocacion = rdr["nom_idlocacion"].ToString()

                    });
                }

                cmd.Connection.Close();
                cmd.Dispose();
                
                foreach(Nombrada n in ls_nombrada)
                {

                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataAdapter da1= new SqlDataAdapter(cmd);
                    Coneccion param1 = Parameter.Leer_parametros();
                    cmd1.Connection = new SqlConnection(param.ConString);
                    cmd1.Connection.Open();
                    cmd1.Parameters.Clear();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "DetalleNombrada";
                    cmd1.Parameters.AddWithValue("@fechaturno", fecha);
                    cmd1.Parameters.AddWithValue("@turno", turno);

                    if (n.idNave == null || n.idNave == "0" || n.idNave == "")
                    {
                        cmd1.Parameters.AddWithValue("@nave", System.DBNull.Value);

                    }
                    else {
                        cmd1.Parameters.AddWithValue("@nave", n.idNave);

                    }
        
                    cmd1.Parameters.AddWithValue("@locacion",n.idLocacion);


                    SqlDataReader rdr1 = cmd1.ExecuteReader();

                    List<TrabajadorNombrada> ltrabadores = new List<TrabajadorNombrada>();

                    while (rdr1.Read())
                    {
                        ltrabadores.Add(new TrabajadorNombrada
                        {
                            idContrato = rdr1["nom_idcontrato"].ToString(),
                            idLabor = rdr1["nom_idlabor"].ToString(),
                            idFuncion = rdr1["nom_idfuncion"].ToString()

                        });
                    }

                    cmd1.Connection.Close();
                    cmd1.Dispose();



                    n.Trabajadores = ltrabadores;

                }

                return ls_nombrada;

            }
            catch (SqlException e)
            {
                string resp = e.Message.ToString();
         
                return ls_nombrada;
            }
        }




        public static string ActualizRL(Nombrada n)
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
                cmd.CommandText = "SP_ACTUALIZA_NOMBRADA";
                cmd.Parameters.AddWithValue("@fechaturno", n.fechaInicioNombrada);
                cmd.Parameters.AddWithValue("@turno", n.idTurno);

                if (n.idNave == null || n.idNave == "0" || n.idNave == "")
                {
                    cmd.Parameters.AddWithValue("@nave", System.DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@nave", n.idNave);

                }

                cmd.Parameters.AddWithValue("@locacion", n.idLocacion);

                if(n.idNombrada.Equals("0") || n.idNombrada.Equals("") || n.idNombrada == null)
                {
                    cmd.Parameters.AddWithValue("@idNombrada", System.DBNull.Value);

                }
                else { 
                cmd.Parameters.AddWithValue("@idNombrada", n.idNombrada);
                }


                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (SqlException exp)
            {
                return exp.Message.ToString();
            }

        }


        public static string CargarRespuestaNombrada(List<Nombrada> list)
        {
            string resp = "";

            foreach (Nombrada nom in list)
            {
                foreach (TrabajadorNombrada per in nom.Trabajadores)
                {
                    try
                    {

                   

                        SqlCommand cmd = new SqlCommand();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        Coneccion param = Parameter.Leer_parametros();
                        cmd.Connection = new SqlConnection(param.ConString);
                        cmd.Connection.Open();
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_ACTUALIZAR_ESTADO_NOMBRADA";
                        cmd.Parameters.AddWithValue("@fecha", nom.fechaInicioNombrada);
                        cmd.Parameters.AddWithValue("@turno", nom.idTurno);
                        cmd.Parameters.AddWithValue("@idcontrato", per.idContrato);
                        cmd.Parameters.AddWithValue("@respuesta", per.respuesta);
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        cmd.Dispose();

                        //DataRow row = dt.Rows[0];

                        //string resp = row["respuesta"].ToString();





                        resp = "ok";
                    }
                    catch (SqlException exp)
                    {
                        resp = exp.Message.ToString();
                    }




                }


            }

            return resp;

        }






    }
}
