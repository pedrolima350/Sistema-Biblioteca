using System;
using MySql.Data.MySqlClient;

public class Livro
{
    public static void ConsultarLivro(MySqlConnection conexao)
    {
        Console.Write("\nDigite o título do livro: ");
        string titulo = Console.ReadLine();

        string sql = @"
            SELECT 
                t.nome_livro AS Titulo,
                GROUP_CONCAT(a.nome_aut SEPARATOR ', ') AS Autores,
                e.nome_edit AS Editora,
                t.qtd_estq AS Estoque
            FROM titulo t
            JOIN titulo_autor ta ON t.cod_tit = ta.cod_tit
            JOIN autor a ON ta.cod_aut = a.cod_aut
            JOIN editora e ON t.cod_edit = e.cod_edit
            WHERE t.nome_livro = @titulo
            GROUP BY t.nome_livro, e.nome_edit, t.qtd_estq;
        ";

        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@titulo", titulo);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Console.WriteLine("\n=== Livro Encontrado ===");
                Console.WriteLine($"Título: {reader["Titulo"]}");
                Console.WriteLine($"Autor(es): {reader["Autores"]}");
                Console.WriteLine($"Editora: {reader["Editora"]}");
                Console.WriteLine($"Estoque: {reader["Estoque"]}");
            }
            else
            {
                Console.WriteLine("\nLivro inexistente no banco de dados.");
            }
        }
    }
}
