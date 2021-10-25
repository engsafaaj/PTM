using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPS.PL;
using PTM;
using System.Data.Entity.Migrations;

namespace EPS.AddPage
{
    public partial class Add_Groups : DevExpress.XtraEditors.XtraForm
    {
        public  int id;
        bool state;
        TB_Tasks add;
        TPMDBEntities db;
        string TaskState;
       public GroupsPage page;
       


        public Add_Groups()
        {
            InitializeComponent();
        }

        private bool Save()
        {
            // check fields

            if (radioDone.Checked == true)
            {
                TaskState = "مكتمل";
            }
            else
            {
                TaskState = "غير مكتمل";

            }

            if (edt_name.Text == "")
            {
                Message("اكمل الحقل لطفا");
            }
            else
            {
                // Add or edit
                if (id == 0)
                {
                    // Add
                    // Check Duplicate Data
                    
                   
                        // Add
                        AddData();
                        state = true;
                        page.LoadData();            
                    
                }
                else
                {
                  
                        // Edit
                        EditData();
                        state = true;
                        page.LoadData();


                    

                }
            }
            return state;
        }

        private void AddData()
        {
           
            try
            {
                db = new TPMDBEntities();
                add = new TB_Tasks
                {
                    TaskName=edt_name.Text,
                    TaskProject=edt_projectname.Text,
                    TaskDes=edt_description.Text,
                    TaskDateStart=datestart.Value,
                    TaskEnd=Convert.ToDateTime(dateend.Text),
                    TaskDuraiton=dateduration.Text,
                    TaskState= this.TaskState,
                    AddDate=DateTime.Now
                    


                };
                db.TB_Tasks.Add(add);
                db.SaveChanges();
                toastNotificationsManager1.ShowNotification("b63bb4e2-c3ce-411f-975a-c860580f1dc7");
               
            }
            catch
            {
                Message("خطأ , لطفا تحقق من متطلبات الادخال والاتصال بالسيرفر");

            }
        }


             
       


        private void EditData()
        {
            try
            {
                db = new TPMDBEntities();
                add = new TB_Tasks
                {
                    Id=this.id,
                    TaskName = edt_name.Text,
                    TaskProject = edt_projectname.Text,
                    TaskDes = edt_description.Text,
                    TaskDateStart = datestart.Value,
                    TaskEnd = Convert.ToDateTime(dateend.Text),
                    TaskDuraiton = dateduration.Text,
                    TaskState = this.TaskState,
                    AddDate = DateTime.Now

                };
                db.TB_Tasks.AddOrUpdate(add);
                db.SaveChanges();
                toastNotificationsManager1.ShowNotification("5eb82af2-6df7-42a2-89da-1a50da44d73b");

            }
            catch
            {
                Message("خطأ , لطفا تحقق من متطلبات الادخال والاتصال بالسيرفر");

            }
        }

       
        private void Message( string message)
        {
            txt_message.Visible = true;
            timer1.Enabled = true;
            txt_message.Text = message;
            txt_message.BackColor = Color.Red;
            state = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txt_message.Visible = false;
            timer1.Enabled = false;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

       

       

        private void SetDate()
        {
            try
            {
                var days = Convert.ToDouble(dateduration.Text);
                dateend.Text = datestart.Value.AddDays(days).ToShortDateString();
            }
            catch { }
          
        }

        private void datestart_ValueChanged(object sender, EventArgs e)
        {
            SetDate();
        }

        private void dateduration_TextChanged(object sender, EventArgs e)
        {
            SetDate();

        }

        private void Add_Groups_Load(object sender, EventArgs e)
        {
            SetDate();
            GetProjectListName();
        }

        private void GetProjectListName()
        {
            try
            {
                db = new TPMDBEntities();
                var listdata = db.TB_Tasks.Select(x => x.TaskProject).ToArray();

                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(listdata);
                edt_projectname.AutoCompleteCustomSource = collection;



            }
            catch
            {

            }
        }
    }
}