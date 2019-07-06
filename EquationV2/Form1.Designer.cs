namespace EquationV2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnNegativeLine = new System.Windows.Forms.Button();
            this.btnEps = new System.Windows.Forms.Button();
            this.negaEps = new System.Windows.Forms.Button();
            this.btnFinal = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnShift = new System.Windows.Forms.Button();
            this.btnSpike = new System.Windows.Forms.Button();
            this.btnStation = new System.Windows.Forms.Button();
            this.btnStatistic = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAutoCorre = new System.Windows.Forms.Button();
            this.btnCrossCorre = new System.Windows.Forms.Button();
            this.btnFastFourier = new System.Windows.Forms.Button();
            this.btnSignal = new System.Windows.Forms.Button();
            this.btnRebuilt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTestHistogram = new System.Windows.Forms.Button();
            this.chartHis = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAutoCorr = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartFourier = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnFluctuation = new System.Windows.Forms.Button();
            this.btnAntiSpike = new System.Windows.Forms.Button();
            this.btnAntiShift = new System.Windows.Forms.Button();
            this.btnExtractTrend = new System.Windows.Forms.Button();
            this.btnEliminate = new System.Windows.Forms.Button();
            this.btnStandard = new System.Windows.Forms.Button();
            this.btnStandardDeviationHarmony = new System.Windows.Forms.Button();
            this.btnLinePitch = new System.Windows.Forms.Button();
            this.btnSineAndEp = new System.Windows.Forms.Button();
            this.btnWave = new System.Windows.Forms.Button();
            this.btnLowPassFilter = new System.Windows.Forms.Button();
            this.btnReadWav = new System.Windows.Forms.Button();
            this.btnHighPassFilter = new System.Windows.Forms.Button();
            this.btnBandPassFilter = new System.Windows.Forms.Button();
            this.btnBandStopFilter = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnWavFFt = new System.Windows.Forms.Button();
            this.btnSeismic = new System.Windows.Forms.Button();
            this.btnExam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAutoCorr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFourier)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series0";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series5";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series6";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series4";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(911, 524);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(13, 634);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(70, 50);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnNegativeLine
            // 
            this.btnNegativeLine.Location = new System.Drawing.Point(89, 634);
            this.btnNegativeLine.Name = "btnNegativeLine";
            this.btnNegativeLine.Size = new System.Drawing.Size(70, 50);
            this.btnNegativeLine.TabIndex = 2;
            this.btnNegativeLine.Text = "Negative Line";
            this.btnNegativeLine.UseVisualStyleBackColor = true;
            this.btnNegativeLine.Click += new System.EventHandler(this.btnNegativeLine_Click);
            // 
            // btnEps
            // 
            this.btnEps.Location = new System.Drawing.Point(165, 634);
            this.btnEps.Name = "btnEps";
            this.btnEps.Size = new System.Drawing.Size(70, 50);
            this.btnEps.TabIndex = 3;
            this.btnEps.Text = "Epsilon";
            this.btnEps.UseVisualStyleBackColor = true;
            this.btnEps.Click += new System.EventHandler(this.btnEps_Click);
            // 
            // negaEps
            // 
            this.negaEps.Location = new System.Drawing.Point(241, 634);
            this.negaEps.Name = "negaEps";
            this.negaEps.Size = new System.Drawing.Size(70, 50);
            this.negaEps.TabIndex = 4;
            this.negaEps.Text = "Negative Eps";
            this.negaEps.UseVisualStyleBackColor = true;
            this.negaEps.Click += new System.EventHandler(this.negaEps_Click);
            // 
            // btnFinal
            // 
            this.btnFinal.Location = new System.Drawing.Point(317, 634);
            this.btnFinal.Name = "btnFinal";
            this.btnFinal.Size = new System.Drawing.Size(70, 50);
            this.btnFinal.TabIndex = 5;
            this.btnFinal.Text = "Final Graph";
            this.btnFinal.UseVisualStyleBackColor = true;
            this.btnFinal.Click += new System.EventHandler(this.btnFinal_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(393, 634);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(70, 50);
            this.btnRandom.TabIndex = 6;
            this.btnRandom.Text = "Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // btnShift
            // 
            this.btnShift.Location = new System.Drawing.Point(469, 634);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(70, 50);
            this.btnShift.TabIndex = 7;
            this.btnShift.Text = "Shift";
            this.btnShift.UseVisualStyleBackColor = true;
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // btnSpike
            // 
            this.btnSpike.Location = new System.Drawing.Point(13, 690);
            this.btnSpike.Name = "btnSpike";
            this.btnSpike.Size = new System.Drawing.Size(70, 50);
            this.btnSpike.TabIndex = 8;
            this.btnSpike.Text = "Spike";
            this.btnSpike.UseVisualStyleBackColor = true;
            this.btnSpike.Click += new System.EventHandler(this.btnSpike_Click);
            // 
            // btnStation
            // 
            this.btnStation.Location = new System.Drawing.Point(165, 689);
            this.btnStation.Name = "btnStation";
            this.btnStation.Size = new System.Drawing.Size(70, 50);
            this.btnStation.TabIndex = 9;
            this.btnStation.Text = "Harmony";
            this.btnStation.UseVisualStyleBackColor = true;
            this.btnStation.Click += new System.EventHandler(this.btnStation_Click);
            // 
            // btnStatistic
            // 
            this.btnStatistic.Location = new System.Drawing.Point(545, 634);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.Size = new System.Drawing.Size(70, 50);
            this.btnStatistic.TabIndex = 10;
            this.btnStatistic.Text = "Statistic";
            this.btnStatistic.UseVisualStyleBackColor = true;
            this.btnStatistic.Click += new System.EventHandler(this.btnStatistic_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 610);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(299, 20);
            this.textBox1.TabIndex = 11;
            // 
            // btnAutoCorre
            // 
            this.btnAutoCorre.Location = new System.Drawing.Point(241, 554);
            this.btnAutoCorre.Name = "btnAutoCorre";
            this.btnAutoCorre.Size = new System.Drawing.Size(70, 50);
            this.btnAutoCorre.TabIndex = 12;
            this.btnAutoCorre.Text = "Auto Correlation";
            this.btnAutoCorre.UseVisualStyleBackColor = true;
            this.btnAutoCorre.Click += new System.EventHandler(this.btnAutoCorre_Click);
            // 
            // btnCrossCorre
            // 
            this.btnCrossCorre.Location = new System.Drawing.Point(165, 554);
            this.btnCrossCorre.Name = "btnCrossCorre";
            this.btnCrossCorre.Size = new System.Drawing.Size(70, 50);
            this.btnCrossCorre.TabIndex = 13;
            this.btnCrossCorre.Text = "Cross Correlation";
            this.btnCrossCorre.UseVisualStyleBackColor = true;
            this.btnCrossCorre.Click += new System.EventHandler(this.btnCrossCorre_Click);
            // 
            // btnFastFourier
            // 
            this.btnFastFourier.Location = new System.Drawing.Point(89, 689);
            this.btnFastFourier.Name = "btnFastFourier";
            this.btnFastFourier.Size = new System.Drawing.Size(70, 50);
            this.btnFastFourier.TabIndex = 14;
            this.btnFastFourier.Text = "Harmony dat";
            this.btnFastFourier.UseVisualStyleBackColor = true;
            this.btnFastFourier.Click += new System.EventHandler(this.btnFastFourier_Click);
            // 
            // btnSignal
            // 
            this.btnSignal.Location = new System.Drawing.Point(241, 690);
            this.btnSignal.Name = "btnSignal";
            this.btnSignal.Size = new System.Drawing.Size(70, 50);
            this.btnSignal.TabIndex = 15;
            this.btnSignal.Text = "Signal";
            this.btnSignal.UseVisualStyleBackColor = true;
            this.btnSignal.Click += new System.EventHandler(this.btnSignal_Click);
            // 
            // btnRebuilt
            // 
            this.btnRebuilt.Location = new System.Drawing.Point(12, 554);
            this.btnRebuilt.Name = "btnRebuilt";
            this.btnRebuilt.Size = new System.Drawing.Size(70, 50);
            this.btnRebuilt.TabIndex = 16;
            this.btnRebuilt.Text = "Rebuild signal";
            this.btnRebuilt.UseVisualStyleBackColor = true;
            this.btnRebuilt.Click += new System.EventHandler(this.btnRebuilt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 554);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 50);
            this.button1.TabIndex = 17;
            this.button1.Text = "Rebuild signal v2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTestHistogram
            // 
            this.btnTestHistogram.Location = new System.Drawing.Point(317, 554);
            this.btnTestHistogram.Name = "btnTestHistogram";
            this.btnTestHistogram.Size = new System.Drawing.Size(70, 50);
            this.btnTestHistogram.TabIndex = 18;
            this.btnTestHistogram.Text = "Test histogram";
            this.btnTestHistogram.UseVisualStyleBackColor = true;
            this.btnTestHistogram.Click += new System.EventHandler(this.btnTestHistogram_Click);
            // 
            // chartHis
            // 
            chartArea2.Name = "ChartArea1";
            this.chartHis.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartHis.Legends.Add(legend2);
            this.chartHis.Location = new System.Drawing.Point(929, 12);
            this.chartHis.Name = "chartHis";
            this.chartHis.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Histogram";
            this.chartHis.Series.Add(series8);
            this.chartHis.Size = new System.Drawing.Size(861, 259);
            this.chartHis.TabIndex = 19;
            this.chartHis.Text = "Histogram";
            // 
            // chartAutoCorr
            // 
            chartArea3.Name = "ChartArea1";
            this.chartAutoCorr.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartAutoCorr.Legends.Add(legend3);
            this.chartAutoCorr.Location = new System.Drawing.Point(929, 277);
            this.chartAutoCorr.Name = "chartAutoCorr";
            this.chartAutoCorr.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Auto corr";
            series9.Points.Add(dataPoint1);
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Legend = "Legend1";
            series10.Name = "My AutoC";
            this.chartAutoCorr.Series.Add(series9);
            this.chartAutoCorr.Series.Add(series10);
            this.chartAutoCorr.Size = new System.Drawing.Size(861, 259);
            this.chartAutoCorr.TabIndex = 20;
            this.chartAutoCorr.Text = "Autocorrelation";
            // 
            // chartFourier
            // 
            chartArea4.Name = "ChartArea1";
            this.chartFourier.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartFourier.Legends.Add(legend4);
            this.chartFourier.Location = new System.Drawing.Point(929, 543);
            this.chartFourier.Name = "chartFourier";
            this.chartFourier.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Legend1";
            series11.Name = "Fourier";
            this.chartFourier.Series.Add(series11);
            this.chartFourier.Size = new System.Drawing.Size(861, 259);
            this.chartFourier.TabIndex = 21;
            this.chartFourier.Text = "Fourier Transform";
            // 
            // btnFluctuation
            // 
            this.btnFluctuation.Location = new System.Drawing.Point(393, 554);
            this.btnFluctuation.Name = "btnFluctuation";
            this.btnFluctuation.Size = new System.Drawing.Size(69, 50);
            this.btnFluctuation.TabIndex = 22;
            this.btnFluctuation.Text = "Fluctuation";
            this.btnFluctuation.UseVisualStyleBackColor = true;
            this.btnFluctuation.Click += new System.EventHandler(this.btnFluctuation_Click);
            // 
            // btnAntiSpike
            // 
            this.btnAntiSpike.Location = new System.Drawing.Point(392, 692);
            this.btnAntiSpike.Name = "btnAntiSpike";
            this.btnAntiSpike.Size = new System.Drawing.Size(70, 48);
            this.btnAntiSpike.TabIndex = 24;
            this.btnAntiSpike.Text = "Anti Spike";
            this.btnAntiSpike.UseVisualStyleBackColor = true;
            this.btnAntiSpike.Click += new System.EventHandler(this.btnAntiSpike_Click);
            // 
            // btnAntiShift
            // 
            this.btnAntiShift.Location = new System.Drawing.Point(317, 691);
            this.btnAntiShift.Name = "btnAntiShift";
            this.btnAntiShift.Size = new System.Drawing.Size(70, 48);
            this.btnAntiShift.TabIndex = 26;
            this.btnAntiShift.Text = "Anti Shift";
            this.btnAntiShift.UseVisualStyleBackColor = true;
            this.btnAntiShift.Click += new System.EventHandler(this.btnAntiShift_Click);
            // 
            // btnExtractTrend
            // 
            this.btnExtractTrend.Location = new System.Drawing.Point(469, 692);
            this.btnExtractTrend.Name = "btnExtractTrend";
            this.btnExtractTrend.Size = new System.Drawing.Size(70, 47);
            this.btnExtractTrend.TabIndex = 27;
            this.btnExtractTrend.Text = "Extract trend";
            this.btnExtractTrend.UseVisualStyleBackColor = true;
            this.btnExtractTrend.Click += new System.EventHandler(this.btnExtractTrend_Click);
            // 
            // btnEliminate
            // 
            this.btnEliminate.Location = new System.Drawing.Point(545, 692);
            this.btnEliminate.Name = "btnEliminate";
            this.btnEliminate.Size = new System.Drawing.Size(70, 47);
            this.btnEliminate.TabIndex = 28;
            this.btnEliminate.Text = "Eliminate trend";
            this.btnEliminate.UseVisualStyleBackColor = true;
            this.btnEliminate.Click += new System.EventHandler(this.btnEliminate_Click);
            // 
            // btnStandard
            // 
            this.btnStandard.Location = new System.Drawing.Point(469, 554);
            this.btnStandard.Name = "btnStandard";
            this.btnStandard.Size = new System.Drawing.Size(70, 50);
            this.btnStandard.TabIndex = 29;
            this.btnStandard.Text = "Standard Deviation";
            this.btnStandard.UseVisualStyleBackColor = true;
            this.btnStandard.Click += new System.EventHandler(this.btnStandard_Click);
            // 
            // btnStandardDeviationHarmony
            // 
            this.btnStandardDeviationHarmony.Location = new System.Drawing.Point(545, 554);
            this.btnStandardDeviationHarmony.Name = "btnStandardDeviationHarmony";
            this.btnStandardDeviationHarmony.Size = new System.Drawing.Size(70, 50);
            this.btnStandardDeviationHarmony.TabIndex = 30;
            this.btnStandardDeviationHarmony.Text = "Harmony Standard";
            this.btnStandardDeviationHarmony.UseVisualStyleBackColor = true;
            this.btnStandardDeviationHarmony.Click += new System.EventHandler(this.btnStandardDeviationHarmony_Click);
            // 
            // btnLinePitch
            // 
            this.btnLinePitch.Location = new System.Drawing.Point(621, 554);
            this.btnLinePitch.Name = "btnLinePitch";
            this.btnLinePitch.Size = new System.Drawing.Size(76, 50);
            this.btnLinePitch.TabIndex = 31;
            this.btnLinePitch.Text = "Line And Pitch";
            this.btnLinePitch.UseVisualStyleBackColor = true;
            this.btnLinePitch.Click += new System.EventHandler(this.btnLinePitch_Click);
            // 
            // btnSineAndEp
            // 
            this.btnSineAndEp.Location = new System.Drawing.Point(702, 554);
            this.btnSineAndEp.Name = "btnSineAndEp";
            this.btnSineAndEp.Size = new System.Drawing.Size(75, 50);
            this.btnSineAndEp.TabIndex = 34;
            this.btnSineAndEp.Text = "Sine and Epsilon";
            this.btnSineAndEp.Click += new System.EventHandler(this.btnSineAndEp_Click);
            // 
            // btnWave
            // 
            this.btnWave.Location = new System.Drawing.Point(783, 554);
            this.btnWave.Name = "btnWave";
            this.btnWave.Size = new System.Drawing.Size(75, 50);
            this.btnWave.TabIndex = 33;
            this.btnWave.Text = "Wave";
            this.btnWave.UseVisualStyleBackColor = true;
            this.btnWave.Click += new System.EventHandler(this.btnWave_Click);
            // 
            // btnLowPassFilter
            // 
            this.btnLowPassFilter.Location = new System.Drawing.Point(622, 634);
            this.btnLowPassFilter.Name = "btnLowPassFilter";
            this.btnLowPassFilter.Size = new System.Drawing.Size(75, 47);
            this.btnLowPassFilter.TabIndex = 35;
            this.btnLowPassFilter.Text = "Low Pass Filter";
            this.btnLowPassFilter.UseVisualStyleBackColor = true;
            this.btnLowPassFilter.Click += new System.EventHandler(this.btnLowPassFilter_Click);
            // 
            // btnReadWav
            // 
            this.btnReadWav.Location = new System.Drawing.Point(622, 752);
            this.btnReadWav.Name = "btnReadWav";
            this.btnReadWav.Size = new System.Drawing.Size(75, 50);
            this.btnReadWav.TabIndex = 36;
            this.btnReadWav.Text = "Record";
            this.btnReadWav.UseVisualStyleBackColor = true;
            this.btnReadWav.Click += new System.EventHandler(this.btnReadWav_Click);
            // 
            // btnHighPassFilter
            // 
            this.btnHighPassFilter.Location = new System.Drawing.Point(705, 636);
            this.btnHighPassFilter.Name = "btnHighPassFilter";
            this.btnHighPassFilter.Size = new System.Drawing.Size(75, 47);
            this.btnHighPassFilter.TabIndex = 37;
            this.btnHighPassFilter.Text = "High Pass Filter";
            this.btnHighPassFilter.UseVisualStyleBackColor = true;
            this.btnHighPassFilter.Click += new System.EventHandler(this.btnHighPassFilter_Click);
            // 
            // btnBandPassFilter
            // 
            this.btnBandPassFilter.Location = new System.Drawing.Point(786, 691);
            this.btnBandPassFilter.Name = "btnBandPassFilter";
            this.btnBandPassFilter.Size = new System.Drawing.Size(75, 47);
            this.btnBandPassFilter.TabIndex = 38;
            this.btnBandPassFilter.Text = "Band Pass Filter";
            this.btnBandPassFilter.UseVisualStyleBackColor = true;
            this.btnBandPassFilter.Click += new System.EventHandler(this.btnBandPassFilter_Click);
            // 
            // btnBandStopFilter
            // 
            this.btnBandStopFilter.Location = new System.Drawing.Point(786, 634);
            this.btnBandStopFilter.Name = "btnBandStopFilter";
            this.btnBandStopFilter.Size = new System.Drawing.Size(72, 50);
            this.btnBandStopFilter.TabIndex = 39;
            this.btnBandStopFilter.Text = "Band Stop Filter";
            this.btnBandStopFilter.UseVisualStyleBackColor = true;
            this.btnBandStopFilter.Click += new System.EventHandler(this.btnBandStopFilter_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Location = new System.Drawing.Point(702, 752);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(75, 50);
            this.btnStopRecord.TabIndex = 40;
            this.btnStopRecord.Text = "Stop and Save";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // btnWavFFt
            // 
            this.btnWavFFt.Location = new System.Drawing.Point(786, 752);
            this.btnWavFFt.Name = "btnWavFFt";
            this.btnWavFFt.Size = new System.Drawing.Size(72, 50);
            this.btnWavFFt.TabIndex = 41;
            this.btnWavFFt.Text = "Wav FFT";
            this.btnWavFFt.UseVisualStyleBackColor = true;
            this.btnWavFFt.Click += new System.EventHandler(this.btnWavFFt_Click);
            // 
            // btnSeismic
            // 
            this.btnSeismic.Location = new System.Drawing.Point(13, 752);
            this.btnSeismic.Name = "btnSeismic";
            this.btnSeismic.Size = new System.Drawing.Size(69, 50);
            this.btnSeismic.TabIndex = 42;
            this.btnSeismic.Text = "Seismic ";
            this.btnSeismic.UseVisualStyleBackColor = true;
            this.btnSeismic.Click += new System.EventHandler(this.btnSeismic_Click);
            // 
            // btnExam
            // 
            this.btnExam.Location = new System.Drawing.Point(89, 752);
            this.btnExam.Name = "btnExam";
            this.btnExam.Size = new System.Drawing.Size(70, 50);
            this.btnExam.TabIndex = 43;
            this.btnExam.Text = "Exam ";
            this.btnExam.UseVisualStyleBackColor = true;
            this.btnExam.Click += new System.EventHandler(this.btnExam_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1809, 861);
            this.Controls.Add(this.btnExam);
            this.Controls.Add(this.btnSeismic);
            this.Controls.Add(this.btnWavFFt);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnBandStopFilter);
            this.Controls.Add(this.btnBandPassFilter);
            this.Controls.Add(this.btnHighPassFilter);
            this.Controls.Add(this.btnReadWav);
            this.Controls.Add(this.btnLowPassFilter);
            this.Controls.Add(this.btnWave);
            this.Controls.Add(this.btnSineAndEp);
            this.Controls.Add(this.btnLinePitch);
            this.Controls.Add(this.btnStandardDeviationHarmony);
            this.Controls.Add(this.btnStandard);
            this.Controls.Add(this.btnEliminate);
            this.Controls.Add(this.btnExtractTrend);
            this.Controls.Add(this.btnAntiShift);
            this.Controls.Add(this.btnAntiSpike);
            this.Controls.Add(this.btnFluctuation);
            this.Controls.Add(this.chartFourier);
            this.Controls.Add(this.chartAutoCorr);
            this.Controls.Add(this.chartHis);
            this.Controls.Add(this.btnTestHistogram);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRebuilt);
            this.Controls.Add(this.btnSignal);
            this.Controls.Add(this.btnFastFourier);
            this.Controls.Add(this.btnCrossCorre);
            this.Controls.Add(this.btnAutoCorre);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnStatistic);
            this.Controls.Add(this.btnStation);
            this.Controls.Add(this.btnSpike);
            this.Controls.Add(this.btnShift);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.btnFinal);
            this.Controls.Add(this.negaEps);
            this.Controls.Add(this.btnEps);
            this.Controls.Add(this.btnNegativeLine);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAutoCorr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFourier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnNegativeLine;
        private System.Windows.Forms.Button btnEps;
        private System.Windows.Forms.Button negaEps;
        private System.Windows.Forms.Button btnFinal;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnShift;
        private System.Windows.Forms.Button btnSpike;
        private System.Windows.Forms.Button btnStation;
        private System.Windows.Forms.Button btnStatistic;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAutoCorre;
        private System.Windows.Forms.Button btnCrossCorre;
        private System.Windows.Forms.Button btnFastFourier;
        private System.Windows.Forms.Button btnSignal;
        private System.Windows.Forms.Button btnRebuilt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTestHistogram;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHis;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAutoCorr;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFourier;
        private System.Windows.Forms.Button btnFluctuation;
        private System.Windows.Forms.Button btnAntiSpike;
        private System.Windows.Forms.Button btnAntiShift;
        private System.Windows.Forms.Button btnExtractTrend;
        private System.Windows.Forms.Button btnEliminate;
        private System.Windows.Forms.Button btnStandard;
        private System.Windows.Forms.Button btnStandardDeviationHarmony;
        private System.Windows.Forms.Button btnLinePitch;
        private System.Windows.Forms.Button btnSineAndEp;
        private System.Windows.Forms.Button btnWave;
        private System.Windows.Forms.Button btnLowPassFilter;
        private System.Windows.Forms.Button btnReadWav;
        private System.Windows.Forms.Button btnHighPassFilter;
        private System.Windows.Forms.Button btnBandPassFilter;
        private System.Windows.Forms.Button btnBandStopFilter;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnWavFFt;
        private System.Windows.Forms.Button btnSeismic;
        private System.Windows.Forms.Button btnExam;
    }
}