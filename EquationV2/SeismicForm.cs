using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EquationV2
{
    public partial class SeismicForm : Form
    {
        public SeismicForm()
        {
            InitializeComponent();
            chartSeismic1.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chartSeismic1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 600);
            chartSeismic1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSeismic1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSeismic1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSeismic1.Series[0].BorderWidth = 1;
            chartSeismic1.Series[0].ChartType = SeriesChartType.Column;

            chartSeismic2.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chartSeismic2.ChartAreas[0].AxisX.ScaleView.Zoom(0, 600);
            chartSeismic2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSeismic2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSeismic2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSeismic2.Series[0].BorderWidth = 1;
            chartSeismic2.Series[0].ChartType = SeriesChartType.Column;

            chartSeismic3.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chartSeismic3.ChartAreas[0].AxisX.ScaleView.Zoom(0, 600);
            chartSeismic3.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSeismic3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSeismic3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSeismic3.Series[0].BorderWidth = 1;
            chartSeismic3.Series[0].ChartType = SeriesChartType.Column;

            chartSeismic4.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chartSeismic4.ChartAreas[0].AxisX.ScaleView.Zoom(0, 600);
            chartSeismic4.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSeismic4.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSeismic4.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSeismic4.Series[0].BorderWidth = 1;
            chartSeismic4.Series[0].ChartType = SeriesChartType.Column;

            chartSeismic5.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chartSeismic5.ChartAreas[0].AxisX.ScaleView.Zoom(0, 600);
            chartSeismic5.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSeismic5.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSeismic5.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSeismic5.Series[0].BorderWidth = 1;
            chartSeismic5.Series[0].ChartType = SeriesChartType.Column;
        }

        private int _layer = 8;//for T array
        private int _totalChannel = 5;
        private int distanceFromGunToFirstSensors = 200;
        private int distanceBetweenSensors = 50;
        private void SeismicForm_Load(object sender, EventArgs e)
        {
            var timeArray = new double[_layer];
            var reflectionCo = new double[_layer];
            var listTimeAndReflectionCoe = new List<TimeAndReflection>();
            var listLayer = new List<Layer>();
            double[] distanceToSensors = { 200, 250, 300, 350, 400 };


            var seaWater = new Layer(1.06, 1500, 150);//Density, velocity, depth 
            var sand = new Layer(2.65, 1700, 150);
            var dolomite = new Layer(2.87, 6400, 290);
            //var shale     = new Layer(2.4, 2000, 280);
            var sandStone = new Layer(2.65, 5800, 370);
            var limeStone = new Layer(2.71, 6100, 478);
            var oil = new Layer(0.7, 1474, 563);
            var gas = new Layer(0.15, 340, 500);
            var Water = new Layer(1.00, 1500, 600);


            listLayer.Add(seaWater);
            listLayer.Add(sand);
            listLayer.Add(dolomite);
            listLayer.Add(gas);
            listLayer.Add(oil);
            listLayer.Add(Water);
            listLayer.Add(sandStone);
            listLayer.Add(limeStone);


            for (int i = 0; i < distanceToSensors.Length; i++)
            {
                timeArray[0] = TwoWayTravelTime(listLayer[0].depth, distanceToSensors[i], listLayer[0].velocity);
                var oldTime = timeArray[0];
                for (int j = 1; j < listLayer.Count; j++)
                {
                    timeArray[j] = TwoWayTravelTime(listLayer[j].depth, distanceToSensors[i], listLayer[j].velocity) + oldTime;
                    oldTime = timeArray[j];

                    reflectionCo[j - 1] = ReflectionCoefficient(listLayer[j], listLayer[j - 1]);
                }
                listTimeAndReflectionCoe.Add(new TimeAndReflection(timeArray.Select(c => c * 100).ToList(), reflectionCo.Select(c => c * 100).ToList(), i));
            }

            for (var i = 0; i < listTimeAndReflectionCoe.Count;)
            {
                //DrawAnything(listTimeAndReflectionCoe[i].timeWithReflect, Controls.OfType<Chart>().ToList()[i]);
                DrawAnything(listTimeAndReflectionCoe[i].GetFinalSignal(), Controls.OfType<Chart>().ToList()[i]);
                i++;
            }
        }
        //Generate the pulse of the gun, Sin(2*pi*t*Hz)*e^(-alpha*dt)
        public double[] GunPulse()
        {
            int size = 600;
            int M = 200;
            int Hz = 7;
            int A = 1;
            int alpha = 25;
            double dt = 0.005;
            var gunPulse = new double[size];
            for (int i = 0; i < M; i++)
            {
                gunPulse[i] =
                    Math.Sin(2 * i * Hz * dt * Math.PI) * Math.Exp(-alpha * i * dt);
            }
            return gunPulse;
        }
        //Caculate reflection coefficient between two geolotical layer
        double ReflectionCoefficient(Layer layer2, Layer layer1)
        {
            return (layer2.density * layer2.velocity - layer1.density * layer1.velocity) / (layer2.density * layer2.velocity + layer1.density * layer1.velocity);
        }
        //Caculate two way travel time, depth: the depth of layer, distance:  distance between the sensors and the gun
        //velocity: velocity in each geolotical layer
        double TwoWayTravelTime(double depth, double distance, double velocity)
        {
            return 2 * Math.Sqrt(Math.Pow(depth, 2) + Math.Pow(distance / 2, 2)) / velocity;
        }
        public void DrawAnything(double[] arrayTodraw, Chart chart)
        {
            var length = arrayTodraw.Length;
            for (int i = 0; i < length; i++)
            {
                chart.Series[0].Points.AddXY(i, arrayTodraw[i]);
            }
        }
    }

    public class TimeAndReflection
    {
        public int channel { get; set; }
        public List<double> time { get; set; }
        public List<double> reflectionCoefficient { get; set; }

        public double[] timeWithReflect = new Double[600];

        public TimeAndReflection(List<double> t, List<double> re, int chan)
        {
            this.time = t;
            reflectionCoefficient = re;
            channel = chan;
            for (int i = 0; i < time.Count - 1; i++)
            {
                timeWithReflect[(int)time[i]] = reflectionCoefficient[i];
            }
            Random rnd = new Random();
            for (int j = 0; j < timeWithReflect.Length; j++)
            {
                if (time.Select(c => (int)c).ToArray().Contains(j)) continue;
                timeWithReflect[j] += rnd.Next(-3, 3);
            }
        }

        public double[] GetFinalSignal()
        {
            var gunPulse = GunPulse();
            return StatHelper.Convolve(timeWithReflect, gunPulse, false);
        }
        public double[] GunPulse()
        {
            int size = 600;
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

            return finalSineAndNegativeEpsilon;
        }
    }

    public class Layer
    {
        public double velocity { get; }
        public double density { get; }
        public double depth { get; }

        public Layer(double den, double velo, double dep)
        {
            this.density = den;
            this.velocity = velo;
            depth = dep;
        }
    }
}
