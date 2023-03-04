using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyJobs2023
{
    public partial class Form1 : Form
    {
        public static SQLiteConnection conn;
        public static SQLiteCommand cmd;
        public static SQLiteDataReader dr;
        public static string path = Database.path;
        public static string cs = @"URI=file:"+path;


        public Form1()
        {
            InitializeComponent();
            Database.Create_db();
            ara();
            this.BackColor = Color.White;
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;



            this.dataGridView1.AllowUserToAddRows = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;


            ContextMenuStrip s = new ContextMenuStrip();
            ToolStripMenuItem goster = new ToolStripMenuItem();
            goster.Text = "Delete This";
            goster.Click += goster_Click;
            s.Items.Add(goster);

            this.ContextMenuStrip = s;


            timer1.Enabled = true;
            timer1.Interval = 3600000; // 60 * 60 * 1000 (1 hour)



        }
        void goster_Click(object sender, EventArgs e)
        {
            string id = textBox3.Text;

            if (id=="")
            {
                
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Sure", "Delete This ?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {

                        var con = new SQLiteConnection(cs);
                        SQLiteDataReader dr;
                        con.Open();

                        //string stm = "select * FROM data ORDER BY id ASC  ";
                        //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
                        string stm = "delete from backup1 where id = "+id;
                        var cmd = new SQLiteCommand(stm, con);
                        dr = cmd.ExecuteReader();

                       

                        con.Close();

                        ara();
                        textBox1.Text="";
                        textBox2.Text="";
                        textBox3.Text="";

                        // dataGridView1.Columns[0].Visible = false;



                    }
                    catch
                    {

                    }
                }
                else if (dialogResult == DialogResult.No)
                {

                    //do something else
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dest1 = textBox1.Text;
            string dest2 = textBox2.Text;
            string dest3 = DateTime.Now.ToString("yyyy-MM-dd");
            string name1 = Environment.UserName.ToString();
            string name2 = Environment.MachineName.ToString();

            string dest4 = name1 +" - "+ name2;
            string dest5 = dest1 +" - "+ dest2;
            if (((dest1 != "") && (dest2 != "")) && (dest1 != dest2))
            {
                Database.dataadd(dest1, dest2,dest3,dest4,dest5);
                textBox1.Text="";
                textBox2.Text="";
                textBox3.Text="";
                ara();
            }
            else 
            {
                MessageBox.Show("0000");

            }



        }

      
        public void copyAll2()
        {
            int rown = dataGridView1.RowCount;

            for (int i = 0; i < rown; i++)
            {

                DataGridViewRow dataGridViewRow = dataGridView1.Rows[i];

               
            

            string q = "";
            try
            {
                     string folder1 = dataGridViewRow.Cells["dest1"].Value.ToString();
                    string folder2 = dataGridViewRow.Cells["dest2"].Value.ToString();
                    String komut = @"robocopy "+folder1+ " "+ folder2 +" /MIR /MT /R:5 /W:15 /NS /NC /NFL /NDL /NP /LOG+:log.txt";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                }



            }
            catch (Exception ex)
            {

                q += "error";

            }
            }



        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                // shows the path to the selected folder in the folder dialog
                textBox1.Text=  fbd.SelectedPath;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                // shows the path to the selected folder in the folder dialog
                textBox2.Text=  fbd.SelectedPath;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
             int  rown = dataGridView1.RowCount;

             for (int i = 0; i< rown; i++)
             {
                 this.BackColor = Color.Red;
                 button1.BackColor = Color.Red;
                 button2.BackColor = Color.Red;
                 button3.BackColor = Color.Red;
                 dataGridView1.BackgroundColor = Color.Red;
                 DataGridViewRow dataGridViewRow = dataGridView1.Rows[i];
                 textBox1.Text = dataGridViewRow.Cells["dest1"].Value.ToString();
                 textBox2.Text = dataGridViewRow.Cells["dest2"].Value.ToString();
                 copyAll2();
                 this.BackColor = Color.White;
                 button1.BackColor = Color.White;
                 button2.BackColor = Color.White;
                 button3.BackColor = Color.White;
                 dataGridView1.BackgroundColor = Color.White;
             }
            */
            if (backgroundWorker1.IsBusy != true)
            {

                this.BackColor = Color.Red;
                button1.BackColor = Color.Red;
                button2.BackColor = Color.Red;
                button3.BackColor = Color.Red;
                dataGridView1.BackgroundColor = Color.Red;

                backgroundWorker1.RunWorkerAsync();
                button2.Text = "STOP";

            }
            else
            {
                this.BackColor = Color.White;
                button1.BackColor = Color.White;
                button2.BackColor = Color.White;
                button3.BackColor = Color.White;
                dataGridView1.BackgroundColor = Color.White;
                backgroundWorker1.CancelAsync();
                button2.Text = "Copy Now";
            }



        }

        public void ara()
        {
           
            dataGridView1.Rows.Clear();

            var con = new SQLiteConnection(cs);
            SQLiteDataReader dr;
            con.Open();


            string stm = "select * FROM backup1 order by id DESC ";

            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString());

            }

            con.Close();



            // dataGridView1.Columns[0].Visible = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = dataGridViewRow.Cells["dest1"].Value.ToString();
                textBox2.Text = dataGridViewRow.Cells["dest2"].Value.ToString();
                textBox3.Text = dataGridViewRow.Cells["id"].Value.ToString();
         



            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string id = textBox3.Text;

            if (id=="")
            {
                button1.Text = "Save";
            }
            else 
            {
                button1.Text = "Update";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text="";
            textBox2.Text="";
            textBox3.Text="";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            copyAll2();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MessageBox.Show("asd");

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            button2.Text = "Copy Now";
            this.BackColor = Color.White;
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {

                this.BackColor = Color.Red;
                button1.BackColor = Color.Red;
                button2.BackColor = Color.Red;
                button3.BackColor = Color.Red;
                dataGridView1.BackgroundColor = Color.Red;

                backgroundWorker1.RunWorkerAsync();
                button2.Text = "STOP";

            }
            else
            {
                this.BackColor = Color.White;
                button1.BackColor = Color.White;
                button2.BackColor = Color.White;
                button3.BackColor = Color.White;
                dataGridView1.BackgroundColor = Color.White;
                backgroundWorker1.CancelAsync();
                button2.Text = "Copy Now";
            }
        }
    }
}
