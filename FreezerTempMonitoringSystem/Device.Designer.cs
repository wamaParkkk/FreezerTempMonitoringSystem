
namespace FreezerTempMonitoringSystem
{
    partial class Device
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelDevice = new System.Windows.Forms.Panel();
            this.textBoxDeviceInfo = new System.Windows.Forms.TextBox();
            this.btnMinMaxTempSave = new System.Windows.Forms.Button();
            this.chartDeviceTemp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rjToggleButtonDeviceUse = new FreezerTempMonitoringSystem.RJControls.RJToggleButton();
            this.labelDeviceTempAlarm = new System.Windows.Forms.Label();
            this.btnDeviceMaxTempDown = new System.Windows.Forms.Button();
            this.btnDeviceMaxTempUp = new System.Windows.Forms.Button();
            this.textBoxDeviceMaxTemp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeviceMinTempDown = new System.Windows.Forms.Button();
            this.btnDeviceMinTempUp = new System.Windows.Forms.Button();
            this.textBoxDeviceMinTemp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDeviceTemp = new System.Windows.Forms.TextBox();
            this.panelDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeviceTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDevice
            // 
            this.panelDevice.BackColor = System.Drawing.Color.AliceBlue;
            this.panelDevice.Controls.Add(this.textBoxDeviceInfo);
            this.panelDevice.Controls.Add(this.btnMinMaxTempSave);
            this.panelDevice.Controls.Add(this.chartDeviceTemp);
            this.panelDevice.Controls.Add(this.rjToggleButtonDeviceUse);
            this.panelDevice.Controls.Add(this.labelDeviceTempAlarm);
            this.panelDevice.Controls.Add(this.btnDeviceMaxTempDown);
            this.panelDevice.Controls.Add(this.btnDeviceMaxTempUp);
            this.panelDevice.Controls.Add(this.textBoxDeviceMaxTemp);
            this.panelDevice.Controls.Add(this.label4);
            this.panelDevice.Controls.Add(this.btnDeviceMinTempDown);
            this.panelDevice.Controls.Add(this.btnDeviceMinTempUp);
            this.panelDevice.Controls.Add(this.textBoxDeviceMinTemp);
            this.panelDevice.Controls.Add(this.label3);
            this.panelDevice.Controls.Add(this.textBoxDeviceTemp);
            this.panelDevice.Location = new System.Drawing.Point(3, 3);
            this.panelDevice.Name = "panelDevice";
            this.panelDevice.Size = new System.Drawing.Size(356, 360);
            this.panelDevice.TabIndex = 17;
            // 
            // textBoxDeviceInfo
            // 
            this.textBoxDeviceInfo.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeviceInfo.ForeColor = System.Drawing.Color.Navy;
            this.textBoxDeviceInfo.Location = new System.Drawing.Point(5, 5);
            this.textBoxDeviceInfo.Name = "textBoxDeviceInfo";
            this.textBoxDeviceInfo.Size = new System.Drawing.Size(170, 39);
            this.textBoxDeviceInfo.TabIndex = 34;
            // 
            // btnMinMaxTempSave
            // 
            this.btnMinMaxTempSave.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinMaxTempSave.ForeColor = System.Drawing.Color.Navy;
            this.btnMinMaxTempSave.Location = new System.Drawing.Point(100, 48);
            this.btnMinMaxTempSave.Name = "btnMinMaxTempSave";
            this.btnMinMaxTempSave.Size = new System.Drawing.Size(75, 23);
            this.btnMinMaxTempSave.TabIndex = 33;
            this.btnMinMaxTempSave.Text = "Save";
            this.btnMinMaxTempSave.UseVisualStyleBackColor = true;
            this.btnMinMaxTempSave.Click += new System.EventHandler(this.btnMinMaxTempSave_Click);
            // 
            // chartDeviceTemp
            // 
            this.chartDeviceTemp.BackColor = System.Drawing.Color.Ivory;
            chartArea2.Name = "ChartArea1";
            this.chartDeviceTemp.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartDeviceTemp.Legends.Add(legend2);
            this.chartDeviceTemp.Location = new System.Drawing.Point(5, 147);
            this.chartDeviceTemp.Name = "chartDeviceTemp";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "SeriesTempValue";
            this.chartDeviceTemp.Series.Add(series2);
            this.chartDeviceTemp.Size = new System.Drawing.Size(346, 207);
            this.chartDeviceTemp.TabIndex = 17;
            this.chartDeviceTemp.Text = "chart1";
            // 
            // rjToggleButtonDeviceUse
            // 
            this.rjToggleButtonDeviceUse.AutoSize = true;
            this.rjToggleButtonDeviceUse.Location = new System.Drawing.Point(5, 48);
            this.rjToggleButtonDeviceUse.MinimumSize = new System.Drawing.Size(45, 22);
            this.rjToggleButtonDeviceUse.Name = "rjToggleButtonDeviceUse";
            this.rjToggleButtonDeviceUse.OffBackColor = System.Drawing.Color.Gray;
            this.rjToggleButtonDeviceUse.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rjToggleButtonDeviceUse.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rjToggleButtonDeviceUse.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rjToggleButtonDeviceUse.Size = new System.Drawing.Size(45, 22);
            this.rjToggleButtonDeviceUse.TabIndex = 32;
            this.rjToggleButtonDeviceUse.UseVisualStyleBackColor = true;
            this.rjToggleButtonDeviceUse.Click += new System.EventHandler(this.rjToggleButtonDeviceUse_Click);
            // 
            // labelDeviceTempAlarm
            // 
            this.labelDeviceTempAlarm.BackColor = System.Drawing.Color.Silver;
            this.labelDeviceTempAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDeviceTempAlarm.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDeviceTempAlarm.Location = new System.Drawing.Point(181, 5);
            this.labelDeviceTempAlarm.Name = "labelDeviceTempAlarm";
            this.labelDeviceTempAlarm.Size = new System.Drawing.Size(170, 40);
            this.labelDeviceTempAlarm.TabIndex = 31;
            this.labelDeviceTempAlarm.Text = "Normal";
            this.labelDeviceTempAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeviceMaxTempDown
            // 
            this.btnDeviceMaxTempDown.BackColor = System.Drawing.Color.White;
            this.btnDeviceMaxTempDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeviceMaxTempDown.Location = new System.Drawing.Point(150, 109);
            this.btnDeviceMaxTempDown.Name = "btnDeviceMaxTempDown";
            this.btnDeviceMaxTempDown.Size = new System.Drawing.Size(25, 29);
            this.btnDeviceMaxTempDown.TabIndex = 30;
            this.btnDeviceMaxTempDown.Tag = "1";
            this.btnDeviceMaxTempDown.Text = "▼";
            this.btnDeviceMaxTempDown.UseVisualStyleBackColor = false;
            this.btnDeviceMaxTempDown.Click += new System.EventHandler(this.Max_Click);
            // 
            // btnDeviceMaxTempUp
            // 
            this.btnDeviceMaxTempUp.BackColor = System.Drawing.Color.White;
            this.btnDeviceMaxTempUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeviceMaxTempUp.Location = new System.Drawing.Point(150, 77);
            this.btnDeviceMaxTempUp.Name = "btnDeviceMaxTempUp";
            this.btnDeviceMaxTempUp.Size = new System.Drawing.Size(25, 29);
            this.btnDeviceMaxTempUp.TabIndex = 29;
            this.btnDeviceMaxTempUp.Tag = "0";
            this.btnDeviceMaxTempUp.Text = "▲";
            this.btnDeviceMaxTempUp.UseVisualStyleBackColor = false;
            this.btnDeviceMaxTempUp.Click += new System.EventHandler(this.Max_Click);
            // 
            // textBoxDeviceMaxTemp
            // 
            this.textBoxDeviceMaxTemp.BackColor = System.Drawing.Color.MidnightBlue;
            this.textBoxDeviceMaxTemp.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeviceMaxTemp.ForeColor = System.Drawing.Color.White;
            this.textBoxDeviceMaxTemp.Location = new System.Drawing.Point(93, 109);
            this.textBoxDeviceMaxTemp.Name = "textBoxDeviceMaxTemp";
            this.textBoxDeviceMaxTemp.ReadOnly = true;
            this.textBoxDeviceMaxTemp.Size = new System.Drawing.Size(57, 29);
            this.textBoxDeviceMaxTemp.TabIndex = 28;
            this.textBoxDeviceMaxTemp.Text = "0.00";
            this.textBoxDeviceMaxTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 29);
            this.label4.TabIndex = 27;
            this.label4.Text = "Max";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeviceMinTempDown
            // 
            this.btnDeviceMinTempDown.BackColor = System.Drawing.Color.White;
            this.btnDeviceMinTempDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeviceMinTempDown.Location = new System.Drawing.Point(62, 109);
            this.btnDeviceMinTempDown.Name = "btnDeviceMinTempDown";
            this.btnDeviceMinTempDown.Size = new System.Drawing.Size(25, 29);
            this.btnDeviceMinTempDown.TabIndex = 26;
            this.btnDeviceMinTempDown.Tag = "1";
            this.btnDeviceMinTempDown.Text = "▼";
            this.btnDeviceMinTempDown.UseVisualStyleBackColor = false;
            this.btnDeviceMinTempDown.Click += new System.EventHandler(this.Min_Click);
            // 
            // btnDeviceMinTempUp
            // 
            this.btnDeviceMinTempUp.BackColor = System.Drawing.Color.White;
            this.btnDeviceMinTempUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeviceMinTempUp.Location = new System.Drawing.Point(62, 77);
            this.btnDeviceMinTempUp.Name = "btnDeviceMinTempUp";
            this.btnDeviceMinTempUp.Size = new System.Drawing.Size(25, 29);
            this.btnDeviceMinTempUp.TabIndex = 25;
            this.btnDeviceMinTempUp.Tag = "0";
            this.btnDeviceMinTempUp.Text = "▲";
            this.btnDeviceMinTempUp.UseVisualStyleBackColor = false;
            this.btnDeviceMinTempUp.Click += new System.EventHandler(this.Min_Click);
            // 
            // textBoxDeviceMinTemp
            // 
            this.textBoxDeviceMinTemp.BackColor = System.Drawing.Color.MidnightBlue;
            this.textBoxDeviceMinTemp.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeviceMinTemp.ForeColor = System.Drawing.Color.White;
            this.textBoxDeviceMinTemp.Location = new System.Drawing.Point(5, 109);
            this.textBoxDeviceMinTemp.Name = "textBoxDeviceMinTemp";
            this.textBoxDeviceMinTemp.ReadOnly = true;
            this.textBoxDeviceMinTemp.Size = new System.Drawing.Size(57, 29);
            this.textBoxDeviceMinTemp.TabIndex = 22;
            this.textBoxDeviceMinTemp.Text = "0.00";
            this.textBoxDeviceMinTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 29);
            this.label3.TabIndex = 21;
            this.label3.Text = "Min";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDeviceTemp
            // 
            this.textBoxDeviceTemp.BackColor = System.Drawing.Color.Black;
            this.textBoxDeviceTemp.Font = new System.Drawing.Font("Nirmala UI", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeviceTemp.ForeColor = System.Drawing.Color.Yellow;
            this.textBoxDeviceTemp.Location = new System.Drawing.Point(181, 60);
            this.textBoxDeviceTemp.Name = "textBoxDeviceTemp";
            this.textBoxDeviceTemp.ReadOnly = true;
            this.textBoxDeviceTemp.Size = new System.Drawing.Size(170, 78);
            this.textBoxDeviceTemp.TabIndex = 16;
            this.textBoxDeviceTemp.Text = "--";
            this.textBoxDeviceTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Device
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDevice);
            this.Name = "Device";
            this.Size = new System.Drawing.Size(362, 367);
            this.Load += new System.EventHandler(this.Device_Load);
            this.panelDevice.ResumeLayout(false);
            this.panelDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeviceTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDevice;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartDeviceTemp;
        public RJControls.RJToggleButton rjToggleButtonDeviceUse;
        public System.Windows.Forms.Label labelDeviceTempAlarm;
        public System.Windows.Forms.Button btnDeviceMaxTempDown;
        public System.Windows.Forms.Button btnDeviceMaxTempUp;
        public System.Windows.Forms.TextBox textBoxDeviceMaxTemp;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnDeviceMinTempDown;
        public System.Windows.Forms.Button btnDeviceMinTempUp;
        public System.Windows.Forms.TextBox textBoxDeviceMinTemp;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxDeviceTemp;
        private System.Windows.Forms.Button btnMinMaxTempSave;
        public System.Windows.Forms.TextBox textBoxDeviceInfo;
    }
}
