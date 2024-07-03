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
using System.Data.SqlClient;




namespace KulupUyeFormu
{
    public partial class Form2 : Form
    {
        public Form2()
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


        OleDbConnection baglanti;
        DataTable Bilgiler;
        OleDbDataAdapter adapter;



        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kadir\Desktop\KulupUyeFormu\KulupUyeFormu\KulupUyeFormu\bin\Debug\uyeKayit.accdb");
            CizPastaGrafik();
        }
        private void CizPastaGrafik()
        {
           
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void listele()
        {
            Bilgiler = new DataTable();

            adapter = new OleDbDataAdapter("Select * From Bilgiler", baglanti);
            adapter.Fill(Bilgiler);
            guna2DataGridView1.DataSource = Bilgiler;
            label13.Text = (guna2DataGridView1.RowCount - 1).ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox9.Text.Length == 11 && guna2TextBox8.Text.Length == 10)
            {
                // OkulNumarası'nın veritabanında var olup olmadığını kontrol et
                if (UyeVarMi(guna2TextBox9.Text))
                {
                    MessageBox.Show("Bu Okul Numarası zaten mevcut, lütfen farklı bir numara girin.");
                }
                else
                {
                    // OkulNumarası veritabanında yoksa üye ekle
                    baglanti.Open();
                    OleDbCommand komut1 = new OleDbCommand("insert into Bilgiler (Ad,Soyad,Bölüm,Sınıf,MailAdresi,TelefonNumarası,OkulNumarası) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                    komut1.Parameters.AddWithValue("@p1", guna2TextBox3.Text);
                    komut1.Parameters.AddWithValue("@p2", guna2TextBox4.Text);
                    komut1.Parameters.AddWithValue("@p3", guna2TextBox5.Text);
                    komut1.Parameters.AddWithValue("@p4", guna2ComboBox1.Text);
                    komut1.Parameters.AddWithValue("@p5", guna2TextBox7.Text);
                    komut1.Parameters.AddWithValue("@p6", guna2TextBox8.Text);
                    komut1.Parameters.AddWithValue("@p7", guna2TextBox9.Text);

                    komut1.ExecuteNonQuery();
                    baglanti.Close();
                    listele();

                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox4.Clear();
                    guna2TextBox5.Clear();
                    guna2ComboBox1.SelectedItem = null;
                    guna2TextBox7.Clear();
                    guna2TextBox8.Clear();
                    guna2TextBox9.Clear();
                }
            }
            else if (guna2TextBox9.Text.Length != 11 && guna2TextBox8.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***).***.**** formatında yazınız!");
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else if (guna2TextBox9.Text.Length != 11)
            {
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***)-***-**** formatında yazınız!");
            }
        }

        private bool UyeVarMi(string okulNumarasi)
        {
            OleDbCommand kontrolKomut = new OleDbCommand("SELECT COUNT(*) FROM Bilgiler WHERE OkulNumarası = @OkulNumarasi", baglanti);
            kontrolKomut.Parameters.AddWithValue("@OkulNumarasi", okulNumarasi);

            baglanti.Open();
            int sayac = (int)kontrolKomut.ExecuteScalar();
            baglanti.Close();

            return sayac > 0;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete From Bilgiler where Sıra=@p8", baglanti);
            komut.Parameters.AddWithValue("@p8", guna2TextBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2ComboBox1.SelectedItem = null;
            guna2TextBox7.Clear();
            guna2TextBox8.Clear();
            guna2TextBox9.Clear();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2TextBox9.Text.Length == 11 && guna2TextBox8.Text.Length == 10)
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update Bilgiler set Ad=@p1,Soyad=@p2,Bölüm=@p3,Sınıf=@p4,MailAdresi=@p5,TelefonNumarası=@p6,OkulNumarası=@p7 where Sıra=@p8", baglanti);
                komut.Parameters.AddWithValue("@p1", guna2TextBox3.Text);
                komut.Parameters.AddWithValue("@p2", guna2TextBox4.Text);
                komut.Parameters.AddWithValue("@p3", guna2TextBox5.Text);
                komut.Parameters.AddWithValue("@p4", guna2ComboBox1.Text);
                komut.Parameters.AddWithValue("@p5", guna2TextBox7.Text);
                komut.Parameters.AddWithValue("@p6", guna2TextBox8.Text);
                komut.Parameters.AddWithValue("@p7", guna2TextBox9.Text);
                komut.Parameters.AddWithValue("@p8", guna2TextBox2.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();

                guna2TextBox1.Clear();
                guna2TextBox2.Clear();
                guna2TextBox3.Clear();
                guna2TextBox4.Clear();
                guna2TextBox5.Clear();
                guna2ComboBox1.SelectedItem = null;
                guna2TextBox7.Clear();
                guna2TextBox8.Clear();
                guna2TextBox9.Clear();

            }
            else if (guna2TextBox9.Text.Length != 11 && guna2TextBox8.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***).***.**** formatında yazınız!");
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else if (guna2TextBox9.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***)-***-**** formatında yazınız!");
            }
            else
            {
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From Bilgiler Where OkulNumarası like '%" + guna2TextBox1.Text + "%'", baglanti);

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2ComboBox1.SelectedItem = null;
            guna2TextBox7.Clear();
            guna2TextBox8.Clear();
            guna2TextBox9.Clear();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = guna2DataGridView1.SelectedCells[0].RowIndex;

            guna2TextBox2.Text = guna2DataGridView1.Rows[secilen].Cells[0].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView1.Rows[secilen].Cells[1].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView1.Rows[secilen].Cells[2].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView1.Rows[secilen].Cells[3].Value.ToString();
            guna2ComboBox1.Text = guna2DataGridView1.Rows[secilen].Cells[4].Value.ToString();
            guna2TextBox7.Text = guna2DataGridView1.Rows[secilen].Cells[5].Value.ToString();
            guna2TextBox8.Text = guna2DataGridView1.Rows[secilen].Cells[6].Value.ToString();
            guna2TextBox9.Text = guna2DataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_Click(object sender, EventArgs e)
        {
   
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2GradientPanel1_Click(object sender, EventArgs e)
        {

            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2ComboBox1.SelectedItem = null;
            guna2TextBox7.Clear();
            guna2TextBox8.Clear();
            guna2TextBox9.Clear();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox9.Text.Length == 11 && guna2TextBox8.Text.Length == 10)
            {
                // OkulNumarası'nın veritabanında var olup olmadığını kontrol et
                if (UyeVarMi(guna2TextBox9.Text))
                {
                    MessageBox.Show("Bu Okul Numarası zaten mevcut, lütfen farklı bir numara girin.");
                }
                else
                {
                    // OkulNumarası veritabanında yoksa üye ekle
                    baglanti.Open();
                    OleDbCommand komut1 = new OleDbCommand("insert into Bilgiler (Ad,Soyad,Bölüm,Sınıf,MailAdresi,TelefonNumarası,OkulNumarası) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                    komut1.Parameters.AddWithValue("@p1", guna2TextBox3.Text);
                    komut1.Parameters.AddWithValue("@p2", guna2TextBox4.Text);
                    komut1.Parameters.AddWithValue("@p3", guna2TextBox5.Text);
                    komut1.Parameters.AddWithValue("@p4", guna2ComboBox1.Text);
                    komut1.Parameters.AddWithValue("@p5", guna2TextBox7.Text);
                    komut1.Parameters.AddWithValue("@p6", guna2TextBox8.Text);
                    komut1.Parameters.AddWithValue("@p7", guna2TextBox9.Text);

                    komut1.ExecuteNonQuery();
                    baglanti.Close();
                    listele();

                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox4.Clear();
                    guna2TextBox5.Clear();
                    guna2ComboBox1.SelectedItem = null;
                    guna2TextBox7.Clear();
                    guna2TextBox8.Clear();
                    guna2TextBox9.Clear();
                }
            }
            else if (guna2TextBox9.Text.Length != 11 && guna2TextBox8.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***).***.**** formatında yazınız!");
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else if (guna2TextBox9.Text.Length != 11)
            {
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***)-***-**** formatında yazınız!");
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete From Bilgiler where Sıra=@p8", baglanti);
            komut.Parameters.AddWithValue("@p8", guna2TextBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2ComboBox1.SelectedItem = null;
            guna2TextBox7.Clear();
            guna2TextBox8.Clear();
            guna2TextBox9.Clear();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (guna2TextBox9.Text.Length == 11 && guna2TextBox8.Text.Length == 10)
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update Bilgiler set Ad=@p1,Soyad=@p2,Bölüm=@p3,Sınıf=@p4,MailAdresi=@p5,TelefonNumarası=@p6,OkulNumarası=@p7 where Sıra=@p8", baglanti);
                komut.Parameters.AddWithValue("@p1", guna2TextBox3.Text);
                komut.Parameters.AddWithValue("@p2", guna2TextBox4.Text);
                komut.Parameters.AddWithValue("@p3", guna2TextBox5.Text);
                komut.Parameters.AddWithValue("@p4", guna2ComboBox1.Text);
                komut.Parameters.AddWithValue("@p5", guna2TextBox7.Text);
                komut.Parameters.AddWithValue("@p6", guna2TextBox8.Text);
                komut.Parameters.AddWithValue("@p7", guna2TextBox9.Text);
                komut.Parameters.AddWithValue("@p8", guna2TextBox2.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();

                guna2TextBox1.Clear();
                guna2TextBox2.Clear();
                guna2TextBox3.Clear();
                guna2TextBox4.Clear();
                guna2TextBox5.Clear();
                guna2ComboBox1.SelectedItem = null;
                guna2TextBox7.Clear();
                guna2TextBox8.Clear();
                guna2TextBox9.Clear();

            }
            else if (guna2TextBox9.Text.Length != 11 && guna2TextBox8.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***).***.**** formatında yazınız!");
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
            else if (guna2TextBox9.Text.Length != 10)
            {
                MessageBox.Show("Telefon numarası 10 haneli olmak zorunda! \n (***)-***-**** formatında yazınız!");
            }
            else
            {
                MessageBox.Show("Okul numarası 11 haneli olmak zorunda!");
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From Bilgiler Where OkulNumarası like '%" + guna2TextBox1.Text + "%'", baglanti);

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
        }

        private void guna2GradientButton5_Click_1(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From Bilgiler Where OkulNumarası like '%" + guna2TextBox1.Text + "%'", baglanti);

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
        }
    }
}
