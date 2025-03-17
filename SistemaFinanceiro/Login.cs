using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace SistemaFinanceiro
{
    public partial class FrmLogin : Form
    {
        Conexao con = new Conexao();

        Int32 ano;
        DateTime data = DateTime.Now;


        public FrmLogin()
        {
            InitializeComponent();
        }
        
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);        

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "USUÁRIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
                lblMensagemErro.Visible = false;
                imgErro.Visible = false;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUÁRIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }
        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == "SENHA")
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = Color.LightGray;
                txtSenha.UseSystemPasswordChar = true;
                lblMensagemErro.Visible = false;
                imgErro.Visible = false;
            }
        }
        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "SENHA";
                txtSenha.ForeColor = Color.DimGray;
                txtSenha.UseSystemPasswordChar = false;
            }
        }
        private void verificado()
        {
            txtUsuario.Text = "admin";
            txtUsuario.ForeColor = Color.DimGray;
            txtSenha.Text = "123";
            txtSenha.ForeColor = Color.DimGray;
            txtSenha.UseSystemPasswordChar = false;
        }
        private void chamarLogin()
        {

            if (txtUsuario.Text.ToString().Trim() == "" || txtUsuario.Text == "admin")
            {
                erroLogin("Usuário ou senha inválida !!!");
                verificado();                
            }
            if (txtSenha.Text.ToString().Trim() == "" || txtSenha.Text == "123")
            {
                erroLogin("Usuário ou senha inválida.");
                verificado();
            }
            else { erroLogin("Verifique usúario e senha."); }

            try
            {
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader; 
                cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario AND  senha = @senha", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
                reader = cmdVerificar.ExecuteReader();
                if (reader.HasRows)
                {                    
                    while (reader.Read())
                    {
                        Program.NomeUsuario = Convert.ToString(reader["nome"]);
                        Program.CargoUsuario = Convert.ToString(Convert.ToString(reader["cargo"]));
                    }
                    lblMensagemErro.Visible = false;
                    imgErro.Visible = false;
                    verificado();
                    Entrar();                   
                }
                con.FecharConexao();
            }
            catch (Exception m)
            {
                MessageBox.Show("O banco de dados apresentou um erro. Entre em contato com o Administrador do sitema: "+ m);
            } 
        }

        private void Entrar()
        {            
            FormMainMenu frm = new FormMainMenu();
            frm.ShowDialog();
            frm.FormClosed += encerraSecao;
            this.Hide();
        }

        private void erroLogin(string msn)
        {
            lblMensagemErro.Text = msn;
            lblMensagemErro.Visible = true;
            imgErro.Visible = true;
            
        }
        private void encerraSecao(object sender, FormClosedEventArgs e)
        {
            verificado();
            lblMensagemErro.Visible = false;
            imgErro.Visible = false;
            this.Show();
        }        
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {            
            chamarLogin();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chamarLogin();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
