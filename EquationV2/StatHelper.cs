using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Statistics;
namespace EquationV2
{
    public static class StatHelper
    {
        
        public static List<int> Bucketize(this IEnumerable<double> source, int totalBuckets)
        {
            var min = source.Min();
            var max = source.Max();
            var buckets = new List<int>(new int[totalBuckets]);
            var bucketSize = (max - min) / totalBuckets;
            foreach (var value in source)
            {
                int bucketIndex = 0;
                if (bucketSize > 0.0)
                {
                    bucketIndex = (int)((value - min) / bucketSize);
                    if (bucketIndex == totalBuckets)
                    {
                        bucketIndex--;
                    }
                }
                buckets[bucketIndex] += 1;
            }

            return buckets;
        }
        public static double[] AutoCorrelationV3(double[] x, int kMin, int kMax)
        {
            var xLen = x.Length;
            double sum = 0;
            for (int i = 0; i < xLen; i++)
            {
                sum += x[i];
            }
            var xMean = sum / xLen;
            var autoCorrelationArray = new double[kMax];
            double variance = 0;
            for (int f = 0; f < xLen; f++)
            {
                variance += (x[f] - xMean) * (x[f] - xMean);
            }
            for (int l = 0; l < xLen; l++)
            {
                double n = 0;
                for (int k = 0; k < xLen - l; k++)
                {
                    var xim = x[k] - xMean;
                    n += xim * (x[k + l] - xMean);
                }
                autoCorrelationArray[l] = n / variance;
            }

            return autoCorrelationArray;
        }
        public static double[] CrossCorrelation(int n, double[] x, double[] y, int mindelay, int maxdelay)
        {
            int i, j;
            double mx, my, sx, sy, sxy, denom, r;
            double[] crossArray = new double[n];
            /* Calculate the mean of the two series x[], y[] */
            mx = 0;
            my = 0;
            for (i = 0; i < n; i++)
            {
                mx += x[i];
                my += y[i];
            }
            mx /= n;
            my /= n;

            /* Calculate the denominator */
            sx = 0;
            sy = 0;
            for (i = 0; i < n; i++)
            {
                sx += (x[i] - mx) * (x[i] - mx);
                sy += (y[i] - my) * (y[i] - my);
            }
            denom = Math.Sqrt(sx * sy);

            int index = 0;
            /* Calculate the correlation series */
            for (var delay = -maxdelay; delay < maxdelay; delay++)
            {
                sxy = 0;
                for (i = 0; i < n; i++)
                {
                    j = i + delay;
                    if (j < 0 || j >= n)
                        continue;
                    else
                        sxy += (x[i] - mx) * (y[j] - my);
                    /* Or should it be (?)
                    if (j < 0 || j >= n)
                       sxy += (x[i] - mx) * (-my);
                    else
                       sxy += (x[i] - mx) * (y[j] - my);
                    */
                }
                r = sxy / denom;
                crossArray[index++] = r;
                /* r is the correlation coefficient at "delay" */
            }
            return crossArray;
        }


        public static LagCorr CrossCorrelation(double[] x1, double[] x2)
        {
            if (x1.Length != x2.Length)
                throw new Exception("Samples must have same size.");

            var len = x1.Length;
            var len2 = 2 * len;
            var len3 = 3 * len;
            var s1 = new double[len3];
            var s2 = new double[len3];
            var cor = new double[len2];
            var lag = new double[len2];

            Array.Copy(x1, 0, s1, len, len);
            Array.Copy(x2, 0, s2, 0, len);

            for (int i = 0; i < len2; i++)
            {
                //cor[i] = Correlation.Pearson(s1, s2);
                cor[i] = MathNet.Numerics.Statistics.Correlation.Pearson(s1, s2);
                lag[i] = i - len;
                Array.Copy(s2, 0, s2, 1, s2.Length - 1);
                s2[0] = 0;
            }

            return new LagCorr { Corr = cor, Lag = lag };
        }
        //Caculate the output array using convolution method, kernel is wavelet/filter array,
        //trim is boolean variable indicates the output array should be trimed or not
        public static double[] Convolve(this double[] a, double[] kernel, bool trim)
        {
            double[] result;
            int m = (int)System.Math.Ceiling(kernel.Length / 2.0);
            if (trim)
            {
                result = new double[a.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = 0;
                    for (int j = 0; j < kernel.Length; j++)
                    {
                        int k = i - j + m - 1;
                        if (k >= 0 && k < a.Length)
                            result[i] += a[k] * kernel[j];
                    }
                }
            }
            else
            {
                result = new double[a.Length + m];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = 0;
                    for (int j = 0; j < kernel.Length; j++)
                    {
                        int k = i - j;
                        if (k >= 0 && k < a.Length)
                            result[i] += a[k] * kernel[j];
                    }
                }
            }
            return result;
        }
    }
    public class LagCorr
    {
        public double[] Lag { get; set; }
        public double[] Corr { get; set; }
    }
}
