using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Retaguarda
{
    public class Metodos
    {
        MtdBd metodos = new MtdBd();

        //Metodo para inserir no banco de dados
        public void InserirSD(Produto produto)
        {
            try
            {
                metodos.InserirSD(produto);

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção do produto no banco de dados! METODOS01 " + erro);
            }
        }
        public void EditarSD(Produto produto)
        {
            try
            {
                metodos.EditarSD(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! METODOS02 " + erro);
            }
        }
        public void ExcluirSD(Produto produto)
        {
            try
            {
                metodos.ExcluirSD(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! Metodos03 " + erro);
            }
        }
        public void ExcluirHR(Produto produto)
        {
            try
            {
                metodos.ExcluirHR(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! Metodos03 " + erro);
            }
        }
        public void ExcluirAPV(Produto produto)
        {
            try
            {
                metodos.ExcluirAPV(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! Metodos03 " + erro);
            }
        }
        public DataTable FiltroPorMesSaidaDiaria(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesSaidaDiaria(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesSaidaDiariaCidade(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesSaidaDiariaCidade(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable ListarRegistroSaidaDiaria()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.ListarRegistroSaidaDiaria();
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable BuscarSaidaDiaria(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.BuscarSaidaDiaria(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesHistoricoDeRegistro(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesHistoricoDeRegistro(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesHistoricoDeRegistroCidade(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesHistoricoDeRegistroCidade(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable ListarUltimoRegistroHistoricoDeRegistro(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.ListarUltimoRegistroHistoricoDeRegistro(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable BuscarHistoricoDeRegistro(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.BuscarHistoricoDeRegistro(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable ListarResumoDeMovimento()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.ListarResumoDeMovimento();
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesResumoDeMovimento(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesResumoDeMovimento(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesResumoDeMovimentoCidade(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesResumoDeMovimentoCidade(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public DataTable FiltroPorMesApuracaoDeValores(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.FiltroPorMesApuracaoDeValores(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        public void InserirHistoricoDeRegistro(Produto produto)
        {
            try
            {
                metodos.InserirHistoricoDeRegistro(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção de dados no resumo de movimento! METODOS03 " + erro);
            }
        }
        public void SelectRegistroMovimento(Produto produto)
        {
            try
            {
                metodos.SelectRegistroMovimento(produto);

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na soma de valores dos registros! METODOS04 " + erro);
            }
        }
        public void SelectVendasParaRM(Produto produto)
        {
            try
            {
                metodos.SelectVendasParaRM(produto);

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na soma de valores dos registros! METODOS04 " + erro);
            }
        }
        public void SelectRegistroMovimentoExcluir(Produto produto)
        {
            try
            {
                metodos.SelectRegistroMovimentoExcluir(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! Metodos03 " + erro);
            }
        }
        public void UpdateResumoDeMovimentoExcluir(Produto produto)
        {
            try
            {
                metodos.UpdateResumoDeMovimentoExcluir(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na edição de dados do produto no banco de dados! Metodos03 " + erro);
            }
        }
        public void UpdateResumoDeMovimento(Produto produto)
        {
            try
            {
                metodos.UpdateResumoDeMovimento(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na atualização no resumo de movimento! METODOS05 " + erro);
            }
        }
        public void HistoricoDeRegistroNomeDataPendente(Produto produto)
        {            
            try
            {
                metodos.HistoricoDeRegistroNomeDataPendente(produto);
            }
            catch (MySqlException)
            {
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na exibição de dados no historico de registro! METODOS06 " + erro);
            }
        }
        public void InserirApuracao(Produto produto)
        {
            try
            {
                metodos.InserirApuracao(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na inserção de dados da apuração no banco de dados! METODOS11 " + erro);
            }
        }
        public void SelectApuracaoDeValores(Produto produto)
        {
            try
            {
                metodos.SelectApuracaoDeValores(produto);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na seleção de valores 2019! METODOS07 " + erro);
            }
        }
        public DataTable BuscarPromissoriaRM(Produto produto)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.BuscarPromissoriaRM(produto);
                return dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}
