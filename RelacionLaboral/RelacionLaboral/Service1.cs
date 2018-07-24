using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RelacionLaboral
{
    public partial class Service1 : ServiceBase
    {
        Timer timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new System.Timers.Timer(30000D);//1000 MILISEGUNDOS O 1 MINUTO
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();

        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //HACER EL PROCESO
        }



    }
}
