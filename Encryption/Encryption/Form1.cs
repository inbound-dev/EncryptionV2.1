using Encryption.EncryptionFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption.EncryptionFiles;
using Encryption.DecryptionFiles;
using System.Windows.Forms.VisualStyles;
using Encryption.HashFunction;


namespace Encryption
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private String fileName;
        private String key;
        String hash;
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

            //if the user actuall selects a file, then continue
            if (dlg.FileName != null && dlg.FileName != "")
            {
                selectedFileLabel.Text = "Selected File: " + dlg.FileName;
                fileName = dlg.FileName;

                selectedFileLabel.Visible = true;
                passwordBox.Enabled = true;

                dlg.Dispose();
            }
        }

        private void passwordBox_Enter(object sender, EventArgs e)
        {
            //when the password box is entered it enables the buttons
            encryptBtn.Enabled = true;
            decryptBtn.Enabled = true;
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            statusLbl.Text = "Encrypting.......";

            //get the current text from the password box
            key = passwordBox.Text;

            //uses hashfunction to create a hash of given password
            Hash hashfunc = new Hash();
            hash = hashfunc.NewHash(key);
            Console.WriteLine("SHA256 Hash: " + hash);

            //encrypts the file with the given key
            EncryptionFiles.Encryption encryption = new EncryptionFiles.Encryption(fileName, hash);

            statusLbl.Text = "Done";
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            statusLbl.Text = "Decrypting.......";


            statusLbl.Text = "Done";
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void resetDefualt()
        {

        }

        private void selectedFileLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
