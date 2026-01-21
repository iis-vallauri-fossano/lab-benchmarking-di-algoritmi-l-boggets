namespace lesson
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            int[][] orderedCases = GenerateOrderedBenchmarkCases(1000);

            int[] sequentialTimes = BenchmarkSequentialSearch(orderedCases);
            int[] optimizedSequentialTimes = BenchmarkOptimizedSequentialSearch(orderedCases);

            PrintTimes("Ricerca sequenziale", sequentialTimes);
            PrintTimes("Ricerca sequenziale ottimizzata", optimizedSequentialTimes);
        }

        private static void PrintTimes(string name, int[] times)
        {
            Console.WriteLine($"{name} :");

            double average = 0;
            for (int i = 0; i < times.Length; i++)
                average += times[i];
            average /= times.Length;

            double discardAvg = 0;
            for (int i = 0; i < times.Length; i++)
            {
                double d = times[i] - average;
                discardAvg += d * d;
            }
            discardAvg = Math.Sqrt(discardAvg);

            Console.WriteLine($"Media (ms): {average}, Scarto quadratico medio (ms): {discardAvg}\n");
        }

        private static int[] BenchmarkSequentialSearch(int[][] cases)
        {
            int[] times = new int[cases.Length];

            for (int i = 0; i < cases.Length; i++)
                times[i] = BenchmarkSequentialSearchCase(cases[i]);

            return times;
        }

        private static int[] BenchmarkOptimizedSequentialSearch(int[][] cases)
        {
            int[] times = new int[cases.Length];

            for (int i = 0; i < cases.Length; i++)
                times[i] = BenchmarkOptimizedSequentialSearchCase(cases[i]);

            return times;
        }

        private static int BenchmarkSequentialSearchCase(int[] benchCase)
        {
            DateTime start = DateTime.Now;

            int index = SequentialSearch(benchCase, benchCase[benchCase.Length / 2]);
            if (index == -1) Console.WriteLine("ERRORE");

            DateTime end = DateTime.Now;
            return (int)(end - start).TotalMilliseconds;
        }

        private static int BenchmarkOptimizedSequentialSearchCase(int[] benchCase)
        {
            DateTime start = DateTime.Now;

            int index = OptimizedSequentialSearch(benchCase, benchCase[benchCase.Length / 2]);
            if (index == -1) Console.WriteLine("ERRORE");

            DateTime end = DateTime.Now;
            return (int)(end - start).TotalMilliseconds;
        }

        private static int SequentialSearch(int[] v, int value)
        {
            for (int i = 0; i < v.Length; i++)
                if (v[i] == value)
                    return i;

            return -1;
        }

        private static int OptimizedSequentialSearch(int[] v, int value)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] == value)
                    return i;

                if (v[i] > value)
                    break;
            }
            return -1;
        }

        private static int[][] GenerateOrderedBenchmarkCases(int n)
        {
            int[][] cases = new int[n][];

            for (int i = 0; i < n; i++)
                cases[i] = GenerateOrderedBenchmarkCase(i * 10 + 1);

            return cases;
        }

        private static int[] GenerateOrderedBenchmarkCase(int n)
        {
            Random rnd = new Random();
            int[] v = new int[n];

            int current = rnd.Next(0, 10);
            for (int i = 0; i < n; i++)
            {
                current += rnd.Next(0, 3);
                v[i] = current;
            }

            return v;
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

