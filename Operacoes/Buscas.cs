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
        private static Queue<Vertice> Fila;
        private static List<Vertice> VerticeList;

        public static void IniciarBuscaEmLarguraLista(List<Vertice> verticeList)
        {
            tempo = 0;
            Fila = new Queue<Vertice>();

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
                Fila.Enqueue(vertice);
                BuscarEmLarguraLista(Fila);
            }
        }

        private static void BuscarEmLarguraLista(Queue<Vertice> Fila)
        {
            while(Fila.Count > 0)
            {
                Vertice vertice = Fila.Dequeue();
                if (vertice != null)
                {
                    foreach(Vertice vizinho in vertice.VerticesVizinhos)
                    {
                        //visita aresta de árvore
                        if(vizinho.IndiceBusca == 0)
                        {
                            Console.WriteLine("Visita aresta de pai {0} -> {1}", vertice.Tag, vizinho.Tag);
                            vizinho.PredecessorBusca = vertice;
                            vizinho.NivelBusca = vertice.NivelBusca ++;
                            tempo++;
                            vizinho.IndiceBusca = tempo;
                            Fila.Enqueue(vizinho);
                        }
                        //visita aresta tio
                        else if(vizinho.NivelBusca == vertice.NivelBusca + 1)
                        {
                            Console.WriteLine("Visita de aresta tio {0} -> {1}", vertice.Tag, vizinho.Tag);
                        }
                        else if(vizinho.NivelBusca == vertice.NivelBusca && vizinho.PredecessorBusca == vertice.PredecessorBusca && vizinho.IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("Visita de aresta irmão {0} -> {1}", vertice.Tag, vizinho.Tag);
                        }
                        else if (vizinho.NivelBusca == vertice.NivelBusca && vizinho.PredecessorBusca != vertice.PredecessorBusca && vizinho.IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("Visita de aresta primo {0} -> {1}", vertice.Tag, vizinho.Tag);
                        }
                    }
                }
            }
        }

        public static void IniciarBuscaEmProfundidade(List<Vertice> verticeList, TipoGrafo tipoGrafo)
        {
            Console.WriteLine("INICIANDO BUSCA EM PROFUNDIDADE");
            tempo = 0;
            VerticeList.Clear();

            for (int i = 0; i < verticeList.Count; i++)
            {
                verticeList[i].TempoDescoberta = 0;
                verticeList[i].TempoTermino = 0;
                verticeList[i].PredecessorBusca = null;
                VerticeList.Add(verticeList[i]);
            }

            while (VerticeList.Find(v => v.TempoDescoberta == 0) != null)
            {
                Vertice vertice = VerticeList.Find(v => v.TempoDescoberta == 0);
                if (vertice != null)
                {
                    if(tipoGrafo == TipoGrafo.ND)
                        BuscarEmProfundidadeND(VerticeList.IndexOf(vertice));
                    else
                        BuscarEmProfundidadeDI(VerticeList.IndexOf(vertice));
                }
            }
        }

        private static void BuscarEmProfundidadeND(int pos)
        {
            tempo++;
            VerticeList[pos].TempoDescoberta = tempo;

            foreach(Vertice vizinho in VerticeList[pos].VerticesVizinhos)
            {
                if (vizinho.TempoDescoberta == 0)
                {
                    Console.WriteLine("Aresta de Arvore {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                    VerticeList[VerticeList.IndexOf(vizinho)].PredecessorBusca = VerticeList[pos];
                    BuscarEmProfundidadeND(VerticeList.IndexOf(vizinho));
                }
                else if(vizinho.TempoTermino == 0 && vizinho != VerticeList[pos])
                {
                    Console.WriteLine("Aresta de Retorno {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                }
            }
            tempo++;
            VerticeList[pos].TempoTermino = tempo;
        }

        private static void BuscarEmProfundidadeDI(int pos)
        {
            tempo++;
            VerticeList[pos].TempoDescoberta = tempo;

            foreach (Vertice vizinho in VerticeList[pos].VerticesFilho)
            {
                if (vizinho.TempoDescoberta == 0)
                {
                    Console.WriteLine("Aresta de Arvore {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                    VerticeList[VerticeList.IndexOf(vizinho)].PredecessorBusca = VerticeList[pos];
                    BuscarEmProfundidadeND(VerticeList.IndexOf(vizinho));
                }
                else
                {
                    if (vizinho.TempoTermino == 0)
                        Console.WriteLine("Aresta de Retorno {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                    else if(VerticeList[pos].TempoDescoberta < vizinho.TempoDescoberta)
                        Console.WriteLine("Aresta de Avanço {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                    else
                        Console.WriteLine("Aresta de Cruzamento {0}:{1}", vizinho.Tag, VerticeList[pos].Tag);
                }
            }
            tempo++;
            VerticeList[pos].TempoTermino = tempo;
        }
    }
}
