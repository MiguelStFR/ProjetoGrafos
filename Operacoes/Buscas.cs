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
        private static List<Vertice> VerticeList = new List<Vertice>();

        public static void IniciarBuscaEmLarguraLista(List<Vertice> verticeList, out bool isConexo)
        {
            tempo = 0;
            Fila = new Queue<Vertice>();
            isConexo = true;
            int contador = 0;

            VerticeList = new List<Vertice>();
            VerticeList = verticeList;

            Console.Clear();
            Console.WriteLine("INICIANDO BUSCA EM LARGURA:\n");
            for (int i = 0; i < verticeList.Count; i++)
            {
                VerticeList[i].IndiceBusca = 0;
                VerticeList[i].NivelBusca = 0;
                VerticeList[i].PredecessorBusca = null;
            }

            while (VerticeList.Find(v => v.IndiceBusca == 0) != null)
            {
                Vertice vertice = VerticeList.Find(v => v.IndiceBusca == 0);
                tempo++;
                vertice.IndiceBusca = tempo;
                Fila.Enqueue(vertice);
                BuscarEmLarguraLista(Fila);
                contador++;
            }

            if(contador > 1)
                isConexo = false;
        }

        private static void BuscarEmLarguraLista(Queue<Vertice> Fila)
        {
            while(Fila.Count > 0)
            {
                Vertice vertice = Fila.Dequeue();
                if (vertice != null)
                {
                    int i = 0;
                    foreach(Vertice vizinho in vertice.VerticesVizinhos)
                    {
                        int pos = VerticeList.IndexOf(vizinho);
                        //visita aresta de árvore
                        if (vizinho.IndiceBusca == 0)
                        {
                            Console.WriteLine("{0}:{1} - Visita aresta de pai", vertice.Tag, vizinho.Tag);
                            VerticeList[pos].PredecessorBusca = vertice;
                            VerticeList[pos].NivelBusca = vertice.NivelBusca ++;
                            tempo++;
                            VerticeList[pos].IndiceBusca = tempo;
                            Fila.Enqueue(VerticeList[pos]);
                        }
                        //visita aresta tio
                        else if(VerticeList[pos].NivelBusca == vertice.NivelBusca + 1)
                        {
                            Console.WriteLine("{0}:{1} - Visita de aresta tio", vertice.Tag, VerticeList[pos].Tag);
                        }
                        else if(VerticeList[pos].NivelBusca == vertice.NivelBusca && VerticeList[pos].PredecessorBusca == vertice.PredecessorBusca && VerticeList[pos].IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("{0}:{1} - Visita de aresta irmão", vertice.Tag, VerticeList[pos].Tag);
                        }
                        else if (VerticeList[pos].NivelBusca == vertice.NivelBusca && VerticeList[pos].PredecessorBusca != vertice.PredecessorBusca && VerticeList[pos].IndiceBusca > vertice.IndiceBusca)
                        {
                            Console.WriteLine("{0}:{1} - Visita de aresta primo", vertice.Tag, VerticeList[pos].Tag);
                        }
                    }
                }

            }
            Console.ReadKey();
        }

        public static void TestarConectividade(List<Vertice> verticeList, out bool isConexo)
        {
            tempo = 0;
            Fila = new Queue<Vertice>();
            isConexo = true;
            int contador = 0;

            VerticeList = new List<Vertice>();

            VerticeList = verticeList;

            Console.Clear();
            for (int i = 0; i < VerticeList.Count; i++)
            {
                VerticeList[i].IndiceBusca = 0;
                VerticeList[i].NivelBusca = 0;
                VerticeList[i].PredecessorBusca = null;
            }

            while (VerticeList.Find(v => v.IndiceBusca == 0) != null)
            {
                Vertice vertice = VerticeList.Find(v => v.IndiceBusca == 0);
                tempo++;
                vertice.IndiceBusca = tempo;
                Fila.Enqueue(vertice);
                TestarConectividade(Fila);
                contador++;
            }

            if (contador > 1)
                isConexo = false;
        }
        private static void TestarConectividade(Queue<Vertice> Fila)
        {
            while (Fila.Count > 0)
            {
                Vertice vertice = Fila.Dequeue();
                if (vertice != null)
                {
                    foreach (Vertice vizinho in vertice.VerticesVizinhos)
                    {
                        if (vizinho.IndiceBusca == 0)
                        {
                            int pos = VerticeList.IndexOf(vizinho);

                            VerticeList[pos].PredecessorBusca = vertice;
                            VerticeList[pos].NivelBusca = vertice.NivelBusca++;
                            tempo++;
                            VerticeList[pos].IndiceBusca = tempo;
                            Fila.Enqueue(VerticeList[pos]);
                        }
                    }
                }
            }
        }

        public static void IniciarBuscaEmProfundidade(List<Vertice> verticeList, TipoGrafo tipoGrafo)
        {
            Console.Clear();
            Console.WriteLine("INICIANDO BUSCA EM PROFUNDIDADE");
            tempo = 0;

            VerticeList = new List<Vertice>();
            VerticeList = verticeList;

            for (int i = 0; i < verticeList.Count; i++)
            {
                VerticeList[i].TempoDescoberta = 0;
                VerticeList[i].TempoTermino = 0;
                VerticeList[i].PredecessorBusca = null;
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
            Console.ReadKey();
        }

        private static void BuscarEmProfundidadeND(int pos)
        {
            tempo++;
            VerticeList[pos].TempoDescoberta = tempo;

            int i = 0;
            foreach(Vertice vizinho in VerticeList[pos].VerticesVizinhos)
            {
                i++;
                if (vizinho.TempoDescoberta == 0)
                {
                    Console.WriteLine("{0}:{1} - Aresta de Arvore", vizinho.Tag, VerticeList[pos].Tag);
                    VerticeList[VerticeList.IndexOf(vizinho)].PredecessorBusca = VerticeList[pos];
                    BuscarEmProfundidadeND(VerticeList.IndexOf(vizinho));
                }
                else if(vizinho.TempoTermino == 0 && vizinho != VerticeList[pos])
                {
                    Console.WriteLine("{0}:{1} - Aresta de Retorno", vizinho.Tag, VerticeList[pos].Tag);
                }
            }
            tempo++;
            VerticeList[pos].TempoTermino = tempo;
        }

        private static void BuscarEmProfundidadeDI(int pos)
        {
            tempo++;
            VerticeList[pos].TempoDescoberta = tempo;

            int i = 0;
            foreach (Vertice vizinho in VerticeList[pos].VerticesFilho)
            {
                i++;
                if (vizinho.TempoDescoberta == 0)
                {
                    Console.WriteLine("{0}:{1} - Aresta de Arvore", vizinho.Tag, VerticeList[pos].Tag);
                    VerticeList[VerticeList.IndexOf(vizinho)].PredecessorBusca = VerticeList[pos];
                    BuscarEmProfundidadeND(VerticeList.IndexOf(vizinho));
                }
                else
                {
                    if (vizinho.TempoTermino == 0)
                        Console.WriteLine("{0}:{1} - Aresta de Retorno", vizinho.Tag, VerticeList[pos].Tag);
                    else if(VerticeList[pos].TempoDescoberta < vizinho.TempoDescoberta)
                        Console.WriteLine("{0}:{1} - Aresta de Avanço", vizinho.Tag, VerticeList[pos].Tag);
                    else
                        Console.WriteLine("{0}:{1} - Aresta de Cruzamento", vizinho.Tag, VerticeList[pos].Tag);
                }
            }
            tempo++;
            VerticeList[pos].TempoTermino = tempo;
        }
    }
}
