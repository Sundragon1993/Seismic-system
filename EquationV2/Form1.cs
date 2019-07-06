using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using Accord.Math;
using MathNet.Filtering;
using MathNet.Filtering.FIR;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.LinearAlgebra.Double;
using NAudio.Utils;
using NAudio.Wave;
using StreamCoders.Container;
using StreamCoders.Wave;

namespace EquationV2
{
    public partial class Form1 : Form
    {
        public Random a = new Random();
        private double beta = 0f, alpha = 0f;
        private double _k = 0f;
        private int _c = 1;
        private double _kFirstLine = 0f;
        private int _myNumber = 0;
        private List<int> UsedList = new List<int>();
        private float[] _rndArray = new float[1000];
        private double[] h2 = new double[1000];
        private double _epsilon = 0f;
        private double _epsilonForFirstLine = 0f;
        private int _choice = 15;
        private float _xMin = 0f, _xMax = 0f;
        private double _xMid = 0.0f;
        private double _disp = 0.0f;
        private double _threeNail = 0.0f;
        private double _muy3;
        private double _muy4;
        private Complex[] samples = new Complex[257];
        private Complex[] spectrum = new Complex[1000];
        private double[] _arrayForAntiSpike;
        private double[] _trendArray;
        private int _checkDoubleClick = 0;
        private int _checkDoubleClickForStandardDeviation = 2;
        private bool _checkForHarmony = false;
        private double[] _extractedTrend;
        private double[] _trendAndRandomArray;
        private double[] _sineAndNegativeEpsilon;
        private double[] _pitchLine;
        private double[] arrayForLowPass = { 0.35577019, 0.2436983, 0.07211497, 0.00630165 };
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        private SeismicForm seismicForm;
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 1000);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.Series[0].BorderWidth = 3;

            chartAutoCorr.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
            chartAutoCorr.ChartAreas[0].AxisX.ScaleView.Zoom(0, 1000);
            chartAutoCorr.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartAutoCorr.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartAutoCorr.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartAutoCorr.Series[0].BorderWidth = 3;
            chartAutoCorr.Series[0].ChartType = SeriesChartType.Spline;
            chartAutoCorr.Series[1].ChartType = SeriesChartType.Spline;
            chartAutoCorr.Series[1].Color = Color.YellowGreen;
            chartAutoCorr.Series[0].ChartArea = "ChartArea1";
            chartAutoCorr.Series[1].ChartArea = "ChartArea1";
            chartAutoCorr.Series[0].BorderWidth = 3;
            chartAutoCorr.Series[1].BorderWidth = 3;


            chartHis.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
            chartHis.ChartAreas[0].AxisX.ScaleView.Zoom(0, 1000);
            chartHis.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartHis.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartHis.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartHis.Series[0].BorderWidth = 3;
            chartHis.Series[0].ChartType = SeriesChartType.Column;
            chartHis.Series[0].ChartArea = "ChartArea1";
            chartHis.Series[0].BorderWidth = 3;

            chartFourier.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
            chartFourier.ChartAreas[0].AxisX.ScaleView.Zoom(0, 1000);
            chartFourier.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartFourier.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartFourier.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartFourier.Series[0].BorderWidth = 3;
            chartFourier.Series[0].ChartType = SeriesChartType.Column;
            chartFourier.Series[0].ChartArea = "ChartArea1";
            chartFourier.Series[0].BorderWidth = 3;
            chartFourier.Series[0].Color = Color.Magenta;

            chart1.Series["Series0"].ChartType = SeriesChartType.Line;
            chart1.Series["Series0"].ChartArea = "ChartArea1";
            chart1.Series["Series0"].BorderWidth = 1;

            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            chart1.Series["Series1"].ChartArea = "ChartArea1";
            chart1.Series["Series1"].BorderWidth = 1;

            chart1.Series["Series2"].ChartType = SeriesChartType.Line;
            chart1.Series["Series2"].ChartArea = "ChartArea1";
            chart1.Series["Series2"].BorderWidth = 3;

            chart1.Series["Series3"].ChartType = SeriesChartType.Line;
            chart1.Series["Series3"].ChartArea = "ChartArea1";
            chart1.Series["Series3"].BorderWidth = 3;

            chart1.Series["Series4"].ChartType = SeriesChartType.Line;
            chart1.Series["Series4"].ChartArea = "ChartArea1";
            chart1.Series["Series4"].BorderWidth = 1;
            chart1.Series["Series4"].Color = Color.OliveDrab;

            chart1.Series["Series5"].ChartType = SeriesChartType.Line;
            chart1.Series["Series5"].ChartArea = "ChartArea1";
            chart1.Series["Series5"].BorderWidth = 1;

            chart1.Series["Series6"].ChartType = SeriesChartType.Line;
            chart1.Series["Series6"].ChartArea = "ChartArea1";
            chart1.Series["Series6"].BorderWidth = 1;

            BuildRandomValues();
        }

        void BuildRandomValues()
        {
            for (int i = 0; i < 1000; i++)
            {
                _rndArray[i] = (float)a.NextDouble();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IntervalDetection();
            switch (_choice)
            {
                case 1:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);

                        for (int i = 0; i <= 1000; i += 200)
                        {
                            chart1.Series[0].Points.AddXY(i, _functionForRandomPositiveLine(i));
                        }

                        break;
                    }
                case 2:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);

                        for (int i = 0; i <= 1000; i += 200)
                        {
                            chart1.Series[0].Points.AddXY(i, _functionForRandomNegativeLine(i));
                        }

                        break;
                    }
                case 3:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);

                        for (int i = -2000; i <= 1000; i += 100)
                        {
                            chart1.Series[0].Points.AddXY(i, _functionForRandomNegativeEpsilon(i));
                        }

                        break;
                    }
                case 4:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);

                        for (int i = -1000; i <= 1000; i += 200)
                        {
                            chart1.Series[0].Points.AddXY(i, _functionForRandomPositiveEpsilon(i));
                        }

                        break;
                    }
                //Final trend
                case 5:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        chart1.Series["Series2"].ChartType = SeriesChartType.Spline;
                        var trendArray = new double[1000];
                        for (int i = 0; i < 1000; i += 1)
                        {
                            if (i < 250)
                            {
                                chart1.Series["Series4"].Points.AddXY(i, _functionForPositiveLine(i));
                                trendArray[i] = _functionForPositiveLine(i);
                            }
                            else if (i > 250 && i <= 500)
                            {
                                chart1.Series["Series3"].Points.AddXY(i, _functionForNegativeEpsilon(i) + _epsilon);
                                trendArray[i] = _functionForNegativeEpsilon(i) + _epsilon;
                            }
                            else if (i >= 500 && i <= 750)
                            {
                                chart1.Series[0].Points.AddXY(i, _functionForNegativeLine(i));
                                trendArray[i] = _functionForNegativeLine(i);
                            }
                            else if (i >= 750)
                            {
                                chart1.Series["Series2"].Points.AddXY(i, _functionForPositiveEpsilon(i));
                                trendArray[i] = _functionForPositiveEpsilon(i);
                            }
                        }

                        _trendArray = trendArray;
                        DrawHistogram(trendArray, 1000);
                        DrawAutoCorrelation(trendArray, 1000);
                        MyDrawAutoCorrelation(trendArray, 1000);
                        PlotFFt(1000, 1000, trendArray);
                        break;
                    }
                //Random done
                case 6:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        var randomArray = new double[1000];
                        chart1.Series[0].ChartType = SeriesChartType.Column;
                        //_xMax = _rndArray.Max();
                        //_xMin = _rndArray.Min();

                        int size = 1000;
                        var notNativeRandomArray = NotNativeRandom(1000);
                        _xMax = (float)notNativeRandomArray.Max();
                        _xMin = (float)notNativeRandomArray.Min();
                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series[0].Points.AddXY(i, _functionForSpikeFormulaV1(notNativeRandomArray[i], 10));
                            randomArray[i] = _functionForSpikeFormulaV1(notNativeRandomArray[i], 10);
                        }

                        DrawHistogram(randomArray, size);
                        DrawAutoCorrelation(randomArray, size);
                        MyDrawAutoCorrelation(randomArray, size);
                        PlotFFt(size, size, randomArray);
                        //Old method
                        //for (int i = 0; i < 1000; i++)
                        //{
                        //    chart1.Series[0].Points.AddXY(i, _functionForSpikeFormulaV1(_rndArray[i]));
                        //    randomArray[i] = _functionForSpikeFormulaV1(_rndArray[i]);
                        //}
                        break;
                    }
                //Shift
                case 7:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        chart1.Series["Series2"].ChartType = SeriesChartType.Spline;
                        var originalArray = new double[1000];
                        for (int i = 0; i < 1000; i += 10)
                        {
                            chart1.Series[0].Points.AddXY(i, Math.Sin(i) + 5);
                            originalArray[i] = Math.Sin(i) + 5;
                        }

                        var afterShiftArray = ShiftArray(-10, originalArray);
                        for (int i = 0; i < 1000; i += 10)
                        {
                            chart1.Series["Series2"].Points.AddXY(i, afterShiftArray[i]);
                        }

                        break;
                    }
                //Spike
                case 8:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        chart1.Series["Series2"].ChartType = SeriesChartType.Spline;
                        var y = new double[1000];
                        var yy = new double[1000];
                        for (int i = 0; i < 1000; i++)
                        {
                            y[i] = (float)Math.Sin(i * Math.PI / 100);
                        }

                        var spikeArray = Spikes(0.006, 100, 0.7, 0.2, y);

                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        _rndArray[0] = (float)Math.Cos(DateTime.Now.Millisecond);
                        for (int i = 1; i < 1000; i++)
                        {
                            _rndArray[i] = (float)Math.Cos(_rndArray[i - 1] * 1000);
                        }

                        _xMax = _rndArray.Max();
                        _xMin = _rndArray.Min();
                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["Series2"].Points.AddXY(i, spikeArray[i]);
                            yy[i] = y[i] + spikeArray[i];
                        }

                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["Series3"].Points.AddXY(i, yy[i] + 5);
                        }

                        DrawHistogram(yy, 1000);
                        DrawAutoCorrelation(yy, 1000);
                        MyDrawAutoCorrelation(yy, 1000);
                        PlotFFt(1000, 1000, yy);
                        break;
                    }
                //Fast Fourier Transform V1 using my algorithm now became harmony
                case 9:
                    {
                        //Harmony process
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-100, 100);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        chart1.Series["Series2"].ChartType = SeriesChartType.RangeColumn;
                        int size = 1000;

                        var harmonyArray = HarmonyProcess(0.002f, 300, 37, -1, size);
                        var csn = new double[size];
                        var xn = new double[size];
                        var dftArray = SlowFourierTransform(size, harmonyArray, out csn, out xn);

                        //double[] rebuiltSignal = RebuiltFourier(xn, csn, size);

                        for (int i = 0; i < size; i++)
                        {
                            chart1.Series["Series3"].Points.AddXY(i, harmonyArray[i]);
                        }

                        DrawHistogram(harmonyArray, size);
                        DrawAutoCorrelation(harmonyArray, size);
                        MyDrawAutoCorrelation(harmonyArray, size);
                        //PlotFFt(size, size, harmonyArray);
                        DrawAnythingOnFourier(dftArray.Length / 2, dftArray.Length, dftArray);
                        break;
                    }
                //Statistics
                case 10:
                    {
                        //xMax = _rndArray.Max();
                        //xMin = _rndArray.Min();
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-0.75, 0.75);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Column;
                        var arrtestNative = BuildRandomNativeValues(100000);
                        var arrtestNotNative = NotNativeRandom(100000);
                        bool station = Station(arrtestNative, 10);
                        bool station2 = Station(arrtestNotNative, 10);

                        if (station)
                            textBox1.Text = @"Input array is station";
                        else if (!station)
                        {
                            textBox1.Text = @"Input array is not station";
                        }

                        var mean_X = MeanX(arrtestNative, arrtestNative.Length);
                        var var_X = VarXX(arrtestNative, arrtestNative.Length);
                        var CO_X = StandardX(arrtestNative, arrtestNative.Length);
                        var CK_X = CK(arrtestNative, arrtestNative.Length);
                        var CKO_X = CKO(arrtestNative, arrtestNative.Length);
                        var M3_X = M3(arrtestNative, arrtestNative.Length);
                        var M4_X = M4(arrtestNative, arrtestNative.Length);
                        var V1_X = V1(arrtestNative, arrtestNative.Length);
                        var V2_X = V2(arrtestNative, arrtestNative.Length);

                        chart1.Series["Series3"].Points.AddXY("Mean Value", mean_X);
                        chart1.Series["Series3"].Points[0].Label = mean_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("Varience", var_X);
                        chart1.Series["Series3"].Points[1].Label = var_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("StandardX", CO_X);
                        chart1.Series["Series3"].Points[2].Label = CO_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("CK", CK_X);
                        chart1.Series["Series3"].Points[3].Label = CK_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("CKO", CKO_X);
                        chart1.Series["Series3"].Points[4].Label = CKO_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("M3", M3_X);
                        chart1.Series["Series3"].Points[5].Label = M3_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("M4", M4_X);
                        chart1.Series["Series3"].Points[6].Label = M4_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("Skewness", V1_X);
                        chart1.Series["Series3"].Points[7].Label = V1_X.ToString();

                        chart1.Series["Series3"].Points.AddXY("Kurtosis", V2_X);
                        chart1.Series["Series3"].Points[8].Label = V2_X.ToString();
                        break;
                    }
                //Auto correlation
                case 11:
                    {
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        chart1.Series["Series2"].ChartType = SeriesChartType.Spline;
                        /*For spike function*/
                        var y = new double[1000];
                        var yy = new double[1000];
                        for (int i = 0; i < 1000; i++)
                        {
                            y[i] = (float)Math.Sin(i * Math.PI / 100);
                        }

                        double[] spikeArray = Spikes(0.006, 100, 0.7, 0.2, y);
                        /*End for spike function*/
                        for (int i = 0; i < 1000; i++)
                        {
                            yy[i] = y[i] + spikeArray[i];
                        }

                        int size = 1000;
                        double[] arrtestNative = RandomSine(size);
                        double[] arrtestNativev2 = NotNativeRandom(size);
                        double[] harmony = HarmonyProcess(0.002f, 300, 37, -1, 1000);
                        var signalarr = ReadDatFile().ToArray();

                        //My algorithm
                        var autoCorrelationArray = AutoCorrelationV2(yy, 0, 1000);
                        //Using library
                        var aut = Correlation.Auto(yy);
                        PlotFFt(autoCorrelationArray.Length, autoCorrelationArray.Length, autoCorrelationArray);

                        for (int i = 0; i < size; i++)
                        {
                            //chart1.Series["Series3"].Points.AddXY(i, arrtestNative[i] + 5);
                            chart1.Series["Series2"].Points.AddXY(i, aut[i]);
                        }

                        for (int j = 0; j < size; j++)
                        {
                            chart1.Series[0].Points.AddXY(j, autoCorrelationArray[j] - 5);
                        }

                        //chart1.Series["Series3"].Points[0].Label = "Signal";
                        chart1.Series["Series2"].Points[0].Label = "Auto correlation";
                        chart1.Series[0].Points[0].Label = "My Auto correlation";

                    }
                    break;
                //Cross correlation
                case 12:
                    {
                        int size = 250;


                        double fs = 250; //sampling rate, Hz
                        double te = 1; //end time, seconds
                        int size1 = (int)(fs * te); //sample size
                        var t = Enumerable.Range(0, size1).Select(p => p / fs).ToArray();
                        var y1 = t.Select(p => p < te / 2 ? 1.0 : 0).ToArray();
                        var y2 = t.Select(p => p < te / 2 ? 1.0 - 2 * p : 0).ToArray();

                        double[] arrtestNative = RandomSine(size);
                        double[] arrtestNative1 = RandomSine(size);
                        int minDelay = -size / 2;
                        int maxDelay = size / 2;
                        var crossArray = StatHelper.CrossCorrelation(size, y2, y1, minDelay, maxDelay);

                        for (int i = 0; i < size; i++)
                        {
                            chart1.Series["Series3"].Points.AddXY(i, y1[i] + 10);
                            chart1.Series["Series2"].Points.AddXY(i, y2[i] + 5);
                            chart1.Series["Series4"].Points.AddXY(i, crossArray[i]);
                        }

                    }
                    break;
                //Built in fast fourier read from file
                case 13:
                    {
                        int size = 1000;
                        var datArray = ReadDatFile();
                        PlotFFt(1000, 1000, datArray.ToArray());
                        DrawAutoCorrelation(datArray.ToArray(), size);
                        MyDrawAutoCorrelation(datArray.ToArray(), size);
                        DrawHistogram(datArray.ToArray(), size);

                        Fourier.Inverse(spectrum, FourierOptions.NoScaling);
                        for (int i = 0; i < size; i++)
                        {
                            //chart1.Series["Series3"].Points.AddXY(i, dftArray[i]);
                            chart1.Series["Series2"].Points.AddXY(i, spectrum[i].Real);
                        }

                        break;
                    }
                //Signal
                case 14:
                    {
                        //Harmony process
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10, 10);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Line;
                        chart1.Series["Series3"].Color = Color.Blue;
                        chart1.Series["Series2"].Color = Color.DeepPink;
                        chart1.Series["Series2"].ChartType = SeriesChartType.Line;
                        chart1.Series["Series2"].BorderWidth = 1;
                        chart1.Series["Series3"].BorderWidth = 1;
                        int size = 1000;
                        //var harmonyArray = HarmonyProcess(0.002f, 300, 37, -1, size);
                        var harmonyArrayv2 = HarmonyProcessV2(0.002f, size);

                        var randomArray = BuildRandomNativeValues(1000);
                        _xMax = (float)randomArray.Max();
                        _xMin = (float)randomArray.Min();
                        //using his random
                        double[] test = randomArray.Select(c => _functionForSpikeFormulaV1(c, 10)).ToArray();
                        var signalAndSpike = new double[size];
                        var final = new double[size];


                        var spikeArray = Spikes(0.006, 100, 0.7, 0.5, harmonyArrayv2);
                        var randomAndSpikeArray = new double[size];
                        for (int i = 0; i < 1000; i++)
                        {
                            //signalAndSpike[i] = harmonyArrayv2[i] + spikeArray[i]*2;
                            randomAndSpikeArray[i] = test[i] + spikeArray[i] * 15;
                            final[i] = randomArray[i] / 2 + harmonyArrayv2[i] + spikeArray[i] * 1.5;
                        }

                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["Series2"].Points.AddXY(i, final[i]);
                            //chart1.Series["Series3"].Points.AddXY(i, randomAndSpikeArray[i]);
                        }

                        _arrayForAntiSpike = final;
                        DrawHistogram(final, size);
                        DrawAutoCorrelation(final, size);
                        MyDrawAutoCorrelation(final, size);
                        PlotFFt(size, size, final);
                        break;
                    }
                //Rebuilt signal
                case 15:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-10000, 10000);
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        chart1.Series["Series2"].ChartType = SeriesChartType.Spline;
                        int size = 1000;
                        var harmonyArray = HarmonyProcess(0.002f, 300, 37, -1, size);
                        var csn = new double[size];
                        var xn = new double[size];
                        var signalarr = ReadDatFile().ToArray();
                        var dftArray = SlowFourierTransform(size, signalarr, out csn, out xn);
                        double[] rebuiltSignal = RebuiltFourier(xn, csn, size);
                        DrawAnything(rebuiltSignal, "Series2");
                        break;
                    }
                //Rebuilt signal v2 using library
                case 16:
                    {
                        Fourier.Inverse(spectrum, FourierOptions.NoScaling);
                        int size = 1000;
                        for (int i = 0; i < size; i++)
                        {
                            //chart1.Series["Series3"].Points.AddXY(i, dftArray[i]);
                            chart1.Series["Series2"].Points.AddXY(i, spectrum[i].Real);
                        }

                        break;
                    }
                //Test Histogram
                case 17:
                    {
                        chart1.Series["Series2"].ChartType = SeriesChartType.Column;
                        chart1.Series["Series3"].ChartType = SeriesChartType.Spline;
                        int size = 1000;
                        var randomSineArray = HarmonyProcessForHistogram(0.001f, 4, 15, 1, 1);

                        var randomArray = new double[1000];
                        var notNativeRandomArray = NotNativeRandom(1000);
                        _xMax = (float)notNativeRandomArray.Max();
                        _xMin = (float)notNativeRandomArray.Min();
                        for (int i = 0; i < 1000; i++)
                        {
                            //chart1.Series["Series2"].Points.AddXY(i, _functionForSpikeFormulaV1(notNativeRandomArray[i]));
                            randomArray[i] = _functionForSpikeFormulaV1(notNativeRandomArray[i], 10);
                        }

                        //Their algorithm
                        var his = new Histogram(randomArray, size);
                        //using my algorithm
                        var bucket2 = StatHelper.Bucketize(randomSineArray, size);
                        for (int i = 0; i < size; i++)
                        {
                            chart1.Series["Series2"].Points.AddXY(i, his[i].Count);
                        }

                        break;
                    }
                //Fluctuation
                case 18:
                    {
                        break;
                    }
                //Anti Shift
                case 19:
                    {
                        int size = 1000;
                        int border = 10;
                        var randomArray = new double[size];
                        var afterAntiShiftArray = new double[size];
                        var testShiftArray = new double[size];
                        randomArray = RandomWithBorder(border, size);
                        testShiftArray = ShiftArray(3, randomArray);
                        afterAntiShiftArray = AntiShift(size, testShiftArray);

                        DrawAnything(randomArray, "Series2");
                        DrawAnything(afterAntiShiftArray, "Series3");
                        break;
                    }
                //Anti Spike
                case 20:
                    {
                        int size = 1000;
                        int border = 10;
                        var randomArray = new double[size];
                        var afterAntiSpikeArray = new double[size];
                        var testShiftArray = new double[size];
                        var spikeArray = new double[size];
                        var spikeAndRandomArray = new double[size];

                        randomArray = RandomWithBorder(border, size);
                        spikeArray = Spikes(0.006, 100, 0.7, 0.5, randomArray);

                        for (int i = 0; i < size; i++)
                        {
                            spikeAndRandomArray[i] = randomArray[i] + spikeArray[i] * 10;
                        }

                        DrawAnything(spikeAndRandomArray, "Series2");

                        afterAntiSpikeArray = AntiSpike(size, spikeAndRandomArray, border);

                        chart1.Series["Series3"].BorderWidth = 3;
                        DrawAnything(afterAntiSpikeArray, "Series3");
                        break;
                    }
                //Extract trend
                case 21:
                    {
                        int size = 1000;
                        int border = 2;
                        var trendAndRandomArray = new double[size];
                        var afterExtractTrendArray = new double[size];
                        var randomArray = new double[size];
                        randomArray = RandomWithBorder(border, size);
                        for (int i = 0; i < size; i++)
                        {
                            if (i == 250) _trendArray[i] = _trendArray[i - 1];
                            trendAndRandomArray[i] = randomArray[i] + _trendArray[i];
                        }

                        _trendAndRandomArray = trendAndRandomArray;
                        DrawAnything(trendAndRandomArray, "Series2");
                        if (_checkDoubleClick % 2 == 0)
                        {
                            afterExtractTrendArray = ExtractTrend(trendAndRandomArray, 25, size);
                            _extractedTrend = afterExtractTrendArray;
                            DrawAnything(afterExtractTrendArray, "Series3");
                        }

                        break;
                    }
                //Eliminate trend
                case 22:
                    {
                        int length = _trendAndRandomArray.Length;
                        var trendAndRandomArray = new double[length];
                        var afterEliminateTrend = new double[length];
                        for (int i = 0; i < length; i++)
                        {
                            afterEliminateTrend[i] = _trendAndRandomArray[i] - _extractedTrend[i];
                        }
                        DrawAnything(afterEliminateTrend, "Series3");
                        break;
                    }
                //Standard deviation
                case 23:
                    {
                        chart1.Series["Series2"].ChartType = SeriesChartType.Line;
                        chart1.Series["Series2"].BorderWidth = 1;
                        int size = 1000;
                        int[] mArray = { 1, 2, 3, 10, 50, 100 };
                        List<double[]> listГм = new List<double[]>();
                        foreach (var m in mArray)
                        {
                            var listRandomArrays = RandomWithBorderToList(10, size, m, _checkForHarmony, 5);
                            var гмElement = new double[size];
                            for (int i = 0; i < size; i++)
                            {
                                гмElement[i] = (ArrayHelper.ColumnSum(listRandomArrays, i)) / m;
                            }
                            listГм.Add(гмElement);
                        }
                        var listStandardDeviation = new List<double>();
                        foreach (var rmItem in listГм)
                        {
                            var standardDeviation = StandardDeviation(rmItem.ToArray(), size);
                            listStandardDeviation.Add(Math.Sqrt(standardDeviation));
                        }
                        if (_checkDoubleClickForStandardDeviation % 2 == 0)
                            for (int i = 0; i < mArray.Length; i++)
                            {
                                DrawAnything(listГм.ToArray()[i], "Series" + i + "");
                            }
                        else
                        {
                            DrawAnything(listStandardDeviation.ToArray(), "Series2");
                        }
                        break;
                    }
                case 24:
                    {
                        int n = 1000;
                        double[] y0 = new double[n];
                        y0[199] = 130;
                        y0[399] = 110;
                        y0[599] = 120;
                        y0[799] = 140;
                        DrawAnything(y0, "Series2");
                        _pitchLine = y0;
                        break;
                    }
                case 25:
                    {
                        chart1.Series["Series2"].BorderWidth = 1;
                        int size = 1000;
                        int M = 200;
                        int Hz = 7;
                        int A = 1;
                        int alpha = 25;
                        double dt = 0.005;
                        var finalSineAndNegativeEpsilon = new double[size];
                        for (int i = 0; i < M; i++)
                        {
                            finalSineAndNegativeEpsilon[i] =
                                Math.Sin(2 * i * Hz * dt * Math.PI) * Math.Exp(-alpha * i * dt);
                        }

                        _sineAndNegativeEpsilon = finalSineAndNegativeEpsilon;
                        DrawAnything(finalSineAndNegativeEpsilon, "Series2");
                        break;
                    }
                case 26:
                    {
                        int M = 200;
                        int N = 1000;
                        var wave = new double[M + N];
                        for (int k = 0; k < N; k++)
                        {
                            for (int m = 0; m < M; m++)
                            {
                                if (k - m >= 0)
                                    wave[k] += (_pitchLine[k - m] * _sineAndNegativeEpsilon[m]);
                            }
                        }

                        //wave = StatHelper.Convolve(_pitchLine,_sineAndNegativeEpsilon,false);
                        DrawAnything(wave, "Series0");
                        break;
                    }
                case 27:
                    {
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
                        chartFourier.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
                        chart1.Series["Series6"].ChartType = SeriesChartType.Spline;
                        int m = 128;
                        var originalFilter = new double[m + 1];
                        var fullLowpassArrayFilter = LowPassFilter(10, m, 0.002, out originalFilter);
                        //var fullLowpassArrayFilter = LowPassFilter(0.3, m, 1, out originalFilter);
                        var lengthDft = fullLowpassArrayFilter.Length;
                        samples = new Complex[lengthDft];
                        for (int i = 0; i < lengthDft; i++)
                        {
                            fullLowpassArrayFilter[i] *= 2 * (m + 1);
                        }
                        //var csn = new double[1000];
                        //var xn = new double[1000];
                        //var dftArray = SlowFourierTransform(lengthDft, fullLowpassArrayFilter, out csn, out xn);
                        DrawAnything(fullLowpassArrayFilter, "Series6");
                        chartFourier.Series[0].ChartType = SeriesChartType.Spline;
                        PlotFFt(fullLowpassArrayFilter.Length, fullLowpassArrayFilter.Length, fullLowpassArrayFilter);
                        DrawFilteredSignal(fullLowpassArrayFilter);
                        break;
                    }
                case 28:
                    {
                        WAV_file wav = new WAV_file();
                        break;
                    }
                case 29:
                    {
                        //High pass filter
                        int m = 128;
                        int N = m + 1;
                        var highfilter = HighPassFilter(90, m, 0.002);
                        var lengthDft = highfilter.Length;
                        samples = new Complex[lengthDft];
                        for (int i = 0; i < lengthDft; i++)
                        {
                            highfilter[i] *= 2 * (N);
                        }

                        DrawAnything(highfilter, "Series5");
                        chartFourier.Series[0].ChartType = SeriesChartType.Spline;
                        PlotFFt(highfilter.Length, highfilter.Length, highfilter);

                        DrawFilteredSignal(highfilter);
                        break;
                    }
                case 30:
                    {
                        int m = 16;
                        int N = m + 1;
                        var bandPassFilter = BandPassFilter(20, 100, m, 0.002);
                        //var bandPassFilter = BandPassFilter(0.3, 0.4, m, 1);
                        var lengthDft = bandPassFilter.Length;
                        samples = new Complex[lengthDft];
                        for (int i = 0; i < lengthDft; i++)
                        {
                            bandPassFilter[i] *= 2 * (N);
                        }

                        DrawAnything(bandPassFilter, "Series5");
                        chartFourier.Series[0].ChartType = SeriesChartType.Spline;
                        PlotFFt(bandPassFilter.Length, bandPassFilter.Length, bandPassFilter);

                        DrawFilteredSignal(bandPassFilter);
                        break;
                    }
                case 31:
                    {
                        int m = 128;
                        int N = m + 1;
                        var bandStopFilter = BandStopFilter(20, 100, m, 0.002);
                        var lengthDft = bandStopFilter.Length;
                        samples = new Complex[lengthDft];

                        for (int i = 0; i < lengthDft; i++)
                        {
                            bandStopFilter[i] *= 2 * (N);
                        }
                        var csn = new double[1000];
                        var xn = new double[1000];
                        var dftArray = SlowFourierTransform(lengthDft, bandStopFilter, out csn, out xn);
                        DrawAnything(dftArray, "Series5");
                        chartFourier.Series[0].ChartType = SeriesChartType.Spline;
                        PlotFFt(bandStopFilter.Length, bandStopFilter.Length, bandStopFilter);
                        DrawFilteredSignal(bandStopFilter);
                        break;
                    }
                case 32:
                    {
                        //////////////////////////////////////////////////////
                        //int sampleRate = 1;
                        //var signal = FileHelper.PrepareWavForFft("C:\\Sound\\record.wav", out sampleRate);
                        //var len = signal.Length;
                        //var sineWave = Generate.Sinusoidal(len, sampleRate, 440, 0.25 * short.MaxValue);
                        //for (int n = 0; n < len; n++)
                        //{
                        //    signal[n] += sineWave[n];
                        //}
                        //var signalConverted = FileHelper.GetBytes(signal);
                        ////var wrote = WriteToWavFile(signalConverted);
                        //samples = new Complex[len];
                        //spectrum = new Complex[len];
                        //chartFourier.Series[0].ChartType = SeriesChartType.Column;
                        ////var reader = new WaveFileReader("C:\\Sound\\record.wav");
                        ////using (var writer = new WaveFileWriter("C:\\Sound\\hope.wav", reader.WaveFormat))
                        ////{
                        ////    writer.Write(signalConverted, 0, signalConverted.Length);
                        ////}
                        //DrawAnything(signal, "Series5");
                        //PlotFFt(len, sampleRate, signal);
                        //var csn = new double[len];
                        //var xn = new double[len];
                        //var dftArray = SlowFourierTransform(signal.Length, signal, out csn, out xn);
                        //DrawAnythingOnFourier(dftArray.Length, sampleRate, dftArray);
                        /////////////////////////////////////////////
                        chartFourier.Series[0].ChartType = SeriesChartType.Column;
                        //

                        //
                        WavHelper wav = new WavHelper();
                        wav.ReadWav("C:\\Sound\\record.wav");
                        double[] Left;
                        double[] Right;
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
                        //FileHelper.OpenWav("C:\\Sound\\record.wav", out Left, out Right);
                        FileHelper.ReadWav("C:\\Sound\\record.wav", out Left, out Right);
                        //var originalArray = wav._aSamples.Take(wav._nOriginalLen).ToArray();
                        //Left = Left.Select(c => 5 * c).ToArray();

                        //var csn = new double[originalArray.Length];
                        //var xn = new double[originalArray.Length];
                        //var dftArray = SlowFourierTransform(originalArray.Length, originalArray, out csn, out xn);
                        //DrawAnythingOnFourier(dftArray.Length, 1, dftArray);


                        chartFourier.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
                        var reader = new WaveFileReader("C:\\Sound\\record.wav");
                        var time = reader.TotalTime.TotalSeconds;
                        var sine = new double[reader.Length];
                        double dt = (double)1 / reader.WaveFormat.SampleRate;
                        double totalTime = 0;
                        int i = 0;
                        //while (totalTime < time - dt)
                        //{
                        //    sine[i++] = Math.Sin(2 * Math.PI * 440 * totalTime);
                        //    totalTime += dt;
                        //}

                        //for (int k = 0; k < Left.Length; k++)
                        //{
                        //    Left[k] += sine[k];
                        //}
                        samples = new Complex[Left.Length];
                        spectrum = new Complex[Left.Length];
                        //sample rate = 22050
                        PlotFFt(Left.Length / 2, reader.WaveFormat.SampleRate / 2, Left.Select(c => c * 1000).ToArray());
                        DrawAnything(Left.Select(c => c * 1000).ToArray(), "Series5");
                        //var csn = new double[Left.Length];
                        //var xn = new double[Left.Length];
                        //var voice = Left.Skip(20000).Take(10000).ToArray();
                        //var dftArray = SlowFourierTransform(voice.Length, voice, out csn, out xn);
                        //DrawAnythingOnFourier(voice.Length, 1, dftArray);

                        //var buffer = new byte[reader.Length];
                        //int bytesRead;
                        //int total = 0;
                        //do
                        //{
                        //    bytesRead = reader.Read(buffer, 0, buffer.Length);
                        //    total += bytesRead;
                        //} while (bytesRead > 0);

                        //var doubleSignal = FileHelper.GetDoubles(buffer);

                        //DrawAnything(doubleSignal,"Series5");
                        //DrawAnythingForbyte(buffer, "Series5");

                        //using (var writer = new WaveFileWriter("C:\\Sound\\hope.wav", reader.WaveFormat))
                        //{
                        //    writer.WriteSamples(Left.Select(c => (float)c).ToArray(), 0, Left.Length);
                        //}

                        //samples = new Complex[wav._nOriginalLen];
                        //spectrum = new Complex[wav._nOriginalLen];
                        ////PlotFFt(processedSignal.Length, reader.WaveFormat.SampleRate, processedSignal);
                        reader.Close();
                        //writer.Close();


                        break;
                    }
                case 33:
                    {
                        var doubleExamArray = ReadDatFile().ToArray();
                        var extractedTrend = ExtractTrend(doubleExamArray, 30, doubleExamArray.Length);
                        DrawAnything(extractedTrend, "Series5");
                        int size = extractedTrend.Length;
                        DrawHistogram(extractedTrend, size);
                        DrawAutoCorrelation(extractedTrend, size);
                        MyDrawAutoCorrelation(extractedTrend, size);
                        PlotFFt(size, size, extractedTrend);
                        break;
                    }
            }
        }
        public bool WriteToWavFile(byte[] bytes, string path, int sampleRate, int channel, int bits)
        {
            try
            {
                using (var writer = new WaveFileWriter(path, new WaveFormat(sampleRate, bits, channel)))
                {
                    writer.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public void DrawFilteredSignal(double[] filterArray)
        {
            var harmonyArray = HarmonyProcess(0.002f, 300, 37, -1, 1000);
            var filteredSignal = StatHelper.Convolve(harmonyArray, filterArray, true);
            DrawAnythingOnCorrelationChart(filteredSignal);
            samples = new Complex[1000];
            PlotFFt(filteredSignal.Length, filteredSignal.Length, filteredSignal, 1);
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
        public void DrawAnythingOnHistogram(double[] arrayTodraw)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chartHis.Series[0].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingOnCorrelationChart(double[] arrayTodraw)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chartAutoCorr.Series[0].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingForbyte(byte[] arrayTodraw, string series)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chart1.Series[series].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnything(double[] arrayTodraw, string series)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chart1.Series[series].Points.AddXY(i, arrayTodraw[i]);
            }
        }
        public void DrawAnythingOnFourier(int numSamples, int sampleRate, double[] arrayTodraw)
        {
            double hzPersample = (double)sampleRate / numSamples;
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chartFourier.Series[0].Points.AddXY(hzPersample * i, (1.0f / sampleRate) * arrayTodraw[i]);
            }
        }
        public double[] AntiSpike(int size, double[] inputArray, int border)
        {
            for (int i = 0; i < size; i++)
            {
                if ((Math.Abs(inputArray[i]) > 1.2 * border) && (i != 0) && (i != (size - 1)))
                {
                    inputArray[i] = (inputArray[i - 1] + inputArray[i + 1]) / 2;
                }
            }
            var antiSpikeArray = inputArray;
            return antiSpikeArray;
        }
        public double[] AntiShift(int size, double[] inputArray)
        {
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 2;
            var antiShiftArray = new double[size];
            var mediumX = MeanX(inputArray, size);
            for (int i = 0; i < size; i++)
            {
                antiShiftArray[i] = inputArray[i] - mediumX;
            }

            return antiShiftArray;
        }
        //Moving average
        public double[] ExtractTrend(double[] inputSignal, int window, int n)
        {
            int i, j, z, k1, k2, hw;
            double tmp;
            double[] output = new double[n];
            if (window % 2 == 0) window++;
            hw = (window - 1) / 2;
            output[0] = inputSignal[0];

            for (i = 1; i < n; i++)
            {
                tmp = 0;
                if (i < hw)
                {
                    k1 = 0;
                    k2 = 2 * i;
                    z = k2 + 1;
                }
                else if ((i + hw) > (n - 1))
                {
                    k1 = i - n + i + 1;
                    k2 = n - 1;
                    z = k2 - k1 + 1;
                }
                else
                {
                    k1 = i - hw;
                    k2 = i + hw;
                    z = window;
                }

                for (j = k1; j <= k2; j++)
                {
                    tmp = tmp + inputSignal[j];
                }

                output[i] = tmp / z;
            }

            return output;
        }

        public double[,] RandomWithBorderToList(int border, int size, int totalRandomArray, bool isHarmony, int Hz)
        {
            var listRandomArrays = new List<double[]>();
            if (!isHarmony)
                for (int i = 0; i < totalRandomArray; i++)
                {
                    listRandomArrays.Add(RandomWithBorder(border, size));
                }
            else
            {
                for (int i = 0; i < totalRandomArray; i++)
                {
                    listRandomArrays.Add(RandomWithBorderAndHarmony(border, size, Hz));
                }
            }
            return ArrayHelper.CreateRectangularArray(listRandomArrays);
        }
        public double[] RandomWithBorderAndHarmony(int border, int size, int Hz)
        {
            var arrayAfter = new double[size];
            var finalArray = new double[size];
            var ranArray = BuildRandomNativeValues(size);
            var harmonyArray = Generate.Sinusoidal(size, size, Hz, 1);

            _xMax = (float)ranArray.Max();
            _xMin = (float)ranArray.Min();
            for (int i = 0; i < size; i++)
            {
                arrayAfter[i] = _functionForSpikeFormulaV1(ranArray[i], border);
                finalArray[i] = arrayAfter[i] + harmonyArray[i];
            }
            return finalArray;
        }
        public double[] RandomWithBorder(int border, int size)
        {
            var arrayAfter = new double[size];
            var ranArray = BuildRandomNativeValues(size);
            _xMax = (float)ranArray.Max();
            _xMin = (float)ranArray.Min();
            for (int i = 0; i < size; i++)
            {
                arrayAfter[i] = _functionForSpikeFormulaV1(ranArray[i], border);
            }
            return arrayAfter;
        }

        public double[] ShiftArray(double shiftValue, double[] inputArray)
        {
            var afterShiftArray = new double[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                afterShiftArray[i] = inputArray[i] + shiftValue;
            }
            return afterShiftArray;
        }
        public void DrawAutoCorrelation(double[] singal, int size)
        {
            var aut = Correlation.Auto(singal);
            for (int i = 0; i < size; i++)
            {
                chartAutoCorr.Series[0].Points.AddXY(i, aut[i]);
            }
        }
        public void MyDrawAutoCorrelation(double[] singal, int size)
        {
            //chartAutoCorr.Series[1].Points[0].Label = "My correlation";
            var aut = AutoCorrelationV2(singal, 0, size);
            for (int i = 0; i < size; i++)
            {
                chartAutoCorr.Series[1].Points.AddXY(i, aut[i] + 5);
            }
        }
        public void DrawHistogram(double[] signal, int size)
        {
            //Their algorithm
            var his = new Histogram(signal, size);
            for (int i = 0; i < size; i++)
            {
                chartHis.Series[0].Points.AddXY(i, his[i].Count);
            }
        }
        public List<double> ReadDatFile()
        {
            var signalArray = new List<double>();
            using (BinaryReader b = new BinaryReader(
                File.Open("G:\\Russiaa\\v1x5.dat", FileMode.Open)))
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



        //For Fourier transform
        public double[] RandomSineV2(int numSamples)
        {
            var fundalmental = Generate.Sinusoidal(numSamples, numSamples, 60, 10);
            return fundalmental;
        }
        //Fast fourier using library
        public void PlotFFt(int numSamples, int sampleRate, double[] signalarr, int his = 0)
        {
            //signalarr = ReadDatFile().ToArray();
            samples = new Complex[numSamples];
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
                    chartHis.Series[0].Points.AddXY(hzPersample * i, mag);
                }
            }
            else
            {
                for (int i = 0; i < numSamples; i++)
                {
                    mag = (1.0f / sampleRate) * Math.Abs(Math.Sqrt(Math.Pow(samples[i].Real, 2) + Math.Pow(samples[i].Imaginary, 2)));
                    spectrum[i] = new Complex(mag, 0);
                    chartFourier.Series[0].Points.AddXY(hzPersample * i, mag);
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

        public double[] HarmonyProcessV2(float deltaT, int size)
        {
            var x = new double[size];
            var y = new double[size];
            var y1 = new double[size];
            var y2 = new double[size];
            var y3 = new double[size];
            for (int i = 0; i < size; i++)
            {
                x[i] = i * Math.PI / 200;
                y1[i] = 2 * Math.Sin(3 * x[i]);
                y2[i] = 0.3 * Math.Sin(x[i] / 2 + Math.PI / 6);
                y3[i] = Math.Cos(x[i] + 5 * Math.PI / 6);
                y[i] = y1[i] + y2[i] + y3[i];
            }
            return y;
        }
        public double[] HarmonyProcess(float deltaT, float A0, float frequency, int k, int size)
        {
            double[] H = new double[size];
            double[] H1 = new double[size];
            double[] H2 = new double[size];
            double[] H3 = new double[size];
            double t0 = 0;
            int index = 0;
            while (t0 < 2)
            {
                H1[index] = 200 * Math.Sin(2 * Math.PI * 3 * t0);
                H[index] = A0 * Math.Sin(2 * Math.PI * frequency * t0);
                H2[index] = 100 * Math.Sin(2 * Math.PI * 137 * t0);
                H3[index] = H[index] + H1[index] + H2[index];
                t0 += deltaT;
                index++;
            }
            return H3;
        }
        public double[] HarmonyProcessForHistogram(float deltaT, float A0, float frequency, int k, int size)
        {
            int sizeofArray = (int)(size / deltaT);
            double[] H = new double[sizeofArray];
            double t0 = 0;
            int index = 0;
            while (t0 < size - deltaT)
            {
                H[index] = A0 * Math.Sin(2 * Math.PI * frequency * t0);
                t0 += deltaT;
                index++;
            }
            return H;
        }
        public double[] RandomSine(int N)
        {
            double fs = 500;
            double T = 1 / fs;
            double[] t = new double[N];
            double[] x = new double[N];
            for (int i = 0; i < N; i++)
            {
                t[i] = i * T;
            }

            var f1 = 8;
            var f2 = f1 * 2;
            for (int j = 0; j < N; j++)
            {
                x[j] = Math.Sin(2 * Math.PI * f1 * a.NextDouble() * t[j] - Math.PI / 2);
            }
            return x;
        }
        public double[] AutoCorrelation(double[] x)
        {
            var xLen = x.Length;
            var xMean = MeanX(x, xLen);
            var autoCorrelationArray = new double[xLen / 2];
            for (int t = 0; t < autoCorrelationArray.Length; t++)
            {
                double n = 0;
                double d = 0;
                for (int i = 0; i < xLen; i++)
                {
                    var xim = x[i] - xMean;
                    n += xim * (x[(i + t) % xLen] - xMean);
                    d += xim * xim;
                }

                autoCorrelationArray[t] = n / d;
            }
            return autoCorrelationArray;
        }

        public double[] AutoCorrelationV2(double[] x, int kMin, int kMax)
        {
            var xLen = x.Length;
            var xMean = MeanX(x, x.Length);
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
        private double[] Spikes(double p1, int p2, double p3, double p4, double[] yArray)
        {
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

        private bool Station(double[] arr, int intervalNum)
        {
            int arrayLength = arr.Length;
            var intervalLength = arrayLength / intervalNum;
            double[] meanAverageSegmentArray = new double[intervalNum];
            double[] varAverageSegmentArray = new double[intervalNum];
            for (int i = 0; i < intervalNum; i++)
            {
                for (int k = i * intervalLength; k < intervalLength * (i + 1); k++)
                {
                    meanAverageSegmentArray[i] += arr[k] / intervalLength;
                }

                double XX = 0f;
                for (int f = i * intervalLength; f < intervalLength * (i + 1); f++)
                {
                    XX += Math.Pow((arr[f] - meanAverageSegmentArray[i]), 2);
                }

                varAverageSegmentArray[i] = XX / intervalLength;
            }

            var meanX = MeanX(arr, arrayLength);
            var varX = VarXX(arr, arrayLength);

            for (int ii = 0; ii < intervalNum; ii++)
            {
                for (int jj = 0; jj < intervalNum; jj++)
                {
                    if (Math.Abs(meanAverageSegmentArray[ii] - meanAverageSegmentArray[jj]) > Math.Abs(0.03 * meanX))
                    {
                        return false;
                    }

                    if (Math.Abs(varAverageSegmentArray[ii] - varAverageSegmentArray[jj]) > Math.Abs(0.03 * varX))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        double[] BuildRandomNativeValues(int size)
        {
            var randomArray = new double[size];
            for (int i = 0; i < size; i++)
            {
                randomArray[i] = (float)a.NextDouble();
            }

            return randomArray;
        }
        double[] NotNativeRandom(int size)
        {
            double[] notNative = new double[size];
            notNative[0] = (float)Math.Cos(DateTime.Now.Millisecond);
            for (int i = 1; i < size; i++)
            {
                notNative[i] = (float)Math.Cos(notNative[i - 1] * size);
            }
            return notNative;
        }
        private double _functionForSpikeFormula(float i)
        {
            double y = 2 * ((i - _xMin) / (_xMax - _xMin) - 0.5) * 10;
            return Math.Abs(y);
        }
        private double _functionForSpikeFormulaV1(double i, int S)
        {
            double y = 2 * ((i - _xMin) / (_xMax - _xMin) - 0.5) * S;
            return (y);
        }
        private double _functionForSpikeFormulaV2(float i)
        {
            double y = 2 * ((i - _xMin) / (_xMax - _xMin) - 0.5) * 1;
            return y;
        }
        private double _functionForRandomPositiveEpsilon(int i)
        {
            return (double)Math.Pow(Math.E, (0.005 * i));
        }

        private double _functionForRandomNegativeEpsilon(int i)
        {
            return (double)Math.Pow(Math.E, (-0.005 * i));
        }

        private double _functionForRandomNegativeLine(int i)
        {
            return (-3 * i / 100 + _c);
        }

        private double _functionForRandomPositiveLine(int i)
        {
            return (3 * i / 100);
        }

        private int NewNumber(int min, int max)
        {
            _myNumber = a.Next(min, max);
            if (!UsedList.Contains(_myNumber))
                UsedList.Add(_myNumber);
            return _myNumber;
        }
        static float NextFloat(Random random)
        {
            // Not a uniform distribution w.r.t. the binary floating-point number line
            // which makes sense given that NextDouble is uniform from 0.0 to 1.0.
            // Uniform w.r.t. a continuous number line.
            //
            // The range produced by this method is 6.8e38.
            //
            // Therefore if NextDouble produces values in the range of 0.0 to 0.1
            // 10% of the time, we will only produce numbers less than 1e38 about
            // 10% of the time, which does not make sense.
            var result = (random.NextDouble()
                          * (Single.MaxValue - (double)Single.MinValue))
                         + Single.MinValue;
            return (float)result;
        }
        static float NextFloatV2(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }
        public void IntervalDetection()
        {
            //y=e^(-0.005x+8)+500
            _k = NewNumber(-12, -2);
            _c = NewNumber(10, 20);
            alpha = -0.007f;
            _k = _k / 750;
            //_k = ((2 * (double)_c) - 1) / 1000;
            beta = Math.Log(_k * 750 + _c) / 750;

            //_kFirstLine = Math.Pow(Math.E, -alpha * 250) / 250;
            _epsilon = Math.Abs(_functionForNegativeEpsilon(500) - _functionForNegativeLine(500));
            _epsilonForFirstLine = _functionForNegativeEpsilon(250) + _epsilon;
            _kFirstLine = _epsilonForFirstLine / 250;
            //var test1 = _functionForPositiveLine(250) - _epsilonForFirstLine;

        }
        private double _functionForPositiveEpsilon(int i)
        {
            double y = (Math.Pow(Math.E, beta * i));
            return y;
        }

        private double _functionForNegativeEpsilon(int i)
        {
            double y = (Math.Pow(Math.E, (alpha * i + 5)));
            return y;
        }
        private double _functionForNegativeLine(int i)
        {
            double y = (_k * i + _c);
            return y;
        }

        private void ClearGraph(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartHis.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartAutoCorr.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartFourier.Series)
            {
                series.Points.Clear();
            }
            Form1_Load(sender, e);
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            _choice = 1;
            ClearGraph(sender, e);
        }

        private void btnNegativeLine_Click(object sender, EventArgs e)
        {
            _choice = 2;
            ClearGraph(sender, e);
        }

        private void btnEps_Click(object sender, EventArgs e)
        {
            _choice = 3;
            ClearGraph(sender, e);
        }

        private void negaEps_Click(object sender, EventArgs e)
        {
            _choice = 4;
            ClearGraph(sender, e);
        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            _choice = 5;
            ClearGraph(sender, e);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            _choice = 6;
            ClearGraph(sender, e);
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            _choice = 7;
            ClearGraph(sender, e);
        }

        private void btnSpike_Click(object sender, EventArgs e)
        {
            _choice = 8;
            ClearGraph(sender, e);
        }

        private void btnStation_Click(object sender, EventArgs e)
        {
            _choice = 9;
            ClearGraph(sender, e);
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            _choice = 10;
            ClearGraph(sender, e);
        }
        private void btnAutoCorre_Click(object sender, EventArgs e)
        {
            _choice = 11;
            ClearGraph(sender, e);
        }
        private void btnCrossCorre_Click(object sender, EventArgs e)
        {
            _choice = 12;
            ClearGraph(sender, e);
        }

        private void btnFastFourier_Click(object sender, EventArgs e)
        {
            _choice = 13;
            ClearGraph(sender, e);
        }
        private void btnSignal_Click(object sender, EventArgs e)
        {
            _choice = 14;
            ClearGraph(sender, e);
        }

        private void btnRebuilt_Click(object sender, EventArgs e)
        {
            _choice = 15;
            ClearGraph(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _choice = 16;
            ClearGraph(sender, e);
        }
        private void btnTestHistogram_Click(object sender, EventArgs e)
        {
            _choice = 17;
            ClearGraph(sender, e);
        }
        private void btnFluctuation_Click(object sender, EventArgs e)
        {
            _choice = 18;
            ClearGraph(sender, e);
        }
        private void btnAntiShift_Click(object sender, EventArgs e)
        {
            _choice = 19;
            ClearGraph(sender, e);
        }

        private void btnAntiSpike_Click(object sender, EventArgs e)
        {
            _choice = 20;
            ClearGraph(sender, e);
        }
        private void btnExtractTrend_Click(object sender, EventArgs e)
        {
            _choice = 21;
            _checkDoubleClick += 1;
            ClearGraph(sender, e);
        }
        private void btnEliminate_Click(object sender, EventArgs e)
        {
            _choice = 22;
            ClearGraph(sender, e);
        }
        private void btnStandard_Click(object sender, EventArgs e)
        {
            _choice = 23;
            _checkDoubleClickForStandardDeviation += 1;
            _checkForHarmony = false;
            ClearGraph(sender, e);
        }
        private void btnStandardDeviationHarmony_Click(object sender, EventArgs e)
        {
            _choice = 23;
            _checkForHarmony = true;
            ClearGraph(sender, e);
        }
        private void btnLinePitch_Click(object sender, EventArgs e)
        {
            _choice = 24;
            ClearGraph(sender, e);
        }
        private void btnSineAndEp_Click(object sender, EventArgs e)
        {
            _choice = 25;
            ClearGraph(sender, e);
        }

        private void btnWave_Click(object sender, EventArgs e)
        {
            _choice = 26;
            ClearGraph(sender, e);
        }
        private void btnLowPassFilter_Click(object sender, EventArgs e)
        {
            _choice = 27;
            ClearGraph(sender, e);
        }
        private void btnReadWav_Click(object sender, EventArgs e)
        {
            // record from microphone
            //mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            //mciSendString("record recsound", "", 0, 0);
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(22050, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(@"C:\Sound\record.wav", waveSource.WaveFormat);

            waveSource.StartRecording();
            ClearGraph(sender, e);
        }
        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            // stop and save 
            //mciSendString("save recsound c:\\Sound\\record.wav", "", 0, 0);
            //mciSendString("close recsound", "", 0, 0);
            waveSource.StopRecording();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        private void btnHighPassFilter_Click(object sender, EventArgs e)
        {
            _choice = 29;
            ClearGraph(sender, e);
        }

        private void btnBandPassFilter_Click(object sender, EventArgs e)
        {
            _choice = 30;
            ClearGraph(sender, e);
        }

        private void btnBandStopFilter_Click(object sender, EventArgs e)
        {
            _choice = 31;
            ClearGraph(sender, e);
        }
        private void btnWavFFt_Click(object sender, EventArgs e)
        {
            _choice = 32;
            ClearGraph(sender, e);
        }
        private void btnExam_Click(object sender, EventArgs e)
        {
            _choice = 33;
            ClearGraph(sender, e);
        }
        private void btnSeismic_Click(object sender, EventArgs e)
        {
            seismicForm = new SeismicForm();
            seismicForm.Show();
            Close();
        }
        public double _functionForPositiveLine(int i)
        {
            double y = (_kFirstLine * i);
            return y;
        }
        private double StandardDeviation(double[] array, int size)
        {
            double sum = 0;
            var meanX = MeanX(array, size);
            for (int i = 0; i < size; i++)
            {
                sum += Math.Pow((array[i] - meanX), 2);
            }
            return sum / size;
        }
        public double MeanX(double[] arr, int N)
        {
            double x = 0;
            for (int i = 0; i < N; i++)
            {
                x += arr[i];
            }
            _xMid = x / N;
            return _xMid;
        }
        public double VarXX(double[] arr, int N)
        {
            double X = 0;
            double xm = MeanX(arr, N);
            for (int i = 0; i < N; i++)
            {
                X += Math.Pow(arr[i] - _xMid, 2);
            }
            _disp = X / N;
            return _disp;
        }
        public double StandardX(double[] arr, int N)
        {
            if (_disp >= 0)
                return Math.Sqrt(_disp);
            else return 0;
        }
        public double CK(double[] arr, int N)
        {
            double x = 0;
            for (int i = 0; i < N; i++)
            {
                x += Math.Pow(arr[i], 2);
            }

            _threeNail = x / N;
            return _threeNail;
        }
        public double CKO(double[] arr, int N)
        {
            return Math.Sqrt(_threeNail);
        }
        public double M3(double[] arr, int N)
        {
            double X = 0;
            for (int i = 0; i < N; i++)
            {
                X += Math.Pow(arr[i] - _xMid, 3);
            }
            _muy3 = X / N;
            return _muy3;
        }
        public double M4(double[] arr, int N)
        {
            double X = 0;
            for (int i = 0; i < N; i++)
            {
                X += Math.Pow(arr[i] - _xMid, 4);
            }
            _muy4 = X / N;
            return _muy4;
        }
        public double V1(double[] arr, int N)
        {
            double X = 0;
            X = _muy3 / Math.Pow(StandardX(arr, N), 3);
            return X;
        }



        public double V2(double[] arr, int N)
        {
            var X = _muy4 / Math.Pow(StandardX(arr, N), 4);
            return X - 3;
        }
        public double[] SysRandomArr(int N, int a, int b)
        {
            Random rnd = new Random();
            double[] arr = new double[N];
            for (int i = 0; i < N; i++)
            {
                arr[i] = (int)(a + (b - a) * rnd.Next(a, b));
            }
            return arr;
        }
    }
}
