using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace huzurevi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        void isaretlimi(CheckBox a)
        {
            if (a.Checked == true) a.Checked = false;
            else a.Checked = true;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lb_odano.Text = Form1.bilgi[0];
            lb_tc.Text = Form1.bilgi[1];
            lb_ad.Text = Form1.bilgi[2];
            lb_soyad.Text = Form1.bilgi[3];
            lb_tel.Text = Form1.bilgi[4];
            lb_dogumt.Text = Form1.bilgi[5];
            lb_dogumy.Text = Form1.bilgi[6];
            if (Form1.bilgi[7] == "Bay") { rd_bay.Checked = true; rd_bayan.Enabled = false; }
            else { rd_bayan.Checked = true; rd_bay.Enabled = false; }
            lb_kan.Text = Form1.bilgi[8];
            lb_meslek.Text = Form1.bilgi[9];
            lb_saglik.Text = Form1.bilgi[10];
            lb_adres.Text = Form1.bilgi[11];
            pictureBox2.ImageLocation = Form1.bilgi[21];

                groupBox1.Visible = true;
                lb_ytc.Text = Form1.bilgi[12];
                lb_yad.Text = Form1.bilgi[13];
                lb_ysoy.Text = Form1.bilgi[14];
                lb_ytel.Text = Form1.bilgi[15];
                lb_yevtel.Text = Form1.bilgi[16];
                if (Form1.bilgi[17] == "Bay") { rd_bay2.Checked = true; rd_bayan2.Enabled = false; }
                else { rd_bayan2.Checked = true; rd_bay2.Enabled = false; }
                lb_ymeslek.Text = Form1.bilgi[18];
                lb_sehir.Text = Form1.bilgi[19];
                lb_yadres.Text = Form1.bilgi[20];

            if (Form1.hasta[0] == "-1") cb_romatizma.Checked = true;
            if (Form1.hasta[1] == "-1") cb_migren.Checked = true;
            if (Form1.hasta[2] == "-1") cb_kolestrol.Checked = true;
            if (Form1.hasta[3] == "-1") cb_kanser.Checked = true;
            if (Form1.hasta[4] == "-1") cb_felc.Checked = true;
            if (Form1.hasta[5] == "-1") cb_kemik.Checked = true;
            if (Form1.hasta[6] == "-1") cb_diyabet.Checked = true;
            if (Form1.hasta[7] == "-1") cb_kalpc.Checked = true;
            if (Form1.hasta[8] == "-1") cb_seker.Checked = true;
            if (Form1.hasta[9] == "-1") cb_tansiyon.Checked = true;
            if (Form1.hasta[10] == "-1") cb_gastrit.Checked = true;
            if (Form1.hasta[11] == "-1") cb_damar.Checked = true;
            if (Form1.hasta[12] == "-1") cb_obez.Checked = true;
            if (Form1.hasta[13] == "-1") cb_prostat.Checked = true;
            if (Form1.hasta[14] == "-1") cb_gorme.Checked = true;
            lb_diger.Text = Form1.hasta[15];
            if (Form1.hasta[16] == "-1") cb_saglikli.Checked = true;
            if (cb_saglikli.Checked) groupBox14.Enabled = false;

        }

        private void cb_romatizma_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_romatizma);
        }

        private void cb_migren_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_migren);
        }

        private void cb_kolestrol_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_kolestrol);
        }

        private void cb_kanser_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_kanser);
        }

        private void cb_felc_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_felc);
        }

        private void cb_kemik_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_kemik);
        }

        private void cb_diyabet_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_diyabet);
        }

        private void cb_kalpc_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_kalpc);
        }

        private void cb_tansiyon_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_tansiyon);
        }

        private void cb_seker_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_seker);
        }

        private void cb_gastrit_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_gastrit);
        }

        private void cb_damar_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_damar);
        }

        private void cb_obez_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_obez);
        }

        private void cb_prostat_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_prostat);
        }
        private void cb_gorme_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_gorme);
        }

        private void cb_saglikli_Click(object sender, EventArgs e)
        {
            isaretlimi(cb_saglikli);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }
    }
}
