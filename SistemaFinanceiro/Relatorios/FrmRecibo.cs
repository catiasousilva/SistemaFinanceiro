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
    public partial class FrmRecibo : Form
    {
        public FrmRecibo()
        {
            InitializeComponent();
        }

        private void FrmRecibo_Load(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {                   
            this.detalhes_lancarvendasPorIdTableAdapter.Fill(vendasDataSet.detalhes_lancarvendasPorId, Convert.ToInt32(Program.idVenda));
            this.lancar_vendasPorId_VendasTableAdapter.Fill(vendasDataSet.lancar_vendasPorId_Vendas, Convert.ToInt32(Program.idVenda));
            this.reportViewer1.RefreshReport();            
        }



    }
}
