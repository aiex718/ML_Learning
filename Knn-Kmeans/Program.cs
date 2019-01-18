using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Demo select, K[m]eans ,K[n]n or [q]uit? [m/n/q]");
                var input = Console.ReadLine().ToLower();

                if (input.Contains('m'))
                {
                    ProgramKmeans.Kmeans();
                    break;
                }
                else if (input.Contains('n'))
                {
                    ProgramKnn.Knn();
                    break;
                }
                else if(input.Contains('q'))
                {
                    break;
                }
            }
        }

    }
}
