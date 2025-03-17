using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFinanceiro.Relatorios
{
    public partial class FrmRelMovimentacao : Form
    {
        public FrmRelMovimentacao()
        {
            InitializeComponent();
        }

        private void FrmRelMovimentacao_Load(object sender, EventArgs e)
        {
            dtInicial.Value = DateTime.Today;
            dtFinal.Value = DateTime.Today;
            Buscar();
        }
        private void Buscar()
        {            

            this.reportViewer1.RefreshReport();

            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicial", dtInicial.Text));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFinal", dtFinal.Text));

            this.reportViewer1.RefreshReport();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {
                dtInicial.Enabled = true;
                dtFinal.Enabled = true;
            }
        }

        private void dtInicial_ValueChanged(object sender, EventArgs e)
        {
            if (Program.CargoUsuario == "Programadora" || Program.CargoUsuario == "Gerente")
            {
                if (dtInicial.Value <= dtFinal.Value)
                {
                    Buscar();
                }
                else
                {
                    MessageBox.Show("Intervalo incorreto. A data inicial não deve ser maior que a data final");
                    dtInicial.Value = DateTime.Today;
                }
            }
        }

        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            if (Program.CargoUsuario == "Programadora" || Program.CargoUsuario == "Gerente")
            {
                if (dtFinal.Value >= dtInicial.Value)
                {
                    Buscar();
                }
                else
                {
                    MessageBox.Show("Intervalo incorreto. A data final não deve ser menor que a data inicial");
                    dtFinal.Value = DateTime.Today;
                }
            }
        }
    }
}
