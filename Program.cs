
using System;
using MySql.Data.MySqlClient;
using MySql.Data;

class Estoque
{
     static MySqlConnection conexao;

    static void Main()
    {
        string caminhoBanco = "server=127.0.0.1;uid=root;pwd=root;database=biblioteca";
        conexao = new MySqlConnection(caminhoBanco);

        try
        {
            conexao.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("1 - Adicionar Livros");
            Console.WriteLine("2 - Alugar");
            Console.WriteLine("3 - Consultar Estoque");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    EntradaLivro();
                    break;
                case 2:
                    AlugarLivro();
                    break;
                case 3:
                    ConsultarEstoque();
                    break;
                case 4:
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        conexao.Close();
    }

    static void EntradaLivro()
    {
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

        string sql = "INSERT INTO livro (titulo, categoria, autor, editora, quantidade, preco) VALUES (@titulo, @categoria, @autor, @editora, @quantidade, @preco)";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@titulo", titulo);
        cmd.Parameters.AddWithValue("@categoria", categoria);
        cmd.Parameters.AddWithValue("@autor", autor);
        cmd.Parameters.AddWithValue("@editora", editora);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.Parameters.AddWithValue("@preco", preco);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Produto adicionado ao estoque.");
    }

    static void AlugarLivro()
    {
        Console.Write("Nome do Livro: ");
        string titulo = Console.ReadLine();
        Console.Write("Quantidade a ser retirada: ");
        int quantidade = int.Parse(Console.ReadLine());

        string sql = "SELECT quantidade, preco FROM produtos WHERE nome=@nome";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@titulo", titulo);
        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            int quantidadeAtual = reader.GetInt32(0);
            double preco = reader.GetDouble(1);
            if (quantidadeAtual >= quantidade)
            {
                sql = "UPDATE produtos SET quantidade = quantidade - @quantidade WHERE nome = @nome";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);
                cmd.Parameters.AddWithValue("@titulo", titulo);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Venda realizada! Valor total: R$ {quantidade * preco:F2}");
            }
            else
            {
                Console.WriteLine("Quantidade insuficiente no estoque.");
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    static void ConsultarEstoque()
    {
        string sql = "SELECT * FROM produtos";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("Estoque:");
        Console.WriteLine("ID | Nome | Quantidade | Preço");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetInt32(2)} | R$ {reader.GetDouble(3):F2}");
        }
    }
}