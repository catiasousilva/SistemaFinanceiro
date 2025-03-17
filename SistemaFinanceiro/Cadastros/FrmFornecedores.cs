using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaFinanceiro.Cadastros
{
    public partial class FrmFornecedores : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string id;
        string cnpjAntigo;
        bool cnpj = false;

        public FrmFornecedores()
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
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "CNPJ";
            grid.Columns[3].HeaderText = "Endereço";
            grid.Columns[4].HeaderText = "Telefone";
            grid.Columns[5].HeaderText = "Vendedor";            

            grid.Columns[0].Visible = false;
        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();

            FormatarGD();
        }
        private void BuscarNome()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores WHERE nome LIKE @nome ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();

            FormatarGD();
        }
        private void BuscarCnpj()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores WHERE cnpj = @cnpj ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cnpj", txtBuscarCnpj.Text);
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
            txtCnpj.Enabled = true;
            txtEndereco.Enabled = true;
            txtTelefone.Enabled = true;
            txtVendedor.Enabled = true;
            txtNome.Focus();
        }
        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtCnpj.Enabled = false;
            txtEndereco.Enabled = false;
            txtTelefone.Enabled = false;
            txtVendedor.Enabled = false;

        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCnpj.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtVendedor.Text = "";
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
            rbNome.Checked = true;
            Listar();
            LoadTheme();
        }

        private void rbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscarNome.Visible = true;
            txtBuscarCnpj.Visible = false;
            txtBuscarCnpj.Text = "";
            txtBuscarNome.Text = "";
        }

        private void rbCnpj_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscarNome.Visible = false;
            txtBuscarCnpj.Visible = true;
            txtBuscarCnpj.Text = "";
            txtBuscarNome.Text = "";
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            cnpj = true;
            HabilitarCampos();
            LimparCampos();
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            Listar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo nome", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCnpj.Text == "  .   .   /    -" || txtCnpj.Text.Length < 18)
            {
                MessageBox.Show("Preencha o campo CNPJ", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCnpj.Focus();
                return;
            }
                        
            con.AbrirConexao();
            sql = "INSERT INTO fornecedores(nome, cnpj, endereco, telefone, vendedor) VALUES(@nome, @cnpj, @endereco, @telefone, @vendedor)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("@vendedor", txtVendedor.Text);
                       
            Verificar();

            cmd.ExecuteNonQuery();
            con.FecharConexao();

            Listar();

            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
            cnpj = false;
        }
        private void Verificar()
        {
              
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM fornecedores WHERE cnpj = @cnpj", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            cmdVerificar.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("CPF já registrado", "Cadastro de fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCnpj.Text = "";
                txtCnpj.Focus();
                return;
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvar.Enabled = false;
                HabilitarCampos();

                cnpjAntigo = grid.CurrentRow.Cells[2].Value.ToString();
                id = grid.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtCnpj.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtEndereco.Text = grid.CurrentRow.Cells[3].Value.ToString();
                txtTelefone.Text = grid.CurrentRow.Cells[4].Value.ToString();
                txtVendedor.Text = grid.CurrentRow.Cells[5].Value.ToString();
            }
            else
            {
                return;
            }
            

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo nome", "Cadastro funcionários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCnpj.Text == "  .   .   /    -" || txtCnpj.Text.Length < 18)
            {
                MessageBox.Show("Preencha o campo CPF", "Cadastro funcionários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCnpj.Focus();
                return;
            }

            
            con.AbrirConexao();
            sql = "UPDATE fornecedores SET nome = @nome, cnpj = @cnpj, endereco = @endereco, telefone = @telefone where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

            
            if (txtCnpj.Text != cnpjAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM fornecedores WHERE cnpj = @cnpj", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("CPF já registrado", "Cadastro de fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCnpj.Text = "";
                    txtCnpj.Focus();
                    return;
                }

            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();
            Listar();

            MessageBox.Show("Registro Editado com sucesso!", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            DesabilitarCampos();
            LimparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro!", "Cadastro fornecedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                
                con.AbrirConexao();
                sql = "DELETE FROM fornecedores WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;                
                DesabilitarCampos();
                LimparCampos();
                Listar();
                MessageBox.Show("Registro Excluído com sucesso!", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void txtBuscarCnpj_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarCnpj.Text == "  .   .   /    -")
            {
                Listar();
            }
            else { BuscarCnpj(); }
        }

        private void txtCnpj_Leave(object sender, EventArgs e)
        {
            if (cnpj == true)
            {
                Verificar();
            }
            
        }
    }
}
