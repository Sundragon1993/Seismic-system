using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using EquationV2;
using MathNet.Numerics.IntegralTransforms;

namespace ImageProcessing
{
    public static class ImageHelper
    {
        public static double meanValue;
        public static double[,] Laplacian3x3
        {
            get
            {
                return new double[,]
                { { -1, -1, -1, },
                    { -1,  8, -1, },
                    { -1, -1, -1, }, };
            }
        }
        public static double[,] Laplacian5x5
        {
            get
            {
                return new double[,]
                { { -1, -1, -1, -1, -1, },
                    { -1, -1, -1, -1, -1, },
                    { -1, -1, 24, -1, -1, },
                    { -1, -1, -1, -1, -1, },
                    { -1, -1, -1, -1, -1  } };
            }
        }
        public static Bitmap DilateAndErodeFilter(this Bitmap sourceBitmap,
                                        int matrixSize,
                                        MorphologyType morphType,
                                        bool applyBlue = true,
                                        bool applyGreen = true,
                                        bool applyRed = true,
                                        MorphologyEdgeType edgeType =
                                        MorphologyEdgeType.None)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;


            int blue = 0;
            int green = 0;
            int red = 0;


            byte morphResetValue = 0;


            if (morphType == MorphologyType.Erosion)
            {
                morphResetValue = 255;
            }


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    blue = morphResetValue;
                    green = morphResetValue;
                    red = morphResetValue;


                    if (morphType == MorphologyType.Dilation)
                    {
                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset;
                                filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset +
                                             (filterX * 4) +
                                (filterY * sourceData.Stride);


                                if (pixelBuffer[calcOffset] > blue)
                                {
                                    blue = pixelBuffer[calcOffset];
                                }


                                if (pixelBuffer[calcOffset + 1] > green)
                                {
                                    green = pixelBuffer[calcOffset + 1];
                                }


                                if (pixelBuffer[calcOffset + 2] > red)
                                {
                                    red = pixelBuffer[calcOffset + 2];
                                }
                            }
                        }
                    }
                    else if (morphType == MorphologyType.Erosion)
                    {
                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset;
                                filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset +
                                             (filterX * 4) +
                                (filterY * sourceData.Stride);


                                if (pixelBuffer[calcOffset] < blue)
                                {
                                    blue = pixelBuffer[calcOffset];
                                }


                                if (pixelBuffer[calcOffset + 1] < green)
                                {
                                    green = pixelBuffer[calcOffset + 1];
                                }


                                if (pixelBuffer[calcOffset + 2] < red)
                                {
                                    red = pixelBuffer[calcOffset + 2];
                                }
                            }
                        }
                    }


                    if (applyBlue == false)
                    {
                        blue = pixelBuffer[byteOffset];
                    }


                    if (applyGreen == false)
                    {
                        green = pixelBuffer[byteOffset + 1];
                    }


                    if (applyRed == false)
                    {
                        red = pixelBuffer[byteOffset + 2];
                    }


                    if (edgeType == MorphologyEdgeType.EdgeDetection ||
                        edgeType == MorphologyEdgeType.SharpenEdgeDetection)
                    {
                        if (morphType == MorphologyType.Dilation)
                        {
                            blue = blue - pixelBuffer[byteOffset];
                            green = green - pixelBuffer[byteOffset + 1];
                            red = red - pixelBuffer[byteOffset + 2];
                        }
                        else if (morphType == MorphologyType.Erosion)
                        {
                            blue = pixelBuffer[byteOffset] - blue;
                            green = pixelBuffer[byteOffset + 1] - green;
                            red = pixelBuffer[byteOffset + 2] - red;
                        }


                        if (edgeType == MorphologyEdgeType.SharpenEdgeDetection)
                        {
                            blue += pixelBuffer[byteOffset];
                            green += pixelBuffer[byteOffset + 1];
                            red += pixelBuffer[byteOffset + 2];
                        }
                    }


                    blue = (blue > 255 ? 255 : (blue < 0 ? 0 : blue));
                    green = (green > 255 ? 255 : (green < 0 ? 0 : green));
                    red = (red > 255 ? 255 : (red < 0 ? 0 : red));


                    resultBuffer[byteOffset] = (byte)blue;
                    resultBuffer[byteOffset + 1] = (byte)green;
                    resultBuffer[byteOffset + 2] = (byte)red;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public static Bitmap
            Laplacian3x3Filter(this Bitmap sourceBitmap,
                bool grayscale = true)
        {
            Bitmap resultBitmap =
                ConvolutionFilter(sourceBitmap,
                    Laplacian3x3,
                    1.0, 0, grayscale);


            return resultBitmap;
        }
        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                     double[,] filterMatrix,
                                          double factor = 1,
                                               int bias = 0,
                                     bool grayscale = false)
        {
            BitmapData sourceData =
                           sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                                             ImageLockMode.ReadOnly,
                                        PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;


            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);


            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                 filterX + filterOffset];


                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];


                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                filterX + filterOffset];
                        }
                    }


                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;


                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }


                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }


                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }


                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                            sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public static Bitmap ConvolutionFilterTwoDimension(this Bitmap sourceBitmap,
                                        double[,] xFilterMatrix,
                                        double[,] yFilterMatrix,
                                              double factor = 1,
                                                   int bias = 0,
                                         bool grayscale = false)
        {
            BitmapData sourceData =
                           sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                                             ImageLockMode.ReadOnly,
                                        PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;


            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;


            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;


            int filterOffset = 1;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;


                    blueTotal = greenTotal = redTotal = 0.0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blueX += (double)
                                      (pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenX += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redX += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            blueY += (double)
                                      (pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenY += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redY += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];
                        }
                    }


                    blueTotal = Math.Sqrt((blueX * blueX) +
                                          (blueY * blueY));


                    greenTotal = Math.Sqrt((greenX * greenX) +
                                           (greenY * greenY));


                    redTotal = Math.Sqrt((redX * redX) +
                                         (redY * redY));


                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }


                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }


                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }


                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public static Bitmap SobelEdgeDetect(Bitmap ori, Bitmap original)
        {
            Bitmap b = original;
            Bitmap bb = original;
            int width = b.Width;
            int height = b.Height;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                        bb.SetPixel(i, j, Color.Black);

                    //bb.SetPixel (i, j, Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
                    else
                        bb.SetPixel(i, j, Color.Transparent);
                }
            }
            return bb;

        }
        public static Bitmap FilterForImage(this Bitmap sourceBitmap, double[] filterKernel, bool isHighpass)
        {
            var sourceBitmapWidth = sourceBitmap.Width;
            var sourceBitmapHeight = sourceBitmap.Height;
            var arrayNoise = new int[sourceBitmap.Width, sourceBitmap.Height];
            for (int x = 0; x < sourceBitmap.Width; x++)
            {
                for (int y = 0; y < sourceBitmap.Height; y++)
                {
                    arrayNoise[x, y] = sourceBitmap.GetPixel(x, y).R;
                }
            }
            var oneDimensionImageArrayNoises = MultiToSingle(arrayNoise);
            var outputImage = new List<List<double>>(sourceBitmapHeight);
            for (int i = 0; i < sourceBitmapHeight; i++)
            {
                outputImage.Add(StatHelper.Convolve(oneDimensionImageArrayNoises.Skip(sourceBitmapWidth * i).Take(sourceBitmapWidth).ToArray(),
                    filterKernel, true).ToList());
            }
            int[,] bufferOutputArray = new int[sourceBitmapHeight, sourceBitmapWidth];
            Parallel.For(0, sourceBitmapHeight, x => Parallel.For(0, sourceBitmapWidth, y =>
            {
                bufferOutputArray[x, y] = (int)outputImage[x][y];
            }));
            if (isHighpass)
                bufferOutputArray = NormaliseData(sourceBitmapHeight, sourceBitmapWidth, bufferOutputArray);
            Bitmap newImage = new Bitmap(sourceBitmapWidth, sourceBitmapHeight);
            for (int i = 0; i < sourceBitmapWidth; i++)
            {
                for (int j = 0; j < sourceBitmapHeight; j++)
                {
                    var colorPixel = (int)bufferOutputArray[j, i];
                    newImage.SetPixel(i, j,
                        Color.FromArgb(colorPixel, colorPixel,
                            colorPixel));
                }
            }

            return newImage;
        }
        public static Bitmap MedianFilter(this Bitmap sourceBitmap,
                                               int matrixSize,
                                                 int bias = 0,
                                        bool grayscale = false)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];

            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    neighbourPixels.Clear();

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {

                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            neighbourPixels.Add(BitConverter.ToInt32(
                                             pixelBuffer, calcOffset));
                        }
                    }

                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(
                                       neighbourPixels[filterOffset]);

                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);

            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
        public static void MedianFiltering(Bitmap bm)
        {
            List<byte> termsList = new List<byte>();

            byte[,] image = new byte[bm.Width, bm.Height];

            //Convert to Grayscale 
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    var c = bm.GetPixel(i, j);
                    byte gray = (byte)(.333 * c.R + .333 * c.G + .333 * c.B);
                    image[i, j] = gray;
                }
            }

            //applying Median Filtering 
            for (int i = 0; i <= bm.Width - 3; i++)
                for (int j = 0; j <= bm.Height - 3; j++)
                {
                    for (int x = i; x <= i + 2; x++)
                        for (int y = j; y <= j + 2; y++)
                        {
                            termsList.Add(image[x, y]);
                        }
                    byte[] terms = termsList.ToArray();
                    termsList.Clear();
                    Array.Sort<byte>(terms);
                    Array.Reverse(terms);
                    byte color = terms[4];
                    bm.SetPixel(i + 1, j + 1, Color.FromArgb(color, color, color));
                }
        }
        public static int[,] ReadXcr(string fileDirectory, int N, int M)
        {
            FileStream fs = new FileStream(fileDirectory, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            int[,] data = new int[M, N];
            byte[] res;
            short sor;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    //res = br.ReadBytes(2);
                    sor = br.ReadInt16();
                    //string st = Convert.ToString(res[0]) + Convert.ToString(res[1]);
                    //int rr = Convert.ToInt16(st);
                    data[i, j] = sor;
                }
            }
            fs.Close();
            br.Close();
            return data;
        }

        public static char[,] ReadXcrv2(string fileDirectory, int N, int M)
        {
            var width = M;
            var height = N;
            var map = new char[width, height];
            var file = new StreamReader(fileDirectory);
            string line;
            var lineCount = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (lineCount < height)
                {
                    for (int i = 0; i < width && i < line.Length;)
                    {
                        map[i, lineCount] = line[i];
                    }
                }
                lineCount++;
            }
            file.Close();
            return map;
        }
        private static float Lerp(float s, float e, float t)
        {
            return s + (e - s) * t;
        }

        private static float Blerp(float c00, float c10, float c01, float c11, float tx, float ty)
        {
            return Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
        }

        public static Image ResizingImageBilinearInterpolate(Bitmap self, double scaleX, double scaleY)
        {
            int newWidth = (int)(self.Width * scaleX);
            int newHeight = (int)(self.Height * scaleY);
            Bitmap newImage = new Bitmap(newWidth, newHeight);

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    float gx = ((float)x) / newWidth * (self.Width - 1);
                    float gy = ((float)y) / newHeight * (self.Height - 1);
                    int gxi = (int)gx;
                    int gyi = (int)gy;
                    Color c00 = self.GetPixel(gxi, gyi);
                    Color c10 = self.GetPixel(gxi + 1, gyi);
                    Color c01 = self.GetPixel(gxi, gyi + 1);
                    Color c11 = self.GetPixel(gxi + 1, gyi + 1);

                    int red = (int)Blerp(c00.R, c10.R, c01.R, c11.R, gx - gxi, gy - gyi);
                    int green = (int)Blerp(c00.G, c10.G, c01.G, c11.R, gx - gxi, gy - gyi);
                    int blue = (int)Blerp(c00.B, c10.B, c01.B, c11.R, gx - gxi, gy - gyi);
                    Color rgb = Color.FromArgb(red, green, blue);
                    newImage.SetPixel(x, y, rgb);
                }
            }

            return newImage;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            returnImage.Save("C:\\Users\\Cong Hoang\\source\\repos\\EquationV2\\ImageProcessing\\user_images/outImage.jpg", ImageFormat.Jpeg);
            return returnImage;
        }

        public static int[,] NormaliseData(int h, int w, int[,] inputArrayForNormalize)
        {
            var t = new int[h, w];
            Parallel.For(0, h, x => Parallel.For(0, w, y =>
            {
                t[x, y] = (int)inputArrayForNormalize[x, y];
            }));
            var globalMax1 = t.Cast<int>().Max();
            var globalMin1 = t.Cast<int>().Min();
            var normalizedXcr1 = new int[h, w];

            Parallel.For(0, h, x => Parallel.For(0, w, y =>
            {
                normalizedXcr1[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
            }));
            return normalizedXcr1;
        }
        public static int[,] ReadImageAsMatrix(string filePath)
        {
            Bitmap img = new Bitmap(filePath);
            Bitmap grayImage = Grayscale.CommonAlgorithms.BT709.Apply(img);
            //Bitmap grayImage = GrayscaleImage(image1);
            var imgWidth = img.Width;
            var imgHeight = img.Height;
            var asMatrix = new int[imgWidth, imgHeight];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    asMatrix[i, j] = grayImage.GetPixel(i, j).R;
                }
            }
            return asMatrix;
        }
        public static Color[][] getBitmapColorMatrix(string filePath)
        {
            Bitmap bmp = new Bitmap(filePath);
            Color[][] matrix;
            int height = bmp.Height;
            int width = bmp.Width;
            if (height > width)
            {
                matrix = new Color[bmp.Width][];
                for (int i = 0; i <= bmp.Width - 1; i++)
                {
                    matrix[i] = new Color[bmp.Height];
                    for (int j = 0; j < bmp.Height - 1; j++)
                    {
                        matrix[i][j] = bmp.GetPixel(i, j);
                    }
                }
            }
            else
            {
                matrix = new Color[bmp.Height][];
                var test = bmp.GetPixel(0, 359);
                for (int i = 0; i <= bmp.Height - 1; i++)
                {
                    matrix[i] = new Color[bmp.Width];
                    for (int j = 0; j < bmp.Width - 1; j++)
                    {
                        matrix[i][j] = bmp.GetPixel(i, j);
                    }
                }
            }
            return matrix;
        }
        public static double MeanX(double[] arr, int N)
        {
            double x = 0;
            for (int i = 0; i < N; i++)
            {
                x += arr[i];
            }

            meanValue = x / N;
            return meanValue;
        }
        public static double VarXX(int[,] arr, int N, int row, double currentMean)
        {
            double X = 0;
            for (int i = 0; i < N; i++)
            {
                X += Math.Pow(arr[row, i] - currentMean, 2);
            }
            return X / N;
        }
        public static double[] row_sum(int[,] arr, int m, int n, out double[] varianceRow)
        {
            int i, j;
            double sum = 0;
            var meanRow = new double[m];
            varianceRow = new double[m];
            // finding the row sum 
            for (i = 0; i < m; ++i)
            {
                for (j = 0; j < n; ++j)
                {
                    // Add the element 
                    sum = sum + arr[i, j];
                }
                meanRow[i] = sum / n;
                varianceRow[i] = VarXX(arr, n, i, meanRow[i]);
                // Print the row sum 
                // Reset the sum 
                sum = 0;
            }
            return meanRow;
        }
        // Function to calculate sum  
        // of each column 
        public static double[] column_sum(int[,] arr, int m, int n)
        {

            int i, j;
            double sum = 0;
            var meanColumn = new double[n];
            // finding the column sum 
            for (i = 0; i < n; ++i)
            {
                for (j = 0; j < m; ++j)
                {
                    // Add the element 
                    sum = sum + arr[j, i];
                }
                meanColumn[i] = sum / n;
                // Reset the sum 
                sum = 0;
            }

            return meanColumn;
        }
        public static List<double> ReadDatFileV1(string path)
        {
            var signalArray = new List<double>();
            using (BinaryReader b = new BinaryReader(
                File.Open(path, FileMode.Open)))
            {
                // Position and length variables.   
                int pos = 0;
                // Use BaseStream.
                int length = (int)b.BaseStream.Length;
                while (pos < length)
                {
                    // Read integer.
                    var v = b.ReadSingle();
                    signalArray.Add(v);
                    // Advance our position variable.
                    pos += sizeof(int);
                }
            }
            return signalArray;
        }

        public static Complex[] DivideTwoComplexsArray(Complex[] inputComplexs, Complex[] kernelComplexs)
        {
            int len = inputComplexs.Length;
            var outputComplexArray = new Complex[len];
            for (int i = 0; i < len; i++)
            {
                outputComplexArray[i] = Complex.Divide(inputComplexs[i], kernelComplexs[i]);
            }
            return outputComplexArray;
        }

        public static Complex[] DivideTwoComplexsArrayForNoisesAndBlurredImage(Complex[] inputComplexs,
            Complex[] kernelComplexs)
        {
            var len = inputComplexs.Length;
            double alpha = 1;
            var outputComplexArray = new Complex[len];
            for (int i = 0; i < len; i++)
            {
                outputComplexArray[i] = Complex.Multiply(inputComplexs[i], Complex.Conjugate(kernelComplexs[i])) / (Math.Pow(kernelComplexs[i].Magnitude, 2) + alpha * alpha);
            }
            return outputComplexArray;
        }
        public static List<List<Complex>> FftForImage(int h, int w, double[,] arrayForFT)
        {
            var sample = new Complex[h * w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    sample[i * w + j] = new Complex(arrayForFT[j, i], 0);
                }
            }
            var outputImage = new List<List<Complex>>(h);
            for (int i = 0; i < h; i++)
            {
                var bufferSample = sample.Skip(i * w).Take(w).ToArray();
                Fourier.Forward(bufferSample, FourierOptions.NoScaling);
                //Fourier.Inverse(bufferSample, FourierOptions.NoScaling);
                outputImage.Add(bufferSample.ToList());
            }

            //var t = new int[h, w];
            //Parallel.For(0, h, x => Parallel.For(0, w, y =>
            //{
            //    t[x, y] = (int)outputImage[x][y].Real;
            //}));
            //var globalMax1 = t.Cast<int>().Max();
            //var globalMin1 = t.Cast<int>().Min();
            //var normalizedXcr1 = new int[h, w];

            //Parallel.For(0, h, x => Parallel.For(0, w, y =>
            //{
            //    normalizedXcr1[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
            //}));
            return outputImage;
        }

        // Perform threshold adjustment on the image.
        //private Bitmap AdjustThreshold(Image image, float threshold)
        //{
        //    // Make the result bitmap.
        //    Bitmap bm = new Bitmap(image.Width, image.Height);

        //    // Make the ImageAttributes object and set the threshold.
        //    ImageAttributes attributes = new ImageAttributes();
        //    attributes.SetThreshold(threshold);

        //    // Draw the image onto the new bitmap while applying the new ColorMatrix.
        //    Point[] points =
        //    {
        //        new Point(0, 0),
        //        new Point(image.Width, 0),
        //        new Point(0, image.Height),
        //    };
        //    Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
        //    using (Graphics gr = Graphics.FromImage(bm))
        //    {
        //        gr.DrawImage(image, points, rect, (float)GraphicsUnit.Pixel, attributes);
        //    }

        //    // Return the result.
        //    return bm;
        //}
        public static double[] MultiToSingle(int[,] array)
        {
            int index = 0;
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            double[] single = new double[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    single[index] = array[x, y];
                    index++;
                }
            }
            return single;
        }

        public static Bitmap ImageSubtraction(Bitmap contourImageForNoise, Bitmap newImage)
        {
            var w = contourImageForNoise.Width;
            var h = contourImageForNoise.Height;
            var t = new int[h, w];

            Bitmap subtractedBitmap = new Bitmap(w, h);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    var colorPixel = contourImageForNoise.GetPixel(i, j);
                    var colorPixel1 = newImage.GetPixel(i, j);
                    var desPixel = (colorPixel.R - colorPixel1.R);
                    t[j, i] = desPixel;
                }
            }
            var globalMax1 = t.Cast<int>().Max();
            var globalMin1 = t.Cast<int>().Min();
            var normalizedBitmap = new int[h, w];

            Parallel.For(0, h, x => Parallel.For(0, w, y =>
            {
                normalizedBitmap[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
            }));

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    var color = normalizedBitmap[j, i];
                    subtractedBitmap.SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }
            return subtractedBitmap;
        }
        public struct RGB
        {
            private byte _r;
            private byte _g;
            private byte _b;

            public RGB(byte r, byte g, byte b)
            {
                this._r = r;
                this._g = g;
                this._b = b;
            }

            public byte R
            {
                get { return this._r; }
                set { this._r = value; }
            }

            public byte G
            {
                get { return this._g; }
                set { this._g = value; }
            }

            public byte B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public bool Equals(RGB rgb)
            {
                return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
            }
        }

        public struct HSV
        {
            private double _h;
            private double _s;
            private double _v;

            public HSV(double h, double s, double v)
            {
                this._h = h;
                this._s = s;
                this._v = v;
            }

            public double H
            {
                get { return this._h; }
                set { this._h = value; }
            }

            public double S
            {
                get { return this._s; }
                set { this._s = value; }
            }

            public double V
            {
                get { return this._v; }
                set { this._v = value; }
            }

            public bool Equals(HSV hsv)
            {
                return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
            }
        }

        public static HSV RGBToHSV(RGB rgb)
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(rgb.R, rgb.G), rgb.B);
            v = Math.Max(Math.Max(rgb.R, rgb.G), rgb.B);
            delta = v - min;

            if (v == 0.0)
                s = 0;
            else
                s = delta / v;

            if (s == 0)
                h = 0.0;

            else
            {
                if (rgb.R == v)
                    h = (rgb.G - rgb.B) / delta;
                else if (rgb.G == v)
                    h = 2 + (rgb.B - rgb.R) / delta;
                else if (rgb.B == v)
                    h = 4 + (rgb.R - rgb.G) / delta;

                h *= 60;

                if (h < 0.0)
                    h = h + 360;
            }

            return new HSV(h, s, (v / 255));
        }
        public enum MorphologyType
        {
            Erosion,
            Dilation
        }
        public enum MorphologyEdgeType
        {
            EdgeDetection,
            SharpenEdgeDetection,
            None
        }
    }


}
