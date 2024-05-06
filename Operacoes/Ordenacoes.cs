using ProjetoGrafos.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Operacoes
{
    internal class Ordenacoes
    {
        private static Queue<Vertice> Fila;
        private static Queue<Vertice> Ordena_Top;

        public static void OrdenacaoTopologicaKhan(List<Vertice>verticeList)
        {
            Console.WriteLine("ORDENAÇÃO TOPOLÓGICA KHAN");
            Fila = new Queue<Vertice>();
            Ordena_Top = new Queue<Vertice>();

            for (int i = 0; i < verticeList.Count; i++)
            {
                verticeList[i].GrauOrdenacao = verticeList[i].GrauEntrada; 
            }
            
            foreach(Vertice vertice in verticeList.FindAll(v => v.GrauEntrada == 0))
            {
                Fila.Enqueue(vertice);
            }

            while(Fila.Count > 0)
            {
                Vertice verticePredescessor = Fila.Dequeue();
                Ordena_Top.Enqueue(verticePredescessor);
                foreach(Vertice verticeSuscessor in verticePredescessor.VerticesFilho)
                {
                    int pos = verticeList.IndexOf(verticeSuscessor);
                    verticeList[pos].GrauOrdenacao--;
                    if (verticeList[pos].GrauOrdenacao == 0)
                        Fila.Enqueue(verticeList[pos]);
                }
            }

            Console.Write("RESULTADO: ");
            foreach(Vertice vertice in Ordena_Top)
            {
                Console.Write(vertice.Tag + " : ");
            }

            if(verticeList.Find(v => v.GrauOrdenacao != 0) != null)
            {
                Console.WriteLine("Existe um Ciclo no grafo");
            }
        }
    }
}
