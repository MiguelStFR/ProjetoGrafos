using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DoctorsChallenge
{
    #region testes
    class GrafoFeriado
    {
        public List<Doutor> doutores = new List<Doutor>();
        public List<PeriodoFeriado> periodoFeriados = new List<PeriodoFeriado>();

        public GrafoFeriado(List<Doutor> doutores, List<PeriodoFeriado> periodoFeriados)
        {
            this.doutores = doutores;
            this.periodoFeriados = periodoFeriados;
        }
    }
    class Doutor
    {
        public List<int> diasDisponiveis;
        public List<PeriodoFeriado> periodoFerias;

        public Doutor(List<int> diasDisponiveis, List<PeriodoFeriado> periodoFerias)
        {
            this.diasDisponiveis = diasDisponiveis;
            this.periodoFerias = periodoFerias;
        }
    }
    class PeriodoFeriado
    {
        public int duracao;

        public PeriodoFeriado(int duracao)
        {
            this.duracao = duracao;
        }
    }
    internal class Operacao
    {
        public static int[] emparelhamento;

        static bool IsMatchingPossible(GrafoFeriado grafo, bool[] visited)
        {
            for (int u = 0; u < grafo.doutores.Count; u++)
            {
                foreach (int diaDisponivel in grafo.doutores[u].diasDisponiveis)
                {
                    if (!visited[diaDisponivel])
                    {
                        visited[diaDisponivel] = true;
                        if (emparelhamento[diaDisponivel] == -1 || IsMatchingPossible(grafo, visited))
                        {
                            emparelhamento[diaDisponivel] = u;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static int validarAtribuicoes(List<Doutor> doutores, List<PeriodoFeriado> feriados, int maxDias)
        {
            int numDoutores = doutores.Count;
            int numDiasFeriados = 0;
            emparelhamento = new int[numDiasFeriados];

            foreach (PeriodoFeriado feriado in feriados)
                numDiasFeriados += feriado.duracao;

            GrafoFeriado grafo = new GrafoFeriado(doutores, feriados);


            for (int i = 0; i < emparelhamento.Length; i++)
                emparelhamento[i] = -1;

            int totalAssigned = 0;
            for (int i = 0; i < numDoutores; i++)
            {
                bool[] visited = new bool[numDiasFeriados];


                if (IsMatchingPossible(grafo, visited))
                {
                    foreach (int match in emparelhamento)
                    {
                        if (match != -1)
                            totalAssigned++;
                    }
                }
            }
            return totalAssigned;
        }

        static void Main_()
        {
            List<Doutor> doutores = new List<Doutor>();

            List<PeriodoFeriado> feriados = new List<PeriodoFeriado>();

            int c = 2;

            int numAtribuicoes = validarAtribuicoes(doutores, feriados, c);

            if (numAtribuicoes == 0)
                Console.WriteLine("Não é possível realizar a atribuição.");
            else
                Console.WriteLine("Existe uma atribuição válida: " + numAtribuicoes);
        }
    }

    #endregion

    public class MaxFlow
    {
        private int size;
        private List<int>[] graph;
        public int[,] capacity;

        public MaxFlow(int n)
        {
            size = n;
            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            capacity = new int[n, n];
        }

        public void AddEdge(int u, int v, int w)
        {
            graph[u].Add(v);
            graph[v].Add(u);
            capacity[u, v] = w;
            capacity[v, u] = 0;
        }

        private bool BFS(int source, int sink, int[] parent)
        {
            bool[] visited = new bool[size];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in graph[u])
                {
                    if (!visited[v] && capacity[u, v] > 0)
                    {
                        parent[v] = u;
                        visited[v] = true;
                        if (v == sink)
                            return true;
                        queue.Enqueue(v);
                    }
                }
            }
            return false;
        }

        public (int, List<List<int>>) EdmondsKarp(int source, int sink)
        {
            int[] parent = new int[size];
            int maxFlow = 0;
            List<List<int>> flowPath = new List<List<int>>();

            while (BFS(source, sink, parent))
            {
                int pathFlow = int.MaxValue;
                int s = sink;
                List<int> path = new List<int>();
                while (s != source)
                {
                    pathFlow = Math.Min(pathFlow, capacity[parent[s], s]);
                    path.Add(s);
                    s = parent[s];
                }
                path.Add(source);
                path.Reverse();
                flowPath.Add(path);

                int v = sink;
                while (v != source)
                {
                    int u = parent[v];
                    capacity[u, v] -= pathFlow;
                    capacity[v, u] += pathFlow;
                    v = parent[v];
                }

                maxFlow += pathFlow;
            }

            return (maxFlow, flowPath);
        }
    }

    public static class DoctorScheduler
    {
        public static (bool, Dictionary<int, List<int>>) CanScheduleDoctors(int n, int k, int c, List<List<int>> periods, List<HashSet<int>> availability)
        {
            int numDays = periods.Sum(Dj => Dj.Count);
            int numNodes = 2 + n + numDays + k;
            int source = 0;
            int sink = 1;
            int offsetDoctors = 2;
            int offsetDays = 2 + n;
            int offsetPeriods = 2 + n + numDays;

            MaxFlow maxflow = new MaxFlow(numNodes);

            // Conectar source aos médicos
            for (int i = 0; i < n; i++)
            {
                maxflow.AddEdge(source, offsetDoctors + i, c);
            }

            int dayCounter = 0;
            Dictionary<int, int> periodNodeMap = new Dictionary<int, int>();
            Dictionary<int, int> dayNodeMap = new Dictionary<int, int>();

            // Conectar médicos aos dias e criar nós intermediários de períodos
            for (int j = 0; j < periods.Count; j++)
            {
                int periodNode = offsetPeriods + j;
                periodNodeMap[j] = periodNode;
                foreach (int day in periods[j])
                {
                    int dayNode = offsetDays + dayCounter;
                    dayNodeMap[day] = dayNode;
                    dayCounter++;
                    // Conectar nó do período a cada dia do período
                    maxflow.AddEdge(periodNode, dayNode, 1);

                    for (int i = 0; i < n; i++)
                    {
                        if (availability[i].Contains(day))
                        {
                            maxflow.AddEdge(offsetDoctors + i, periodNode, 1);
                            maxflow.AddEdge(offsetDoctors + i, dayNode, 1);
                        }
                    }

                    // Conectar dias ao sink
                    maxflow.AddEdge(dayNode, sink, 1);
                }
            }

            // Verificar se o fluxo máximo é igual ao número de dias
            int totalDays = dayCounter;

            int max_Flow = 0;
            List<List<int>> flowPath;
            (max_Flow, flowPath) = maxflow.EdmondsKarp(source, sink);

            if (max_Flow != totalDays)
                return (false, new Dictionary<int, List<int>>());

            // Atribuir médicos aos dias com base nos caminhos de fluxo
            Dictionary<int, List<int>> doctorAssignments = new Dictionary<int, List<int>>();
            foreach (var path in flowPath)
            {
                for (int i = 1; i < path.Count - 1; i++)
                {
                    int u = path[i];
                    int v = path[i + 1];
                    if (source < u && u < offsetDays && offsetDays <= v && v < offsetPeriods)
                    {
                        int doctorIdx = u - offsetDoctors + 1; // Ajustar para começar em 1
                        int dayIdx = dayNodeMap.FirstOrDefault(pair => pair.Value == v).Key + 1; // Ajustar para começar em 1
                        if (!doctorAssignments.ContainsKey(doctorIdx))
                            doctorAssignments[doctorIdx] = new List<int>();
                        doctorAssignments[doctorIdx].Add(dayIdx);
                    }
                    else if (source < u && u < offsetDays && v >= offsetPeriods)
                    {
                        int doctorIdx = u - offsetDoctors + 1; // Ajustar para começar em 1
                        int periodIdx = v - offsetPeriods;
                        foreach (int day in periods[periodIdx])
                        {
                            if (maxflow.capacity[u, dayNodeMap[day]] == 0)
                            {
                                doctorAssignments[doctorIdx].Add(day + 1); // Ajustar para começar em 1
                                break;
                            }
                        }
                    }
                }
            }

            return (true, doctorAssignments);
        }

        public static void Menu()
        {
            Console.Write("Número de médicos: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Número de períodos de férias: ");
            int k = int.Parse(Console.ReadLine());

            Console.Write("Máximo de dias de feriado que um médico pode trabalhar no total: ");
            int c = int.Parse(Console.ReadLine());

            List<List<int>> periods = new List<List<int>>();
            Console.WriteLine("Informe os períodos de férias (ex: para 3 dias, insira 1 2 3):");
            for (int j = 0; j < k; j++)
            {
                List<int> period = new List<int>();
                string[] input = Console.ReadLine().Split();
                foreach (string day in input)
                {
                    period.Add(int.Parse(day) - 1); // Ajustar para começar em 0 internamente
                }
                periods.Add(period);
            }

            List<HashSet<int>> availability = new List<HashSet<int>>();
            Console.WriteLine("Informe a disponibilidade dos médicos (ex: para os dias 1 e 2, insira 1 2):");
            for (int i = 0; i < n; i++)
            {
                HashSet<int> availableDays = new HashSet<int>();
                string[] input = Console.ReadLine().Split(' ');
                foreach (string day in input)
                {
                    availableDays.Add(int.Parse(day) - 1); // Ajustar para começar em 0 internamente
                }
                availability.Add(availableDays);
            }
            bool canSchedule;
            Dictionary<int, List<int>> assignments;
            (canSchedule, assignments) = CanScheduleDoctors(n, k, c, periods, availability);

            if (canSchedule)
            {
                Console.WriteLine("É possível agendar os médicos. As atribuições são:");
                foreach (var doctor in assignments.Keys)
                {
                    Console.WriteLine($"Médico {doctor}: Dias {string.Join(", ", assignments[doctor])}");
                }
            }
            else
            {
                Console.WriteLine("Não é possível agendar os médicos.");
            }
            Console.ReadKey();
        }
    }
}