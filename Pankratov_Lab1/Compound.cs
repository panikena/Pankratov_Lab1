using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pankratov_Lab1
{
    public class Compound : Matrix
    {
        public Compound(double[][] data) : base(data)
        {
            
        }

        public double[] V
        {
            get
            {
                return MatrixArray[0];
            }
        }

        public int[] P
        {
            get
            {
                var intArr = MatrixArray[1].Select(x => Convert.ToInt32(x));

                return intArr.ToArray();
            }
        }

    }
}
