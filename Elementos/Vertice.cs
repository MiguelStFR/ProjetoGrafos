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
        private List<Vertice> _verticesVizinhos = new List<Vertice>();

        private List<Vertice> _verticesPai = new List<Vertice>();

        private List<Vertice> _verticesFilho = new List<Vertice>();

        private Vertice _predecessorBusca = null;

        private string _tag = string.Empty;

        private int _valor;

        private int _grauEntrada = 0;

        private int _grauSaida = 0;

        private int _grau = 0;

        private int _grupo;

        private int _grauOrdenacao = 0;

        private int _indiceBusca = 0;

        private int _nivelBusca = 0;

        private int _tempoDescoberta = 0;

        private int _tempoTermino = 0;
        
        private bool _temLaco = false;

        public int Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

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
        }

        public int GrauEntrada
        {
            get { return _grauEntrada; }
        }

        public int GrauSaida
        {
            get { return _grauSaida; }
        }

        public bool TemLaco
        {
            get { return _temLaco; }
            set { _temLaco = value; }
        }

        public int GrauOrdenacao
        {
            get { return _grauOrdenacao; }
            set { _grauOrdenacao = value; }
        }

        public int TempoTermino
        {
            get { return _tempoTermino; }
            set { _tempoTermino = value; }
        }
        public int IndiceBusca
        {
            get { return _indiceBusca; }
            set { _indiceBusca = value; }
        }

        public int NivelBusca
        {
            get { return _nivelBusca; }
            set { _nivelBusca = value; }
        }

        public int TempoDescoberta
        {
            get { return _tempoDescoberta; }
            set { _tempoDescoberta = value; }
        }

        public Vertice PredecessorBusca
        {
            get { return _predecessorBusca; }
            set { _predecessorBusca = value; }
        }

        public List<Vertice> VerticesVizinhos
        {
            get { return _verticesVizinhos; }
        }

        public List<Vertice> VerticesPai
        {
            get { return _verticesPai; }
        }

        public List<Vertice> VerticesFilho
        {
            get { return _verticesFilho; }
        }

        public Vertice(string tag)
        {
            Tag = tag;
            Grupo = -1;
        }

        public void AdicionarVerticePai(Vertice vertice)
        {
            if (vertice != null)
            {
                if (_verticesPai.Find(v => v._tag == vertice._tag) == null)
                    _verticesPai.Add(vertice);

                if(_verticesVizinhos.Find(v => v._tag == vertice._tag) == null)
                    _verticesVizinhos.Add(vertice);

                _grauEntrada++;

                if(vertice.Tag ==  Tag)
                    TemLaco = true;
            }
        }

        public void AdicionarVerticeFilho(Vertice vertice)
        {
            if (vertice != null)
            {
                if (_verticesFilho.Find(v => v._tag == vertice._tag) == null)
                    _verticesFilho.Add(vertice);

                if (_verticesVizinhos.Find(v => v._tag == vertice._tag) == null)
                    _verticesVizinhos.Add(vertice);

                _grauSaida++;

                if (vertice.Tag == Tag)
                    TemLaco = true;
            }
        }

        public void RemoverVerticePai(Vertice vertice)
        {
            if (vertice != null)
            {
                _verticesPai.Remove(vertice);
                _verticesVizinhos.Remove(vertice);

                _grauEntrada--;

                if (VerticesPai.Find(v => v.Tag == Tag) == null)
                    TemLaco = false;
            }
        }

        public void RemoverVerticeFilho(Vertice vertice)
        {
            if (vertice != null)
            {
                _verticesFilho.Remove(vertice);
                _verticesVizinhos.Remove(vertice);

                _grauSaida--;

                if (VerticesFilho.Find(v => v.Tag == Tag) == null)
                    TemLaco = false;
            }
        }

        public string MostrarPais()
        {
            string mescla = "{ ";

            foreach (Vertice vertice in _verticesPai)
                mescla += vertice.Tag + "; ";

            mescla.Remove(mescla.Length - 1);
            mescla += " }";

            return mescla;
        }

        public string MostrarFilhos()
        {
            string mescla = "{ ";

            foreach (Vertice vertice in _verticesFilho)
                mescla += vertice.Tag + "; ";

            mescla.Remove(mescla.Length - 1);
            mescla += " }";

            return mescla;
        }

        public string MostrarVizinhos()
        {
            string mescla = "{ ";

            foreach (Vertice vertice in _verticesVizinhos)
                mescla += vertice.Tag + "; ";

            mescla.Remove(mescla.Length - 1);
            mescla += " }";

            return mescla;
        }

        public void AtualizarGrau(List<Aresta> arestas, TipoGrafo tipo)
        {

            if (tipo == TipoGrafo.ND)
            {
                _grauEntrada = _grauSaida = arestas.Count / 2;
            }
            else
            {
                _grauEntrada = VerticesPai.Count;
                _grauSaida = VerticesFilho.Count;
            }
            _grau = _grauEntrada + _grauSaida;
        }

        public void ZerarVizinhanca()
        {
            _verticesFilho.Clear();
            _verticesPai.Clear();
            _verticesVizinhos.Clear();
            _grau = _grauEntrada = _grauSaida = 0;
        }
    }
}
