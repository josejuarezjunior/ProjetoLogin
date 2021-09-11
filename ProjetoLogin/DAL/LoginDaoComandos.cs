using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLogin.DAL
{
    class LoginDaoComandos
    {
        //À princípio, essa variável será "false".
        public bool tem = false;

        public string mensagem = "";

        //Instanciando objeto para utilizar comandos SQL
        SqlCommand cmd = new SqlCommand();

        //Instanciando a classe de conexão
        Conexao con = new Conexao();

        //Classe para guardar informações vindas do banco de dados.
        SqlDataReader dr;
        public bool verificarLogin(string login, string senha)
        {
            //Comandos em SQL para procurar o login/senha no banco de dados.
            cmd.CommandText = "select * from Logins where email = @login and senha = @senha";
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha", senha);
            try
            {
                //Conectando com o banco de dados
                cmd.Connection = con.conectar();

                /*
                 * Irá executar a linha do "cmd.CommandText" e guardará
                 * as informações que foram buscadas, nesse caso
                 * com o comando "select".
                 */
                dr = cmd.ExecuteReader();

                /* Verificando se há no banco de dados, linhas com o
                 * login e senha informados.
                 */
                if (dr.HasRows)
                {
                    tem = true;
                }
                con.desconectar();
                dr.Close();
            }
            catch (SqlException)
            {

                this.mensagem = "Erro na conexão com o banco de dados!";
            }
            return tem;
        }
        public string cadastrar(string email, string senha, string confSenha)
        {
            if(senha.Equals(confSenha))
            {
                cmd.CommandText = "insert into Logins values (@e, @s);";
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@s", senha);
                try
                {
                    //Conexão com o banco de dados, através da Classe Conexão(Objeto "con").
                    cmd.Connection = con.conectar();
                    //Executando os comandos SQL
                    cmd.ExecuteNonQuery();
                    con.desconectar();
                    this.mensagem = "Cadastrado com sucesso!";
                    tem = true;
                }
                catch (SqlException)
                {

                    this.mensagem = "Erro ao conectar o banco de dados!";
                }
            }
            else
            {
                this.mensagem = "Senhas não correspondem!";
            }
            return mensagem;
        }
    }
}
