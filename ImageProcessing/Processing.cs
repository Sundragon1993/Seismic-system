using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    class Processing
    {
        int minR, maxR, minB, maxB, minG, maxG, R, G, B;
        int[] histogramR = new int[256];
        int[] histogramG = new int[256];
        int[] histogramB = new int[256];
        int[] chistogramR = new int[256];
        int[] chistogramG = new int[256];
        int[] chistogramB = new int[256];

        #region Contrast Stretching
        public Bitmap contrast_stretching(Bitmap resim)
        {
            minR = minG = minB = 255;
            maxR = maxG = maxB = 0;
            Color c;
            for (int y = 0; y < resim.Height; y++)
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    /*resimde gerekli olan max ve min degerlerini buluyoruz..
                    Histogram degerlerini artırıyoruz..
                    */
                    c = resim.GetPixel(x, y);
                    histogramR[c.R]++;
                    minR = minR > c.R ? c.R : minR;
                    maxR = maxR > c.R ? maxR : c.R;
                    histogramG[c.G]++;
                    minG = minG > c.G ? c.G : minG;
                    maxG = maxG > c.G ? maxG : c.G;
                    histogramB[c.B]++;
                    minB = minB > c.B ? c.R : minB;
                    maxB = maxB > c.B ? maxR : c.B;
                }
            }

            Color p;
            for (int y = 0; y < resim.Height; y++)
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    /*
                    Formul=> s=(r-min(f(x,y)))/max(f(x,y))-min(f(x,y))
                    */
                    p = resim.GetPixel(x, y);
                    R = (int)((double)(p.R - minR) / (maxR - minR) * 255);
                    G = (int)((double)(p.G - minG) / (maxG - minG) * 255);
                    B = (int)((double)(p.B - minB) / (maxB - minB) * 255);
                    resim.SetPixel(x, y, Color.FromArgb(p.A, R, G, B));
                }
            }

            return resim;
        }

        #endregion
        //public int[] GetHistogram(Rgb component)
        //{
        //    var histogram = new int[256];

        //    lock (_imageLock)
        //    {
        //        var data = OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //        var offset = data.Stride - OriginalImage.Width * 3;

        //        var p = (byte*)data.Scan0.ToPointer();

        //        for (var i = 0; i < OriginalImage.Height; i++)
        //        {
        //            for (var j = 0; j < OriginalImage.Width; j++, p += 3)
        //            {
        //                switch (component)
        //                {
        //                    case Rgb.R:
        //                        histogram[p[2]]++;
        //                        break;
        //                    case Rgb.G:
        //                        histogram[p[1]]++;
        //                        break;
        //                    default:
        //                        histogram[p[0]]++;
        //                        break;
        //                }
        //            }
        //            p += offset;
        //        }

        //        OriginalImage.UnlockBits(data);
        //    }

        //    return histogram;
        //}
        //public Bitmap EqualizeHistogram(Image OriginalImage)
        //{
        //    var finalImg = new Bitmap(OriginalImage.Width, OriginalImage.Height);

        //    var rHistogram = GetHistogram(Rgb.R);
        //    var gHistogram = GetHistogram(Rgb.G);
        //    var bHistogram = GetHistogram(Rgb.B);

        //    var data = OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        //    var finalData = finalImg.LockBits(new Rectangle(0, 0, finalImg.Width, finalImg.Height),
        //         ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    var histR = new float[256];
        //    var histG = new float[256];
        //    var histB = new float[256];

        //    histR[0] = (rHistogram[0] * rHistogram.Length) / (finalData.Width * finalData.Height);
        //    histG[0] = (gHistogram[0] * gHistogram.Length) / (finalData.Width * finalData.Height);
        //    histB[0] = (bHistogram[0] * bHistogram.Length) / (finalData.Width * finalData.Height);

        //    long cumulativeR = rHistogram[0];
        //    long cumulativeG = gHistogram[0];
        //    long cumulativeB = bHistogram[0];

        //    for (var i = 1; i < histR.Length; i++)
        //    {
        //        cumulativeR += rHistogram[i];
        //        histR[i] = (cumulativeR * rHistogram.Length) / (finalData.Width * finalData.Height);

        //        cumulativeG += gHistogram[i];
        //        histG[i] = (cumulativeG * gHistogram.Length) / (finalData.Width * finalData.Height);

        //        cumulativeB += bHistogram[i];
        //        histB[i] = (cumulativeB * bHistogram.Length) / (finalData.Width * finalData.Height);
        //    }

        //    var ptr = (byte*)data.Scan0;
        //    var ptrFinal = (byte*)finalData.Scan0;

        //    var remain = data.Stride - data.Width * 3;

        //    for (var i = 0; i < data.Height; i++, ptr += remain, ptrFinal += remain)
        //    {
        //        for (var j = 0; j < data.Width; j++, ptrFinal += 3, ptr += 3)
        //        {
        //            var intensityR = ptr[2];
        //            var intensityG = ptr[1];
        //            var intensityB = ptr[0];

        //            var nValueR = (byte)histR[intensityR];
        //            var nValueG = (byte)histG[intensityG];
        //            var nValueB = (byte)histB[intensityB];

        //            if (histR[intensityR] < 255)
        //                nValueR = 255;
        //            if (histG[intensityG] < 255)
        //                nValueG = 255;
        //            if (histB[intensityB] < 255)
        //                nValueB = 255;

        //            ptrFinal[2] = nValueR;
        //            ptrFinal[1] = nValueG;
        //            ptrFinal[0] = nValueB;
        //        }
        //    }

        //    OriginalImage.UnlockBits(data);
        //    finalImg.UnlockBits(finalData);

        //    return finalImg;
        //}


        #region Histogram equalization
        public Bitmap histogram_equalization(Bitmap resim)
        {
            Color p;
            Bitmap his = new Bitmap(resim.Width, resim.Height, PixelFormat.Format32bppArgb);

            for (int y = 0; y < resim.Height; y++)
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    p = resim.GetPixel(x, y);
                    histogramR[p.R]++;
                    histogramB[p.B]++;
                    histogramG[p.G]++;
                }
            }
            chistogramR[0] = histogramR[0];
            chistogramG[0] = histogramG[0];
            chistogramB[0] = histogramB[0];

            for (int i = 1; i < 256; i++)
            {
                chistogramR[i] = histogramR[i] + chistogramR[i - 1];
                chistogramG[i] = histogramG[i] + chistogramG[i - 1];
                chistogramB[i] = histogramB[i] + chistogramB[i - 1];
            }
            for (int y = 0; y < resim.Height; y++)
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    p = resim.GetPixel(x, y);
                    int r = (int)((double)(chistogramR[p.R] * 255.0 / (resim.Height * resim.Width)));
                    int g = (int)((double)(chistogramG[p.G] * 255.0 / (resim.Height * resim.Width)));
                    int b = (int)((double)(chistogramB[p.B] * 255.0 / (resim.Height * resim.Width)));
                    his.SetPixel(x, y, Color.FromArgb(p.A, r, g, b));
                }
            }

            return his;
        }

        #endregion

        #region Gri Dönüşüm
        public Bitmap gri_donusum(Bitmap resim)
        {
            Color p;
            for (int y = 0; y < resim.Height; y++)//bu iki for'da resimin tüm pixellerini geziyoruz..
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    p = resim.GetPixel(x, y);// resimdeki pixelleri alıyoruz..
                    int gri = (p.R + p.G + p.B) / 3;//Her piksel değerlerinin aritmetik ortalamasını alıyoruz..
                    resim.SetPixel(x, y, Color.FromArgb(p.A, gri, gri, gri));
                }
            }
            return resim;
        }

        #endregion

        #region Negative image
        public Bitmap negatif_donusum(Bitmap resim)
        {
            Color p;
            /*
                 s = (L – 1) – r
                 s = 255 – r
                 L=>2^n-1
                 r=>Pixel değeri
           */
            for (int y = 0; y < resim.Height; y++)//bu iki for'da resimin tüm pixellerini geziyoruz..
            {
                for (int x = 0; x < resim.Width; x++)
                {
                    p = resim.GetPixel(x, y);// resimdeki pixelleri alıyoruz..
                    resim.SetPixel(x, y, Color.FromArgb(255 - p.R, 255 - p.G, 255 - p.B));//resime yeni  degerleri yüklüyoruz..
                }
            }
            return resim;
        }
        #endregion

        #region Logarit transform
        public Bitmap Logarit_transform(Bitmap resim, int constant /*double RR, double RG, double RB*/)
        {
            Bitmap log = new Bitmap(resim.Width,resim.Height,PixelFormat.Format32bppArgb);
            /*
                        s = c log(r + 1) 
                        c = 255/log(1+r)
                        s=>yeni pixel değeri
                        c=>Dönüşüm katsayısı
                        RR=>Max R
                        RG=>Max G
                        RB=>Max B
            */
            for (int y = 0; y < resim.Height; y++)
            {
                for (int x = 0; x < resim.Width; x++)
                {

                    Color renk = resim.GetPixel(x, y);
                    int sR = (int)(constant * Math.Log10(1 + renk.R));
                    int sG = (int)(constant * Math.Log10(1 + renk.G));
                    int sB = (int)(constant * Math.Log10(1 + renk.B));
                    log.SetPixel(x, y, Color.FromArgb(renk.A, sR, sG, sB));
                }
            }
            return log;
        }

        #endregion

        #region Gamma Correction
        public Bitmap Gamma_Correction(Bitmap resim, double valueGamma)
        {
            //Bitmap gamma = (Bitmap)resim.Clone();
            Bitmap gamma = new Bitmap(resim.Width, resim.Height, PixelFormat.Format32bppArgb);

            /*
                s = c*(r/255)^gamma
            */

            for (int y = 0; y < gamma.Height; y++)
            {
                for (int x = 0; x < gamma.Width; x++)
                {
                    Color renk = resim.GetPixel(x, y);
                    double gd = ((double)valueGamma / 1);
                    int sR = (int)(255 * Math.Pow((renk.R / 255.0), gd));
                    int sG = (int)(255 * Math.Pow((renk.G / 255.0), gd));
                    int sB = (int)(255 * Math.Pow((renk.B / 255.0), gd));
                    gamma.SetPixel(x, y, Color.FromArgb(renk.A, sR, sG, sB));
                }
            }

            return gamma;
        }

        #endregion

    }
}
