﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.Elementos
{
    //DI = Direcionado ND = Não-Direcionado
    public enum TipoGrafo { DI, ND}
    internal class Grafo
    {
        private List<Vertice> VerticeList = new List<Vertice>();

        private string _nome;

        private TipoGrafo _tipo;

        private bool _isSimples = false;

        private bool _isRegular = false;

        private bool _isCompleto = false;

        private bool _isBipartido = false;

        public string Nome
        {
            get {  return _nome; }
        }

        public TipoGrafo Tipo
        {
            get { return _tipo; }
        }

        public bool IsSimples
        {
            get { return _isSimples; }
            set { _isSimples = value; }
        }

        public bool IsRegular
        {
            get { return _isRegular; }
            set { _isRegular = value; }
        }
        
        public bool IsCompleto
        {
            get { return _isCompleto; }
            set { _isCompleto = value; }
        }

        public bool IsBipartido
        {
            get { return _isBipartido; }
            set { _isBipartido = value; }
        }
        
        public Grafo(string Nome, TipoGrafo Tipo) 
        {
            _nome = Nome;
            _tipo = Tipo;
        }

        private void AdicionarVertice(Vertice vertice)
        {
            if (vertice != null) 
            { 
                if (VerticeList != null)
                    if (VerticeList.Find(v => v.Tag == vertice.Tag) != null) 
                        return; 
            }
            else 
                return;

            VerticeList.Add(vertice);
        }

        public void CriarVertices()
        {
            Console.Clear();

            Console.Write("Digite o nome de cada vértice em sequecia(ex:'V1/V2/V3/...')\n" + "->");
            string[] retorno = Console.ReadLine().Split('/');

            foreach (string s in retorno)
            {
                Vertice vertice = new Vertice(s);
                AdicionarVertice(vertice);
            }
        }

        private void RelacionarNaoDirecional(string vertice_pai, string vertice_filho)
        {
            //int indexPai = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_pai));
            //int indexFilho = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_filho));

            VerticeList[IndexVertice(vertice_pai)].AdicionarProxVertice(VerticeAux(vertice_filho));
            VerticeList[IndexVertice(vertice_filho)].AdicionarProxVertice(VerticeAux(vertice_pai));
        }

        private void RemoverRelacionarNaoDirecional(string vertice_pai, string vertice_filho)
        {
            //int indexPai = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_pai));
            //int indexFilho = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_filho));

            VerticeList[IndexVertice(vertice_pai)].RemoverProxVertice(VerticeAux(vertice_filho));
            VerticeList[IndexVertice(vertice_filho)].RemoverProxVertice(VerticeAux(vertice_pai));
        }

        private void RelacionarDirecional(string vertice_pai, string vertice_filho)
        {
            //int indexPai = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_pai));
            //int indexFilho = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_filho));

            VerticeList[IndexVertice(vertice_pai)].AdicionarProxVertice(VerticeAux(vertice_filho));
            VerticeList[IndexVertice(vertice_filho)].AdicionarAntVertice(VerticeAux(vertice_pai));
        }

        private void RemoverRelacionarDirecional(string vertice_pai, string vertice_filho)
        {
            //int indexPai = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_pai));
            //int indexFilho = VerticeList.IndexOf(VerticeList.Find(v => v.Tag == vertice_filho));

            VerticeList[IndexVertice(vertice_pai)].RemoverProxVertice(VerticeAux(vertice_filho));
            VerticeList[IndexVertice(vertice_filho)].RemoverAntVertice(VerticeAux(vertice_pai));
        }

        public void AdicionarAresta()
        {
            Console.Clear();

            Console.Write("Digite a aresta(vertice_pai/vertice_filho) a ser criada(ex:'V1:V2')\n" + "->");
            string[] aresta = Console.ReadLine().Split(':');

            if (!BuscarVertice(aresta[0].Trim()))
            {
                Console.WriteLine("O vertice " + aresta[0].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }
                
            else if (!BuscarVertice(aresta[1].Trim()))
            {
                Console.WriteLine("O vertice " + aresta[1].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            if (Tipo == TipoGrafo.ND)
                RelacionarNaoDirecional(aresta[0].Trim(), aresta[1].Trim());
            else
                RelacionarDirecional(aresta[0].Trim(), aresta[1].Trim());
        }

        public void RemoverAresta()
        {
            Console.Clear();

            Console.Write("Digite a aresta(vertice_pai/vertice_filho) a ser removida(ex:'V1:V2')\n" + "->");
            string[] aresta = Console.ReadLine().Split(':');

            if (!BuscarVertice(aresta[0].Trim()))
            {
                Console.WriteLine("O vertice " + aresta[0].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            else if (!BuscarVertice(aresta[1].Trim()))
            {
                Console.WriteLine("O vertice " + aresta[1].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            if (Tipo == TipoGrafo.ND)
                RemoverRelacionarNaoDirecional(aresta[0].Trim(), aresta[1].Trim());
            else
                RemoverRelacionarDirecional(aresta[0].Trim(), aresta[1].Trim());
        }

        public void CriarRelacoes()
        {
            Console.Clear();

            Console.Write("Digite as relações de cada par de vertices(pai/filho) em sequencia (ex:'V1:V2/V2:V1/V1:V3/V3:V4/...')\n" + "->");
            string[] retorno = Console.ReadLine().Split('/');

            foreach (string s in retorno)
            {
                string[] vertice = s.Split(':');

                if (VerticeList.Find(v => v.Tag == vertice[0].Trim()) == null)
                    continue;
                else if (VerticeList.Find(v => v.Tag == vertice[1].Trim()) == null)
                    continue;

                if (Tipo == TipoGrafo.ND)
                    RelacionarNaoDirecional(vertice[0].Trim(), vertice[1].Trim());
                else
                    RelacionarDirecional(vertice[0].Trim(), vertice[1].Trim());
            }
        }

        public void ExibirGrafo()
        {

            Console.WriteLine(
                "\n\nGrafo: " + Nome +
                "\nTipo:" + Tipo +
                "\nÉ simples? " + IsSimples +
                "\nÉ regular? " + IsRegular +
                "\nÉ completo?" + IsCompleto +
                "\nÉ Bipartido?" + IsBipartido +
                "\nRelações:"
                );
            foreach(Vertice vertice in VerticeList) 
            { 
                vertice.ExibirProxVertices();
            }
        }

        public bool BuscarVertice(string Tag)
        {
            if (VerticeList.Find(v => v.Tag == Tag) == null)
                return false;
            else 
                return true;
        }

        public int IndexVertice(string Tag)
        {
            return VerticeList.IndexOf(VerticeAux(Tag));
        }

        public Vertice VerticeAux(string Tag)
        {
            return VerticeList.Find(v => v.Tag == Tag);
        }

        public void Deletar()
        {
            Console.Clear();
            Console.Write("Confirmar deleção(S/N):");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "N":
                case "n":
                default:
                    Console.WriteLine("\nDeleção cancelada\n");
                    break;
                case "S":
                case "s":
                    Program._Grafos.Remove(this);
                    break;
            }
        }
    }
}
