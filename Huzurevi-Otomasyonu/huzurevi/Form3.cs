using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace huzurevi
{
    public partial class Form3 : Form
    {
        DataSet ds;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public Form3()
        {
            InitializeComponent();
        }

        
        private void Form3_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from kullanici", Form1.con);
            Form1.con.Open();
            da.Fill(ds, "kullanici");
            tb_mail.Focus();
        }
        bool giris;
        private void button1_Click(object sender, EventArgs e)
        {
            giris = false;
                da = new OleDbDataAdapter("Select *from kullanici where kadi='" + tb_kadi.Text + "' and sifre='" + tb_sifre.Text +"'" , Form1.con);
                ds.Clear();
                da.Fill(ds, "kullanici");
                if (ds.Tables["kullanici"].Rows.Count > 0) { giris = true; }
                if (giris)
                {
                    Form1 f1 = new Form1();
                    this.Hide();
                    f1.ShowDialog();
                    this.Show();

                }
            else MessageBox.Show("Kullanıcı Adı veya Şifreniz Hatalı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Kayıt Ol")
            {
                button1.Visible = false; btn_kayit.Visible = label4.Visible = lb_sifre2.Visible = tb_sifre2.Visible = tb_mail.Visible = true;
                button2.Text = "Giriş Yap";
            }
            else { button2.Text = "Kayıt Ol"; button1.Visible = true; btn_kayit.Visible = label4.Visible = lb_sifre2.Visible = tb_sifre2.Visible = tb_mail.Visible = false; }


        }
        bool mail, kadi;
        private void btn_kayit_Click(object sender, EventArgs e)
        {
            kadi = mail = false;

            da = new OleDbDataAdapter("Select *from kullanici where kadi='" + tb_kadi.Text + "'", Form1.con);
            ds.Clear();
            da.Fill(ds, "kullanici");
            if (ds.Tables["kullanici"].Rows.Count == 0) { kadi = true; }
            da = new OleDbDataAdapter("Select *from kullanici where mail='" + tb_mail.Text + "'", Form1.con);
            ds.Clear();
            da.Fill(ds, "kullanici");
            if (ds.Tables["kullanici"].Rows.Count == 0) { mail = true; }

            if (tb_kadi.Text != "" && tb_sifre.Text != "" && tb_sifre2.Text != "" && tb_mail.Text != "")
            {
                if (tb_sifre.Text == tb_sifre2.Text)
                {
                    if (mail && kadi)
                    {
                        cmd = new OleDbCommand("insert into kullanici (kadi,sifre,mail) values (@kadi,@sifre,@mail)", Form1.con);
                        cmd.Parameters.AddWithValue("@kadi", tb_kadi.Text);
                        cmd.Parameters.AddWithValue("@sifre", tb_sifre.Text);
                        cmd.Parameters.AddWithValue("@mail", tb_mail.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Başarıyla kayıt oldunuz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Visible = true; btn_kayit.Visible = label4.Visible = lb_sifre2.Visible = tb_sifre2.Visible = tb_mail.Visible = false; button2.Text = "Kayıt Ol";
                    } else MessageBox.Show("Kullanıcı veya E-Posta sistemde mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else MessageBox.Show("Girdiğiniz şifreler farklı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else MessageBox.Show("Lütfen boş yer bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
