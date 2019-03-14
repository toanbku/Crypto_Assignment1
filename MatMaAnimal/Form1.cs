using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MatMaAnimal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           InitializeComponent();
            radioE.Checked = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtCipherText.Text == "" || txtPlaintext.Text == "")
            {
                MessageBox.Show("", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Measure time executing
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //Encryt
            if (radioE.Checked)
            {
                Aes.EncryptFile(txtPlaintext.Text, txtCipherText.Text +"/cipher.txt", txtPassword.Text);
            }
            //Decrypt
            else
            {
                Aes.DecryptFile(txtCipherText.Text, txtPlaintext.Text + "/plaintext.txt", txtPassword.Text);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            txtStatus.Text = "Successful!" + "\r\n";
            txtStatus.Text += "Time executing: " + elapsedMs.ToString() + " ms";
        }

        private void btnBrowsePla_Click(object sender, EventArgs e)
        {
            if (radioE.Checked)
            {
                //when Encrypt, we open file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Browse Files";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "*.*";
                openFileDialog1.Filter = "All files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|MP3 files (*.mp3)|*.mp3";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtPlaintext.Text = openFileDialog1.FileName;
                }
            }
            //when Decrypt, we open folder
            else
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtPlaintext.Text = folderDlg.SelectedPath;
                    Environment.SpecialFolder root = folderDlg.RootFolder;
                }
            }
        }

        private void btnBrowseCip_Click(object sender, EventArgs e)
        {
            if (radioD.Checked)
            {
                //when Decrypt, we open file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Browse Files";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "*.*";
                openFileDialog1.Filter = "All files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|MP3 files (*.mp3)|*.mp3";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtCipherText.Text = openFileDialog1.FileName;
                }
            }
            //when Ecrypt, we open folder
            else
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCipherText.Text = folderDlg.SelectedPath;
                    Environment.SpecialFolder root = folderDlg.RootFolder;
                }
            }
        }
    }
}
