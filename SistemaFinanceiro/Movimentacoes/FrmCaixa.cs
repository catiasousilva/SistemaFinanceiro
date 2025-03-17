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
    public partial class FrmCaixa : Form
    {

        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        double pagtoD = 0;
        double pagtoC = 0;
        double pagtoP = 0;

        public FrmCaixa()
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
        private void FrmCaixa_Load(object sender, EventArgs e)
        {
            LoadTheme();
            dtInicial.Text = DateTime.Now.ToString();
            dtFinal.Text = DateTime.Now.ToString();
            cbTipo.SelectedIndex = 0;
           
            BuscarTipo();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {
                grid.Enabled = true;
                btnAdd.Enabled = true;
                btnSub.Enabled = true;
                lblTransRecebidaNaConta.Visible = true;
                lblTransRecebidaNoCaixa.Visible = true;
                btnConta.Enabled = true;
                btnOKCaixa.Enabled = true;
                cbTipo.Enabled = true;
                lblEntrada.Visible = true;
                lblTransferencias.Visible = true;
                dtInicial.Enabled = true;
                dtFinal.Enabled = true;
                Listar();
            }
        }
        private void FormatarGD()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Data";
            grid.Columns[2].HeaderText = "Hisórico";
            grid.Columns[3].HeaderText = "Forma Pagamento";
            grid.Columns[4].HeaderText = "Valor";
            grid.Columns[5].HeaderText = "Taxa";
            grid.Columns[6].HeaderText = "Total";
            grid.Columns[7].HeaderText = "Saídas";
            grid.Columns[8].HeaderText = "Transf. D/C";
            grid.Columns[9].HeaderText = "Transf. C/D";              
            grid.Columns[10].HeaderText = "tipo";
            grid.Columns[11].HeaderText = "Dinheiro";
            grid.Columns[12].HeaderText = "Pix";
            grid.Columns[13].HeaderText = "Cartão";
            grid.Columns[14].HeaderText = "Funcionário";

            grid.Columns[4].DefaultCellStyle.Format = "C2";
            grid.Columns[5].DefaultCellStyle.Format = "C2";
            grid.Columns[6].DefaultCellStyle.Format = "C2";
            grid.Columns[7].DefaultCellStyle.Format = "C2";
            grid.Columns[8].DefaultCellStyle.Format = "C2";
            grid.Columns[9].DefaultCellStyle.Format = "C2";
            grid.Columns[10].DefaultCellStyle.Format = "C2";
            grid.Columns[11].DefaultCellStyle.Format = "C2";
            grid.Columns[12].DefaultCellStyle.Format = "C2";
            grid.Columns[0].Visible = false;
            
            Totalizando();
        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM caixa ORDER BY data desc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        private void LimparCampos()
        {
            textDescricao.Text = "";
            txtSaida.Text = "0";        
            txtDescricao.Text = "";
            txtEntrada.Text = "0";
            txtTaxa.Text = "0";
            txtTaxa.Enabled = false;
            cbFormaPagto.SelectedIndex = 0;
        }
        private void BuscarData()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM caixa WHERE data>=@dataInicial AND data<=@dataFinal AND tipo=@tipo ORDER BY data DESC ";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@dataInicial", Convert.ToDateTime(dtInicial.Text));
            cmd.Parameters.AddWithValue("@dataFinal", Convert.ToDateTime(dtFinal.Text));
            cmd.Parameters.AddWithValue("@tipo", cbTipo.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();            
            FormatarGD();
        }
        private void BuscarTipo()
        {
            try
            {
                con.AbrirConexao();
                if (cbTipo.SelectedIndex == 0)
                {
                    sql = "SELECT * FROM caixa WHERE data>=@dataInicial AND data<=@dataFinal ORDER BY data desc";
                    cmd = new MySqlCommand(sql, con.con);
                }
                else
                {
                    sql = "SELECT * FROM caixa WHERE data>=@dataInicial AND data<=@dataFinal AND tipo=@tipo ORDER BY data desc";
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
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

            }
            else
            {
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEntrada.Text.ToString().Trim() == "" || txtEntrada.Text == "0" || txtEntrada.Text == "0,00")
            {
                MessageBox.Show("Preencher o valor da Entrada(+)", " Mensagem ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEntrada.Focus();
                return;
            }
            if (txtDescricao.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher o campo da Justificativa.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescricao.Focus();
                return;
            }
                     
            if (cbFormaPagto.Text == "Dinheiro")
            {
                pagtoD = Convert.ToDouble(txtEntrada.Text);
                pagtoC = 0;
                pagtoP = 0;
            }
            if (cbFormaPagto.Text == "Pix")
            {
                pagtoP = Convert.ToDouble(txtEntrada.Text);
                pagtoC = 0;
                pagtoD = 0;
            }
            if (cbFormaPagto.Text == "Cartão")
            {
                pagtoC = Convert.ToDouble(txtEntrada.Text);
                pagtoD = 0;               
                pagtoP = 0;
            }
            try
            {
                con.AbrirConexao();
                sql = "INSERT INTO caixa(data, historico, forma_pagto, valor, taxa, total, saida, trans_dinheiro, trans_conta, tipo, dinheiro, pix, cartao, funcionario) VALUES(curDate(), @historico, @forma_pagto, @valor, @taxa, @total, @saida,  @trans_dinheiro, @trans_conta, @tipo, @dinheiro, @pix, @cartao, @funcionario)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@historico",txtDescricao.Text);
                cmd.Parameters.AddWithValue("@forma_pagto", cbFormaPagto.Text);
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtEntrada.Text));
                cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@total", Convert.ToDouble(txtEntrada.Text)- Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@saida", 0);
                cmd.Parameters.AddWithValue("@trans_dinheiro", 0);
                cmd.Parameters.AddWithValue("@trans_conta", 0);
                cmd.Parameters.AddWithValue("@tipo", "Entrada");
                cmd.Parameters.AddWithValue("@dinheiro", pagtoD);
                cmd.Parameters.AddWithValue("@pix", pagtoP);
                cmd.Parameters.AddWithValue("@cartao", pagtoC);
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                
                cbFormaPagto.Enabled = true;
            }
            catch (Exception h)
            {
                MessageBox.Show("Feche o sistema e abra novamente...   " + h);
            }            

            LimparCampos();
            BuscarTipo();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {                
                Listar();
            }
            MessageBox.Show("Lançado com sucesso", "Lançamento");
        }
        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtSaida.Text.ToString().Trim() == "" || txtSaida.Text == "0" || txtSaida.Text == "0,00")
            {
                MessageBox.Show("Preencher campo Saída(-)", " Mensagem ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSaida.Focus();
                return;
            }
            if (textDescricao.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher o campo descrição !!!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textDescricao.Focus();
                return;
            }            
            try
            {
                con.AbrirConexao();
                sql = "INSERT INTO caixa(data, historico, forma_pagto, valor, taxa, total, saida, trans_dinheiro, trans_conta, tipo, dinheiro, pix, cartao, funcionario) VALUES(curDate(), @historico, @forma_pagto, @valor, @taxa, @total, @saida, @trans_dinheiro, @trans_conta, @tipo, @dinheiro, @pix, @cartao, @funcionario)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@historico", textDescricao.Text);
                cmd.Parameters.AddWithValue("@forma_pagto", "Retirada");
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtSaida.Text));
                cmd.Parameters.AddWithValue("@taxa", 0);
                cmd.Parameters.AddWithValue("@total",0);
                cmd.Parameters.AddWithValue("@saida", Convert.ToDouble(txtSaida.Text));
                cmd.Parameters.AddWithValue("@trans_dinheiro", 0);
                cmd.Parameters.AddWithValue("@trans_conta", 0);
                cmd.Parameters.AddWithValue("@tipo", "Saída");
                cmd.Parameters.AddWithValue("@dinheiro", 0);
                cmd.Parameters.AddWithValue("@pix", 0);
                cmd.Parameters.AddWithValue("@cartao", 0);
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
            }
            catch (Exception h)
            {
                MessageBox.Show("Feche o sistema e abra novamente...   " + h);
            }
            LimparCampos();
            BuscarTipo();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {
                Listar();
            }
            MessageBox.Show("Lançado com sucesso", "Lançamento");
        }
        private void Totalizando()
        {
            double total = 0;
            double totalD = 0;
            double totalC = 0;
            double totalP = 0;
            double taxa = 0;
            double totalSaida = 0;
            double transfD = 0;
            double transfC = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {                
                total += Convert.ToDouble(row.Cells["total"].Value);
                totalD += Convert.ToDouble(row.Cells["dinheiro"].Value);
                totalP += Convert.ToDouble(row.Cells["pix"].Value);
                totalC += Convert.ToDouble(row.Cells["cartao"].Value);
                taxa += Convert.ToDouble(row.Cells["taxa"].Value);
                totalSaida += Convert.ToDouble(row.Cells["saida"].Value);
                transfC += Convert.ToDouble(row.Cells["trans_dinheiro"].Value);
                transfD += Convert.ToDouble(row.Cells["trans_conta"].Value);
            }
            lblEntrada.Text = Convert.ToDouble(total).ToString("C2");
            lblSaida.Text = Convert.ToDouble(totalSaida).ToString("C2");
            lblDinheiro.Text = Convert.ToDouble(totalD).ToString("C2");
            lblPix.Text = Convert.ToDouble(totalP).ToString("C2");
            lblCartao.Text = Convert.ToDouble(totalC).ToString("C2");
            lblTaxas.Text = Convert.ToDouble(taxa).ToString("C2");
            lblEmContas.Text = string.Format("{0:c2}", (totalC - taxa) + totalP+ transfC);// 
            lblEmCaixa.Text = string.Format("{0:c2}", (totalD - totalSaida)+ transfD);// 
            lblSaida.Text = string.Format("{0:c2}", totalSaida);
            lblTransferencias.Text= string.Format("{0:c2}", transfD+ transfC);
            lblSaldo.Text= string.Format("{0:c2}", total-totalSaida);
            lblSaldoEmCaixa.Text = string.Format("{0:c2}", (totalD - totalSaida) + transfD);
            lblSaldoEmConta.Text = string.Format("{0:c2}", (totalC - taxa) + totalP + transfC);
            lblTransRecebidaNoCaixa.Text = string.Format("{0:c2}", transfD);
            lblTransRecebidaNaConta.Text = string.Format("{0:c2}", transfC);
        }       
        private void formatarTextNumero(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -2)
                {
                    e.Handled = true;
                }


                if (e.KeyChar == ','
                    && (sender as TextBox).Text.IndexOf(',') > -2)
                {
                    e.Handled = true;
                }

                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)44)
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

            }
        }
             
        public static void Moeda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(2, '0');

                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);

                v = Convert.ToDouble(n) / 100;

                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
            }
        }

        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtSaida_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtEntrada_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtEntrada);
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTaxa);
        }

        private void txtSaida_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtSaida);
        }

        private void cbFormaPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFormaPagto.Text == "Cartão")
            {
                MessageBox.Show("Insira o valor cobrado pelo cartão.","TAXA DA BANDEIRA DO CARTÃO");
                txtTaxa.Enabled = true;
                txtTaxa.Focus();
            }
            if (cbFormaPagto.Text == "Pix")
            {
                txtTaxa.Enabled = false;
            }
            if (cbFormaPagto.Text == "Dinheiro")
            {
                txtTaxa.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtParaCaixa.Text = "0";
            txtParaConta.Text = "0";
        }
         
        private void btnOKCaixa_Click(object sender, EventArgs e)
        {
            if (txtParaConta.Text.ToString().Trim() == "" || txtParaConta.Text == "0" || txtParaConta.Text == "0,00")
            {
                MessageBox.Show("Não foi declarado o valor da conta a ser transferido.", "TRANSFERÊCIA");
                txtParaConta.Focus();
                return;
            }
            try
            {
                con.AbrirConexao();
                sql = "INSERT INTO caixa(data, historico, forma_pagto, valor, taxa, total, saida, trans_dinheiro, trans_conta, tipo, dinheiro, pix, cartao, funcionario) VALUES(curDate(), @historico, @forma_pagto, @valor, @taxa, @total, @saida, @trans_dinheiro, @trans_conta, @tipo, @dinheiro, @pix, @cartao, @funcionario)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@historico", "Dinheiro p/ Conta");
                cmd.Parameters.AddWithValue("@forma_pagto", "Transferência");
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtParaConta.Text));
                cmd.Parameters.AddWithValue("@taxa", 0);
                cmd.Parameters.AddWithValue("@total", 0);
                cmd.Parameters.AddWithValue("@saida", 0);
                cmd.Parameters.AddWithValue("@trans_dinheiro", Convert.ToDouble(txtParaConta.Text));
                cmd.Parameters.AddWithValue("@trans_conta", 0);
                cmd.Parameters.AddWithValue("@tipo", "Transferência");
                cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtParaConta.Text) * -1);
                cmd.Parameters.AddWithValue("@pix", 0);
                cmd.Parameters.AddWithValue("@cartao", 0);
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
            }
            catch (Exception h)
            {
                MessageBox.Show("Feche o sistema e abra novamente...   " + h);
            }
            txtParaConta.Text = "0";
            LimparCampos();
            BuscarTipo();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {
                Listar();
            }
            MessageBox.Show("Transferido com sucesso.", "TRANSFERÊCIA");
        }

        private void btnConta_Click(object sender, EventArgs e)
        {
            if (txtParaCaixa.Text.ToString().Trim() == "" || txtParaCaixa.Text == "0" || txtParaCaixa.Text == "0,00")
            {
                MessageBox.Show("Não foi declarado o valor do caixa a ser transferido.", "TRANSFERÊCIA");
                txtParaCaixa.Focus();
                return;
            }
            try
            {
                con.AbrirConexao();
                sql = "INSERT INTO caixa(data, historico, forma_pagto, valor, taxa, total, saida, trans_dinheiro, trans_conta, tipo, dinheiro, pix, cartao, funcionario) VALUES(curDate(), @historico, @forma_pagto, @valor, @taxa, @total, @saida, @trans_dinheiro, @trans_conta, @tipo, @dinheiro, @pix, @cartao, @funcionario)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@historico", "Conta p/ Dinheiro");
                cmd.Parameters.AddWithValue("@forma_pagto", "Transferência");
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtParaConta.Text));
                cmd.Parameters.AddWithValue("@taxa", 0);
                cmd.Parameters.AddWithValue("@total", 0);
                cmd.Parameters.AddWithValue("@saida", 0);
                cmd.Parameters.AddWithValue("@trans_dinheiro", 0);
                cmd.Parameters.AddWithValue("@trans_conta", Convert.ToDouble(txtParaCaixa.Text));
                cmd.Parameters.AddWithValue("@tipo", "Transferência");
                cmd.Parameters.AddWithValue("@dinheiro",0);
                cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtParaCaixa.Text) * -1);
                cmd.Parameters.AddWithValue("@cartao", 0);
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
            }
            catch (Exception h)
            {
                MessageBox.Show("Feche o sistema e abra novamente...   " + h);//
            }
            txtParaCaixa.Text = "0";
            LimparCampos();
            BuscarTipo();
            if (Program.CargoUsuario == "Gerente" || Program.CargoUsuario == "Programadora")
            {
                Listar();
            }
            MessageBox.Show("Transferido com sucesso.", "TRANSFERÊCIA");
        }

        private void txtParaConta_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtParaConta);
        }

        private void txtParaConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtParaCaixa_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtParaCaixa);
        }

        private void txtParaCaixa_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtParaConta_Click(object sender, EventArgs e)
        {
            txtParaCaixa.Text = "0";
        }

        private void txtParaCaixa_Click(object sender, EventArgs e)
        {
            txtParaConta.Text = "0";
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarTipo();
        }

        private void dtInicial_ValueChanged(object sender, EventArgs e)
        {
            BuscarTipo();
        }

        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            BuscarTipo();
        }

        private void btnRel_Click(object sender, EventArgs e)
        {
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
