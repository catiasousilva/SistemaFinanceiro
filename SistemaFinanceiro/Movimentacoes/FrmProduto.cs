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

namespace SistemaHotel.Produtos
{
    public partial class FrmProdutos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        string foto;
        string alterou; //p/ uso de editar imagem
        string id;
        string codAntigo;
        bool sim = false;
        double vCompra, vVenda, tLancado;
        int Entrada;
        string ultimoIdGasto;

        public FrmProdutos()
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
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            //lblBuscaProduto.ForeColor = ThemeColor.SecondaryColor;
            lblTituloProdutos.ForeColor = ThemeColor.PrimaryColor;            
        }

        private void LimparFoto()
        {
            image.Image = Properties.Resources.sem_foto; //aqui coloca a imagem sem_foto na picture do form
            foto = "img/sem_foto.jpg"; //atribuindo um camiho de foto (assim este imagem tem q estar na pasta debug)
        }
        
        private void Formatargridum()
        {
            gridum.Columns[0].HeaderText = "ID";
            gridum.Columns[1].HeaderText = "Código";
            gridum.Columns[2].HeaderText = "Produto";
            gridum.Columns[3].HeaderText = "Descrição";
            gridum.Columns[4].HeaderText = "Estoque";
            gridum.Columns[5].HeaderText = "Fornecedor";

            gridum.Columns[6].HeaderText = "Entrada";
            gridum.Columns[7].HeaderText = "V. Pago";
            gridum.Columns[8].HeaderText = "Venda";
            gridum.Columns[9].HeaderText = "Custo Unit";          
            
            gridum.Columns[10].HeaderText = "Data";
            gridum.Columns[11].HeaderText = "Imagem";
            gridum.Columns[12].HeaderText = "Id fornecedor";
            gridum.Columns[13].HeaderText = "Mínimo";
            //gridum.Columns[14].HeaderText = "N.º Doc.";

            //gridum.Width[2] = "300";

            //formatar coluna  moeda
            gridum.Columns[7].DefaultCellStyle.Format = "c2";
            gridum.Columns[8].DefaultCellStyle.Format = "c2";
            gridum.Columns[9].DefaultCellStyle.Format = "c2";

            gridum.Columns[0].Visible = false;
            gridum.Columns[11].Visible = false;
            gridum.Columns[12].Visible = false;

        }
        private void Listarum()
        {
            // inner join 
            // adicionar uma nova coluna na grid sem ter criar a coluna na tabela, essa nova coluna é o nome do forncedor, poderia ser telefone ou outras.
            con.AbrirConexao();
            sql = "SELECT pro.id, pro.cod, pro.nome, pro.descricao, pro.estoque, forn.nome, pro.entrada, pro.total_compra, pro.valor_venda, pro.valor_compra, pro.data, pro.imagem, pro.fornecedor, pro.minimo, pro.nota  FROM produtos as pro INNER JOIN fornecedores as forn  ON pro.fornecedor = forn.id ORDER BY pro.nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridum.DataSource = dt;
            con.FecharConexao();

            Formatargridum();
        }
        private void BuscarNome()
        {
            con.AbrirConexao();
            sql = "SELECT pro.id, pro.cod, pro.nome, pro.descricao, pro.estoque, forn.nome, pro.entrada, pro.total_compra, pro.valor_compra, pro.valor_venda, pro.data, pro.imagem, pro.fornecedor, pro.minimo, pro.nota  FROM produtos as pro INNER JOIN fornecedores as forn  ON pro.fornecedor = forn.id WHERE pro.nome LIKE @nome ORDER BY pro.nome asc"; //LIKE, busca nome por aproximacao
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");  //"%" - operador LIKE, busca nome por aproximacao
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridum.DataSource = dt;
            con.FecharConexao();

            Formatargridum();
        }
        private void BuscarCod()
        {
            con.AbrirConexao();
            sql = "SELECT pro.id, pro.cod, pro.nome, pro.descricao, pro.estoque, forn.nome, pro.entrada, pro.total_compra, pro.valor_compra, pro.valor_venda, pro.data, pro.imagem, pro.fornecedor, pro.minimo, pro.nota  FROM produtos as pro INNER JOIN fornecedores as forn  ON pro.fornecedor = forn.id WHERE pro.cod = @cod ORDER BY pro.nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cod", txtBuscarCod.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridum.DataSource = dt;
            con.FecharConexao();

            Formatargridum();
        }
        private void CarregarCampos()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores ORDER BY nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbFornecedor.DataSource = dt;
            cbFornecedor.ValueMember = "id"; //vou usar o ID para relacionar o Produto com o Fornecedor
            cbFornecedor.DisplayMember = "nome"; //aqui mostrar o nome no comboBox
            con.FecharConexao();
        }
        private void HabilitarCampos()
        {

            panelStatus.Enabled = true;
            panelValores.Enabled = true;
            panelEstoque.Enabled = true;
            txtCodBarra.Enabled = true;
            btnImg.Enabled = true;
            txtCodBarra.Focus();
        }
        private void DesabilitarCampos()
        {
            panelStatus.Enabled = false;
            panelValores.Enabled = false;
            panelEstoque.Enabled = false;
            btnImg.Enabled = false;
            
        }
        private void LimparCampos()
        {
            txtCodBarra.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtUnitario.Text = "";
            txtEntrada.Text = "0";
           
            txtCompra.Text = "";
            txtValorVenda.Text = "";
            txtEstoque.Text = "";
            txtMinimo.Text = "";
            //txtNota.Text = "";
            LimparFoto();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            LimparFoto();
            CarregarCampos();
            Listarum();
            LoadTheme();            
        }
        private void verificar()
        {
            //Verificar se cod ja existe  
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE cod = @cod", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            cmdVerificar.Parameters.AddWithValue("@cod", txtCodBarra.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Código de barra já registrado", "Cadastro de produtos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCodBarra.Text = "";
                txtCodBarra.Focus();
                return;
            }
        }
        private void botnNovo_Click(object sender, EventArgs e)
        {           
            sim = true;
            gridum.Enabled = false;
            
            HabilitarCampos();

            panelEntrada.Enabled = false;

            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = false;
           
            

            LimparCampos();
            txtCodBarra.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {            
            if (txtCodBarra.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo Código de barra", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                panelStatus.Enabled = true;
                txtCodBarra.Focus();
                return;
            }
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo nome", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                panelStatus.Enabled = true;
                txtNome.Focus();
                return;
            }
            
            if (int.Parse(txtEntrada.Text) < 1)
            {
                MessageBox.Show("Preencha quantidade de Entrada", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEntrada.Text = "";
                txtEntrada.Focus();
                return;
            }
            if (vCompra < 1)
            {
                MessageBox.Show("Preencha o valor total da compra", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompra.Focus();
                return;
            }
            if (vVenda < 1)
            {
                MessageBox.Show("Preencha o valor de venda", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorVenda.Focus();
                return;
            }
            vCompra = Convert.ToDouble(txtCompra.Text);
            vVenda = Convert.ToDouble(txtValorVenda.Text);
            Entrada = int.Parse(txtEntrada.Text);

            //botao salvar
            con.AbrirConexao();
            sql = "INSERT INTO produtos(cod, nome, descricao, estoque, fornecedor, entrada, total_compra, valor_venda, valor_compra, data, imagem, minimo) VALUES(@cod, @nome, @descricao, @estoque, @fornecedor, @entrada, @total_compra, @valor_venda, @valor_compra, curDate(), @imagem, @minimo)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cod", txtCodBarra.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@estoque", txtEntrada.Text);
            cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.SelectedValue); //aqui eu quero o valor selecionado que é o ID. p/ fazer o relacionamento.
            cmd.Parameters.AddWithValue("@entrada", txtEntrada.Text);
            cmd.Parameters.AddWithValue("@total_compra", Convert.ToDouble(txtCompra.Text));
            cmd.Parameters.AddWithValue("@valor_compra", Convert.ToDouble(txtUnitario.Text));
            cmd.Parameters.AddWithValue("@valor_venda", Convert.ToDouble(txtValorVenda.Text));
            cmd.Parameters.AddWithValue("@imagem", img()); //img() é o metodo criado p/ tratar imagem p/ o bd.
            cmd.Parameters.AddWithValue("@minimo", txtMinimo.Text);
            //cmd.Parameters.AddWithValue("@nota", txtNota.Text);

            verificar();            
            cmd.ExecuteNonQuery();
            con.FecharConexao();
                        


            ////
            ////TESTE DE LANÇAMENTO DE GASTO E MOVIMENTACAO AO CADASTRAR PRODUTO
            ////
            ////IMPORTANTE, 1º vem a tabela de gastos, para que possa ter o id do gasto e usar na movimentacoes
            ////lançar tab de Gastos
            //con.AbrirConexao();
            //sql = "INSERT INTO gastos(descricao, valor, nota, forma_pagto, funcionario, data) VALUES(@descricao, @valor, @nota, @forma_pagto, @funcionario, curDate())";
            //cmd = new MySqlCommand(sql, con.con);
            //cmd.Parameters.AddWithValue("@descricao", "Compra de Produtos");
            //cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtCompra.Text));
            //cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            //cmd.Parameters.AddWithValue("@forma_pagto", cbPagto.Text);
            //cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            //cmd.ExecuteNonQuery();
            //con.FecharConexao();
            //// fim Gastos

            ////recuperar ultimo id do GASTO
            //con.AbrirConexao();
            //MySqlCommand cmdVerificar;
            //MySqlDataReader reader; //com o reader vou conseguir extrair dados da tabela e usar em outros form

            //cmdVerificar = new MySqlCommand("SELECT id FROM gastos ORDER BY id DESC LIMIT 1", con.con);
            //MySqlDataAdapter da = new MySqlDataAdapter();
            //da.SelectCommand = cmdVerificar;
            //reader = cmdVerificar.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    //extraíndo dados do id
            //    while (reader.Read())
            //    {
            //        ultimoIdGasto = Convert.ToString(reader["id"]);
            //    }
            //}
            ////fim recuperar ultimo id do GASTO

            ////lançar valor do pedido nas movimentacoes
            //con.AbrirConexao();
            //sql = "INSERT INTO movimentacoes(quarto, hospede, tipo, movimento, descricao, valor, desconto, cartao, dinheiro, valor_pago, funcionario, data, id_movimento, exclusao) VALUES(@quarto, @hospede, @tipo, @movimento,  @descricao, @valor, @desconto, @cartao, @dinheiro, @valor_pago, @funcionario, curDate(), @id_movimento, @exclusao)";
            //cmd = new MySqlCommand(sql, con.con);
            //cmd.Parameters.AddWithValue("@quarto", "N/A");
            //cmd.Parameters.AddWithValue("@hospede", "N/A");
            //cmd.Parameters.AddWithValue("@tipo", "Saída");
            //cmd.Parameters.AddWithValue("@movimento", "Gastos");
            //cmd.Parameters.AddWithValue("@descricao", "Compra de Produtos");
            //cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtCompra.Text));
            //cmd.Parameters.AddWithValue("@desconto", 0);
            //cmd.Parameters.AddWithValue("@cartao", 0);
            //cmd.Parameters.AddWithValue("@dinheiro", 0);
            //cmd.Parameters.AddWithValue("@valor_pago", Convert.ToDouble(txtCompra.Text));
            //cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            ////cmd.Parameters.AddWithValue("@id_movimento", "0"); //se nao precisar desse id_movimento, coloca zero
            //cmd.Parameters.AddWithValue("@id_movimento", ultimoIdGasto);
            //cmd.Parameters.AddWithValue("@exclusao", 0);

            //cmd.ExecuteNonQuery();
            //con.FecharConexao();
            ////fim lançar valor do pedido nas movimentacoes
            ////
            ////FIM TESTE
            ////

            panelEntrada.Enabled = false;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnFinalizar.Enabled = true;
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;

            //DesabilitarCampos();
            gridum.Enabled = false;
            LimparCampos();
            gridum.Visible = true;
            Listarum();
            sim = true;
            MessageBox.Show("Registro Salvo com sucesso!", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }
        //metodo finalizando entrada de mercadoria e finalizando total do lançamento da compra.
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            tLancado += Convert.ToDouble(txtCompra.Text);


            //limpando campos:
            panelEntrada.Enabled = false;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnFinalizar.Enabled = false;
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;

            DesabilitarCampos();
            gridum.Enabled = true;
            LimparCampos();
            gridum.Visible = true;
            Listarum();
            sim = true;
            MessageBox.Show("Lançamento Salvo com sucesso!", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        //Metodo De EDIÇÃO dos produtos
        private void EditarProdutos()
        {
            if (txtCodBarra.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo código", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo nome", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (txtValorVenda.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo Valor", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            //botao editar
            con.AbrirConexao();
            if (alterou == "1")
            {
                sql = "UPDATE produtos SET cod = @cod, nome = @nome, descricao = @descricao, fornecedor = @fornecedor, valor_venda = @valor_venda, imagem = @imagem, minimo = @minimo where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@imagem", img());
            }
            else
            {
                sql = "UPDATE produtos SET cod = @cod, nome = @nome, descricao = @descricao, estoque = @estoque, fornecedor = @fornecedor, entrada =@entrada, total_compra = @total_compra, valor_venda = @valor_venda, valor_compra = @valor_compra, minimo = @minimo, nota = @nota where id = @id";
                cmd = new MySqlCommand(sql, con.con);
            }

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cod", txtCodBarra.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@estoque", Convert.ToInt32(txtEstoque.Text) + Convert.ToInt32(txtEntrada.Text));
            cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.SelectedValue);
            cmd.Parameters.AddWithValue("@entrada", txtEntrada.Text);
            cmd.Parameters.AddWithValue("@total_compra", txtCompra.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@valor_compra", txtUnitario.Text.Replace(",", "."));//o replace substitui "???" por "??"
            cmd.Parameters.AddWithValue("@valor_venda", txtValorVenda.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@minimo", txtMinimo.Text);
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);


            //Verificar se códigp ja existe
            if (txtCodBarra.Text != codAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE cod = @cod", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@cod", txtCodBarra.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Código já registrado", "Cadastro de produtos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodBarra.Text = "";
                    txtCodBarra.Focus();
                    return;
                }

            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();
            MessageBox.Show("Registro Editado com sucesso!", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;
            
            DesabilitarCampos();
            gridum.Enabled = true;
            LimparCampos();
            Listarum();
            alterou = "";
            sim = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarProdutos();
            panelEntrada.Enabled = false;
            gridum.ClearSelection();
            
        }
        private void btnGravarRegistro_Click(object sender, EventArgs e)
        {            
            if (int.Parse(txtEntrada.Text) <1)
            {
                MessageBox.Show("Informe a quantidade", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEntrada.Focus();
                return;
            }
            if (double.Parse(txtCompra.Text) <0)
            {
                MessageBox.Show("Informe o valor da compra", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompra.Focus();
                return;
            }
            if (double.Parse(txtValorVenda.Text) < 0)
            {
                MessageBox.Show("Informe o valor de venda", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtValorVenda.Focus();
                return;
            }

            //EDITANDO ONOME
            EditarProdutos();

            //
            //TESTE DE LANÇAMENTO DE GASTO E MOVIMENTACAO AO DÁ ENTRADA PRODUTO
            //

            //IMPORTANTE, 1º vem a tabela de gastos, para que possa ter o id do gasto e usar na movimentacoes
            //lançar tab de Gastos
            con.AbrirConexao();
            sql = "INSERT INTO gastos(descricao, valor, nota, funcionario, data) VALUES(@descricao, @valor, @nota, @funcionario, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@descricao", "Compra de Produtos");
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtCompra.Text));
            cmd.Parameters.AddWithValue("@nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            cmd.ExecuteNonQuery();
            con.FecharConexao();
            // fim Gastos

            //recuperar ultimo id do GASTO
            con.AbrirConexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader; //com o reader vou conseguir extrair dados da tabela e usar em outros form

            cmdVerificar = new MySqlCommand("SELECT id FROM gastos ORDER BY id DESC LIMIT 1", con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {
                //extraíndo dados do id
                while (reader.Read())
                {
                    ultimoIdGasto = Convert.ToString(reader["id"]);
                }
            }
            //fim recuperar ultimo id do GASTO

            //lançar valor do pedido nas movimentacoes
            con.AbrirConexao();
            sql = "INSERT INTO movimentacoes(quarto, hospede, tipo, movimento, descricao, valor, desconto, cartao, dinheiro, valor_pago, funcionario, data, id_movimento, exclusao) VALUES(@quarto, @hospede, @tipo, @movimento,  @descricao, @valor, @desconto, @cartao, @dinheiro, @valor_pago, @funcionario, curDate(), @id_movimento, @exclusao)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@quarto", "N/A");
            cmd.Parameters.AddWithValue("@hospede", "N/A");
            cmd.Parameters.AddWithValue("@tipo", "Saída");
            cmd.Parameters.AddWithValue("@movimento", "Gastos");
            cmd.Parameters.AddWithValue("@descricao", "Compra de Produtos");
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtCompra.Text));
            cmd.Parameters.AddWithValue("@desconto", 0);
            cmd.Parameters.AddWithValue("@cartao", 0);
            cmd.Parameters.AddWithValue("@dinheiro", 0);
            cmd.Parameters.AddWithValue("@valor_pago", Convert.ToDouble(txtCompra.Text));
            cmd.Parameters.AddWithValue("@funcionario", Program.NomeUsuario);
            //cmd.Parameters.AddWithValue("@id_movimento", "0"); //se nao precisar desse id_movimento, coloca zero
            cmd.Parameters.AddWithValue("@id_movimento", ultimoIdGasto);
            cmd.Parameters.AddWithValue("@exclusao", 0);

            cmd.ExecuteNonQuery();
            con.FecharConexao();
            //fim lançar valor do pedido nas movimentacoes


            //
            //FIM TESTE
            //





            panelEntrada.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o prooduto " + txtNome.Text+ "?", "Cadastro produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                //botao excluir
                con.AbrirConexao();
                sql = "DELETE FROM produtos WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                Listarum();
                MessageBox.Show("Registro Excluído com sucesso!", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cancelar();
                gridum.Enabled = true;
                panelEntrada.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
            gridum.Enabled = true;
            panelEntrada.Enabled = false;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;

            return;
        }
        private void btnImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "Arquivos(*.jpg)|*.jpg | Arquivos(*.PNG)| *.png;| All (*.*) | *.*"; //mostra uma de cada vez
            dialog.Filter = "Imagens(*.jpg; *.png) | *.jpg;*.png"; //mostra jpg e png
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString(); //pegando o caminho da imagem q selecionei e dei ok
                image.ImageLocation = foto; //jogando caminho da imagem p/ componete img p/ exibir no form
                alterou = "1"; //p/ uso editar alterando a imagem
            }
        }

        private void gridum_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                sim = false;
                panelEntrada.Enabled = true;
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                btnEntrada.Enabled = true;
                btnGravarRegistro.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = true;
                
                
                

                lblalterarcod.Visible = true;
                HabilitarCampos();

                txtEstoque.Enabled = false;

                id = gridum.CurrentRow.Cells[0].Value.ToString();
                codAntigo = gridum.CurrentRow.Cells[1].Value.ToString();                
                txtCodBarra.Text = gridum.CurrentRow.Cells[1].Value.ToString();
                txtNome.Text = gridum.CurrentRow.Cells[2].Value.ToString();
                txtDescricao.Text = gridum.CurrentRow.Cells[3].Value.ToString();
                txtEstoque.Text = gridum.CurrentRow.Cells[4].Value.ToString();
                cbFornecedor.Text = gridum.CurrentRow.Cells[5].Value.ToString();

                txtEntrada.Text = gridum.CurrentRow.Cells[6].Value.ToString();
                txtCompra.Text = gridum.CurrentRow.Cells[7].Value.ToString();
                txtValorVenda.Text = gridum.CurrentRow.Cells[8].Value.ToString();
                txtUnitario.Text = gridum.CurrentRow.Cells[9].Value.ToString();
                txtMinimo.Text = gridum.CurrentRow.Cells[13].Value.ToString();
                txtNota.Text = gridum.CurrentRow.Cells[14].Value.ToString();
                //Img.Image = grid.CurrentRow.Cells[6].Value.ToString(); isso nao exite, deve se fazer a conversao.

                //recuperando imagem do banco de dado para a grid, fazendo a devida conversao      
                //testando o campo imagem se é nulo antes
                if (gridum.CurrentRow.Cells[11].Value != DBNull.Value) //DBNull.Value campo quem do Banco de dado
                {
                    byte[] imagem = (byte[])gridum.CurrentRow.Cells[11].Value; //criada var byte[] imagem p/ receber convetido em byte[] o que vem da grid
                    MemoryStream ms = new MemoryStream(imagem); //recebe a var byte[] q já contem o valor da grid (decodificao/ covertido)

                    //o componete 'Image' sempre pede um 'System.Drawing' entao...
                    image.Image = System.Drawing.Image.FromStream(ms); //passando o memorystream no objeto q ele recebe um System.Drawing e seu parametor  FromStream() q vai receber o memorystream

                }
                else
                {
                    image.Image = Properties.Resources.sem_foto; //aqui coloca a imagem sem_foto na picture do form
                }
                txtCodBarra.Enabled = false;
            }
            else
            {
                Cancelar();
                return;
            }
            
        }
        private void Cancelar()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;
            lblalterarcod.Visible = false;
            DesabilitarCampos();
            LimparCampos();
            Listarum();
            sim = true;
        }
        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
            gridum.Visible = true;
        }

        private void txtBuscarCod_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarCod.Text == "")
            {
                Listarum();
            }
            else
            {
                BuscarCod();
                gridum.Visible = true;
            }

            
        }

        private void gridum_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (Program.ChamadaProdutos == "estoque")
            {
                Program.IdProduto = gridum.CurrentRow.Cells[0].Value.ToString();
                Program.NomeProduto = gridum.CurrentRow.Cells[2].Value.ToString();
                Program.EstoqueProduto = gridum.CurrentRow.Cells[4].Value.ToString();
                Program.ValorProduto = gridum.CurrentRow.Cells[8].Value.ToString(); //venda
                
                Movimentacoes.FrmVendas frm = new Movimentacoes.FrmVendas();

                frm.txtProduto.Text = Program.NomeProduto;
                frm.txtEstoque.Text = Program.EstoqueProduto;
                frm.txtValor.Text = Program.ValorProduto;
                Close();
            }
            else
            {
                Produtos.FrmEstoque frm = new FrmEstoque();
                frm.ShowDialog();
            }
        }
        private byte[] img() //este metodo é padrao,  serve sempre q deseja enviar uma imagem para p bando de dado
        {
            byte[] imagem_byte = null; //*essa var vou usar p/ enviar o comprimento da imagem 
            if (foto =="") //a string foto, nunca vai estar vazia, pq no metodo LimparFoto() foi passado o caminho de uma imagem 'sem_foto' 
            {
                return  null;
            }
            
            //usar o FileStream p/ enviar imagem p/ o BD e tres paramento 'local(foto), tipo de imagem(FileMode), tipo de acesso(FileAcess)'
            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read); //isso aqui é padrao, 

            BinaryReader br = new BinaryReader(fs); //serve para trabalhar com o FileStream

            imagem_byte = br.ReadBytes((int)fs.Length); //*pegAndo o comprimento de FileStream jogando dentro  de uma tipo IMAGEM BYTE

            return imagem_byte;
        }

        private void txtCodBarra_Leave(object sender, EventArgs e)
        {
            if (sim == true)
            {
                con.AbrirConexao();
                verificar();
                con.FecharConexao();
            }            
        }

        private void txtCodBarra_Click(object sender, EventArgs e)
        {
            sim = true;
            btnCancelar.Enabled = true;
        }

        private void lblalterarcod_Click(object sender, EventArgs e)
        {
            txtCodBarra.Enabled = true;
            txtCodBarra.Focus();
            sim = false;
            lblalterarcod.Visible = false;
        }
        //APENAS NUMEROS
        private void formatarTextNumero(object sender, KeyPressEventArgs e)
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

        //TextBox ao digitar apresenta no formato moeda
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

        private void txtCodBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {            
            formatarTextNumero(sender, e);
        }

        private void txtMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtBuscarCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtCompra_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtCompra); 
            
            vCompra = Convert.ToDouble(txtCompra.Text);
            Entrada = Convert.ToInt32(txtEntrada.Text);
            var rs = vCompra / Entrada;
            txtUnitario.Text = rs.ToString();
        }       

        private void cbFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //bloqueia a digitação,
            e.Handled = true;
        }

        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        private void txtValorVenda_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtValorVenda);
            vVenda = Convert.ToDouble(txtValorVenda.Text);
        }

        private void txtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            formatarTextNumero(sender, e);
        }

        //dá entrada nas mercadorias....
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if (int.Parse(gridum.CurrentRow.Cells[0].Value.ToString()) > -1)
            {
                sim = false;
                panelEntrada.Enabled = true;
                btnEntrada.Enabled = false;
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                btnGravarRegistro.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;

                lblalterarcod.Visible = true;
                HabilitarCampos();

                txtEstoque.Enabled = false;

                id = gridum.CurrentRow.Cells[0].Value.ToString();
                codAntigo = gridum.CurrentRow.Cells[1].Value.ToString();
                txtCodBarra.Text = gridum.CurrentRow.Cells[1].Value.ToString();
                txtNome.Text = gridum.CurrentRow.Cells[2].Value.ToString();
                txtDescricao.Text = gridum.CurrentRow.Cells[3].Value.ToString();
                txtEstoque.Text = gridum.CurrentRow.Cells[4].Value.ToString();
                cbFornecedor.Text = gridum.CurrentRow.Cells[5].Value.ToString();

                txtEntrada.Text = "0";
                txtCompra.Text = "0";
                txtValorVenda.Text = gridum.CurrentRow.Cells[8].Value.ToString();
                txtUnitario.Text = gridum.CurrentRow.Cells[9].Value.ToString();
                txtMinimo.Text = gridum.CurrentRow.Cells[13].Value.ToString();

                txtNota.Text = "";
                //Img.Image = grid.CurrentRow.Cells[6].Value.ToString(); isso nao exite, deve se fazer a conversao.

                //recuperando imagem do banco de dado para a grid, fazendo a devida conversao      
                //testando o campo imagem se é nulo antes
                if (gridum.CurrentRow.Cells[11].Value != DBNull.Value) //DBNull.Value campo quem do Banco de dado
                {
                    byte[] imagem = (byte[])gridum.CurrentRow.Cells[11].Value; //criada var byte[] imagem p/ receber convetido em byte[] o que vem da grid
                    MemoryStream ms = new MemoryStream(imagem); //recebe a var byte[] q já contem o valor da grid (decodificao/ covertido)

                    //o componete 'Image' sempre pede um 'System.Drawing' entao...
                    image.Image = System.Drawing.Image.FromStream(ms); //passando o memorystream no objeto q ele recebe um System.Drawing e seu parametor  FromStream() q vai receber o memorystream

                }
                else
                {
                    image.Image = Properties.Resources.sem_foto; //aqui coloca a imagem sem_foto na picture do form
                }
                txtCodBarra.Enabled = false;
                txtEntrada.Focus();
            }
            else
            {
                MessageBox.Show("Selecione o item a ser alterado", "Cadastro produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Cancelar();
                return;
            }

        }

        private void txtNome_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
        }

        private void cbFornecedor_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
        }

        

        private void txtEntrada_Click(object sender, EventArgs e)
        {
            txtEntrada.Text="0";

            txtCompra.Text = "";
            txtValorVenda.Text = "";
        }        

        private void txtDescricao_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
        }



    }//fim
}
