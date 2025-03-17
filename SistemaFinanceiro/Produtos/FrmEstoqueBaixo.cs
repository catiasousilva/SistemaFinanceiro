using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFinanceiro.Produtos
{
    public partial class FrmEstoqueBaixo : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        Int32 ValorMini;
        Int32 ValorEstoque;

        public FrmEstoqueBaixo()
        {
            InitializeComponent();
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    
                    btn.ForeColor = Color.White;
                    
                }
            }
            
                     
        }
        private void FormatarGD()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Código";
            grid.Columns[2].HeaderText = "Produto";
            grid.Columns[3].HeaderText = "Descrição";
            grid.Columns[4].HeaderText = "Estoque";
            grid.Columns[5].HeaderText = "Fornecedor";
            grid.Columns[6].HeaderText = "Entrada";
            grid.Columns[7].HeaderText = "V. Pago";
            grid.Columns[8].HeaderText = "Venda";
            grid.Columns[9].HeaderText = "Custo Unit";
            grid.Columns[10].HeaderText = "Data";
            grid.Columns[11].HeaderText = "Imagem";
            
            grid.Columns[13].HeaderText = "Mínimo";
            grid.Columns[14].HeaderText = "N. Doc";

            grid.Columns[7].DefaultCellStyle.Format = "c2";
            grid.Columns[8].DefaultCellStyle.Format = "c2";
            grid.Columns[9].DefaultCellStyle.Format = "c2";
            grid.Columns[0].Visible = false;
            grid.Columns[11].Visible = false;
            grid.Columns[12].Visible = false;

        }
       
        private void Listar()
        {       

            con.AbrirConexao();
            
            sql = "SELECT pro.id, pro.cod, pro.nome, pro.descricao, pro.estoque, forn.nome, pro.entrada, pro.total_compra, pro.valor_compra, pro.valor_venda, pro.data, pro.imagem, pro.fornecedor, pro.minimo, pro.nota  FROM produtos as pro INNER JOIN fornecedores as forn  ON pro.fornecedor = forn.id WHERE estoque < pro.minimo ORDER BY pro.nome asc";

            cmd = new MySqlCommand(sql, con.con);
            //cmd.Parameters.AddWithValue("@estoque", 15);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }

        private void FrmEstoqueBaixo_Load(object sender, EventArgs e)
        {            
            Listar();
            LoadTheme();
          
        }

        private void FrmEstoqueBaixo_Activated(object sender, EventArgs e)
        {
            Listar();
        }
       
    }
}
