namespace huzurevi
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sakinkayitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.verilerDataSet = new huzurevi.verilerDataSet();
            this.yakinkayitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sakinkayitTableAdapter = new huzurevi.verilerDataSetTableAdapters.sakinkayitTableAdapter();
            this.yakinkayitTableAdapter = new huzurevi.verilerDataSetTableAdapters.yakinkayitTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sakinkayitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verilerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yakinkayitBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // sakinkayitBindingSource
            // 
            this.sakinkayitBindingSource.DataMember = "sakinkayit";
            this.sakinkayitBindingSource.DataSource = this.verilerDataSet;
            // 
            // verilerDataSet
            // 
            this.verilerDataSet.DataSetName = "verilerDataSet";
            this.verilerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // yakinkayitBindingSource
            // 
            this.yakinkayitBindingSource.DataMember = "yakinkayit";
            this.yakinkayitBindingSource.DataSource = this.verilerDataSet;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sakinkayitBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "huzurevi.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ShowZoomControl = false;
            this.reportViewer1.Size = new System.Drawing.Size(1014, 138);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
            // 
            // reportViewer2
            // 
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.yakinkayitBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "huzurevi.Report2.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(12, 146);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ShowZoomControl = false;
            this.reportViewer2.Size = new System.Drawing.Size(1014, 138);
            this.reportViewer2.TabIndex = 0;
            this.reportViewer2.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
            // 
            // sakinkayitTableAdapter
            // 
            this.sakinkayitTableAdapter.ClearBeforeFill = true;
            // 
            // yakinkayitTableAdapter
            // 
            this.yakinkayitTableAdapter.ClearBeforeFill = true;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 291);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rapor";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sakinkayitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verilerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yakinkayitBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource sakinkayitBindingSource;
        private verilerDataSet verilerDataSet;
        private verilerDataSetTableAdapters.sakinkayitTableAdapter sakinkayitTableAdapter;
        private System.Windows.Forms.BindingSource yakinkayitBindingSource;
        private verilerDataSetTableAdapters.yakinkayitTableAdapter yakinkayitTableAdapter;
    }
}