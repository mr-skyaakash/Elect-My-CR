using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elections
{
    public partial class Homepage_Student : Form
    {
        private String usrname;

        public Homepage_Student(String name)
        {
            InitializeComponent();
            label1.Text = "Welcome, " + name;
            usrname = name;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            label7.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            label8.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            label9.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            if (label7.Text.ToString().EndsWith("Participate"))
                button1.Enabled = true;
            sq.close_con();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
;        }

        private void label6_Click(object sender, EventArgs e)
        {
            StartPage st = new StartPage();
            st.Show();
            this.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            int max_can = (int)ds.Tables[0].Rows[0].ItemArray[1];
            int tot_can = (int)ds.Tables[0].Rows[0].ItemArray[2];
            if(tot_can < max_can)
            {
                DataSet ds1 = sq.data_access("select * from StudentList");
                for(int i= 0;i<ds1.Tables[0].Rows.Count;i++)
                {
                    if(label1.Text.ToString().EndsWith(ds1.Tables[0].Rows[i].ItemArray[0].ToString()) && !(bool)ds1.Tables[0].Rows[i].ItemArray[1])
                    {
                        
                        tabControl1.SelectTab(1);
                    }
                    else
                    {
                        MessageBox.Show("You have already Registered !");
                    }
                }
                
            }
            sq.close_con();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                int att = Int32.Parse(textBox3.Text.ToString());
                int bck = Int32.Parse(textBox4.Text.ToString());
                SqlCon sq = new SqlCon();
                sq.open_con();
                sq.exec_query("insert into Votes values ('" + usrname + "','"+textBox1.Text.ToString()+"','"+textBox2.Text.ToString() + "'," + att.ToString() + "," + bck.ToString() +",0)");
                button4.Enabled = false;
                MessageBox.Show("Thanks for Participating ! Your Submission has been Recorded !");
                SqlCon sq1 = new SqlCon();
                sq1.open_con();
                sq1.exec_query("update StudentList set hasParticipate='True' where [Student Name]='" + usrname + "'");
                sq1.open_con();
                DataSet ds = sq1.data_access("select * from Elect");
                int ncount = (int)ds.Tables[0].Rows[0].ItemArray[2] + 1;
                sq1.exec_query("update Elect set TotalCandidates='"+ ncount.ToString() +"' where ElectionStatus='Participate'" );

                tabControl1.SelectTab(0);
                update();
            }
            else
            {
                MessageBox.Show("Kindly Fill in all the Details !");
            }
        }

        private void update()
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            label7.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            label8.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            label9.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            if (label7.Text.ToString().EndsWith("Participate"))
                button1.Enabled = true;
            sq.close_con();
        }
    }
}
