using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaFinanceiro.Cadastros
{
    public partial class FrmAnoVigente : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string AnoAntigo;
        Int32 Ano, anoAnterior, anoSuperior;

        public FrmAnoVigente()
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
            gridAno.Columns[0].HeaderText = "CÓDIGO";
            gridAno.Columns[1].HeaderText = "ANO";
            gridAno.Columns[2].HeaderText = "DATA";

        }
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM ano ORDER BY ano ASC";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridAno.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }
        private void FrmAnoAtual_Load(object sender, EventArgs e)
        {
            LoadTheme();
            Listar();
            Ano = Convert.ToInt32(DateTime.Now.Year);            
            anoSuperior = Ano + 2;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {            
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            txtAno.Enabled = true;
            txtAno.Focus();
        }
        
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                btnEditar.Enabled = true;
                btnSalvar.Enabled = false;
                txtAno.Enabled = true;
                AnoAntigo = gridAno.CurrentRow.Cells[1].Value.ToString();
                id = gridAno.CurrentRow.Cells[0].Value.ToString();
                txtAno.Text = gridAno.CurrentRow.Cells[1].Value.ToString();
            }
            else 
            {
                return;
            }
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtAno.Text.Trim() == "")
            {
                MessageBox.Show("Preencher campo ano", "Cadastro de Ano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToInt32(txtAno.Text) < Ano)
            {
                MessageBox.Show("Ops! Este ano inferior da virgência", "Cadastro de Ano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAno.Text = "";
                txtAno.Focus();
                return;
            }
            if (Convert.ToInt32(txtAno.Text) > anoSuperior)
            {
                MessageBox.Show("Ano muito além do ano atual", "Cadastro de Ano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAno.Text = "";
                txtAno.Focus();
                return;
            }

            
            con.AbrirConexao();
            sql = "INSERT INTO ano(ano, data) VALUES(@ano, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@ano", txtAno.Text);

              
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM ano WHERE ano = @ano", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            cmdVerificar.Parameters.AddWithValue("@ano", txtAno.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Ano já cadastrado", "Cadastro de ano", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAno.Text = "";
                txtAno.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();            
            Listar();
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
            txtAno.Text = "";
            txtAno.Enabled = false;
            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtAno.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo ano", "Cadastro usuários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAno.Text = "";
                txtAno.Focus();
                return;
            }
            if (Ano <= anoAnterior)
            {
                MessageBox.Show("Ops! você está tentando cadastrar um ano fora de vigencia, tente cadastrar " + Ano + " ou acima", "Cadastro de Ano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Ano > anoSuperior)
            {
                MessageBox.Show("Ops! você está tentando cadastrar um ano muito além do ano atual, tente cadastrar até " + anoSuperior, "Cadastro de Ano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            con.AbrirConexao();
            sql = "UPDATE ano SET ano = @ano where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ano", txtAno.Text);

            
            if (txtAno.Text != AnoAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM ano WHERE ano = @ano", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@ano", txtAno.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Ano já registrado", "Cadastro de ano", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAno.Text = "";
                    txtAno.Focus();
                    return;
                }
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();

            txtAno.Text = "";
            txtAno.Enabled = false;
            Listar();
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
            
            MessageBox.Show("Registro Editado com sucesso!", "Cadastro funcionários", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }
    }
}
