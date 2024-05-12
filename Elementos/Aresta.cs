using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    internal class Aresta
    {
        private Vertice _sucessor;

        private Vertice _predecessor;

        private TipoGrafo _tipoAresta;

        private string _nome;

        public int num_arestas = 0;

        private int _peso = 0;

        public Vertice VerticePredecessor
        {
            get { return _predecessor; } 
            set { _predecessor = value;}
        }
        public Vertice VerticeSucessor
        {
            get { return _sucessor; }
            set { _sucessor = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public Aresta(Vertice verticePredecessor, Vertice verticeSucessor, int peso, TipoGrafo tipoAresta) 
        { 
            _sucessor = verticeSucessor;
            _predecessor = verticePredecessor;
            Nome = verticePredecessor.Tag + ":" + verticeSucessor.Tag;
            Peso = peso;
            _tipoAresta = tipoAresta;
            num_arestas++;
        }
    }
}
