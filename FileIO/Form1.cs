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

namespace FileIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] fileLines;

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OFDialog = new OpenFileDialog())
            {
                OFDialog.InitialDirectory = "c:\\";
                OFDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                OFDialog.FilterIndex = 2;
                OFDialog.RestoreDirectory = true;

                if(OFDialog.ShowDialog() == DialogResult.OK)
                {
                    string pathway = OFDialog.FileName;
                    fileLines = File.ReadAllLines(pathway);

                    btnEncrypt.Enabled = true;
                    btnSave.Enabled = true;
                    txtFile.Text = pathway;
                    txtFile.BackColor = Color.PaleGreen;

                    MessageBox.Show("File successfully read!", "Good job", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(saveFileDialog1.FileName, fileLines);
                }
            }
        }

        public string EncryptString(string toEncrypt)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncrypt);
            string encrypted = Convert.ToBase64String(b);
            Console.WriteLine(encrypted);
            return encrypted;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < fileLines.Length; i++)
            {
                fileLines[i] = EncryptString(fileLines[i]);
            }

            txtFile.BackColor = Color.IndianRed;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = true;
        }

        public string DecryptString(string toDecrypt)
        {
            byte[] b;
            string returner;
            try
            {
                b = Convert.FromBase64String(toDecrypt);
                returner = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch(FormatException fe)
            {
                returner = "";
            }
            Console.WriteLine(returner);
            return returner;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fileLines.Length; i++)
            {
                fileLines[i] = DecryptString(fileLines[i]);
            }

            txtFile.BackColor = Color.PaleGreen;
            btnEncrypt.Enabled = true;
            btnDecrypt.Enabled = false;
        }
    }
}
