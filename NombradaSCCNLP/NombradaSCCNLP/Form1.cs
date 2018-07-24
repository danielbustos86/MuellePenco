using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BOL;

namespace NombradaSCCNLP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime mifecha = dateTimePicker1.Value.Date;


            string FECHA = mifecha.Date.ToString("yyyy-MM-dd");
            string turno = comboBox1.Text;

            if (turno == "")
            {
                MessageBox.Show("DEBE SELECCIONAR UN TURNO");
                return;

            }

            CredencialesWS credenciales = new CredencialesWS();
            credenciales = Parameter.LeerCredencialesws();


         List<Nombrada> nombradas = DAL.NombradaDal.RecuperaRL(FECHA, turno);

            foreach(Nombrada n in nombradas)
            {
                Nombrada n1 = new Nombrada();                    
                n1  = wsNombrada.CargaNombrada(credenciales, n);

                NombradaDal.ActualizRL(n1);

                if(n1.respuesta !="OK")
                {

                    MessageBox.Show(n1.respuesta);
                }



            }





        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime mifecha = dateTimePicker1.Value.Date;


            string FECHA = mifecha.Date.ToString("yyyy-MM-dd");
            string turno = comboBox1.Text;

            if (turno == "")
            {
                MessageBox.Show("DEBE SELECCIONAR UN TURNO");
                return;

            }

            CredencialesWS credenciales = new CredencialesWS();
            credenciales = Parameter.LeerCredencialesws();

            Nombrada nom = new Nombrada();
            nom.fechaInicioNombrada = FECHA;
            nom.idTurno = turno;

            string resp = WsRecuperaNombrada.Execute(credenciales, nom);



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
