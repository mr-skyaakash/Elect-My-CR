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
    public partial class Form1 : Form
    {
        public Form1()
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

            String name = "";
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (textBox1.Text.Equals(ds.Tables[0].Rows[i].ItemArray[0]) && textBox2.Text.Equals(ds.Tables[0].Rows[i].ItemArray[1]))
                {
                    name = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    MessageBox.Show("Welcome " + name);
                }
            }
            this.Close();
            Form2 frm = new Elections.Form2(name);
            frm.Show();

            sq.close_con();
            
        }
    }
}
