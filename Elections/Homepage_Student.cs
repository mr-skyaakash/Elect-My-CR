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
        private String participant;

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
            if(ds.Tables[0].Rows.Count==0)
            {
                label7.Text = "Not Stared";
                label8.Text = "Not Set";
                label9.Text = "Not Applicable";
            }
            else
            {
                label7.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                label8.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                label9.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                if (label7.Text.ToString().EndsWith("Registration"))
                    button1.Enabled = true;
                if (label7.Text.ToString().EndsWith("Ongoing"))
                    button2.Enabled = true;
                if(label7.Text.ToString().EndsWith("Finished"))
                {
                    SqlCon sq1 = new SqlCon();
                    DataSet ds1 = sq.data_access("select * from Elect");
                    int rownum = (int)ds.Tables[0].Rows.Count;
                    rownum--;
                    label14.Text = "The winner is : ";
                    label14.Text += ds.Tables[0].Rows[1].ItemArray[0].ToString();
                }
            }
            
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
                    if(label1.Text.ToString().EndsWith(ds1.Tables[0].Rows[i].ItemArray[0].ToString()))
                    {
                        if(!(bool)ds1.Tables[0].Rows[i].ItemArray[1])
                        {
                            tabControl1.SelectTab(1);
                        }
                        else
                        {
                            MessageBox.Show("You have already Participated !");
                        }
                        
                    }
                    
                }
                
            }
            else
            {
                MessageBox.Show("Sorry ! Registrations are Full !");
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
                //MessageBox.Show(ncount.ToString());
                sq1.exec_query("update Elect set TotalCandidates='"+ ncount.ToString() +"' where ElectionStatus='Registration'" );

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
            if (label7.Text.ToString().EndsWith("Registration"))
                button1.Enabled = true;
            sq.close_con();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from StudentList");
            for(int i = 0;i< ds.Tables[0].Rows.Count;i++)
            {
                if (usrname.Equals(ds.Tables[0].Rows[i].ItemArray[0]))
                {
                    if(ds.Tables[0].Rows[i].ItemArray[1].ToString().Equals("False"))
                    {
                         if(ds.Tables[0].Rows[i].ItemArray[2].ToString().Equals("False"))
                        {
                            tabControl1.SelectTab(2);
                        }
                         else
                        {
                            MessageBox.Show("You have already Voted !");
                        }
                    }

                    else
                    {
                        MessageBox.Show("You cannot Vote since you have Participated!");
                    }
                }
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.CheckedItems.Count!=1)
            {
                MessageBox.Show("Please select one and only one Name");
            }
            else
            {
                participant = checkedListBox1.CheckedItems[0].ToString();
                
                tabControl1.SelectTab(3);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Votes");
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                checkedListBox1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0]);
            }
            sq.close_con();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            label11.Text = participant + "'s Profile";
            label12.Text = "Why " + participant + " wants to become the CR ?";
            label13.Text = participant + "'s Commitments towards being the CR";

            SqlCon sq = new SqlCon();
            sq.open_con();
            DataSet ds = sq.data_access("select * from Votes");
            for(int i=0;i< ds.Tables[0].Rows.Count;i++)
            {
                if(participant.Equals(ds.Tables[0].Rows[i].ItemArray[0].ToString()))
                {
                    textBox9.Text = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    textBox10.Text = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                }
            }
            sq.close_con();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Do You Want to cast your Vote for " + participant, "Confirm Vote", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCon sq = new SqlCon();
                sq.open_con();
                sq.exec_query("update Votes set TotalVotes=TotalVotes+1 where Candidatename='" + participant + "'");
                sq.open_con();
                sq.exec_query("update Elect set TotalVotes=TotalVotes+1 where TotalVotes=TotalVotes");
                sq.open_con();
                sq.exec_query("update StudentList set hasVote='True' where [Student Name]='"+ usrname+"'");
                MessageBox.Show("Your Vote has been recorded !");
                tabControl1.SelectTab(0);
            }
        }
    }
}
