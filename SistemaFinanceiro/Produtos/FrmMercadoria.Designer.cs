namespace SistemaHotel.Produtos
{
    partial class FrmProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProdutos));
            this.cbFornecedor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuscarNome = new System.Windows.Forms.TextBox();
            this.lblBuscaProduto = new System.Windows.Forms.Label();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValorVenda = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodBarra = new System.Windows.Forms.TextBox();
            this.txtBuscarCod = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMinimo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTituloProdutos = new System.Windows.Forms.Label();
            this.labelescolher = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIncluirRegistro = new System.Windows.Forms.Label();
            this.lblAlterar = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gridum = new System.Windows.Forms.DataGridView();
            this.lblalterarcod = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCompra = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtUnitario = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panelBusca = new System.Windows.Forms.Panel();
            this.panelValores = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.panelEstoque = new System.Windows.Forms.Panel();
            this.cbPagto = new System.Windows.Forms.ComboBox();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblEntrada = new System.Windows.Forms.Label();
            this.panelIncluirRegistro = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.panelEntrada = new System.Windows.Forms.Panel();
            this.btnGravarRegistro = new System.Windows.Forms.Button();
            this.lblGravarRegistro = new System.Windows.Forms.Label();
            this.btnEntrada = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnImg = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridum)).BeginInit();
            this.panelBusca.SuspendLayout();
            this.panelValores.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelEstoque.SuspendLayout();
            this.panelIncluirRegistro.SuspendLayout();
            this.panelEntrada.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFornecedor
            // 
            this.cbFornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFornecedor.FormattingEnabled = true;
            this.cbFornecedor.Location = new System.Drawing.Point(3, 62);
            this.cbFornecedor.MaxDropDownItems = 15;
            this.cbFornecedor.Name = "cbFornecedor";
            this.cbFornecedor.Size = new System.Drawing.Size(203, 21);
            this.cbFornecedor.TabIndex = 3;
            this.cbFornecedor.Click += new System.EventHandler(this.cbFornecedor_Click);
            this.cbFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFornecedor_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(215, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Fornecedor";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(3, 88);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(203, 20);
            this.txtDescricao.TabIndex = 4;
            this.txtDescricao.Text = "UN";
            this.txtDescricao.Click += new System.EventHandler(this.txtDescricao_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(214, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Embalagem";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(3, 36);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(203, 20);
            this.txtNome.TabIndex = 2;
            this.txtNome.Click += new System.EventHandler(this.txtNome_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(186, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Nome do Produto";
            // 
            // txtBuscarNome
            // 
            this.txtBuscarNome.Location = new System.Drawing.Point(48, 70);
            this.txtBuscarNome.Name = "txtBuscarNome";
            this.txtBuscarNome.Size = new System.Drawing.Size(120, 20);
            this.txtBuscarNome.TabIndex = 21;
            this.txtBuscarNome.TextChanged += new System.EventHandler(this.txtBuscarNome_TextChanged);
            // 
            // lblBuscaProduto
            // 
            this.lblBuscaProduto.AutoSize = true;
            this.lblBuscaProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscaProduto.ForeColor = System.Drawing.Color.White;
            this.lblBuscaProduto.Location = new System.Drawing.Point(5, 4);
            this.lblBuscaProduto.Name = "lblBuscaProduto";
            this.lblBuscaProduto.Size = new System.Drawing.Size(159, 24);
            this.lblBuscaProduto.TabIndex = 37;
            this.lblBuscaProduto.Text = "Busca de produto";
            // 
            // txtEstoque
            // 
            this.txtEstoque.Enabled = false;
            this.txtEstoque.Location = new System.Drawing.Point(4, 88);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(80, 20);
            this.txtEstoque.TabIndex = 101;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(703, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Estoque atual";
            // 
            // txtValorVenda
            // 
            this.txtValorVenda.Location = new System.Drawing.Point(5, 88);
            this.txtValorVenda.Name = "txtValorVenda";
            this.txtValorVenda.Size = new System.Drawing.Size(67, 20);
            this.txtValorVenda.TabIndex = 7;
            this.txtValorVenda.TextChanged += new System.EventHandler(this.txtValorVenda_TextChanged);
            this.txtValorVenda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(526, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Valor Venda";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(194, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Código de barra";
            // 
            // txtCodBarra
            // 
            this.txtCodBarra.Location = new System.Drawing.Point(3, 10);
            this.txtCodBarra.MaxLength = 13;
            this.txtCodBarra.Name = "txtCodBarra";
            this.txtCodBarra.Size = new System.Drawing.Size(203, 20);
            this.txtCodBarra.TabIndex = 1;
            this.txtCodBarra.Click += new System.EventHandler(this.txtCodBarra_Click);
            this.txtCodBarra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodBarra_KeyPress);
            this.txtCodBarra.Leave += new System.EventHandler(this.txtCodBarra_Leave);
            // 
            // txtBuscarCod
            // 
            this.txtBuscarCod.Location = new System.Drawing.Point(48, 38);
            this.txtBuscarCod.MaxLength = 13;
            this.txtBuscarCod.Name = "txtBuscarCod";
            this.txtBuscarCod.Size = new System.Drawing.Size(120, 20);
            this.txtBuscarCod.TabIndex = 20;
            this.txtBuscarCod.TextChanged += new System.EventHandler(this.txtBuscarCod_TextChanged);
            this.txtBuscarCod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarCod_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Produto";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Código";
            // 
            // txtMinimo
            // 
            this.txtMinimo.Location = new System.Drawing.Point(4, 58);
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.Size = new System.Drawing.Size(80, 20);
            this.txtMinimo.TabIndex = 8;
            this.txtMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinimo_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(692, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 69;
            this.label10.Text = "Estoque mínimo";
            // 
            // lblTituloProdutos
            // 
            this.lblTituloProdutos.AutoSize = true;
            this.lblTituloProdutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloProdutos.Location = new System.Drawing.Point(4, 31);
            this.lblTituloProdutos.Name = "lblTituloProdutos";
            this.lblTituloProdutos.Size = new System.Drawing.Size(373, 26);
            this.lblTituloProdutos.TabIndex = 72;
            this.lblTituloProdutos.Text = "GERENCIAMENTO DE PRODUTOS";
            // 
            // labelescolher
            // 
            this.labelescolher.AutoSize = true;
            this.labelescolher.ForeColor = System.Drawing.Color.White;
            this.labelescolher.Location = new System.Drawing.Point(762, 18);
            this.labelescolher.Name = "labelescolher";
            this.labelescolher.Size = new System.Drawing.Size(87, 13);
            this.labelescolher.TabIndex = 74;
            this.labelescolher.Text = "Escolher imagem";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Novo produto";
            // 
            // lblIncluirRegistro
            // 
            this.lblIncluirRegistro.AutoSize = true;
            this.lblIncluirRegistro.Location = new System.Drawing.Point(137, 36);
            this.lblIncluirRegistro.Name = "lblIncluirRegistro";
            this.lblIncluirRegistro.Size = new System.Drawing.Size(72, 13);
            this.lblIncluirRegistro.TabIndex = 77;
            this.lblIncluirRegistro.Text = "Incluir registro";
            // 
            // lblAlterar
            // 
            this.lblAlterar.AutoSize = true;
            this.lblAlterar.Location = new System.Drawing.Point(203, 36);
            this.lblAlterar.Name = "lblAlterar";
            this.lblAlterar.Size = new System.Drawing.Size(79, 13);
            this.lblAlterar.TabIndex = 78;
            this.lblAlterar.Text = "Alterar Registro";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(285, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 79;
            this.label13.Text = "Excluir";
            // 
            // gridum
            // 
            this.gridum.AllowUserToAddRows = false;
            this.gridum.AllowUserToDeleteRows = false;
            this.gridum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridum.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridum.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.gridum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridum.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridum.ColumnHeadersHeight = 30;
            this.gridum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridum.EnableHeadersVisualStyles = false;
            this.gridum.GridColor = System.Drawing.Color.SteelBlue;
            this.gridum.Location = new System.Drawing.Point(3, 195);
            this.gridum.Name = "gridum";
            this.gridum.ReadOnly = true;
            this.gridum.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridum.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridum.RowHeadersVisible = false;
            this.gridum.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.gridum.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridum.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridum.Size = new System.Drawing.Size(1126, 321);
            this.gridum.TabIndex = 130;
            this.gridum.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridum_CellClick);
            this.gridum.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridum_CellDoubleClick);
            // 
            // lblalterarcod
            // 
            this.lblalterarcod.AutoSize = true;
            this.lblalterarcod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblalterarcod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblalterarcod.Location = new System.Drawing.Point(402, 54);
            this.lblalterarcod.Name = "lblalterarcod";
            this.lblalterarcod.Size = new System.Drawing.Size(92, 13);
            this.lblalterarcod.TabIndex = 131;
            this.lblalterarcod.Text = "alterar código?";
            this.lblalterarcod.Visible = false;
            this.lblalterarcod.Click += new System.EventHandler(this.lblalterarcod_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(183, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 140;
            this.label14.Text = "Cancelar";
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(4, 10);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(68, 20);
            this.txtEntrada.TabIndex = 5;
            this.txtEntrada.Click += new System.EventHandler(this.txtEntrada_Click);
            this.txtEntrada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEntrada_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(547, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 142;
            this.label15.Text = "Entrada";
            // 
            // txtCompra
            // 
            this.txtCompra.Location = new System.Drawing.Point(4, 36);
            this.txtCompra.Name = "txtCompra";
            this.txtCompra.Size = new System.Drawing.Size(68, 20);
            this.txtCompra.TabIndex = 6;
            this.txtCompra.TextChanged += new System.EventHandler(this.txtCompra_TextChanged);
            this.txtCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompra_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(521, 106);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 144;
            this.label16.Text = "Valor Compra";
            // 
            // txtUnitario
            // 
            this.txtUnitario.Location = new System.Drawing.Point(5, 62);
            this.txtUnitario.Name = "txtUnitario";
            this.txtUnitario.Size = new System.Drawing.Size(67, 20);
            this.txtUnitario.TabIndex = 145;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(521, 135);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 13);
            this.label17.TabIndex = 146;
            this.label17.Text = "Valor Unitário";
            // 
            // panelBusca
            // 
            this.panelBusca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelBusca.Controls.Add(this.txtBuscarCod);
            this.panelBusca.Controls.Add(this.lblBuscaProduto);
            this.panelBusca.Controls.Add(this.txtBuscarNome);
            this.panelBusca.Controls.Add(this.label8);
            this.panelBusca.Controls.Add(this.label9);
            this.panelBusca.Location = new System.Drawing.Point(5, 70);
            this.panelBusca.Name = "panelBusca";
            this.panelBusca.Size = new System.Drawing.Size(176, 118);
            this.panelBusca.TabIndex = 147;
            // 
            // panelValores
            // 
            this.panelValores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(95)))));
            this.panelValores.Controls.Add(this.txtEntrada);
            this.panelValores.Controls.Add(this.txtCompra);
            this.panelValores.Controls.Add(this.txtUnitario);
            this.panelValores.Controls.Add(this.txtValorVenda);
            this.panelValores.Location = new System.Drawing.Point(592, 70);
            this.panelValores.Name = "panelValores";
            this.panelValores.Size = new System.Drawing.Size(80, 118);
            this.panelValores.TabIndex = 148;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(95)))));
            this.panelStatus.Controls.Add(this.txtCodBarra);
            this.panelStatus.Controls.Add(this.cbFornecedor);
            this.panelStatus.Controls.Add(this.txtNome);
            this.panelStatus.Controls.Add(this.txtDescricao);
            this.panelStatus.Enabled = false;
            this.panelStatus.Location = new System.Drawing.Point(277, 70);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(213, 118);
            this.panelStatus.TabIndex = 149;
            // 
            // panelEstoque
            // 
            this.panelEstoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(95)))));
            this.panelEstoque.Controls.Add(this.cbPagto);
            this.panelEstoque.Controls.Add(this.txtNota);
            this.panelEstoque.Controls.Add(this.txtMinimo);
            this.panelEstoque.Controls.Add(this.txtEstoque);
            this.panelEstoque.Enabled = false;
            this.panelEstoque.Location = new System.Drawing.Point(779, 70);
            this.panelEstoque.Name = "panelEstoque";
            this.panelEstoque.Size = new System.Drawing.Size(91, 118);
            this.panelEstoque.TabIndex = 149;
            // 
            // cbPagto
            // 
            this.cbPagto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPagto.FormattingEnabled = true;
            this.cbPagto.Items.AddRange(new object[] {
            "Á vista",
            "Prazo"});
            this.cbPagto.Location = new System.Drawing.Point(4, 35);
            this.cbPagto.MaxDropDownItems = 15;
            this.cbPagto.Name = "cbPagto";
            this.cbPagto.Size = new System.Drawing.Size(80, 21);
            this.cbPagto.TabIndex = 5;
            // 
            // txtNota
            // 
            this.txtNota.Location = new System.Drawing.Point(4, 13);
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(80, 20);
            this.txtNota.TabIndex = 102;
            this.txtNota.Text = "0";
            this.txtNota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNota_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(712, 83);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 13);
            this.label18.TabIndex = 103;
            this.label18.Text = "N.º do Doc.";
            // 
            // lblEntrada
            // 
            this.lblEntrada.AutoSize = true;
            this.lblEntrada.Location = new System.Drawing.Point(32, 36);
            this.lblEntrada.Name = "lblEntrada";
            this.lblEntrada.Size = new System.Drawing.Size(44, 13);
            this.lblEntrada.TabIndex = 151;
            this.lblEntrada.Text = "Entrada";
            // 
            // panelIncluirRegistro
            // 
            this.panelIncluirRegistro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(120)))));
            this.panelIncluirRegistro.Controls.Add(this.label12);
            this.panelIncluirRegistro.Controls.Add(this.btnFinalizar);
            this.panelIncluirRegistro.Controls.Add(this.lblIncluirRegistro);
            this.panelIncluirRegistro.Controls.Add(this.btnNovo);
            this.panelIncluirRegistro.Controls.Add(this.label1);
            this.panelIncluirRegistro.Controls.Add(this.btnSalvar);
            this.panelIncluirRegistro.Location = new System.Drawing.Point(0, 522);
            this.panelIncluirRegistro.Name = "panelIncluirRegistro";
            this.panelIncluirRegistro.Size = new System.Drawing.Size(370, 53);
            this.panelIncluirRegistro.TabIndex = 154;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(250, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 79;
            this.label12.Text = "Finalizar registro";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.FlatAppearance.BorderSize = 0;
            this.btnFinalizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnFinalizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Image = global::SistemaHotel.Properties.Resources.compra;
            this.btnFinalizar.Location = new System.Drawing.Point(272, 3);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(35, 35);
            this.btnFinalizar.TabIndex = 78;
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnNovo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Image = global::SistemaHotel.Properties.Resources.add;
            this.btnNovo.Location = new System.Drawing.Point(68, 3);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(35, 35);
            this.btnNovo.TabIndex = 53;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.botnNovo_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Image = global::SistemaHotel.Properties.Resources.incluir;
            this.btnSalvar.Location = new System.Drawing.Point(159, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(35, 35);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // panelEntrada
            // 
            this.panelEntrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(120)))));
            this.panelEntrada.Controls.Add(this.btnGravarRegistro);
            this.panelEntrada.Controls.Add(this.lblGravarRegistro);
            this.panelEntrada.Controls.Add(this.lblEntrada);
            this.panelEntrada.Controls.Add(this.btnEntrada);
            this.panelEntrada.Controls.Add(this.btnEditar);
            this.panelEntrada.Controls.Add(this.lblAlterar);
            this.panelEntrada.Enabled = false;
            this.panelEntrada.Location = new System.Drawing.Point(410, 522);
            this.panelEntrada.Name = "panelEntrada";
            this.panelEntrada.Size = new System.Drawing.Size(311, 53);
            this.panelEntrada.TabIndex = 155;
            // 
            // btnGravarRegistro
            // 
            this.btnGravarRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravarRegistro.Enabled = false;
            this.btnGravarRegistro.FlatAppearance.BorderSize = 0;
            this.btnGravarRegistro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnGravarRegistro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnGravarRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravarRegistro.Image = global::SistemaHotel.Properties.Resources.save_2;
            this.btnGravarRegistro.Location = new System.Drawing.Point(127, 3);
            this.btnGravarRegistro.Name = "btnGravarRegistro";
            this.btnGravarRegistro.Size = new System.Drawing.Size(35, 35);
            this.btnGravarRegistro.TabIndex = 152;
            this.btnGravarRegistro.UseVisualStyleBackColor = true;
            this.btnGravarRegistro.Click += new System.EventHandler(this.btnGravarRegistro_Click);
            // 
            // lblGravarRegistro
            // 
            this.lblGravarRegistro.AutoSize = true;
            this.lblGravarRegistro.Location = new System.Drawing.Point(107, 36);
            this.lblGravarRegistro.Name = "lblGravarRegistro";
            this.lblGravarRegistro.Size = new System.Drawing.Size(77, 13);
            this.lblGravarRegistro.TabIndex = 153;
            this.lblGravarRegistro.Text = "Incluir Registro";
            // 
            // btnEntrada
            // 
            this.btnEntrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnEntrada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrada.Enabled = false;
            this.btnEntrada.FlatAppearance.BorderSize = 0;
            this.btnEntrada.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnEntrada.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnEntrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrada.Image = global::SistemaHotel.Properties.Resources.compra;
            this.btnEntrada.Location = new System.Drawing.Point(37, 3);
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Size = new System.Drawing.Size(35, 35);
            this.btnEntrada.TabIndex = 150;
            this.btnEntrada.UseVisualStyleBackColor = false;
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Image = global::SistemaHotel.Properties.Resources.editar_property;
            this.btnEditar.Location = new System.Drawing.Point(228, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(35, 35);
            this.btnEditar.TabIndex = 22;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(120)))));
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.btnExcluir);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Location = new System.Drawing.Point(759, 522);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 53);
            this.panel2.TabIndex = 156;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Image = global::SistemaHotel.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(191, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(35, 35);
            this.btnCancelar.TabIndex = 139;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Enabled = false;
            this.btnExcluir.FlatAppearance.BorderSize = 0;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Image = global::SistemaHotel.Properties.Resources.excluir;
            this.btnExcluir.Location = new System.Drawing.Point(286, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(35, 35);
            this.btnExcluir.TabIndex = 54;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnImg
            // 
            this.btnImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImg.FlatAppearance.BorderSize = 0;
            this.btnImg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnImg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImg.Image = global::SistemaHotel.Properties.Resources.image;
            this.btnImg.Location = new System.Drawing.Point(847, 14);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(27, 21);
            this.btnImg.TabIndex = 9;
            this.btnImg.UseVisualStyleBackColor = false;
            this.btnImg.Click += new System.EventHandler(this.btnImg_Click);
            // 
            // image
            // 
            this.image.Location = new System.Drawing.Point(880, 2);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(249, 187);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image.TabIndex = 73;
            this.image.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(694, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 157;
            this.label11.Text = "Forma de pagto";
            // 
            // FrmProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1130, 584);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelEntrada);
            this.Controls.Add(this.panelIncluirRegistro);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panelEstoque);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panelValores);
            this.Controls.Add(this.panelBusca);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblalterarcod);
            this.Controls.Add(this.gridum);
            this.Controls.Add(this.btnImg);
            this.Controls.Add(this.labelescolher);
            this.Controls.Add(this.image);
            this.Controls.Add(this.lblTituloProdutos);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRO DE PRODUTOS";
            this.Load += new System.EventHandler(this.FrmProdutos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridum)).EndInit();
            this.panelBusca.ResumeLayout(false);
            this.panelBusca.PerformLayout();
            this.panelValores.ResumeLayout(false);
            this.panelValores.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelEstoque.ResumeLayout(false);
            this.panelEstoque.PerformLayout();
            this.panelIncluirRegistro.ResumeLayout(false);
            this.panelIncluirRegistro.PerformLayout();
            this.panelEntrada.ResumeLayout(false);
            this.panelEntrada.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.ComboBox cbFornecedor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscarNome;
        private System.Windows.Forms.Label lblBuscaProduto;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValorVenda;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodBarra;
        private System.Windows.Forms.TextBox txtBuscarCod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMinimo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTituloProdutos;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label labelescolher;
        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIncluirRegistro;
        private System.Windows.Forms.Label lblAlterar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView gridum;
        private System.Windows.Forms.Label lblalterarcod;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCompra;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtUnitario;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panelBusca;
        private System.Windows.Forms.Panel panelValores;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Panel panelEstoque;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblEntrada;
        private System.Windows.Forms.Button btnEntrada;
        private System.Windows.Forms.Panel panelIncluirRegistro;
        private System.Windows.Forms.Panel panelEntrada;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGravarRegistro;
        private System.Windows.Forms.Label lblGravarRegistro;
        private System.Windows.Forms.ComboBox cbPagto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnFinalizar;
    }
}