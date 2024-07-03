using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Guna.UI2.WinForms;


namespace KulupUyeFormu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AyarlaFormKonumu();
        }

        private void AyarlaFormKonumu()
        {
            // Formun masaüstündeki konumunu ayarla
            Screen screen = Screen.PrimaryScreen;

            int x = (screen.WorkingArea.Width - this.Width) / 2; // Yatayda ortala
            int y = (screen.WorkingArea.Height - this.Height) / 4; // Dikeyde belirli bir uzaklık

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
 
        }

        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string ad = guna2TextBox1.Text;
            string sifre = guna2TextBox2.Text;
            con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kadir\Desktop\KulupUyeFormu\KulupUyeFormu\KulupUyeFormu\bin\Debug\uyeKayit.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM kullanici where isim='" + guna2TextBox1.Text + "' AND parola='" + guna2TextBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
            }

            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
          
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
