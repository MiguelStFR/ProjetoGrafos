using ProjetoGrafos.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Operacoes
{
    internal class Buscas
    {
        private static int tempo = 0;
        private static List<Vertice> Fila;

        public static void IniciarBuscaEmLarguraLista(List<Vertice> verticeList)
        {
            tempo = 0;
            Fila = new List<Vertice>();

            Console.WriteLine("INICIANDO BUSCA EM LARGURA:\n");
            for (int i = 0; i < verticeList.Count; i++)
            {
                verticeList[i].IndiceBusca = 0;
                verticeList[i].NivelBusca = 0;
                verticeList[i].PredecessorBusca = null;
            }

            while (verticeList.Find(v => v.IndiceBusca == 0) != null)
            {
                Vertice vertice = verticeList.Find(v => v.IndiceBusca == 0);
                tempo++;
                vertice.NivelBusca = tempo;
                Fila.Add(vertice);
                BuscarEmLarguraLista(Fila);
            }
        }

        private static void BuscarEmLarguraLista(List<Vertice> Fila)
        {
            while(Fila.Count > 0)
            {
                Vertice vertice = Fila.First();
                if (vertice != null)
                {
                    foreach(Vertice vizinho in vertice.VerticesVizinhos)
                    {
                        //visita aresta de árvore
                        if(vizinho.IndiceBusca == 0)
                        {
                            Console.WriteLine("Visita aresta de pai {0} -> {1}", vertice, vizinho);
                            vizinho.PredecessorBusca = vertice;
                            vizinho.NivelBusca = vertice.NivelBusca ++;
                            tempo++;
                            vizinho.IndiceBusca = tempo;
                            Fila.Add(vizinho);
                        }
                        //visita aresta tio
                        else if(vizinho.NivelBusca == vertice.NivelBusca + 1)
                        {
                            Console.WriteLine("Visita de aresta tio {0} -> {1}", vertice, vizinho);
                        }
                        else if(vizinho.NivelBusca == vertice.NivelBusca && vizinho.PredecessorBusca == vertice.PredecessorBusca && vizinho.IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("Visita de aresta irmão {0} -> {1}", vertice, vizinho);
                        }
                        else if (vizinho.NivelBusca == vertice.NivelBusca && vizinho.PredecessorBusca != vertice.PredecessorBusca && vizinho.IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("Visita de aresta primo {0} -> {1}", vertice, vizinho);
                        }
                    }
                }
            }
        }

        public static void IniciarBuscaEmProfundidade(List<Vertice> verticeList)
        {
            Console.WriteLine("INICIANDO BUSCA EM PROFUNDIDADE");
            tempo = 0;

            for (int i = 0; i < verticeList.Count; i++)
            {
                verticeList[i].TempoDescoberta = 0;
                verticeList[i].TempoTermino = 0;
                verticeList[i].PredecessorBusca = null;
            }
        }
    }
}
