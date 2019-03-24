using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatMaAnimal
{
    public partial class GeneratorKeyForm : Form
    {
        public GeneratorKeyForm()
        {
            InitializeComponent();
        }

        private void btnKeyGenerate_Click(object sender, EventArgs e)
        {
            int Num;
            bool isNum = int.TryParse(txtGenKeyLength.Text, out Num);
            if (txtGenKeyLength.Text == "" || !isNum)
            {
                MessageBox.Show("Key length is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtKeyGen.Text = RandomString(Num);

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "!@#$%^&*()_+-=`~?><,.abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void btnKeyCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtKeyGen.Text);
            MessageBox.Show("Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSaveGenKey_Click(object sender, EventArgs e)
        {
            if (txtKeyGen.Text == "")
            {
                MessageBox.Show("Please generate key before save it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            string filename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "_KeyGenerator";
            savefile.FileName = filename + ".txt";
            // set filters - this can be done in properties as well
            savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName))
                sw.WriteLine(txtKeyGen.Text);
            }
        }

        private void btnGenKey_Click(object sender, EventArgs e)
        {
            switch (cbKeySize.SelectedIndex)
            {
                case 0:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string name = saveFileDialog1.FileName;
                            RSA.Gen_key(name, 2048);
                        }
                        MessageBox.Show("Succesfull");
                        break;
                    }
                case 1:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string name = saveFileDialog1.FileName;
                            RSA.Gen_key(name, 3072);
                        }
                        MessageBox.Show("Succesfull");
                        break;
                    }
                case 2:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string name = saveFileDialog1.FileName;
                            RSA.Gen_key(name, 4096);
                        }
                        MessageBox.Show("Succesfull");
                        break;
                    }
                case 3:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string name = saveFileDialog1.FileName;
                            RSA.Gen_key(name, 8192);
                        }
                        MessageBox.Show("Succesfull");
                        break;
                    }
                case 4:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string name = saveFileDialog1.FileName;
                            RSA.Gen_key(name, 16384);
                        }
                        MessageBox.Show("Succesfull");
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Key size is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
            }
        }
    }
}
