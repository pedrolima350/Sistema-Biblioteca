using System;
using MySql.Data.MySqlClient;

public class Cliente
{
    public static void ConsultarCliente(MySqlConnection conexao)
    {
        Console.Write("\nDigite o nome do cliente: ");
        string nome = Console.ReadLine();

        // Verifica se cliente existe
        string checkSql = "SELECT cod_cli FROM cliente WHERE nome_cli = @nome";
        MySqlCommand checkCmd = new MySqlCommand(checkSql, conexao);
        checkCmd.Parameters.AddWithValue("@nome", nome);
        object resultado = checkCmd.ExecuteScalar();

        if (resultado == null)
        {
            Console.WriteLine("\nCliente não cadastrado.");
            return;
        }

        // Histórico do cliente
        string sql = @"
            SELECT 
                t.nome_livro AS Livro,
                p.data_ped AS DataPedido
            FROM cliente c
            JOIN pedido p ON c.cod_cli = p.cod_cli
            JOIN titulo_pedido tp ON p.cod_ped = tp.cod_ped
            JOIN titulo t ON tp.cod_tit = t.cod_tit
            WHERE c.nome_cli = @nome
            ORDER BY p.data_ped DESC;
        ";

        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@nome", nome);

        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\n=== Histórico de Empréstimos ===");
            while (reader.Read())
            {
                Console.WriteLine($"Livro: {reader["Livro"]} | Data: {Convert.ToDateTime(reader["DataPedido"]).ToString("dd/MM/yyyy")}");
            }
        }
    }
}

