using ProjetoGrafos.Elementos;
using ProjetoGrafos.Operacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos
{
    internal class Program
    {
        public static List<Grafo> _Grafos = new List<Grafo>();
        public static void Main(string[] args)
        {
            Console.Title = "TRABALHO 01 - GRAFOS";
            Menu.ExbirMenu();
        }
    }
}
