using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;

namespace Biblioteca
{
    internal class Conexao
    {

    public virtual void ConectarBanco(string CaminhoProBanco)
        {
            
            string Caminho = CaminhoProBanco;
            MySqlConnection conexao = new MySqlConnection(Caminho);

            try
            {
                conexao.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return;
        }

    }
}
