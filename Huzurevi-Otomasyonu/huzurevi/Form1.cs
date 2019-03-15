using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace huzurevi
{
    public partial class Form1 : Form
    {
       
        public static OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=veriler.mdb");
        OleDbDataAdapter da;
        OleDbCommand cmd;

        public static DataSet ds;
        DataSet ds2;

        BindingSource bs = new BindingSource();
        BindingSource bsy = new BindingSource();

        bool guncel, check, iptal, yenikayityakin, tabkapat, yenikayit, mevcut, mevcuty;
         
        int odasayisi, i, satirsayisi, cbsecili, no;
        Int64 sayi;

        DialogResult cevap;

        public string cins = "-";
        public string ycins = "-";
        string yol = "";

        void isaretli(CheckBox a)
        {
            if (a.Checked) check = true;
        }
        void listekle()
        {
            listBox1.Items.Clear(); listBox2.Items.Clear(); listBox3.Items.Clear();
            for (int i = 0; i < odasayisi; i++)
            {
                if (ds.Tables["odalar"].Rows[i]["durum"].ToString() != "Bakımda")
                {
                    da = new OleDbDataAdapter("Select *from sakinkayit where odano like'" + (i + 1) + "'", con);
                    ds2 = new DataSet();
                    da.Fill(ds2, "sakinkayit1");
                    int row = ds2.Tables["sakinkayit1"].Rows.Count;
                    if (row >= 1) { listBox3.Items.Add((i + 1)); listBox1.Items.Remove((i + 1)); }
                    else listBox1.Items.Add((i + 1));
                } else listBox2.Items.Add((i + 1));
            }         
        }

        void odaekle()
        {
            cb_odano.Items.Clear();
            for (int i = 0; i < odasayisi; i++) cb_odano.Items.Add((i + 1));
        }
        void odaekle2()
        {
            cb_odano.Items.Clear();
            for (int i = 0; i < odasayisi; i++)
            {
                if (ds.Tables["odalar"].Rows[i]["durum"].ToString() != "Bakımda")
                {
                    da = new OleDbDataAdapter("Select *from sakinkayit where odano like'" + (i + 1) + "'", con);
                    ds2 = new DataSet();
                    da.Fill(ds2, "sakinkayit1");
                    int row = ds2.Tables["sakinkayit1"].Rows.Count;
                    if (!guncel)
                    {
                        if ((Convert.ToInt32(ds.Tables["odalar"].Rows[i]["kişi"]) - row) != 0) cb_odano.Items.Add((i + 1));
                    }
                    else
                    {
                        if ((Convert.ToInt32(ds.Tables["odalar"].Rows[i]["kişi"]) - row) != 0 | (i + 1) == Convert.ToInt32(datagridkayit.CurrentRow.Cells[0].Value)) cb_odano.Items.Add((i + 1));
                    }
                }
            }
        }
        void kayitgoster()
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from sakinkayit", con);
            da.Fill(ds, "sakinkayit");
            da = new OleDbDataAdapter("Select * from yakinkayit", con);
            da.Fill(ds, "yakinkayit");
            da = new OleDbDataAdapter("Select * from hastaliklar", con);
            da.Fill(ds, "hastaliklar");
            da = new OleDbDataAdapter("Select * from odalar", con);
            da.Fill(ds, "odalar");
            da = new OleDbDataAdapter("Select * from tut", con);
            da.Fill(ds, "tut");
            bs.DataSource = ds.Tables["sakinkayit"];
            bsy.DataSource = ds.Tables["yakinkayit"];
            datagridkayit.DataSource = bs; datagridbilgi.DataSource = bs; datagridyakin.DataSource = bsy;
    
            tb_odasayisi.Text = ds.Tables["tut"].Rows[0]["adet"].ToString();
            if (ds.Tables["sakinkayit"].Rows.Count == 0) { button3.Enabled = guncelle.Enabled = button12.Enabled = button1.Enabled = false; tb_aratc.ReadOnly = tb_odara.ReadOnly = textBox2.ReadOnly = true; }
            else { button3.Enabled = guncelle.Enabled = button12.Enabled = button1.Enabled = true; tb_aratc.ReadOnly = tb_odara.ReadOnly = textBox2.ReadOnly = false; }
            if (ds.Tables["yakinkayit"].Rows.Count == 0) { button8.Enabled = button7.Enabled = false; textBox1.ReadOnly = true; }
            else { button8.Enabled = button7.Enabled = true; textBox1.ReadOnly = false; }
            if(tabControl1.SelectedIndex==0 || tabControl1.SelectedIndex == 2) toolStripStatusLabel1.Text = "Kayıt Sayısı: "+ ds.Tables["sakinkayit"].Rows.Count.ToString();
            else if(tabControl1.SelectedIndex == 1) toolStripStatusLabel1.Text = "Kayıt Sayısı: " + ds.Tables["yakinkayit"].Rows.Count.ToString();
            if (ds.Tables["sakinkayit"].Rows.Count == 0) { rd_bay.Enabled = false; rd_bayan.Enabled = false; }
            if (ds.Tables["sakinkayit"].Rows.Count == ds.Tables["yakinkayit"].Rows.Count) button1.Enabled = false;
            else button1.Enabled = true;

            btn_ekle.Enabled = tb_tc.ReadOnly = tb_dogum.ReadOnly = tb_ad.ReadOnly = tb_soyad.ReadOnly = tb_meslek.ReadOnly = mtb_telefon.ReadOnly 
            = tb_adres.ReadOnly = tb_diger.ReadOnly = true;
            tb_ytc.ReadOnly = tb_yad.ReadOnly = tb_ysoyad.ReadOnly = tb_ymeslek.ReadOnly = tb_yadres.ReadOnly = tb_ysehir.ReadOnly = mtb_ytelefon.ReadOnly 
            = mtb_evtel.ReadOnly = true;

            dt_tarih.Enabled = cb_kan.Enabled = cb_saglik.Enabled = cb_odano.Enabled = cb_saglikli.Enabled = cb_romatizma.Enabled = cb_migren.Enabled = cb_kolestrol.Enabled =
            cb_kanser.Enabled = cb_felc.Enabled = cb_kemik.Enabled = cb_diyabet.Enabled = cb_kalp.Enabled = cb_seker.Enabled = cb_tansiyon.Enabled = cb_gastrit.Enabled =
            cb_damar.Enabled = cb_obez.Enabled = cb_prostat.Enabled = cb_gorme.Enabled = radioButton1.Enabled = radioButton2.Enabled = false;
            odaekle(); listekle();
        }
        void mevcutmu()
        {
            mevcut = false; mevcuty = false;
            for (int i = 0; i < ds.Tables["sakinkayit"].Rows.Count; i++)
            {
                if (ds.Tables["sakinkayit"].Rows[i]["tcno"].ToString() == tb_tc.Text) mevcut = true;
                if (ds.Tables["sakinkayit"].Rows[i]["tcno"].ToString() == tb_ytc.Text) { mevcut = true; mevcuty = true; }
            }
            for (int i = 0; i < ds.Tables["yakinkayit"].Rows.Count; i++)
            {
                if (ds.Tables["yakinkayit"].Rows[i]["ytcno"].ToString() == tb_tc.Text) mevcut = true;
                if (ds.Tables["yakinkayit"].Rows[i]["tcno"].ToString() == tb_yakin.Text) mevcuty = true;
                if (ds.Tables["yakinkayit"].Rows[i]["ytcno"].ToString() == tb_ytc.Text) mevcuty = true;
            }
        }
        static bool IsNumeric(string a)
        {
           Int64 test;
            return Int64.TryParse(a, out test);
        }
        public Form1()
        {
            InitializeComponent();
        }
        void temizle()
         {
            tb_tc.Clear(); tb_ytc.Clear();
            tb_ad.Clear(); tb_yad.Clear();
            tb_soyad.Clear(); tb_ysoyad.Clear();
            mtb_telefon.Clear(); mtb_ytelefon.Clear();
            dt_tarih.Text = "11.09.1997"; mtb_evtel.Clear();
            tb_dogum.Clear(); 
            rd_bay.Checked = false; radioButton2.Checked = false;
            rd_bayan.Checked = false; radioButton1.Checked = false;
            cb_kan.SelectedIndex = 0;
            tb_meslek.Clear(); tb_ymeslek.Clear();
            cb_saglik.Text = ""; tb_ysehir.Clear();
            tb_adres.Clear(); tb_yadres.Clear();
            secimikaldir(cb_romatizma); secimikaldir(cb_migren); secimikaldir(cb_kolestrol); secimikaldir(cb_kanser); secimikaldir(cb_felc); secimikaldir(cb_kemik); secimikaldir(cb_diyabet);
            secimikaldir(cb_kalp); secimikaldir(cb_seker); secimikaldir(cb_tansiyon); secimikaldir(cb_gastrit); secimikaldir(cb_damar); secimikaldir(cb_obez); secimikaldir(cb_prostat);
            secimikaldir(cb_gorme); secimikaldir(cb_saglikli);
            tb_diger.Clear();
            tb_aratc.Clear();
            tb_odara.Clear();
            pictureBox1.ImageLocation = "";
            if (ds.Tables["sakinkayit"].Rows.Count == 0) tb_yakin.Text = "";
            cb_odano.SelectedIndex = 0;
        }
        void secimikaldir(CheckBox a)
        {
            if (a.Checked) a.Checked = false;
        }
        void error(TextBox a)
        {
            if(iptal) errorProvider1.SetError(a, "");
            else
            {
                if (a.Text == "") errorProvider1.SetError(a, "Zorunlu Alan");
                else errorProvider1.SetError(a, "");
            }
        }

        void error3(ComboBox a)
        {
            if (iptal) errorProvider1.SetError(a, "");
            else
            {
                if (a.Text == "") errorProvider1.SetError(a, "Zorunlu Alan");
                else errorProvider1.SetError(a, "");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            kayitgoster();
            toolTip1.SetToolTip(btn_ekle, "Yeni Kayıt"); toolTip1.SetToolTip(button3, "Sil"); toolTip1.SetToolTip(guncelle, "Güncelle"); toolTip1.SetToolTip(onayla, "Onayla"); toolTip1.SetToolTip(btn_iptal, "İptal");
            toolTip1.SetToolTip(btn_foto, "Fotoğraf Ekle"); toolTip1.SetToolTip(tb_aratc, "T.C Numarası Giriniz"); toolTip1.SetToolTip(tb_odara, "Oda Numarası Giriniz"); toolTip1.SetToolTip(button1, "Yeni Kayıt"); toolTip1.SetToolTip(button8, "Sil"); toolTip1.SetToolTip(button7, "Güncelle");
            toolTip1.SetToolTip(btn_yonayla, "Onayla"); toolTip1.SetToolTip(btn_yiptal, "İptal"); toolTip1.SetToolTip(textBox1, "T.C Numarası Giriniz"); toolTip1.SetToolTip(button12, "Sakin Bilgileri");
    
            odasayisi = Convert.ToInt32(ds.Tables["tut"].Rows[0]["adet"]);
            odaekle(); listekle();
            tb_kisisayisi.Enabled = onayla.Visible = btn_iptal.Visible = btn_yonayla.Visible = btn_yiptal.Visible = tb_odasayisi.Enabled = btn_foto.Enabled = groupBox17.Visible = groupBox19.Visible = false;
            if (ds.Tables["sakinkayit"].Rows.Count != ds.Tables["yakinkayit"].Rows.Count) { tabControl1.SelectedIndex = 1; tabkapat = true; }
            cb_odano.SelectedIndex = 0; cb_kan.SelectedIndex = 0; cb_saglik.SelectedIndex = 0;

        }
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            yenikayit = true; tabkapat = true;  guncel = false;
            onayla.Visible = btn_foto.Enabled = btn_iptal.Visible = true;
            btn_ekle.Enabled = guncelle.Enabled = button3.Enabled = tb_tc.ReadOnly = tb_dogum.ReadOnly = tb_ad.ReadOnly = tb_soyad.ReadOnly = tb_meslek.ReadOnly = 
            mtb_telefon.ReadOnly = tb_adres.ReadOnly = tb_diger.ReadOnly = false;
            dt_tarih.Enabled = rd_bay.Enabled = rd_bayan.Enabled = tb_aratc.ReadOnly = tb_odara.ReadOnly = cb_kan.Enabled = cb_saglik.Enabled = cb_odano.Enabled = cb_saglikli.Enabled =
            cb_romatizma.Enabled = cb_migren.Enabled = cb_kolestrol.Enabled = cb_kanser.Enabled = cb_felc.Enabled = cb_kemik.Enabled = cb_diyabet.Enabled = cb_kalp.Enabled = 
            cb_seker.Enabled = cb_tansiyon.Enabled = cb_gastrit.Enabled = cb_damar.Enabled = cb_obez.Enabled = cb_prostat.Enabled = cb_gorme.Enabled = true;
            odaekle2();
            temizle();
        }

        private void rd_bay_Click(object sender, EventArgs e)
        {
            cins = "Bay";
        }

        private void rd_bayan_Click(object sender, EventArgs e)
        {
          cins = "Bayan";
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // try
           // {
                DialogResult cevap = MessageBox.Show("Silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    no = bs.Position;
                    cmd = new OleDbCommand("Delete From sakinkayit Where tcno=@tcno", con);
                    cmd.Parameters.AddWithValue("@tcno", datagridkayit.CurrentRow.Cells[1].Value);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("Update odalar Set durum=@durum Where odano=@odano", con);
                    cmd.Parameters.AddWithValue("@durum", "Boş");
                    cmd.Parameters.AddWithValue("@odano", datagridkayit.CurrentRow.Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    kayitgoster();
                    MessageBox.Show("Kayıt başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bs.Position = no;
                    temizle();
                    selectionchanged();
                }
           // }
           // catch (NullReferenceException) { MessageBox.Show("Bir kayıt seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information); }
    {

    }
        }
        void selectionchanged()
        {
            if (ds.Tables["sakinkayit"].Rows.Count == 0) { rd_bay.Enabled = false; rd_bayan.Enabled = false; }
            else
            {
                cb_odano.Text = datagridkayit.CurrentRow.Cells[0].Value.ToString();
                tb_tc.Text = datagridkayit.CurrentRow.Cells[1].Value.ToString();
                tb_ad.Text = datagridkayit.CurrentRow.Cells[2].Value.ToString();
                tb_soyad.Text = datagridkayit.CurrentRow.Cells[3].Value.ToString();
                mtb_telefon.Text = datagridkayit.CurrentRow.Cells[4].Value.ToString();
                dt_tarih.Text = datagridkayit.CurrentRow.Cells[5].Value.ToString();
                tb_dogum.Text = datagridkayit.CurrentRow.Cells[6].Value.ToString();
                if (datagridkayit.CurrentRow.Cells[7].Value.ToString() == "Bay") { rd_bay.Enabled = rd_bay.Checked = true; rd_bayan.Enabled = false; }
                else { rd_bayan.Enabled = rd_bayan.Checked = true; rd_bay.Enabled = false; }
                cb_kan.SelectedItem = datagridkayit.CurrentRow.Cells[8].Value.ToString();
                tb_meslek.Text = datagridkayit.CurrentRow.Cells[9].Value.ToString();
                cb_saglik.Text = datagridkayit.CurrentRow.Cells[10].Value.ToString();
                tb_adres.Text = datagridkayit.CurrentRow.Cells[11].Value.ToString();
                pictureBox1.ImageLocation = datagridkayit.CurrentRow.Cells[12].Value.ToString();
                try
                {
                    if(tabControl1.SelectedIndex==0) i = datagridkayit.CurrentRow.Index;
                    else if (tabControl1.SelectedIndex == 2) i = datagridbilgi.CurrentRow.Index;
                    if (ds.Tables["hastaliklar"].Rows[i]["saglikli"].ToString() == "-1") cb_saglikli.Checked = true;
                    else
                    {
                        cb_saglikli.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["romatizma"].ToString() == "-1") cb_romatizma.Checked = true;
                        else cb_romatizma.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["migren"].ToString() == "-1") cb_migren.Checked = true;
                        else cb_migren.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["kolestrol"].ToString() == "-1") cb_kolestrol.Checked = true;
                        else cb_kolestrol.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["kanser"].ToString() == "-1") cb_kanser.Checked = true;
                        else cb_kanser.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["felc"].ToString() == "-1") cb_felc.Checked = true;
                        else cb_felc.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["kemike"].ToString() == "-1") cb_kemik.Checked = true;
                        else cb_kemik.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["diyabet"].ToString() == "-1") cb_diyabet.Checked = true;
                        else cb_diyabet.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["kalpc"].ToString() == "-1") cb_kalp.Checked = true;
                        else cb_kalp.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["seker"].ToString() == "-1") cb_seker.Checked = true;
                        else cb_seker.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["tansiyon"].ToString() == "-1") cb_tansiyon.Checked = true;
                        else cb_tansiyon.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["gastrit"].ToString() == "-1") cb_gastrit.Checked = true;
                        else cb_gastrit.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["damar"].ToString() == "-1") cb_damar.Checked = true;
                        else cb_damar.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["obezite"].ToString() == "-1") cb_obez.Checked = true;
                        else cb_obez.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["prostat"].ToString() == "-1") cb_prostat.Checked = true;
                        else cb_prostat.Checked = false;
                        if (ds.Tables["hastaliklar"].Rows[i]["gorme"].ToString() == "-1") cb_gorme.Checked = true;
                        else cb_gorme.Checked = false;
                        tb_diger.Text = ds.Tables["hastaliklar"].Rows[i]["diger"].ToString();
                    }
                }
                catch (NullReferenceException) {}
                catch (IndexOutOfRangeException) {}
            }
        }
        private void datagridkayit_SelectionChanged(object sender, EventArgs e)
        {
            selectionchanged();      
        }

        private void tb_aratc_TextChanged(object sender, EventArgs e)
        {
            if (tb_aratc.Text != "")
            {
                btn_ekle.Enabled = false;
                da = new OleDbDataAdapter("Select *from sakinkayit where tcno like '%" + tb_aratc.Text + "%'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit1");
                if (ds2.Tables["sakinkayit1"].Rows.Count == 0) guncelle.Enabled = button3.Enabled = false;
                else guncelle.Enabled = button3.Enabled = true;
                datagridkayit.DataSource = ds2.Tables["sakinkayit1"];
               
            }
            else { btn_ekle.Enabled = true; datagridkayit.DataSource = ds.Tables["sakinkayit"]; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_saglikli.Checked)
            {
                secimikaldir(cb_romatizma); secimikaldir(cb_migren); secimikaldir(cb_kolestrol); secimikaldir(cb_kanser); secimikaldir(cb_felc); secimikaldir(cb_kemik); secimikaldir(cb_diyabet);
                secimikaldir(cb_kalp); secimikaldir(cb_seker); secimikaldir(cb_tansiyon); secimikaldir(cb_gastrit); secimikaldir(cb_damar); secimikaldir(cb_obez); secimikaldir(cb_prostat);
                secimikaldir(cb_gorme);
                tb_diger.Clear();
                groupBox10.Enabled = false;
            }
            else
            {
                groupBox10.Enabled = true;
            }
        }
        private void bt_foto_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\foto\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                yol = pictureBox1.ImageLocation;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0 || tabControl1.SelectedIndex == 2) { kayitgoster(); selectionchanged(); toolStripStatusLabel1.Text = "Kayıt Sayısı: " + ds.Tables["sakinkayit"].Rows.Count.ToString(); }
            else if (tabControl1.SelectedIndex == 1) { yselection(); toolStripStatusLabel1.Text = "Kayıt Sayısı: " + ds.Tables["yakinkayit"].Rows.Count.ToString(); }
            else if (tabControl1.SelectedIndex == 3)
            {
                toolStripStatusLabel1.Text = "";
                lb_odas.Text = listBox1.Items.Count.ToString();
                lb_bsayi.Text = listBox2.Items.Count.ToString();
                lb_doluoda.Text = listBox3.Items.Count.ToString();
            }
        }
        public static string[] bilgi = new string[22];
        public static string[] hasta = new string[18];
        private void button12_Click(object sender, EventArgs e)
        {
            int i = datagridbilgi.CurrentRow.Index;
            for(int j=0;j<=11;j++)
            {
                bilgi[j] = datagridbilgi.CurrentRow.Cells[j].Value.ToString();
            }
                  bilgi[21] = ds.Tables["sakinkayit"].Rows[i]["foto"].ToString();

                bilgi[12] = ds.Tables["yakinkayit"].Rows[i]["ytcno"].ToString();
                bilgi[13] = ds.Tables["yakinkayit"].Rows[i]["yad"].ToString();
                bilgi[14] = ds.Tables["yakinkayit"].Rows[i]["ysoyad"].ToString();
                bilgi[15] = ds.Tables["yakinkayit"].Rows[i]["ytelefon"].ToString();
                bilgi[16] = ds.Tables["yakinkayit"].Rows[i]["yevtel"].ToString();
                bilgi[17] = ds.Tables["yakinkayit"].Rows[i]["ycinsiyet"].ToString();
                bilgi[18] = ds.Tables["yakinkayit"].Rows[i]["ymeslek"].ToString();
                bilgi[19] = ds.Tables["yakinkayit"].Rows[i]["ysehir"].ToString();
                bilgi[20] = ds.Tables["yakinkayit"].Rows[i]["yadres"].ToString();
             
                hasta[0] = ds.Tables["hastaliklar"].Rows[i]["romatizma"].ToString();
                hasta[1] = ds.Tables["hastaliklar"].Rows[i]["migren"].ToString();
                hasta[2] = ds.Tables["hastaliklar"].Rows[i]["kolestrol"].ToString();
                hasta[3] = ds.Tables["hastaliklar"].Rows[i]["kanser"].ToString();
                hasta[4] = ds.Tables["hastaliklar"].Rows[i]["felc"].ToString();
                hasta[5] = ds.Tables["hastaliklar"].Rows[i]["kemike"].ToString();
                hasta[6] = ds.Tables["hastaliklar"].Rows[i]["diyabet"].ToString();
                hasta[7] = ds.Tables["hastaliklar"].Rows[i]["kalpc"].ToString();
                hasta[8] = ds.Tables["hastaliklar"].Rows[i]["seker"].ToString();
                hasta[9] = ds.Tables["hastaliklar"].Rows[i]["tansiyon"].ToString();
                hasta[10] = ds.Tables["hastaliklar"].Rows[i]["gastrit"].ToString();
                hasta[11] = ds.Tables["hastaliklar"].Rows[i]["damar"].ToString();
                hasta[12] = ds.Tables["hastaliklar"].Rows[i]["obezite"].ToString();
                hasta[13] = ds.Tables["hastaliklar"].Rows[i]["prostat"].ToString();
                hasta[14] = ds.Tables["hastaliklar"].Rows[i]["gorme"].ToString();
                hasta[15] = ds.Tables["hastaliklar"].Rows[i]["diger"].ToString();
                hasta[16] = ds.Tables["hastaliklar"].Rows[i]["saglikli"].ToString();
                Form2 f2 = new Form2();
                this.Hide();
                f2.ShowDialog();
                this.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                da = new OleDbDataAdapter("Select *from sakinkayit where tcno like '" + textBox2.Text + "%'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit1");
                if (ds2.Tables["sakinkayit1"].Rows.Count == 0) button12.Enabled = false;
                else button12.Enabled = true;
                datagridbilgi.DataSource = ds2.Tables["sakinkayit1"];
            }
            else { button12.Enabled = true; datagridbilgi.DataSource = ds.Tables["sakinkayit"]; }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                ycins = "Bay";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                ycins = "Bayan";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                da = new OleDbDataAdapter("Select *from yakinkayit where tcno like '%" + textBox1.Text + "%'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "yakinkayit1");
                if (ds2.Tables["yakinkayit1"].Rows.Count == 0) button7.Enabled = false;
                else button7.Enabled = true;
                datagridyakin.DataSource = ds2.Tables["yakinkayit1"];
            }
            else kayitgoster();
        }

        void yselection()
        {
           try
            {
                tb_yakin.Text = datagridyakin.CurrentRow.Cells[0].Value.ToString();
                tb_ytc.Text = datagridyakin.CurrentRow.Cells[1].Value.ToString();
                tb_yad.Text = datagridyakin.CurrentRow.Cells[2].Value.ToString();
                tb_ysoyad.Text = datagridyakin.CurrentRow.Cells[3].Value.ToString();
                mtb_ytelefon.Text = datagridyakin.CurrentRow.Cells[4].Value.ToString();
                mtb_evtel.Text = datagridyakin.CurrentRow.Cells[5].Value.ToString();
                if (datagridyakin.CurrentRow.Cells[6].Value.ToString() == "Bay") { radioButton2.Enabled = radioButton2.Checked = true; radioButton1.Enabled = false; }
                else { radioButton1.Enabled = radioButton1.Checked = true; radioButton2.Enabled = false; }
                tb_ymeslek.Text = datagridyakin.CurrentRow.Cells[7].Value.ToString();
                tb_ysehir.Text = datagridyakin.CurrentRow.Cells[8].Value.ToString();
                tb_yadres.Text = datagridyakin.CurrentRow.Cells[9].Value.ToString();
            }
            catch(NullReferenceException)
            {

            }
        }
        private void datagridyakin_SelectionChanged(object sender, EventArgs e)
        {
            yselection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void onayla_Click(object sender, EventArgs e)
        {
            if(yenikayit)
            {
                error(tb_ad); error(tb_adres); error(tb_soyad); error(tb_meslek); error(tb_dogum);
                error3(cb_kan); error3(cb_saglik); error3(cb_odano);
                
                if (!rd_bay.Checked && !rd_bayan.Checked) errorProvider1.SetError(rd_bayan, "Zorunlu Alan");
                else errorProvider1.SetError(rd_bayan, "");
                check = false;
                isaretli(cb_romatizma); isaretli(cb_migren); isaretli(cb_kolestrol); isaretli(cb_kanser);
                isaretli(cb_felc); isaretli(cb_kemik); isaretli(cb_diyabet); isaretli(cb_kalp); isaretli(cb_seker);
                isaretli(cb_tansiyon); isaretli(cb_gastrit); isaretli(cb_damar); isaretli(cb_obez); isaretli(cb_prostat);
                isaretli(cb_gorme);
                if (tb_tc.TextLength != 11 || !IsNumeric(tb_tc.Text))
                {
                    errorProvider1.SetError(tb_tc, "Zorunlu Alan");
                    MessageBox.Show("Yanlış T.C Kimlik numarası girdiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorProvider1.SetError(tb_tc, "");
                    mevcutmu();
                    if (mevcut) MessageBox.Show("Girdiğiniz T.C. Kimlik numarası sistemde mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (tb_soyad.Text == "" || tb_ad.Text == "" || tb_meslek.Text == "" || tb_tc.Text == "" || mtb_telefon.Text == "" || tb_adres.Text == "" || cb_saglik.SelectedItem == null || cb_kan.SelectedItem == null || !rd_bay.Checked && !rd_bayan.Checked) MessageBox.Show("Lütfen boş alan bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            if (cb_saglikli.Checked || check || tb_diger.Text != "")
                            {
                                errorProvider1.SetError(cb_saglikli, "");
                                cmd = new OleDbCommand("Update tut Set tctut=@tctut Where adet=@adet", con);
                                cmd.Parameters.AddWithValue("@tctut", tb_tc.Text);
                                cmd.Parameters.AddWithValue("@adet",odasayisi);
                                cmd.ExecuteNonQuery();
                                cmd = new OleDbCommand("Update odalar Set durum=@durum Where odano=@odanoo", con);
                                cmd.Parameters.AddWithValue("@durum", "Dolu");
                                cmd.Parameters.AddWithValue("@odanoo", cb_odano.SelectedItem.ToString());
                                cmd.ExecuteNonQuery();
                                cmd = new OleDbCommand("Insert into sakinkayit (odano,tcno,ad,soyad,telefon,dogumt,dogumy,cinsiyet,kang,meslek,saglikg,adres,foto) values (@odano,@tcno,@Ad,@Soyad,@telefon,@DoğumTarihi,@DoğumYeri,@cinsiyet,@KanGrubu,@meslek,@SağlıkGüvencesi,@adres,@foto)", con);
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@odano", cb_odano.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tcno", tb_tc.Text);
                                cmd.Parameters.AddWithValue("@Ad", tb_ad.Text);
                                cmd.Parameters.AddWithValue("@Soyad", tb_soyad.Text);
                                cmd.Parameters.AddWithValue("@telefon", mtb_telefon.Text);
                                cmd.Parameters.AddWithValue("@DoğumTarihi", dt_tarih.Text);
                                cmd.Parameters.AddWithValue("@DoğumYeri", tb_dogum.Text);
                                cmd.Parameters.AddWithValue("@cinsiyet", cins);
                                cmd.Parameters.AddWithValue("@KanGrubu", cb_kan.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@meslek", tb_meslek.Text);
                                cmd.Parameters.AddWithValue("@SağlıkGüvencesi", cb_saglik.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@adres", tb_adres.Text);
                                cmd.Parameters.AddWithValue("@foto", pictureBox1.ImageLocation);            
                                cmd.ExecuteNonQuery();
                                cmd = new OleDbCommand("Insert into hastaliklar (tcno,romatizma,migren,kolestrol,kanser,felc,kemike,diyabet,kalpc,seker,tansiyon,gastrit,damar,obezite,prostat,gorme,diger,saglikli) values (@tcno, @romatizma, @migren, @kolestrol, @kanser, @felc, @kemike, @diyabet, @kalpc, @seker, @tansiyon, @gastrit, @damar, @obezite, @prostat, @gorme, @diger, @saglikli)", con);
                                cmd.Parameters.AddWithValue("@tcno", tb_tc.Text);
                                cmd.Parameters.AddWithValue("@romatizma", cb_romatizma.Checked);
                                cmd.Parameters.AddWithValue("@migren", cb_migren.Checked);
                                cmd.Parameters.AddWithValue("@kolestrol", cb_kolestrol.Checked);
                                cmd.Parameters.AddWithValue("@kanser", cb_kanser.Checked);
                                cmd.Parameters.AddWithValue("@felc", cb_felc.Checked);
                                cmd.Parameters.AddWithValue("@kemike", cb_kemik.Checked);
                                cmd.Parameters.AddWithValue("@diyabet", cb_diyabet.Checked);
                                cmd.Parameters.AddWithValue("@kalpc", cb_kalp.Checked);
                                cmd.Parameters.AddWithValue("@seker", cb_seker.Checked);
                                cmd.Parameters.AddWithValue("@tansiyon", cb_tansiyon.Checked);
                                cmd.Parameters.AddWithValue("@gastrit", cb_gastrit.Checked);
                                cmd.Parameters.AddWithValue("@damar", cb_damar.Checked);
                                cmd.Parameters.AddWithValue("@obezite", cb_obez.Checked);
                                cmd.Parameters.AddWithValue("@prostat", cb_prostat.Checked);
                                cmd.Parameters.AddWithValue("@gorme", cb_gorme.Checked);
                                cmd.Parameters.AddWithValue("@diger", tb_diger.Text);
                                cmd.Parameters.AddWithValue("@saglikli", cb_saglikli.Checked);
                                cmd.ExecuteNonQuery();
                                kayitgoster();                           
                                yenikayit = false;
                                onayla.Visible = btn_iptal.Visible = btn_foto.Enabled = tb_aratc.ReadOnly = tb_odara.ReadOnly = false;
                                tabkapat = false;  
                                tabControl1.SelectedIndex = 1;
                                tabkapat = true;
                                listekle();
                                button8.Enabled = button7.Enabled = false;
                            }
                            else errorProvider1.SetError(cb_saglikli, "Zorunlu Alan");
                        }
                    }
                }

            }
            else
            {
                error(tb_ad); error(tb_adres); error(tb_soyad); error(tb_meslek); error(tb_dogum);
                error3(cb_kan); error3(cb_saglik);
                if (!rd_bay.Checked && !rd_bayan.Checked) errorProvider1.SetError(rd_bayan, "Zorunlu Alan");
                else errorProvider1.SetError(rd_bayan, "");
                check = false;
                isaretli(cb_romatizma); isaretli(cb_migren); isaretli(cb_kolestrol); isaretli(cb_kanser);
                isaretli(cb_felc); isaretli(cb_kemik); isaretli(cb_diyabet); isaretli(cb_kalp); isaretli(cb_seker);
                isaretli(cb_tansiyon); isaretli(cb_gastrit); isaretli(cb_damar); isaretli(cb_obez); isaretli(cb_prostat);
                isaretli(cb_gorme);
                if (tb_soyad.Text == "" || tb_ad.Text == "" || tb_meslek.Text == "" || tb_tc.Text == "" || mtb_telefon.Text == "" || tb_adres.Text == "" || cb_saglik.SelectedItem == null || cb_kan.SelectedItem == null || !rd_bay.Checked && !rd_bayan.Checked) MessageBox.Show("Lütfen boş alan bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (cb_saglikli.Checked || check || tb_diger.Text != "")
                    {
                        cmd = new OleDbCommand("Update odalar Set durum=@durum Where odano=@odano", con);
                        cmd.Parameters.AddWithValue("@durum", "Boş");
                        cmd.Parameters.AddWithValue("@odano", datagridkayit.CurrentRow.Cells[0].Value);
                        cmd.ExecuteNonQuery();
                        cmd = new OleDbCommand("Update odalar Set durum=@durum Where odano=@odano", con);
                        cmd.Parameters.AddWithValue("@durum", "Dolu");
                        cmd.Parameters.AddWithValue("@odano", cb_odano.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        cmd = new OleDbCommand("Update sakinkayit Set odano=@odano,tcno=@tcno,ad=@ad,soyad=@soyad,telefon=@telefon,dogumt=@dogumt,dogumy=@dogumy,cinsiyet=@cinsiyet,kang=@kang, meslek=@meslek,saglikg=@saglikg, adres=@adres,foto=@foto Where tcno=@tcno", con);
                        cmd.Parameters.AddWithValue("@odano", cb_odano.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@tcno", datagridkayit.CurrentRow.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@ad", tb_ad.Text);
                        cmd.Parameters.AddWithValue("@soyad", tb_soyad.Text);
                        cmd.Parameters.AddWithValue("@telefon", mtb_telefon.Text);
                        cmd.Parameters.AddWithValue("@dogumt", dt_tarih.Text);
                        cmd.Parameters.AddWithValue("@dogumy", tb_dogum.Text);
                        cmd.Parameters.AddWithValue("@cinsiyet", cins);
                        cmd.Parameters.AddWithValue("@kang", cb_kan.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@meslek", tb_meslek.Text);
                        cmd.Parameters.AddWithValue("@saglikg", cb_saglik.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@adres", tb_adres.Text);
                        cmd.Parameters.AddWithValue("@foto", pictureBox1.ImageLocation);
                        cmd.ExecuteNonQuery();
                        cmd = new OleDbCommand("Update hastaliklar Set romatizma=@romatizma,migren=@migren,kolestrol=@kolestrol,kanser=@kanser,felc=@felc,kemike=@kemike,diyabet=@diyabet,kalpc=@kalpc,seker=@seker,tansiyon=@tansiyon,gastrit=@gastrit,damar=@damar,obezite=@obezite,prostat=@prostat,gorme=@gorme,diger=@diger,saglikli=@saglikli Where tcno=@tcno", con);
                        cmd.Parameters.AddWithValue("@romatizma", cb_romatizma.Checked);
                        cmd.Parameters.AddWithValue("@migren", cb_migren.Checked);
                        cmd.Parameters.AddWithValue("@kolestrol", cb_kolestrol.Checked);
                        cmd.Parameters.AddWithValue("@kanser", cb_kanser.Checked);
                        cmd.Parameters.AddWithValue("@felc", cb_felc.Checked);
                        cmd.Parameters.AddWithValue("@kemike", cb_kemik.Checked);
                        cmd.Parameters.AddWithValue("@diyabet", cb_diyabet.Checked);
                        cmd.Parameters.AddWithValue("@kalpc", cb_kalp.Checked);
                        cmd.Parameters.AddWithValue("@seker", cb_seker.Checked);
                        cmd.Parameters.AddWithValue("@tansiyon", cb_tansiyon.Checked);
                        cmd.Parameters.AddWithValue("@gastrit", cb_gastrit.Checked);
                        cmd.Parameters.AddWithValue("@damar", cb_damar.Checked);
                        cmd.Parameters.AddWithValue("@obezite", cb_obez.Checked);
                        cmd.Parameters.AddWithValue("@prostat", cb_prostat.Checked);
                        cmd.Parameters.AddWithValue("@gorme", cb_gorme.Checked);
                        cmd.Parameters.AddWithValue("@diger", tb_diger.Text);
                        cmd.Parameters.AddWithValue("@saglikli", cb_saglikli.Checked);
                        cmd.Parameters.AddWithValue("@tcno", tb_tc.Text);
                        cmd.ExecuteNonQuery();        
                        guncel = tabkapat = false;
                        datagridkayit.Enabled = true;
                        kayitgoster();
                        MessageBox.Show("Kayıt başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bs.Position = no;
                        cb_odano.Text = datagridkayit.CurrentRow.Cells[0].Value.ToString();
                        listekle();
                        onayla.Visible = btn_iptal.Visible = btn_foto.Enabled = tb_aratc.ReadOnly = tb_odara.ReadOnly = false;
                    }
                    else errorProvider1.SetError(cb_saglikli, "Zorunlu Alan");
                }         
            }
        }
        private void guncelle_Click(object sender, EventArgs e)
        {
            datagridkayit.Enabled = false;
            yenikayit = false;
            tabkapat = true;
            guncel = true;
            onayla.Visible = btn_iptal.Visible = btn_foto.Enabled = true;
            btn_ekle.Enabled = guncelle.Enabled = button3.Enabled = false;
            odaekle2();
            tb_tc.ReadOnly = tb_dogum.ReadOnly = tb_ad.ReadOnly = tb_soyad.ReadOnly = tb_meslek.ReadOnly = mtb_telefon.ReadOnly = tb_adres.ReadOnly = tb_diger.ReadOnly = false;
            dt_tarih.Enabled = rd_bay.Enabled = rd_bayan.Enabled = cb_kan.Enabled = cb_saglik.Enabled = cb_odano.Enabled = tb_aratc.ReadOnly = tb_odara.ReadOnly = true;
            cb_saglikli.Enabled = cb_romatizma.Enabled = cb_migren.Enabled = cb_kolestrol.Enabled = cb_kanser.Enabled = cb_felc.Enabled = cb_kemik.Enabled =
            cb_diyabet.Enabled = cb_kalp.Enabled = cb_seker.Enabled = cb_tansiyon.Enabled = cb_gastrit.Enabled = cb_damar.Enabled = cb_obez.Enabled = cb_prostat.Enabled = cb_gorme.Enabled = true;
            no = bs.Position;
            cb_odano.Text = datagridkayit.CurrentRow.Cells[0].Value.ToString();
        }
        private void btn_iptal_Click(object sender, EventArgs e)
        {
            iptal = datagridkayit.Enabled = true;
            error(tb_tc); error(tb_ad); error(tb_adres); error(tb_soyad); error(tb_meslek); error(tb_dogum);
            error3(cb_kan); error3(cb_saglik); error3(cb_odano);
            errorProvider1.SetError(rd_bayan, "");
            iptal = false;
            if (ds.Tables["sakinkayit"].Rows.Count != 0) guncelle.Enabled = button3.Enabled = true;
            btn_ekle.Enabled = true;
            onayla.Visible = btn_iptal.Visible = btn_foto.Enabled = tb_aratc.ReadOnly = tb_odara.ReadOnly = false;
            tb_tc.ReadOnly = tb_dogum.ReadOnly = tb_ad.ReadOnly = tb_soyad.ReadOnly = tb_meslek.ReadOnly = mtb_telefon.ReadOnly = tb_adres.ReadOnly = tb_diger.ReadOnly = true;
            dt_tarih.Enabled = cb_kan.Enabled = cb_saglik.Enabled = cb_odano.Enabled = false;
            cb_saglikli.Enabled = cb_romatizma.Enabled = cb_migren.Enabled = cb_kolestrol.Enabled = cb_kanser.Enabled = cb_felc.Enabled = cb_kemik.Enabled =
            cb_diyabet.Enabled = cb_kalp.Enabled = cb_seker.Enabled = cb_tansiyon.Enabled = cb_gastrit.Enabled = cb_damar.Enabled = cb_obez.Enabled = cb_prostat.Enabled = cb_gorme.Enabled = false;
            selectionchanged();
            guncel = tabkapat = false;
            odaekle();



            if (ds.Tables["sakinkayit"].Rows.Count != 0) cb_odano.Text = datagridkayit.CurrentRow.Cells[0].Value.ToString();
            else cb_odano.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e) //Yakın ekleme
        {
            datagridyakin.Enabled = false;
            yenikayityakin = true;
            btn_yonayla.Visible = btn_yiptal.Visible = textBox1.ReadOnly = true;
            button1.Enabled = button8.Enabled = button7.Enabled = false;
            tb_ytc.ReadOnly = tb_yad.ReadOnly = tb_ysoyad.ReadOnly = tb_ymeslek.ReadOnly = tb_yadres.ReadOnly = tb_ysehir.ReadOnly = mtb_ytelefon.ReadOnly
            = mtb_evtel.ReadOnly = false;
            radioButton1.Enabled = radioButton2.Enabled = true;
            tb_yakin.Text = ds.Tables["tut"].Rows[0]["tctut"].ToString();
            temizle();
        }
        private void button8_Click(object sender, EventArgs e) //Yakın silme
        {
             cevap = MessageBox.Show("Silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                cmd = new OleDbCommand("Delete From yakinkayit Where ytcno=@ytcno", con);
                cmd.Parameters.AddWithValue("@ytcno", datagridyakin.CurrentRow.Cells[1].Value);
                cmd.ExecuteNonQuery();
                kayitgoster();
                MessageBox.Show("Kayıt başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                tabkapat = true;
            }
        }
        private void button7_Click(object sender, EventArgs e) //Yakın güncelleme
        {
            yenikayityakin = datagridyakin.Enabled = false;
            btn_yonayla.Visible = btn_yiptal.Visible = textBox1.ReadOnly = true;
            button1.Enabled = button8.Enabled = button7.Enabled = false;
            tb_ytc.ReadOnly = tb_yad.ReadOnly = tb_ysoyad.ReadOnly = tb_ymeslek.ReadOnly = tb_yadres.ReadOnly = tb_ysehir.ReadOnly = mtb_ytelefon.ReadOnly
            = mtb_evtel.ReadOnly = false;
            radioButton1.Enabled = radioButton2.Enabled = true;
            no = bsy.Position;
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabkapat)
            {
                if (e.TabPage == tabPage1 || e.TabPage == tabPage2 || e.TabPage == tabPage3 || e.TabPage == tabPage4) e.Cancel = true;
            }
        }

        private void btn_yonayla_Click(object sender, EventArgs e)
        {
            if(yenikayityakin)
            {
                error(tb_yad); error(tb_ytc); error(tb_yadres); error(tb_ysoyad); error(tb_ymeslek); error(tb_ysehir);
                if (!radioButton1.Checked && !radioButton2.Checked) errorProvider1.SetError(radioButton1, "Zorunlu Alan");
                else errorProvider1.SetError(radioButton1, "");
                if (tb_ytc.TextLength != 11 || !IsNumeric(tb_ytc.Text))
                {
                    errorProvider1.SetError(tb_ytc, "Zorunlu Alan");
                    MessageBox.Show("Yanlış T.C Kimlik numarası girdiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorProvider1.SetError(tb_ytc, "");
                    mevcutmu();
                    if (mevcuty) MessageBox.Show("Girdiğiniz T.C. Kimlik numarası sistemde mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (tb_ysoyad.Text == "" || tb_yad.Text == "" || tb_ymeslek.Text == "" || tb_ytc.Text == "" || mtb_ytelefon.Text == "" || tb_yadres.Text == "" || !radioButton1.Checked && !radioButton2.Checked) MessageBox.Show("Lütfen boş alan bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            string sorgu = "Insert into yakinkayit (tcno,ytcno,yad,ysoyad,ytelefon,yevtel,ycinsiyet,ymeslek,ysehir,yadres) values (@tcno,@ytcno,@yad,@ysoyad,@ytelefon,@yevtel,@ycinsiyet,@ymeslek,@ysehir,@yadres)";
                            cmd = new OleDbCommand(sorgu, con);
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@tcno", tb_yakin.Text);
                            cmd.Parameters.AddWithValue("@ytcno", tb_ytc.Text);
                            cmd.Parameters.AddWithValue("@yad", tb_yad.Text);
                            cmd.Parameters.AddWithValue("@ysoyad", tb_ysoyad.Text);
                            cmd.Parameters.AddWithValue("@ytelefon", mtb_ytelefon.Text);
                            cmd.Parameters.AddWithValue("@yevtel", mtb_evtel.Text);
                            cmd.Parameters.AddWithValue("@ycinsiyet", ycins);
                            cmd.Parameters.AddWithValue("@ymeslek", tb_ymeslek.Text);
                            cmd.Parameters.AddWithValue("@ysehir", tb_ysehir.Text);
                            cmd.Parameters.AddWithValue("@yadres", tb_yadres.Text);
                            cmd.ExecuteNonQuery();
                            kayitgoster();
                            btn_yonayla.Visible = btn_yiptal.Visible = textBox1.ReadOnly = false;
                            datagridyakin.Enabled = true;
                            tabkapat = false;
                        }
                    }
                }
            }
            else
            {
                error(tb_yad); error(tb_ytc); error(tb_yadres); error(tb_ysoyad); error(tb_ymeslek); error(tb_ysehir);
                if (!radioButton1.Checked && !radioButton2.Checked) errorProvider1.SetError(radioButton1, "Zorunlu Alan");
                else errorProvider1.SetError(radioButton1, "");
                if (tb_ytc.Text == "" || tb_ysoyad.Text == "" || mtb_evtel.Text == "" || tb_yad.Text == "" || tb_ymeslek.Text == "" || mtb_ytelefon.Text == "" || tb_yadres.Text == "" || !radioButton2.Checked && !radioButton1.Checked) MessageBox.Show("Lütfen boş alan bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    cmd = new OleDbCommand("Update yakinkayit Set ytcno=@ytcno, yad=@yad, ysoyad=@ysoyad, ytelefon=@ytelefon,yevtel=@yevtel,ycinsiyet=@ycinsiyet,ymeslek=@ymeslek,ysehir=@ysehir, yadres=@yadres Where ytcno=@ytcnoo", con);
                    cmd.Parameters.AddWithValue("@ytcnoo", tb_ytc.Text);
                    cmd.Parameters.AddWithValue("@yad", tb_yad.Text);
                    cmd.Parameters.AddWithValue("@ysoyad", tb_ysoyad.Text);
                    cmd.Parameters.AddWithValue("@ytelefon", mtb_ytelefon.Text);
                    cmd.Parameters.AddWithValue("@yevtel", mtb_evtel.Text);
                    cmd.Parameters.AddWithValue("@ycinsiyet", ycins);
                    cmd.Parameters.AddWithValue("@ymeslek", tb_ymeslek.Text);
                    cmd.Parameters.AddWithValue("@ysehir", tb_ysehir.Text);
                    cmd.Parameters.AddWithValue("@yadres", tb_yadres.Text);
                    cmd.Parameters.AddWithValue("@ytcnoo", datagridyakin.CurrentRow.Cells[1].Value);
                    cmd.ExecuteNonQuery();
                    kayitgoster();
                    datagridyakin.Enabled = true;
                    btn_yonayla.Visible = btn_yiptal.Visible = textBox1.ReadOnly = false;
                    MessageBox.Show("Kayıt başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

       
        private void btn_yiptal_Click(object sender, EventArgs e)
        {
            iptal = true;
            error(tb_yad); error(tb_ytc); error(tb_yadres); error(tb_ysoyad); error(tb_ymeslek); error(tb_ysehir);
            errorProvider1.SetError(radioButton1, "");
            iptal = false;
            if (ds.Tables["yakinkayit"].Rows.Count != 0) button7.Enabled = button8.Enabled = true;
            button1.Enabled = true;
            btn_yonayla.Visible = btn_yiptal.Visible = textBox1.ReadOnly = false;
            tb_ytc.ReadOnly = tb_yad.ReadOnly = tb_ysoyad.ReadOnly = tb_ymeslek.ReadOnly = tb_yadres.ReadOnly = tb_ysehir.ReadOnly = mtb_ytelefon.ReadOnly
            = mtb_evtel.ReadOnly = true;
            radioButton1.Enabled = radioButton2.Enabled = false;
            datagridyakin.Enabled = true;
            yselection();
        }


        //ODALAR KISMI
        private void tb_kisisayisi_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tb_kisisayisi.Text))
                if (Convert.ToInt64(tb_kisisayisi.Text) > 10 || tb_kisisayisi.TextLength > 2 || Convert.ToInt64(tb_kisisayisi.Text) < 1) tb_kisisayisi.Text = "1";
        }

        private void tb_odasayisi_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tb_odasayisi.Text))
                if (Convert.ToInt64(tb_odasayisi.Text) > 300 || tb_odasayisi.TextLength > 3) tb_odasayisi.Text = odasayisi.ToString();
        }
        private void btn_bakim_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string sebep = Interaction.InputBox("Odayı bakıma alma sebeplerinizi yazınız..");
                if (sebep.Length != 0)
                {
                    cmd = new OleDbCommand("update odalar Set durum=@durum, bakim=@bakim where odano=@odano", con);
                    cmd.Parameters.AddWithValue("@durum", "Bakımda");
                    cmd.Parameters.AddWithValue("@bakim", sebep);
                    cmd.Parameters.AddWithValue("@odano", listBox1.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    listBox2.Items.Add(listBox1.SelectedItem);
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    lb_odas.Text = listBox1.Items.Count.ToString();
                    lb_bsayi.Text = listBox2.Items.Count.ToString();
                }
            }
        }

        private void btn_bakimcikar_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                cmd = new OleDbCommand("update odalar Set durum=@durum,bakim=@bakim where odano=@odano", con);
                cmd.Parameters.AddWithValue("@durum", "Boş");
                cmd.Parameters.AddWithValue("@bakim", "");
                cmd.Parameters.AddWithValue("@odano", listBox2.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.Remove(listBox2.SelectedItem);
                lb_odas.Text = listBox1.Items.Count.ToString();
                lb_bsayi.Text = listBox2.Items.Count.ToString();
                lb_doluoda.Text = listBox3.Items.Count.ToString();
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null && !checkBox1.Checked)
            {
                string sebep = Interaction.InputBox("Odayı bakıma alma sebeplerinizi yazınız..");
                if (sebep.Length != 0)
                {
                    cmd = new OleDbCommand("update odalar Set durum=@durum, bakim=@bakim where odano=@odano", con);
                    cmd.Parameters.AddWithValue("@durum", "Bakımda");
                    cmd.Parameters.AddWithValue("@bakim", sebep);
                    cmd.Parameters.AddWithValue("@odano", listBox1.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    listBox2.Items.Add(listBox1.SelectedItem);
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    lb_odas.Text = listBox1.Items.Count.ToString();
                    lb_bsayi.Text = listBox2.Items.Count.ToString();
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem != null && !checkBox1.Checked)
            {
                cmd = new OleDbCommand("update odalar Set durum=@durum,bakim=@bakim where odano=@odano", con);
                cmd.Parameters.AddWithValue("@durum", "Boş");
                cmd.Parameters.AddWithValue("@bakim", "");
                cmd.Parameters.AddWithValue("@odano", listBox2.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.Remove(listBox2.SelectedItem);
                lb_odas.Text = listBox1.Items.Count.ToString();
                lb_bsayi.Text = listBox2.Items.Count.ToString();
                lb_doluoda.Text = listBox3.Items.Count.ToString();
            }
        }
        void odalarr()
        {
            da = new OleDbDataAdapter("Select *from odalar where odano like'" + odaNo + "'", con);
            ds2 = new DataSet();
            da.Fill(ds2, "odalar1");
            tb_kisisayisi.Text = ds2.Tables["odalar1"].Rows[0]["kişi"].ToString();
            kisisayisi = ds2.Tables["odalar1"].Rows[0]["kişi"].ToString();  

        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1 && !checkBox1.Checked)
            {
                odaNo = listBox3.SelectedItem.ToString();
                odalarr();
                toolTip1.SetToolTip(lb_odadurum, "Dolu");
                da = new OleDbDataAdapter("Select *from sakinkayit where odano like'" + odaNo + "'", con);
                da.Fill(ds2, "sakinkayit1");
                lb_odadurum.ForeColor = Color.Red;
                groupBox17.Visible = false; groupBox19.Visible = true;
                lb_adisoyadi.Text = "";
                for (int i = 0; i < ds2.Tables["sakinkayit1"].Rows.Count;i++ )
                {
                    lb_adisoyadi.Text += "Ad-Soyad: " + ds2.Tables["sakinkayit1"].Rows[i]["Ad"].ToString() + " " + ds2.Tables["sakinkayit1"].Rows[i]["Soyad"].ToString() + "\n\n";
                }
                listBox1.SelectedIndex = -1; listBox2.SelectedIndex = -1;
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1 && !checkBox1.Checked)
            {
                odaNo = listBox2.SelectedItem.ToString();
                odalarr();
                toolTip1.SetToolTip(lb_odadurum, "Bakımda");
                lb_odadurum.ForeColor = Color.Gray;
                groupBox17.Visible = true; groupBox19.Visible = false;
                listBox1.SelectedIndex = -1; listBox3.SelectedIndex = -1;
                textBox3.Text = ds2.Tables["odalar1"].Rows[0]["bakim"].ToString();
            }
        }
        string kisisayisi,odaNo;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && !checkBox1.Checked)
            {
                odaNo = listBox1.SelectedItem.ToString();
                odalarr();
                toolTip1.SetToolTip(lb_odadurum, "Müsait");
                lb_odadurum.ForeColor = Color.LimeGreen;
                groupBox17.Visible = groupBox19.Visible = false;
                listBox3.SelectedIndex = -1; listBox2.SelectedIndex = -1;
            }
        }

        private void lb_tumbilgiler_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1 && !checkBox1.Checked)
            {
                da = new OleDbDataAdapter("Select *from sakinkayit where odano like'" + odaNo + "'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit");
                lb_odadurum.ForeColor = Color.Red;
                groupBox17.Visible = false; groupBox19.Visible = true;
                listBox1.SelectedIndex = -1; listBox2.SelectedIndex = -1;
                string odaa = ds2.Tables["sakinkayit"].Rows[0]["odano"].ToString();
                tabControl1.SelectedIndex = 0;
                tb_odara.Text = odaa;
            }
        }
        private void cb_kisiduzenle_Click(object sender, EventArgs e)
        {

            if (cb_kisiduzenle.Checked)
            {
                btn_bakim.Enabled = btn_bakimcikar.Enabled = false;
                tabkapat = tb_kisisayisi.Enabled = true;
                cb_kisiduzenle.Text = "Düzenlemeyi Bitir";
            }
            else
            {
                da = new OleDbDataAdapter("Select *from sakinkayit where odano like'" + odaNo + "'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit1");
                int row = ds2.Tables["sakinkayit1"].Rows.Count;

                if (listBox1.SelectedIndex != -1 || listBox2.SelectedIndex != -1 || listBox3.SelectedIndex != -1)
                {
                    if (IsNumeric(tb_kisisayisi.Text))
                    {
                        sayi = Convert.ToInt64(tb_kisisayisi.Text);
                        if (sayi >= row)
                        {
                            if (sayi >= 0 && sayi <= 10)
                            {
                                for (int i = satirsayisi + 1; i <= sayi; i++)
                                {
                                    cmd = new OleDbCommand("update odalar Set kişi=@kişi where odano=@odano", con);
                                    cmd.Parameters.AddWithValue("@kişi", tb_kisisayisi.Text);
                                    cmd.Parameters.AddWithValue("@odano", odaNo);
                                    cmd.ExecuteNonQuery();
                                }
                            } else { tb_kisisayisi.Text = kisisayisi; cb_kisiduzenle.Checked = false; MessageBox.Show("Min. Kişi Sayısı= 1\nMaks. Kişi Sayısı= 10", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                         } else MessageBox.Show("Bu odaya kayıtlı "+row.ToString() + " sakin var!\nBu odanın kişi sayısını azaltamazsınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else { tb_kisisayisi.Text = kisisayisi; cb_kisiduzenle.Checked = false; }
                }else MessageBox.Show("Herhangi bir Oda Seçmediniz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btn_bakim.Enabled = btn_bakimcikar.Enabled = true;
                    tabkapat = tb_kisisayisi.Enabled = false;
                    cb_kisiduzenle.Text = "Sayıyı Düzenle";
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            satirsayisi = Convert.ToInt32(ds.Tables["odalar"].Rows.Count);
            if (checkBox1.Checked)
            {
                btn_bakim.Enabled = btn_bakimcikar.Enabled = false;
                tabkapat = true; tb_odasayisi.Enabled = true;
                checkBox1.Text = "Düzenlemeyi Bitir";
            }
            else
            {
                {
                    tb_odasayisi.Enabled = false; checkBox1.Text = "Sayıyı Düzenle";
                    if (IsNumeric(tb_odasayisi.Text))
                    {
                        sayi = Convert.ToInt64(tb_odasayisi.Text);
                        if (sayi >= 5 && sayi<=300)
                        {
                            if (sayi > satirsayisi)
                            {
                                for (int i = satirsayisi + 1; i <= sayi; i++)
                                {
                                    cmd = new OleDbCommand("insert into odalar (odano,durum,bakim,kişi) values (@odano,@durum,@bakim,@kişi)", con);
                                    cmd.Parameters.AddWithValue("@odano", i);
                                    cmd.Parameters.AddWithValue("@durum", "Boş");
                                    cmd.Parameters.AddWithValue("@bakim", "");
                                    cmd.Parameters.AddWithValue("@kişi", "1");
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (sayi < satirsayisi)
                            {
                                cevap = MessageBox.Show("Sildiğiniz odalarda bulunan kayıtlar silinecektir.\n\rDevam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (cevap == DialogResult.Yes)
                                {
                                    for (int i = satirsayisi; i > sayi; i--)
                                    {
                                        cmd = new OleDbCommand("delete from odalar where odano=@odano", con);
                                        cmd.Parameters.AddWithValue("@odano", i);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else tb_odasayisi.Text = satirsayisi.ToString();
                            }
                            cmd = new OleDbCommand("update tut Set adet=@adet where adet=@adet2", con);
                            cmd.Parameters.AddWithValue("@adet", tb_odasayisi.Text);
                            cmd.Parameters.AddWithValue("@adet2", odasayisi);
                            cmd.ExecuteNonQuery();
                            odasayisi = Convert.ToInt32(tb_odasayisi.Text);
                            kayitgoster(); listekle();
                            lb_odas.Text = listBox1.Items.Count.ToString();
                            lb_bsayi.Text = listBox2.Items.Count.ToString();
                            lb_doluoda.Text = listBox3.Items.Count.ToString();
                            tabkapat = false; btn_bakim.Enabled = btn_bakimcikar.Enabled = true;
                        }
                        else { tb_odasayisi.Text = satirsayisi.ToString(); checkBox1.Checked = false; MessageBox.Show("Min. Oda Sayısı= 5\nMaks. Oda Sayısı= 300", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else { tb_odasayisi.Text = satirsayisi.ToString(); checkBox1.Checked = false; }
                }       
            }
        }

        private void cb_odano_SelectedIndexChanged(object sender, EventArgs e)
        {
                da = new OleDbDataAdapter("Select *from odalar where odano like'" + cb_odano.SelectedItem.ToString() + "'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "odalar1");
                if (ds2.Tables["odalar1"].Rows[0]["kişi"].ToString()=="1") lb_kisisayisi.Text = "Tek Kişilik";
                else if (ds2.Tables["odalar1"].Rows[0]["kişi"].ToString() == "2") lb_kisisayisi.Text = " Çift Kişilik";
                else lb_kisisayisi.Text = ds2.Tables["odalar1"].Rows[0]["kişi"].ToString() + " Kişilik";
        }

        private void tb_odara_TextChanged(object sender, EventArgs e)
        {
            if (tb_odara.Text != "")
            {
                btn_ekle.Enabled = false;
                da = new OleDbDataAdapter("Select *from sakinkayit where odano like '%" + tb_odara.Text + "%'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit1");
                if (ds2.Tables["sakinkayit1"].Rows.Count == 0) guncelle.Enabled = button3.Enabled = false;
                else guncelle.Enabled = button3.Enabled = true;
                datagridkayit.DataSource = ds2.Tables["sakinkayit1"];
            }
            else { btn_ekle.Enabled = true; datagridkayit.DataSource = ds.Tables["sakinkayit"]; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                button12.Enabled = false;
                da = new OleDbDataAdapter("Select *from sakinkayit where odano like '%" + textBox4.Text + "%'", con);
                ds2 = new DataSet();
                da.Fill(ds2, "sakinkayit1");
                if (ds2.Tables["sakinkayit1"].Rows.Count == 0) button12.Enabled = false;
                else button12.Enabled = true;
                datagridbilgi.DataSource = ds2.Tables["sakinkayit1"];
            }
            else { button12.Enabled = true; datagridbilgi.DataSource = ds.Tables["sakinkayit"]; }
        }
    }
}
