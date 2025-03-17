
namespace SistemaFinanceiro.Relatorios
{
    partial class FrmRecibo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecibo));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.vendasDataSet = new SistemaFinanceiro.vendasDataSet();
            this.detalheslancarvendasPorIdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.detalhes_lancarvendasPorIdTableAdapter = new SistemaFinanceiro.vendasDataSetTableAdapters.detalhes_lancarvendasPorIdTableAdapter();
            this.lancarvendasPorIdVendasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lancar_vendasPorId_VendasTableAdapter = new SistemaFinanceiro.vendasDataSetTableAdapters.lancar_vendasPorId_VendasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.vendasDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalheslancarvendasPorIdBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lancarvendasPorIdVendasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSetDetalheVenda";
            reportDataSource1.Value = this.detalheslancarvendasPorIdBindingSource;
            reportDataSource2.Name = "DataSetLancarVendasPorIdVendas";
            reportDataSource2.Value = this.lancarvendasPorIdVendasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaFinanceiro.Relatorios.RelRecibo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-2, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(790, 602);
            this.reportViewer1.TabIndex = 0;
            // 
            // vendasDataSet
            // 
            this.vendasDataSet.DataSetName = "vendasDataSet";
            this.vendasDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // detalheslancarvendasPorIdBindingSource
            // 
            this.detalheslancarvendasPorIdBindingSource.DataMember = "detalhes_lancarvendasPorId";
            this.detalheslancarvendasPorIdBindingSource.DataSource = this.vendasDataSet;
            // 
            // detalhes_lancarvendasPorIdTableAdapter
            // 
            this.detalhes_lancarvendasPorIdTableAdapter.ClearBeforeFill = true;
            // 
            // lancarvendasPorIdVendasBindingSource
            // 
            this.lancarvendasPorIdVendasBindingSource.DataMember = "lancar_vendasPorId_Vendas";
            this.lancarvendasPorIdVendasBindingSource.DataSource = this.vendasDataSet;
            // 
            // lancar_vendasPorId_VendasTableAdapter
            // 
            this.lancar_vendasPorId_VendasTableAdapter.ClearBeforeFill = true;
            // 
            // FrmRecibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 602);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRecibo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comprovante";
            this.Load += new System.EventHandler(this.FrmRecibo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vendasDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalheslancarvendasPorIdBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lancarvendasPorIdVendasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource detalheslancarvendasPorIdBindingSource;
        private vendasDataSet vendasDataSet;
        private System.Windows.Forms.BindingSource lancarvendasPorIdVendasBindingSource;
        private vendasDataSetTableAdapters.detalhes_lancarvendasPorIdTableAdapter detalhes_lancarvendasPorIdTableAdapter;
        private vendasDataSetTableAdapters.lancar_vendasPorId_VendasTableAdapter lancar_vendasPorId_VendasTableAdapter;
    }
}