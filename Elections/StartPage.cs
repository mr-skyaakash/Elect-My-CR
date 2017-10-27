using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Elections
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from LoginCred");
            int f = 0;
            String name = "";
            int i;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (textBox1.Text.Equals(ds.Tables[0].Rows[i].ItemArray[0]) && textBox2.Text.Equals(ds.Tables[0].Rows[i].ItemArray[1]))
                {
                    name = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    f = 1;
                    break;
                }
            }

            if(f==1)
            {
                this.Close();

                if(ds.Tables[0].Rows[i].ItemArray[3].ToString().Equals("True"))
                {
                    Admin ad = new Admin(name);
                    ad.Show();
                }

                else
                {
                    Homepage_Student frm = new Elections.Homepage_Student(name);
                    frm.Show();
                }


                
            }
            else
            {
                MessageBox.Show("Incorrect Credentials ! Please Try Again !");
            }

            sq.close_con();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
