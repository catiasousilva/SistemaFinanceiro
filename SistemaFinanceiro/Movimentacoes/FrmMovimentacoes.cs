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

namespace SistemaFinanceiro.Movimentacoes
{
    public partial class FrmMovimentacoes : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string idVenda;
        double totalEntradas, totalSaidas;       
        public FrmMovimentacoes()
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
            grid.Columns[1].HeaderText = "Cliente";
            grid.Columns[2].HeaderText = "Tipo";
            grid.Columns[3].HeaderText = "Movimentações";
            grid.Columns[4].HeaderText = "Descrição";
            grid.Columns[5].HeaderText = "Valor";
            grid.Columns[6].HeaderText = "Desconto";
            grid.Columns[7].HeaderText = "Taxa";
            grid.Columns[8].HeaderText = "Valor total";
            grid.Columns[9].HeaderText = "Cartão";
            grid.Columns[10].HeaderText = "Pix";
            grid.Columns[11].HeaderText = "Dinheiro";
            grid.Columns[12].HeaderText = "Nota";
            grid.Columns[13].HeaderText = "Total pago";
            grid.Columns[14].HeaderText = "Funcionário";
            grid.Columns[15].HeaderText = "Id Movimentação";
            grid.Columns[16].HeaderText = "Data";
            grid.Columns[5].DefaultCellStyle.Format = "c2";
            grid.Columns[6].DefaultCellStyle.Format = "c2";
            grid.Columns[7].DefaultCellStyle.Format = "c2";
            grid.Columns[8].DefaultCellStyle.Format = "c2";
            grid.Columns[9].DefaultCellStyle.Format = "c2";
            grid.Columns[10].DefaultCellStyle.Format = "c2";
            grid.Columns[11].DefaultCellStyle.Format = "c2";
            grid.Columns[13].DefaultCellStyle.Format = "c2";
                             
            grid.Columns[0].Visible = false;
            grid.Columns[15].Visible = false;

            TotalizandoEntradas();
            TotalizandoDinheiro();
            TotalizandoPix();
            TotalizandoDescontos();
            TotalizandoCartao();
            TotalizandoSaindas();
           
            TotalizandoCancelados();
            Totalizando();

        }

        private void listaMovimento()
        {
            try
            {
                con.AbrirConexao();
                if (cbTipo.SelectedIndex == 0)
                {
                    sql = "SELECT * FROM lancar_movimentacoes WHERE data >= @dataInicial AND data <= @dataFinal ORDER BY data desc";
                    cmd = new MySqlCommand(sql, con.con);
                }
                else
                {
                    sql = "SELECT * FROM lancar_movimentacoes WHERE data >= @dataInicial AND data <= @dataFinal AND tipo = @tipo ORDER BY data desc";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@tipo", cbTipo.Text);
                }
                cmd.Parameters.AddWithValue("@dataInicial", DateTime.Today);
                cmd.Parameters.AddWithValue("@dataFinal", DateTime.Today);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();
                FormatarGD();

                if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
                {
                    cbTipo.Enabled = true;
                    dtInicial.Enabled = true;
                    dtFinal.Enabled = true;
                    txtNome.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listarNota() 
        {
            con.AbrirConexao();
            sql = "SELECT * FROM lancar_movimentacoes WHERE nota=@nota ORDER BY nota asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        private void ListarNome()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM lancar_movimentacoes WHERE cliente LIKE @cliente ORDER BY cliente asc"; //LIKE, busca nome por aproximacao
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", txtNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        
        private void BuscarData()
        {
            try
            {
                con.AbrirConexao();
                if (cbTipo.SelectedIndex == 0)
                {
                    sql = "SELECT * FROM lancar_movimentacoes WHERE data >= @dataInicial AND data <= @dataFinal ORDER BY data desc";
                    cmd = new MySqlCommand(sql, con.con);
                }
                else
                {
                    sql = "SELECT * FROM lancar_movimentacoes WHERE data >= @dataInicial AND data <= @dataFinal AND tipo = @tipo ORDER BY data desc";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@tipo", cbTipo.Text);
                }
                cmd.Parameters.AddWithValue("@dataInicial", Convert.ToDateTime(dtInicial.Text));
                cmd.Parameters.AddWithValue("@dataFinal", Convert.ToDateTime(dtFinal.Text));
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();
                FormatarGD();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        
        private void FrmMovimentacoes_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
            dtInicial.Value = DateTime.Today;
            dtFinal.Value = DateTime.Today;
           
            listaMovimento();
            
            LoadTheme();
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            BuscarData(); 
        }

        private void dtInicial_ValueChanged(object sender, EventArgs e)
        {
            if (Program.CargoUsuario == "Programadora" || Program.CargoUsuario == "Gerente")
            {
               
                BuscarData(); 

            }
        }

        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            if (Program.CargoUsuario == "Programadora" || Program.CargoUsuario == "Gerente")
            {
               
                BuscarData(); 

            }
        }
        private void TotalizandoEntradas()
        {
            double total = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {                    
                    total += Convert.ToDouble(row.Cells["valor_pago"].Value); 
                }
                
            }
            totalEntradas = total;
            lbEntradas.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoDinheiro()
        {
            double total = 0;
          
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {
                   
                    total += Convert.ToDouble(row.Cells["dinheiro"].Value);
                }

            }
          
            lblDinheiro.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoCartao()
        {
            double total = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {                   
                    total += Convert.ToDouble(row.Cells["cartao"].Value); 
                }

            }
           
            lblCartao.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoPix()
        {
            double total = 0;
          
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {                   
                    total += Convert.ToDouble(row.Cells["pix"].Value);
                }

            }
           
            lblPix.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoDescontos()
        {
            double total = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {                   
                    total += Convert.ToDouble(row.Cells["desconto"].Value); 
                }
            }           
            lblDescontos.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoValorExcluido()
        {
            double total = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Entrada")
                {
                    
                    total += Convert.ToDouble(row.Cells["exclusao"].Value); 
                }

            }
            
            lblValorExcluido.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoSaindas()
        {
            double total = 0;
            
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Saída")
                {                    
                    total += Convert.ToDouble(row.Cells["valor_pago"].Value); 
                }
            }
            totalSaidas = total;
            lbSaidas.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void TotalizandoCancelados()
        {            
            double total = 0;
            
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["tipo"].Value.ToString() == "Cancelado")
                {
                    
                    total += Convert.ToDouble(row.Cells["exclusao"].Value); 
                }

            }
           
            lblCancelado.Text = Convert.ToDouble(total).ToString("C2");
        }

        private void FrmMovimentacoes_FormClosing(object sender, FormClosingEventArgs e)
        {
            grid.ClearSelection();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            ListarNome();
        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {
            listarNota();
        }

        private void Totalizando()
        {
            double total = 0;
            
            foreach (DataGridViewRow row in grid.Rows)
            {
                total = totalEntradas + totalSaidas; 
            }
            if (total > 0 )
            {
                lbSaldo.ForeColor = Color.Green;
            }
            else if(total < 0)
            {
                lbSaldo.ForeColor = Color.Red;
            }
            else if (total == 0)
            {
                lbSaldo.ForeColor = Color.Black;
            }
            lbSaldo.Text = Convert.ToDouble(total).ToString("C2");
        }

    }
}
