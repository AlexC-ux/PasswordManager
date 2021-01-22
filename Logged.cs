using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class Logged : Form
    {

        AutoCompleteStringCollection auCompl = new AutoCompleteStringCollection();

        public string all_data = null;
        string[] spData = null;

        public static List<string> resourses = new List<string>();
        public static List<string> logins = new List<string>();
        public static List<string> passws = new List<string>();

        Des DesAlg = new Des();

        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager");
        public Logged()
        {
            InitializeComponent();
        }

        private void Logged_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.crypted)
            {
                DesAlg.DecryptDataFile();
            }
            while (Properties.Settings.Default.crypted == true)
            {
                continue;
            }
               
            resourses.Clear();
            logins.Clear();
            passws.Clear();

            all_data = File.ReadAllText(Path.Combine(dir, "data"));
            
            if (File.ReadAllBytes(Path.Combine(dir, "data")).Length > 3)
            {


                spData = all_data.Split(new[] { "<--!-->" }, StringSplitOptions.None);
                //<--!-->example.com<!-!>login<!-!>password
                foreach (string block in spData)
                {
                    if (block.Length > 7)
                    {
                        
                        string[] bList = block.Split(new[] { "<!-!>" }, StringSplitOptions.None);
                        if (bList.Length==3)
                        {
                            resourses.Add(bList[0]);
                            logins.Add(bList[1]);
                            passws.Add(bList[2]);
                        }
                        
                    }
                }

                
            }
           

        }

        private void Logged_FormClosing(object sender, FormClosingEventArgs e)
        {
           
           

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            

            //Application.Exit();
        }

        private void Logged_Activated(object sender, EventArgs e)
        {

            auCompl.Clear();
            auCompl.AddRange(resourses.ToArray());

            textBox1.AutoCompleteCustomSource = auCompl;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            if (File.Exists(Path.Combine(dir, "data")))
            {

                label2.Hide();
                label3.Hide();
                label4.Hide();

            }

            




            

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            New nF = new New();
            nF.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (resourses.Contains(textBox1.Text))
            {
                label2.Text = "Resource: " + textBox1.Text;
                label3.Text ="Login: "+ logins[resourses.IndexOf(textBox1.Text)];
                label4.Text ="Pass: "+ passws[resourses.IndexOf(textBox1.Text)];

                label2.Show();
                label3.Show();
                label4.Show();
            }
            else
            {
                MessageBox.Show("Try to add credentais from this resource.");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeletePasw dp = new DeletePasw();
            dp.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will delete your actual password.\nYou can choose new at restart.\nAre you sure?","Password", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Properties.Settings.Default.newpass = true;
                Properties.Settings.Default.Save();
                FirstRun fr = new FirstRun();
                fr.Show();
                this.Hide();
            }
        }

        private void Logged_FormClosed(object sender, FormClosedEventArgs e)
        {
            DesAlg.EncrypDataFile();
            Application.ExitThread();
        }
    }
}
