﻿using System;
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

        private string _tag = string.Empty;

        private int _valor;

        private bool _temLaco = false;

        private int _grau = 0;

        private int _grupo;

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

        public Vertice(string tag) {
            Tag = tag;
            Grupo = -1;
        }

        public int Grau
        {
            get { return _grau; }
        }

        public bool TemLaco
        {
            get { return _temLaco; }
            set { _temLaco = value; }
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

        public void AdicionarVerticePai(Vertice vertice)
        {
            if (vertice != null)
            {
                _verticesPai.Add(vertice);
                _verticesVizinhos.Add(vertice);

                _grau++;

                if(vertice.Tag ==  Tag)
                    TemLaco = true;
            }
        }

        public void AdicionarVerticeFilho(Vertice vertice)
        {
            if (vertice != null)
            {
                _verticesFilho.Add(vertice);
                _verticesVizinhos.Add(vertice);

                _grau--;

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
                _grau = (arestas.Count)/2;
            else
                _grau = arestas.Count;
        }
    }
}
