using ProjetoGrafos.Elementos;
using ProjetoGrafos.Operacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//   Funções básicas para manipulação de grafos em ambas representações, incluindo:
//1) Criação de um grafo com X vértices(o número de vértices deve ser inserido pelo usuário),
//2) Definição do tipo de grafo(direcionado ou não direcionado),
//3) Criação e remoção de arestas,
//4) Identificação da vizinhança de um vértice(grafo não direcionado),
//5) Identificação dos sucessores e predecessores de um vértice(grafo direcionado) ,
//6) Identificação do grau de um determinado vértice. (Observar direcionamento ou não do grafo),
//7) Testar se o grafo é simples,
//8) Testar se o grafo é regular,
//9) Testar se o grafo é completo,
//10) Testar se o grafo é bipartido.
namespace ProjetoGrafos.Operacoes
{
    internal class Menu
    {
        public static void ExbirMenu() 
        {
            Console.Clear();
            string escolha;

            do
            {
                Console.Write(
                "Digite o que deseja realizar:\n" +
                "1 - Criar Grafo.\n" +
                "2 - Acessar Grafo.\n" +
                "3 - Sair.\n" +
                "-> ");

                escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        Console.WriteLine("\nCriar Grafo:\n");
                        CrudGrafo.CriarGrafo();
                        continue;
                    case "2":
                        Console.WriteLine("\nAcessar Grafo:\n");
                        Operacoes.AcessarGrafo();
                        continue;
                    case "3":
                        Console.WriteLine("\nSaindo.\n");
                        break;
                    default:
                        Console.WriteLine("Nenhuma opção válida escolhida");
                        continue;
                }
            } while (escolha != "3");
        }


        //3) Criação e remoção de arestas,
        //4) Identificação da vizinhança de um vértice(grafo não direcionado),
        //5) Identificação dos sucessores e predecessores de um vértice(grafo direcionado) ,
        //6) Identificação do grau de um determinado vértice. (Observar direcionamento ou não do grafo),
        //7) Testar se o grafo é simples,
        //8) Testar se o grafo é regular,
        //9) Testar se o grafo é completo,
        //10) Testar se o grafo é bipartido.   
        public static void GrafoMenu(int pos) 
        {
            string escolha;
            Console.Clear();
            do
            {
                Console.Write(
                "\nDigite o que deseja realizar:\n" +
                "1 - Identificar a vizinhança de um vértice.\n" +
                "2 - Identificar sucessores e predecessores de um vértice.\n" +
                "3 - Identificar do grau de um vértice.\n" +
                "4 - Testar se o grafo é simples.\n" +
                "5 - Testar se o grafo é regular.\n" +
                "6 - Testar se o grafo é completo.\n" +
                "7 - Testar se o grafo é bipartido.\n" +
                "8 - Criar arestas\n" +
                "9 - Remover arestas\n" +
                "10 - Detalhes do Grafo\n" +
                "11 - Deletar o Grafo\n" +
                "12 - Voltar.\n" +
                "13 - Sair.\n"+
                "-> ");

                escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        Operacoes.IdentificarVizinhancaVertice(pos);
                        continue;
                    case "2":
                        Operacoes.IdentificarSucessoresPredecessores(pos);
                        continue;
                    case "3":
                        Operacoes.IdentificarGrauVertice(pos);
                        continue;
                    case "4":
                        Operacoes.TestarGrafoSimples(pos);
                        continue;
                    case "5":
                        Operacoes.TestarGrafoRegular(pos);
                        continue;
                    case "6":
                        Operacoes.TestarGrafoCompleto(pos);
                        continue;
                    case "7":
                        Operacoes.TestarGrafoBipartido(pos);
                        continue;
                    case "8":
                        Program._Grafos[pos].AdicionarAresta();
                        continue;
                    case "9":
                        Program._Grafos[pos].RemoverAresta();
                        continue;
                    case "10":
                        Console.Clear();
                        Program._Grafos[pos].ExibirGrafo();
                        continue;
                    case "11":
                        Program._Grafos[pos].Deletar();                   
                        return;
                    case "12":
                        Console.WriteLine("\nVoltar.\n");
                        return;
                    case "13":
                        Console.WriteLine("\nSaindo.\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nenhuma opção válida escolhida");
                        continue;
                }
            } while (true);
        }
    }
}
