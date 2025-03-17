using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFinanceiro.Movimentacoes
{
    public partial class FrmHistorico : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        double totalEntradas, totalApagar, montante, restante, valorDescontado, troco, Pagar;
        double dinheiroQ = 0;
        double pixQ = 0;
        double cartaoQ = 0;
        string id, cliente, valor, desconto, taxa, dinheiro, pix, cartao, funcionario, valor_total, entrada, pago, apagar, nota, data, status;
        
        
        private void FrmHistorico_Activated(object sender, EventArgs e)
        {
            DiaMesAno();
            LimparSelecao();
            ListarData();
        }              

        private void btnRel_Click(object sender, EventArgs e)
        {
            Relatorios.FrmRecibo frm = new Relatorios.FrmRecibo();
            frm.ShowDialog();
            LimparSelecao();
            ListarData();
        }

        public FrmHistorico()
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
            //
        }
        private void FrmHistorico_Load(object sender, EventArgs e)
        {
            LoadTheme();
            cbStatus.SelectedIndex = 0;
            panel3.Location = new Point(0, 54);
            grid.Location = new Point(0, 89);
            panelGrid.Visible = false;
            DiaMesAno();
            LimparSelecao();
            ListarData();         

        }       

        private void FormatarGD()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Cliente";
            grid.Columns[2].HeaderText = "Valor";
            grid.Columns[3].HeaderText = "Desconto";            
            grid.Columns[4].HeaderText = "Taxa";
            grid.Columns[5].HeaderText = "V. Total";
            grid.Columns[6].HeaderText = "Entrada";
            grid.Columns[7].HeaderText = "Pago";
            grid.Columns[8].HeaderText = "Á pagar";
            grid.Columns[9].HeaderText = "Pix";
            grid.Columns[10].HeaderText = "Dinheiro";
            grid.Columns[11].HeaderText = "Cartão";
            grid.Columns[12].HeaderText = "Doc.:";
            grid.Columns[13].HeaderText = "Funcionário";
            grid.Columns[14].HeaderText = "Data";
            grid.Columns[15].HeaderText = "Status";
            grid.Columns[2].DefaultCellStyle.Format = "c2";
            grid.Columns[3].DefaultCellStyle.Format = "c2"; 
            grid.Columns[4].DefaultCellStyle.Format = "c2";
            grid.Columns[5].DefaultCellStyle.Format = "c2";
            grid.Columns[6].DefaultCellStyle.Format = "c2";
            grid.Columns[7].DefaultCellStyle.Format = "c2";
            grid.Columns[8].DefaultCellStyle.Format = "c2";
            grid.Columns[9].DefaultCellStyle.Format = "c2";
            grid.Columns[10].DefaultCellStyle.Format = "c2";
            grid.Columns[11].DefaultCellStyle.Format = "c2";
          
            grid.Columns[0].Visible = false;
            grid.Columns[7].Visible = false;
           
            TotalizandoAberto();
            TotalizandoValorPorCliente();
        }
        private void ListarTodas()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM lancar_vendas ORDER BY data ASC";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGD();
        }        

        private void ListarData()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM lancar_vendas WHERE data>=@dataInicial AND data<=@dataFinal AND status=@status ORDER BY data DESC";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@dataInicial", Convert.ToDateTime(dtInical.Text));
            cmd.Parameters.AddWithValue("@dataFinal", Convert.ToDateTime(dtFinal.Text));
            cmd.Parameters.AddWithValue("@status", cbStatus.Text);
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
            sql = "SELECT * FROM lancar_vendas WHERE cliente LIKE @cliente ORDER BY cliente asc";
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

        private void DiaMesAno()
        {
            
            DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtInical.Text = primeiroDia.ToString();
           
            DateTime dtUltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            dtFinal.Text = dtUltimoDiaMes.ToString();
        }

        private void limparDados()
        {
            lblCliente.Text = "...";
            lblNota.Text = "0";
            lblStatus.Text = "...";
            lblMontante.Text = "0";
            lblValor.Text = "0";
            lblDesconto.Text = "0";
            lblTaxa.Text = "0";
            lblEntrada.Text = "0";
            lbltotal.Text = "0";
            txtDesconto.Text = "0";
            txtTaxa.Text = "0";
            txtDinheiro.Text="0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            lblTroco.Text = "0";
            lblRestante.Text = "0";
            restante = 0;
            dinheiroQ = 0;
            pixQ = 0;
            cartaoQ = 0;

            id = "";
            cliente = "Selecione o cliente";
            valor = "0";
            desconto = "0";
            taxa = "0";
            valor_total ="0";
            entrada = "0";
            pago = "0";
            apagar = "0";
            pix = "0";
            dinheiro = "0";
            cartao = "0";
            nota = "0";
            funcionario = "";
            data = "";
            status = "...";
            btnQuitar.Enabled = false;
        }
        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDesconto);
            var l = Double.Parse(apagar, System.Globalization.NumberStyles.Currency);
            var d = Double.Parse(txtDesconto.Text, System.Globalization.NumberStyles.Currency);//Convert.ToDouble(txtDesconto.Text);
            valorDescontado = d;
            var valorDesconto = l - d;
            lblApagar.Text = String.Format("{0:C2}", valorDesconto);
            lblRestante.Text= String.Format("{0:C2}", valorDesconto);
        }
        private void txtTaxa_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTaxa);
            var vp = Double.Parse(apagar, System.Globalization.NumberStyles.Currency);
            var t = Double.Parse(txtTaxa.Text, System.Globalization.NumberStyles.Currency);
            var taxaPagar = vp + t;
            lblApagar.Text = String.Format("{0:C2}", taxaPagar);
            lblRestante.Text = String.Format("{0:C2}", taxaPagar);
        }
        
        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtTaxa_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }        

        private void txtDinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDinheiro.Focus();
            }
        }

        private void txtTaxa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDinheiro.Focus();
            }
        }

        private void txtDinheiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPix.Focus();
            }
        }

        private void txtDesconto_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = "0";
            txtTaxa.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            lblApagar.Text = string.Format("{0:c2}", totalApagar);
            lblRestante.Text = string.Format("{0:c2}", totalApagar);
            lblTroco.Text = string.Format("{0:c2}", 0);
        }

        private void txtTaxa_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = "0";
            txtTaxa.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            lblApagar.Text = string.Format("{0:c2}", totalApagar);
            lblRestante.Text = string.Format("{0:c2}", totalApagar);
            lblTroco.Text = string.Format("{0:c2}", 0);
        }

        private void txtDinheiro_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDinheiro);
            dinheiroQ = Convert.ToDouble(txtDinheiro.Text);
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                lblTroco.Text = "0";
                lblRestante.Text = string.Format("{0:c2}", lblApagar.Text);
                return;
            }
            Troco();            
           
            restante = Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency);
            if (restante < 0) 
            {
                lblRestante.Text = string.Format("{0:c2}", restante);
            }
            else
            {
                lblRestante.Text = string.Format("{0:c2}", 0);
            }
          
            if (Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency) < 0)
            {
                lblTroco.Text = "0";
            }
        }
        private void txtPix_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtPix);
            pixQ = dinheiroQ + Convert.ToDouble(txtPix.Text);
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                if (txtPix.Text.Trim() == "0,00" || txtPix.Text.Trim() == "0")
                {
                    lblTroco.Text = "0";
                    lblRestante.Text = string.Format("{0:c2}", lblApagar.Text);
                    return;
                }
            }
            Troco();
            
            restante = Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency);
            if (restante < 0)
            {
                lblRestante.Text = string.Format("{0:c2}", restante);
            }
            else
            {
                lblRestante.Text = string.Format("{0:c2}", 0);
            }
           
            if (Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency) < 0)
            {
                lblTroco.Text = "0";
            }
        }

        private void txtCartao_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtCartao);            
            cartaoQ = dinheiroQ + pixQ + Convert.ToDouble(txtCartao.Text);
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                if (txtPix.Text.Trim() == "0,00" || txtPix.Text.Trim() == "0")
                {
                    if (txtCartao.Text.Trim() == "0,00" || txtCartao.Text.Trim() == "0")
                    {
                        lblTroco.Text = "0";
                        lblRestante.Text = string.Format("{0:c2}", lblApagar.Text);
                        return;
                    }
                }
            }
            Troco();
           
            restante = Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency);
            if (restante < 0)
            {
                lblRestante.Text = string.Format("{0:c2}", restante);
            }
            else
            {
                lblRestante.Text = string.Format("{0:c2}", 0);
            }
            
            if (Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency) < 0)
            {
                lblTroco.Text = "0";
            }
        }

        private void FrmHistorico_FormClosing(object sender, FormClosingEventArgs e)
        {
            limparDados();
            OcultarPanel();
            LimparSelecao();
            ListarData();
            cbStatus.SelectedIndex = 0;
            grid.Enabled = true;
        }

        private void txtPix_Click(object sender, EventArgs e)
        {
            txtPix.Text = "0";
            txtCartao.Text = "0";
            pixQ = 0;
            cartaoQ = 0;
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                lblTroco.Text = "0";
                restante = 0;
                lblRestante.Text = string.Format("{0:c2}", 0);
                return;
            }
        }

        private void txtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }        

        private void txtCartao_Click(object sender, EventArgs e)
        {
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                if (txtPix.Text.Trim() == "0,00" || txtPix.Text.Trim() == "0")
                {
                    lblTroco.Text = "0";
                    return;
                }                
            }
        }

        private void txtPix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCartao.Focus();
            }
        }

        private void txtCartao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuitar.Focus();
            }
        }

        private void txtPix_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtCartao_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }        

        private void txtDinheiro_Click(object sender, EventArgs e)
        {
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            dinheiroQ = 0;
            pixQ = 0;
            cartaoQ = 0;
            lblRestante.Text = string.Format("{0:c2}", 0);
            restante = 0;
            if (txtDinheiro.Text.Trim() == "0,00" || txtDinheiro.Text.Trim() == "0")
            {
                lblTroco.Text = "0";
                return;
            }
        }

        private void OcultarPanel()
        {
            panel3.Location = new Point(0, 53);
            grid.Location = new Point(0, 89);
            panelGrid.Visible = false;
        }
        private void dtInical_ValueChanged(object sender, EventArgs e)
        {
            OcultarPanel();
            LimparSelecao();
            ListarData();
        }
        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            OcultarPanel();
            LimparSelecao();
            ListarData();
        }
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarPanel();
            LimparSelecao();
            ListarData();
        }
        private void txtNome_TextChanged(object sender, EventArgs e)
        {            
            ListarNome();
        }
        private void btnListarTodas_Click(object sender, EventArgs e)
        {
            OcultarPanel();
            LimparSelecao();
            cbStatus.SelectedIndex = 0;
            ListarTodas();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparDados();
            OcultarPanel();
            LimparSelecao();
            ListarData();
            cbStatus.SelectedIndex = 0;
            grid.ClearSelection();
            grid.Enabled = true;
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Quitado")
            {
                btnQuitar.Enabled = false;
                txtDinheiro.Text = "0";
                txtPix.Text = "0";
                txtCartao.Text = "0";
                dinheiroQ = 0;
                pixQ = 0;
                cartaoQ = 0;
                MessageBox.Show("Movimentação já encontra-se quitada", "AVISO");
                return;
            }
            
            Pagar = Convert.ToDouble(txtDinheiro.Text) + Convert.ToDouble(txtPix.Text) + Convert.ToDouble(txtCartao.Text);
            if(Pagar <=0)
            {
                MessageBox.Show("Informe o valor!", "AVISO");
                return;
            }
            var totalPagarAvista = Double.Parse(lblApagar.Text, System.Globalization.NumberStyles.Currency); 
            if (Pagar > totalPagarAvista)
            {
                con.AbrirConexao();
                sql = "UPDATE lancar_vendas SET desconto=@desconto, taxa=@taxa, valor_total=@valor_total, pago=@pago, apagar=@apagar, pix=@pix, dinheiro=@dinheiro, cartao=@cartao, funcionario=@funcionario, data=curDate(), status=@status WHERE id=@id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(desconto) + Convert.ToDouble(txtDesconto.Text));
                cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(taxa) + Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@valor_total", (Convert.ToDouble(valor_total) - Convert.ToDouble(txtDesconto.Text)) + Convert.ToDouble(txtTaxa.Text));                //
                cmd.Parameters.AddWithValue("@pago", (Convert.ToDouble(valor_total) - Convert.ToDouble(txtDesconto.Text)) + Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@apagar", 0);
                cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPix.Text));
                cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtDinheiro.Text) - Double.Parse(lblTroco.Text, System.Globalization.NumberStyles.Currency));
                cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartao.Text));
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.Parameters.AddWithValue("@status", "Quitado");
                cmd.Parameters.AddWithValue("@id", id); 
                cmd.ExecuteNonQuery();
                con.FecharConexao();                
            }          
            if (Pagar == totalPagarAvista)
            {
                con.AbrirConexao(); 
                sql = "UPDATE lancar_vendas SET desconto=@desconto, taxa=@taxa, valor_total=@valor_total, pago=@pago, apagar=@apagar, pix=@pix, dinheiro=@dinheiro, cartao=@cartao, funcionario=@funcionario, data=curDate(), status=@status WHERE id=@id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(desconto) + Convert.ToDouble(txtDesconto.Text));
                cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(taxa) + Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@valor_total", (Convert.ToDouble(valor_total) - Convert.ToDouble(txtDesconto.Text)) + Convert.ToDouble(txtTaxa.Text));
              
                cmd.Parameters.AddWithValue("@pago", Convert.ToDouble(pago) + (dinheiroQ + pixQ + cartaoQ)); 
                cmd.Parameters.AddWithValue("@apagar", 0);
                cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPix.Text));
                cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtDinheiro.Text));
                cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartao.Text));
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.Parameters.AddWithValue("@status","Quitado");
                cmd.Parameters.AddWithValue("@id", id); 
                cmd.ExecuteNonQuery();
                con.FecharConexao();
            }
            if (Pagar < totalPagarAvista)
            {
                var vRestantes = Double.Parse(lblRestante.Text, System.Globalization.NumberStyles.Currency); 
                con.AbrirConexao(); 
                sql = "UPDATE lancar_vendas SET desconto=@desconto, taxa=@taxa, valor_total=@valor_total, entrada=@entrada, pago=@pago, apagar=@apagar, pix=@pix, dinheiro=@dinheiro, cartao=@cartao, funcionario=@funcionario, data=curDate(), status=@status WHERE id=@id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(desconto) + Convert.ToDouble(txtDesconto.Text));
                cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(taxa) + Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@valor_total", (Convert.ToDouble(valor_total) - Convert.ToDouble(txtDesconto.Text)) + Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@entrada", Convert.ToDouble(entrada) + (dinheiroQ+pixQ+cartaoQ));
                cmd.Parameters.AddWithValue("@pago", Convert.ToDouble(pago) + (dinheiroQ + pixQ + cartaoQ)); 
                cmd.Parameters.AddWithValue("@apagar", -vRestantes);
                cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(pix) + Convert.ToDouble(txtPix.Text));
                cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(dinheiro) + Convert.ToDouble(txtDinheiro.Text));
                cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(cartao) + Convert.ToDouble(txtCartao.Text));
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.Parameters.AddWithValue("@status", "Aberto");
                cmd.Parameters.AddWithValue("@id", id); 
                cmd.ExecuteNonQuery();
                con.FecharConexao();
            }
                      
            var valorPadrao = Double.Parse(valor_total, System.Globalization.NumberStyles.Currency);
            var g = Convert.ToDouble(txtTaxa.Text);
            var w = Convert.ToDouble(txtDesconto.Text);
            var novoValor = (valorPadrao + g) - w;

            con.AbrirConexao();
            sql = "INSERT INTO lancar_movimentacoes(cliente, tipo, movimento, descricao, valor, desconto, taxa, valor_total, cartao, pix, dinheiro, nota, valor_pago, funcionario, id_movimento, data) VALUES(@cliente, @tipo, @movimento, @descricao, @valor, @desconto, @taxa, @valor_total, @cartao, @pix, @dinheiro, @nota, @valor_pago, @funcionario, @id_movimento, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", lblCliente.Text);
            cmd.Parameters.AddWithValue("@tipo", "Entrada");
            cmd.Parameters.AddWithValue("@movimento", "Pagamento de mercadoria");
            cmd.Parameters.AddWithValue("@descricao", "Produto");
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(valor));
            cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(desconto) + Convert.ToDouble(txtDesconto.Text));
            cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(taxa) + Convert.ToDouble(txtTaxa.Text));
            cmd.Parameters.AddWithValue("@valor_total", novoValor);
            cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartao.Text));
            cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPix.Text));
            cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtDinheiro.Text));
            cmd.Parameters.AddWithValue("@nota", nota);
            cmd.Parameters.AddWithValue("@valor_pago", dinheiroQ + pixQ + cartaoQ);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@id_movimento", id);
            cmd.ExecuteNonQuery();
            con.FecharConexao();            


            if (MessageBox.Show("Deseja emitir recibo ?", "AVISO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Program.idVenda = id;
                Relatorios.FrmRecibo frm = new Relatorios.FrmRecibo();
                frm.ShowDialog();
            }
            MessageBox.Show("Movimentação realizada com sucesso", "AVISO");
            limparDados();
            OcultarPanel();
            LimparSelecao();
            ListarData();
            cbStatus.SelectedIndex = 0;
            grid.Enabled = true;            
        }
        private void LimparSelecao()
        {
            grid.ClearSelection();            
            btnRel.BackColor = Color.FromArgb(45, 66, 91);
            txtNome.Text = "";            
            btnRel.Enabled = false;
        }
        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkViolet;
                grid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void grid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(45, 66, 91);
                grid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.RowIndex > -1)
            {                
                Program.idVenda = grid.CurrentRow.Cells[0].Value.ToString();  

                panel3.Location = new Point(0, 223);
                grid.Location = new Point(1, 255);
                grid.ClearSelection();
                panelGrid.Visible = false;                                          

                id = grid.CurrentRow.Cells[0].Value.ToString();
                cliente = grid.CurrentRow.Cells[1].Value.ToString();
                valor = grid.CurrentRow.Cells[2].Value.ToString();
                desconto = grid.CurrentRow.Cells[3].Value.ToString();
                taxa = grid.CurrentRow.Cells[4].Value.ToString();
                valor_total = grid.CurrentRow.Cells[5].Value.ToString();
                entrada = grid.CurrentRow.Cells[6].Value.ToString();
                pago = grid.CurrentRow.Cells[7].Value.ToString();
                apagar = grid.CurrentRow.Cells[8].Value.ToString();
                pix = grid.CurrentRow.Cells[9].Value.ToString();
                dinheiro = grid.CurrentRow.Cells[10].Value.ToString();
                cartao = grid.CurrentRow.Cells[11].Value.ToString();
                nota = grid.CurrentRow.Cells[12].Value.ToString();
                funcionario = grid.CurrentRow.Cells[13].Value.ToString();
                data = grid.CurrentRow.Cells[14].Value.ToString();
                status = grid.CurrentRow.Cells[15].Value.ToString();
                
                totalApagar = Convert.ToDouble(apagar);

                lblCliente.Text = cliente;
                lblNota.Text = nota;
                lblStatus.Text = status;
                lblValor.Text = string.Format("{0:c2}", valor);
                lblDesconto.Text = string.Format("{0:c2}", desconto);
                lblTaxa.Text = string.Format("{0:c2}", taxa);
                lblEntrada.Text = string.Format("{0:c2}", entrada);
                lbltotal.Text = string.Format("{0:c2}", valor_total);
                lblApagar.Text = string.Format("{0:c2}", totalApagar);
                lblRestante.Text = string.Format("{0:c2}", totalApagar);

                TotalizandoValorPorCliente();
                btnQuitar.Enabled = true;
                panelGrid.Visible = true;
                btnRel.Enabled = true;
                btnRel.BackColor = Color.Green;
                grid.Enabled = false;
            }
            else
            {
                panelGrid.Visible = false;
                panel3.Location = new Point(0, 53);
                grid.Location = new Point(0, 89);
                btnRel.Enabled = false;
            }
        }
        
        private void TotalizandoAberto()
        {
            double total = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["status"].Value.ToString() == "Aberto")
                {                    
                    total += Convert.ToDouble(row.Cells["apagar"].Value);
                }
            }
            totalEntradas = total;
        }
        private void TotalizandoValorPorCliente()
        {
            double totalPago = 0;
           
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["cliente"].Value.ToString() == lblCliente.Text)
                {
                   
                    totalPago += Convert.ToDouble(row.Cells["pago"].Value);
                }
            }
            montante = totalPago;
            lblMontante.Text = string.Format("{0:c2}", montante);
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
        private void Troco()
        {
            if (valorDescontado > 0)
            {               
                var Pagando = Double.Parse(lblApagar.Text, System.Globalization.NumberStyles.Currency); 
                troco = (Convert.ToDouble(txtDinheiro.Text) + Convert.ToDouble(txtPix.Text) + Convert.ToDouble(txtCartao.Text)) - Pagando;
                lblTroco.Text = String.Format("{0:C2}", troco);
            }
            if (valorDescontado <= 0)
            {
                var vTotal = Double.Parse(lblApagar.Text, System.Globalization.NumberStyles.Currency);
                troco = (Convert.ToDouble(txtDinheiro.Text) + Convert.ToDouble(txtPix.Text) + Convert.ToDouble(txtCartao.Text)) - vTotal;
                lblTroco.Text = String.Format("{0:C2}", troco);
            }
        }


    }
}