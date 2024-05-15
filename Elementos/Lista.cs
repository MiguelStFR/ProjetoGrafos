using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    internal class Lista
    {
        private static List<Vertice> _vertices;

        public static List<Vertice> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Lista(List<Vertice> verticeList, List<Aresta> arestasList, out List<Vertice> VerticeListAux) 
        {
            Vertices = AdicionarVizinhos(verticeList, arestasList);
            AgruparVertices();
            VerticeListAux = Vertices;
        }

        public static List<Vertice> AdicionarVizinhos(List<Vertice> verticeList, List<Aresta> arestasList)
        {
            List<Aresta> arestas;

            foreach (Vertice vertice in verticeList)
            {
                verticeList[verticeList.IndexOf(vertice)].ZerarVizinhanca();
            }

            foreach (Vertice vertice in verticeList)
            {

                if (arestasList[0].TipoAresta == TipoGrafo.DI)
                {
                    arestas = arestasList.FindAll(a => a.VerticePredecessor.Equals(vertice) || a.VerticeSucessor.Equals(vertice));

                    foreach (Aresta aresta in arestas)
                    {
                        if (aresta.VerticePredecessor.Equals(vertice))
                            verticeList[verticeList.IndexOf(vertice)].AdicionarVerticeFilho(aresta.VerticeSucessor);
                        else
                            verticeList[verticeList.IndexOf(vertice)].AdicionarVerticePai(aresta.VerticePredecessor);
                    }
                }
                else
                {
                    arestas = arestasList.FindAll(a => a.VerticePredecessor.Equals(vertice));

                    foreach (Aresta aresta in arestas)
                    {
                        verticeList[verticeList.IndexOf(vertice)].AdicionarVerticeFilho(aresta.VerticeSucessor);
                        verticeList[verticeList.IndexOf(vertice)].AdicionarVerticePai(aresta.VerticeSucessor);

                        verticeList[verticeList.IndexOf(aresta.VerticeSucessor)].AdicionarVerticeFilho(vertice);
                        verticeList[verticeList.IndexOf(aresta.VerticeSucessor)].AdicionarVerticePai(vertice);
                    }
                }              
            }
            return verticeList;
        }

        public void MostrarListaFilhos()
        {
            string ListaString = string.Empty;
            ListaString += "\nRepresentação em Lista:\n\n";
            foreach(Vertice vertice in Vertices)
            {
                ListaString += vertice.Tag + ": " + vertice.MostrarFilhos() + "\n";
            }

            Console.WriteLine(ListaString);
        }

        public void MostrarListaPais()
        {
            string ListaString = string.Empty;
            ListaString += "\nRepresentação em Lista Antecessores:\n\n";
            foreach (Vertice vertice in Vertices)
            {
                ListaString += vertice.Tag + ": " + vertice.MostrarPais() + "\n";
            }

            Console.WriteLine(ListaString);
        }

        public void MostrarListaVizinhos()
        {
            string ListaString = string.Empty;
            ListaString += "\nRepresentação em Lista:\n\n";
            foreach (Vertice vertice in Vertices)
            {
                ListaString += vertice.Tag + ": " + vertice.MostrarVizinhos() + "\n";
            }

            Console.WriteLine(ListaString);
        }


        private void AdicionarEmGrupo(Vertice vertice)
        {
            Vertices[Vertices.IndexOf(vertice)].Grupo = 1;

            foreach(Vertice vizinho in vertice.VerticesVizinhos)
            {
                if(vizinho.Grupo == -1)
                {
                    Vertices[Vertices.IndexOf(vizinho)].Grupo = 1 - Vertices[Vertices.IndexOf(vertice)].Grupo;
                }
            }
        }

        private void AgruparVertices()
        {
            for(int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Grupo == -1)
                {
                    AdicionarEmGrupo(Vertices[i]);
                }
            }
        }
    }
}
