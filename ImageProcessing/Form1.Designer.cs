namespace ImageProcessing
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chartRow = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCollumn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnNearest = new System.Windows.Forms.Button();
            this.btnBilinear = new System.Windows.Forms.Button();
            this.btnLogTransform = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnHisEqual = new System.Windows.Forms.Button();
            this.btnNegative = new System.Windows.Forms.Button();
            this.ScaleTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xrayButton = new System.Windows.Forms.Button();
            this.btnAddNoises = new System.Windows.Forms.Button();
            this.btnSalfPepper = new System.Windows.Forms.Button();
            this.btnSumNoise = new System.Windows.Forms.Button();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnContourLPF = new System.Windows.Forms.Button();
            this.btnContourHPF = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnErosion = new System.Windows.Forms.Button();
            this.btnDilation = new System.Windows.Forms.Button();
            this.btnBlurred = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numStones = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCollumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Location = new System.Drawing.Point(12, 821);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(70, 40);
            this.btnOpenImage.TabIndex = 0;
            this.btnOpenImage.Text = "Open image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1284, 362);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 453);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1284, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(494, 344);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // chartRow
            // 
            chartArea1.Name = "ChartArea1";
            this.chartRow.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRow.Legends.Add(legend1);
            this.chartRow.Location = new System.Drawing.Point(12, 12);
            this.chartRow.Name = "chartRow";
            this.chartRow.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chartRow.Series.Add(series1);
            this.chartRow.Series.Add(series2);
            this.chartRow.Size = new System.Drawing.Size(344, 344);
            this.chartRow.TabIndex = 3;
            this.chartRow.Text = "Image Row";
            // 
            // chartCollumn
            // 
            chartArea2.Name = "ChartArea1";
            this.chartCollumn.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartCollumn.Legends.Add(legend2);
            this.chartCollumn.Location = new System.Drawing.Point(362, 12);
            this.chartCollumn.Name = "chartCollumn";
            this.chartCollumn.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            this.chartCollumn.Series.Add(series3);
            this.chartCollumn.Series.Add(series4);
            this.chartCollumn.Size = new System.Drawing.Size(404, 343);
            this.chartCollumn.TabIndex = 4;
            this.chartCollumn.Text = "Collumn image";
            // 
            // btnNearest
            // 
            this.btnNearest.Location = new System.Drawing.Point(88, 821);
            this.btnNearest.Name = "btnNearest";
            this.btnNearest.Size = new System.Drawing.Size(70, 40);
            this.btnNearest.TabIndex = 5;
            this.btnNearest.Text = "Nearest Neighbor";
            this.btnNearest.UseVisualStyleBackColor = true;
            this.btnNearest.Click += new System.EventHandler(this.btnNearest_Click);
            // 
            // btnBilinear
            // 
            this.btnBilinear.Location = new System.Drawing.Point(164, 821);
            this.btnBilinear.Name = "btnBilinear";
            this.btnBilinear.Size = new System.Drawing.Size(70, 40);
            this.btnBilinear.TabIndex = 6;
            this.btnBilinear.Text = "Bilinear Inter";
            this.btnBilinear.UseVisualStyleBackColor = true;
            this.btnBilinear.Click += new System.EventHandler(this.btnBilinear_Click);
            // 
            // btnLogTransform
            // 
            this.btnLogTransform.Location = new System.Drawing.Point(240, 821);
            this.btnLogTransform.Name = "btnLogTransform";
            this.btnLogTransform.Size = new System.Drawing.Size(70, 40);
            this.btnLogTransform.TabIndex = 7;
            this.btnLogTransform.Text = "Logarit Transform";
            this.btnLogTransform.UseVisualStyleBackColor = true;
            this.btnLogTransform.Click += new System.EventHandler(this.btnLogTransform_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(316, 821);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 40);
            this.button4.TabIndex = 8;
            this.button4.Text = "Gamma Transform";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chartHistogram
            // 
            chartArea3.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartHistogram.Legends.Add(legend3);
            this.chartHistogram.Location = new System.Drawing.Point(12, 362);
            this.chartHistogram.Name = "chartHistogram";
            this.chartHistogram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartHistogram.Series.Add(series5);
            this.chartHistogram.Size = new System.Drawing.Size(344, 344);
            this.chartHistogram.TabIndex = 9;
            this.chartHistogram.Text = "chart1";
            // 
            // btnHisEqual
            // 
            this.btnHisEqual.Location = new System.Drawing.Point(468, 821);
            this.btnHisEqual.Name = "btnHisEqual";
            this.btnHisEqual.Size = new System.Drawing.Size(70, 40);
            this.btnHisEqual.TabIndex = 10;
            this.btnHisEqual.Text = "Histogram equalization";
            this.btnHisEqual.UseVisualStyleBackColor = true;
            this.btnHisEqual.Click += new System.EventHandler(this.btnHisEqual_Click);
            // 
            // btnNegative
            // 
            this.btnNegative.Location = new System.Drawing.Point(392, 821);
            this.btnNegative.Name = "btnNegative";
            this.btnNegative.Size = new System.Drawing.Size(70, 40);
            this.btnNegative.TabIndex = 11;
            this.btnNegative.Text = "Negative Image";
            this.btnNegative.UseVisualStyleBackColor = true;
            this.btnNegative.Click += new System.EventHandler(this.btnNegative_Click);
            // 
            // ScaleTextbox
            // 
            this.ScaleTextbox.Location = new System.Drawing.Point(49, 795);
            this.ScaleTextbox.Name = "ScaleTextbox";
            this.ScaleTextbox.Size = new System.Drawing.Size(100, 20);
            this.ScaleTextbox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 798);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scale";
            // 
            // xrayButton
            // 
            this.xrayButton.Location = new System.Drawing.Point(544, 821);
            this.xrayButton.Name = "xrayButton";
            this.xrayButton.Size = new System.Drawing.Size(70, 40);
            this.xrayButton.TabIndex = 14;
            this.xrayButton.Text = "Xray image";
            this.xrayButton.UseVisualStyleBackColor = true;
            this.xrayButton.Click += new System.EventHandler(this.xrayButton_Click);
            // 
            // btnAddNoises
            // 
            this.btnAddNoises.Location = new System.Drawing.Point(620, 821);
            this.btnAddNoises.Name = "btnAddNoises";
            this.btnAddNoises.Size = new System.Drawing.Size(70, 40);
            this.btnAddNoises.TabIndex = 15;
            this.btnAddNoises.Text = "Random noises";
            this.btnAddNoises.UseVisualStyleBackColor = true;
            this.btnAddNoises.Click += new System.EventHandler(this.btnAddNoises_Click);
            // 
            // btnSalfPepper
            // 
            this.btnSalfPepper.Location = new System.Drawing.Point(696, 821);
            this.btnSalfPepper.Name = "btnSalfPepper";
            this.btnSalfPepper.Size = new System.Drawing.Size(70, 40);
            this.btnSalfPepper.TabIndex = 16;
            this.btnSalfPepper.Text = "Salf Pepper Noise";
            this.btnSalfPepper.UseVisualStyleBackColor = true;
            this.btnSalfPepper.Click += new System.EventHandler(this.btnSalfPepper_Click);
            // 
            // btnSumNoise
            // 
            this.btnSumNoise.Location = new System.Drawing.Point(772, 821);
            this.btnSumNoise.Name = "btnSumNoise";
            this.btnSumNoise.Size = new System.Drawing.Size(70, 40);
            this.btnSumNoise.TabIndex = 17;
            this.btnSumNoise.Text = "Random+SNP";
            this.btnSumNoise.UseVisualStyleBackColor = true;
            this.btnSumNoise.Click += new System.EventHandler(this.btnSumNoise_Click);
            // 
            // chart4
            // 
            chartArea4.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart4.Legends.Add(legend4);
            this.chart4.Location = new System.Drawing.Point(362, 361);
            this.chart4.Name = "chart4";
            this.chart4.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart4.Series.Add(series6);
            this.chart4.Size = new System.Drawing.Size(404, 345);
            this.chart4.TabIndex = 18;
            this.chart4.Text = "Chart cross cor";
            // 
            // btnContourLPF
            // 
            this.btnContourLPF.Location = new System.Drawing.Point(849, 821);
            this.btnContourLPF.Name = "btnContourLPF";
            this.btnContourLPF.Size = new System.Drawing.Size(74, 40);
            this.btnContourLPF.TabIndex = 19;
            this.btnContourLPF.Text = "Contour LPF";
            this.btnContourLPF.UseVisualStyleBackColor = true;
            this.btnContourLPF.Click += new System.EventHandler(this.btnContourLPF_Click);
            // 
            // btnContourHPF
            // 
            this.btnContourHPF.Location = new System.Drawing.Point(929, 821);
            this.btnContourHPF.Name = "btnContourHPF";
            this.btnContourHPF.Size = new System.Drawing.Size(74, 40);
            this.btnContourHPF.TabIndex = 20;
            this.btnContourHPF.Text = "Contour HPF";
            this.btnContourHPF.UseVisualStyleBackColor = true;
            this.btnContourHPF.Click += new System.EventHandler(this.btnContourHPF_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(784, 11);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(494, 344);
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1489, 835);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Sobel edge detector";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(965, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Laplacian edge detector";
            // 
            // btnErosion
            // 
            this.btnErosion.Location = new System.Drawing.Point(1009, 821);
            this.btnErosion.Name = "btnErosion";
            this.btnErosion.Size = new System.Drawing.Size(74, 40);
            this.btnErosion.TabIndex = 24;
            this.btnErosion.Text = "Erosion";
            this.btnErosion.UseVisualStyleBackColor = true;
            this.btnErosion.Click += new System.EventHandler(this.btnErosion_Click);
            // 
            // btnDilation
            // 
            this.btnDilation.Location = new System.Drawing.Point(1089, 821);
            this.btnDilation.Name = "btnDilation";
            this.btnDilation.Size = new System.Drawing.Size(74, 40);
            this.btnDilation.TabIndex = 25;
            this.btnDilation.Text = "Dilation";
            this.btnDilation.UseVisualStyleBackColor = true;
            this.btnDilation.Click += new System.EventHandler(this.btnDilation_Click);
            // 
            // btnBlurred
            // 
            this.btnBlurred.Location = new System.Drawing.Point(1169, 821);
            this.btnBlurred.Name = "btnBlurred";
            this.btnBlurred.Size = new System.Drawing.Size(74, 40);
            this.btnBlurred.TabIndex = 26;
            this.btnBlurred.Text = "Blurred Image";
            this.btnBlurred.UseVisualStyleBackColor = true;
            this.btnBlurred.Click += new System.EventHandler(this.btnBlurred_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(960, 482);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Stones:";
            // 
            // numStones
            // 
            this.numStones.Location = new System.Drawing.Point(1009, 479);
            this.numStones.Name = "numStones";
            this.numStones.Size = new System.Drawing.Size(100, 20);
            this.numStones.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 869);
            this.Controls.Add(this.numStones);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBlurred);
            this.Controls.Add(this.btnDilation);
            this.Controls.Add(this.btnErosion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnContourHPF);
            this.Controls.Add(this.btnContourLPF);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.btnSumNoise);
            this.Controls.Add(this.btnSalfPepper);
            this.Controls.Add(this.btnAddNoises);
            this.Controls.Add(this.xrayButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScaleTextbox);
            this.Controls.Add(this.btnNegative);
            this.Controls.Add(this.btnHisEqual);
            this.Controls.Add(this.chartHistogram);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnLogTransform);
            this.Controls.Add(this.btnBilinear);
            this.Controls.Add(this.btnNearest);
            this.Controls.Add(this.chartCollumn);
            this.Controls.Add(this.chartRow);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOpenImage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCollumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRow;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCollumn;
        private System.Windows.Forms.Button btnNearest;
        private System.Windows.Forms.Button btnBilinear;
        private System.Windows.Forms.Button btnLogTransform;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.Button btnHisEqual;
        private System.Windows.Forms.Button btnNegative;
        private System.Windows.Forms.TextBox ScaleTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button xrayButton;
        private System.Windows.Forms.Button btnAddNoises;
        private System.Windows.Forms.Button btnSalfPepper;
        private System.Windows.Forms.Button btnSumNoise;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.Button btnContourLPF;
        private System.Windows.Forms.Button btnContourHPF;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnErosion;
        private System.Windows.Forms.Button btnDilation;
        private System.Windows.Forms.Button btnBlurred;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numStones;
    }
}

