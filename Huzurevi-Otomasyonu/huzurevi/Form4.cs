using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.OleDb;

namespace huzurevi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbDataAdapter da;
        DataSet ds;
        private void Form4_Load(object sender, EventArgs e)
        {
            da = new OleDbDataAdapter("Select *from sakinkayit where tcno= '" + Form1.bilgi[1] + "'", Form1.con);
            ds = new DataSet();
            da.Fill(ds, "sakinkayit1");
            da = new OleDbDataAdapter("Select *from yakinkayit where tcno= '" + Form1.bilgi[1] + "'", Form1.con);
            da.Fill(ds, "yakinkayit1");

            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["sakinkayit1"]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();

            ReportDataSource rds2 = new ReportDataSource("DataSet2", ds.Tables["yakinkayit1"]);
            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(rds2);
            this.reportViewer2.LocalReport.Refresh();
            this.reportViewer2.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
