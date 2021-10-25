using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using EPS.AddPage;
using EPS.PL;
using PTM;

namespace EPS
{
    public partial class FRM_Start : SplashScreen
    {
        int st;
        public FRM_Start()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 2021-" + DateTime.Now.Year.ToString() + " - By Safaa Jassim";
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void peImage_EditValueChanged(object sender, EventArgs e)
        {

        }

        private async void FRM_Start_Load(object sender, EventArgs e)
        {
            await Task.Run(() => Thread.Sleep(2000));

            // Check Login 

            lb_state.Text = "... الاتصال بقاعدة البيانات";
            var state = await Task.Run(() => CheckLogin());
            if (state == 1)
            {
                Main main = new Main();
                main.Show();
                Hide();
            }
            
            else
            {
                MessageBox.Show("خطأ في الاتصال في قاعدة البيانات , يبدو ان لديك مشكلة في عملية تثبيت البرنامج ");
                Application.Exit();
            }

        }

        private int CheckLogin()
        {
            try
            {
                TPMDBEntities db = new TPMDBEntities();

                var data = db.TB_Tasks.Select(x=>x.TaskName).ToList();
                if (data.Count >=0)
                {
                    st = 1;
                }
                else
                {
                    st = 0;
                }


            }
            catch
            {
                return 0;
            }
            return st;

        }
    }
}