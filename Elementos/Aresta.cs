using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    internal class Aresta
    {
        private Vertice _verticeFilho;

        private Vertice _verticePai;

        private string _nome;

        public int num_arestas = 0;

        public int peso = 0;

        public Vertice VerticeFilho
        {
            get { return _verticeFilho; } 
            set { _verticeFilho = value;}
        }
        public Vertice VerticePai
        {
            get { return _verticePai; }
            set { _verticePai = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public Aresta(Vertice verticePai, Vertice verticeFilho) 
        { 
            VerticeFilho = verticeFilho;
            VerticePai = verticePai;
            Nome = verticePai.Tag + ":" + verticeFilho.Tag;
            num_arestas++;
        }
    }
}
