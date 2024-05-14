using System;
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
        private List<Vertice> _verticeList = new List<Vertice>();

        private List<Aresta> _arestasList = new List<Aresta>();

        private List<Vertice> _lacos = new List<Vertice>();

        private TipoGrafo _tipo;

        private Matriz[,] _matrizAdjacencia;

        private Lista _listaAdjacencia;

        private string _nome;

        private bool _isSimples = false;

        private bool _isRegular = false;

        private bool _isCompleto = false;

        private bool _isBipartido = false;

        public string Nome
        {
            get {  return _nome; }
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

        public List<Aresta> ArestaList
        {
            get { return _arestasList; }
            set { _arestasList = value; }
        }

        public List<Vertice> VerticeList
        {
            get { return _verticeList; }
        }

        public TipoGrafo Tipo
        {
            get { return _tipo; }
        }

        public Matriz[,] MatrizAdjacencia
        {
            get { return _matrizAdjacencia; }
            set { _matrizAdjacencia = value; }
        }

        public Lista ListaAdjacencia
        {
            set { _listaAdjacencia = value; }
            get { return _listaAdjacencia; }
        }

        private void AdicionarVertice(Vertice vertice)
        {
            if (vertice != null) 
            { 
                if (_verticeList != null)
                    if (_verticeList.Find(v => v.Tag == vertice.Tag) != null) 
                        return;
            }
            else 
                return;

            _verticeList.Add(vertice);
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

        private void AdicionarRelacao(string vertice_pai, string vertice_filho, int peso, TipoGrafo tipoAresta)
        {
            if (vertice_pai != null && vertice_filho != null)
            {
                if (ArestaList.Find(a => a.VerticePredecessor.Tag == vertice_pai && a.VerticeSucessor.Tag == vertice_filho) != null)
                    _arestasList[_arestasList.IndexOf(_arestasList.Find(a => a.VerticePredecessor.Tag == vertice_pai && a.VerticeSucessor.Tag == vertice_filho))].num_arestas++;
                else
                    _arestasList.Add(new Aresta(VerticeAux(vertice_pai), VerticeAux(vertice_filho), peso, tipoAresta));
                
            }
        }

        private void RemoverRelacao(string vertice_pai, string vertice_filho)
        {
            if (vertice_pai != null && vertice_filho != null)
            {
                Aresta aresta = ArestaList.Find(a => a.VerticePredecessor.Tag == vertice_pai && a.VerticeSucessor.Tag == vertice_filho);

                if (aresta != null && aresta.num_arestas == 0)
                    _arestasList.Remove(aresta);
                else
                    _arestasList[_arestasList.IndexOf(_arestasList.Find(a => a.VerticePredecessor.Tag == vertice_pai && a.VerticeSucessor.Tag == vertice_filho))].num_arestas--;
            }
        }

        public void AdicionarAresta()
        {
            Console.Clear();

            Console.Write("Digite a aresta(predescessor:sucessor:peso) a ser criada(ex:'V1:V2:4')\n" + "->");
            string[] vertice = Console.ReadLine().Split(':');

            if (!BuscarVertice(vertice[0].Trim()))
            {
                Console.WriteLine("O vertice " + vertice[0].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }
                
            else if (!BuscarVertice(vertice[1].Trim()))
            {
                Console.WriteLine("O vertice " + vertice[1].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            int peso = 0;

            if (String.IsNullOrEmpty(vertice[2].Trim()))
                peso = int.Parse(vertice[2].Trim());

            AdicionarRelacao(vertice[0].Trim(), vertice[1].Trim(), peso, Tipo);

            atualizarGrafo();
        }

        public void RemoverAresta()
        {
            Console.Clear();

            Console.Write("Digite a aresta(vertice_pai/vertice_filho) a ser removida(ex:'V1:V2')\n" + "->");
            string[] vertice = Console.ReadLine().Split(':');

            if (!BuscarVertice(vertice[0].Trim()))
            {
                Console.WriteLine("O vertice " + vertice[0].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            else if (!BuscarVertice(vertice[1].Trim()))
            {
                Console.WriteLine("O vertice " + vertice[1].Trim() + " não existe no grafo " + Nome + ". Operação cancelada.");
                return;
            }

            RemoverRelacao(vertice[0].Trim(), vertice[1].Trim());

            atualizarGrafo();
        }

        public void CriarRelacoes()
        {
            Console.Clear();

            Console.Write("Digite as relações de cada par de vertices(pai:filho:peso) em sequencia (ex:'V1:V2:2/V2:V1:3/V1:V3:1/V3:V4:5/...')\n" + "->");
            string[] retorno = Console.ReadLine().Split('/');

            foreach (string s in retorno)
            {
                string[] vertice = s.Split(':');

                if (_verticeList.Find(v => v.Tag == vertice[0].Trim()) == null)
                    continue;
                else if (_verticeList.Find(v => v.Tag == vertice[1].Trim()) == null)
                    continue;
    
                AdicionarRelacao(vertice[0].Trim(), vertice[1].Trim(), int.Parse(vertice[2].Trim()), Tipo);
            }
            atualizarGrafo();
        }

        public void ExibirGrafo()
        {
            Console.Clear();
            Console.WriteLine(
                "\nGrafo:\t\t" + Nome +
                "\nTipo:\t\t" + ((Tipo == TipoGrafo.DI)?"Direcionado":"Não Direcionado") +
                "\nÉ simples?\t" + IsSimples +
                "\nÉ regular?\t" + IsRegular +
                "\nÉ completo?\t" + IsCompleto +
                "\nÉ Bipartido?\t" + IsBipartido +
                "\nLaços:\t\t" + ((_lacos != null) ? MostrarLacos() : "NA") + "\n"
                );
            MostrarMatriz();
            MostrarLista();
            Console.WriteLine("\n=====================================================================================================\n");
            Console.WriteLine("Clique em qualquer tecla para voltar\n");
            Console.ReadKey();
        }

        public void ExibirFilhosVertice(Vertice vertice)
        {
            string mesclarVertices = String.Concat(vertice.Tag, " : { ");

            List<Aresta> arestasVertice = _arestasList.FindAll(a => a.VerticePredecessor.Equals(vertice));

            foreach (Aresta aresta in arestasVertice)
            {
                mesclarVertices += String.Concat(aresta.VerticeSucessor.Tag, ";");
            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        public void ExibirPaisVertice(Vertice vertice)
        {
            string mesclarVertices = String.Concat(vertice.Tag, " : { ");

            List<Aresta> arestasVertice = _arestasList.FindAll(a => a.VerticeSucessor.Equals(vertice));

            foreach (Aresta aresta in arestasVertice)
            {
                mesclarVertices += String.Concat(aresta.VerticePredecessor.Tag, ";");
            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        public void ExibirVizinhanca(Vertice vertice)
        {
            string mesclarVertices = String.Concat("\t" + vertice.Tag, " : { ");

            List<Aresta> arestasVertice = _arestasList.FindAll(a => a.VerticePredecessor.Equals(vertice));

            foreach (Aresta aresta in arestasVertice)
            {
                mesclarVertices += String.Concat(aresta.VerticePredecessor.Tag, ";");
            }

            foreach (Aresta aresta in arestasVertice)
            {
                mesclarVertices += String.Concat(aresta.VerticeSucessor.Tag, ";");
            }

            mesclarVertices.Remove(mesclarVertices.Length - 1);
            mesclarVertices += " }";

            Console.WriteLine(mesclarVertices);
        }

        //
        public bool BuscarVertice(string Tag)
        {
            if (_verticeList.Find(v => v.Tag == Tag) == null)
                return false;
            else 
                return true;
        }

        //
        public int IndexVertice(string Tag)
        {
            return _verticeList.IndexOf(VerticeAux(Tag));
        }

        //
        public Vertice VerticeAux(string Tag)
        {
            return _verticeList.Find(v => v.Tag == Tag);
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
                    Console.WriteLine("\nDeleção cancelada.\n");
                    break;
                case "S":
                case "s":
                    Program._Grafos.Remove(this);
                    break;
            }
        }
    
        public void atualizarMatriz()
        {
            _matrizAdjacencia = new Matriz[_verticeList.Count, _verticeList.Count];
            _matrizAdjacencia = Matriz.CriarMatriz(ArestaList, _verticeList);
        }

        public void atualizarSimples()
        {
            if( _lacos != null && _lacos.Count > 0)
            {
                IsSimples = false;
                return;
            }

            for(int i = 0; i < _verticeList.Count; i++)
            {

                for(int j = 0; j < _verticeList.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (MatrizAdjacencia[i, j].numRelacoes > 1)
                    {
                        IsSimples = false;
                        return;
                    }
                    j++;
                }   
            }
            IsSimples = true;
        }

        public void AtualizarRegular()
        {
            //Se existe algum vértice com um grau diferente do primeiro, então o grafo não é regular 
            if (_verticeList.Find(v => v.Grau != _verticeList[0].Grau) != null)
                IsRegular = false;
            else
                IsRegular = true;
        }

        public void AtualizarBipartido()
        {
            IsBipartido = true;
            foreach(Vertice vertice in _verticeList)
            {
                if(vertice.VerticesVizinhos.Find(v => v.Grupo.Equals(vertice.Grupo)) != null)
                {
                    IsBipartido = false;
                    return;
                }
            }
        }

        public void AtualizarCompleto()
        {
            IsCompleto = true;

            if(!IsSimples)
            {
                IsCompleto = false;
                return;
            }

            if(Tipo == TipoGrafo.DI)
            {
                if (_verticeList.FindAll(v => v.Grau != (_verticeList.Count - 1) * 2).Count > 0)
                {
                    IsCompleto = false;
                    return;
                }
            }
            else
            {
                if (_verticeList.FindAll(v => v.Grau != (_verticeList.Count - 1)).Count > 0)
                {
                    IsCompleto = false;
                    return;
                }
            }

            int i = 0;

            foreach (Vertice vertice in _verticeList)
            {
                int j = 0;

                if (MatrizAdjacencia[i,i].numRelacoes > 0)
                {
                    IsCompleto = false;
                    return;
                }

                foreach (Vertice verticeAux in _verticeList)
                {
                    j++;
                    if (j == i + 1)
                        continue;

                    if (MatrizAdjacencia[i, j-1].numRelacoes > 0)
                        continue;
                    else
                    {
                        IsCompleto = false;
                        return;
                    }
                }
                i++;
            }
        }

        public void AtualizarGrauVertices()
        {
            foreach (Vertice vertice in _verticeList)
                vertice.AtualizarGrau(ArestaList.FindAll(a => a.VerticePredecessor.Equals(vertice) || a.VerticeSucessor.Equals(vertice)), Tipo);            
        }

        public void MostrarMatriz()
        {
            Matriz.mostrarMatriz(MatrizAdjacencia, _verticeList.Count);
        }

        public void MostrarLista()
        {
            ListaAdjacencia.MostrarListaFilhos();
        }

        public string MostrarLacos()
        {
            string lacos = "{ ";
            foreach (Vertice vertice in _lacos)
                lacos += vertice.Tag + "; ";
            lacos.Remove(lacos.Length - 1);
            lacos += "}";
            return lacos;
        }

        private void atualizarLista()
        {
            ListaAdjacencia = new Lista(_verticeList, ArestaList, out _verticeList);
        }

        private void AtualizarLacos()
        {
            _lacos.Clear();
            if(_verticeList != null)
                _lacos = _verticeList.FindAll(v => v.TemLaco == true);
        }

        //É chamado sempre que é inserido/removido uma nova aresta/vértice
        public void atualizarGrafo()
        {
            atualizarLista();

            atualizarMatriz();

            AtualizarLacos();

            AtualizarGrauVertices();

            AtualizarBipartido();
            
            AtualizarRegular();
            
            atualizarSimples();

            AtualizarCompleto();
        }
    }
}
