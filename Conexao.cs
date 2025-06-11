using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;

namespace Biblioteca
{
    internal class Conexao
    {

        public MySqlConnection con { get; set; }
        public virtual bool ConectarBanco(string CaminhoProBanco)
        {

            string Caminho = CaminhoProBanco;


            try
            {
                con = new MySqlConnection(Caminho);
                con.Open();
                Console.WriteLine("Conexão estabelecida");
                return true;
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao conectar");
                Console.WriteLine(erro.Message);
                con = null;
                return false;
            }


        }

        public void Desconectar()
        {
            if (con != null)
            {
                con.Close();
                Console.WriteLine("Conexão encerrada.");
            }
        }
    }
}
