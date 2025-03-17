using System;
using System.Windows.Forms;

namespace SistemaFinanceiro
{
    static class Program
    {
        //declaracao de variaveis
        public static string NomeUsuario;
        public static string CargoUsuario;

        public static string ChamadaProdutos;
        public static string NomeProduto;
        public static string EstoqueProduto;
        public static string EstoqueMinimo;
        public static string ValorProduto;//
        public static string modLucro;//p/ alteracao de produto
        public static bool DesabilitarTxt;

        public static string IdProduto;
        public static string idVenda;
        public static string chamadaHospede;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());            
        }
    }
}
