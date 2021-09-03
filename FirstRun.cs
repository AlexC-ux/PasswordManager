using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class FirstRun : Form
    {


        int strt_x = Screen.PrimaryScreen.WorkingArea.Width / 3;
        int strt_y = Screen.PrimaryScreen.WorkingArea.Height / 4;

        int c_x;
        int c_y;

        bool moving;
        public FirstRun()
        {
            InitializeComponent();
        }

        private void FirstRun_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
            
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void FirstRun_Activated(object sender, EventArgs e)
        {
            pictureBox2.Width = pictureBox2.Height; 
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.newpass)
            {
                Properties.Settings.Default.newpass = false;
                Properties.Settings.Default.pass = textBox1.Text.GetHashCode();
                Properties.Settings.Default.Save();
                this.Hide();
                Logged l = new Logged();
                l.Show();


            }
            else
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.firstRun = false;
                Properties.Settings.Default.pass = textBox1.Text.GetHashCode();
                Properties.Settings.Default.Save();
                this.Hide();
                Logged l = new Logged();
                l.Show();
            }
            
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Firebrick;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Red;
        }
    }
}
