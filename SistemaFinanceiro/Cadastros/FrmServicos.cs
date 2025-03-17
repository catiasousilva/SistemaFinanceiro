using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaFinanceiro.Cadastros
{
    public partial class FrmServicos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string id;
        public FrmServicos()
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
            grid.Columns[1].HeaderText = "Serviço";
            grid.Columns[2].HeaderText = "Valor";
            grid.Columns[3].HeaderText = "Ativado";
            grid.Columns[2].DefaultCellStyle.Format = "C2";    
            grid.Columns[0].Visible = false;
        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM servicos ORDER BY nome asc";
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
            txtNome.Enabled = true;
            txtValor.Enabled = true;            
            txtNome.Focus();
        }
        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtValor.Enabled = false;
            cbAtivo.Enabled = false;
        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtValor.Text = "";
            cbTipo.SelectedIndex = 0;
        }

        private void FrmServicos_Load(object sender, EventArgs e)
        {
            Listar();
            cbTipo.SelectedIndex = 0;
            LoadTheme();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            cbAtivo.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            HabilitarCampos();
            LimparCampos();
            Listar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher campo nome do serviço !!", "Cadastro de Serviço", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtValor.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencher campo valor !!", "Cadastro de Serviço", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
            con.AbrirConexao();
            sql = "INSERT INTO servicos(nome, valor, ativo) VALUES(@nome, @valor, @ativo)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@valor", txtValor.Text.Replace(",", "."));
            if (cbAtivo.Checked == true)
            {
                cmd.Parameters.AddWithValue("@ativo", "Sim");
            }
            if (cbAtivo.Checked == false)
            {
                cmd.Parameters.AddWithValue("@ativo", "Não");
            }
            cmd.ExecuteNonQuery();
            con.FecharConexao();
            btnNovo.Enabled = true;
            cbAtivo.Enabled = false;
            btnSalvar.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
            Listar();
            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro Serviço", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo nome do serviço", "Cadastro serviço", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtValor.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo valor do serviço", "Cadastro serviço", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Text = "";
                txtValor.Focus();
                return;
            }

            
            con.AbrirConexao();
            sql = "UPDATE servicos SET nome = @nome, valor = @valor, ativo = @ativo WHERE id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@valor", txtValor.Text.Replace(",","."));
            if (cbAtivo.Checked == true)
            {
                cmd.Parameters.AddWithValue("@ativo", "Sim");
            }
            if (cbAtivo.Checked == false)
            {
                cmd.Parameters.AddWithValue("@ativo", "Não");
            }
            cmd.ExecuteNonQuery();
            con.FecharConexao();           

            btnNovo.Enabled = true;
            cbAtivo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
            Listar();
            MessageBox.Show("Registro Editado com sucesso!", "Cadastro serviço", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                cbAtivo.Enabled = true;
                btnSalvar.Enabled = false;
                HabilitarCampos();

                id = grid.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtValor.Text = grid.CurrentRow.Cells[2].Value.ToString();
           
                if (grid.CurrentRow.Cells[3].Value.ToString() == "Sim")
                {
                    cbAtivo.Checked = true;
                }
                else
                {
                    cbAtivo.Checked = false;
                }
            }
            else
            {
                return;
            }
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro!", "Cadastro serviço", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {               
                con.AbrirConexao();
                sql = "DELETE FROM servicos WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                txtNome.Text = "";
                txtValor.Text = "";
                LimparCampos();
                DesabilitarCampos();
                Listar();
                MessageBox.Show("Registro Excluído com sucesso!", "Cadastro serviço", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
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

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtValor);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipo.SelectedIndex == 0)
            {
                Listar();
            }
            else
            {
                Buscartipo();
            }
        }        
        private void Buscartipo()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM servicos WHERE ativo = @ativo ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@ativo", cbTipo.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();

            FormatarGD();

        }
    }
}
