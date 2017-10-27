using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Elections
{
    public partial class Admin : Form
    {
        String usrname;

        public Admin(String name)
        {
            InitializeComponent();
            usrname = name;
            label13.Text = "Welcome, " + usrname;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            if(ds.Tables[0].Rows.Count == 0)
            {
                comboBox1.Text = "";
                textBox1.Text = "";
                label11.Text = "";
                label12.Text = "";
            }
            else
            {
                comboBox1.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                textBox1.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                label11.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                label12.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            sq.close_con();
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            StartPage st = new StartPage();
            st.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void update()
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            if (ds.Tables[0].Rows.Count == 0)
            {
                comboBox1.Text = "";
                textBox1.Text = "";
                label11.Text = "";
                label12.Text = "";
            }
            else
            {
                comboBox1.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                textBox1.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                label11.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                label12.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            sq.close_con();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to Reset ? ", "Confirm Reset", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                SqlCon sq = new SqlCon();
                sq.open_con();
                sq.exec_query("delete from Elect");
                sq.open_con();
                sq.exec_query("update StudentList set hasParticipate='False',hasVote='False' where (hasParticipate='True' or hasVote='True')");
                sq.open_con();
                sq.exec_query("delete from Votes");
                MessageBox.Show("Elections Resetted !");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(comboBox1.Text!="" && textBox1.Text!="")
            {
                
                Regex r = new Regex(@"^\d{1,2}$");
                Match m = r.Match(textBox1.Text);
                if(m.Success)
                {
                    if(textBox1.Text.ToString().Equals("0") || textBox1.Text.ToString().Equals("1"))
                    {
                        MessageBox.Show("A Election must have at least 2 candidates ! Try changing Max Candidate Values !");
                    }
                    else
                    {
                        SqlCon sq = new SqlCon();
                        sq.open_con();
                        DataSet ds = sq.data_access("select * from Elect");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            sq.exec_query("insert into ELect values ('" + comboBox1.Text.ToString() + "','" + textBox1.Text.ToString() + "','0','0')");
                        }
                        else
                        {
                            sq.exec_query("update Elect set ElectionStatus='" + comboBox1.Text.ToString() + "',MaxCandidates='" + textBox1.Text.ToString() + "' where TotalCandidates='" + ds.Tables[0].Rows[0].ItemArray[2] + "'");
                        }
                        MessageBox.Show("Changes Committed !");
                    }
                    
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from "+ comboBox2.SelectedItem.ToString());
            dataGridView1.DataSource = ds.Tables[0];
            sq.close_con();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Elect");
            if(ds.Tables[0].Rows[0].ItemArray[0].ToString().Equals("Finished"))
            {
                
                //if ((int)ds.Tables[0].Rows[0].ItemArray[2] > 2 && (int)ds.Tables[0].Rows[0].ItemArray[3] > 1)
                {
                    MessageBox.Show("Hello");
                    int f = 0;
                    int max = (int)ds.Tables[0].Rows[0].ItemArray[1];
                    ds = sq.data_access("select Candidatename,TotalVotes from Votes order by TotalVotes desc");
                    for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (max == (int)ds.Tables[0].Rows[i].ItemArray[1])
                        {
                            f = 1;
                            break;
                        }
                    }

                    if (f == 1)
                    {
                        ds = sq.data_access("select * from Votes where TotalVotes='" + max.ToString() + "'");

                    }
                    else
                    {
                        sq.exec_query("insert into Elect values ('" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "',0,0,0)");
                    }
                }
                MessageBox.Show("Election Finished");
            }
            else
            {
                MessageBox.Show("Please Finish the Elections First");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }
    }
}
