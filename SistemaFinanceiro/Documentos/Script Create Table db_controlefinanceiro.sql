use db_controlefinanceiro;

-- --------------------------------------------------------

-- Banco de dados: `db_controlefinanceiro`

-- --------------------------------------------------------

--
-- Estrutura da tabela `ano`
--

CREATE TABLE `ano` (
  `id` int(11) NOT NULL,
  `ano` varchar(4) NOT NULL,
  `data` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `caixa`
--

CREATE TABLE `caixa` (
  `id` int(11) NOT NULL,
  `data` date DEFAULT NULL,
  `historico` varchar(500) DEFAULT '...',
  `forma_pagto` varchar(20) DEFAULT 'Dinheiro',
  `valor` decimal(10,2) DEFAULT 0.00,
  `taxa` decimal(10,2) DEFAULT 0.00,
  `total` decimal(10,2) DEFAULT 0.00,
  `saida` decimal(10,2) DEFAULT 0.00,
  `trans_dinheiro` double(10,2) DEFAULT 0.00,
  `trans_conta` decimal(10,2) DEFAULT 0.00,
  `tipo` varchar(30) DEFAULT 'Entrada',
  `dinheiro` decimal(10,2) DEFAULT 0.00,
  `pix` decimal(10,2) DEFAULT 0.00,
  `cartao` decimal(10,2) DEFAULT 0.00,
  `funcionario` varchar(100) DEFAULT 'supervisor'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estrutura da tabela `cargos`
--

CREATE TABLE `cargos` (
  `id` int(11) NOT NULL,
  `cargo` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `clientes`
--

CREATE TABLE `clientes` (
  `id` int(11) NOT NULL,
  `codigo` varchar(20) DEFAULT '0',
  `nome` varchar(80) NOT NULL,
  `cpf` varchar(14) DEFAULT NULL,
  `valorAberto` decimal(10,2) NOT NULL DEFAULT 0.00,
  `tel` varchar(20) DEFAULT NULL,
  `email` varchar(80) DEFAULT NULL,
  `desbloqueado` varchar(20) NOT NULL DEFAULT 'Desbloqueado',
  `Inadiplente` varchar(5) NOT NULL DEFAULT 'Não',
  `endereco` varchar(200) DEFAULT 'Rua',
  `funcionario` varchar(80) DEFAULT NULL,
  `imagem` longblob DEFAULT NULL,
  `data` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Estrutura da tabela `detalhes_lancarvendas`
--

CREATE TABLE `detalhes_lancarvendas` (
  `id` int(11) NOT NULL,
  `id_venda` int(11) NOT NULL,
  `codigo_produto` varchar(20) DEFAULT '',
  `item` int(11) DEFAULT 0,
  `descricao` varchar(120) NOT NULL,
  `quantidade` int(11) NOT NULL,
  `valor_unitario` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  `funcionario` varchar(30) NOT NULL,
  `id_produto` int(11) NOT NULL,
  `data` date NOT NULL,
  `nota` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `detalhes_venda`
--

CREATE TABLE `detalhes_venda` (
  `id` int(11) NOT NULL,
  `id_venda` int(11) NOT NULL,
  `produto` varchar(80) NOT NULL,
  `quantidade` int(11) NOT NULL,
  `valor_unitario` decimal(10,2) NOT NULL,
  `valor_total` decimal(10,2) NOT NULL,
  `funcionario` varchar(30) NOT NULL,
  `id_produto` int(11) NOT NULL,
  `data` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `fornecedores`
--

CREATE TABLE `fornecedores` (
  `id` int(11) NOT NULL,
  `nome` varchar(80) NOT NULL,
  `cnpj` varchar(20) NOT NULL,
  `endereco` varchar(80) NOT NULL,
  `telefone` varchar(20) NOT NULL,
  `vendedor` varchar(50) NOT NULL DEFAULT 'vendedor'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `funcionarios`
--

CREATE TABLE `funcionarios` (
  `id` int(11) NOT NULL,
  `nome` varchar(60) NOT NULL,
  `cpf` varchar(20) NOT NULL,
  `endereco` varchar(80) NOT NULL,
  `telefone` varchar(20) NOT NULL,
  `cargo` varchar(20) NOT NULL,
  `data` date NOT NULL,
  `imagem` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



--
-- Estrutura da tabela `gastos`
--

CREATE TABLE `gastos` (
  `id` int(11) NOT NULL,
  `descricao` varchar(60) NOT NULL,
  `destino` varchar(200) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `pagto` varchar(10) DEFAULT 'Dinheiro',
  `funcionario` varchar(30) NOT NULL,
  `data` date NOT NULL,
  `nota` int(11) NOT NULL DEFAULT 0,
  `vencimento` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `historico`
--

CREATE TABLE `historico` (
  `id` int(11) NOT NULL,
  `data` datetime NOT NULL,
  `hospede` varchar(100) NOT NULL,
  `quarto` varchar(10) NOT NULL,
  `descricao` varchar(200) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `pago` decimal(10,2) NOT NULL,
  `apagar` decimal(10,2) NOT NULL,
  `nota` int(11) NOT NULL,
  `funcionario` varchar(100) NOT NULL,
  `id_historico` int(11) NOT NULL,
  `status` varchar(10) NOT NULL DEFAULT 'Quitado'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estrutura da tabela `lancar_movimentacoes`
--

CREATE TABLE `lancar_movimentacoes` (
  `id` int(11) NOT NULL,
  `cliente` varchar(100) NOT NULL,
  `tipo` varchar(20) NOT NULL DEFAULT 'Entrada',
  `movimento` varchar(20) NOT NULL DEFAULT 'Emtrada',
  `descricao` varchar(20) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `desconto` decimal(10,2) NOT NULL,
  `taxa` decimal(10,2) NOT NULL,
  `valor_total` decimal(10,2) NOT NULL,
  `cartao` decimal(10,2) NOT NULL,
  `pix` decimal(10,2) NOT NULL,
  `dinheiro` decimal(10,2) NOT NULL,
  `nota` int(11) NOT NULL,
  `valor_pago` decimal(10,2) NOT NULL,
  `funcionario` varchar(100) NOT NULL,
  `id_movimento` int(11) NOT NULL,
  `data` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Estrutura da tabela `lancar_vendas`
--

CREATE TABLE `lancar_vendas` (
  `id` int(11) NOT NULL,
  `cliente` varchar(100) NOT NULL DEFAULT 'Padrão',
  `valor` decimal(10,2) NOT NULL,
  `desconto` decimal(10,2) NOT NULL,
  `taxa` decimal(10,2) NOT NULL,
  `valor_total` decimal(10,2) NOT NULL,
  `entrada` decimal(10,2) NOT NULL DEFAULT 0.00,
  `pago` decimal(10,2) NOT NULL DEFAULT 0.00,
  `apagar` decimal(10,2) NOT NULL,
  `pix` decimal(10,2) NOT NULL,
  `dinheiro` decimal(10,2) NOT NULL,
  `cartao` decimal(10,2) NOT NULL,
  `nota` int(11) NOT NULL,
  `funcionario` varchar(100) NOT NULL,
  `data` date NOT NULL,
  `status` varchar(12) NOT NULL DEFAULT 'Quitado'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Estrutura da tabela `lucro`
--

CREATE TABLE `lucro` (
  `id` int(11) NOT NULL,
  `descricao` varchar(80) NOT NULL DEFAULT '0.00',
  `valor_compra` decimal(10,2) NOT NULL DEFAULT 0.00,
  `valor_venda` decimal(10,2) NOT NULL DEFAULT 0.00,
  `lucro` decimal(10,2) NOT NULL DEFAULT 0.00,
  `grupo` varchar(20) NOT NULL DEFAULT 'todos',
  `subgrupo` varchar(20) NOT NULL DEFAULT 'todos',
  `avista` varchar(4) NOT NULL DEFAULT 'Sim',
  `aprazo` varchar(4) NOT NULL DEFAULT 'Sim'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estrutura da tabela `produtos`
--

CREATE TABLE `produtos` (
  `id` int(11) NOT NULL,
  `cod` varchar(13) NOT NULL,
  `nome` varchar(80) NOT NULL,
  `descricao` varchar(12) NOT NULL DEFAULT 'UN',
  `estoque` int(11) NOT NULL DEFAULT 0,
  `fornecedor` int(11) NOT NULL,
  `entrada` int(11) NOT NULL DEFAULT 0,
  `total_compra` decimal(10,2) NOT NULL DEFAULT 0.00,
  `valor_venda` decimal(10,2) NOT NULL DEFAULT 0.00,
  `Valor_compra` decimal(10,2) NOT NULL DEFAULT 0.00,
  `data` date NOT NULL,
  `imagem` longblob DEFAULT NULL,
  `minimo` int(11) NOT NULL DEFAULT 0,
  `nota` int(11) NOT NULL,
  `lucro` decimal(10,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `servicos`
--

CREATE TABLE `servicos` (
  `id` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `ativo` varchar(5) NOT NULL DEFAULT 'Sim'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `tela`
--

CREATE TABLE `tela` (
  `id` int(11) NOT NULL,
  `tipo` varchar(10) DEFAULT 'TRIAL',
  `codigo` varchar(100) DEFAULT '8208-A49D-3F46-F67C-1175-B7F3-38B4-BAB5',
  `serial` varchar(100) DEFAULT 'HR3BB-H9JW8-78C09-8F02E-JX9F0-12RW1',
  `hide` int(11) NOT NULL,
  `old` varchar(5) NOT NULL DEFAULT 'n',
  `dt` date DEFAULT NULL,
  `winner` varchar(5) DEFAULT 'Tim'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Estrutura da tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `nome` varchar(30) NOT NULL,
  `cargo` varchar(20) NOT NULL,
  `usuario` varchar(30) NOT NULL,
  `senha` varchar(30) NOT NULL,
  `data` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `vendas`
--

CREATE TABLE `vendas` (
  `id` int(11) NOT NULL,
  `hospede` varchar(80) NOT NULL,
  `quarto` varchar(10) DEFAULT '01',
  `valor_total` decimal(10,2) NOT NULL,
  `desconto` decimal(10,2) NOT NULL,
  `cartao` decimal(10,2) NOT NULL,
  `pix` decimal(10,2) DEFAULT 0.00,
  `dinheiro` decimal(10,2) NOT NULL,
  `valor_pago` decimal(10,2) NOT NULL,
  `exclusao` decimal(10,2) NOT NULL DEFAULT 0.00,
  `status` varchar(25) NOT NULL DEFAULT 'efetuada',
  `data` date NOT NULL,
  `funcionario` varchar(20) NOT NULL,
  `nota` int(11) NOT NULL DEFAULT 0,
  `apagar` decimal(10,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `ano`
--
ALTER TABLE `ano`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `caixa`
--
ALTER TABLE `caixa`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `cargos`
--
ALTER TABLE `cargos`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `detalhes_lancarvendas`
--
ALTER TABLE `detalhes_lancarvendas`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `detalhes_venda`
--
ALTER TABLE `detalhes_venda`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `fornecedores`
--
ALTER TABLE `fornecedores`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `gastos`
--
ALTER TABLE `gastos`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `historico`
--
ALTER TABLE `historico`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `lancar_movimentacoes`
--
ALTER TABLE `lancar_movimentacoes`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `lancar_vendas`
--
ALTER TABLE `lancar_vendas`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `lucro`
--
ALTER TABLE `lucro`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `produtos`
--
ALTER TABLE `produtos`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `servicos`
--
ALTER TABLE `servicos`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `tela`
--
ALTER TABLE `tela`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `vendas`
--
ALTER TABLE `vendas`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `ano`
--
ALTER TABLE `ano`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de tabela `caixa`
--
ALTER TABLE `caixa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT de tabela `cargos`
--
ALTER TABLE `cargos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de tabela `clientes`
--
ALTER TABLE `clientes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `detalhes_lancarvendas`
--
ALTER TABLE `detalhes_lancarvendas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=250;

--
-- AUTO_INCREMENT de tabela `detalhes_venda`
--
ALTER TABLE `detalhes_venda`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=194;

--
-- AUTO_INCREMENT de tabela `fornecedores`
--
ALTER TABLE `fornecedores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de tabela `gastos`
--
ALTER TABLE `gastos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de tabela `historico`
--
ALTER TABLE `historico`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de tabela `lancar_movimentacoes`
--
ALTER TABLE `lancar_movimentacoes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=55;

--
-- AUTO_INCREMENT de tabela `lancar_vendas`
--
ALTER TABLE `lancar_vendas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT de tabela `lucro`
--
ALTER TABLE `lucro`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `produtos`
--
ALTER TABLE `produtos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;

--
-- AUTO_INCREMENT de tabela `servicos`
--
ALTER TABLE `servicos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT de tabela `tela`
--
ALTER TABLE `tela`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de tabela `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT de tabela `vendas`
--
ALTER TABLE `vendas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=134;
COMMIT;