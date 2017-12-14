using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pankratov_Lab1
{
    public class Matrix
    {

        public double[] this[int i]
        {
            get
            {
                return MatrixArray[i];
            }
        }

        public double[][] MatrixArray { get; set; }

        public Matrix()
        {

        }

        public Matrix(int m, int n)
        {
            MatrixArray = new double[m][];

            for (int i = 0; i < m; i++)
            {
                MatrixArray[i] = new double[n];
            }
        }

        public Matrix(double[][] data)
        {
            MatrixArray = data;
        }

        public void Print()
        {
            foreach (var row in MatrixArray)
            {
                foreach (var cell in row)
                {
                    Console.Write(cell + "\t");
                }
                Console.WriteLine();
            }
        }

        public void Fill(double value)
        {
            for(int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j <  MatrixArray[i].Length; j++)
                {
                    MatrixArray[i][j] = value;
                }
            }
        }

        public double[] AlternativesFromComparisonMatrix()
        {
            var weights = new Matrix(new double[][] { SumRows() });
            NormalizeByRowSum(weights);

            return weights.MatrixArray[0];
        }


        public Matrix Compare(bool withEqual)
        {
            var result = new Matrix(MatrixArray.Length, MatrixArray[0].Length);
            int value = 1;

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    value = 1;
                    if (i != j)
                    {
                        var compare = MatrixArray[i][j].CompareTo(MatrixArray[j][i]);
                        if (withEqual)
                        {
                            value = compare > 0 ? 2 : (compare == 0 ? 1 : 0);

                        }
                        else
                        {
                            value = compare >= 0 ? 1 : 0;
                        }
                    }

                    result.MatrixArray[i][j] = value;
                }
            }

            return result;
        }

        public double[] SumRows()
        {
            double[] result = new double[MatrixArray.Length];

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                result[i] = 0;
                foreach (var cell in MatrixArray[i])
                {
                    result[i] += cell;
                }
            }

            return result;
        }


        public double[] SumColumns()
        {
            double[] result = new double[MatrixArray[0].Length];

            for (int i = 0, j = 0; i < MatrixArray.Length; i++, j = 0)
            {
                foreach (var cell in MatrixArray[i])
                {
                    result[j++] += cell;
                }
            }

            return result;
        }

        public double Sum()
        {
            double sum = 0;
            foreach (var row in MatrixArray)
            {
                foreach (var cell in row)
                {
                    sum += cell;
                }
            }

            return sum;
        }


        public static void NormalizeByRowSum(Matrix m)
        {
            var rowSums = m.SumRows();
            for (int i = 0; i < m.MatrixArray.Length; i++)
            {
                for (int j = 0; j < m.MatrixArray[i].Length; j++)
                {
                    m.MatrixArray[i][j] /= rowSums[i];
                }
            }
        }

        public void NormalizeByRowSum()
        {
            NormalizeByRowSum(this);
        }

        public Matrix Normalize()
        {
            var result = new Matrix(MatrixArray.Length, MatrixArray[0].Length);
            double sum = Sum();

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    result.MatrixArray[i][j] = MatrixArray[i][j] / sum;
                }
            }

            return result;
        }

        public static Matrix Normalize(Matrix m)
        {
            var sum = m.Sum();
            var result = new Matrix(m.MatrixArray.Length, m.MatrixArray[0].Length);

            for (int i = 0; i < m.MatrixArray.Length; i++)
            {
                for (int j = 0; j < m.MatrixArray[i].Length; j++)
                {
                    result.MatrixArray[i][j] = m.MatrixArray[i][j] / sum;
                }
            }

            return result;
        }

        public Matrix CalculateWeightenedExpertMatrix()
        {
            int alternatives = MatrixArray[0].Length;

            var modifiedMatrix = new Matrix(MatrixArray.Length, MatrixArray[0].Length);

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    modifiedMatrix.MatrixArray[i][j] = alternatives - MatrixArray[i][j];
                }
            }

            return modifiedMatrix;
        }

        public double[] ExpertPreferred()
        {

            var weightenedMatrix = CalculateWeightenedExpertMatrix();
            var columnSums = weightenedMatrix.SumColumns();
            var totalSum = weightenedMatrix.Sum();

            var result = new double[columnSums.Length];

            for (int i = 0; i < columnSums.Length; i++)
            {
                result[i] = columnSums[i] / totalSum;
            }

            return result;
        }

        public static double[] ExpertWithCompetenceStatic(Matrix m, double[] competence)
        {
            double totalCompetence = 0;
            foreach (var i in competence)
            {
                totalCompetence += i;
            }
            double[] weightenedCompetence = new double[competence.Length];
            for(int i = 0; i < competence.Length; i++)
            {
                weightenedCompetence[i] = competence[i] / totalCompetence;
            }

            var result = new double[m.MatrixArray[0].Length];

            for (int i = 0; i < m.MatrixArray.Length; i++)
            {
                for (int j = 0; j < m.MatrixArray[i].Length; j++)
                {
                    result[j] += weightenedCompetence[i] * m.MatrixArray[i][j];
                }
            }

            return result;
        }

        public double[] ExpertWithCompetence(double[] competence)
        {
            return ExpertWithCompetenceStatic(this, competence);
        }

        public double[] RankMethod()
        {
            var rowSums = SumRows();
        
            NormalizeByRowSum();

            var weightenedResult = new double[MatrixArray[0].Length];

            for (int j = 0; j < MatrixArray[0].Length; j++)
            {
                foreach(var row in MatrixArray)
                {
                    weightenedResult[j] += row[j];
                }
                weightenedResult[j] /= MatrixArray.Length;
                
            }

            return weightenedResult;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var row in MatrixArray)
            {
                foreach (var cell in row)
                {
                    sb.AppendFormat("{0:G4}\t", cell);
                }
                sb.AppendLine();
            }

            return sb.ToString();

        }

    }
}
