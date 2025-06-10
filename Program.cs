
using System;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace Biblioteca
{
    class Biblioteca
    {
        static void Main()
        {
            string caminhoBanco = "server=127.0.0.1;uid=root;pwd=;database=Biblioteca";
            var conexao = new Conexao();
            conexao.ConectarBanco(caminhoBanco);
            Estoque estoque = new Estoque();
            int opcao;
            do
            {
                Console.WriteLine("1 - Adicionar Livros");
                Console.WriteLine("2 - Comprar");
                Console.WriteLine("3 - Consultar Estoque");
                Console.WriteLine("4 - Alugar");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        estoque.EntradaLivro(conexao);
                        break;
                    case 2:
                        estoque.VenderLivro(conexao);
                        break;
                    case 3:
                        estoque.ConsultarEstoque(conexao);
                        break;
                    case 4:
                        estoque.AlugarLivro(conexao);
                        break;
                    case 5:
                        Console.WriteLine("Finalizando...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (opcao != 5);


        }

    }


}
