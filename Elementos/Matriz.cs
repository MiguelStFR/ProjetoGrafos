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

        public static Matriz[,] CriarMatriz(List<Vertice> verticeList)
        {
            int i = 0, j = 0;

            Matriz[,] matriz = new Matriz[verticeList.Count, verticeList.Count];

            foreach (Vertice vLin in verticeList)
            {
                foreach (Vertice vCol in verticeList)
                {
                    matriz[i, j] = new Matriz(vLin.Tag + ":" + vCol.Tag, existeAdjacencia(vLin, vCol));
                    j++;
                }
                i++;
                j = 0;
            }
            return matriz;
        }
    
        private static int existeAdjacencia(Vertice vLin, Vertice vCol)
        {
            return (vLin.Prox_Vertices.Contains(vCol))? 1 : 0;
        }

        public static void mostrarMatriz(Matriz [,]matriz, int tam)
        {
            Console.WriteLine("\nMATRIZ:");
            for(int i = 0; i < tam; i++)
            {
                string[] vertice = matriz[i, 0].Combinacao.Split(':');
                Console.Write("\t" + vertice[0].Trim());
            }
            Console.Write("\n");
                
            for (int i = 0; i < tam; i++)
            {
                string[] vertice = matriz[0, i].Combinacao.Split(':');
                Console.Write(vertice[1].Trim() + "\t");

                for (int j = 0; j < tam; j++)
                {
                    Console.Write(matriz[i, j].numRelacoes + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
