using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    internal class Estoque
    {

        public void EntradaLivro(Conexao conexao)
        {
            try
            {
                if (conexao.con == null || conexao.con.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Conexão com o banco não está disponível");
                    return;
                }

                Console.WriteLine("Nome do Livro: ");
                string titulo = Console.ReadLine();
                Console.WriteLine("Categoria: ");
                string categoria = Console.ReadLine();
                Console.WriteLine("Autor: ");
                string autor = Console.ReadLine();
                Console.WriteLine("Editora: ");
                string editora = Console.ReadLine();
                Console.WriteLine("Quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());
                Console.WriteLine("Preço: ");
                double preco = double.Parse(Console.ReadLine());

                string sql = "INSERT INTO livro (titulo, categoria, autor, editora, quantidade, preco) " +
                             "VALUES (@titulo, @categoria, @autor, @editora, @quantidade, @preco)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao.con))
                {
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@categoria", categoria);
                    cmd.Parameters.AddWithValue("@autor", autor);
                    cmd.Parameters.AddWithValue("@editora", editora);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@preco", preco);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Livro adicionado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nenhum registro foi inserido.");
                    }
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao inserir livro: {0}", erro.Message);
            }
        }

        public void VenderLivro(Conexao conexao)
        {
            try
            {
                if (conexao.con == null)
                {
                    Console.WriteLine("Não foi possível conectar com o banco");
                    return;
                }
                else
                {
                    Console.Write("Nome do Livro: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Quantidade a ser retirada: ");
                    int quantidade = int.Parse(Console.ReadLine());

                    string sql = "SELECT quantidade, preco FROM livro WHERE titulo=@titulo";
                    MySqlCommand cmd = new MySqlCommand(sql, conexao.con);
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int quantidadeAtual = reader.GetInt32(0);
                        double preco = reader.GetDouble(1);
                        if (quantidadeAtual >= quantidade)
                        {
                            reader.Close();
                            sql = "UPDATE livro SET quantidade = quantidade - @quantidade WHERE titulo = @titulo";
                            cmd = new MySqlCommand(sql, conexao.con);
                            cmd.Parameters.AddWithValue("@quantidade", quantidade);
                            cmd.Parameters.AddWithValue("@titulo", titulo);

                            int rowsUpdated = cmd.ExecuteNonQuery();
                            if (rowsUpdated > 0) {
                                Console.WriteLine($"Venda realizada! Valor total: R$ {quantidade * preco:F2}");

                            } else
                            {
                                Console.WriteLine("Nenhum registro foi atualizado");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade insuficiente no estoque.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Livro não encontrado.");
                    }


                }

            }
            catch(Exception erro)
            {
                Console.WriteLine("Erro ao alugar o livro {0}", erro.Message);
            }
            
        }

        public void ConsultarEstoque(Conexao conexao)
        {
            if (conexao.con == null)
            {
                Console.WriteLine("Não foi possível conectar com o banco");
                return;
            }
            else
            {
                string sql = "SELECT * FROM livro";
                using (MySqlCommand cmd = new MySqlCommand(sql, conexao.con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Estoque:");
                        Console.WriteLine("===================================================================");
                        Console.WriteLine("|ID            |Título                   |QTD            |Preço             ");
                        while (reader.Read())
                        {
    
                            Console.WriteLine("|{0}             |{1}            |{2}              |R${3}      ",reader["cod_livro"], reader["titulo"],reader["quantidade"], reader["preco"]);
                            Console.WriteLine("===================================================================");

                        }
                        reader.Close();
                    }


                }
                

            }
        }


        
    }


}
