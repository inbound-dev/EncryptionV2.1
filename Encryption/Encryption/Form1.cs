using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //sets all the background stuff up
            Form form = (Form)sender;

            form.BackColor = Color.DarkGray;
            form.StartPosition = FormStartPosition.CenterScreen;

            selectedFileLabel.Visible = false;

            encryptBtn.Enabled = false;
            decryptBtn.Enabled = false;
            passwordBox.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //uses the button to open a dialog box then show the selected file
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.FileName != null && dlg.FileName != "")
            {
                selectedFileLabel.Text = "Selected File: " + dlg.FileName;
                selectedFileLabel.Visible = true;

                passwordBox.Enabled = true;
            }
        }

        private void passwordBox_Enter(object sender, EventArgs e)
        {
            encryptBtn.Enabled = true;
            decryptBtn.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {

        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
