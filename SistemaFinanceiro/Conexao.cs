using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaFinanceiro
{
    class Conexao
    {
        //Servidor local
        public string conec = "User Id=root;Password=Sql2025;Host=localhost;Database=db_controlefinanceiro;Persist Security Info=True";

        public MySqlConnection con = null;
        
        public void AbrirConexao()
        {            
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Erro de conexão com o Banco de Dados: " + ex.Message);
            }
        }
        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
                con.Dispose();
                con.ClearAllPoolsAsync();
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Erro de conexão com o Banco de Dados: " + ex.Message); 
            }
        }
       
    }
}
