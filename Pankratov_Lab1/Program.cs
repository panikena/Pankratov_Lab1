using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pankratov_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Matrix(new double[][]{
                new double[]{ 1, 1, 1, 1, 0 }, 
                new double[]{ 0, 1, 1, 1, 0 },
                new double[]{ 0, 0, 1, 1, 0 },
                new double[]{ 0, 0, 1, 1, 0 },
                new double[]{ 1, 1, 1, 1, 1 }});

            m.Print();

            delimiter();

            m.Compare(true).Print();

            delimiter();

            foreach (var i in m.SumRows())
            {
                Console.Write("{0}\t", i);
            }

            delimiter();

            foreach (var i in m.Compare(true).SumRows())
            {
                Console.Write("{0}\t", i);
            }

            Console.Write("Sum m: {0}", m.Sum());

            Console.Write("Sum m compare: {0}", m.Compare(true).Sum());


            delimiter();

         

            new Matrix(new double[][] { m.SumRows() }).Normalize().Print();


            var experts = new Matrix(new double[][] {
                                    new double[] { 1, 3, 2, 6, 5, 4 },
                                    new double[] { 2, 4, 1, 5, 6, 3 }});

            delimiter();
            delimiter();
            delimiter();

            Console.WriteLine("Experts: ");

            foreach (var k in experts.ExpertPreferred())
            {
                Console.Write("{0:G4}\t", k);
            }

            Console.ReadKey();

        }


        static void delimiter()
        {
            Console.WriteLine("\n========================================");
        }
    }
}
