using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOL;
using DAL;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<RelacionLaboral> relacionLaborals = new List<RelacionLaboral>();
            relacionLaborals = RelacionLaboralDAL.RecuperaRL();
            CredencialesWS credencialesWS = new CredencialesWS();
            credencialesWS = Parameter.LeerCredencialesws();

            string resp;


            foreach (RelacionLaboral R in relacionLaborals)
            {
                
                resp = wsRelacionLaboral.Contrato(credencialesWS, R);

               


            }






        }
    }
}
