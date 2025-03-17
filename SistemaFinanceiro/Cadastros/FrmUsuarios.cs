using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaFinanceiro.Cadastros
{
    public partial class FrmUsuarios : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string id;
        string usuarioAntigo;
        string Cargo;

        public FrmUsuarios()
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
            grid.Columns[1].HeaderText = "Funcionário";
            grid.Columns[2].HeaderText = "Cargo";
            grid.Columns[3].HeaderText = "Usuário";
            grid.Columns[4].HeaderText = "Senha";
            grid.Columns[5].HeaderText = "Data";
            
            grid.Columns[0].Visible = false;

        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM usuarios ORDER BY nome asc";
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
            sql = "SELECT * FROM usuarios WHERE nome LIKE @nome ORDER BY nome asc";
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
        
        private void CarregarFuncionarios()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM funcionarios ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbFuncionario.DataSource = dt;           
            cbFuncionario.DisplayMember = "nome";
            con.FecharConexao();
        }
        
        private void HabilitarCampos()
        {
            txtSenha.Enabled = true;
            txtUsuario.Enabled = true;
            cbFuncionario.Enabled = true;
            txtCargo.Enabled = true;
            txtUsuario.Focus();
        }
        private void DesabilitarCampos()
        {
            txtSenha.Enabled = false;
            txtUsuario.Enabled = false;
            cbFuncionario.Enabled = false;
            txtCargo.Enabled = false;
            
        }
        private void LimparCampos()
        {
            txtUsuario.Text = "";
            txtSenha.Text = "";
            txtBuscarNome.Text = "";
            txtCargo.Text = "Selecione";
            cbFuncionario.Text = "Selecione";
            cbFuncionario.Focus();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            Listar();
            LimparCampos();            
            CarregarFuncionarios();
            LoadTheme();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (cbFuncionario.Text == "")
            {
                var res = MessageBox.Show("Não há funcionário cadastrado!!! Deseja cadastrar?", "Cadastro de Usuários", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Cadastros.FrmFuncionarios frm = new FrmFuncionarios();
                    frm.ShowDialog();
                    HabilitarCampos();
                    btnSalvar.Enabled = true;
                    btnNovo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;                    
                    CarregarFuncionarios();
                    Listar();
                }
                else { this.Close(); }
            }
            if (txtCargo.Text == "Selecione")
            {
                var res = MessageBox.Show("Não há cargo cadastrado!!! Deseja cadastrar?", "Cadastro de Usuários", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Cadastros.FrmCargos frm = new FrmCargos();
                    frm.ShowDialog();
                    HabilitarCampos();
                    btnSalvar.Enabled = true;
                    btnNovo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                   
                    CarregarFuncionarios();
                    Listar();
                }
                else { this.Close(); }
            }
            
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
            if (txtUsuario.Text.Trim() == "")
            {
                MessageBox.Show("Preencher campo Usuário !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Preencher campo Senha !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbFuncionario.Text == "Selecione" || cbFuncionario.Text.Trim()=="")
            {
                MessageBox.Show("Preencher selecionar um Funcionário !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCargo.Text.Trim() == "" || txtCargo.Text == "Selecione")
            {
                MessageBox.Show("Preencher selecionar um Cargo !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            con.AbrirConexao();
            sql = "INSERT INTO usuarios(nome, cargo, usuario, senha, data) VALUES(@nome, @cargo, @usuario, @senha, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", cbFuncionario.Text);
            cmd.Parameters.AddWithValue("@cargo", txtCargo.Text);
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);

              
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Usuário já cadastrado", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUsuario.Text = "";
                txtUsuario.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();

            LimparCampos();
            Listar();

            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            DesabilitarCampos();
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cbFuncionario.Text == "Selecione" || cbFuncionario.Text.Trim() == "")
            {
                MessageBox.Show("Preencher selecionar um Funcionário !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCargo.Text.Trim() == "" || txtCargo.Text == "Selecione")
            {
                MessageBox.Show("Preencher selecionar um Cargo !!", "Cadastro de Usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUsuario.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo Usuário", "Cadastro usuários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Text = "";
                txtUsuario.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo senha", "Cadastro usuários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Text = "";
                txtSenha.Focus();
                return;
            }

            
            con.AbrirConexao();
            sql = "UPDATE usuarios SET nome = @nome, cargo = @cargo, usuario = @usuario, senha = @senha where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", cbFuncionario.Text);
            cmd.Parameters.AddWithValue("@cargo", txtCargo.Text);
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);

            
            if (txtUsuario.Text != usuarioAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Usuário já registrado", "Cadastro de usuários", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    return;
                }
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();

            LimparCampos();
            Listar();

            MessageBox.Show("Registro Editado com sucesso!", "Cadastro funcionários", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            DesabilitarCampos();
            
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();        
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
                cbFuncionario.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtCargo.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtUsuario.Text = grid.CurrentRow.Cells[3].Value.ToString();
                txtSenha.Text = grid.CurrentRow.Cells[4].Value.ToString();

                usuarioAntigo = grid.CurrentRow.Cells[3].Value.ToString();
            }
            else
            {
                return;
            }
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro!", "Cadastro usuários", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                
                con.AbrirConexao();
                sql = "DELETE FROM usuarios WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                MessageBox.Show("Registro Excluído com sucesso!", "Cadastro usuários", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                cbFuncionario.Text = "";
                cbFuncionario.Enabled = false;

                LimparCampos();
                Listar();
            }

        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {            

            MySqlCommand cmdVerificar;
            MySqlDataReader reader; 
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM funcionarios WHERE nome = @nome", con.con);
            cmdVerificar.Parameters.AddWithValue("@nome", cbFuncionario.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {                
                while (reader.Read())
                {
                    Cargo = Convert.ToString(reader["cargo"]);//                    
                }
                txtCargo.Text = Cargo;
            }
            con.FecharConexao();
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFuncionario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
