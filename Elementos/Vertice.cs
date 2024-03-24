using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    internal class Vertice
    {

        private List<Vertice> _prox_vertice = new List<Vertice>();

        private List<Vertice> _ant_vertice = new List<Vertice>();
        
        private string _tag = string.Empty;

        private int _valor;

        private int _grau = 0;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public int Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
    
        public int Grau
        {
            get { return _grau; }
            set { _grau = value; }
        }

        public Vertice(string tag) {
            Tag = tag;
        }

        //Adiciona um novo vértice-filho 
        public void AdicionarProxVertice(Vertice vertice)
        {
            if (vertice != null)
                _prox_vertice.Add(vertice);
        }

        //Adiciona um novo vértice-pai
        public void AdicionarAntVertice(Vertice vertice)
        {
            if (vertice != null)
                _ant_vertice.Add(vertice);
        }

        //Remove um vértice-filho
        public void RemoverProxVertice(Vertice vertice)
        {
            if (vertice != null)
                _prox_vertice.Remove(vertice);
        }

        //Remove um vértice-pai
        public void RemoverAntVertice(Vertice vertice)
        {
            if (vertice != null)
                _ant_vertice.Remove(vertice);
        }

        public void ExibirProxVertices()
        {
            string mesclarVertices = String.Concat("\t" + Tag, " : { ");

            foreach (Vertice vertice in _prox_vertice)
            {
                mesclarVertices += String.Concat(vertice.Tag, ";");
            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        public void ExibirAntVertices()
        {
            string mesclarVertices = String.Concat("\t" + Tag, " : { ");

            foreach (Vertice vertice in _ant_vertice)
            {
                mesclarVertices += String.Concat(vertice.Tag, ";");
            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        public void ExibirVizinhanca()
        {
            string mesclarVertices = String.Concat("\t" + Tag, " : { ");

            foreach (Vertice vertice in _prox_vertice)
            {
                mesclarVertices += String.Concat(vertice.Tag, ";");
            }

            foreach (Vertice vertice in _ant_vertice)
            {
                mesclarVertices += String.Concat(vertice.Tag, ";");

            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        public void ExibirTodosAntecessores()
        {

        }

        public void ExibirTOdosSucessores()
        {

        }
    }
}
