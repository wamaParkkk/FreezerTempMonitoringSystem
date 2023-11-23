using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FreezerTempMonitoringSystem
{
    public partial class Device : UserControl
    {
        private ServerForm m_ServerForm;

        double dTempMin;
        double dTempMax;
        byte ModuleNo;
        bool bUsed;
        int iLogWriteCnt;

        // Temp alarm flag
        public bool[] bTempAlarm = { false, false, false, false, false, false, false, false, false, false };
        int[] iTempAlarmCnt = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public Device(ServerForm parent, byte Module)
        {
            m_ServerForm = parent;
            ModuleNo = Module;

            InitializeComponent();
        }

        private void Device_Load(object sender, EventArgs e)
        {
            chartDeviceTemp.ChartAreas[0].AxisX.Interval = 10;
            chartDeviceTemp.ChartAreas[0].AxisX.Minimum = 0;
            chartDeviceTemp.ChartAreas[0].AxisX.Maximum = 100;

            chartDeviceTemp.ChartAreas[0].AxisY.Interval = 10;
            chartDeviceTemp.ChartAreas[0].AxisY.Minimum = -50.0;
            chartDeviceTemp.ChartAreas[0].AxisY.Maximum = 30.0;

            chartDeviceTemp.Series["SeriesTempValue"].BorderWidth = 3;
            chartDeviceTemp.Series["SeriesTempValue"].Color = Color.Red;

            _TEMP_PARAMETER_LOAD();

            if ((ModuleNo == 1 || ModuleNo == 3 || ModuleNo == 5 || ModuleNo == 7 || ModuleNo == 9))
                chartDeviceTemp.BackColor = Color.LavenderBlush;

            //bUsed = true;
            //rjToggleButtonDeviceUse.Checked = true;
        }
        
        public void _Display()
        {
            try
            {
                if (textBoxDeviceTemp.InvokeRequired)
                {
                    textBoxDeviceTemp.BeginInvoke(new MethodInvoker(delegate
                    {
                        if (bUsed)
                        {
                            if (!rjToggleButtonDeviceUse.Checked)
                                rjToggleButtonDeviceUse.Checked = true;

                            textBoxDeviceTemp.Text = m_ServerForm.iDeviceTemp[ModuleNo].ToString("0.00");

                            if (chartDeviceTemp.Series["SeriesTempValue"].Points.Count > 90)
                                chartDeviceTemp.Series["SeriesTempValue"].Points.RemoveAt(0);

                            chartDeviceTemp.Series["SeriesTempValue"].Points.AddXY(DateTime.Now.ToString("HH:mm:ss"), m_ServerForm.iDeviceTemp[ModuleNo]);


                            if ((m_ServerForm.iDeviceTemp[ModuleNo] < ServerForm.Configure_List.Configure_TempMin[ModuleNo]) ||
                                (m_ServerForm.iDeviceTemp[ModuleNo] > ServerForm.Configure_List.Configure_TempMax[ModuleNo]))
                            {
                                if (iTempAlarmCnt[ModuleNo] > 120)  // 1분 경과 후 알람 발생
                                {
                                    if (!bTempAlarm[ModuleNo])
                                        bTempAlarm[ModuleNo] = true;
                                }
                                else
                                {
                                    iTempAlarmCnt[ModuleNo]++;
                                }
                            }
                            else
                            {
                                if (bTempAlarm[ModuleNo] != false)
                                    bTempAlarm[ModuleNo] = false;

                                if (iTempAlarmCnt[ModuleNo] != 0)
                                    iTempAlarmCnt[ModuleNo] = 0;
                            }

                            if (bTempAlarm[ModuleNo])
                            {
                                labelDeviceTempAlarm.Text = "Alarm";
                                if (labelDeviceTempAlarm.BackColor != Color.Red)
                                    labelDeviceTempAlarm.BackColor = Color.Red;
                                else
                                    labelDeviceTempAlarm.BackColor = Color.Silver;
                            }
                            else
                            {
                                labelDeviceTempAlarm.Text = "Normal";
                                if (labelDeviceTempAlarm.BackColor != Color.Lime)
                                    labelDeviceTempAlarm.BackColor = Color.Lime;
                            }

                            if (!textBoxDeviceInfo.Enabled)
                                textBoxDeviceInfo.Enabled = true;

                            // Log 파일 저장
                            if (iLogWriteCnt >= 120)
                            {
                                _TEMP_LOG_SAVE(m_ServerForm.iDeviceTemp[ModuleNo].ToString("0.00"));

                                if (iLogWriteCnt != 1)
                                    iLogWriteCnt = 1;
                            }
                            else
                            {
                                iLogWriteCnt++;
                            }
                        }
                        else
                        {
                            if (rjToggleButtonDeviceUse.Checked != false)
                                rjToggleButtonDeviceUse.Checked = false;

                            textBoxDeviceTemp.Text = "--";

                            labelDeviceTempAlarm.Text = "Not used";
                            if (labelDeviceTempAlarm.BackColor != Color.Silver)
                                labelDeviceTempAlarm.BackColor = Color.Silver;

                            if (textBoxDeviceInfo.Enabled != false)
                                textBoxDeviceInfo.Enabled = false;

                            if (bTempAlarm[ModuleNo] != false)
                                bTempAlarm[ModuleNo] = false;

                            if (iTempAlarmCnt[ModuleNo] != 0)
                                iTempAlarmCnt[ModuleNo] = 0;

                            if (iLogWriteCnt != 1)
                                iLogWriteCnt = 1;
                        }
                    }));
                }
            }
            catch (Exception)
            {

            }
        }

        private void _TEMP_PARAMETER_LOAD()
        {            
            try
            {
                string sTmpData;
                string FileName = string.Format("FR{0}.txt", ModuleNo + 1);

                if (File.Exists(m_ServerForm.ConfigurePath + FileName))
                {
                    byte[] bytes;
                    using (var fs = File.Open(m_ServerForm.ConfigurePath + FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, (int)fs.Length);
                        sTmpData = Encoding.Default.GetString(bytes);

                        char sp = ',';
                        string[] spString = sTmpData.Split(sp);
                        for (int i = 0; i < spString.Length; i++)
                        {
                            textBoxDeviceInfo.Text = spString[0].ToString();

                            if (spString[1] == "True")
                                bUsed = true;
                            else
                                bUsed = false;

                            ServerForm.Configure_List.Configure_TempMin[ModuleNo] = double.Parse(spString[2]);
                            ServerForm.Configure_List.Configure_TempMax[ModuleNo] = double.Parse(spString[3]);

                            textBoxDeviceMinTemp.Text = (ServerForm.Configure_List.Configure_TempMin[ModuleNo]).ToString("0.00");
                            textBoxDeviceMaxTemp.Text = (ServerForm.Configure_List.Configure_TempMax[ModuleNo]).ToString("0.00");

                            dTempMin = ServerForm.Configure_List.Configure_TempMin[ModuleNo];
                            dTempMax = ServerForm.Configure_List.Configure_TempMax[ModuleNo];
                        }
                    }
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
        }

        private void Min_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string strTmp = btn.Tag.ToString();
            
            bool bChk = double.TryParse(textBoxDeviceMinTemp.Text.ToString(), out double dVal);
            if (bChk)
            {
                dTempMin = dVal;
            }                
            else
            {
                MessageBox.Show("잘못 된 값이 입력되었습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            switch (strTmp)
            {                
                case "0":
                    {
                        if (dTempMin < dTempMax)
                        {
                            dTempMin = dTempMin + 0.5;
                            textBoxDeviceMinTemp.Text = dTempMin.ToString("0.00");
                        }
                        else
                        {
                            MessageBox.Show("Maximum 값 보다 큽니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    break;

                case "1":
                    {
                        dTempMin = dTempMin - 0.5;
                        textBoxDeviceMinTemp.Text = dTempMin.ToString("0.00");
                    }
                    break;
            }
        }

        private void Max_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string strTmp = btn.Tag.ToString();            

            bool bChk = double.TryParse(textBoxDeviceMaxTemp.Text.ToString(), out double dVal);
            if (bChk)
            {
                dTempMax = dVal;
            }
            else
            {
                MessageBox.Show("잘못 된 값이 입력되었습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            switch (strTmp)
            {
                case "0":
                    {
                        dTempMax = dTempMax + 0.5;
                        textBoxDeviceMaxTemp.Text = dTempMax.ToString("0.00");
                    }
                    break;

                case "1":
                    {
                        if (dTempMin < dTempMax)
                        {
                            dTempMax = dTempMax - 0.5;
                            textBoxDeviceMaxTemp.Text = dTempMax.ToString("0.00");
                        }
                        else
                        {
                            MessageBox.Show("Minimum 값 보다 작습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }                        
                    }
                    break;
            }
        }

        private void btnMinMaxTempSave_Click(object sender, EventArgs e)
        {
            string sDeviceName = textBoxDeviceInfo.Text.ToString();
            string sTempMin = textBoxDeviceMinTemp.Text.ToString().Trim();
            string sTempMax = textBoxDeviceMaxTemp.Text.ToString().Trim();
            bool bUseChk = bUsed;

            bool bChkMin = double.TryParse(sTempMin, out double dValMin);
            bool bChkMax = double.TryParse(sTempMax, out double dValMax);
            if ((bChkMin) && (bChkMax))
            {
                if (Parameter_WriteFile(sDeviceName, bUseChk, dValMin, dValMax, ModuleNo))
                {
                    ServerForm.Configure_List.Configure_TempMin[ModuleNo] = dValMin;
                    ServerForm.Configure_List.Configure_TempMax[ModuleNo] = dValMax;

                    MessageBox.Show("Temp tolerance 값이 저장 되었습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Temp tolerance 값이 저장 되지 않았습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("잘못 된 값이 입력되었습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private bool Parameter_WriteFile(string param1, bool param2, double param3, double param4, byte iModule)
        {
            try
            {
                string FileName = string.Format("FR{0}.txt", iModule + 1);            
                File.WriteAllText(m_ServerForm.ConfigurePath + FileName, param1 + "," + param2 + "," + param3 + "," + param4, Encoding.Default);

                return true;
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void rjToggleButtonDeviceUse_Click(object sender, EventArgs e)
        {
            if (rjToggleButtonDeviceUse.Checked)
                bUsed = true;
            else
                bUsed = false;
        }

        #region Temp value log file create
        private void _TEMP_LOG_SAVE(string sTempVal)
        {
            string sYear = string.Format("{0:yyyy}", DateTime.Now).Trim();
            string sMonth = string.Format("{0:MM}", DateTime.Now).Trim();
            string sDay = string.Format("{0:dd}", DateTime.Now).Trim();
            string sDate = string.Format("{0}-{1}-{2}", sYear, sMonth, sDay);
            string sTime = DateTime.Now.ToString("HH:mm:ss");
            string sDateTime;            
            sDateTime = string.Format("[{0}, {1}] ", sDate, sTime);

            WriteFile(sDateTime + sTempVal);
        }

        private void WriteFile(string Msg)
        {
            string sYear = string.Format("{0:yyyy}", DateTime.Now).Trim();
            string sMonth = string.Format("{0:MM}", DateTime.Now).Trim();
            string sDay = string.Format("{0:dd}", DateTime.Now).Trim();
            string FileName = string.Format("{0}.txt", sDay);     

            try
            {
                if (!Directory.Exists(string.Format("{0}FR{1}\\{2}", m_ServerForm.logFilePath, ModuleNo + 1, sYear)))
                {
                    CreateYearFolder(string.Format("{0}FR{1}", m_ServerForm.logFilePath, ModuleNo + 1));
                }

                if (!Directory.Exists(string.Format("{0}FR{1}\\{2}\\{3}", m_ServerForm.logFilePath, ModuleNo + 1, sYear, sMonth)))
                {
                    CreateMonthFolder(string.Format("{0}FR{1}", m_ServerForm.logFilePath, ModuleNo + 1));
                }

                if (File.Exists(string.Format("{0}FR{1}\\{2}\\{3}\\{4}", m_ServerForm.logFilePath, ModuleNo + 1, sYear, sMonth, FileName)))
                {
                    StreamWriter writer;
                    writer = File.AppendText(string.Format("{0}FR{1}\\{2}\\{3}\\{4}", m_ServerForm.logFilePath, ModuleNo + 1, sYear, sMonth, FileName));
                    writer.WriteLine(Msg);
                    writer.Close();
                }
                else
                {
                    CreateFile(string.Format("{0}FR{1}", m_ServerForm.logFilePath, ModuleNo + 1), Msg);

                    StreamWriter writer;
                    writer = File.AppendText(string.Format("{0}FR{1}\\{2}\\{3}\\{4}", m_ServerForm.logFilePath, ModuleNo + 1, sYear, sMonth, FileName));
                    writer.WriteLine(Msg);
                    writer.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private void CreateYearFolder(string Path)
        {
            string sYear = string.Format("{0:yyyy}", DateTime.Now).Trim();
            string FolderName = sYear;

            Directory.CreateDirectory(string.Format("{0}\\{1}", Path, FolderName));
        }

        private void CreateMonthFolder(string Path)
        {
            string sYear = string.Format("{0:yyyy}", DateTime.Now).Trim();
            string sMonth = string.Format("{0:MM}", DateTime.Now).Trim();
            string FolderName = sMonth;

            Directory.CreateDirectory(Path + "\\" + sYear + "\\" + FolderName);
        }

        private void CreateFile(string Path, string Msg)
        {
            string sYear = string.Format("{0:yyyy}", DateTime.Now).Trim();
            string sMonth = string.Format("{0:MM}", DateTime.Now).Trim();
            string sDay = string.Format("{0:dd}", DateTime.Now).Trim();
            string FileName = sDay + ".txt";

            if (!File.Exists(string.Format("{0}\\{1}\\{2}\\{3}", Path, sYear, sMonth, FileName)))
            {
                using (File.Create(string.Format("{0}\\{1}\\{2}\\{3}", Path, sYear, sMonth, FileName))) ;
            }
        }
        #endregion
    }
}
