using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using AForge.Math.Random;
using EquationV2;
using MathNet.Filtering.FIR;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Complex = System.Numerics.Complex;
using Complex2 = AForge.Math.Complex;
using Correlation = EquationV2.Correlation;
using Histogram = MathNet.Numerics.Statistics.Histogram;
using Image = System.Drawing.Image;
using Parallel = System.Threading.Tasks.Parallel;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public ImageOutputForm OutputXrayForm;
        private int _choice = 6;
        private Image originalImage;
        public double[] meanRow;
        private Complex[] samples;
        private Complex[] spectrum;
        private double[] arrayForLowPass = { 0.35577019, 0.2436983, 0.07211497, 0.00630165 };
        double[,] gx = new double[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        double[,] gy = new double[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        public Form1()
        {
            InitializeComponent();
            chartRow.ChartAreas[0].AxisY.ScaleView.Zoom(-0.1, 0.1);
            chartRow.ChartAreas[0].AxisX.ScaleView.Zoom(0, 2000);
            chartRow.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartRow.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartRow.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartRow.Series[0].BorderWidth = 3;
            chartRow.Series[0].Color = Color.Blue;
            chartRow.Series[1].Color = Color.Red;
            chartRow.Series[1].BorderWidth = 3;

            chartCollumn.ChartAreas[0].AxisY.ScaleView.Zoom(-0.1, 0.1);
            chartCollumn.ChartAreas[0].AxisX.ScaleView.Zoom(0, 2000);
            chartCollumn.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartCollumn.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartCollumn.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartCollumn.Series[0].BorderWidth = 3;
            chartCollumn.Series[0].ChartType = SeriesChartType.Spline;
            chartCollumn.Series[1].BorderWidth = 3;

            chartHistogram.ChartAreas[0].AxisY.ScaleView.Zoom(-0.1, 0.1);
            chartHistogram.ChartAreas[0].AxisX.ScaleView.Zoom(0, 2000);
            chartHistogram.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartHistogram.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartHistogram.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            chart4.ChartAreas[0].AxisY.ScaleView.Zoom(-0.1, 0.1);
            chart4.ChartAreas[0].AxisX.ScaleView.Zoom(0, 2000);
            chart4.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart4.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart4.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart4.Series[0].BorderWidth = 3;
            chart4.Series[0].ChartType = SeriesChartType.Spline;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            var imgPro = new Processing();
            // Retrieve the image.
            Bitmap image1 = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\grace.jpg", true);
            var imgWidth = image1.Width;
            var imgHeight = image1.Height;
            Bitmap grayImage = Grayscale.CommonAlgorithms.BT709.Apply(image1);
            //Bitmap grayImage = GrayscaleImage(image1);
            var array1 = ImageHelper.ReadImageAsMatrix(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\grace.jpg");
            var oneDimensionImageArray = ImageHelper.MultiToSingle(array1);
            double scale = 1.5f;

            if (ScaleTextbox.Text != null)
            {
                bool valid = double.TryParse(ScaleTextbox.Text, out scale);
                if (!valid)
                {
                    scale = 1.5f;
                }
            }
            switch (_choice)
            {
                //Nearest Neighbor 
                case 1:
                    {
                        //var globalMax = array1.Cast<int>().Max();
                        //var globalMin = array1.Cast<int>().Min();
                        //for (int x = 0; x < image1.Width; x++)
                        //{
                        //    for (int y = 0; y < image1.Height; y++)
                        //    {
                        //       var fk^ = (array1[x,y] - min)*255/(max-min)   
                        //    }
                        //}
                        DrawHistogram(oneDimensionImageArray, oneDimensionImageArray.Length);
                        var rowVariance = new double[image1.Width];
                        var meanRow = ImageHelper.row_sum(array1, image1.Width, image1.Height, out rowVariance);
                        var meanColumn = ImageHelper.column_sum(array1, image1.Width, image1.Height);
                        //DrawAnything(rowVariance, "Series1");
                        DrawAnythingOnCharRow(meanRow, "Series2");
                        DrawAnythingOnCollumn(meanColumn, "Series2");
                        pictureBox1.Image = ResizingImageNearestNeighbor(grayImage, scale, scale, oneDimensionImageArray);
                        pictureBox2.Image = image1;
                        break;
                    }
                //Bilinear interpolation
                case 2:
                    {
                        //DrawHistogram(oneDimensionImageArray, oneDimensionImageArray.Length);
                        pictureBox1.Image = ImageHelper.ResizingImageBilinearInterpolate(grayImage, scale, scale);
                        pictureBox2.Image = image1;
                        break;
                    }
                //Negative image
                case 3:
                    {
                        Bitmap outImage3 = new Bitmap(imgWidth, imgHeight);
                        for (int x = 0; x < imgWidth; x++)
                        {
                            for (int y = 0; y < imgHeight; y++)
                            {
                                Color newColor = Color.FromArgb(255 - array1[x, y], 255 - array1[x, y], 255 - array1[x, y]);
                                outImage3.SetPixel(x, y, newColor);
                            }
                        }
                        var cdfOut = GetCDF(outImage3);
                        DrawAnythingOnCharFour(cdfOut, "Series1");
                        pictureBox1.Image = outImage3;
                        pictureBox2.Image = image1;
                        break;
                    }
                //Gamma correction
                case 4:
                    {
                        Bitmap imageGamma = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\LA.jpg", true);
                        Bitmap grayImageGamma = Grayscale.CommonAlgorithms.BT709.Apply(imageGamma);
                        var outImage4 = imgPro.Gamma_Correction(grayImageGamma, 5);
                        var cdfOut = GetCDF(grayImageGamma);
                        DrawAnythingOnCharFour(cdfOut, "Series1");
                        pictureBox1.Image = outImage4;
                        pictureBox2.Image = imageGamma;
                        break;
                    }
                //Log transform
                case 5:
                    {
                        Bitmap imageLog = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\image2.jpg", true);
                        Bitmap grayImageLog = Grayscale.CommonAlgorithms.BT709.Apply(imageLog);

                        var outImage5 = imgPro.Logarit_transform(grayImageLog, 100);
                        var cdfOut = GetCDF(grayImageLog);
                        DrawAnythingOnCharFour(cdfOut, "Series1");
                        pictureBox1.Image = imgPro.Logarit_transform(grayImageLog, 100);
                        pictureBox2.Image = imageLog;
                        break;
                    }
                case 6:
                    {
                        //Histogram equalization
                        Bitmap imageHis = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\HollywoodLC.jpg", true);
                        Bitmap grayImageHist = Grayscale.CommonAlgorithms.BT709.Apply(imageHis);
                        var cdf = GetCDF(grayImageHist);
                        DrawAnythingOnCharRow(cdf, "Series2");

                        var hisEqualized = new double[256];
                        for (int i = 0; i < 256; i++)
                        {
                            hisEqualized[i] = ((cdf[i] - 1) / (grayImageHist.Height * grayImageHist.Width - 1)) * 255;
                        }
                        hisEqualized = hisEqualized.Select(c => Math.Round(c)).ToArray();
                        Bitmap newImage = new Bitmap(grayImageHist.Width, grayImageHist.Height);
                        for (int i = 0; i < grayImageHist.Width; i++)
                            for (int j = 0; j < grayImageHist.Height; j++)
                            {
                                Color pixel = grayImageHist.GetPixel(i, j);
                                newImage.SetPixel(i, j, Color.FromArgb((int)hisEqualized[pixel.R], (int)hisEqualized[pixel.R], (int)hisEqualized[pixel.R]));
                            }

                        var cdfOut = GetCDF(newImage);
                        var cdfInverse = InverseCDF(cdf);
                        var histogramOfOutput = ImageHelper.MultiToSingle(GetTwoDimensionArrayForImage(newImage));
                        pictureBox1.Image = newImage;
                        pictureBox2.Image = imageHis;
                        DrawAnythingOnCharFour(cdfOut, "Series1");
                        DrawAnythingWithCustomList(cdfInverse, "Series1");
                        DrawHistogram(histogramOfOutput, histogramOfOutput.Length);
                        break;
                    }
                case 7:
                    {
                        //Convert image into frequency domain then apply filter
                        var xcrFile =
                            ImageHelper.ReadXcr(
                                @"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\h400x300.xcr", 300,
                                400);
                        var globalMax = xcrFile.Cast<int>().Max();
                        var globalMin = xcrFile.Cast<int>().Min();
                        var normalizedXcr = new int[300, 400];
                        Parallel.For(0, 300, y => Parallel.For(0, 400, x =>
                        {
                            normalizedXcr[y, x] = (xcrFile[x, y] - globalMin) * 255 / (globalMax - globalMin);
                        }));
                        var imageMatrix1 = ImageHelper.MultiToSingle(normalizedXcr);
                        var firstRow = imageMatrix1.Take(400).ToArray();
                        var bandStopFilter1 = BSPF(0.2, 0.4, 16, 1);
                        //var bandStopFilter1 = FirCoefficients.BandStop(1, 0.2, 0.3);
                        //var lengthDft = bandStopFilter1.Length;
                        //for (int i = 0; i < lengthDft; i++)
                        //{
                        //    bandStopFilter1[i] *= 2 * (17);
                        //}

                        var outputImage = new List<List<double>>(300);
                        for (int i = 0; i < 300; i++)
                        {
                            outputImage.Add(StatHelper.Convolve(imageMatrix1.Skip(400 * i).Take(400).ToArray(),
                                bandStopFilter1, true).ToList());
                        }
                        var bufferOutputArray = new int[300, 400];
                        Parallel.For(0, 300, x => Parallel.For(0, 400, y =>
                        {
                            bufferOutputArray[x, y] = (int)outputImage[x][y];
                        }));

                        var globalMax1 = bufferOutputArray.Cast<int>().Max();
                        var globalMin1 = bufferOutputArray.Cast<int>().Min();
                        var normalizedXcr1 = new int[300, 400];

                        Parallel.For(0, 300, x => Parallel.For(0, 400, y =>
                        {
                            normalizedXcr1[x, y] = (bufferOutputArray[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
                        }));

                        //var csn = new double[1000];
                        //var xn = new double[1000];
                        //var dftArray = SlowFourierTransform(firstRow.Length, firstRow, out csn, out xn);
                        //DrawAnythingOnCharRow(dftArray, "Series2");
                        var firstRowAfterConvoled =
                            TakeRowInMatrix(normalizedXcr1, 400, 1);
                        var fifthRowAfterConvoled =
                            TakeRowInMatrix(normalizedXcr1, 400, 100);

                        var autoCor = StatHelper.AutoCorrelationV3(firstRowAfterConvoled, 0, 400);
                        var crossCor = StatHelper.CrossCorrelation(400, firstRowAfterConvoled, fifthRowAfterConvoled, -200, 200);

                        PlotFFt(400, 400, autoCor);
                        PlotFFt(400, 400, crossCor, 2);
                        //PlotFFt(bandStopFilter1.Length, bandStopFilter1.Length, bandStopFilter1, 1);
                        PlotFFt(firstRow.Length, firstRow.Length, firstRow, 1);
                        DrawAnythingOnCharRow(autoCor, "Series2");
                        DrawAnythingOnCharRow(crossCor, "Series1");

                        Bitmap newImage = new Bitmap(400, 300);
                        for (int i = 0; i < 400; i++)
                        {
                            for (int j = 0; j < 300; j++)
                            {
                                var colorPixel = normalizedXcr1[j, i];
                                newImage.SetPixel(i, j,
                                    Color.FromArgb(colorPixel, colorPixel,
                                        colorPixel));
                            }
                        }
                        pictureBox1.Image = newImage;
                        pictureBox2.Refresh();
                        break;
                    }
                //Random noise
                case 8:
                    {
                        Bitmap imageForNoise = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap grayImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(imageForNoise);
                        var grayimage2 = grayImageForNoise;
                        // create random generator
                        IRandomNumberGenerator generator = new UniformGenerator(new Range(-15, 15));
                        // create filter
                        AdditiveNoise filter1 = new AdditiveNoise(generator);
                        // apply the filter
                        filter1.ApplyInPlace(grayimage2);
                        var lowpassfilter = LPF(90, 16, 0.001);
                        //Bitmap newImage = ImageHelper.FilterForImage(grayImageForNoise, lowpassfilter, false);//uncomment this line to see filtered image
                        Bitmap newImageEdgeDetected = grayImageForNoise.ConvolutionFilterTwoDimension(gx, gy);
                        Bitmap newImageEdgeDetectedUsingLaplacian = grayImageForNoise.Laplacian3x3Filter();

                        var imageLaplacianApply = Grayscale.CommonAlgorithms.RMY.Apply(newImageEdgeDetectedUsingLaplacian);
                        // create filter
                        IFilter threshold = new Threshold(100);
                        var test = threshold.Apply(imageLaplacianApply);

                        pictureBox1.Image = newImageEdgeDetected;// Edge detected
                        //pictureBox1.Image = newImage;
                        pictureBox2.Image = grayimage2;
                        //pictureBox3.Image = test;
                        pictureBox3.Image = newImageEdgeDetectedUsingLaplacian;


                        //add some noise for each pixel
                        //for (var y = 0; y < imgHeight; y++)
                        //{
                        //    var rowRandom = RandomeHelper.RandomWithBorder(25, 300);
                        //    var spikeArray = RandomeHelper.Spikes(0.006, 90, 0.7, 0.2, TakeRowInMatrix(array1, y));
                        //    for (int x = 0; x < imgWidth; x++)
                        //    {
                        //        array1[x, y] += (int)spikeArray[x];
                        //    }
                        //}
                        //Bitmap newImage = new Bitmap(grayImage.Width, grayImage.Height);
                        //for (int i = 0; i < grayImage.Width; i++)
                        //    for (int j = 0; j < grayImage.Height; j++)
                        //    {
                        //        Color pixel = grayImage.GetPixel(i, j);
                        //        newImage.SetPixel(i, j, Color.FromArgb((int)array1[i, j], (int)array1[i, j], (int)array1[i, j]));
                        //    }
                        //pictureBox1.Image = grayImageForNoise;
                        break;
                    }
                //Salf and pepper noise
                case 9:
                    {
                        Bitmap imageForNoise = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap grayImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(imageForNoise);

                        var grayimage2 = grayImageForNoise;
                        // create filter
                        SaltAndPepperNoise filter = new SaltAndPepperNoise(1);
                        // apply the filter
                        filter.ApplyInPlace(grayImageForNoise);
                        //var newImage = ImageHelper.MedianFilter(grayImageForNoise, 5);//uncomment this line to see filtered image

                        Bitmap newImageEdgeDetected = grayImageForNoise.ConvolutionFilterTwoDimension(gx, gy);
                        pictureBox1.Image = newImageEdgeDetected;// Edge detected using sobel

                        Bitmap newImageEdgeDetectedUsingLaplacian = grayImageForNoise.Laplacian3x3Filter();

                        pictureBox3.Image = newImageEdgeDetectedUsingLaplacian;

                        //pictureBox1.Image = newImage;
                        pictureBox2.Image = grayImageForNoise;
                        break;
                    }
                //Random noise + salf and pepper noise
                case 10:
                    {
                        Bitmap imageForNoise = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap grayImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(imageForNoise);
                        // create random generator
                        IRandomNumberGenerator generator = new UniformGenerator(new Range(-10, 10));
                        // create filter
                        AdditiveNoise filter1 = new AdditiveNoise(generator);
                        var grayimage2 = grayImageForNoise;
                        // create filter
                        filter1.ApplyInPlace(grayImageForNoise);
                        SaltAndPepperNoise filter = new SaltAndPepperNoise(1);
                        // apply the filter
                        filter.ApplyInPlace(grayImageForNoise);

                        //var newImage = ImageHelper.MedianFilter(grayImageForNoise, 5);//uncomment this line to see filtered image

                        Bitmap newImageEdgeDetected = grayImageForNoise.ConvolutionFilterTwoDimension(gx, gy);
                        pictureBox1.Image = newImageEdgeDetected;// Edge detected
                        Bitmap newImageEdgeDetectedUsingLaplacian = grayImageForNoise.Laplacian3x3Filter();
                        var image111 = Grayscale.CommonAlgorithms.RMY.Apply(newImageEdgeDetectedUsingLaplacian);
                        IFilter threshold = new Threshold(120);
                        var thresholdLaplacian = threshold.Apply(image111);

                        pictureBox3.Image = thresholdLaplacian;
                        //pictureBox1.Image = newImage;
                        pictureBox2.Image = grayImageForNoise;
                        break;
                    }
                case 11:
                    {
                        Bitmap imageForFT = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\image2.jpg", true);
                        Bitmap grayImageForFT = Grayscale.CommonAlgorithms.BT709.Apply(imageForFT);
                        var h = grayImageForFT.Height;
                        var w = grayImageForFT.Width;
                        var arrayForFT = ImageHelper.ReadImageAsMatrix(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\image2.jpg");
                        //var firstRow = TakeRowInMatrix(arrayForFT, grayImage.Width, 0);
                        //PlotFFt(grayImageForFT.Width, grayImageForFT.Width, firstRow);

                        //Convert to Complex Array
                        //var sample = new Complex[h, w];
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
                            Fourier.Inverse(bufferSample, FourierOptions.NoScaling);
                            outputImage.Add(bufferSample.ToList());
                        }

                        //var spectrumImage = outputImage.Select(l => l.Select(c => new Complex(c.Magnitude, 0)).ToList())
                        //    .ToList();
                        //var outputImage1 = new List<List<double>>(h);

                        //for (int i = 0; i < h; i++)
                        //{
                        //    var bufferRow = spectrumImage[i].ToArray();
                        //    Fourier.Inverse(bufferRow, FourierOptions.NoScaling);
                        //    outputImage1.Add(bufferRow.Select(c => c.Real).ToList());
                        //}

                        //var t = outputImage1.Select(l => l.ToArray()).ToArray();
                        var t = new int[h, w];
                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            t[x, y] = (int)outputImage[x][y].Real;
                        }));
                        var globalMax1 = t.Cast<int>().Max();
                        var globalMin1 = t.Cast<int>().Min();
                        var normalizedXcr1 = new int[h, w];

                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            normalizedXcr1[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
                        }));

                        //var one = MultiToSingle(arrayForFT);
                        //var outputImage3 = new List<List<double>>(h);
                        //for (int i = 0; i < h; i++)
                        //{
                        //    var csn = new double[w];
                        //    var xn = new double[w];
                        //    var signalarr = one.Skip(i*w).Take(w).ToArray();
                        //    var dftArray = SlowFourierTransformOri(w, signalarr, out csn, out xn);
                        //    var re = RebuiltFourier(xn, csn, w).ToList();
                        //    outputImage3.Add(re);
                        //}

                        //var t = new int[h, w];
                        //Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        //{
                        //    t[x, y] = (int)outputImage3[x][y];
                        //}));
                        //var globalMax1 = t.Cast<int>().Max();
                        //var globalMin1 = t.Cast<int>().Min();
                        //var normalizedXcr1 = new int[h, w];

                        //Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        //{
                        //    normalizedXcr1[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
                        //}));

                        Bitmap newImage2 = new Bitmap(w, h);
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                var colorPixel = normalizedXcr1[j, i];
                                newImage2.SetPixel(i, j,
                                    Color.FromArgb(colorPixel, colorPixel,
                                        colorPixel));
                            }
                        }

                        pictureBox1.Image = newImage2;
                        //Fourier.Forward2D(sample, w, h, FourierOptions.NoScaling);
                        break;
                    }
                case 12:
                    {
                        int w = 307, h = 221;
                        var blurredImage = ImageHelper.ReadDatFileV1(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\blur307x221D.dat");
                        var blurredAndNoisesImage = ImageHelper.ReadDatFileV1(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\blur307x221D_N.dat");
                        var kernel = ImageHelper.ReadDatFileV1(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\kernD76_f4.dat");
                        var tailKernel = new double[w - kernel.Count];
                        var complexKernel = kernel.Concat(tailKernel).Select(c => new Complex(c, 0)).ToArray();
                        //Take FFT for kernel
                        Fourier.Forward(complexKernel, FourierOptions.NoScaling);
                        var arrayBlurred = SingleToMulti(blurredImage.ToArray(), w, h);
                        var arrayBlurredAndNoises = SingleToMulti(blurredAndNoisesImage.ToArray(), w, h);
                        //var testMax = arrayBlurred.Cast<double>().Max();
                        //Take FFT for BlurredImage
                        var fftBlurredImage = ImageHelper.FftForImage(h, w, arrayBlurred);
                        var fftBlurredAndNoisesImage = ImageHelper.FftForImage(h, w, arrayBlurredAndNoises);

                        var filteredBluredImage =
                            fftBlurredImage.Select(l => ImageHelper.DivideTwoComplexsArray(l.ToArray(), complexKernel)).ToList();
                        var filteredBlurredAndNoisesImage = fftBlurredAndNoisesImage.Select(l =>
                                ImageHelper.DivideTwoComplexsArrayForNoisesAndBlurredImage(l.ToArray(), complexKernel))
                            .ToList();

                        foreach (var lf in filteredBlurredAndNoisesImage)
                        {
                            Fourier.Inverse(lf);
                        }
                        foreach (var l in filteredBluredImage)
                        {
                            Fourier.Inverse(l);
                        }
                        //Normalize the blurred Image
                        var t1 = new int[h, w];
                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            t1[x, y] = (int)filteredBluredImage[x][y].Real;
                        }));
                        var globalMax2 = t1.Cast<int>().Max();
                        var globalMin2 = t1.Cast<int>().Min();
                        var normalizedXcr2 = new int[h, w];
                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            normalizedXcr2[x, y] = ((int)t1[x, y] - globalMin2) * 255 / (globalMax2 - globalMin2);
                        }));

                        //Normalize the blurred and noise Image
                        var t = new int[h, w];
                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            t[x, y] = (int)filteredBlurredAndNoisesImage[x][y].Real;
                        }));
                        var globalMax1 = t.Cast<int>().Max();
                        var globalMin1 = t.Cast<int>().Min();
                        var normalizedXcr1 = new int[h, w];

                        Parallel.For(0, h, x => Parallel.For(0, w, y =>
                        {
                            normalizedXcr1[x, y] = ((int)t[x, y] - globalMin1) * 255 / (globalMax1 - globalMin1);
                        }));

                        Bitmap newImage2 = new Bitmap(w, h);
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                var colorPixel = (int)normalizedXcr2[j, i];
                                newImage2.SetPixel(i, j,
                                    Color.FromArgb(colorPixel, colorPixel,
                                        colorPixel));
                            }
                        }
                        Bitmap newImage3 = new Bitmap(w, h);
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                //var colorPixel = (int)arrayBlurredAndNoises[i,j];
                                var colorPixel = normalizedXcr1[j, i];
                                newImage3.SetPixel(i, j,
                                    Color.FromArgb(colorPixel, colorPixel,
                                        colorPixel));
                            }
                        }
                        pictureBox1.Image = newImage2;
                        pictureBox2.Image = newImage3;

                        break;
                    }
                case 13:
                    {
                        //Extract Contour
                        Bitmap contourImage = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap contourImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(contourImage);
                        var imageForHighPass = contourImageForNoise;

                        var lowpassfilter = LPF(0.07, 16, 1);
                        Bitmap newImage = ImageHelper.FilterForImage(contourImageForNoise, lowpassfilter, false);
                        Bitmap imageAfterSubtraction = ImageHelper.ImageSubtraction(contourImageForNoise, newImage);
                        var image = Grayscale.CommonAlgorithms.RMY.Apply(imageAfterSubtraction);
                        // create filter
                        IFilter threshold = new Threshold(100);
                        var test = threshold.Apply(image);
                        // apply the filter
                        pictureBox1.Image = test;
                        break;
                    }
                case 14:
                    {
                        //Extract Contour
                        Bitmap contourImage = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap contourImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(contourImage);

                        var highpassfilter = HighPassFilter(0.07, 16, 1);
                        Bitmap newImageForHighPass = ImageHelper.FilterForImage(contourImageForNoise, highpassfilter, true);
                        //Bitmap imageAfterSubtractionForHighPass = ImageHelper.ImageSubtraction(contourImageForNoise, newImageForHighPass);
                        var image11 = Grayscale.CommonAlgorithms.RMY.Apply(newImageForHighPass);
                        // create filter
                        IFilter threshold = new Threshold(100);
                        var test1 = threshold.Apply(image11);
                        // apply the filter
                        pictureBox2.Image = test1;
                        //pictureBox2.Image = test1;
                        break;
                    }
                case 15:
                    {
                        //1. original, 2. original + noise, grad, laplasian, sobel
                        Bitmap imageForNoise = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap grayImageForNoise = Grayscale.CommonAlgorithms.BT709.Apply(imageForNoise);
                        var grayimage2 = grayImageForNoise;
                        Bitmap newImageEdgeDetected = grayImageForNoise.ConvolutionFilterTwoDimension(gx, gy);
                        pictureBox1.Image = newImageEdgeDetected;
                        pictureBox2.Image = grayimage2;
                        break;
                    }
                case 16:
                    {
                        //extract contour by erosion and dilation
                        Bitmap contourImage = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap contourImageGrayScale = Grayscale.CommonAlgorithms.BT709.Apply(contourImage);
                        var erosionImage = ImageHelper.DilateAndErodeFilter(contourImageGrayScale, 4,
                            ImageHelper.MorphologyType.Erosion, true, true, true,
                            ImageHelper.MorphologyEdgeType.EdgeDetection);
                        var grayImageErosionImage = Grayscale.CommonAlgorithms.RMY.Apply(erosionImage);
                        // create filter
                        IFilter threshold = new Threshold(30);
                        var threshHoldErosionImage = threshold.Apply(grayImageErosionImage);
                        pictureBox2.Image = threshHoldErosionImage;
                        break;
                    }
                case 17:
                    {
                        //extract contour by dilation
                        Bitmap contourImage = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\MODEL1.jpg", true);
                        Bitmap contourImageGrayScale = Grayscale.CommonAlgorithms.BT709.Apply(contourImage);

                        var dilationImage = ImageHelper.DilateAndErodeFilter(contourImageGrayScale, 4,
                            ImageHelper.MorphologyType.Dilation, true, true, true,
                            ImageHelper.MorphologyEdgeType.EdgeDetection);
                        var grayImageDilationImage = Grayscale.CommonAlgorithms.RMY.Apply(dilationImage);
                        // create filter
                        IFilter threshold = new Threshold(30);
                        var threshHoldDilationImage = threshold.Apply(grayImageDilationImage);
                        pictureBox1.Image = threshHoldDilationImage;
                        break;
                    }
                case 18:
                    {
                        //extract contour by dilation
                        Bitmap contourImage = new Bitmap(@"G:\\Russiaa\\2nd Year\\Processing experimental data\\ImageTest\\20190514_144422.jpg", true);
                        Bitmap contourImageGrayScale = Grayscale.CommonAlgorithms.BT709.Apply(contourImage);
                        
                        OtsuThresholding.ApplyOtsuThreshold(ref contourImageGrayScale);
                        var dilationImage = ImageHelper.DilateAndErodeFilter(contourImageGrayScale, 4,
                            ImageHelper.MorphologyType.Dilation, true, true, true,
                            ImageHelper.MorphologyEdgeType.EdgeDetection);
                        var grayImageDilationImage = Grayscale.CommonAlgorithms.RMY.Apply(dilationImage);


                        HoughCircleTransformation circleTransform = new HoughCircleTransformation(9);
                        // apply Hough circle transform
                        circleTransform.ProcessImage(grayImageDilationImage);
                        Bitmap houghCirlceImage = circleTransform.ToBitmap();
                        // get circles using relative intensity
                        HoughCircle[] circles = circleTransform.GetCirclesByRelativeIntensity(81);
                        numStones.Text = circles.Length.ToString();
                        // create filter
                        //IFilter threshold = new Threshold(60);
                        //var threshHoldDilationImage = threshold.Apply(grayImageDilationImage);
                        pictureBox1.Image = grayImageDilationImage;
                        break;
                    }
            }
        }
        private double[] RebuiltFourier(double[] xn, double[] csn, int size)
        {
            var rebuiltSignal = new double[size];
            var xk = new double[size];
            double[] ReXk = new double[size];
            double[] ImXk = new double[size];
            for (int n = 0; n < size; n++)
            {
                for (int k = 0; k < size; k++)
                {
                    xk[n] += xn[n] * Math.Exp((2 * Math.PI * n * k) / size);
                    ReXk[n] += csn[k] * Math.Sin((2 * Math.PI * n * k) / size);
                    ImXk[n] += csn[k] * Math.Cos((2 * Math.PI * n * k) / size);
                }
                rebuiltSignal[n] = ReXk[n] + ImXk[n];
            }

            return rebuiltSignal;
        }
        private double[] TakeRowInMatrix(int[,] inputMatrix, int rowLength, int row = 1)
        {
            return inputMatrix.Cast<int>().Skip(row * rowLength).Take(rowLength).Select(c => (double)c).ToArray();

        }
        private List<Point> InverseCDF(double[] cdfOut)
        {
            var len = cdfOut.Length;
            var listPoint = new List<Point>();
            for (int i = 0; i < len; i++)
            {
                listPoint.Add(new Point(cdfOut[i], i));
            }

            return listPoint;
        }
        public double[] HighPassFilter(double fcut, int m, double dt)
        {
            var hpw = new double[2 * m + 1];
            var lpw = new double[m + 1];
            var fullLowpass = LowPassFilter(fcut, m, dt, out lpw);
            for (int i = 0; i <= 2 * m; i++)
            {
                if (i == m)
                {
                    hpw[i] = 1 - fullLowpass[i];
                }
                else
                {
                    hpw[i] = -fullLowpass[i];
                }
            }
            return hpw;
        }
        public double[] LowPassFilter(double fcut, int m, double dt, out double[] lpw)
        {
            lpw = new double[m + 1];
            double argument = 2 * fcut * dt;
            lpw[0] = argument;
            argument *= Math.PI;
            for (int i = 1; i <= m; i++)
            {
                lpw[i] = Math.Sin(argument * i) / (Math.PI * i);
            }

            lpw[m] /= 2;
            double sumg = lpw[0];
            for (int i2 = 1; i2 <= m; i2++)
            {
                double sum = arrayForLowPass[0];
                argument = Math.PI * i2 / m;
                for (int k = 1; k <= 3; k++)
                {
                    sum += 2 * arrayForLowPass[k] * Math.Cos(argument * k);
                }
                lpw[i2] *= sum;
                sumg += 2 * lpw[i2];
            }

            for (int i = 0; i <= m; i++)
            {
                lpw[i] /= sumg;
            }
            var reverseArray = lpw.Reverse();
            var finalLowpassArray = reverseArray.Concat(lpw.Skip(1)).ToArray();

            return finalLowpassArray;
        }
        public double[] BandStopFilter(double fcut1, double fcut2, int m, double dt)
        {
            var lpw1 = new double[m + 1];
            var lpw2 = new double[m + 1];
            var bsw = new double[2 * m + 1];
            var fullLowpass1 = LowPassFilter(fcut1, m, dt, out lpw1);
            var fullLowpass2 = LowPassFilter(fcut2, m, dt, out lpw2);
            for (int i = 0; i < 2 * m; i++)
            {
                if (i == m)
                {
                    bsw[i] = 1 + (fullLowpass1[i] - fullLowpass2[i]);
                }
                else
                {
                    bsw[i] = fullLowpass1[i] - fullLowpass2[i];
                }
            }
            return bsw;
        }
        public double[] BandPassFilter(double fcut1, double fcut2, int m, double dt)
        {
            var lpw1 = new double[m + 1];
            var lpw2 = new double[m + 1];
            var bpw = new double[2 * m + 1];
            var fullLowpass1 = LowPassFilter(fcut1, m, dt, out lpw1);
            var fullLowpass2 = LowPassFilter(fcut2, m, dt, out lpw2);
            for (int i = 0; i <= 2 * m; i++)
            {
                bpw[i] = fullLowpass2[i] - fullLowpass1[i];
            }
            return bpw;
        }

        public static double[] LPF(double fc, int M, double dt)
        {
            double[] lpw = new double[M + 1];
            double[] d = { 0.35577019, 0.24369830, 0.07211497, 0.00630165 };

            double arg = 2 * fc * dt;
            lpw[0] = arg;
            arg *= Math.PI;

            for (int i = 1; i <= M; i++)
            {
                lpw[i] = Math.Sin(arg * i) / (Math.PI * i);
            }

            lpw[M] /= 2;
            double sumg = lpw[0];
            for (int i = 1; i <= M; i++)
            {
                double sum = d[0];
                arg = Math.PI * i / M;
                for (int k = 1; k <= 3; k++) { sum += 2 * d[k] * Math.Cos(arg * k); }
                lpw[i] *= sum;
                sumg += 2 * lpw[i];
            }

            for (int i = 0; i <= M; i++) { lpw[i] /= sumg; }

            double[] lpw_result = new double[2 * M + 1];
            double[] f = new double[M + 1];
            for (int i = M; i <= 2 * M; i++)
            {
                lpw_result[i] = lpw[i - M];
                int t = i - M;
                f[M - t] = lpw[t];
            }

            double[] l = new double[2 * M + 1];
            for (int i = 0; i < 2 * M + 1; i++)
            {
                if (i < M + 1) { l[i] = f[i]; }
                else { l[i] = lpw_result[i]; }
            }

            return l;
        }
        public double[] BSPF(double fc1, double fc2, int M, double dt)
        {
            double[] lpw1 = LPF(fc1, M, dt);
            double[] lpw2 = LPF(fc2, M, dt);
            double[] bspw = new double[lpw1.Count()];
            for (int i = 0; i < lpw1.Count(); i++)
            {
                if (i == M) { bspw[i] = 1 + lpw1[i] - lpw2[i]; }
                else { bspw[i] = lpw1[i] - lpw2[i]; }
            }
            return bspw;
        }
        //Fast fourier using library
        public void PlotFFt(int numSamples, int sampleRate, double[] signalarr, int his = 0)
        {
            samples = new Complex[numSamples];
            spectrum = new Complex[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                samples[i] = new Complex(signalarr[i], 0);
            }

            Fourier.Forward(samples, FourierOptions.NoScaling);
            double hzPersample = (double)sampleRate / numSamples;
            double mag = 0f;
            if (his == 1)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    mag = (1.0f / sampleRate) * Math.Abs(Math.Sqrt(Math.Pow(samples[i].Real, 2) + Math.Pow(samples[i].Imaginary, 2)));
                    spectrum[i] = new Complex(mag, 0);
                    chartCollumn.Series[0].Points.AddXY(hzPersample * i, mag);
                }
            }
            else if (his == 2)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    mag = (1.0f / sampleRate) * Math.Abs(Math.Sqrt(Math.Pow(samples[i].Real, 2) + Math.Pow(samples[i].Imaginary, 2)));
                    spectrum[i] = new Complex(mag, 0);
                    chart4.Series[0].Points.AddXY(hzPersample * i, mag);
                }
            }
            else
            {
                for (int i = 0; i < numSamples; i++)
                {
                    mag = (1.0f / sampleRate) * Math.Abs(Math.Sqrt(Math.Pow(samples[i].Real, 2) + Math.Pow(samples[i].Imaginary, 2)));
                    spectrum[i] = new Complex(mag, 0);
                    chartHistogram.Series[0].Points.AddXY(hzPersample * i, mag);
                }
            }

        }
        //Slow Fourier using my implement
        public double[] SlowFourierTransform(int size, double[] signalArray, out double[] CsN, out double[] Xn)
        {
            Xn = new double[size];
            double[] ReXn = new double[size];
            double[] ImXn = new double[size];
            double[] Cn = new double[size];
            CsN = new double[size];
            Parallel.For(0, size, n =>
            {
                for (int k = 0; k < size; k++)
                {
                    //Xn[n] += signalArray[k] * Math.Exp((2 * Math.PI * n * k) / size);
                    ReXn[n] += signalArray[k] * Math.Sin((2 * Math.PI * n * k) / size);
                    ImXn[n] += signalArray[k] * Math.Cos((2 * Math.PI * n * k) / size);
                }

                //Xn[n] /= size;
                ReXn[n] /= size;
                ImXn[n] /= size;
                Cn[n] = Math.Sqrt(ReXn[n] * ReXn[n] + ImXn[n] * ImXn[n]);
                //CsN[n] = ReXn[n] + ImXn[n];
            });
            return Cn;
        }
        public double[] SlowFourierTransformOri(int size, double[] signalArray, out double[] CsN, out double[] Xn)
        {
            Xn = new double[size];
            double[] ReXn = new double[size];
            double[] ImXn = new double[size];
            double[] Cn = new double[size];
            CsN = new double[size];
            for (int n = 0; n < size; n++)
            {
                for (int k = 0; k < size; k++)
                {
                    Xn[n] += signalArray[k] * Math.Exp((2 * Math.PI * n * k) / size);
                    ReXn[n] += signalArray[k] * Math.Sin((2 * Math.PI * n * k) / size);
                    ImXn[n] += signalArray[k] * Math.Cos((2 * Math.PI * n * k) / size);
                }

                Xn[n] /= size;
                ReXn[n] /= size;
                ImXn[n] /= size;
                Cn[n] = Math.Sqrt(ReXn[n] * ReXn[n] + ImXn[n] * ImXn[n]);
                CsN[n] = ReXn[n] + ImXn[n];
            }
            return Cn;
        }
        int[,] GetTwoDimensionArrayForImage(Bitmap inputImage, int deltaY = 1)
        {
            var imgWidth = inputImage.Width;
            var imgHeight = inputImage.Height;
            var outPut2DArray = new int[imgWidth, imgHeight / deltaY];
            for (int x = 0; x < imgWidth; x++)
            {
                int j = 0;
                for (int y = 0; y < imgHeight; y += deltaY)
                {
                    outPut2DArray[x, j++] = inputImage.GetPixel(x, y).R;
                }
            }

            return outPut2DArray;
        }
        public double[] GetCDF(Bitmap inputImage)
        {
            var his = GetHistogram(inputImage);
            //double[] probabilityArray = his.Select(c => (double)c / (grayImage.Width * grayImage.Height)).ToArray();
            var cdf = new double[256];
            var hisEqualized = new double[256];
            for (int grayLevel = 0; grayLevel < 256; grayLevel++)
            {
                for (int i = 0; i < grayLevel; i++)
                {
                    cdf[grayLevel] += his[i];
                }
            }

            return cdf;
        }
        public long[] GetHistogram(System.Drawing.Bitmap picture)
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < picture.Size.Width; i++)
                for (int j = 0; j < picture.Size.Height; j++)
                {
                    System.Drawing.Color c = picture.GetPixel(i, j);

                    long Temp = 0;
                    Temp += c.R;
                    Temp += c.G;
                    Temp += c.B;

                    Temp = (int)Temp / 3;
                    myHistogram[Temp]++;
                }

            return myHistogram;
        }
        private Bitmap GrayscaleImage(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            BitmapData sd = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            img.UnlockBits(sd);
            int current = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    current = y * sd.Stride + x * 4;
                    int gray = (int)(buffer[current] * 0.0722 + buffer[current + 1] * 0.7152 + buffer[current + 2] * 0.2126);
                    for (int i = 0; i < 3; i++)
                    {
                        result[current + i] = (byte)gray;
                    }
                    result[current + 3] = 255;
                }
            }
            Bitmap resimg = new Bitmap(w, h);
            BitmapData rd = resimg.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, rd.Scan0, bytes);
            resimg.UnlockBits(rd);
            return resimg;
        }
        private Bitmap LogTransform(Bitmap img, int constant)
        {
            int w = img.Width;
            int h = img.Height;
            img = GrayscaleImage(img);

            BitmapData sd = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            img.UnlockBits(sd);
            int current = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    current = y * sd.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[current + i] = (byte)(constant * Math.Log10(buffer[current + i] + 1));
                    }
                    result[current + 3] = 255;
                }
            }
            Bitmap resimg = new Bitmap(w, h);
            BitmapData rd = resimg.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, rd.Scan0, bytes);
            resimg.UnlockBits(rd);
            return resimg;
        }
        public Bitmap ResizingImageNearestNeighbor(Bitmap originalImage, double xScale, double yScale, double[] oneDimensionImageArray)
        {
            var originalImageWidth = originalImage.Width;
            var originalImageHeight = originalImage.Height;
            var scaledImage = ResizePixelsNearestNeighbor2(oneDimensionImageArray, originalImageWidth, originalImageHeight, (int)(originalImageWidth * xScale),
                (int)(originalImageHeight * yScale));
            var twoDiScaledImage = SingleToMulti(scaledImage, (int)(originalImageWidth * xScale), (int)(originalImageHeight * yScale));
            Bitmap outImage = new Bitmap((int)(originalImageWidth * xScale), (int)(originalImageHeight * yScale));
            for (int x = 0; x < twoDiScaledImage.GetLength(0); x++)
            {
                var total = 0;
                for (int y = 0; y < twoDiScaledImage.GetLength(1); y++)
                {
                    Color newColor = Color.FromArgb((int)twoDiScaledImage[x, y], (int)twoDiScaledImage[x, y], (int)twoDiScaledImage[x, y]);
                    outImage.SetPixel(x, y, newColor);
                }
            }
            return outImage;
        }
        public double[,] SingleToMulti(double[] array, int width, int height)
        {
            int index = 0;
            int sqrt = (int)Math.Sqrt(array.Length);
            double[,] multi = new double[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    multi[x, y] = array[index];
                    index++;
                }
            }
            return multi;
        }

        public int[] ResizePixelsNearestNeighbor(int[] pixels, int w1, int h1, int w2, int h2)
        {
            int[] temp = new int[w2 * h2];
            double x_ratio = w1 / (double)w2;
            double y_ratio = h1 / (double)h2;
            double px, py;
            for (int i = 0; i < h2; i++)
            {
                for (int j = 0; j < w2; j++)
                {
                    px = Math.Floor(j * x_ratio);
                    py = Math.Floor(i * y_ratio);
                    temp[(i * w2) + j] = pixels[(int)((py * w1) + px)];
                }
            }
            return temp;
        }
        public double[] ResizePixelsNearestNeighbor2(double[] pixels, int w1, int h1, int w2, int h2)
        {
            double[] temp = new double[w2 * h2];
            // EDIT: added +1 to account for an early rounding problem
            int x_ratio = (int)((w1 << 16) / w2) + 1;
            int y_ratio = (int)((h1 << 16) / h2) + 1;
            //int x_ratio = (int)((w1<<16)/w2) ;
            //int y_ratio = (int)((h1<<16)/h2) ;
            int x2, y2;
            for (int i = 0; i < h2; i++)
            {
                for (int j = 0; j < w2; j++)
                {
                    x2 = ((j * x_ratio) >> 16);
                    y2 = ((i * y_ratio) >> 16);
                    temp[(i * w2) + j] = pixels[(y2 * w1) + x2];
                }
            }
            return temp;
        }
        //Event
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "JPEG|*.jpg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    pictureBox1.Refresh();
                    originalImage = pictureBox1.Image;
                    _choice = 1;
                    Form1_Load(sender, e);
                }
            }
        }
        public void DrawAnythingOnCharFour(double[] arrayTodraw, string series)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chart4.Series[series].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingOnCharRow(double[] arrayTodraw, string series)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chartRow.Series[series].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingOnCollumn(double[] arrayTodraw, string series)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chartCollumn.Series[series].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingWithCustomList(List<Point> arrayTodraw, string series)
        {
            var length = arrayTodraw.Count;
            for (int i = 0; i < length; i++)
            {
                chartCollumn.Series[series].Points.AddXY(arrayTodraw[i].x, arrayTodraw[i].y);
            }
        }
        public void DrawHistogram(double[] signal, int size)
        {
            //Their algorithm
            var his = new Histogram(signal, size);
            for (int i = 0; i < size; i++)
            {
                chartHistogram.Series[0].Points.AddXY(i, his[i].Count);
            }
        }

        private void btnNearest_Click(object sender, EventArgs e)
        {
            _choice = 1;
            ClearGraph(sender, e);
        }
        private void btnBilinear_Click(object sender, EventArgs e)
        {
            _choice = 2;
            ClearGraph(sender, e);
        }
        private void btnNegative_Click(object sender, EventArgs e)
        {
            _choice = 3;
            ClearGraph(sender, e);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _choice = 4;
            ClearGraph(sender, e);
        }
        private void btnLogTransform_Click(object sender, EventArgs e)
        {
            _choice = 5;
            ClearGraph(sender, e);
        }
        private void btnHisEqual_Click(object sender, EventArgs e)
        {
            _choice = 6;
            ClearGraph(sender, e);
        }
        private void xrayButton_Click(object sender, EventArgs e)
        {
            _choice = 7;
            ClearGraph(sender, e);
        }
        private void ClearGraph(object sender, EventArgs e)
        {
            foreach (var series in chartRow.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartCollumn.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartHistogram.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart4.Series)
            {
                series.Points.Clear();
            }
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            Form1_Load(sender, e);
        }

        private void btnAddNoises_Click(object sender, EventArgs e)
        {
            _choice = 8;
            ClearGraph(sender, e);
        }

        private void btnSalfPepper_Click(object sender, EventArgs e)
        {
            _choice = 9;
            ClearGraph(sender, e);
        }

        private void btnSumNoise_Click(object sender, EventArgs e)
        {
            _choice = 10;
            ClearGraph(sender, e);
        }

        private void btnContourLPF_Click(object sender, EventArgs e)
        {
            _choice = 13;
            ClearGraph(sender, e);
        }

        private void btnContourHPF_Click(object sender, EventArgs e)
        {
            _choice = 14;
            ClearGraph(sender, e);
        }

        private void btnErosion_Click(object sender, EventArgs e)
        {
            _choice = 16;
            ClearGraph(sender, e);
        }

        private void btnDilation_Click(object sender, EventArgs e)
        {
            _choice = 17;
            ClearGraph(sender, e);
        }

        private void btnBlurred_Click(object sender, EventArgs e)
        {
            _choice = 12;
            ClearGraph(sender, e);
        }
    }

    public class Point
    {
        public double x;
        public double y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
