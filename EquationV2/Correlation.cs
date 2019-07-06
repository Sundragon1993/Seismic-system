using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Statistics;

namespace EquationV2
{
    public static class Correlation
    {
        /// <summary>
        /// Auto-correlation function (ACF) based on FFT for all possible lags k.
        /// </summary>
        /// <param name="x">Data array to calculate auto correlation for.</param>
        /// <returns>An array with the ACF as a function of the lags k.</returns>
        public static double[] Auto(double[] x)
        {
            return AutoCorrelationFft(x, 0, x.Length - 1);
        }

        /// <summary>
        /// Auto-correlation function (ACF) based on FFT for lags between kMin and kMax.
        /// </summary>
        /// <param name="x">The data array to calculate auto correlation for.</param>
        /// <param name="kMax">Max lag to calculate ACF for must be positive and smaller than x.Length.</param>
        /// <param name="kMin">Min lag to calculate ACF for (0 = no shift with acf=1) must be zero or positive and smaller than x.Length.</param>
        /// <returns>An array with the ACF as a function of the lags k.</returns>
        public static double[] Auto(double[] x, int kMax, int kMin = 0)
        {
            // assert max and min in proper order
            var kMax2 = Math.Max(kMax, kMin);
            var kMin2 = Math.Min(kMax, kMin);

            return AutoCorrelationFft(x, kMin2, kMax2);
        }

        /// <summary>
        /// Auto-correlation function based on FFT for lags k.
        /// </summary>
        /// <param name="x">The data array to calculate auto correlation for.</param>
        /// <param name="k">Array with lags to calculate ACF for.</param>
        /// <returns>An array with the ACF as a function of the lags k.</returns>
        public static double[] Auto(double[] x, int[] k)
        {
            if (k == null)
            {
                throw new ArgumentNullException(nameof(k));
            }

            if (k.Length < 1)
            {
                throw new ArgumentException("k");
            }

            var kMin = k.Min();
            var kMax = k.Max();

            // get acf between full range
            var acf = AutoCorrelationFft(x, kMin, kMax);

            // map output by indexing
            var result = new double[k.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = acf[k[i] - kMin];
            }

            return result;
        }

        private static double[] AutoCorrelationFft(double[] x, int kLow, int kHigh)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            int N = x.Length;    // Sample size

            if (kLow < 0 || kLow >= N)
                throw new ArgumentOutOfRangeException(nameof(kLow), "kMin must be zero or positive and smaller than x.Length");
            if (kHigh < 0 || kHigh >= N)
                throw new ArgumentOutOfRangeException(nameof(kHigh), "kMax must be positive and smaller than x.Length");
            if (N < 1)
                return new double[0];

            int nFFT = Euclid.CeilingToPowerOfTwo(N) * 2;

            Complex[] xFFT = new Complex[nFFT];
            Complex[] xFFT2 = new Complex[nFFT];

            double xDash = ArrayStatistics.Mean(x);
            double xArrNow = 0.0d;

            // copy values in range and substract mean - all the remaining parts are padded with zero.
            for (int i = 0; i < x.Length; i++)
            {
                xFFT[i] = new Complex(x[i] - xDash, 0.0);    // copy values in range and substract mean
            }

            Fourier.Forward(xFFT, FourierOptions.Matlab);

            // maybe a Vector<Complex> implementation here would be faster
            for (int i = 0; i < xFFT.Length; i++)
            {
                xFFT2[i] = Complex.Multiply(xFFT[i], Complex.Conjugate(xFFT[i]));
            }

            Fourier.Inverse(xFFT2, FourierOptions.Matlab);

            double dc = xFFT2[0].Real;

            double[] result = new double[kHigh - kLow + 1];

            // normalize such that acf[0] would be 1.0
            for (int i = 0; i < (kHigh - kLow + 1); i++)
            {
                result[i] = xFFT2[kLow + i].Real / dc;
            }

            return result;
        }

    }
}
