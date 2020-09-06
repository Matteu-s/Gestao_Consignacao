using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Retaguarda
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            Produto produto = new Produto();
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        //Metodo para inserir um produto no banco
        private void InserirSD(Produto produto)
        {
            Metodos metodos = new Metodos();
            if (cmbNomeProdutoSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Produto está vazio.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbNomeProdutoSD.BackColor = Color.LightBlue;
            }
            else if (txtPrecoProdutoSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Preço está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecoProdutoSD.BackColor = Color.LightBlue;
            }
            else if (txtSaidaInicialSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Saída Inicial está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSaidaInicialSD.BackColor = Color.LightBlue;
            }
            else if (cmbCobradorSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Cobrador está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCobradorSD.BackColor = Color.LightBlue;
            }
            else if (cmbRotaSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Rota está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbRotaSD.BackColor = Color.LightBlue;
            }
            else
            {
                try
                {
                    produto.nomeProduto = cmbNomeProdutoSD.Text;
                    produto.precoProduto = Convert.ToDouble(txtPrecoProdutoSD.Text);
                    produto.saidaDiaria = Convert.ToInt32(txtSaidaInicialSD.Text);
                    produto.cobrador = cmbCobradorSD.Text;
                    produto.rota = cmbRotaSD.Text;
                    produto.devolucoes = Convert.ToInt32(txtDevolucoesSD.Text);
                    produto.saidaMenosDevolucoes = produto.saidaDiaria - produto.devolucoes;
                    produto.valorTotalAtual = Convert.ToDouble(produto.saidaMenosDevolucoes) * produto.precoProduto;
                    double desconto1 = (produto.valorTotalAtual * 25) / 100;
                    produto.valorTotalAtual = produto.valorTotalAtual - desconto1;
                    produto.totalSaidaDiariaFin = Convert.ToDouble(txtPrecoProdutoSD.Text) * Convert.ToDouble(txtSaidaInicialSD.Text);
                    double desconto2 = (produto.totalSaidaDiariaFin * 25) / 100;
                    produto.totalSaidaDiariaFin = produto.totalSaidaDiariaFin - desconto2;
                    metodos.InserirSD(produto);

                    dataGridSD.DataSource = metodos.ListarRegistroSaidaDiaria();
                    ListarSaidaDiaria();
                    LimparCamposPrimeiraTela();
                }
                catch (Exception erro)
                {
                    MessageBox.Show("" + erro);
                }
            }
        }
        private void InserirHistoricoDeRegistro(Produto produto)
        {
            Metodos metodos = new Metodos();

            try
            {
                produto.vendas_CodLotes = Convert.ToInt32(txtCodLoteHR.Text);
                produto.codLote = produto.vendas_CodLotes;
                produto.promissorias = Convert.ToInt32(txtPromissoriasHR.Text);
                produto.consignados = Convert.ToInt32(txtConsignadosHR.Text);
                produto.produtosDevolvidos = Convert.ToInt32(txtDevolvidosHR.Text);
                produto.valorRecebido = Convert.ToDouble(txtValorRecebidoHR.Text);
                metodos.SelectVendasParaRM(produto);
                produto.nomeProduto = produto.nomeProduto;
                produto.cobrador = produto.cobrador;
                produto.rota = produto.rota;
                produto.precoProduto = produto.precoProduto;
                metodos.InserirHistoricoDeRegistro(produto);

                dataGridHR.DataSource = metodos.ListarUltimoRegistroHistoricoDeRegistro(produto);
                ListarHistoricoDeRegistro();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no Registro Financeiro" + erro);
            }
        }
        private void EditarSD(Produto produto)
        {
            Metodos metodos = new Metodos();

            if (txtCodLoteSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Selecione um produto para ser editado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Deseja realmente editar esse produto?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
            }
            else
            {
                if (cmbNomeProdutoSD.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("O campo Nome está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbNomeProdutoSD.BackColor = Color.LightBlue;
                }
                else if (txtPrecoProdutoSD.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("O campo Preço está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecoProdutoSD.BackColor = Color.LightBlue;
                }
                else if (txtSaidaInicialSD.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("O campo Estoque está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaidaInicialSD.BackColor = Color.LightBlue;
                }
                else if (cmbCobradorSD.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("O campo Cobrador está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCobradorSD.BackColor = Color.LightBlue;
                }
                else if (cmbRotaSD.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("O campoRota está vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbRotaSD.BackColor = Color.LightBlue;
                }
                else
                {
                    produto.codLote = Convert.ToInt32(txtCodLoteSD.Text);
                    produto.nomeProduto = cmbNomeProdutoSD.Text;
                    produto.precoProduto = Convert.ToDouble(txtPrecoProdutoSD.Text);
                    produto.saidaDiaria = Convert.ToInt32(txtSaidaInicialSD.Text);
                    produto.cobrador = cmbCobradorSD.Text;
                    produto.rota = cmbRotaSD.Text;
                    produto.devolucoes = Convert.ToInt32(txtDevolucoesSD.Text);
                    produto.saidaMenosDevolucoes = produto.saidaDiaria - produto.devolucoes;
                    produto.valorTotalAtual = Convert.ToDouble(produto.saidaMenosDevolucoes) * produto.precoProduto;
                    double desconto1 = (produto.valorTotalAtual * 25) / 100;
                    produto.valorTotalAtual = produto.valorTotalAtual - desconto1;
                    produto.totalSaidaDiariaFin = Convert.ToDouble(txtPrecoProdutoSD.Text) * Convert.ToDouble(txtSaidaInicialSD.Text);
                    double desconto2 = (produto.totalSaidaDiariaFin * 25) / 100;
                    produto.totalSaidaDiariaFin = produto.totalSaidaDiariaFin - desconto2;
                    metodos.EditarSD(produto);

                    produto.mes = cmbFiltroPorMesSD.Text.ToUpper();
                    produto.rota = cmbFiltroPorRotaSD.Text.ToUpper();
                    dataGridSD.DataSource = metodos.FiltroPorMesSaidaDiaria(produto);
                    ListarSaidaDiaria();
                    LimparCamposPrimeiraTela();
                }
            }
        }
        private void ExcluirSD(Produto produto)
        {
            Metodos metodos = new Metodos();

            if (txtCodLoteSD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Selecione um produto para ser excluído.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Deseja realmente excluir esse produto?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
            }
            else
            {
                produto.codLote = Convert.ToInt32(txtCodLoteSD.Text);
                metodos.ExcluirSD(produto);
                produto.mes = cmbFiltroPorMesSD.Text.ToUpper();
                produto.rota = cmbFiltroPorRotaSD.Text.ToUpper(); ;

                //Se o comboboxmes for vazio, limpa os campos e nao retorna nada
                if (cmbFiltroPorMesSD.Text.Trim() == string.Empty && cmbFiltroPorRotaSD.Text.Trim() == string.Empty)
                {
                    for (int i = 0; i < dataGridSD.RowCount; i++)
                    {
                        dataGridSD.Rows[i].DataGridView.Columns.Clear();
                        LimparCamposPrimeiraTela();
                    }
                }
                //senao, vai filtrar pelo mes que estiver
                else if (cmbFiltroPorMesSD.Text.Trim() == string.Empty && cmbFiltroPorRotaSD.Text.Trim() != string.Empty)
                {
                    for (int i = 0; i < dataGridSD.RowCount; i++)
                    {
                        dataGridSD.Rows[i].DataGridView.Columns.Clear();
                        LimparCamposPrimeiraTela();
                    }
                }
                else if (cmbFiltroPorMesSD.Text.Trim() != string.Empty && cmbFiltroPorRotaSD.Text.Trim() != string.Empty)
                {
                    dataGridSD.DataSource = metodos.FiltroPorMesSaidaDiariaCidade(produto);
                    ListarSaidaDiaria();
                    LimparCamposPrimeiraTela();
                }
                else
                {
                    dataGridSD.DataSource = metodos.FiltroPorMesSaidaDiaria(produto);
                    ListarSaidaDiaria();
                    LimparCamposPrimeiraTela();
                }
            }
        }
        private void ExcluirAPV(Produto produto)
        {
            Metodos metodos = new Metodos();

            if (txtCodAPV.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Selecione um produto para ser excluído.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Deseja realmente excluir esse produto?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
            }
            else
            {
                produto.codApuracao = Convert.ToInt32(txtCodAPV.Text);
                metodos.ExcluirAPV(produto);
                produto.mes = cmbFiltroPorMesAPV.Text.ToUpper();

                //Se o comboboxmes for vazio, limpa os campos e nao retorna nada
                if (cmbFiltroPorMesAPV.Text.Trim() == string.Empty)
                {
                    for (int i = 0; i < DataGridAPV.RowCount; i++)
                    {
                        DataGridAPV.Rows[i].DataGridView.Columns.Clear();
                        LimparCamposQuartaTela();
                    }
                }
                else if (cmbFiltroPorMesAPV.Text.Trim() != string.Empty)
                {
                    DataGridAPV.DataSource = metodos.FiltroPorMesApuracaoDeValores(produto);
                    ListarApuracaoDeValores();
                    LimparCamposQuartaTela();
                }
            }
        }
        private void ListarSaidaDiaria()
        {
            try
            {
                dataGridSD.Columns[0].HeaderText = "Cód. Lote";
                dataGridSD.Columns[1].HeaderText = "Data Saída";
                dataGridSD.Columns[1].DefaultCellStyle.Format = "d";
                dataGridSD.Columns[2].HeaderText = "Produto";
                dataGridSD.Columns[3].HeaderText = "Valor Unitário";
                dataGridSD.Columns[3].DefaultCellStyle.Format = "C2";
                dataGridSD.Columns[4].HeaderText = "Saída Inicial";
                dataGridSD.Columns[5].HeaderText = "Total Saída";
                dataGridSD.Columns[5].DefaultCellStyle.Format = "C2";
                dataGridSD.Columns[6].HeaderText = "Dev. Inicial";
                dataGridSD.Columns[7].HeaderText = "Consignado";
                dataGridSD.Columns[8].HeaderText = "R$ Consignado";
                dataGridSD.Columns[8].DefaultCellStyle.Format = "C2";
                dataGridSD.Columns[9].HeaderText = "Cobrador";
                dataGridSD.Columns[10].HeaderText = "Rota";

                dataGridSD.Columns[0].Width = 70;
                dataGridSD.Columns[1].Width = 87;
                dataGridSD.Columns[2].Width = 170;
                dataGridSD.Columns[3].Width = 90;
                dataGridSD.Columns[4].Width = 65;
                dataGridSD.Columns[5].Width = 90;
                dataGridSD.Columns[6].Width = 60;
                dataGridSD.Columns[7].Width = 90;
                dataGridSD.Columns[8].Width = 90;
                dataGridSD.Columns[9].Width = 100;
                dataGridSD.Columns[10].Width = 100;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no metodo novo " + erro);
            }
        }
        private void ListarHistoricoDeRegistro()
        {
            try
            {
                dataGridHR.Columns[0].HeaderText = "Data Registro";
                dataGridHR.Columns[0].DefaultCellStyle.Format = "d";
                dataGridHR.Columns[1].HeaderText = "Promissória";
                dataGridHR.Columns[2].HeaderText = "Produto";
                dataGridHR.Columns[3].HeaderText = "Cód. Lote";
                dataGridHR.Columns[4].HeaderText = "Consignados";
                dataGridHR.Columns[5].HeaderText = "Devolvidos";
                dataGridHR.Columns[6].HeaderText = "Valor Recebido";
                dataGridHR.Columns[6].DefaultCellStyle.Format = "C2";
                dataGridHR.Columns[7].HeaderText = "Cobrador";
                dataGridHR.Columns[8].HeaderText = "Rota";
                dataGridHR.Columns[9].HeaderText = "Cod. Registro";

                dataGridHR.Columns[0].Width = 87;
                dataGridHR.Columns[1].Width = 88;
                dataGridHR.Columns[2].Width = 160;
                dataGridHR.Columns[3].Width = 60;
                dataGridHR.Columns[4].Width = 100;
                dataGridHR.Columns[5].Width = 85;
                dataGridHR.Columns[6].Width = 90;
                dataGridHR.Columns[8].Width = 100;
                dataGridHR.Columns[9].Width = 100;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no filtro por mes de registro" + erro);
            }

        }
        private void ListarResumoDeMovimento()
        {
            //dataGridRM.Columns[0].HeaderText = "Data Saída";
            dataGridRM.Columns[0].HeaderText = "Promissória";
            dataGridRM.Columns[1].HeaderText = "Produto";
            dataGridRM.Columns[2].HeaderText = "Total Consignado";
            dataGridRM.Columns[3].HeaderText = "Total Recebido";
            dataGridRM.Columns[3].DefaultCellStyle.Format = "C2";
            dataGridRM.Columns[4].HeaderText = "Total Devolvido";
            dataGridRM.Columns[5].HeaderText = "Qtd. à receber";
            dataGridRM.Columns[6].HeaderText = "Cobrador";
            dataGridRM.Columns[7].HeaderText = "Rota";


            //dataGridRM.Columns[0].Width = 87;
            //dataGridRM.Columns[0].DefaultCellStyle.Format = "d";
            dataGridRM.Columns[0].Width = 90;
            dataGridRM.Columns[1].Width = 200;
            dataGridRM.Columns[2].Width = 100;
            dataGridRM.Columns[3].Width = 90;
            dataGridRM.Columns[4].Width = 80;
            dataGridRM.Columns[5].Width = 80;
            dataGridRM.Columns[6].Width = 110;
            dataGridRM.Columns[7].Width = 110;

        }
        private void UpdateResumoDeMovimento(Produto produto)
        {
            Metodos metodos = new Metodos();
            try
            {
                produto.promissorias = Convert.ToInt32(txtPromissoriasHR.Text);
                produto.nomeProduto = txtNomeProdutoHR.Text;
                produto.rota = produto.rota;
                produto.cobrador = produto.cobrador;
                metodos.SelectRegistroMovimento(produto);
                produto.valorTotalRecebido = produto.valorRecebido;
                produto.produtoTotalDevolvido = produto.produtosDevolvidos;
                produto.valorTotalConsignado = produto.consignados;
                produto.precoProduto = produto.valorTotalRecebido / produto.precoProduto;
                produto.produtosPendentes = (produto.valorTotalConsignado - produto.produtoTotalDevolvido) - Convert.ToInt32(produto.precoProduto);
                metodos.UpdateResumoDeMovimento(produto);
                //HistoricoDeRegistroNomeDataPendente(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no update de valores" + erro);

            }
        }
        private void HistoricoDeRegistroNomeDataPendente(Produto produto)
        {
            Metodos metodos = new Metodos();

            try
            {
                metodos.HistoricoDeRegistroNomeDataPendente(produto);
                txtNomeProdutoHR.Text = produto.nomeProduto;
                txtDataSaidaHR.Text = Convert.ToString(produto.dataSaida);
                txtPendenteHR.Text = Convert.ToString(produto.saidaMenosDevolucoes);

                //se o espaço for nulo, vazio ou branco, limpa os campos
                if (string.IsNullOrWhiteSpace(produto.nomeProduto))
                {
                    LimparCamposNomeDataPendente();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no buscar historico" + erro);
            }
        }
        private void ListarApuracaoDeValores()
        {
            try
            {
                DataGridAPV.Columns[0].HeaderText = "Cód. Apuração";
                DataGridAPV.Columns[1].HeaderText = "Data da Apuração";
                DataGridAPV.Columns[1].DefaultCellStyle.Format = "d";
                DataGridAPV.Columns[2].HeaderText = "Receitas";
                DataGridAPV.Columns[2].DefaultCellStyle.Format = "C2";
                DataGridAPV.Columns[3].HeaderText = "Despesas";
                DataGridAPV.Columns[3].DefaultCellStyle.Format = "C2";
                DataGridAPV.Columns[4].HeaderText = "Faturamento";
                DataGridAPV.Columns[4].DefaultCellStyle.Format = "C2";
                DataGridAPV.Columns[5].HeaderText = "Data Inicial";
                DataGridAPV.Columns[5].DefaultCellStyle.Format = "d";
                DataGridAPV.Columns[6].HeaderText = "Data Final";
                DataGridAPV.Columns[6].DefaultCellStyle.Format = "d";

                DataGridAPV.Columns[0].Width = 70;
                DataGridAPV.Columns[1].Width = 100;
                DataGridAPV.Columns[2].Width = 85;
                DataGridAPV.Columns[3].Width = 85;
                DataGridAPV.Columns[4].Width = 100;
                DataGridAPV.Columns[5].Width = 138;
                DataGridAPV.Columns[6].Width = 138;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro no metodo novo " + erro);
            }
        }
        private void LimparCamposPrimeiraTela()
        {
            txtCodLoteSD.Clear();
            cmbNomeProdutoSD.SelectedIndex = -1;
            txtPrecoProdutoSD.Clear();
            txtSaidaInicialSD.Clear();
            txtDevolucoesSD.Clear();
            cmbCobradorSD.SelectedIndex = -1;
            cmbRotaSD.SelectedIndex = -1;
            cmbNomeProdutoSD.BackColor = Color.White;
            txtPrecoProdutoSD.BackColor = Color.White;
            txtSaidaInicialSD.BackColor = Color.White;
            cmbCobradorSD.BackColor = Color.White;
            cmbRotaSD.BackColor = Color.White;
        }
        private void LimparCamposSegundaTela()
        {
            txtCodLoteHR.Clear();
            txtNomeProdutoHR.Clear();
            txtDataSaidaHR.Clear();
            txtPendenteHR.Clear();
            txtPromissoriasHR.Clear();
            txtConsignadosHR.Clear();
            txtDevolvidosHR.Clear();
            txtValorRecebidoHR.Clear();
            txtCodRegistroHR.Clear();
            txtPromissoriasHR.BackColor = Color.White;
            txtConsignadosHR.BackColor = Color.White;
            txtDevolvidosHR.BackColor = Color.White;
            txtValorRecebidoHR.BackColor = Color.White;
        }
        private void LimparCamposQuartaTela()
        {
            txtReceitaAPV.Clear();
            txtDespesasAPV.Clear();
        }
        public void LimparCamposNomeDataPendente()
        {
            txtNomeProdutoHR.Clear();
            txtDataSaidaHR.Clear();
            txtPendenteHR.Clear();
        }
        //APRESENTA NA TELA DE REGISTROS, NOME E DATA DE SAIDA DO PRODUTO.     
        private void DataGridSD_TrazerDadosParaTela(object sender, DataGridViewCellEventArgs e)
        {
            txtCodLoteSD.Text = dataGridSD.CurrentRow.Cells[0].Value.ToString();
            cmbNomeProdutoSD.Text = dataGridSD.CurrentRow.Cells[2].Value.ToString();
            txtPrecoProdutoSD.Text = dataGridSD.CurrentRow.Cells[3].Value.ToString();
            txtSaidaInicialSD.Text = dataGridSD.CurrentRow.Cells[4].Value.ToString();
            txtDevolucoesSD.Text = dataGridSD.CurrentRow.Cells[6].Value.ToString();
            cmbCobradorSD.Text = dataGridSD.CurrentRow.Cells[9].Value.ToString();
            cmbRotaSD.Text = dataGridSD.CurrentRow.Cells[10].Value.ToString();
            //this.txtNomeProdutoSD.ReadOnly = true;

        }
        //Carregar os dados do banco nos txtbox
        private void DataGridHR_TrazerDadosParaTela(object sender, DataGridViewCellEventArgs e)
        {
            txtCodLoteHR.Text = dataGridHR.CurrentRow.Cells[1].Value.ToString();
            txtNomeProdutoHR.Text = dataGridHR.CurrentRow.Cells[2].Value.ToString();
            txtDataSaidaHR.Text = dataGridHR.CurrentRow.Cells[0].Value.ToString();
            txtCodRegistroHR.Text = dataGridHR.CurrentRow.Cells[9].Value.ToString();
            txtPromissoriasHR.Text = dataGridHR.CurrentRow.Cells[3].Value.ToString();
            txtConsignadosHR.Text = dataGridHR.CurrentRow.Cells[4].Value.ToString();
            txtDevolvidosHR.Text = dataGridHR.CurrentRow.Cells[5].Value.ToString();
            txtValorRecebidoHR.Text = dataGridHR.CurrentRow.Cells[6].Value.ToString();

        }
        private void DataGridAPV_TrazerDadosParaTela(object sender, DataGridViewCellEventArgs e)
        {
            txtCodAPV.Text = DataGridAPV.CurrentRow.Cells[0].Value.ToString();
        }
        //Botão de Inserir produto na tela inicial
        private void BtnInserirSD_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            InserirSD(produto);
        }
        //Botão de Editar Produto na tela inicial
        private void BtnEditarSD_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            EditarSD(produto);
        }
        //Botão de Excluir Produto na tela inicial
        private void BtnExcluirSD_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            ExcluirSD(produto);
        }
        private void btnBuscarSD_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            produto.mes = cmbFiltroPorMesSD.Text.ToUpper();
            produto.rota = cmbFiltroPorRotaSD.Text.ToUpper();

            if (produto.rota != string.Empty && produto.mes != string.Empty)
            {
                dataGridSD.DataSource = metodos.FiltroPorMesSaidaDiariaCidade(produto);
                ListarSaidaDiaria();
            }
            else if (produto.rota == string.Empty && produto.mes == string.Empty)
            {
                MessageBox.Show("Os campos de rota e mês estão vazios", "Alerta");
            }
            else if (produto.rota != string.Empty && produto.mes == string.Empty)
            {
                MessageBox.Show("O campo filtro por mês está vazio. ", "Alerta");
            }
            else if (produto.rota == string.Empty && produto.mes != string.Empty)
            {
                dataGridSD.DataSource = metodos.FiltroPorMesSaidaDiaria(produto);
                ListarSaidaDiaria();
            }
        }
        private void btnLimparSD_Click(object sender, EventArgs e)
        {
            LimparCamposPrimeiraTela();
            MessageBox.Show("Campos resetados com sucesso!");
        }
        private void btnInserirHR_Click(object sender, EventArgs e)
        {

            if (txtPromissoriasHR.Text == string.Empty.Trim())
            {
                MessageBox.Show("O campo Promissória está vazio.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPromissoriasHR.BackColor = Color.LightBlue;
            }
            else if (txtDevolvidosHR.Text == string.Empty.Trim())
            {
                MessageBox.Show("O campo Devolvidos está vazio.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDevolvidosHR.BackColor = Color.LightBlue;
            }
            else if (txtValorRecebidoHR.Text == string.Empty)
            {
                MessageBox.Show("O campo Valor Recebido está vazio.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtValorRecebidoHR.BackColor = Color.LightBlue;
            }
            else if (txtNomeProdutoHR.Text == string.Empty && txtDataSaidaHR.Text == string.Empty && txtPendenteHR.Text == string.Empty && txtCodLoteHR.Text == string.Empty)
            {
                MessageBox.Show("Digite o código da venda!");
            }
            else
            {
                Produto produto = new Produto();
                InserirHistoricoDeRegistro(produto);
                UpdateResumoDeMovimento(produto);
                LimparCamposSegundaTela();
            }

        }
        private void btnBuscarHR_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            if (txtCodLoteHR.Text.Trim() == string.Empty)
            {
                MessageBox.Show("O campo Cód. Lote está vazio!", "Alerta");
            }
            else
            {
                produto.codLote = Convert.ToInt32(txtCodLoteHR.Text);
                dataGridHR.DataSource = metodos.BuscarHistoricoDeRegistro(produto);
                ListarHistoricoDeRegistro();
                HistoricoDeRegistroNomeDataPendente(produto);
            }
        }
        private void btnBuscarFiltroHR_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            produto.mes = cmbFiltroPorMesHR.Text.ToUpper();
            produto.rota = cmbFiltroPorRotaHR.Text.ToUpper();

            if (produto.rota != string.Empty && produto.mes != string.Empty)
            {
                dataGridHR.DataSource = metodos.FiltroPorMesHistoricoDeRegistroCidade(produto);
                ListarHistoricoDeRegistro();
            }
            else if (produto.rota == string.Empty && produto.mes == string.Empty)
            {
                MessageBox.Show("Os campos de rota e mês estão vazios", "Aviso");
            }
            else
            {
                dataGridHR.DataSource = metodos.FiltroPorMesHistoricoDeRegistro(produto);
                ListarHistoricoDeRegistro();
            }
        }
        //Deixando o valor com ,00
        private void btnLimparHR_Click(object sender, EventArgs e)
        {
            LimparCamposSegundaTela();
        }
        private void btnBuscarRM_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();
            produto.mes = cmbFiltroPorMesRM.Text.ToUpper();
            produto.rota = cmbFiltroPorRotaRM.Text.ToUpper();

            if (produto.rota != string.Empty && produto.mes != string.Empty)
            {
                dataGridRM.DataSource = metodos.FiltroPorMesResumoDeMovimentoCidade(produto);
                ListarResumoDeMovimento();
            }
            else if (produto.rota == string.Empty && produto.mes == string.Empty)
            {
                MessageBox.Show("Os campos Rota e Mês estão vazios", "Aviso");
            }
            else if (produto.rota != string.Empty && produto.mes == string.Empty)
            {
                MessageBox.Show("O campo Mês está vazio");
            }
            else
            {
                dataGridRM.DataSource = metodos.FiltroPorMesResumoDeMovimento(produto);
                ListarResumoDeMovimento();
            }
        }
        private void btnBuscarAPV_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            produto.dataDE = Convert.ToDateTime(dateTimerDEAPV.Text);
            produto.dataATE = Convert.ToDateTime(dateTimerATEAPV.Text);


            if (Convert.ToString(produto.dataDE) != string.Empty && Convert.ToString(produto.dataATE) != string.Empty)
            {
                metodos.SelectApuracaoDeValores(produto);
                if (produto.valorRecebido == 0)
                {
                    txtReceitaAPV.Clear();
                }
                else if (produto.valorRecebido != 0)
                {
                    txtReceitaAPV.Text = Convert.ToString(produto.valorRecebido);
                    MessageBox.Show("Sua Receita foi de aproxidamente R$" + produto.valorRecebido + "", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            else if (Convert.ToString(produto.dataDE) == string.Empty && Convert.ToString(produto.dataATE) == string.Empty)
            {
                MessageBox.Show("Os campos de data inicial e data final estão vazios!", "Alerta");
            }
            else if (Convert.ToString(produto.dataDE) == string.Empty && Convert.ToString(produto.dataATE) != string.Empty)
            {
                MessageBox.Show("O campo data inicial está vazio", "Alerta");
            }
            else if (Convert.ToString(produto.dataDE) != string.Empty && Convert.ToString(produto.dataATE) == string.Empty)
            {
                MessageBox.Show("O campo data final está vazio", "Alerta");
            }

        }
        private void btnCalcularAPV_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            produto.dataDE = Convert.ToDateTime(dateTimerDEAPV.Text);
            produto.dataATE = Convert.ToDateTime(dateTimerATEAPV.Text);
            produto.receitas = Convert.ToDouble(txtReceitaAPV.Text);
            produto.despesas = Convert.ToDouble(txtDespesasAPV.Text);
            produto.faturamento = produto.receitas - produto.despesas;

            if (MessageBox.Show("Seu faturamento foi de aproxidamente R$" + produto.faturamento + ". Deseja salvar estes dados?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {

            }
            else
            {
                metodos.InserirApuracao(produto);
            }

        }
        private void btnFiltrarRegistrosAPV_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            Produto produto = new Produto();

            produto.mes = cmbFiltroPorMesAPV.Text.ToUpper();
            if (produto.mes == "TODOS REGISTROS" || produto.mes == "JANEIRO" || produto.mes == "FEVEREIRO" || produto.mes == "MARÇO" || produto.mes == "ABRIL" || produto.mes == "MAIO" ||
                produto.mes == "JUNHO" || produto.mes == "JULHO" || produto.mes == "AGOSTO" || produto.mes == "SETEMBRO" || produto.mes == "OUTUBRO" || produto.mes == "NOVEMBRO" || produto.mes == "DEZEMBRO")
            {
                DataGridAPV.DataSource = metodos.FiltroPorMesApuracaoDeValores(produto);
                ListarApuracaoDeValores();
            }
            else
            {
                MessageBox.Show("Mês digitado incorretamente, digite novamente!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void TxtPreco_LeavePrecoSD(object sender, EventArgs e)
        {
            if (txtPrecoProdutoSD.Text.Trim() == String.Empty)
            {
                txtPrecoProdutoSD.Clear();
            }
            else
            {
                txtPrecoProdutoSD.Text = Convert.ToDouble(txtPrecoProdutoSD.Text).ToString("F");
            }
        }
        private void TxtPreco_EnterPrecoSD(object sender, EventArgs e)
        {
            String x = "";

            for (int i = 0; i <= txtPrecoProdutoSD.Text.Length - 1; i++)
            {
                if ((txtPrecoProdutoSD.Text[i] >= '0' &&
                    txtPrecoProdutoSD.Text[i] <= '9') ||
                    txtPrecoProdutoSD.Text[i] == ',')
                {
                    x += txtPrecoProdutoSD.Text[i];
                }
            }
            txtPrecoProdutoSD.Text = x;
            txtPrecoProdutoSD.SelectAll();

        }
        // MUDA O TXT APERTANDO ENTER
        private void TxtPreco_LeavePrecoHR(object sender, EventArgs e)
        {
            if (txtValorRecebidoHR.Text.Trim() == String.Empty)
            {
                txtValorRecebidoHR.Clear();
            }
            else
            {
                txtValorRecebidoHR.Text = Convert.ToDouble(txtValorRecebidoHR.Text).ToString("F");
            }
        }
        private void TxtPreco_EnterPrecoHR(object sender, EventArgs e)
        {
            String x = "";

            for (int i = 0; i <= txtValorRecebidoHR.Text.Length - 1; i++)
            {
                if ((txtValorRecebidoHR.Text[i] >= '0' &&
                    txtValorRecebidoHR.Text[i] <= '9') ||
                    txtValorRecebidoHR.Text[i] == ',')
                {
                    x += txtValorRecebidoHR.Text[i];
                }
            }
            txtValorRecebidoHR.Text = x;
            txtValorRecebidoHR.SelectAll();

        }
        private void MudarNoEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ProcessTabKey(true);
                e.Handled = true;
            }
        }
        private void MudarNoEnterNumero(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;

                if (e.KeyChar == (char)13)
                {
                    this.ProcessTabKey(true);
                    e.Handled = true;
                }
            }
        }
        // APOS DIGITAR O CODIGO DA VENDA, TRAS O RESULTADO APERTANDO ENTER
        private void BuscarSaidaDiariaNoEnter(object sender, KeyPressEventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            //ACEITA SOMENTE NUMEROS
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;


                if (e.KeyChar == (char)13)
                {

                    if (txtCodLoteSD.Text != string.Empty)
                    {
                        produto.codLote = Convert.ToInt32(txtCodLoteSD.Text);
                        dataGridSD.DataSource = metodos.BuscarSaidaDiaria(produto);
                        ListarSaidaDiaria();
                    }
                    else if (txtCodLoteSD.Text == string.Empty)
                    {
                        this.ProcessTabKey(true);
                        e.Handled = true;
                    }
                }
            }
        }
        private void BuscarHistoridoDeRegistroNoEnter(object sender, KeyPressEventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;

                if (e.KeyChar == (char)13)
                {
                    if (txtCodLoteHR.Text != string.Empty)
                    {
                        produto.codLote = Convert.ToInt32(txtCodLoteHR.Text);
                        dataGridHR.DataSource = metodos.BuscarHistoricoDeRegistro(produto);
                        ListarHistoricoDeRegistro();
                        HistoricoDeRegistroNomeDataPendente(produto);
                    }
                    else if (txtCodLoteHR.Text == string.Empty)
                    {
                        this.ProcessTabKey(true);
                        e.Handled = true;
                    }

                }
            }
        }
        private void cmbProximo_KeyUp(object sender, KeyEventArgs e)
        {
            Produto produto = new Produto();

            if (e.KeyValue == (char)13 || e.KeyValue == (char)9 || e.KeyValue == (char)11)
            {
                this.ProcessTabKey(true);
                e.Handled = true;

            }
        }
        private void MudarNoEnterCMB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                this.ProcessTabKey(true);
                e.Handled = true;
            }
        }
        private void ExcluirHR(Produto produto)
        {
            Metodos metodos = new Metodos();

            if (txtCodRegistroHR.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Selecione um produto para ser excluído.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Deseja realmente excluir esse produto?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
            }
            else
            { 
                produto.codRegistro = Convert.ToInt32(txtCodRegistroHR.Text);
                produto.codLote = Convert.ToInt32(txtCodLoteHR.Text);
                produto.promissorias = Convert.ToInt32(txtPromissoriasHR.Text);
                produto.nomeProduto = txtNomeProdutoHR.Text;
                metodos.ExcluirHR(produto);
                metodos.SelectRegistroMovimentoExcluir(produto);
                produto.valorTotalRecebido = produto.valorRecebido;
                produto.produtoTotalDevolvido = produto.produtosDevolvidos;
                produto.valorTotalConsignado = produto.consignados;
                produto.precoProduto = produto.valorTotalRecebido / produto.precoProduto;
                produto.produtosPendentes = (Convert.ToDouble(produto.valorTotalConsignado) - Convert.ToDouble(produto.produtoTotalDevolvido)) - produto.precoProduto;
                metodos.UpdateResumoDeMovimentoExcluir(produto);
                produto.mes = cmbFiltroPorMesHR.Text.ToUpper();
                produto.rota = cmbFiltroPorRotaHR.Text.ToUpper();

                //Se o comboboxmes for vazio, limpa os campos e nao retorna nada
                if (cmbFiltroPorMesHR.Text.Trim() == string.Empty && cmbFiltroPorRotaHR.Text.Trim() == string.Empty)
                {
                    for (int i = 0; i < dataGridHR.RowCount; i++)
                    {
                        dataGridHR.Rows[i].DataGridView.Columns.Clear();
                        LimparCamposSegundaTela();
                    }
                }
                //senao, vai filtrar pelo mes que estiver
                else if (cmbFiltroPorMesHR.Text.Trim() == string.Empty && cmbFiltroPorRotaHR.Text.Trim() != string.Empty)
                {
                    for (int i = 0; i < dataGridSD.RowCount; i++)
                    {
                        dataGridHR.Rows[i].DataGridView.Columns.Clear();
                        LimparCamposSegundaTela();
                    }
                }
                else if (cmbFiltroPorMesHR.Text.Trim() != string.Empty && cmbFiltroPorRotaHR.Text.Trim() != string.Empty)
                {
                    dataGridHR.DataSource = metodos.FiltroPorMesHistoricoDeRegistroCidade(produto);
                    ListarHistoricoDeRegistro();
                    LimparCamposSegundaTela();
                }
                else if (cmbFiltroPorMesHR.Text.Trim() != string.Empty && cmbFiltroPorRotaHR.Text.Trim() == string.Empty)
                {
                    dataGridHR.DataSource = metodos.FiltroPorMesHistoricoDeRegistro(produto);
                    ListarHistoricoDeRegistro();
                    LimparCamposSegundaTela();
                }
            }
        }
        private void btnExcluirHR_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            ExcluirHR(produto);
        }
        private void btnLimparAPV_Click(object sender, EventArgs e)
        {
            LimparCamposQuartaTela();
            MessageBox.Show("Campos resetados com sucesso!", "Alerta");
        }
        private void btnExcluirAPV_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            ExcluirAPV(produto);
        }
        private void btnBuscarPromissoriaRM_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            Metodos metodos = new Metodos();

            produto.promissorias = Convert.ToInt32(txtPromissoriaRM.Text);
            metodos.BuscarPromissoriaRM(produto);
            produto.promissorias = produto.promissorias;
            produto.codRegistro = produto.codRegistro;
            MessageBox.Show("" + produto.codRegistro);
            MessageBox.Show(""+produto.promissorias);
            if (produto.codRegistro != 0)
            {
                MessageBox.Show("Digite o número da promissória! ", "Alerta");
            }
            else if (produto.codRegistro == 0)
            {
                dataGridRM.DataSource = metodos.BuscarPromissoriaRM(produto);
                ListarResumoDeMovimento();
                txtPromissoriaRM.Clear();

                //fazer um select para se o numero retornar nao existente, avisar.
            }
        }
    }
}
