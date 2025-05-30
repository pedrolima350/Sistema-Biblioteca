
using System;
using MySql.Data.MySqlClient;


class Estoque
{
    static MySqlConnection conexao;

    static void Main()
    {
        string caminhoBanco = "estoque.db";
        conexao = new MySqlConnection($"Data Source={caminhoBanco};Version=3;");
        conexao.Open();
        CriarTabela();

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("1 - Entrada de Produto");
            Console.WriteLine("2 - Saída de Produto");
            Console.WriteLine("3 - Consultar Estoque");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    EntradaProduto();
                    break;
                case 2:
                    SaidaProduto();
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

    static void CriarTabela()
    {
        string sql = @"CREATE TABLE IF NOT EXISTS produtos (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT NOT NULL,
                        quantidade INTEGER NOT NULL,
                        preco REAL NOT NULL)";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.ExecuteNonQuery();
    }

    static void EntradaProduto() // entrada do produto no estoque
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine();
        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        Console.Write("Preço: ");
        double preco = double.Parse(Console.ReadLine());

        string sql = "INSERT INTO produtos (nome, quantidade, preco) VALUES (@nome, @quantidade, @preco)";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.Parameters.AddWithValue("@preco", preco);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Produto adicionado ao estoque.");
    }

    static void SaidaProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine();
        Console.Write("Quantidade a ser retirada: ");
        int quantidade = int.Parse(Console.ReadLine());

        string sql = "SELECT quantidade, preco FROM produtos WHERE nome=@nome";
        MySqlCommand cmd = new MySqlCommand(sql, conexao);
        cmd.Parameters.AddWithValue("@nome", nome);
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
                cmd.Parameters.AddWithValue("@nome", nome);
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
        MySql
        DataReader reader = cmd.ExecuteReader();

        Console.WriteLine("Estoque:");
        Console.WriteLine("ID | Nome | Quantidade | Preço");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetInt32(2)} | R$ {reader.GetDouble(3):F2}");
        }
    }
}