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
                        Console.WriteLine("Livro adicionado com sucesso!");
                    else
                        Console.WriteLine("Nenhum registro foi inserido.");
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
    
                            Console.WriteLine("|{0}             |{1}            |{2}              |R${3}      ", reader["cod_livro"], reader["titulo"],reader["quantidade"], reader["preco"]);
                            Console.WriteLine("===================================================================");

                        }
                        reader.Close();
                    }


                }
                

            }
        }

        public void AlugarLivro(Conexao conexao)
        {

        }

        //public void demoInserts(Conexao conexao)
        //{
        //    try
        //    {
        //        // Inserir AUTOR
        //        string sqlAutor = @"INSERT IGNORE INTO autor(cod_aut, nome_aut) VALUES 
        //            (1, 'Rick Riordan'),
        //            (2, 'Eduardo Spohr'),
        //            (3, 'Stephen King'),
        //            (4, 'Pierre Boulle'),
        //            (5, 'Richard Matheson'),
        //            (6, 'George Orwell'),
        //            (7, 'Franz Kafka'),
        //            (8, 'Walter Isacson'),
        //            (9, 'Ernest Cline'),
        //            (10, 'Baruch Espinoza');";

        //        // Inserir EDITORA
        //        string sqlEditora = @"INSERT IGNORE INTO editora(cod_edit, nome_edit) VALUES 
        //            (1, 'Intrinseca'),
        //            (2, 'Leya'),
        //            (3, 'Ponto de Leitura'),
        //            (4, 'Aleph'),
        //            (5, 'Secker'),
        //            (6, 'Suma'),
        //            (7, 'Companhia das Letras'),
        //            (8, 'Lebooks Editora');";

        //        // Inserir CATEGORIA
        //        string sqlCategoria = @"INSERT IGNORE INTO categoria(cod_cat, nome_cat) VALUES 
        //            (1, 'Infanto-Juvenil'),
        //            (2, 'Romance'),
        //            (3, 'Terror'),
        //            (4, 'Ficção científica'),
        //            (5, 'Drama'),
        //            (6, 'Suspense'),
        //            (7, 'Novela'),
        //            (8, 'Biografia'),
        //            (9, 'Aventura'),
        //            (10, 'Filosofia');";

        //        // Inserir CLIENTE
        //        string sqlCliente = @"INSERT IGNORE INTO cliente(cod_cli, cod_cid, nome_cli, endereco_cli, sexo_cli) VALUES
        //            (1, 1, 'José Nogueira', 'Rua A',  'M'),
        //            (2, 1, 'Angelo Pereira', 'Rua B',  'M'),
        //            (3, 1, 'Além Mar Paranhos', 'Rua C',  'F'),
        //            (4, 1, 'Catarina Souza', 'Rua D',  'F'),
        //            (5, 1, 'Vagner Costa', 'Rua E', 'M'),
        //            (6, 2, 'Antenor da Costa', 'Rua F',  'M'),
        //            (7, 2, 'Maria Amélia de Sousa', 'Rua G', 'F'),
        //            (8, 2, 'Paulo Roberto Silva', 'Rua H', 'M'),
        //            (9, 3, 'Fátima Souza', 'Rua I',  'F'),
        //            (10, 3, 'Joel da Rocha', 'Rua J',  'M');";

        //        // Executar comandos
        //        MySqlCommand cmd = new MySqlCommand(sqlAutor, conexao.con);
        //        cmd.ExecuteNonQuery();
        //        Console.WriteLine("Autores inseridos.");

        //        cmd.CommandText = sqlEditora;
        //        cmd.ExecuteNonQuery();
        //        Console.WriteLine("Editoras inseridas.");

        //        cmd.CommandText = sqlCategoria;
        //        cmd.ExecuteNonQuery();
        //        Console.WriteLine("Categorias inseridas.");

        //        cmd.CommandText = sqlCliente;
        //        cmd.ExecuteNonQuery();
        //        Console.WriteLine("Clientes inseridos.");

        //        Console.WriteLine("Todos os dados de demonstração foram inseridos com sucesso.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erro: " + ex.Message);
        //    }
        //}

        //Console.WriteLine("Pressione qualquer tecla para sair...");
        //Console.ReadKey();
    }
}
