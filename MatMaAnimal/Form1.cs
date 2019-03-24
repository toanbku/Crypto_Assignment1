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
using System.Security.Cryptography;
using System.Diagnostics;

namespace MatMaAnimal
{
    public partial class Form1 : Form
    {
        private GeneratorKeyForm genForm = new GeneratorKeyForm(); //form generator when click button gen key
        public Form1()
        {
            InitializeComponent();

            radioE_AES.Checked = true;
            radioD_AES.Checked = true;
        }

        public void OpenFile(TextBox txt, string formatsp)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Browse Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "*.*";
            openFileDialog1.Filter = formatsp;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txt.Text = openFileDialog1.FileName;
            }
        }
        public void OpenFolder(TextBox txt)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btnBrowseE_Pla_Click(object sender, EventArgs e)
        {
            OpenFile(txtE_Plaintext, "All files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|MP3 files (*.mp3)|*.mp3");
        }

        private void btnBrowseE_Cip_Click(object sender, EventArgs e)
        {
            if (txtE_Plaintext.Text != "")
            {
                FileAttributes attr = File.GetAttributes(txtE_Plaintext.Text);
                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    MessageBox.Show("No need to do this", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtE_CipherText.Enabled = false;
                    return;
                }
            }
            OpenFolder(txtE_CipherText);
        }

        private void btnBrowseE_Pass_Click(object sender, EventArgs e)
        {
            if (radioE_RSA.Checked)
                OpenFile(txtE_Password, "PUBK files (*.pubk)|*.pubk");
            else
                OpenFile(txtE_Password, "TXT files (*.txt)|*.txt");
        }



        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            bool isFolder = true;
            bool check = true;

            if (txtE_Password.Text == "" || txtE_Plaintext.Text == "")
            {
                MessageBox.Show("Please complete all required fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //|| txtE_CipherText.Text == ""
            FileAttributes attr = File.GetAttributes(txtE_Plaintext.Text);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFolder = true;
                this.Invoke((Action)(() => txtE_CipherText.Text = txtE_Plaintext.Text));
            }
            else
            {
                isFolder = false;
            }
             
             
            //detect whether its a directory or file
            if (isFolder == true)
            {
                this.Invoke((Action)(() => txtE_Status.Text = " =========ENCRYPT A FOLDER========= \r\n"));
                //MessageBox.Show("Its a directory");
                string[] filePaths = Directory.GetFiles(txtE_Plaintext.Text, "*.*", SearchOption.AllDirectories);

                foreach (var item in filePaths)
                {
                    if (radioE_AES.Checked)
                    {
                        
                        try
                        {
                            var watchfl = System.Diagnostics.Stopwatch.StartNew();
                            Aes.EncryptFile(item, item + ".enc", txtE_Password.Text);
                            watchfl.Stop();
                            var elapsedMsfl = watchfl.ElapsedMilliseconds;
                            this.Invoke((Action)(() => txtE_Status.Text += item + " => OK. Time: " + elapsedMsfl.ToString() + " ms \r\n"));

                        }
                        catch
                        {
                            check = false;
                            this.Invoke((Action)(() => txtE_Status.Text += item + "=> Some thing went wrong \r\n"));
                        }

                    }
                    else if (radioE_TripleDES.Checked)
                    {
                        
                        try
                        {
                            var watchfl = System.Diagnostics.Stopwatch.StartNew();
                            TrippleDES.EncryptFile(item, item + ".enc", txtE_Password.Text);
                            watchfl.Stop();
                            var elapsedMsfl = watchfl.ElapsedMilliseconds;
                            this.Invoke((Action)(() => txtE_Status.Text += item + " => OK. Time: " + elapsedMsfl.ToString() + " ms \r\n"));

                        }
                        catch
                        {
                            check = false;
                            this.Invoke((Action)(() => txtE_Status.Text += item + "=> Some thing went wrong \r\n"));
                        }
                    }
                }
                //Open Target
                if (checkBoxOpenTarg.Checked == true)
                    Process.Start("explorer.exe", txtD_Ciphertext.Text);
            }
            else
            {
                this.Invoke((Action)(() => txtE_Status.Text = " =========ENCRYPT A FILE========= \n"));

                //Measure time executing
                var watch = System.Diagnostics.Stopwatch.StartNew();

                string pathE = txtE_CipherText.Text + "\\" + Path.GetFileName(txtE_Plaintext.Text) + ".enc"; //path of ciphertext
                DirectoryInfo di = new DirectoryInfo(pathE);
                string folderpathE = di.Parent.FullName;

                if (radioE_RSA.Checked)
                {
                    // RSA EncryptFile
                    //string path_file = @txtE_Plaintext.Text;
                    //string ext_file = Path.GetExtension(str);
                    try
                    {
                        RSA.EncryptFile(txtE_Plaintext.Text, pathE, txtE_Password.Text);
                    }
                    catch
                    {
                        check = false;
                    }

                }

                else if (radioE_AES.Checked)
                {
                    try
                    {
                        //MessageBox.Show(pathE);
                        Aes.EncryptFile(txtE_Plaintext.Text, pathE, txtE_Password.Text);
                    }
                    catch
                    {
                        check = false;
                        txtE_Status.Text += "Some thing went wrong";
                    }

                }
                else //DES Algorithm
                {
                    try
                    {
                        TrippleDES.EncryptFile(txtE_Plaintext.Text, pathE, txtE_Password.Text);
                    }
                    catch (Exception eee)
                    {
                        check = false;
                        txtE_Status.Text += $"{eee}\n";
                    }

                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                if (check)
                    txtE_Status.Text = "Successful!" + "\r\n";
                txtE_Status.Text += "Time executing: " + elapsedMs.ToString() + " ms";
                
                //Open Target
                if (checkBoxOpenTarg.Checked == true)
                    Process.Start("explorer.exe", folderpathE);
            }


        }


        /*
         *              DECRYPTION AREA
         */



        private void btnBrowseD_Cip_Click(object sender, EventArgs e)
        {
            OpenFile(txtD_Ciphertext, "ENC files (*.enc)|*.enc");
        }

        private void btnBrowseD_Pass_Click(object sender, EventArgs e)
        {
            if (radioD_RSA.Checked == true)
                OpenFile(txtD_Password, "PRVK files (*.prvk)|*.prvk");
            else
                OpenFile(txtD_Password, "TXT files (*.txt)|*.txt");
        }

        private void btnBrowseD_Pla_Click(object sender, EventArgs e)
        {
            if (txtD_Ciphertext.Text != "")
            {
                FileAttributes attr = File.GetAttributes(txtD_Ciphertext.Text);
                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    MessageBox.Show("No need to do this", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtD_Plaintext.Enabled = false;
                    return;
                }
            }
            OpenFolder(txtD_Plaintext);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            bool isFolder = true;
            bool check = true;

            if (txtD_Ciphertext.Text == "" || txtD_Password.Text == "")
            {
                MessageBox.Show("Please complete all required fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //|| txtE_CipherText.Text == ""
            FileAttributes attr = File.GetAttributes(txtD_Ciphertext.Text);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFolder = true;
                this.Invoke((Action)(() => txtD_Plaintext.Text = txtD_Ciphertext.Text));
            }
            else
            {
                isFolder = false;
            }


            //detect whether its a directory or file
            if (isFolder == true)
            {
                this.Invoke((Action)(() => txtD_Status.Text = " =========DECRYPT A FOLDER========= \r\n"));
                //MessageBox.Show("Its a directory");
                string[] filePaths = Directory.GetFiles(txtD_Ciphertext.Text, "*.enc", SearchOption.AllDirectories);

                foreach (var item in filePaths)
                {
                    string pathD = item.Substring(0, (item.Length - 4)); //.enc = 4
                    if (radioD_AES.Checked)
                    {
                        try
                        {
                            var watchfl = System.Diagnostics.Stopwatch.StartNew();
                            Aes.DecryptFile(item, pathD, txtD_Password.Text);
                            watchfl.Stop();
                            check = true;
                            var elapsedMsfl = watchfl.ElapsedMilliseconds;
                            this.Invoke((Action)(() => txtD_Status.Text += item + " => OK. Time: " + elapsedMsfl.ToString() + " ms \r\n"));
                        }
                        catch
                        {
                            check = false;
                            this.Invoke((Action)(() => txtD_Status.Text += item + "=> Some thing went wrong \r\n"));
                        }

                    }
                    else if (radioD_TripleDES.Checked)
                    {
                        try
                        {
                            var watchfl = System.Diagnostics.Stopwatch.StartNew();
                            TrippleDES.DecryptFile(item, pathD, txtD_Password.Text);
                            watchfl.Stop();
                            check = true;
                            var elapsedMsfl = watchfl.ElapsedMilliseconds;
                            this.Invoke((Action)(() => txtD_Status.Text += item + " => OK. Time: " + elapsedMsfl.ToString() + " ms \r\n"));

                        }
                        catch
                        {
                            check = false;
                            this.Invoke((Action)(() => txtD_Status.Text += item + "=> Some thing went wrong \r\n"));
                        }
                    }
                }
                //Open Target
                if (checkBoxOpenTarg.Checked == true)
                    Process.Start("explorer.exe", txtD_Ciphertext.Text);
            }
            else
            {
                string namecipher = Path.GetFileName(txtD_Ciphertext.Text);
                string pathD = txtD_Plaintext.Text + "\\" + namecipher.Substring(0, (namecipher.Length - 4)); //.enc = 4


                //Measure time executing
                var watch = System.Diagnostics.Stopwatch.StartNew();

                if (radioD_RSA.Checked)
                {
                    // RSA DecryptFile
                    try
                    {
                        check = RSA.DecryptFile(txtD_Ciphertext.Text, pathD, txtD_Password.Text);
                    }
                    catch
                    {
                        check = false;
                        txtD_Status.Text += "Some thing went wrong\n";
                    }
                }

                else if (radioD_AES.Checked)
                {
                    try
                    {
                        check = Aes.DecryptFile(txtD_Ciphertext.Text, pathD, txtD_Password.Text);
                    }
                    catch
                    {
                        check = false;
                        txtD_Status.Text += "Some thing went wrong\n";
                    }

                }
                else //DES Algorithm
                {
                    try
                    {
                        TrippleDES.DecryptFile(txtD_Ciphertext.Text, pathD, txtD_Password.Text);
                    }
                    catch (Exception eee)
                    {

                        Console.WriteLine($"{eee}\n");
                        txtE_Status.Text += $"{eee}\n";
                    }
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                if (check)
                {
                    txtD_Status.ForeColor = Color.GreenYellow;
                    txtD_Status.Text = "Successful!" + "\r\n";
                }
                else
                {
                    txtD_Status.ForeColor = Color.Red;
                    txtD_Status.Text = "Failed!" + "\r\n";
                }

                txtD_Status.Text += "Time executing: " + elapsedMs.ToString() + " ms";


                //Open Associated Application
                if (checkBoxOpenAssociatedA.Checked == true)
                {
                    try
                    {
                        Process.Start(pathD);
                    }
                    catch
                    {
                        MessageBox.Show("File not found");
                    }
                }
                //checkBoxOpenTargF
                if (checkBoxOpenTargF.Checked == true)
                {
                    Process.Start("explorer.exe", txtD_Plaintext.Text);
                }
            }



            

        }

        //private void radioE_TripleDES_CheckedChanged(object sender, EventArgs e)
        //{

        //}


        private const string createSymKeyText = "Create A Password/Symmetric Key";
        private const string createAsmKeyText = "Create Asymmetric Keys";
        private const string pwdSymKey = "Password/Symmetric Key File Path";
        private const string pwdAsmKey = "Password/Asymmetric Key File Path";
        private void radioE_RSA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioE_RSA.Checked)
            {
                buttonCreateKey.Text = createAsmKeyText;
                label3.Text = pwdAsmKey;
            }
            else
            {
                buttonCreateKey.Text = createSymKeyText;
                label3.Text = pwdSymKey;
            }
            
        }

        //private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void label11_Click(object sender, EventArgs e)
        //{

        //}

        //private void tabPage1_Click(object sender, EventArgs e)
        //{

        //}

        //private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        //{

        //}

        //private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void tabPage2_Click(object sender, EventArgs e)
        //{

        //}

        //private void radioD_TripleDES_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        //private void txtD_Password_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void label9_Click(object sender, EventArgs e)
        //{

        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        //private void tableLayoutPanelTitle_Paint(object sender, PaintEventArgs e)
        //{

        //}

        private void checkBoxUseSrc_CheckedChanged(object sender, EventArgs e)
        {
            if (txtE_Plaintext.Text != "" && checkBoxUseSrc.Checked == true && txtE_Plaintext.Enabled == true)
            {
                string src = txtE_Plaintext.Text;
                DirectoryInfo di = new DirectoryInfo(src);
                txtE_CipherText.Text = di.Parent.FullName;

                //by hand ~~ another implement :)))
                //string[] res = src.Split('\\');
                //string temp = "";
                //for (int i = 0; i < res.Length - 1; i++)
                //    temp += res[i] + "\\";
                //txtE_CipherText.Text = temp;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtD_Ciphertext.Text != "" && checkBoxUseSrcD.Checked == true)
            {
                string src = txtD_Ciphertext.Text;
                DirectoryInfo di = new DirectoryInfo(src);
                txtD_Plaintext.Text = di.Parent.FullName;

            }
        }
        
        private void buttonCreateKey_Click(object sender, EventArgs e)
        {
            genForm.Show();
        }





        /*              HASH TASK
         *              SHA-256
         *          Show percent familiar between two hash :))
         */ 
        private void btnBrowseOriginalFile_Click(object sender, EventArgs e)
        {
            OpenFile(txtHashOriginalFile, "All files (*.*)|*.*");
        }

        private void btnBrowseDescryptedFile_Click(object sender, EventArgs e)
        {
            OpenFile(txtHashDescryptedFile, "All files (*.*)|*.*");
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            string text1 = File.ReadAllText(txtHashOriginalFile.Text, Encoding.UTF8);
            string text2 = File.ReadAllText(txtHashDescryptedFile.Text, Encoding.UTF8);

            string res1 = ComputeSha256Hash(text1);
            string res2 = ComputeSha256Hash(text2);
            int match = CalcLevenshteinDistance(res1, res2);
            txtHashResult.ForeColor = Color.BlueViolet;
            txtHashResult.Text = match.ToString() + " %";

        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //math compare string
        private static int CalcLevenshteinDistance(string a, string b)
        {
            if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
            {
                return 0;
            }
            if (String.IsNullOrEmpty(a))
            {
                return b.Length;
            }
            if (String.IsNullOrEmpty(b))
            {
                return a.Length;
            }
            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return (100 - distances[lengthA, lengthB]);
        }

        private void metroToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        //private void label3_Click(object sender, EventArgs e)
        //{

        //}

        private void radioD_RSA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioD_RSA.Checked)
            {
                label6.Text = pwdAsmKey;
            }
            else
            {
                label6.Text = pwdSymKey;
            }
        }

        private void txtD_Ciphertext_Leave(object sender, EventArgs e)
        { 
            if (txtD_Ciphertext.Text != null && txtD_Ciphertext.Text != "" && !File.Exists(txtD_Ciphertext.Text) && !Directory.Exists(txtD_Ciphertext.Text) )
            {
                MessageBox.Show("File/Folder is not exist or there is an invalid character in the path!");
                txtD_Ciphertext.Focus();
            }
        }

        private void txtD_Password_Leave(object sender, EventArgs e)
        {
            if (txtD_Password.Text != null && txtD_Password.Text != "" && !File.Exists(txtD_Password.Text))
            {
                MessageBox.Show("File is not exist or there is an invalid character in the file path!");
                txtD_Password.Focus();
            }
        }

        private void txtD_Plaintext_Leave(object sender, EventArgs e)
        {
            if (txtD_Plaintext.Text != null && txtD_Plaintext.Text != "" && !Directory.Exists(txtD_Plaintext.Text))
            {
                MessageBox.Show("Folder is not exist or there is an invalid character in the path!");
                txtD_Plaintext.Focus();
            }
        }

        private void txtE_Plaintext_Leave(object sender, EventArgs e)
        {
            if (txtE_Plaintext.Text != null && txtE_Plaintext.Text != "" && (!File.Exists(txtE_Plaintext.Text) && !Directory.Exists(txtE_Plaintext.Text) ) )
            {
                MessageBox.Show("File/Folder is not exist or there is an invalid character in the path!");
                txtE_Plaintext.Focus();
            }
        }

        private void txtE_Password_Leave(object sender, EventArgs e)
        {
            if (txtE_Password.Text != null && txtE_Password.Text != "" && !File.Exists(txtE_Password.Text))
            {
                MessageBox.Show("File is not exist or there is an invalid character in the file path!");
                txtE_Password.Focus();
            }
        }

        private void txtE_CipherText_Leave(object sender, EventArgs e)
        {
            if (txtE_CipherText.Text != null && txtE_CipherText.Text != "" && !Directory.Exists(txtE_CipherText.Text))
            {
                MessageBox.Show("Folder is not exist or there is an invalid character in the path!");
                txtE_CipherText.Focus();
            }
        }

        private void radioE_TripleDES_MouseHover(object sender, EventArgs e)
        {
            //metroToolTip1.
            metroToolTip1.Show("Encrypt data with the Triple Des algorithm.\nYour key will be transformed to a 192-bit key before used for encryption!\nNote that this algorithm is not recommended anymore.", radioE_TripleDES);
        }


        //ENCRYPT FOLDER
        private void btnBrowseE_PlaFolder_Click(object sender, EventArgs e)
        {
            if (radioE_RSA.Checked == true)
            {
                MessageBox.Show("You can't use this feature for RSA Algorithm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OpenFolder(txtE_Plaintext);
        }

        //DECRYPT FOLDER
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioD_RSA.Checked == true)
            {
                MessageBox.Show("You can't use this feature for RSA Algorithm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OpenFolder(txtD_Ciphertext);
    
        }


    }
}
