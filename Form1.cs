using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PasswordManager
{
    public partial class Form1 : Form
    {

        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager");

        

        int strt_x = Screen.PrimaryScreen.WorkingArea.Width / 3;
        int strt_y = Screen.PrimaryScreen.WorkingArea.Height / 4;
        
        int c_x;
        int c_y;

        bool moving;
        public Form1()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager"));
            }
            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager", "data")))
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager", "data")).Close();
                File.WriteAllText(Path.Combine(dir,"data"),"<--!-->http://example.com<!-!>Some login<!-!>Some password");
            }

            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
            if (Properties.Settings.Default.firstRun)
            {
                this.Hide();
                FirstRun f = new FirstRun();
                f.Show();
            }
            else
            {
                Form1 thForm = this;
                thForm.Location = new Point(strt_x, strt_y);
                try
                {
                    strt_x = this.Location.X;
                    strt_y = this.Location.Y;
                }
                catch
                {
                    
                }
                textBox1.Focus();
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            c_y = Form1.MousePosition.Y;
            c_x = Form1.MousePosition.X;
            moving = true;

        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

            if (moving)
            {

                moving = false;
                Point p = new Point();
                p.X = Form1.MousePosition.X - (c_x - strt_x);
                p.Y = Form1.MousePosition.Y - (c_y - strt_y);
                Form1.ActiveForm.Location = p;

                strt_x = Form.ActiveForm.Location.X;
                strt_y = Form.ActiveForm.Location.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = new string('*',textBox1.Text.Length);
            if(textBox1.Text.GetHashCode() == Properties.Settings.Default.pass)
            {
                this.Hide();
                
                Logged l = new Logged();
                l.Show();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
