using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    class Demonstração_INSERT
    { }

namespace Biblioteca
    {
        class Demonstração_INSERT
        {
            static void Main(string[] args)
            {
                string connStr = "server=127.0.0.1;uid=root;pwd=root;database=biblioteca";

                using (MySqlConnection conexao = new MySqlConnection(connStr))
                {
                    try
                    {
                        conexao.Open();
                        Console.WriteLine("Conexão estabelecida com sucesso!");

                        // Inserir AUTOR
                        string sqlAutor = @"INSERT IGNORE INTO autor(cod_aut, nome_aut) VALUES 
                        (1, 'Rick Riordan'),
                        (2, 'Eduardo Spohr'),
                        (3, 'Stephen King'),
                        (4, 'Pierre Boulle'),
                        (5, 'Richard Matheson'),
                        (6, 'George Orwell'),
                        (7, 'Franz Kafka'),
                        (8, 'Walter Isacson'),
                        (9, 'Ernest Cline'),
                        (10, 'Baruch Espinoza');";

                        // Inserir EDITORA
                        string sqlEditora = @"INSERT IGNORE INTO editora(cod_edit, nome_edit) VALUES 
                        (1, 'Intrinseca'),
                        (2, 'Leya'),
                        (3, 'Ponto de Leitura'),
                        (4, 'Aleph'),
                        (5, 'Secker'),
                        (6, 'Suma'),
                        (7, 'Companhia das Letras'),
                        (8, 'Lebooks Editora');";

                        // Inserir CATEGORIA
                        string sqlCategoria = @"INSERT IGNORE INTO categoria(cod_cat, nome_cat) VALUES 
                        (1, 'Infanto-Juvenil'),
                        (2, 'Romance'),
                        (3, 'Terror'),
                        (4, 'Ficção científica'),
                        (5, 'Drama'),
                        (6, 'Suspense'),
                        (7, 'Novela'),
                        (8, 'Biografia'),
                        (9, 'Aventura'),
                        (10, 'Filosofia');";

                        // Inserir CLIENTE
                        string sqlCliente = @"INSERT IGNORE INTO cliente(cod_cli, cod_cid, nome_cli, endereco_cli, sexo_cli) VALUES
                        (1, 1, 'José Nogueira', 'Rua A',  'M'),
                        (2, 1, 'Angelo Pereira', 'Rua B',  'M'),
                        (3, 1, 'Além Mar Paranhos', 'Rua C',  'F'),
                        (4, 1, 'Catarina Souza', 'Rua D',  'F'),
                        (5, 1, 'Vagner Costa', 'Rua E', 'M'),
                        (6, 2, 'Antenor da Costa', 'Rua F',  'M'),
                        (7, 2, 'Maria Amélia de Sousa', 'Rua G', 'F'),
                        (8, 2, 'Paulo Roberto Silva', 'Rua H', 'M'),
                        (9, 3, 'Fátima Souza', 'Rua I',  'F'),
                        (10, 3, 'Joel da Rocha', 'Rua J',  'M');";

                        // Executar comandos
                        MySqlCommand cmd = new MySqlCommand(sqlAutor, conexao);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Autores inseridos.");

                        cmd.CommandText = sqlEditora;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Editoras inseridas.");

                        cmd.CommandText = sqlCategoria;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Categorias inseridas.");

                        cmd.CommandText = sqlCliente;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Clientes inseridos.");

                        Console.WriteLine("Todos os dados de demonstração foram inseridos com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                }

                Console.WriteLine("Pressione qualquer tecla para sair...");
                Console.ReadKey();
            }
        }
    }

}
        
    

    //        -- DADOS: Autor
    //INSERT INTO autor(cod_aut, nome_aut) VALUES
    //(1, 'Rick Riordan'),
    //(2, 'Eduardo Spohr'),
    //(3, 'Stephen King'),
    //(4, 'Pierre Boulle'),
    //(5, 'Richard Matheson'),
    //(6, 'George Orwell'),
    //(7, 'Franz Kafka'),
    //(8, 'Walter Isacson'),
    //(9, 'Ernest Cline'),
    //(10, 'Baruch Espinoza');

    //-- DADOS: Editora
    //INSERT INTO editora(cod_edit, nome_edit) VALUES
    //(1, 'Intrinseca'),
    //(2, 'Leya'),
    //(3, 'Ponto de Leitura'),
    //(4, 'Aleph'),
    //(5, 'Secker'),
    //(6, 'Suma'),
    //(7, 'Companhia das Letras'),
    //(8, 'Lebooks Editora');

    //-- DADOS: Categoria
    //INSERT INTO categoria(cod_cat, nome_cat) VALUES
    //(1, 'Infanto-Juvenil'),
    //(2, 'Romance'),
    //(3, 'Terror'),
    //(4, 'Ficção científica'),
    //(5, 'Drama'),
    //(6, 'Suspense'),
    //(7, 'Novela'),
    //(8, 'Biografia'),
    //(9, 'Aventura'),
    //(10, 'Filosofia');


    //-- DADOS: Cliente
    //INSERT INTO cliente(cod_cli, cod_cid, nome_cli, endereco_cli, sexo_cli) VALUES
    //(1, 1, 'José Nogueira', 'Rua A',  'M'),
    //(2, 1, 'Angelo Pereira', 'Rua B',  'M'),
    //(3, 1, 'Além Mar Paranhos', 'Rua C',  'F'),
    //(4, 1, 'Catarina Souza', 'Rua D',  'F'),
    //(5, 1, 'Vagner Costa', 'Rua E', 'M'),
    //(6, 2, 'Antenor da Costa', 'Rua F',  'M'),
    //(7, 2, 'Maria Amélia de Sousa', 'Rua G', 'F'),
    //(8, 2, 'Paulo Roberto Silva', 'Rua H', 'M'),
    //(9, 3, 'Fátima Souza', 'Rua I',  'F'),
    //(10, 3, 'Joel da Rocha', 'Rua J',  'M');

