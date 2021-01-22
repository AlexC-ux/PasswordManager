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
    public partial class DeletePasw : Form
    {

        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager");

        public DeletePasw()
        {
            InitializeComponent();
        }

        private void DeletePasw_Load(object sender, EventArgs e)
        {
            
        }

        private void DeletePasw_Activated(object sender, EventArgs e)
        {

            button1.Hide();
            listBox1.Items.Clear();
            foreach (string res in Logged.resourses)
            {
                listBox1.Items.Add(res);
            }
            
        }

        private void DeletePasw_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logged lg = new Logged();
            lg.Show();
            this.Hide();
        }
        string item;
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Logged.resourses.Contains(listBox1.SelectedItem.ToString()))
            {
                item = listBox1.SelectedItem.ToString();
                button1.Show();
                label1.Text = "Resource: " + item;
                label2.Text = "Login: " + Logged.logins[Logged.resourses.IndexOf(item)];
                label3.Text = "Pass: " + Logged.passws[Logged.resourses.IndexOf(item)];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?","Warning",MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {

                string newData = null;
                
                
                Logged.logins.RemoveAt(Logged.resourses.IndexOf(item));
                Logged.passws.RemoveAt(Logged.resourses.IndexOf(item));
                Logged.resourses.Remove(item);

                foreach (string res in Logged.resourses)
                {
                    string newPos = "<--!-->";
                    newPos += res + "<!-!>";
                    newPos += Logged.logins[Logged.resourses.IndexOf(res)]+"<!-!>";
                    newPos += Logged.passws[Logged.resourses.IndexOf(res)];
                    newData += newPos;
                }
                File.WriteAllText(Path.Combine(dir,"data"),newData);
                Logged lg = new Logged();
                lg.Show();
                this.Hide();
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
