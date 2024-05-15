using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{

    internal class Matriz
    {
        public int _numRelacoes = 0;
        public string _combinacao = string.Empty;

        public int numRelacoes { get { return _numRelacoes; } set { _numRelacoes = value; } }
        public string Combinacao { get { return _combinacao; } set { _combinacao = value; } }

        public Matriz(string combinacao, int numRelacoes) 
        { 
            _combinacao = combinacao;
            _numRelacoes += numRelacoes;
        }

        public static Matriz[,] CriarMatriz(List<Aresta> arestaList, List<Vertice> verticeList)
        {
            Matriz[,] matriz = new Matriz[verticeList.Count, verticeList.Count];

            foreach (Vertice vertice in verticeList)
            {
                int indexLin = verticeList.IndexOf(vertice);

                if (arestaList[0].TipoAresta == TipoGrafo.DI)
                {
                    List<Aresta> arestasVertice = arestaList.FindAll(a => a.VerticePredecessor.Equals(vertice));

                    foreach (Vertice verticeAux in verticeList)
                    {
                        int indexCol = verticeList.IndexOf(verticeAux);

                        int num_relacoes = arestasVertice.FindAll(a => a.VerticeSucessor.Equals(verticeAux)).Count;

                        matriz[indexCol, indexLin] = new Matriz(vertice.Tag + ":" + verticeAux.Tag, num_relacoes);
                    }
                }
                else
                {
                    List<Aresta> arestasVertice = arestaList.FindAll(a => a.VerticePredecessor.Equals(vertice));

                    for (int indexCol = indexLin; indexCol < verticeList.Count; indexCol++)
                    {
                        Vertice verticeAux = verticeList[indexCol];
                        int num_relacoes = arestaList.FindAll(
                            a => a.VerticeSucessor.Equals(verticeAux) && a.VerticePredecessor.Equals(vertice) || 
                            a.VerticeSucessor.Equals(vertice) && a.VerticePredecessor.Equals(verticeAux)).Count;

                        matriz[indexCol, indexLin] = new Matriz(vertice.Tag + ":" + verticeAux.Tag, num_relacoes);
                        
                        if(verticeAux != vertice)
                            matriz[indexLin, indexCol] = new Matriz(verticeAux.Tag + ":" + vertice.Tag, num_relacoes);
                    }
                }
            }
            return matriz;
        }

        public static void mostrarMatriz(Matriz [,]matriz, int tam)
        {
            Console.WriteLine("\nMATRIZ:\n");
            for(int i = 0; i < tam; i++)
            {
                string[] vertice = matriz[0, i].Combinacao.Split(':');
                Console.Write("\t" + vertice[0].Trim());
            }
            Console.Write("\n");
                
            for (int i = 0; i < tam; i++)
            {
                string[] vertice = matriz[i, 0].Combinacao.Split(':');
                Console.Write(vertice[1].Trim() + "\t");

                for (int j = 0; j < tam; j++)
                {
                    Console.Write(matriz[j, i].numRelacoes + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
