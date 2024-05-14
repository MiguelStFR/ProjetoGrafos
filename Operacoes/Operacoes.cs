using ProjetoGrafos.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Operacoes
{
    internal class Operacoes
    {

        public static void AcessarGrafo()
        {
            Console.Clear();

            if(Program._Grafos.Count == 0 ) 
            {
                Console.WriteLine("Nenhum grafo foi criado.\n");
                return;
            }

            Console.WriteLine("Grafos criados:\n");
            foreach (Grafo g in Program._Grafos)
            {
                Console.WriteLine("Grafo: " +  g.Nome);
            }

            Console.Write("\nEscolha um dos grafos acima:\n-> ");
            String escolha = Console.ReadLine();

            Grafo grafo = Program._Grafos.Find(g => g.Nome == escolha.Trim());

            if (grafo != null)
            {
                int pos = Program._Grafos.IndexOf(grafo);
                Menu.GrafoMenu(pos);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhum Grafo válido selecionado.\n");
            }
        }

        public static void TestarGrafoCompleto(int pos)
        {
            Console.Clear();
            Console.Write("\nTestar se o grafo é completo: ");
            if (Program._Grafos[pos].IsCompleto)
                Console.WriteLine("O Grafo é completo\n");
            else
                Console.WriteLine("O grafo não é completo\n");
        }

        public static void TestarGrafoSimples(int pos)
        {
            Console.Clear();
            Console.Write("\nTestar se o grafo é simples: ");

            if (Program._Grafos[pos].IsSimples)
                Console.WriteLine("O Grafo é simples.\n");
            else
                Console.WriteLine("O grafo não é simples.\n");
        }

        public static void TestarGrafoBipartido(int pos)
        {
            Console.Clear();
            Console.Write("\nTestar se o grafo é completo: ");
            if (Program._Grafos[pos].IsBipartido)
                Console.WriteLine("O Grafo é bipartido.\n");
            else
                Console.WriteLine("O grafo não é bipartido.\n");
        }

        public static void TestarGrafoRegular(int pos)
        {
            Console.Clear();
            Console.Write("\nTestar se o grafo é simples: ");

            if (Program._Grafos[pos].IsRegular)
                Console.WriteLine("O Grafo é regular.\n");
            else
                Console.WriteLine("O grafo não é regular.\n");
        }

        public static void TestarGrafoConexo(int pos)
        {
            Console.Clear();
            Console.Write("\nTestar se o grafo é simples: ");

            if (Ordenacoes.isConexo(Program._Grafos[pos].VerticeList))
                Console.WriteLine("O Grafo é conexo.\n");
            else
                Console.WriteLine("O grafo não é conexo.\n");
        }

        public static void IdentificarGrauVertice(int pos)
        {
            Console.Clear();
            Console.Write("Digite o vértice de interesse(ex: 'V1')\n->");
            string tag_vertice = Console.ReadLine().Trim();

            if (!Program._Grafos[pos].BuscarVertice(tag_vertice))
                Console.WriteLine("Nenhum vértice com esse nome foi localizado no grafo em questão.\n");
            else
            {
                Console.WriteLine("Grau do vértice " + tag_vertice + ": " + Program._Grafos[pos].VerticeAux(tag_vertice).Grau + "\n"); 
            }
        }

        public static void IdentificarVizinhancaVertice(int pos)
        {
            Console.Clear();
            Console.Write("Digite o vértice de interesse(ex: 'V1')\n->");
            string tag_vertice = Console.ReadLine().Trim();

            if (!Program._Grafos[pos].BuscarVertice(tag_vertice))
                Console.WriteLine("Nenhum vértice com esse nome foi localizado no grafo em questão.\n");
            else
            {
                Console.Write("Vizinhança do vértice ");
                Program._Grafos[pos].ExibirVizinhanca(Program._Grafos[pos].VerticeAux(tag_vertice));
                Console.Write("\n");
            }
        }

        public static void IdentificarSucessoresPredecessores(int pos)
        {
            Console.Clear();
            Console.Write("Digite o vértice de interesse(ex: 'V1')\n->");
            string tag_vertice = Console.ReadLine().Trim();

            if (!Program._Grafos[pos].BuscarVertice(tag_vertice))
                Console.WriteLine("Nenhum vértice com esse nome foi localizado no grafo em questão.\n");
            else
            {
                Console.Write("Sucessores ");
                Program._Grafos[pos].ExibirFilhosVertice(Program._Grafos[pos].VerticeAux(tag_vertice));

                Console.Write("Antecessores ");
                Program._Grafos[pos].ExibirPaisVertice(Program._Grafos[pos].VerticeAux(tag_vertice));
                Console.Write("\n");
            }
        }

        public static void OrdenacaoPrim(int pos)
        {
            Console.Clear();
            Console.Write("Digite o vértice de origem(ex: 'V1')\n->");
            string tag_vertice = Console.ReadLine().Trim();

            if (!Program._Grafos[pos].BuscarVertice(tag_vertice))
                Console.WriteLine("Nenhum vértice com esse nome foi localizado no grafo em questão.\n");
            else
            {
                Ordenacoes.OrdenacaoPrimND(Program._Grafos[pos].VerticeList.Find(v => v.Tag == tag_vertice), Program._Grafos[pos].VerticeList, Program._Grafos[pos].ArestaList);
            }
        }

        public static void OrdenacaoKruskal(int pos)
        {
            Console.Clear();
            Console.Write("Digite o vértice de origem(ex: 'V1')\n->");
            string tag_vertice = Console.ReadLine().Trim();

            if (!Program._Grafos[pos].BuscarVertice(tag_vertice))
                Console.WriteLine("Nenhum vértice com esse nome foi localizado no grafo em questão.\n");
            else
            {
                Ordenacoes.OrdenacaoKruskalND(Program._Grafos[pos].VerticeList.Find(v => v.Tag == tag_vertice), Program._Grafos[pos].VerticeList, Program._Grafos[pos].ArestaList);
            }
        }
    }
}
