using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Retaguarda
{
    public class Conexao
    {
        string conecta = "server=localhost;user id=root;password=12345678;database=Retaguarda";
        protected MySqlConnection conexao = null;

        //Método para conectar no banco
        public void AbrirConexao()
        {
            try
            {
                conexao = new MySqlConnection(conecta);
                conexao.Open();
            }
            catch(Exception erro)
            {
                MessageBox.Show("O banco de dados não foi iniciado corretamente! ERRO 01" + erro);
            }
        }
        //Método para fechar encerrar a conexao com o bancO
        public void FecharConexao()
        {
            try
            {
                conexao = new MySqlConnection(conecta);
                conexao.Close();
            }
            catch(Exception erro)
            {
                 MessageBox.Show("O banco de dados não foi finalizado corretamente! ERRO 02" + erro);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
