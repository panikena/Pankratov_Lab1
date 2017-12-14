using System;
using System.Collections;
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
            //  Test();
            Task();

            Console.ReadKey();
        }

        static void Task()
        {
            // int K = 4;
            int P = 13;

            var lambda = new double[] { 0.3, 0.3, 0.4 };
            var M = new int[] { 3, 3, 2 };


            //var K1 = new Matrix(new double[][] {
            //    new double[] { 0.2, 0.3, 0.5},
            //    new double[] { 2, 3, 5}
            //});
            //var K2 = new Matrix(new double[][] {
            //    new double[] { 0.25, 0.35, 0.4},
            //    new double[] { 2, 4, 5}
            //});
            //var K3 = new Matrix(new double[][] {
            //    new double[] { 0.6, 0.4},
            //    new double[] { 8, 6}
            //});



            Console.WriteLine("\n========TASK========");
            #region Data

            //int P = 35;

            //var lambda = new double[] { 0.2, 0.2, 0.3, 0.3 };
            //var M = new int[] { 7, 9, 6, 6 };

            //var matrixCouple = new Matrix(new double[][] {
            //                    new double[] { 1, 1, 1, 2, 2, 2, 2},
            //                    new double[] { 1, 1, 1, 1, 2, 2, 2},
            //                    new double[] { 1, 1, 1, 2, 1, 2, 2},
            //                    new double[] { 0, 1, 1, 1, 2, 2, 2},
            //                    new double[] { 0, 0, 1, 1, 1, 2, 2},
            //                    new double[] { 0, 0, 0, 0, 0, 1, 1},
            //                    new double[] { 0, 0, 0, 0, 0, 1, 1},
            //});


            //var M1 = matrixCouple.AlternativesFromComparisonMatrix();

            //var K1 = new Compound(new double[][] {
            //    M1,
            //    new double[] { 10, 9, 8, 4, 3, 2, 2}
            //});

            //Console.WriteLine("Comparison matrix");
            //PrintArray(M1);

            //var expertPreferred = new Matrix(new double[][] {
            //                    new double[] { 8, 5, 7, 9, 3, 4, 2, 6, 1},
            //                    new double[] { 9, 4, 6, 8, 3, 5, 1, 7, 2},
            //                    new double[] { 8, 5, 6, 9, 3, 4, 2, 7, 2},
            //});

            //var M2 = expertPreferred.ExpertPreferred();

            //var K2 = new Compound(new double[][] {
            //    M2,
            //    new double[] { 11, 10, 14, 15, 6, 5 ,4, 6, 3}
            //});

            //Console.WriteLine("Expert Preferred");
            //PrintArray(M2);

            //var competence = new double[] { 11, 12.7, 8 };

            //var competentExperts = new Matrix(new double[][]{
            //                    new double[] { 0.078, 0.179, 0.109, 0.021, 0.381, 0.232},
            //                    new double[] { 0.078, 0.167, 0.082, 0.015, 0.415, 0.243},
            //                    new double[] { 0.044, 0.179, 0.148, 0, 0.419, 0.224},
            //    });


            //var M3 = competentExperts.ExpertWithCompetence(competence);
            //Console.WriteLine("Expert with competence");

            //PrintArray(M3);

            //var K3 = new Compound(new double[][] {
            //    M3,
            //    new double[] { 9, 16, 12, 10, 21, 19}
            //});


            //var rankMatrix = new Matrix(new double[][] {
            //                    new double[] { 9, 7, 10, 3, 4, 5},
            //                    new double[] { 7, 5, 8, 5, 3, 6},
            //                    new double[] { 10, 6, 9, 4, 5, 3}
            //});

            //var M4 = rankMatrix.RankMethod();
            //Console.WriteLine("Rank Method");

            //PrintArray(M4);

            //var K4 = new Compound(new double[][] {
            //    M4,
            //    new double[] { 6, 5, 6, 3, 4, 4}
            //});


            //var KList = new List<Compound>() { K1, K2, K3, K4 };

            #endregion Data


            var K1 = new Compound(new double[][] {
                new double[] { 0.2, 0.3, 0.5},
                new double[] { 2, 3, 5}
            });
            var K2 = new Compound(new double[][] {
                new double[] { 0.25, 0.35, 0.4},
                new double[] { 2, 4, 5}
            });
            var K3 = new Compound(new double[][] {
                new double[] { 0.6, 0.4},
                new double[] { 8, 6}
            });

            var KList = new List<Compound>() { K1, K2, K3 };
            int K = KList.Count;

            var O = new Matrix(K, P);
            O.Fill(-1);
            var S = new Matrix(K, P);
            S.Fill(-1);


            DynamicProgramming(KList, P, M, lambda, O, S);



        }

        public static void CountFirstIteration(IList<Compound> K, int P, int[] M, double[] lambda, Matrix O, Matrix S)
        {
            int k = 0;

            for (int i = 0; i < M.Length; i++)
            {
                var o = K[k].V[i] * lambda[k];
                var p = K[k].P[i];
                //less or equal
                var leP = p <= P;
                if (o > O[k][p - 1] && leP)
                {
                    O[k][p - 1] = o;
                    S[k][p - 1] = i + 1;
                }
            }
        }

        public static void AnalyzePairs(IList<Compound> K, int P, int[] M, double[] lambda, Matrix O, Matrix S)
        {
            var nonNullIndexes = new List<int>(O[0].Where(x => x > 0).Count());

            for (int i = 0; i < O[0].Length; i++)
            {
                if (O[0][i] > 0)
                {
                    nonNullIndexes.Add(i);
                }
            }

            int kInd = 1;

            for (int aInd = 0; aInd < K[kInd].V.Length; aInd++)
            {
                for (int i = 0; i < nonNullIndexes.Count; i++)
                {
                    //index in O
                    var m = aInd;
                    var nnInd = nonNullIndexes[i];
                    
                    //get p value of first compound
                    var p = K[0].P[i];
                    //get value of O matrix for 1st compound
                    var oVal = O[0][p - 1];

                    var x = oVal + lambda[kInd] * K[kInd].V[m];

                    var pSum = p + K[kInd].P[m];
                    //sum is less than P
                    var leP = pSum <= P;

                    var newO = O[kInd][pSum];
                    //bigger than O value
                    var btO = x > newO;

                    if (leP && btO)
                    {
                        O[kInd][pSum - 1] = x;
                        S[kInd][pSum - 1] = 1;
                    }
                }
            }





        }


        public static void DynamicProgramming(IList<Compound> K, int P, int[] M, double[] lambda, Matrix O, Matrix S)
        {
            CountFirstIteration(K, P, M, lambda, O, S);

            AnalyzePairs(K, P, M, lambda, O, S);
        }


        static void Test()
        {
            var m = new Matrix(new double[][]{
                new double[]{ 1, 1, 1, 1, 0 },
                new double[]{ 0, 1, 1, 1, 0 },
                new double[]{ 0, 0, 1, 1, 0 },
                new double[]{ 0, 0, 1, 1, 0 },
                new double[]{ 1, 1, 1, 1, 1 }});



            Console.WriteLine("Comparison==================: ");
            foreach (var k in m.AlternativesFromComparisonMatrix())
            {
                Console.Write("{0:G4}\t", k);
            }


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



            var competence = new double[] { 4.5, 8 };
            var expertsWithCompetence = new Matrix(new double[][] {
                                                new double[] { 0.5, 0, 0.33, 0.17},
                                                new double[] { 0.54, 0.09, 0.2, 0.17}
            });

            delimiter();

            foreach (var i in expertsWithCompetence.ExpertWithCompetence(competence))
            {
                Console.Write("{0:G4}\t", i);
            }




            delimiter();
            delimiter();
            delimiter();


            var rankMatrix = new Matrix(new double[][] {
                new double[] { 10, 7, 9, 3, 4, 5 },
                new double[] { 8, 6, 10, 4, 2, 7}
            });


            var rankResult = rankMatrix.RankMethod();

            Console.WriteLine("\n==============RANK METHOD=======================");
            foreach (var i in rankResult)
            {
                Console.Write("{0:G4}\t", i);
            }


        }

        static void PrintArray(IEnumerable<double> array)
        {
            foreach (var i in array)
            {
                Console.Write("{0:G4}\t", i);
            }
            Console.WriteLine();
        }

        static void delimiter()
        {
            Console.WriteLine("\n========================================");
        }
    }
}
