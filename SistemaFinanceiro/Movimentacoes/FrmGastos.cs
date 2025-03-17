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
    public partial class FrmGastos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string ultimoIdGasto;
        string favorecido;
        String FormaPagto;
        double Pc = 0;
        double Pp = 0;
        double Pd = 0; 

        public FrmGastos()
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
            grid.Columns[1].HeaderText = "Descrição";
            grid.Columns[2].HeaderText = "Favorecido";
            grid.Columns[3].HeaderText = "Valor";
            grid.Columns[4].HeaderText = "Forma Pagto";
            grid.Columns[5].HeaderText = "Funcionário";
            grid.Columns[6].HeaderText = "Data";
            grid.Columns[7].HeaderText = "N.Doc.:";
            grid.Columns[8].HeaderText = "Vencimento";

            grid.Columns[3].DefaultCellStyle.Format = "C2";
            grid.Columns[4].DefaultCellStyle.Format = "C2";           
            grid.Columns[0].Visible = false;
            
            Totalizando();
        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM gastos ORDER BY data desc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        private void HabilitarCampos()
        {
            txtDescricao.Enabled = true;
            txtFavorecido.Enabled = true;
            txtValor.Enabled = true;
            txtNota.Enabled = true;
            txtDescricao.Focus();
            cbPagto.Enabled = true;
            dtData.Enabled = true;
        }
        private void DesabilitarCampos()
        {
            txtDescricao.Enabled = false;
            txtFavorecido.Enabled = false;
            txtValor.Enabled = false;
            txtNota.Enabled = false;
            cbPagto.Enabled = false;
            dtData.Enabled = false;
        }
        private void LimparCampos()
        {
            txtDescricao.Text = "";
            txtFavorecido.Text = "";
            txtNota.Text = "";
            txtValor.Text = "";
            cbPagto.SelectedIndex = 0;            
            dtData.Value = DateTime.Today;
        }
        private void BuscarData()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM gastos WHERE data = @data ORDER BY data desc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dtBuscar.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        private void listarPeriodo()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM gastos WHERE data>=@dataInicial AND data<=@dataFinal ORDER BY data DESC";
            cmd = new MySqlCommand(sql, con.con);
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
        private void FrmGastos_Load(object sender, EventArgs e)
        {
            dtBuscar.Value = DateTime.Today;
            dtData.Value = DateTime.Today;
            cbPagto.SelectedIndex = 0;
            Listar();
            BuscarData();
            LoadTheme();
        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex > -1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvar.Enabled = false;
                HabilitarCampos();
              
                id = grid.CurrentRow.Cells[0].Value.ToString();
                txtDescricao.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtFavorecido.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtValor.Text = grid.CurrentRow.Cells[3].Value.ToString();
                cbPagto.Text = grid.CurrentRow.Cells[4].Value.ToString();
                txtNota.Text = grid.CurrentRow.Cells[7].Value.ToString();                
                dtData.Value = Convert.ToDateTime(grid.CurrentRow.Cells[8].Value.ToString());
               
            }
            else
            {
                return;
            }
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            HabilitarCampos();
            LimparCampos();
            Listar();
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher campo descrição !!!", "Cadastro de Gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtFavorecido.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher campo destino !!!", "Cadastro de Gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtValor.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher campo valor !!!", "Cadastro de Gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            con.AbrirConexao();
            sql = "INSERT INTO gastos(descricao, destino, valor, pagto, funcionario, data, nota, vencimento) VALUES(@descricao,  @destino, @valor, @pagto, @funcionario, curDate(), @nota, @vencimento)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@destino", txtFavorecido.Text);
            cmd.Parameters.AddWithValue("@valor", txtValor.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@pagto", FormaPagto);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@vencimento", Convert.ToDateTime(dtData.Text));
            cmd.ExecuteNonQuery();
            con.FecharConexao();
                      
            con.AbrirConexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            cmdVerificar = new MySqlCommand("SELECT id FROM gastos ORDER BY id DESC LIMIT 1", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {
               
                while (reader.Read())
                {
                    ultimoIdGasto = Convert.ToString(reader["id"]);
                }
            }
            con.FecharConexao();
           
            con.AbrirConexao();
            sql = "INSERT INTO lancar_movimentacoes(cliente, tipo, movimento, descricao, valor, desconto, taxa, valor_total, cartao, pix, dinheiro, nota, valor_pago, funcionario, id_movimento, data) VALUES(@cliente, @tipo, @movimento, @descricao, @valor, @desconto, @taxa, @valor_total, @cartao, @pix, @dinheiro, @nota, @valor_pago, @funcionario, @id_movimento, @data)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", txtFavorecido.Text);
            cmd.Parameters.AddWithValue("@tipo", "Saída");
            cmd.Parameters.AddWithValue("@movimento", "Gastos");
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);//implementado p/ exibir na mov. 
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text) * -1);
            cmd.Parameters.AddWithValue("@desconto", 0);
            cmd.Parameters.AddWithValue("@taxa", 0);
            cmd.Parameters.AddWithValue("@valor_total", Convert.ToDouble(txtValor.Text) * -1);
            cmd.Parameters.AddWithValue("@cartao", Pc);
            cmd.Parameters.AddWithValue("@pix", Pp);
            cmd.Parameters.AddWithValue("@dinheiro", Pd);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@valor_pago", Convert.ToDouble(txtValor.Text) * -1); 
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@id_movimento", ultimoIdGasto);
            cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dtData.Text)); 
            cmd.ExecuteNonQuery();
            con.FecharConexao();
            
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
            Listar();
            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro Gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo descrição", "Cadastro gatos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Text = "";
                txtDescricao.Focus();
                return;
            }
            if (txtFavorecido.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo destino", "Cadastro gatos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Text = "";
                txtDescricao.Focus();
                return;
            }
            if (txtValor.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo valor", "Cadastro gastos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Text = "";
                txtValor.Focus();
                return;
            }
            FormaPagto = cbPagto.Text;
            if (cbPagto.Text == "Dinheiro")
            {
                Pd = Convert.ToDouble(txtValor.Text) * -1;
                Pp = 0;
                Pc = 0;
            }
            else if (cbPagto.Text == "Pix")
            {
                Pp = Convert.ToDouble(txtValor.Text) * -1;
                Pd = 0;
                Pc = 0;
            }
            else if (cbPagto.Text == "Cartão")
            {
                Pc = Convert.ToDouble(txtValor.Text) * -1;
                Pd = 0;
                Pp = 0;
            }
           
            con.AbrirConexao();
            sql = "UPDATE gastos SET descricao=@descricao, destino=@destino, valor=@valor, pagto=@pagto, funcionario=@funcionario, data=curDate(), nota=@nota, vencimento=@vencimento where id=@id";
            cmd = new MySqlCommand(sql, con.con); 
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@destino", txtFavorecido.Text);
            cmd.Parameters.AddWithValue("@valor", txtValor.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@pagto", FormaPagto);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@vencimento", Convert.ToDateTime(dtData.Text));
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.FecharConexao();
                       
            con.AbrirConexao();
            sql = "UPDATE lancar_movimentacoes SET cliente=@cliente, descricao = @descricao, valor=@valor, valor_total=@valor_total, cartao=@cartao, pix=@pix, dinheiro=@dinheiro, nota=@nota, valor_pago=@valor_pago, funcionario = @funcionario, data = curDate(), nota = @nota WHERE id_movimento = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", txtFavorecido.Text);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text) * -1);
            cmd.Parameters.AddWithValue("@valor_total", Convert.ToDouble(txtValor.Text) * -1);
            cmd.Parameters.AddWithValue("@cartao", Pc);
            cmd.Parameters.AddWithValue("@pix", Pp);
            cmd.Parameters.AddWithValue("@dinheiro", Pd);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@valor_pago", Convert.ToDouble(txtValor.Text) * -1);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dtData.Text));
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.FecharConexao();
            
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
            Listar();
            MessageBox.Show("Registro Editado com sucesso!", "Cadastro gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro!", "Cadastro gastos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {               
                con.AbrirConexao();
                sql = "DELETE FROM gastos WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();                
                              
                con.AbrirConexao();
                sql = "DELETE FROM lancar_movimentacoes WHERE id_movimento = @id AND movimento = @movimento";                                                                                                       
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@movimento", "Gastos");
               
                cmd.ExecuteNonQuery();
                con.FecharConexao();
               
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                DesabilitarCampos();
                LimparCampos();
                Listar();
                MessageBox.Show("Registro Excluído com sucesso!", "Cadastro gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void dtBuscar_ValueChanged(object sender, EventArgs e)
        {
            BuscarData();
        }
        private void Totalizando()
        {
            double total = 0;
            
            foreach (DataGridViewRow row in grid.Rows)
            {               
                total += Convert.ToDouble(row.Cells["valor"].Value);
            }
            lblTotal.Text = Convert.ToDouble(total).ToString("C2");
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

        private void txtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtValor);
            Pd = Convert.ToDouble(txtValor.Text) * -1;
        }

        private void cbPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormaPagto = cbPagto.Text;
            if (cbPagto.Text == "Dinheiro")
            {
                Pd = Convert.ToDouble(txtValor.Text) * -1;
                Pp = 0;
                Pc = 0;
            }
            else if (cbPagto.Text == "Pix")
            {
                Pp = Convert.ToDouble(txtValor.Text) * -1;
                Pd = 0;
                Pc = 0;               
            }
            else if (cbPagto.Text == "Cartão")
            {
                Pc = Convert.ToDouble(txtValor.Text) * -1;
                Pd = 0;
                Pp = 0;               
            }
        }

        private void dtInicial_ValueChanged(object sender, EventArgs e)
        {
            listarPeriodo();
        }

        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            listarPeriodo();
        }
    }
}
