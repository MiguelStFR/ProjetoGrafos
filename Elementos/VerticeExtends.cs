using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    internal class VerticeExtends
    {
        private Vertice _vertice = null;
        public Vertice Vertice
        {
            get { return _vertice; }
            set { _vertice = value; }
        }

        private int _grauOrdenacao = 0;
        public int GrauOrdenacao
        {
            get { return _grauOrdenacao; }
            set { _grauOrdenacao = value; }
        }

        private int _indiceBusca = 0;
        public int IndiceBusca
        {
            get { return _indiceBusca; }
            set { _indiceBusca = value; }
        }

        private int _nivelBusca = 0;
        public int NivelBusca
        {
            get { return _nivelBusca; }
            set { _nivelBusca = value; }
        }

        private int _tempoDescoberta = 0;
        public int TempoDescoberta
        {
            get { return _tempoDescoberta; }
            set { _tempoDescoberta = value; }
        }

        private int _tempoTermino = 0;
        public int TempoTermino
        {
            get { return _tempoTermino; }
            set { _tempoTermino = value; }
        }

        private Vertice _predecessorBusca = null;
        public Vertice PredecessorBusca
        {
            get { return _predecessorBusca; }
            set { _predecessorBusca = value; }
        }

        public VerticeExtends(Vertice vertice) 
        {
            _vertice = vertice;  
        }
    }

    internal class VerticeExtendsList
    {
        private List<VerticeExtends> _verticeExtendsList = new List<VerticeExtends>();

        public List<VerticeExtends> verticeExtendsList
        {
            get { return this._verticeExtendsList; }
            set {  this._verticeExtendsList = value; }
        }

        public VerticeExtendsList(List<Vertice> verticeList)
        {
            foreach (Vertice vertice in verticeList)
            {
                _verticeExtendsList.Add(new VerticeExtends(vertice));
            }
        }
    }
}
