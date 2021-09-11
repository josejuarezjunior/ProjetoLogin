using ProjetoLogin.DAL;

namespace ProjetoLogin.Modelo
{
    public class Controle
    {
        public bool tem = false;
        public string mensagem = "";
        public bool acessar(string login, string senha)
        {
            LoginDaoComandos loginDao = new LoginDaoComandos();
            tem = loginDao.verificarLogin(login, senha);
            /*
             * Verificando se a mensagem do objeto "loginDao"
             * é diferente de vazio. Pois se for diferente de vazio,
             * é por que houve um erro.
             */
            if (!loginDao.mensagem.Equals(""))
            {
                this.mensagem = loginDao.mensagem;
            }
            return tem;
        }

        public string cadastrar(string email, string senha, string confSenha)
        {
            LoginDaoComandos loginDao = new LoginDaoComandos();
            //Essa mensagem é o retorno do método cadastrar da classe "LoginDaoComando".
            this.mensagem = loginDao.cadastrar(email, senha, confSenha);
            if (loginDao.tem)
            {
                this.tem = true;
            }
            return mensagem;
        }
    }
}
