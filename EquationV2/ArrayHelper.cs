using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationV2
{
    public static class ArrayHelper
    {
        public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        public static double RowSum(this double[,] value, int index)
        {
            double result = 0;
            for (int i = 0; i <= value.GetUpperBound(1); i++)
            {
                result += value[index, i];
            }
            return result;
        }

        public static double ColumnSum(this double[,] value, int index)
        {
            double result = 0;
            for (int i = 0; i <= value.GetUpperBound(0); i++)
            {
                result += value[i, index];
            }
            return result;
        }
    }
}
