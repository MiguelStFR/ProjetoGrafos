using ProjetoGrafos.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public static void OrdenacaoPrimND(Vertice raiz, List<Vertice> verticeList, List<Aresta> arestaList)
        {
            List<Vertice> verticesSelecionadosList = new List<Vertice>();
            List<Aresta> arestasEscolhidasList = new List<Aresta>();

            verticesSelecionadosList.Add(raiz);

            for(int i = 0; verticesSelecionadosList.Count != verticeList.Count; i++)
            {
                List<Aresta> arestaAuxList = new List<Aresta>();
                arestaAuxList = arestaList.FindAll(a => a.VerticeSucessor == verticesSelecionadosList[i] && verticesSelecionadosList.Find(v => v.Tag == a.VerticePredecessor.Tag) == null);

                if(arestaAuxList.Count > 0)
                {
                    Aresta ArestaMenorPeso = arestaAuxList[0];
                    foreach(Aresta a in arestaAuxList)
                    {
                        if(ArestaMenorPeso.Peso > a.Peso)
                            ArestaMenorPeso = a;
                    }

                    verticesSelecionadosList.Add(ArestaMenorPeso.VerticeSucessor);
                    arestasEscolhidasList.Add(ArestaMenorPeso);
                }             
            }

            Console.WriteLine("Arestas selecionadas: ");
            foreach (Aresta aresta in arestasEscolhidasList)
                Console.Write(aresta.VerticePredecessor + ":" + aresta.VerticeSucessor + "\n");
        }

        public static void OrdenacaoKruskalND(Vertice raiz, List<Vertice> verticeList, List<Aresta> arestaList)
        {
            List<Vertice> verticesSelecionadosList = new List<Vertice>();
            List<Aresta> arestasEscolhidasList = new List<Aresta>();
            
            verticesSelecionadosList.Add(raiz);
            arestaList = OrdenacaoBubbleSort(arestaList);

            arestasEscolhidasList.Add(arestaList[0]);
            int j = 1;
            while (arestasEscolhidasList.Count < verticesSelecionadosList.Count - 1)
            {
                if (!hasCiclo(arestaList[j], arestasEscolhidasList))
                    arestasEscolhidasList.Add(arestaList[j]);
                j++;
            }

            Console.WriteLine("Arestas selecionadas: ");
            foreach (Aresta aresta in arestasEscolhidasList)
                Console.Write(aresta.VerticePredecessor + ":" + aresta.VerticeSucessor + "\n");
        }

        public static List<Aresta> OrdenacaoBubbleSort(List<Aresta> arestaList)
        {
            int tamanho = arestaList.Count;
            int comparacoes = 0;
            int trocas = 0;

            for (int i = tamanho - 1; i >= 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    comparacoes++;
                    if (arestaList[j].Peso > arestaList[j + 1].Peso)
                    {
                        Aresta aux = arestaList[j];
                        arestaList[j] = arestaList[j + 1];
                        arestaList[j + 1] = aux;
                        trocas++;
                    }
                }
            }
            return arestaList;
        }

        public static bool isConexo(List<Vertice> verticeList)
        {
            foreach(Vertice vertice in verticeList)
                if(vertice.VerticesVizinhos == null)
                    return false;
            return true;
        }

        public static bool hasCiclo(Aresta aresta, List<Aresta> arestaList)
        {
            return false;
        }
    }
}
