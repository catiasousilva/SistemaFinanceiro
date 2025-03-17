using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFinanceiro.Vendas
{
    public partial class FrmVendas : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string id, idExcluirProduto, nomeProduto, qtdProduto, unitario, ultimoIdVenda;
        int item = 0;
        double qtd, valorUnitario, subTotal, valorVenda, dinheiro, pix, cartao, valorDescontado, troco, dinheiroX;
        double valor_totaly, valor_totalx, valor_totalz, valor_total, totalPagar, cartaoDinheiroPix;

        string idProduto, codigoProduto, atualizarEstoque, ultimoIdGrid, numeroNota, idDetalheVenda;
        int estoqueProduto, nota;

        public FrmVendas()
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
        private void FrmVendas_Load(object sender, EventArgs e)
        {
            LoadTheme();
            dtCompra.Value = DateTime.Now;
            this.panelLancamento.Location = new Point(1, 71);
            panelVendaAvista.Visible = false;
            panelVendaAPrazo.Visible = false;
            txtCodProduto.Focus();
            Listardetalhes_lancarvendas();
            TotalizandoEntradas();
        }

        private void detalhes_lancarvendas()
        {
            gridDetalhes.Columns[0].HeaderText = "Id";
            gridDetalhes.Columns[1].HeaderText = "Id. Vendas";
            gridDetalhes.Columns[2].HeaderText = "Código";
            gridDetalhes.Columns[3].HeaderText = "Item";
            gridDetalhes.Columns[4].HeaderText = "Descrição";
            gridDetalhes.Columns[5].HeaderText = "Qtd";
            gridDetalhes.Columns[6].HeaderText = "Unitário";
            gridDetalhes.Columns[7].HeaderText = "Subtotal";
            gridDetalhes.Columns[8].HeaderText = "Atendente";
            gridDetalhes.Columns[9].HeaderText = "id_produto";
            gridDetalhes.Columns[10].HeaderText = "Data";
            gridDetalhes.Columns[11].HeaderText = "Recibo";
            gridDetalhes.Columns[6].DefaultCellStyle.Format = "c2";
            gridDetalhes.Columns[7].DefaultCellStyle.Format = "c2";
            gridDetalhes.Columns[0].Visible = false;
            gridDetalhes.Columns[1].Visible = false;
            gridDetalhes.Columns[8].Visible = false;
            gridDetalhes.Columns[9].Visible = false;
            gridDetalhes.Columns[10].Visible = false;
            gridDetalhes.Columns[11].Visible = false;
        }
        private void Listardetalhes_lancarvendas()
        {

            con.AbrirConexao();
            sql = "SELECT * FROM detalhes_lancarvendas WHERE id_venda=@id_venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_venda", "0");
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridDetalhes.DataSource = dt;
            con.FecharConexao();
            detalhes_lancarvendas();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;
                cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE nome LIKE @nome", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.Add(new MySqlParameter("@nome", "%" + txtBusca.Text + "%"));
                cmdVerificar.ExecuteNonQuery();
                reader = cmdVerificar.ExecuteReader();
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();

                while (reader.Read())
                {
                    col.Add(reader.GetString(2));
                }
                txtBusca.AutoCompleteCustomSource = col;
                con.FecharConexao();
            }
            catch (Exception)
            {
                MessageBox.Show("Algo deu errado, feche o sistema e tente novamente", "Sitema Controle Financeiro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtClientes_TextChanged(object sender, EventArgs e)
        {
            txtClientes.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtClientes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            con.AbrirConexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            cmdVerificar = new MySqlCommand("SELECT * FROM clientes WHERE nome LIKE @nome", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            cmdVerificar.Parameters.Add(new MySqlParameter("@nome", "%" + txtClientes.Text + "%"));
            cmdVerificar.ExecuteNonQuery();
            reader = cmdVerificar.ExecuteReader();
            while (reader.Read())
            {
                coll.Add(reader.GetString("nome"));
            }
            con.FecharConexao();
            txtClientes.AutoCompleteCustomSource = coll;
            con.FecharConexao();
        }

        private void txtCodProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProdutoPorCodigo();
                txtQuantidade.Focus();
                txtQuantidade.SelectAll();
            }
            if (e.KeyCode == Keys.Tab)
            {
                ProdutoPorCodigo();
                txtQuantidade.Focus();
                txtQuantidade.SelectAll();
            }

            if (e.KeyCode == Keys.F3)
            {
                finalizarVendaAvista();
            }
            if (e.KeyCode == Keys.F4)
            {
                finalizarVendaPrazo();
            }

            if (e.KeyCode == Keys.F5)
            {
                cancelarVenda();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Vendas.FrmAjuda frm = new Vendas.FrmAjuda();
                frm.ShowDialog();
            }
        }
        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProdutoPorCodigo();
                txtQuantidade.Focus();
                txtQuantidade.SelectAll();
            }
            if (e.KeyCode == Keys.Tab)
            {
                ProdutoPorCodigo();
                txtQuantidade.Focus();
                txtQuantidade.SelectAll();
            }

            if (e.KeyCode == Keys.F3)
            {
                finalizarVendaAvista();
            }
            if (e.KeyCode == Keys.F4)
            {
                finalizarVendaPrazo();
            }

            if (e.KeyCode == Keys.F5)
            {
                cancelarVenda();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Vendas.FrmAjuda frm = new Vendas.FrmAjuda();
                frm.ShowDialog();
            }
        }
        private void gridDetalhes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletarItem();
            }
        }
        private void txtClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                informacoesCliente();
                txtClientes.Enabled = false;
                btnConfirmar.Enabled = false;

                panelCliente.Visible = true;
                if (lblClienteBloqeado.Text == "Não")
                {
                    bntConcluirVenda.Enabled = true;
                    txtDinheiro.Focus();
                }
                else if (lblClienteBloqeado.Text == "Sim")
                {
                    bntConcluirVenda.Enabled = false;
                    MessageBox.Show("Cliente bloqueado!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (e.KeyCode == Keys.Tab)
            {
                informacoesCliente();
                txtClientes.Enabled = false;
                btnConfirmar.Enabled = false;

                panelCliente.Visible = true;
                if (lblClienteBloqeado.Text == "Não")
                {
                    bntConcluirVenda.Enabled = true;
                    txtDinheiro.Focus();
                }
                else if (lblClienteBloqeado.Text == "Sim")
                {
                    bntConcluirVenda.Enabled = false;
                    MessageBox.Show("Cliente bloqueado!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void informacoesCliente()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM clientes WHERE nome=@nome", con.con);
            cmdVerificar.Parameters.AddWithValue("@nome", txtClientes.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    lblCliente.Text = Convert.ToString(reader["nome"]);
                    lblValorAberto.Text = Convert.ToString(reader["valorAberto"]);
                    lblClienteBloqeado.Text = Convert.ToString(reader["desbloqueado"]);
                }
            }
            con.FecharConexao();
        }


        private void LancarItemNaVenda()
        {
            con.AbrirConexao();
            sql = "INSERT INTO detalhes_lancarvendas(id_venda, codigo_produto, item, descricao, quantidade, valor_unitario, subtotal, funcionario, id_produto, data, nota) VALUES(@id_venda, @codigo_produto, @item, @descricao, @quantidade, @valor_unitario, @subtotal, @funcionario, @id_produto, curDate(), @nota)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_venda", "0");
            cmd.Parameters.AddWithValue("@codigo_produto", codigoProduto);
            cmd.Parameters.AddWithValue("@item", item);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@quantidade", txtQtd.Text);
            cmd.Parameters.AddWithValue("@valor_unitario", valorUnitario);
            cmd.Parameters.AddWithValue("@subtotal", subTotal);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@id_produto", FinalizarVendas.IdProduto);
            cmd.Parameters.AddWithValue("@nota", nota);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            con.AbrirConexao();
            sql = "UPDATE produtos SET estoque=@estoque WHERE id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", FinalizarVendas.IdProduto);
            cmd.Parameters.AddWithValue("@estoque", Convert.ToInt32(estoqueProduto) - Convert.ToInt32(txtQuantidade.Text));
            cmd.ExecuteNonQuery();
            con.FecharConexao();


            idDetalheVenda = "";
            Listardetalhes_lancarvendas();

        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBusca.Text.Trim() == "")
                {
                    MessageBox.Show("Selecione o item do lançamento!!! ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodProduto.Focus();
                    return;
                }

                LimparItem();
                txtDescricao.Text = txtBusca.Text;
                txtQtd.Text = txtQuantidade.Text;
                txtItem.Text = item.ToString();
                TotalizandoEntradas();
                calcSubTotal();
                txtValorUnitario.Text = String.Format("{0:C2}", valorUnitario);
                LancarItemNaVenda();
                limparCampos();
                TotalizandoEntradas();
            }
            if (e.KeyCode == Keys.Tab)
            {
                if (txtBusca.Text.Trim() == "")
                {
                    MessageBox.Show("Selecione o item do lançamento!!! ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodProduto.Focus();
                }

                LimparItem();
                txtDescricao.Text = txtBusca.Text;
                txtQtd.Text = txtQuantidade.Text;
                txtItem.Text = item.ToString();
                TotalizandoEntradas();
                calcSubTotal();
                txtValorUnitario.Text = String.Format("{0:C2}", valorUnitario);
                LancarItemNaVenda();
                limparCampos();
                TotalizandoEntradas();
            }
        }
        private void calcSubTotal()
        {
            qtd = Convert.ToInt32(txtQuantidade.Text);
            valorUnitario = Convert.ToDouble(unitario) / 100;
            subTotal = (qtd * valorUnitario);
            txtSubTotal.Text = String.Format("{0:C2}", subTotal);
            valorVenda += subTotal;
            txtTotalVenda.Text = String.Format("{0:C2}", valorVenda);
        }

        private void TotalizandoEntradas()
        {
            double total = 0;


            foreach (DataGridViewRow row in gridDetalhes.Rows)
            {
                if (row.Cells["data"].Value.ToString() == DateTime.Today.ToString())
                {
                    total += Convert.ToDouble(row.Cells["subtotal"].Value);
                }

            }
            txtTotalVenda.Text = Convert.ToDouble(total).ToString("C2");
        }
        private void somaItem()
        {
            item += 1;
        }
        private void limparVendaPrazo()
        {
            valor_totaly = 0;
            valor_totalx = 0;
            valor_total = 0;
            totalPagar = 0;
            cartaoDinheiroPix = 0;
        }
        private void limparCampos()
        {
            txtCodProduto.Text = "";
            txtBusca.Text = "";
            txtQuantidade.Text = "1";
            estoqueProduto = 0;
            FinalizarVendas.IdProduto = 0;
            txtCodProduto.Focus();

            lblIdPro.Text = "0";
            lblIdItem.Text = "0";
            lblEstoque.Text = "0";
        }

        private void LimparItem()
        {
            txtDescricao.Text = "...";
            txtQtd.Text = "0";
            txtValorUnitario.Text = "0,00";
            txtSubTotal.Text = "0,00";
            txtItem.Text = "0";
            txtTotalVenda.Text = "0,00";
        }
        private void ProdutoPorCodigo()
        {
            NumeroNota();
            if (txtCodProduto.Text.Trim() != "")
            {
                con.AbrirConexao();
                MySqlCommand Cmm = new MySqlCommand("SELECT * FROM produtos WHERE cod=@cod", con.con);
                Cmm.CommandText = "SELECT  * from produtos WHERE cod =  " + txtCodProduto.Text;
                Cmm.CommandType = CommandType.Text;
                Cmm.Connection = con.con;
                MySqlDataReader DR;
                DR = Cmm.ExecuteReader();
                DR.Read();

                if (DR.HasRows)
                {
                    id = Convert.ToString(DR.GetInt64(0));
                    FinalizarVendas.IdProduto = DR.GetInt32(0);
                    txtCodProduto.Text = DR.GetString(1);
                    txtBusca.Text = DR.GetString(2);
                    unitario = Convert.ToString(DR.GetMySqlDecimal(8));
                    codigoProduto = DR.GetString(1);
                    estoqueProduto = DR.GetInt32(4);
                    lblIdPro.Text = FinalizarVendas.IdProduto.ToString();
                    lblEstoque.Text = estoqueProduto.ToString();

                    somaItem();
                }
                else
                {
                    txtBusca.Text = "Produto não cadastrado";
                }
                con.FecharConexao();
                lblNota.Text = nota.ToString();
            }
        }

        private void bntConcluirVenda_Click(object sender, EventArgs e)
        {
            if (txtClientes.Text.Trim() == "")
            {
                MessageBox.Show("Necessário selecionar o cliente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtClientes.Focus();
            }
            else
            {
                txtClientes.Focus();
                valor_totaly = Double.Parse(lblValorPagar.Text, System.Globalization.NumberStyles.Currency); //double SEM R$ Convert.ToDouble(lblValorPagar.Text);
                valor_totalx = Convert.ToDouble(txtDesconto.Text);
                valor_totalz = Convert.ToDouble(txtTaxa.Text);
                valor_total = valor_totaly - valor_totalx;

                totalPagar = Double.Parse(txtValorPagar.Text, System.Globalization.NumberStyles.Currency); //double SEM R$
                cartaoDinheiroPix = (dinheiro + pix + cartao);
                if (cartaoDinheiroPix >= totalPagar)
                {
                    MessageBox.Show("Valor inserido de:  " + cartaoDinheiroPix.ToString("C2") + " corresponde ao valor total do consumo, aplique o pagamento á vista", "AVISO");

                    dinheiro = 0;
                    pix = 0;
                    cartao = 0;
                    txtDinheiro.Text = "0";
                    txtPix.Text = "0";
                    txtCartao.Text = "0";
                    txtDinheiro.Focus();
                    return;
                }

                con.AbrirConexao();
                sql = "INSERT INTO lancar_vendas(cliente, valor, desconto, taxa, valor_total, entrada, pago, apagar, pix, dinheiro, cartao, nota, funcionario, data, status) VALUES(@cliente, @valor, @desconto, @taxa, @valor_total, @entrada, @pago, @apagar, @pix, @dinheiro, @cartao, @nota, @funcionario, curDate(), @status)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@cliente", txtClientes.Text);
                cmd.Parameters.AddWithValue("@valor", valor_totaly);
                cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(txtDesconto.Text));
                cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(txtTaxa.Text));
                cmd.Parameters.AddWithValue("@valor_total", valor_total + valor_totalz);
                cmd.Parameters.AddWithValue("@entrada", cartaoDinheiroPix);
                cmd.Parameters.AddWithValue("@pago", cartaoDinheiroPix);
                cmd.Parameters.AddWithValue("@apagar", (valor_total - cartaoDinheiroPix) + valor_totalz);
                cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPix.Text));
                cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtDinheiro.Text));
                cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartao.Text));
                cmd.Parameters.AddWithValue("@nota", Convert.ToInt32(lblNota.Text));
                cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                cmd.Parameters.AddWithValue("@status", "Aberto");
                cmd.ExecuteNonQuery();
                con.FecharConexao();

                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;
                cmdVerificar = new MySqlCommand("SELECT id FROM lancar_vendas ORDER BY id DESC LIMIT 1", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                reader = cmdVerificar.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ultimoIdVenda = Convert.ToString(reader["id"]);
                    }
                }
                con.FecharConexao();

                if (cartaoDinheiroPix > 0)
                {
                    con.AbrirConexao();
                    sql = "INSERT INTO lancar_movimentacoes(cliente, tipo, movimento, descricao, valor, desconto, taxa, valor_total, cartao, pix, dinheiro, nota, valor_pago, funcionario, id_movimento, data) VALUES(@cliente, @tipo, @movimento, @descricao, @valor, @desconto, @taxa, @valor_total, @cartao, @pix, @dinheiro, @nota, @valor_pago, @funcionario, @id_movimento, curDate())";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@cliente", txtClientes.Text);
                    cmd.Parameters.AddWithValue("@tipo", "Entrada");
                    cmd.Parameters.AddWithValue("@movimento", "Venda com entrada");
                    cmd.Parameters.AddWithValue("@descricao", "Produto");
                    cmd.Parameters.AddWithValue("@valor", valor_totaly);
                    cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(txtDesconto.Text));
                    cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(txtTaxa.Text));
                    cmd.Parameters.AddWithValue("@valor_total", valor_total + valor_totalz);
                    cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartao.Text));
                    cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPix.Text));
                    cmd.Parameters.AddWithValue("@dinheiro", Convert.ToDouble(txtDinheiro.Text));
                    cmd.Parameters.AddWithValue("@nota", lblNota.Text);
                    cmd.Parameters.AddWithValue("@valor_pago", cartaoDinheiroPix);
                    cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
                    cmd.Parameters.AddWithValue("@id_movimento", ultimoIdVenda);
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();
                }


                con.AbrirConexao();
                sql = "UPDATE detalhes_lancarvendas SET id_venda = @id_venda WHERE id_venda = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", "0");
                cmd.Parameters.AddWithValue("@id_venda", ultimoIdVenda);
                cmd.ExecuteNonQuery();
                con.FecharConexao();


                cancelar();
                limparCampos();
                LimparItem();
                limparVendaPrazo();
                Listardetalhes_lancarvendas();
                lblNota.Text = "0";
                panelVenda.Enabled = true;
                panelLancamento.Visible = true;
                panelVendaAvista.Visible = false;
                panelVendaAPrazo.Visible = false;
                this.panelLancamento.Location = new Point(1, 71);
                MessageBox.Show("Concluído com sucesso!", "AVISO");
                if (MessageBox.Show("Deseja emitir recibo ?", "AVISO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Program.idVenda = ultimoIdVenda;
                    Relatorios.FrmRecibo frm = new Relatorios.FrmRecibo();
                    frm.ShowDialog();
                }
            }
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            cancelarVenda();
            panelVenda.Enabled = true;
        }

        private void cancelarVenda()
        {
            var res = double.Parse(txtTotalVenda.Text, System.Globalization.NumberStyles.Currency);
            if (res > 0)
            {
                MessageBox.Show("Necessário excluir todos os itens da lista, antes de excluir!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                con.AbrirConexao();
                sql = "DELETE FROM detalhes_lancarvendas WHERE nota=@nota";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nota", numeroNota);
                cmd.ExecuteNonQuery();
                con.FecharConexao();


                limparCampos();
                LimparItem();
                lblNota.Text = "0";
                Listardetalhes_lancarvendas();

                panelVenda.Enabled = true;
                panelLancamento.Visible = true;
                panelVendaAvista.Visible = false;
                panelVendaAPrazo.Visible = false;
                this.panelLancamento.Location = new Point(1, 71);

                MessageBox.Show("Lançamento excluído com sucesso!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            txtClientes.Enabled = true;
            btnConfirmar.Enabled = true;
            txtClientes.Text = "";
            lblValorAberto.Text = "0";
            lblCliente.Text = "...";
            lblClienteBloqeado.Text = "...";
            txtDesconto.Text = "0";
            txtTaxa.Text = "0";
            txtValorPagar.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            dinheiro = 0;
            pix = 0;
            cartao = 0;

            panelCliente.Visible = false;

            DateTime fimMes = DateTime.Now;
            fimMes.AddDays(30);
            dtVencimento.Text = fimMes.ToString();
            panelVenda.Enabled = true;
            panelLancamento.Visible = true;
            panelVendaAvista.Visible = false;
            panelVendaAPrazo.Visible = false;
            this.panelLancamento.Location = new Point(1, 71);
            txtCodProduto.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            informacoesCliente();
            txtClientes.Enabled = false;
            btnConfirmar.Enabled = false;

            panelCliente.Visible = true;
            if (lblClienteBloqeado.Text == "Não")
            {
                bntConcluirVenda.Enabled = true;
                txtDinheiro.Focus();
            }
            else if (lblClienteBloqeado.Text == "Sim")
            {
                bntConcluirVenda.Enabled = false;
                MessageBox.Show("Cliente bloqueado!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDinheiro_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDinheiro);
            dinheiro = Convert.ToDouble(txtDinheiro.Text);
        }

        private void txtPix_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtPix);
            pix = Convert.ToDouble(txtPix.Text);
        }

        private void txtCartao_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtCartao);
            cartao = Convert.ToDouble(txtCartao.Text);
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDesconto);
            var l = Double.Parse(lblValorPagar.Text, System.Globalization.NumberStyles.Currency);
            var d = Double.Parse(txtDesconto.Text, System.Globalization.NumberStyles.Currency);//Convert.ToDouble(txtDesconto.Text);
            var valorPrazo = l - d;
            txtValorPagar.Text = String.Format("{0:C2}", valorPrazo);
        }

        private void txtTaxa_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTaxa);
            var vp = Double.Parse(lblValorPagar.Text, System.Globalization.NumberStyles.Currency);
            var t = Double.Parse(txtTaxa.Text, System.Globalization.NumberStyles.Currency);
            var taxaPrazo = vp + t;
            txtValorPagar.Text = String.Format("{0:C2}", taxaPrazo);
        }

        private void cancelar()
        {
            txtClientes.Enabled = true;
            btnConfirmar.Enabled = true;
            txtClientes.Text = "";
            lblValorAberto.Text = "0";
            lblCliente.Text = "0";
            lblClienteBloqeado.Text = "...";
            txtDesconto.Text = "0";
            txtTaxa.Text = "0";
            txtValorPagar.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            dinheiro = 0;
            pix = 0;
            cartao = 0;
            panelCliente.Visible = false;
            DateTime fimMes = DateTime.Now;
            DateTime vencimento = fimMes.AddDays(30);
            dtVencimento.Text = vencimento.ToString();
            txtClientes.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar();
            txtDinheiro.Focus();
            panelVenda.Enabled = false;
        }

        private void txtDescontoAvista_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDescontoAvista);
            var lbl = Double.Parse(lblValorAvista.Text, System.Globalization.NumberStyles.Currency);
            var txt = Double.Parse(txtDescontoAvista.Text, System.Globalization.NumberStyles.Currency);
            var valor = lbl - txt;
            txtValorAvista.Text = String.Format("{0:C2}", valor);

            valorDescontado = txt;
        }

        private void txtTaxaAvista_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTaxaAvista);

            var lblTaxa = Double.Parse(lblValorAvista.Text, System.Globalization.NumberStyles.Currency);
            var txtTaxa = Double.Parse(txtTaxaAvista.Text, System.Globalization.NumberStyles.Currency);
            var valorTaxa = lblTaxa + txtTaxa;
            txtValorAvista.Text = String.Format("{0:C2}", valorTaxa);
        }

        private void txtDinheiroAvista_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtDinheiroAvista);
            dinheiro = Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text);
            if (txtDinheiroAvista.Text == "0,00")
            {
                labelTroco.Text = "0,00";
            }
            Troco();
        }

        private void txtPixAvista_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtPixAvista);
            pix = Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text);
            Troco();
        }

        private void txtCartaoAvista_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtCartaoAvista);
            cartao = Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text);
            Troco();
        }

        private void txtDescontoAvista_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtTaxaAvista_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtDinheiroAvista_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Troco();
            }
        }

        private void txtPixAvista_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Troco();
            }
        }

        private void txtCartaoAvista_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Troco();
                vDinheiro();
            }
        }

        private void txtPixAvista_Leave(object sender, EventArgs e)
        {
            Troco();
            vDinheiro();
            txtDinheiroAvista.Focus();
        }

        private void txtDescontoAvista_Click(object sender, EventArgs e)
        {
            txtDescontoAvista.Text = "0";
            txtTaxaAvista.Text = "0";
            txtDinheiroAvista.Text = "0";
            txtPixAvista.Text = "0";
            txtCartaoAvista.Text = "0";
            labelTroco.Text = "0,00";
        }

        private void txtTaxaAvista_Click(object sender, EventArgs e)
        {
            txtDescontoAvista.Text = "0";
            txtTaxaAvista.Text = "0";
            txtDinheiroAvista.Text = "0";
            txtPixAvista.Text = "0";
            txtCartaoAvista.Text = "0";
            labelTroco.Text = "0,00";
        }

        private void txtDinheiroAvista_Click(object sender, EventArgs e)
        {
            txtDinheiroAvista.Text = "0";
            txtPixAvista.Text = "0";
            txtCartaoAvista.Text = "0";
            labelTroco.Text = "0,00";
        }

        private void txtCartaoAvista_Leave(object sender, EventArgs e)
        {
            Troco();
            vDinheiro();
            txtPixAvista.Focus();
        }

        private void txtDescontoAvista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDinheiroAvista.Focus();
            }
        }

        private void txtTaxaAvista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDinheiroAvista.Focus();
            }
        }

        private void txtDinheiroAvista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPixAvista.Focus();
            }
        }

        private void txtPixAvista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCartaoAvista.Focus();
            }
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

        private void txtPix_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtCartao_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void btnConfirmar_KeyDown(object sender, KeyEventArgs e)
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
                bntConcluirVenda.Focus();
            }
        }

        private void txtClientes_Enter(object sender, EventArgs e)
        {

        }

        private void txtCartaoAvista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnVendaAvista.Focus();
            }
        }

        private void txtDinheiro_Click(object sender, EventArgs e)
        {
            txtPix.Text = "0";
            txtCartao.Text = "0";
            pix = 0;
            cartao = 0;
        }

        private void txtPix_Click(object sender, EventArgs e)
        {
            txtCartao.Text = "0";
            cartao = 0;
        }

        private void txtDesconto_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            txtTaxa.Text = "0";
            dinheiro = 0;
            pix = 0;
            cartao = 0;
        }

        private void txtTaxa_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = "0";
            txtDinheiro.Text = "0";
            txtPix.Text = "0";
            txtCartao.Text = "0";
            dinheiro = 0;
            pix = 0;
            cartao = 0;
        }

        private void btnCancelarAvista_Click(object sender, EventArgs e)
        {
            cancelarAvista();
            panelVenda.Enabled = true;
        }
        private void btnContinuaAvista_Click(object sender, EventArgs e)
        {
            lblValorAvista.Text = "0";
            cancelarAvista();
            panelVenda.Enabled = true;
            panelLancamento.Visible = true;
            panelVendaAvista.Visible = false;
            panelVendaAPrazo.Visible = false;
            this.panelLancamento.Location = new Point(1, 71);
            txtCodProduto.Focus();
        }
        void cancelarAvista()
        {
            txtValorAvista.Text = lblValorAvista.Text;
            txtDescontoAvista.Text = "0";
            txtTaxaAvista.Text = "0";
            txtDinheiroAvista.Text = "0";
            txtPixAvista.Text = "0";
            txtCartaoAvista.Text = "0";
            labelTroco.Text = "0";
        }
        private void vDinheiro()
        {
            dinheiroX = (Double.Parse(lblValorAvista.Text, System.Globalization.NumberStyles.Currency) - Convert.ToDouble(txtDescontoAvista.Text) + Convert.ToDouble(txtTaxaAvista.Text)) - Convert.ToDouble(txtCartaoAvista.Text) - Convert.ToDouble(txtPixAvista.Text);

        }
        private void btnVendaAvista_Click(object sender, EventArgs e)
        {

            var Pagar = Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text);
            var totalPagarAvista = Double.Parse(txtValorAvista.Text, System.Globalization.NumberStyles.Currency);
            if (Pagar < totalPagarAvista)
            {
                MessageBox.Show("Não é permitido pagamento à vista com valor MENOR que o consumo! Você pode aplicar DESCONTO", "AVISTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCartaoAvista.Text = "0";
                txtDinheiroAvista.Text = "0";
                txtPixAvista.Text = "0";
                txtDinheiroAvista.Focus();
                return;
            }

            var Xvalor = Double.Parse(lblValorAvista.Text, System.Globalization.NumberStyles.Currency);
            var x = Xvalor;
            var y = Double.Parse(txtDescontoAvista.Text);
            var z = Convert.ToDouble(txtTaxaAvista.Text);
            var XvalorAvista = (x - y) + z;

            vDinheiro();

            con.AbrirConexao();
            sql = "INSERT INTO lancar_vendas(cliente, valor, desconto, taxa, valor_total, entrada, pago, apagar, pix, dinheiro, cartao, nota, funcionario, data, status) VALUES(@cliente, @valor, @desconto, @taxa, @valor_total, @entrada, @pago, @apagar, @pix, @dinheiro, @cartao, @nota, @funcionario, curDate(), @status)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", "Padrão");
            cmd.Parameters.AddWithValue("@valor", Xvalor);
            cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(txtDescontoAvista.Text));
            cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(txtTaxaAvista.Text));
            cmd.Parameters.AddWithValue("@valor_total", XvalorAvista);
            cmd.Parameters.AddWithValue("@entrada", 0);
            cmd.Parameters.AddWithValue("@pago", XvalorAvista);
            cmd.Parameters.AddWithValue("@apagar", 0);
            cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPixAvista.Text));
            cmd.Parameters.AddWithValue("@dinheiro", dinheiroX);
            cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartaoAvista.Text));
            cmd.Parameters.AddWithValue("@nota", Convert.ToInt32(lblNota.Text));
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@status", "Quitado");
            cmd.ExecuteNonQuery();
            con.FecharConexao();


            con.AbrirConexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            cmdVerificar = new MySqlCommand("SELECT id FROM lancar_vendas ORDER BY id DESC LIMIT 1", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ultimoIdVenda = Convert.ToString(reader["id"]);
                }
            }
            con.FecharConexao();

            var Yvalor = Double.Parse(lblValorAvista.Text, System.Globalization.NumberStyles.Currency);
            con.AbrirConexao();
            sql = "INSERT INTO lancar_movimentacoes(cliente, tipo, movimento, descricao, valor, desconto, taxa, valor_total, cartao, pix, dinheiro, nota, valor_pago, funcionario, id_movimento, data) VALUES(@cliente, @tipo, @movimento, @descricao, @valor, @desconto, @taxa, @valor_total, @cartao, @pix, @dinheiro, @nota, @valor_pago, @funcionario, @id_movimento, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cliente", "Padrão");
            cmd.Parameters.AddWithValue("@tipo", "Entrada");
            cmd.Parameters.AddWithValue("@movimento", "Venda mercadoria");
            cmd.Parameters.AddWithValue("@descricao", "Produto");
            cmd.Parameters.AddWithValue("@valor", Yvalor);
            cmd.Parameters.AddWithValue("@desconto", Convert.ToDouble(txtDescontoAvista.Text));
            cmd.Parameters.AddWithValue("@taxa", Convert.ToDouble(txtTaxaAvista.Text));
            cmd.Parameters.AddWithValue("@valor_total", XvalorAvista);
            cmd.Parameters.AddWithValue("@cartao", Convert.ToDouble(txtCartaoAvista.Text));
            cmd.Parameters.AddWithValue("@pix", Convert.ToDouble(txtPixAvista.Text));
            cmd.Parameters.AddWithValue("@dinheiro", dinheiroX);
            cmd.Parameters.AddWithValue("@nota", lblNota.Text);
            cmd.Parameters.AddWithValue("@valor_pago", XvalorAvista);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.Parameters.AddWithValue("@id_movimento", ultimoIdVenda);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            con.AbrirConexao();
            sql = "UPDATE detalhes_lancarvendas SET id_venda = @id_venda WHERE id_venda = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", "0");
            cmd.Parameters.AddWithValue("@id_venda", ultimoIdVenda);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            cancelarAvista();

            Listardetalhes_lancarvendas();
            limparCampos();
            LimparItem();
            lblNota.Text = "0";
            panelVenda.Enabled = true;
            panelLancamento.Visible = true;
            panelVendaAvista.Visible = false;
            panelVendaAPrazo.Visible = false;
            this.panelLancamento.Location = new Point(1, 71);
            MessageBox.Show("Concluído com sucesso!", "AVISO");
            if (MessageBox.Show("Deseja emitir recibo ?", "AVISO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Program.idVenda = ultimoIdVenda;
                Relatorios.FrmRecibo frm = new Relatorios.FrmRecibo();
                frm.ShowDialog();
            }
        }

        private void gridDetalhes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                gridDetalhes.ClearSelection();
                return;
            }
            else
            {
                btnExcluirItem.Enabled = true;
                id = gridDetalhes.CurrentRow.Cells[0].Value.ToString();
                nomeProduto = gridDetalhes.CurrentRow.Cells[4].Value.ToString();
                qtdProduto = gridDetalhes.CurrentRow.Cells[5].Value.ToString();
                idExcluirProduto = gridDetalhes.CurrentRow.Cells[9].Value.ToString();
                idProduto = gridDetalhes.CurrentRow.Cells[9].Value.ToString();
                lblIdPro.Text = gridDetalhes.CurrentRow.Cells[9].Value.ToString();
                numeroNota = gridDetalhes.CurrentRow.Cells[11].Value.ToString();
                txtItem.Text = gridDetalhes.CurrentRow.Cells[3].Value.ToString();
                txtDescricao.Text = gridDetalhes.CurrentRow.Cells[4].Value.ToString();
                txtQtd.Text = gridDetalhes.CurrentRow.Cells[5].Value.ToString();
                txtValorUnitario.Text = gridDetalhes.CurrentRow.Cells[6].Value.ToString();
                txtSubTotal.Text = gridDetalhes.CurrentRow.Cells[7].Value.ToString();

                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;
                cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE id = @id", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@id", idProduto);
                reader = cmdVerificar.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        atualizarEstoque = Convert.ToString(reader["estoque"]);
                        lblEstoque.Text = Convert.ToString(reader["estoque"]);
                    }
                }
                con.FecharConexao();
            }
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            DeletarItem();
        }
        private void DeletarItem()
        {
            btnExcluirItem.Enabled = false;
            //

            if (MessageBox.Show("Deseja excluir o item: " + nomeProduto + " ?", "AVISO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {

                    con.AbrirConexao();
                    sql = "UPDATE detalhes_lancarvendas SET codigo_produto=@codigo_produto, quantidade=@quantidade, valor_unitario=@valor_unitario, subtotal=@subtotal, data = curDate() WHERE id=@id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@codigo_produto", "Item cancelado");
                    cmd.Parameters.AddWithValue("@quantidade", 0);
                    cmd.Parameters.AddWithValue("@valor_unitario", 0);
                    cmd.Parameters.AddWithValue("@subtotal", 0);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();

                }
                catch (Exception)
                {
                }

                try
                {

                    con.AbrirConexao();
                    sql = "UPDATE produtos SET estoque=@estoque WHERE id=@id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", idProduto);
                    cmd.Parameters.AddWithValue("@estoque", Convert.ToInt32(atualizarEstoque) + Convert.ToInt32(qtdProduto));
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();

                }
                catch (Exception)
                {
                }

                try
                {
                    con.AbrirConexao();
                    MySqlCommand cmdVerificar;
                    MySqlDataReader reader;
                    cmdVerificar = new MySqlCommand("SELECT id FROM detalhes_lancarvendas ORDER BY id DESC LIMIT 1", con.con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    reader = cmdVerificar.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ultimoIdGrid = Convert.ToString(reader["id"]);
                        }

                        MySqlCommand cmdVerifi;
                        MySqlDataReader real;
                        con.AbrirConexao();
                        cmdVerifi = new MySqlCommand("SELECT * FROM detalhes_lancarvendas WHERE id=@id", con.con);
                        cmdVerifi.Parameters.AddWithValue("@id", ultimoIdGrid);
                        MySqlDataAdapter Ada = new MySqlDataAdapter();
                        Ada.SelectCommand = cmdVerifi;
                        real = cmdVerifi.ExecuteReader();
                        if (real.HasRows)
                        {
                            while (real.Read())
                            {
                                txtItem.Text = Convert.ToString(real["item"]);
                                txtDescricao.Text = Convert.ToString(real["descricao"]);
                                txtQtd.Text = Convert.ToString(real["quantidade"]);
                                txtValorUnitario.Text = Convert.ToString(real["valor_unitario"]);
                                txtSubTotal.Text = Convert.ToString(real["subtotal"]);
                            }
                        }
                        con.FecharConexao();
                        //                        
                    }
                    else
                    {
                        txtItem.Text = "0";
                        txtDescricao.Text = "...";
                        txtQtd.Text = "0";
                        txtSubTotal.Text = "0,00";
                        txtValorUnitario.Text = "0,00";
                    }
                    con.FecharConexao();

                }
                catch (Exception)
                {
                }

                Listardetalhes_lancarvendas();
                TotalizandoEntradas();
                gridDetalhes.ClearSelection();
                txtCodProduto.Focus();
            }
        }
        private void ProdutoPorNome()
        {
            NumeroNota();

            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE nome=@nome", con.con);
            cmdVerificar.Parameters.AddWithValue("@nome", txtBusca.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FinalizarVendas.IdProduto = Convert.ToInt32(reader["id"]);
                    txtCodProduto.Text = Convert.ToString(reader["cod"]);
                    txtBusca.Text = Convert.ToString(reader["nome"]);
                    estoqueProduto = Convert.ToInt32(reader["estoque"]);
                    lblEstoque.Text = Convert.ToString(reader["estoque"]);
                    double unit = Convert.ToDouble(reader[8]);
                    unitario = (unit * 100).ToString();

                    lblEstoque.Text = estoqueProduto.ToString();
                    lblIdPro.Text = FinalizarVendas.IdProduto.ToString();

                    somaItem();
                }
            }
            else
            {
                txtBusca.Text = "Produto não cadastrado";
            }
            con.FecharConexao();
            lblNota.Text = nota.ToString();

        }

        private void NumeroNota()
        {

            con.AbrirConexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            cmdVerificar = new MySqlCommand("SELECT id FROM lancar_vendas ORDER BY id DESC LIMIT 1", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    nota = Convert.ToInt32(reader["id"]);
                }
            }
            con.FecharConexao();

        }
        private void btnVendaVista_Click(object sender, EventArgs e)
        {
            finalizarVendaAvista();
            panelVenda.Enabled = false;
        }
        private void btnVendaPrazo_Click(object sender, EventArgs e)
        {
            finalizarVendaPrazo();
            bntConcluirVenda.Enabled = true;
            panelVenda.Enabled = false;
        }
        private void finalizarVendaPrazo()
        {
            var res = double.Parse(txtTotalVenda.Text, System.Globalization.NumberStyles.Currency);
            if (res > 0)
            {
                panelLancamento.Visible = false;
                panelVendaAvista.Visible = false;
                panelVendaAPrazo.Visible = true;
                this.panelVendaAPrazo.Location = new Point(7, 71);
                txtValorPagar.Text = txtTotalVenda.Text;
                lblValorPagar.Text = txtTotalVenda.Text;
                DateTime fimMes = DateTime.Now;
                DateTime vencimento = fimMes.AddDays(30);
                dtVencimento.Text = vencimento.ToString();
                txtClientes.Focus();
            }
            else
            {
                MessageBox.Show("Para finalizar um lançamento,  deve adiconar itens.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void finalizarVendaAvista()
        {
            var res = double.Parse(txtTotalVenda.Text, System.Globalization.NumberStyles.Currency);
            if (res > 0)
            {
                panelLancamento.Visible = false;
                panelVendaAPrazo.Visible = false;
                panelVendaAvista.Visible = true;
                this.panelVendaAvista.Location = new Point(7, 71);
                txtValorAvista.Text = txtTotalVenda.Text;
                lblValorAvista.Text = txtTotalVenda.Text;
                txtDinheiroAvista.Focus();
            }
            else
            {
                MessageBox.Show("Para finalizar um lançamento,  deve adiconar itens.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }
        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
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

                var Pagando = Double.Parse(txtValorAvista.Text, System.Globalization.NumberStyles.Currency);
                troco = (Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text)) - Pagando;
                labelTroco.Text = String.Format("{0:C2}", troco);
            }
            if (valorDescontado <= 0)
            {
                var vTotal = Double.Parse(txtValorAvista.Text, System.Globalization.NumberStyles.Currency);
                troco = (Convert.ToDouble(txtDinheiroAvista.Text) + Convert.ToDouble(txtPixAvista.Text) + Convert.ToDouble(txtCartaoAvista.Text)) - vTotal;
                labelTroco.Text = String.Format("{0:C2}", troco);
            }
        }


    }
}
