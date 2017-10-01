using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Palindromes
{
    class Program
    {
        public const int OPERATIONS_PER_THREAD = 10000;
        static void Main(string[] args)
        {
            Console.WriteLine(Palindrome.FromNumeric(26616).Run(Palindrome.RunMode.Verbose));
            Console.WriteLine("End of tests, starting algorithm...\n\n");
            RunMany();
            Console.ReadLine();
        }

        private static void RunMany()
        {
            object locker = new object();
            var csv = new StringBuilder();
            Parallel.For(0, Environment.ProcessorCount, i =>
            {
                for (int x = i * OPERATIONS_PER_THREAD; x < (i + 1) * OPERATIONS_PER_THREAD; x++)
                {
                    Palindrome p = Palindrome.FromNumeric(x);
                    int result = p.Run();
                    if (result > 40)
                    {
                        Console.WriteLine("Input: {0}; Steps: {1}; Final palindrome: {2}", x, p.StepsTaken, p.Outcome);
                        lock (locker)
                        {
                            csv.Append($"{x},{p.StepsTaken},{p.Outcome}\n");
                        }
                    }
                }
            });

            File.WriteAllText("results.csv", csv.ToString());
        }
    }
}
