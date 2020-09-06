using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace Retaguarda
{
    public class MtdBd : Conexao
    {
        MySqlCommand comando = null;

        public void InserirSD(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("INSERT INTO Vendas (CodLote, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota) values (null, @NomeProduto, @PrecoProduto, @SaidaDiaria, @TotalSaidaDiariaFin, @Devolucoes, @SaidaMenosDevolucoes, @ValorTotalAtual, @Cobrador, @Rota)", conexao);
                comando.Parameters.AddWithValue("@NomeProduto", produto.nomeProduto);
                comando.Parameters.AddWithValue("@PrecoProduto", produto.precoProduto);
                comando.Parameters.AddWithValue("@SaidaDiaria", produto.saidaDiaria);
                comando.Parameters.AddWithValue("@TotalSaidaDiariaFin", produto.totalSaidaDiariaFin);
                comando.Parameters.AddWithValue("@Devolucoes", produto.devolucoes);
                comando.Parameters.AddWithValue("@SaidaMenosDevolucoes", produto.saidaMenosDevolucoes);
                comando.Parameters.AddWithValue("@ValorTotalAtual", produto.valorTotalAtual);
                comando.Parameters.AddWithValue("@Cobrador", produto.cobrador);
                comando.Parameters.AddWithValue("@Rota", produto.rota);
                //Para pegar o valor inicial da saida e apresentar nos produtos pendentes
                comando.ExecuteNonQuery();
                MessageBox.Show("Produto Inserido com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção do produto no banco de dados! MtdBd01 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void EditarSD(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Update vendas set NomeProduto = @NomeProduto, PrecoProduto = @PrecoProduto, SaidaDiaria = @SaidaDiaria, TotalSaidaDiariaFin = @TotalSaidaDiariaFin, Devolucoes = @Devolucoes, SaidaMenosDevolucoes = @SaidaMenosDevolucoes, ValorTotalAtual = @ValorTotalAtual, Cobrador = @Cobrador, Rota = @Rota where CodLote = @CodLote", conexao);
                comando.Parameters.AddWithValue("@NomeProduto", produto.nomeProduto);
                comando.Parameters.AddWithValue("@PrecoProduto", produto.precoProduto);
                comando.Parameters.AddWithValue("@SaidaDiaria", produto.saidaDiaria);
                comando.Parameters.AddWithValue("@TotalSaidaDiariaFin", produto.totalSaidaDiariaFin);
                comando.Parameters.AddWithValue("@Devolucoes", produto.devolucoes);
                comando.Parameters.AddWithValue("@SaidaMenosDevolucoes", produto.saidaMenosDevolucoes);
                comando.Parameters.AddWithValue("@ValorTotalAtual", produto.valorTotalAtual);
                comando.Parameters.AddWithValue("@Cobrador", produto.cobrador);
                comando.Parameters.AddWithValue("@Rota", produto.rota);
                comando.Parameters.AddWithValue("@codLote", produto.codLote);
                comando.ExecuteNonQuery();
                MessageBox.Show("Produto editado com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! MtdBd02 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void ExcluirSD(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Delete From Vendas where codLote = @codLote", conexao);
                comando.Parameters.AddWithValue("@codLote", produto.codLote);
                comando.ExecuteReader();
                MessageBox.Show("Produto Excluido com sucesso!", "Alerta");
            }
            catch (MySqlException)
            {
                MessageBox.Show("Esta venda já possui registros no sistema, não é possível excluir!", "Alerta");
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public void ExcluirHR(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Delete From Registros where CodRegistro = @CodRegistro", conexao);
                comando.Parameters.AddWithValue("@CodRegistro", produto.codRegistro);
                comando.ExecuteReader();
                MessageBox.Show("Produto Excluido com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na exclusão de um registro! MtbBD03 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void ExcluirAPV(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Delete From Apuracao where CodApuracao = @CodApuracao", conexao);
                comando.Parameters.AddWithValue("@CodApuracao", produto.codApuracao);
                comando.ExecuteReader();
                MessageBox.Show("Produto Excluido com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na exclusão de um registro! MtbBD03 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesSaidaDiaria(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas", conexao);
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-01-01' AND '2019-01-31' OR DATASAIDA BETWEEN '2020-01-01' AND '2020-01-31' OR DATASAIDA BETWEEN '2021-01-01' AND '2021-01-31' OR DATASAIDA BETWEEN '2022-01-01' AND '2022-01-31'", conexao);
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-02-01' AND '2019-02-28' OR DATASAIDA BETWEEN '2020-02-01' AND '2020-02-29' OR DATASAIDA BETWEEN '2021-02-01' AND '2021-02-28' OR DATASAIDA BETWEEN '2022-02-01' AND '2022-02-28'", conexao);
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-03-01' AND '2019-03-31' OR DATASAIDA BETWEEN '2020-03-01' AND '2020-03-31' OR DATASAIDA BETWEEN '2021-03-01' AND '2021-03-31' OR DATASAIDA BETWEEN '2022-03-01' AND '2022-03-31'", conexao);
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-04-01' AND '2019-04-30' OR DATASAIDA BETWEEN '2020-04-01' AND '2020-04-30' OR DATASAIDA BETWEEN '2021-04-01' AND '2021-04-30' OR DATASAIDA BETWEEN '2022-04-01' AND '2022-04-30'", conexao);
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-05-01' AND '2019-05-31' OR DATASAIDA BETWEEN '2020-05-01' AND '2020-05-31' OR DATASAIDA BETWEEN '2021-05-01' AND '2021-05-31' OR DATASAIDA BETWEEN '2022-05-01' AND '2022-05-31'", conexao);
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-06-01' AND '2019-06-30' OR DATASAIDA BETWEEN '2020-06-01' AND '2020-06-30' OR DATASAIDA BETWEEN '2021-06-01' AND '2021-06-30' OR DATASAIDA BETWEEN '2022-06-01' AND '2022-06-30'", conexao);
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-07-01' AND '2019-07-31' OR DATASAIDA BETWEEN '2020-07-01' AND '2020-07-31' OR DATASAIDA BETWEEN '2021-07-01' AND '2021-07-31' OR DATASAIDA BETWEEN '2022-07-01' AND '2022-07-31'", conexao);
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-08-01' AND '2019-08-31' OR DATASAIDA BETWEEN '2020-08-01' AND '2020-08-31' OR DATASAIDA BETWEEN '2021-08-01' AND '2021-08-31' OR DATASAIDA BETWEEN '2022-08-01' AND '2022-08-31'", conexao);
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-09-01' AND '2019-09-30' OR DATASAIDA BETWEEN '2020-09-01' AND '2020-09-30' OR DATASAIDA BETWEEN '2021-09-01' AND '2021-09-30' OR DATASAIDA BETWEEN '2022-09-01' AND '2022-09-30'", conexao);
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-10-01' AND '2019-10-31' OR DATASAIDA BETWEEN '2020-10-01' AND '2020-10-31' OR DATASAIDA BETWEEN '2021-10-01' AND '2021-10-31' OR DATASAIDA BETWEEN '2022-10-01' AND '2022-10-31'", conexao);
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-11-01' AND '2019-11-30' OR DATASAIDA BETWEEN '2020-11-01' AND '2020-11-30' OR DATASAIDA BETWEEN '2021-11-01' AND '2021-11-30' OR DATASAIDA BETWEEN '2022-11-01' AND '2022-11-30'", conexao);
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-12-01' AND '2019-12-31' OR DATASAIDA BETWEEN '2020-12-01' AND '2020-12-31' OR DATASAIDA BETWEEN '2021-12-01' AND '2021-12-31' OR DATASAIDA BETWEEN '2022-12-01' AND '2022-12-31'", conexao);
                        break;
                    default:
                        comando = new MySqlCommand("Select codLote, DataSaida, nomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from vendas where DataSaida between '2100-01-01' and 2100-01-02", conexao);
                        MessageBox.Show("Você digitou um mês incorreto! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Teste" + erro);
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesSaidaDiariaCidade(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();

                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where Rota = @rota", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-01-01' AND '2019-01-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-01-01' AND '2020-01-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-01-01' AND '2021-01-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-01-01' AND '2022-01-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-02-01' AND '2019-02-28' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-02-01' AND '2020-02-29' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-02-01' AND '2021-02-28' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-02-01' AND '2022-02-28' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-03-01' AND '2019-03-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-03-01' AND '2020-03-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-03-01' AND '2021-03-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-03-01' AND '2022-03-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-04-01' AND '2019-04-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-04-01' AND '2020-04-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-04-01' AND '2021-04-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-04-01' AND '2022-04-30' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-05-01' AND '2019-05-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-05-01' AND '2020-05-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-05-01' AND '2021-05-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-05-01' AND '2022-05-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-06-01' AND '2019-06-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-06-01' AND '2020-06-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-06-01' AND '2021-06-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-06-01' AND '2022-06-30' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-07-01' AND '2019-07-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-07-01' AND '2020-07-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-07-01' AND '2021-07-31' AND ROTA = @R0TA OR DATASAIDA BETWEEN '2022-07-01' AND '2022-07-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-08-01' AND '2019-08-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-08-01' AND '2020-08-31' AND ROTA = @R0TA OR DATASAIDA BETWEEN '2021-08-01' AND '2021-08-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-08-01' AND '2022-08-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-09-01' AND '2019-09-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-09-01' AND '2020-09-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-09-01' AND '2021-09-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-09-01' AND '2022-09-30' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-10-01' AND '2019-10-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-10-01' AND '2020-10-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-10-01' AND '2021-10-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-10-01' AND '2022-10-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-11-01' AND '2019-11-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-11-01' AND '2020-11-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-11-01' AND '2021-11-30' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-11-01' AND '2022-11-30' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2019-12-01' AND '2019-12-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2020-12-01' AND '2020-12-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2021-12-01' AND '2021-12-31' AND ROTA = @ROTA OR DATASAIDA BETWEEN '2022-12-01' AND '2022-12-31' AND ROTA = @ROTA", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    default:
                        comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where DataSaida between '2100-12-01' AND '2100-12-31'", conexao);
                        MessageBox.Show("Você digitou a cidade ou o mês incorretamente! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable ListarRegistroSaidaDiaria()
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota FROM Vendas ORDER BY codLote ", conexao);
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable BuscarSaidaDiaria(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("SELECT codLote, DataSaida, NomeProduto, PrecoProduto, SaidaDiaria, TotalSaidaDiariaFin, Devolucoes, SaidaMenosDevolucoes, ValorTotalAtual, Cobrador, Rota from Vendas where codLote = @codLote", conexao);
                comando.Parameters.Add("@codLote", MySqlDbType.Int32).Value = produto.codLote;
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesHistoricoDeRegistroCidade(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-01-01' and '2019-01-31' and v.Rota = @v.Rota OR r.DataRegistro between '2020-01-01' and '2020-01-31' and v.rota = @v.rota OR r.DataRegistro between '2021-01-01' and '2021-01-31' and v.rota = @v.rota OR r.DataRegistro between '2022-01-01' and '2022-01-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-02-01' and '2019-02-28' and v.rota = @v.rota OR r.DataRegistro between '2020-02-01' and '2020-02-29' and v.rota = @v.rota OR r.DataRegistro between '2021-02-01' and '2021-02-28' and v.rota = @v.rota OR r.DataRegistro between '2022-02-01' and '2022-02-28' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-03-01' and '2019-03-31' and v.rota = @v.rota OR r.DataRegistro between '2020-03-01' and '2020-03-31' and v.rota = @v.rota OR r.DataRegistro between '2021-03-01' and '2021-03-31' and v.rota = @v.rota OR r.DataRegistro between '2022-03-01' and '2022-03-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-04-01' and '2019-04-30' and v.rota = @v.rota OR r.DataRegistro between '2020-04-01' and '2020-04-30' and v.rota = @v.rota OR r.DataRegistro between '2021-04-01' and '2021-04-30' and v.rota = @v.rota OR r.DataRegistro between '2022-04-01' and '2022-04-30' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-05-01' and '2019-05-31' and v.rota = @v.rota OR r.DataRegistro between '2020-05-01' and '2020-05-31' and v.rota = @v.rota OR r.DataRegistro between '2021-05-01' and '2021-05-31' and v.rota = @v.rota OR r.DataRegistro between '2022-05-01' and '2022-05-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-06-01' and '2019-06-30' and v.rota = @v.rota OR r.DataRegistro between '2020-06-01' and '2020-06-30' and v.rota = @v.rota OR r.DataRegistro between '2021-06-01' and '2021-06-30' and v.rota = @v.rota OR r.DataRegistro between '2022-06-01' and '2022-06-30' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-07-01' and '2019-07-31' and v.rota = @v.rota OR r.DataRegistro between '2020-07-01' and '2020-07-31' and v.rota = @v.rota OR r.DataRegistro between '2021-07-01' and '2021-07-31' and v.rota = @v.rota OR r.DataRegistro between '2022-07-01' and '2022-07-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-08-01' and '2019-08-31' and v.rota = @v.rota OR r.DataRegistro between '2020-08-01' and '2020-08-31' and v.rota = @v.rota OR r.DataRegistro between '2021-08-01' and '2021-08-31' and v.rota = @v.rota OR r.DataRegistro between '2022-08-01' and '2022-08-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-09-01' and '2019-09-30' and v.rota = @v.rota OR r.DataRegistro between '2020-09-01' and '2020-09-30' and v.rota = @v.rota OR r.DataRegistro between '2021-09-01' and '2021-09-30' and v.rota = @v.rota OR r.DataRegistro between '2022-09-01' and '2022-09-30' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-10-01' and '2019-10-31' and v.rota = @v.rota OR r.DataRegistro between '2020-10-01' and '2020-10-31' and v.rota = @v.rota OR r.DataRegistro between '2021-10-01' and '2021-10-31' and v.rota = @v.rota OR r.DataRegistro between '2022-10-01' and '2022-10-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-11-01' and '2019-11-30' and v.rota = @v.rota OR r.DataRegistro between '2020-11-01' and '2020-11-30' and v.rota = @v.rota OR r.DataRegistro between '2021-11-01' and '2021-11-30' and v.rota = @v.rota OR r.DataRegistro between '2022-11-01' and '2022-11-30' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2019-12-01' and '2019-12-31' and v.rota = @v.rota OR r.DataRegistro between '2020-12-01' and '2020-12-31' and v.rota = @v.rota OR r.DataRegistro between '2021-12-01' and '2021-12-31' and v.rota = @v.rota OR r.DataRegistro between '2022-12-01' and '2022-12-31' and v.rota = @v.rota", conexao);
                        comando.Parameters.Add("@v.rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    default:
                        comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, v.SaidaDiaria, v.TotalSaidaDiariaFin, r.ProdutosDevolvidos, r.ValorRecebido, r.Promissorias, v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where r.DataRegistro between '2100-12-01' and '2100-12-31'", conexao);
                        MessageBox.Show("Você digitou a cidade ou o mês incorretamente! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesHistoricoDeRegistro(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote", conexao);
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-01-01' and '2019-01-31' OR r.DataRegistro between '2020-01-01' and '2020-01-31' OR r.DataRegistro between '2021-01-01' and '2021-01-31' OR r.DataRegistro between '2022-01-01' and '2022-01-31'", conexao);
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-02-01' and '2019-02-29' OR r.DataRegistro between '2020-02-01' and '2020-02-28' OR r.DataRegistro between '2021-02-01' and '2021-02-28' OR r.DataRegistro between '2022-02-01' and '2022-02-28'", conexao);
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-03-01' and '2019-03-31' OR r.DataRegistro between '2020-03-01' and '2020-03-31' OR r.DataRegistro between '2021-03-01' and '2021-03-31' OR r.DataRegistro between '2022-03-01' and '2022-03-31'", conexao);
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-04-01' and '2019-04-30' OR r.DataRegistro between '2020-04-01' and '2020-04-30' OR r.DataRegistro between '2021-04-01' and '2021-04-30' OR r.DataRegistro between '2022-04-01' and '2022-04-30'", conexao);
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-05-01' and '2019-05-31' OR r.DataRegistro between '2020-05-01' and '2020-05-31' OR r.DataRegistro between '2021-05-01' and '2021-05-31' OR r.DataRegistro between '2022-05-01' and '2022-05-31'", conexao);
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-06-01' and '2019-06-30' OR r.DataRegistro between '2020-06-01' and '2020-06-30' OR r.DataRegistro between '2021-06-01' and '2021-06-30' OR r.DataRegistro between '2022-06-01' and '2022-06-30'", conexao);
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-07-01' and '2019-07-31' OR r.DataRegistro between '2020-07-01' and '2020-07-31' OR r.DataRegistro between '2021-07-01' and '2021-07-31' OR r.DataRegistro between '2022-07-01' and '2022-07-31'", conexao);
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("SELECT r.DataRegistro,  r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-08-01' and '2019-08-31' OR r.DataRegistro between '2020-08-01' and '2020-08-31' OR r.DataRegistro between '2021-08-01' and '2021-08-31' OR r.DataRegistro between '2022-08-01' and '2022-08-31'", conexao);
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-09-01' and '2019-09-30' OR r.DataRegistro between '2020-09-01' and '2020-09-30' OR r.DataRegistro between '2021-09-01' and '2021-09-30' OR r.DataRegistro between '2022-09-01' and '2022-09-30'", conexao);
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v  inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-10-01' and '2019-10-31' OR r.DataRegistro between '2020-10-01' and '2020-10-31' OR r.DataRegistro between '2021-10-01' and '2021-10-31' OR r.DataRegistro between '2022-10-01' and '2022-10-31'", conexao);
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-11-01' and '2019-11-30' OR r.DataRegistro between '2020-11-01' and '2020-11-30' OR r.DataRegistro between '2021-11-01' and '2021-11-30' OR r.DataRegistro between '2022-11-01' and '2022-11-30'", conexao);
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2019-12-01' and '2019-12-31' OR r.DataRegistro between '2020-12-01' and '2020-12-31' OR r.DataRegistro between '2021-12-01' and '2021-12-31' OR r.DataRegistro between '2022-12-01' and '2022-12-31'", conexao);
                        break;
                    default:
                        comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido, v.Cobrador, v.Rota, r.CodRegistro from vendas as v inner join registros as r on r.Vendas_codLote = v.codLote where r.DataRegistro between '2100-12-01' and '2100-12-31'", conexao);
                        MessageBox.Show("Você digitou um mês incorreto! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable ListarUltimoRegistroHistoricoDeRegistro(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("SELECT r.DataRegistro, r.Promissorias, v.NomeProduto, v.codLote, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido,  v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where v.codLote = @v.codLote ORDER BY r.Promissorias", conexao);
                comando.Parameters.Add("@v.codLote", MySqlDbType.Int32).Value = produto.codLote;
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable BuscarHistoricoDeRegistro(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("SELECT r.DataRegistro, v.codLote, v.NomeProduto, r.Promissorias, r.Consignados, r.ProdutosDevolvidos, r.ValorRecebido,  v.Cobrador, v.Rota, r.CodRegistro from Vendas as v inner join registros as r on r.vendas_codLote = v.codLote where v.codLote = @v.codLote ORDER BY PROMISSORIAS", conexao);
                comando.Parameters.Add("@V.codLote", MySqlDbType.Int32).Value = produto.codLote;
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }

            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable ListarResumoDeMovimento()
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros order by Promissorias", conexao);
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesResumoDeMovimento(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros order by Promissorias", conexao);
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-01-01' and '2019-01-31' OR DataRegistro between '2020-01-01' and '2020-01-31' OR DataRegistro between '2021-01-01' and '2021-01-31' OR DataRegistro between '2022-01-01' and '2022-01-31' order by Promissorias", conexao);
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-02-01' and '2019-02-28' OR DataRegistro between '2020-02-01' and '2020-02-29' OR DataRegistro between '2021-02-01' and '2021-02-28' OR DataRegistro between '2022-02-01' and '2022-02-28' order by Promissorias", conexao);
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-03-01' and '2019-03-31' OR DataRegistro between '2020-03-01' and '2020-03-31' OR DataRegistro between '2021-03-01' and '2021-03-31' OR DataRegistro between '2022-03-01' and '2022-03-31' order by Promissorias", conexao);
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-04-01' and '2019-04-30' OR DataRegistro between '2020-04-01' and '2020-04-30' OR DataRegistro between '2021-04-01' and '2021-04-30' OR DataRegistro between '2022-04-01' and '2022-04-30' order by Promissorias", conexao);
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-05-01' and '2019-05-31' OR DataRegistro between '2020-05-01' and '2020-05-31' OR DataRegistro between '2021-05-01' and '2021-05-31' OR DataRegistro between '2022-05-01' and '2022-05-31' order by Promissorias", conexao);
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-06-01' and '2019-06-30' OR DataRegistro between '2020-06-01' and '2020-06-30' OR DataRegistro between '2021-06-01' and '2021-06-30' OR DataRegistro between '2022-06-01' and '2022-06-30' order by Promissorias", conexao);
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-07-01' and '2019-07-31' OR DataRegistro between '2020-07-01' and '2020-07-31' OR DataRegistro between '2021-07-01' and '2021-07-31' OR DataRegistro between '2022-07-01' and '2022-07-31' order by Promissorias", conexao);
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-08-01' and '2019-08-31' OR DataRegistro between '2020-08-01' and '2020-08-31' OR DataRegistro between '2021-08-01' and '2021-08-31' OR DataRegistro between '2022-08-01' and '2022-08-31' order by Promissorias", conexao);
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-09-01' and '2019-09-30' OR DataRegistro between '2020-09-01' and '2020-09-30' OR DataRegistro between '2021-09-01' and '2021-09-30' OR DataRegistro between '2022-09-01' and '2022-09-30' order by Promissorias", conexao);
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-10-01' and '2019-10-31' OR DataRegistro between '2020-10-01' and '2020-10-31' OR DataRegistro between '2021-10-01' and '2021-10-31' OR DataRegistro between '2022-10-01' and '2022-10-31' order by Promissorias", conexao);
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-11-01' and '2019-11-30' OR DataRegistro between '2020-11-01' and '2020-11-30' OR DataRegistro between '2021-11-01' and '2021-11-30' OR DataRegistro between '2022-11-01' and '2022-11-30' order by Promissorias", conexao);
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-12-01' and '2019-12-31' OR DataRegistro between '2020-12-01' and '2020-12-31' OR DataRegistro between '2021-12-01' and '2021-12-31' OR DataRegistro between '2022-12-01' and '2022-12-31' order by Promissorias", conexao);
                        break;
                    default:
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2100-12-01' and '2100-12-31' order by Promissorias", conexao);
                        MessageBox.Show("Você digitou um mês incorreto! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }

            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesResumoDeMovimentoCidade(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-01-01' and '2019-01-31' and rota = @rota OR DataRegistro between '2020-01-01' and '2020-01-31' and rota = @rota OR DataRegistro between '2021-01-01' and '2021-01-31' and rota = @rota OR DataRegistro between '2022-01-01' and '2022-01-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-02-01' and '2019-02-28' and rota = @rota OR DataRegistro between '2020-02-01' and '2020-02-29' and rota = @rota OR DataRegistro between '2021-02-01' and '2021-02-28' and rota = @rota OR DataRegistro between '2022-02-01' and '2022-02-28' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-03-01' and '2019-03-31' and rota = @rota OR DataRegistro between '2020-03-01' and '2020-03-31' and rota = @rota OR DataRegistro between '2021-03-01' and '2021-03-31' and rota = @rota OR DataRegistro between '2022-03-01' and '2022-03-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-04-01' and '2019-04-30' and rota = @rota OR DataRegistro between '2020-04-01' and '2020-04-30' and rota = @rota OR DataRegistro between '2021-04-01' and '2021-04-30' and rota = @rota OR DataRegistro between '2022-04-01' and '2022-04-30' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-05-01' and '2019-05-31' and rota = @rota OR DataRegistro between '2020-05-01' and '2020-05-31' and rota = @rota OR DataRegistro between '2021-05-01' and '2021-05-31' and rota = @rota OR DataRegistro between '2022-05-01' and '2022-05-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-06-01' and '2019-06-30' and rota = @rota OR DataRegistro between '2020-06-01' and '2020-06-30' and rota = @rota OR DataRegistro between '2021-06-01' and '2021-06-30' and rota = @rota OR DataRegistro between '2022-06-01' and '2022-06-30' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-07-01' and '2019-07-31' and rota = @rota OR DataRegistro between '2020-07-01' and '2020-07-31' and rota = @rota OR DataRegistro between '2021-07-01' and '2021-07-31' and rota = @rota OR DataRegistro between '2022-07-01' and '2022-07-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-08-01' and '2019-08-31' and rota = @rota OR DataRegistro between '2020-08-01' and '2020-08-31' and rota = @rota OR DataRegistro between '2021-08-01' and '2021-08-31' and rota = @rota OR DataRegistro between '2022-08-01' and '2022-08-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-09-01' and '2019-09-30' and rota = @rota OR DataRegistro between '2020-09-01' and '2020-09-30' and rota = @rota OR DataRegistro between '2021-09-01' and '2021-09-30' and rota = @rota OR DataRegistro between '2022-09-01' and '2022-09-30' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-10-01' and '2019-10-31' and rota = @rota OR DataRegistro between '2020-10-01' and '2020-10-31' and rota = @rota OR DataRegistro between '2021-10-01' and '2021-10-31' and rota = @rota OR DataRegistro between '2022-10-01' and '2022-10-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-11-01' and '2019-11-30' and rota = @rota OR DataRegistro between '2020-11-01' and '2020-11-30' and rota = @rota OR DataRegistro between '2021-11-01' and '2021-11-30' and rota = @rota OR DataRegistro between '2022-11-01' and '2022-11-30' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2019-12-01' and '2019-12-31' and rota = @rota OR DataRegistro between '2020-12-01' and '2020-12-31' and rota = @rota OR DataRegistro between '2021-12-01' and '2021-12-31' and rota = @rota OR DataRegistro between '2022-12-01' and '2022-12-31' and rota = @rota order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                    default:
                        MessageBox.Show("Você digitou a cidade ou o mês incorretamente! Digite novamente.", "Alerta");
                        comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where DataRegistro between '2100-12-01' and '2100-12-31' order by Promissorias", conexao);
                        comando.Parameters.Add("@rota", MySqlDbType.String).Value = produto.rota;
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;

            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public DataTable FiltroPorMesApuracaoDeValores(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                switch (produto.mes)
                {
                    case "TODOS REGISTROS":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao", conexao);
                        break;
                    case "JANEIRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-01-01' and '2019-01-31' OR DataApuracao between '2020-01-01' and '2020-01-31' OR DataApuracao between '2021-01-01' and '2021-01-31' OR DataApuracao between '2022-01-01' and '2022-01-31'", conexao);
                        break;
                    case "FEVEREIRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-02-01' and '2019-02-28' OR DataApuracao between '2020-02-01' and '2020-02-29' OR DataApuracao between '2021-02-01' and '2021-02-28' OR DataApuracao between '2022-02-01' and '2022-02-28'", conexao);
                        break;
                    case "MARÇO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-03-01' and '2019-03-31' OR DataApuracao between '2020-03-01' and '2020-03-31' OR DataApuracao between '2021-03-01' and '2021-03-31' OR DataApuracao between '2022-03-01' and '2022-03-31'", conexao);
                        break;
                    case "ABRIL":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-04-01' and '2019-04-30' OR DataApuracao between '2020-04-01' and '2020-04-30' OR DataApuracao between '2021-04-01' and '2021-04-30' OR DataApuracao between '2022-04-01' and '2022-04-30'", conexao);
                        break;
                    case "MAIO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-05-01' and '2019-05-31' OR DataApuracao between '2020-05-01' and '2020-05-31' OR DataApuracao between '2021-05-01' and '2021-05-31' OR DataApuracao between '2022-05-01' and '2022-05-31'", conexao);
                        break;
                    case "JUNHO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-06-01' and '2019-06-30' OR DataApuracao between '2020-06-01' and '2020-06-30' OR DataApuracao between '2021-06-01' and '2021-06-30' OR DataApuracao between '2022-06-01' and '2022-06-30'", conexao);
                        break;
                    case "JULHO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-07-01' and '2019-07-31' OR DataApuracao between '2020-07-01' and '2020-07-31' OR DataApuracao between '2021-07-01' and '2021-07-31' OR DataApuracao between '2022-07-01' and '2022-07-31'", conexao);
                        break;
                    case "AGOSTO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-08-01' and '2019-08-31' OR DataApuracao between '2020-08-01' and '2020-08-31' OR DataApuracao between '2021-08-01' and '2021-08-31' OR DataApuracao between '2022-08-01' and '2022-08-31'", conexao);
                        break;
                    case "SETEMBRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-09-01' and '2019-09-30' OR DataApuracao between '2020-09-01' and '2020-09-30' OR DataApuracao between '2021-09-01' and '2021-09-30' OR DataApuracao between '2022-09-01' and '2022-09-30'", conexao);
                        break;
                    case "OUTUBRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-10-01' and '2019-10-31' OR DataApuracao between '2020-10-01' and '2020-10-31' OR DataApuracao between '2021-10-01' and '2021-10-31' OR DataApuracao between '2022-10-01' and '2022-10-31'", conexao);
                        break;
                    case "NOVEMBRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-11-01' and '2019-11-30' OR DataApuracao between '2020-11-01' and '2020-11-30' OR DataApuracao between '2021-11-01' and '2021-11-30' OR DataApuracao between '2022-11-01' and '2022-11-30'", conexao);
                        break;
                    case "DEZEMBRO":
                        comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2019-12-01' and '2019-12-31' OR DataApuracao between '2020-12-01' and '2020-12-31' OR DataApuracao between '2021-12-01' and '2021-12-31' OR DataApuracao between '2022-12-01' and '2022-12-31'", conexao);
                        break;
                    default:
                        //comando = new MySqlCommand("Select CodApuracao, DataApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte From Apuracao where DataApuracao between '2100-12-01' and '2100-12-31'", conexao);
                        //MessageBox.Show("Você digitou um mês incorreto! Digite novamente.", "Alerta");
                        break;
                }
                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public void InserirHistoricoDeRegistro(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("INSERT INTO Registros (CodRegistro, Promissorias, Consignados, ProdutosDevolvidos, ValorRecebido, Vendas_codLote, NomeProduto, Cobrador, Rota, PrecoProduto) values (null, @Promissorias, @Consignados, @ProdutosDevolvidos, @ValorRecebido, @Vendas_CodLote, @NomeProduto, @Cobrador, @Rota, @PrecoProduto)", conexao);
                comando.Parameters.AddWithValue("@Vendas_codLote", produto.vendas_CodLotes);
                comando.Parameters.AddWithValue("@Promissorias", produto.promissorias);
                comando.Parameters.AddWithValue("@Consignados", produto.consignados);
                comando.Parameters.AddWithValue("@ProdutosDevolvidos", produto.produtosDevolvidos);
                comando.Parameters.AddWithValue("@ValorRecebido", produto.valorRecebido);
                comando.Parameters.AddWithValue("@NomeProduto", produto.nomeProduto);
                comando.Parameters.AddWithValue("@Cobrador", produto.cobrador);
                comando.Parameters.AddWithValue("@Rota", produto.rota);
                comando.Parameters.AddWithValue("@PrecoProduto", produto.precoProduto);

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Inserido com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção de dados no resumo de movimento! MtdBd04 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void SelectRegistroMovimento(Produto produto)
        {
            try
            {
                AbrirConexao();
                MySqlCommand comando = new MySqlCommand("select sum(ValorRecebido), sum(ProdutosDevolvidos), sum(Consignados) from Registros where Promissorias = @Promissorias and NomeProduto = @NomeProduto and Rota = @Rota and Cobrador = @Cobrador", conexao);
                comando.Parameters.Clear();
                comando.Parameters.Add("@ValorRecebido", MySqlDbType.Double).Value = produto.valorRecebido;
                comando.Parameters.Add("@ProdutosDevolvidos", MySqlDbType.Int32).Value = produto.produtosDevolvidos;
                comando.Parameters.Add("@Consignados", MySqlDbType.Int32).Value = produto.consignados;
                comando.Parameters.Add("@Promissorias", MySqlDbType.Int32).Value = produto.promissorias;
                comando.Parameters.Add("@NomeProduto", MySqlDbType.String).Value = produto.nomeProduto;
                comando.Parameters.Add("@Rota", MySqlDbType.String).Value = produto.rota;
                comando.Parameters.Add("@Cobrador", MySqlDbType.String).Value = produto.cobrador;

                comando.CommandType = CommandType.Text;
                MySqlDataReader dr;
                dr = comando.ExecuteReader();
                dr.Read();
                produto.valorRecebido = dr.GetDouble(0);
                produto.produtosDevolvidos = dr.GetInt32(1);
                produto.consignados = dr.GetInt32(2);
                FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na soma de valores dos registros! MtdBd05 " + erro);
            }
        }
        public void SelectRegistroMovimentoExcluir(Produto produto)
        {
            try
            {
                AbrirConexao();
                MySqlCommand comando = new MySqlCommand("select sum(ValorRecebido), sum(ProdutosDevolvidos), sum(Consignados), PrecoProduto from Registros where Promissorias = @Promissorias and NomeProduto = @NomeProduto", conexao);
                comando.Parameters.Clear();
                comando.Parameters.Add("@ValorRecebido", MySqlDbType.Double).Value = produto.valorRecebido;
                comando.Parameters.Add("@ProdutosDevolvidos", MySqlDbType.Int32).Value = produto.produtosDevolvidos;
                comando.Parameters.Add("@Consignados", MySqlDbType.Int32).Value = produto.consignados;
                comando.Parameters.Add("@Promissorias", MySqlDbType.Int32).Value = produto.promissorias;
                comando.Parameters.Add("@NomeProduto", MySqlDbType.String).Value = produto.nomeProduto;
                comando.Parameters.Add("@PrecoProduto", MySqlDbType.Double).Value = produto.precoProduto;


                comando.CommandType = CommandType.Text;
                MySqlDataReader dr;
                dr = comando.ExecuteReader();
                dr.Read();
                produto.valorRecebido = dr.GetDouble(0);
                produto.produtosDevolvidos = dr.GetInt32(1);
                produto.consignados = dr.GetInt32(2);
                produto.precoProduto = dr.GetDouble(3);
                FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na soma de valores dos registros! MtdBd05 " + erro);
            }
        }
        public void UpdateResumoDeMovimentoExcluir(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Update Registros set ValorTotalRecebido = @ValorTotalRecebido, ProdutoTotalDevolvido = @ProdutoTotalDevolvido, ProdutosPendente = @ProdutosPendente, ValorTotalConsignado = @ValorTotalConsignado where Promissorias = @Promissorias and NomeProduto = @NomeProduto", conexao);
                comando.Parameters.AddWithValue("@ValorTotalRecebido", produto.valorTotalRecebido);
                comando.Parameters.AddWithValue("@ProdutoTotalDevolvido", produto.produtoTotalDevolvido);
                comando.Parameters.AddWithValue("@ProdutosPendente", produto.produtosPendentes);
                comando.Parameters.AddWithValue("@ValorTotalConsignado", produto.valorTotalConsignado);
                comando.Parameters.AddWithValue("@Promissorias", produto.promissorias);
                comando.Parameters.AddWithValue("@NomeProduto", produto.nomeProduto);

                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na atualização no resumo de movimento! MtdBd06 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void SelectVendasParaRM(Produto produto)
        {
            try
            {
                AbrirConexao();
                MySqlCommand comando = new MySqlCommand("Select NomeProduto, Cobrador, Rota, PrecoProduto from vendas where codLote = @codLote", conexao);
                comando.Parameters.Clear();
                comando.Parameters.Add("@CodLote", MySqlDbType.Int32).Value = produto.codLote;
                comando.Parameters.Add("@NomeProduto", MySqlDbType.String).Value = produto.nomeProduto;
                comando.Parameters.Add("@Cobrador", MySqlDbType.String).Value = produto.cobrador;
                comando.Parameters.Add("@Rota", MySqlDbType.String).Value = produto.rota;
                comando.Parameters.Add("@PrecoProduto", MySqlDbType.Double).Value = produto.precoProduto;

                comando.CommandType = CommandType.Text;
                MySqlDataReader dr;
                dr = comando.ExecuteReader();
                dr.Read();
                produto.nomeProduto = dr.GetString(0);
                produto.cobrador = dr.GetString(1);
                produto.rota = dr.GetString(2);
                produto.precoProduto = dr.GetDouble(3);
                FecharConexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na soma de valores dos registros! MtdBd05 " + erro);
            }
        }
        public void UpdateResumoDeMovimento(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("Update Registros set ValorTotalRecebido = @ValorTotalRecebido, ProdutoTotalDevolvido = @ProdutoTotalDevolvido, ProdutosPendente = @ProdutosPendente, ValorTotalConsignado = @ValorTotalConsignado where Promissorias = @Promissorias and NomeProduto = @NomeProduto and Rota = @rota and Cobrador = @Cobrador", conexao);
                comando.Parameters.AddWithValue("@ValorTotalRecebido", produto.valorTotalRecebido);
                comando.Parameters.AddWithValue("@ProdutoTotalDevolvido", produto.produtoTotalDevolvido);
                comando.Parameters.AddWithValue("@ProdutosPendente", produto.produtosPendentes);
                comando.Parameters.AddWithValue("@ValorTotalConsignado", produto.valorTotalConsignado);
                comando.Parameters.AddWithValue("@Promissorias", produto.promissorias);
                comando.Parameters.AddWithValue("@NomeProduto", produto.nomeProduto);
                comando.Parameters.AddWithValue("@Rota", produto.rota);
                comando.Parameters.AddWithValue("@Cobrador", produto.cobrador);

                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na atualização no resumo de movimento! MtdBd06 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void HistoricoDeRegistroNomeDataPendente(Produto produto)
        {
            try
            {
                AbrirConexao();

                MySqlCommand comando = new MySqlCommand("SELECT NomeProduto, DataSaida, SaidaMenosDevolucoes from Vendas where CodLote = @CodLote", conexao);
                comando.Parameters.Clear();
                comando.Parameters.Add("@CodLote", MySqlDbType.Int32).Value = produto.codLote;
                comando.Parameters.Add("@NomeProduto", MySqlDbType.String).Value = produto.nomeProduto;
                comando.Parameters.Add("@DataSaida", MySqlDbType.DateTime).Value = produto.dataSaida;
                comando.Parameters.Add("@SaidaMenosDevolucoes", MySqlDbType.Int32).Value = produto.saidaMenosDevolucoes;


                comando.CommandType = CommandType.Text;

                MySqlDataReader dr;

                dr = comando.ExecuteReader();
                dr.Read();
                produto.nomeProduto = dr.GetString(0);
                produto.dataSaida = dr.GetDateTime(1);
                produto.saidaMenosDevolucoes = dr.GetInt32(2);
                FecharConexao();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Este Código não Existe!", "Alerta");
                throw;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na exibição de dados no historico de registro! MtdBD07 " + erro);
            }
        }
        public void InserirApuracao(Produto produto)
        {
            try
            {
                AbrirConexao();
                comando = new MySqlCommand("INSERT INTO Apuracao (CodApuracao, Receitas, Despesas, Faturamento, DataDe, DataAte) values (null, @Receitas, @Despesas, @Faturamento, @DataDe, @DataAte)", conexao);
                comando.Parameters.AddWithValue("@Receitas", produto.receitas);
                comando.Parameters.AddWithValue("@Despesas", produto.despesas);
                comando.Parameters.AddWithValue("@Faturamento", produto.faturamento);
                comando.Parameters.AddWithValue("@DataDe", produto.dataDE);
                comando.Parameters.AddWithValue("@DataAte", produto.dataATE);
                comando.ExecuteNonQuery();
                MessageBox.Show("Dados salvos com sucesso!", "Alerta");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção de dados da apuração no banco de dados! MtbBd11 " + erro);
            }
            finally
            {
                FecharConexao();
            }
        }
        public void SelectApuracaoDeValores(Produto produto)
        {
            try
            {
                AbrirConexao();

                MySqlCommand comando = new MySqlCommand("select sum(ValorRecebido) from registros where DataRegistro between '" + produto.dataDE.ToString("yyyy-MM-dd 00:00:00") + " 00:00:00'  AND '" + produto.dataATE.ToString("yyyy-MM-dd 23:59:59") + " 23:59:59'", conexao);

                MySqlDataReader dr;
                dr = comando.ExecuteReader();
                dr.Read();
                produto.valorRecebido = dr.GetDouble(0);
            }
            catch (SqlNullValueException)
            {
                MessageBox.Show("Não há registros para esse período!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable BuscarPromissoriaRM(Produto produto)
        {
            try
            {
                AbrirConexao();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                comando = new MySqlCommand("Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where Promissorias = @Promissorias order by Promissorias", conexao);
                comando.Parameters.Add("@Promissorias", MySqlDbType.Int32).Value = produto.promissorias;

                da.SelectCommand = comando;
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        public void SelectValidacaoPromissoriaRM(Produto produto)
        {
            AbrirConexao();
            MySqlCommand comando = new MySqlCommand("Select * from registros where Promissorias = @Promissorias", conexao);
            comando.Parameters.Clear();
            comando.Parameters.Add("@Promissorias", MySqlDbType.Int32).Value = produto.promissorias;
            comando.Parameters.Add("@Promissorias", MySqlDbType.Int32).Value = produto.promissorias;

        }
    }
}
