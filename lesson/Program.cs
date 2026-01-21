using System;

namespace lesson
{
    public class Program
    {
        /// <summary>
        /// The main entrypoint of your application.
        /// </summary>
        /// <param name="args">The arguments passed to the program</param>
        public static void Main(string[] args)
        {
            // definire:
            // - l'algoritmo (o gli algoritmi) di cui fare il benchmark
            //   in questo caso "Algoritmi di ricerca"
            // - i test cases (casi numerosi ma univoci, uguali per tutti)
            //   su cui far girare gli algoritmi. I test cases spesso hanno un nome
            //   "ordinato", "ordinato al contrario" "casuale" etc...
            // - calcolare i tempi (media, varianza)

            int choice = Menu();

            switch (choice)
            {
                case 1:
                    BenchmarkSearch();
                    break;
                case 2:
                    BenchmarkSort();
                    break;
                default:
                    Console.WriteLine("Scelta non valida");
                    break;
            }
            Console.ReadKey();
        }

        private static void BenchmarkSort()
        {
            throw new NotImplementedException();
        }

        private static void BenchmarkSearch()
        {
            Console.WriteLine("Benchmark degli algoritmi di ricerca...");

            // Abbiamo (algoritmi)
            // - ricerca sequenziale
            // - ricerca sequenziale ottimizzata
            // - ricerca binaria
            //
            // Abbiamo (casi)
            // - vettori di dimensioni diverse
            // - vettori ordinati e non
            // - vettori ordinati al contrario
            // - vettori parzialmente ordinati
            //
            // Abbiamo (unità di misura target) = ms

            int[][] cases = GenerateBenchmarkCases(1000);
            int[] sequentialSearchTimes = BenchmarkSequentialSearch(cases);

            int[][] sortedCases = GenerateSortedBenchmarkCases(1000);
            int[] optimizedSequentialSearchTimes = BenchmarkOptimizedSequentialSearch(sortedCases);

            PrintTimes("Ricerca sequenziale", sequentialSearchTimes);
            PrintTimes("Ricerca sequenziale ottimizzata", optimizedSequentialSearchTimes);
        }

        /// <summary>
        /// Stampa i tempi di un benchmark.
        /// </summary>
        /// <param name="name">Nome visualizzato del benchmark</param>
        /// <param name="times">I tempi da stampare</param>
        private static void PrintTimes(string name, int[] times)
        {
            Console.WriteLine($"{name} :");

            double average = times[0];
            for (int i = 1; i < times.Length; i++)
            {
                average += times[i];
            }
            average /= times.Length;

            double discardAvg = 0;
            for (int i = 0; i < times.Length; i++)
            {
                double discard = times[i] - average;
                discardAvg += discard * discard;
            }
            discardAvg = Math.Sqrt(discardAvg);

            Console.WriteLine($"Media (ms): {average}, Scarto quadratico medio (ms): {discardAvg}");
        }

        private static int[] BenchmarkBinarySearch(int[] cases)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Effetua un benchmark della ricerca sequenziale ottimizzata
        /// </summary>
        /// <param name="cases">I bench cases da usare</param>
        /// <returns>I tempi dei benchmark</returns>
        private static int[] BenchmarkOptimizedSequentialSearch(int[][] cases)
        {
            int[] times = new int[cases.Length];

            for (int i = 0; i < cases.Length; i++)
            {
                times[i] = BenchmarkOptimizedSequentialSearchCase(cases[i]);
            }

            return times;
        }

        /// <summary>
        /// Restituisce il tempo di benchmark per un singolo caso di 
        /// ricerca sequenziale ottimizzata.
        /// </summary>
        /// <param name="benchCase">il bench case</param>
        /// <returns>Il tempo del benchmark</returns>
        private static int BenchmarkOptimizedSequentialSearchCase(int[] benchCase)
        {
            DateTime start = DateTime.Now;

            int index = OptimizedSequentialSearch(benchCase, benchCase[benchCase.Length / 2]);
            if (index == -1)
            {
                Console.WriteLine("ERRORE IMPORTANTE");
            }

            DateTime end = DateTime.Now;

            return (int)(end - start).TotalMilliseconds;
        }

        /// <summary>
        /// Ricerca sequenziale ottimizzata che si ferma quando trova un valore maggiore
        /// (funziona solo su array ordinati)
        /// </summary>
        /// <param name="benchCase">L'array ordinato in cui cercare</param>
        /// <param name="value">Il valore da cercare</param>
        /// <returns>L'indice del valore trovato, -1 se non trovato</returns>
        private static int OptimizedSequentialSearch(int[] benchCase, int value)
        {
            for (int i = 0; i < benchCase.Length; i++)
            {
                if (benchCase[i] == value)
                {
                    return i;
                }
                if (benchCase[i] > value)
                {
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Effetua un benchmark della ricerca sequenziale
        /// </summary>
        /// <param name="cases">I bench cases da usare</param>
        /// <returns>I tempi dei benchmark</returns>
        private static int[] BenchmarkSequentialSearch(int[][] cases)
        {
            int[] times = new int[cases.Length];

            for (int i = 0; i < cases.Length; i++)
            {
                times[i] = BenchmarkSequentialSearchCase(cases[i]);
            }

            return times;
        }

        /// <summary>
        /// Restituisce il tempo di benchmark per un singolo caso di 
        /// ricerca sequenziale.
        /// </summary>
        /// <param name="benchCase">il bench case</param>
        /// <returns>Il tempo del benchmark</returns>
        private static int BenchmarkSequentialSearchCase(int[] benchCase)
        {
            DateTime start = DateTime.Now;

            // cerchiamo sempre l'elemento a metà, per comodità, ma si può cambiare
            int index = SequentialSearch(benchCase, benchCase[benchCase.Length / 2]);
            if (index == -1)
            {
                Console.WriteLine("ERRORE IMPORTANTE");
            }

            DateTime end = DateTime.Now;

            return (int)(end - start).TotalMilliseconds;
        }

        private static int SequentialSearch(int[] benchCase, int value)
        {
            for (int i = 0; i < benchCase.Length; i++)
            {
                if (benchCase[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Genera i vettori da usare in tutti i benchmark:
        /// - vettori di dimensioni diverse
        /// - vettori ordinati e non
        /// - vettori ordinati al contrario
        /// - vettori parzialmente ordinati
        /// </summary>
        /// <returns>I vettori generati</returns>
        private static int[][] GenerateBenchmarkCases(int n)
        {
            int[][] cases = new int[n][];

            for (int i = 0; i < n; i++)
            {
                cases[i] = GenerateRandomBenchmarkCase(i * 10 + 1);
            }

            return cases;
        }

        /// <summary>
        /// Genera vettori ordinati casuali da usare nei benchmark
        /// </summary>
        /// <param name="n">Numero di vettori da generare</param>
        /// <returns>I vettori ordinati generati</returns>
        private static int[][] GenerateSortedBenchmarkCases(int n)
        {
            int[][] cases = new int[n][];

            for (int i = 0; i < n; i++)
            {
                cases[i] = GenerateSortedRandomBenchmarkCase(i * 10 + 1);
            }

            return cases;
        }

        /// <summary>
        /// Genera un singolo case (un vettore casuale)
        /// </summary>
        /// <returns>Un vettore generato casualmente</returns>
        private static int[] GenerateRandomBenchmarkCase(int n)
        {
            Random rnd = new Random();
            int[] benchCase = new int[n];

            for (int i = 0; i < n; i++)
            {
                benchCase[i] = rnd.Next(0, 10000);
            }

            return benchCase;
        }

        /// <summary>
        /// Genera un singolo case ordinato (un vettore casuale ordinato con possibili ripetizioni)
        /// </summary>
        /// <param name="n">La dimensione del vettore</param>
        /// <returns>Un vettore generato casualmente e ordinato</returns>
        private static int[] GenerateSortedRandomBenchmarkCase(int n)
        {
            Random rnd = new Random();
            int[] benchCase = new int[n];

            for (int i = 0; i < n; i++)
            {
                benchCase[i] = rnd.Next(0, 10000);
            }

            Array.Sort(benchCase);

            return benchCase;
        }

        private static int Menu()
        {
            int choice;

            do
            {
                Console.WriteLine("1 -> Benchmark ricerca");
                Console.WriteLine("2 -> Benchmark ordinamento");

                choice = Convert.ToInt32(Console.ReadLine());
            }
            while (choice < 1 || choice > 2);

            return choice;
        }
    }
}
