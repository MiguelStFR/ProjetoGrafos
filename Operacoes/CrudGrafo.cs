using ProjetoGrafos.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Operacoes
{
    internal class CrudGrafo
    {
        public static void CriarGrafo()
        {
            Console.Clear();

            Console.Write("Digite o Nome e Tipo do grafo(ex:'Nome:DI/ND')\n" + "->");
            string[] retorno = Console.ReadLine().Split(':');

            TipoGrafo tipoGrafo;

            if (retorno[1] == "ND") 
                tipoGrafo = TipoGrafo.ND;
            else 
                tipoGrafo = TipoGrafo.DI;

            Grafo grafo = new Grafo(retorno[0].Trim(), tipoGrafo);
            Program._Grafos.Add(grafo);

            int pos = Program._Grafos.IndexOf(grafo);

            Program._Grafos[pos].CriarVertices();
            Program._Grafos[pos].CriarRelacoes();

            Console.WriteLine("\nDetalhes do Grafo Criado:\n");
            Program._Grafos[pos].ExibirGrafo();
        }

        public static void VisualizarGrafo()
        {
            Console.Clear();

            Console.Write("Escolha um Grafo para exibir(ex: 'Nome do Grafo'):\n->");
            string nomeGrafo = Console.ReadLine();

            Grafo grafo = Program._Grafos.Find(g => g.Nome == nomeGrafo.Trim());

            if (grafo != null) 
            {
                int pos = Program._Grafos.IndexOf(grafo);
                Program._Grafos[pos].ExibirGrafo();
            }

        }
    }
}
