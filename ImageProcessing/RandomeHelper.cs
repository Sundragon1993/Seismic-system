using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class RandomeHelper
    {
        private static float _xMin = 0f, _xMax = 0f;
        public static int[] RandomWithBorder(int border, int size)
        {
            var arrayAfter = new int[size];
            var ranArray = BuildRandomNativeValues(size);
            _xMax = (float)ranArray.Max();
            _xMin = (float)ranArray.Min();
            for (int i = 0; i < size; i++)
            {
                arrayAfter[i] = _functionForSpikeFormulaV1(ranArray[i], border);
            }
            return arrayAfter;
        }
        public static int[] BuildRandomNativeValues(int size)
        {
            Random a = new Random();
            var randomArray = new int[size];
            for (int i = 0; i < size; i++)
            {
                randomArray[i] = a.Next();
            }
            return randomArray;
        }
        private static int _functionForSpikeFormulaV1(double i, int S)
        {
            double y = 2 * ((i - _xMin) / (_xMax - _xMin) - 0.5) * S;
            return ((int)y);
        }
        public static double[] Spikes(double p1, int p2, double p3, double p4, double[] yArray)
        {
            Random a = new Random();
            var yArrayLength = yArray.Length;
            var number = Math.Ceiling(p1 * a.NextDouble() * yArrayLength);
            var intervalLen = yArrayLength / (number + 1); // point + 1 = segment numbers
            double[] zeroArray = new double[yArrayLength];
            for (int i = 0; i < zeroArray.Length; i++)
            {
                zeroArray[i] = 0;
            }

            for (int i = 1; i < number; i++)
            {
                var position = (int)Math.Ceiling(i * intervalLen + (a.NextDouble() - 0.5) * 2 * p2);
                if (position < 1) position = 1;
                else if (position > yArrayLength) position = yArrayLength;
                var spike_sign = a.NextDouble();
                if (spike_sign > 0.5) spike_sign = 1;
                else
                {
                    spike_sign = -1;
                }
                zeroArray[position] = (float)(spike_sign * p3 + (a.NextDouble() - 0.5) * 2 * p4);
            }
            return zeroArray;
        }

    }
}
