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
    public partial class New : Form
    {
        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager");
        int strt_x = Screen.PrimaryScreen.WorkingArea.Width / 3;
        int strt_y = Screen.PrimaryScreen.WorkingArea.Height / 4;

        int c_x;
        int c_y;

        bool moving;

        public New()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Logged.resourses.Contains(textBox1.Text))
            {


                File.Decrypt(Path.Combine(dir, "data"));
                string old = File.ReadAllText(Path.Combine(dir, "data"));
                old += String.Format("<--!-->{0}<!-!>{1}<!-!>{2}", textBox1.Text, textBox2.Text, textBox3.Text);

                File.WriteAllText(Path.Combine(dir, "data"), old);

                this.Hide();
                Logged lg = new Logged();
                lg.Show();
            }
            else { MessageBox.Show("Choose another resource name or delete firstly."); }
        }

        private void New_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logged lg = new Logged();
            lg.Show();
        }

        private void New_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            c_y = Form1.MousePosition.Y;
            c_x = Form1.MousePosition.X;
            moving = true;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
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
    }
}
