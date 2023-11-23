using MsSqlManagerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace FreezerTempMonitoringSystem
{
    public enum MODULE: byte
    {
        _FR1 = 0,
        _FR2,
        _FR3,
        _FR4,
        _FR5,
        _FR6,
        _FR7,
        _FR8,
        _FR9,
        _FR10,
    }    
    
    public partial class ServerForm : Form
    {
        public string ConfigurePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\Configure\"));
        public string logFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\Log\"));

        string hostEquipmentInfo = "K5EE_TempHumidityMonitoring";

        private Socket m_ServerSocket;
        private List<Socket> m_ClientSocket;        
        private byte[] szData;        

        public Device m_Device1Form;
        public Device m_Device2Form;
        public Device m_Device3Form;
        public Device m_Device4Form;
        public Device m_Device5Form;
        public Device m_Device6Form;
        public Device m_Device7Form;
        public Device m_Device8Form;
        public Device m_Device9Form;
        public Device m_Device10Form;

        public LogForm m_logForm;

        // Temp값 저장 할 변수
        public double[] iDeviceTemp = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };

        // CONFIGURE LIST //////////////////////////////////////
        public class Configure_List
        {
            public static double[] Configure_TempMin = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
            public static double[] Configure_TempMax = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
        }
        ////////////////////////////////////////////////////////

        private Timer displayTimer = new Timer();

        public ServerForm()
        {
            InitializeComponent();

            m_Device1Form = new Device(this, (byte)MODULE._FR1);
            m_Device1Form.Visible = false;
            Controls.Add(m_Device1Form);

            m_Device2Form = new Device(this, (byte)MODULE._FR2);
            m_Device2Form.Visible = false;
            Controls.Add(m_Device2Form);

            m_Device3Form = new Device(this, (byte)MODULE._FR3);
            m_Device3Form.Visible = false;
            Controls.Add(m_Device3Form);

            m_Device4Form = new Device(this, (byte)MODULE._FR4);
            m_Device4Form.Visible = false;
            Controls.Add(m_Device4Form);

            m_Device5Form = new Device(this, (byte)MODULE._FR5);
            m_Device5Form.Visible = false;
            Controls.Add(m_Device5Form);

            m_Device6Form = new Device(this, (byte)MODULE._FR6);
            m_Device6Form.Visible = false;
            Controls.Add(m_Device6Form);

            m_Device7Form = new Device(this, (byte)MODULE._FR7);
            m_Device7Form.Visible = false;
            Controls.Add(m_Device7Form);

            m_Device8Form = new Device(this, (byte)MODULE._FR8);
            m_Device8Form.Visible = false;
            Controls.Add(m_Device8Form);

            m_Device9Form = new Device(this, (byte)MODULE._FR9);
            m_Device9Form.Visible = false;
            Controls.Add(m_Device9Form);

            m_Device10Form = new Device(this, (byte)MODULE._FR10);
            m_Device10Form.Visible = false;
            Controls.Add(m_Device10Form);            

            _Init_Server();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            displayTimer.Interval = 500;
            displayTimer.Elapsed += new ElapsedEventHandler(_Display);
            displayTimer.Start();

            if (!m_Device1Form.Visible)
                m_Device1Form.Visible = true;

            if (!m_Device2Form.Visible)
                m_Device2Form.Visible = true;

            if (!m_Device3Form.Visible)
                m_Device3Form.Visible = true;

            if (!m_Device4Form.Visible)
                m_Device4Form.Visible = true;

            if (!m_Device5Form.Visible)
                m_Device5Form.Visible = true;

            if (!m_Device6Form.Visible)
                m_Device6Form.Visible = true;

            if (!m_Device7Form.Visible)
                m_Device7Form.Visible = true;

            if (!m_Device8Form.Visible)
                m_Device8Form.Visible = true;

            if (!m_Device9Form.Visible)
                m_Device9Form.Visible = true;

            if (!m_Device10Form.Visible)
                m_Device10Form.Visible = true;

            m_Device1Form.Top = 100;
            m_Device1Form.Left = 0;

            m_Device2Form.Top = 100;
            m_Device2Form.Left = 360;

            m_Device3Form.Top = 100;
            m_Device3Form.Left = 720;

            m_Device4Form.Top = 100;
            m_Device4Form.Left = 1080;

            m_Device5Form.Top = 100;
            m_Device5Form.Left = 1440;

            m_Device6Form.Top = 470;
            m_Device6Form.Left = 0;

            m_Device7Form.Top = 470;
            m_Device7Form.Left = 360;

            m_Device8Form.Top = 470;
            m_Device8Form.Left = 720;

            m_Device9Form.Top = 470;
            m_Device9Form.Left = 1080;

            m_Device10Form.Top = 470;
            m_Device10Form.Left = 1440;            
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Device1Form != null)
            {
                m_Device1Form.Dispose();
                m_Device2Form.Dispose();
                m_Device3Form.Dispose();
                m_Device4Form.Dispose();
                m_Device5Form.Dispose();
                m_Device6Form.Dispose();
                m_Device7Form.Dispose();
                m_Device8Form.Dispose();
                m_Device9Form.Dispose();
                m_Device10Form.Dispose();
            }            

            if (m_logForm != null)
                m_logForm.Dispose();

            Dispose();
            //Application.Exit();
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void ServerForm_Activated(object sender, EventArgs e)
        {
            SetDoubleBuffered(m_Device1Form);
            SetDoubleBuffered(m_Device2Form);
            SetDoubleBuffered(m_Device3Form);
            SetDoubleBuffered(m_Device4Form);
            SetDoubleBuffered(m_Device5Form);
            SetDoubleBuffered(m_Device6Form);
            SetDoubleBuffered(m_Device7Form);
            SetDoubleBuffered(m_Device8Form);
            SetDoubleBuffered(m_Device9Form);
            SetDoubleBuffered(m_Device10Form);

            SetDoubleBuffered(richTextBoxServerStatus);
            SetDoubleBuffered(richTextBoxRecvMsg);
        }

        private void SetDoubleBuffered(Control control, bool doubleBuffered = true)
        {
            PropertyInfo propertyInfo = typeof(Control).GetProperty
            (
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            propertyInfo.SetValue(control, doubleBuffered, null);
        }

        private void _Init_Server()
        {
            m_ClientSocket = new List<Socket>();            
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            // Server IP 및 Port
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8000);

            // Server binding
            m_ServerSocket.Bind(iPEndPoint);
            m_ServerSocket.Listen(10);

            // Socket event
            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
            // Event 발생 시, Accept_Completed 함수 실행
            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(_Accept_Completed);

            // Waiting for client connection
            m_ServerSocket.AcceptAsync(socketAsyncEventArgs);



            // SQL server connect
            string strRtn = HostConnection.Connect();
            if (strRtn == "OK")
            {
                DisplayText_ServerStatus("EE 서버 접속에 성공했습니다");
            }
            else
            {
                DisplayText_ServerStatus("EE 서버 접속에 실패했습니다");
            }
        }

        /*
         * Client connection acceptance callback function
         */
        private void _Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket clientSocket = e.AcceptSocket;            

            // 요청 Socket을 수락 후 리스트에 추가
            m_ClientSocket.Add(clientSocket);
            
            if(m_ClientSocket != null)
            {
                DisplayText_ServerStatus("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + " is connected");

                SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();                

                // 수신용 buffer 할당
                szData = new byte[1024];                
                socketAsyncEventArgs.SetBuffer(szData, 0, 1024);
                socketAsyncEventArgs.UserToken = m_ClientSocket;
                socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(_Receive_Completed);

                // 수락 된 Socket의 데이터 수신 대기
                clientSocket.ReceiveAsync(socketAsyncEventArgs);
            }

            e.AcceptSocket = null;
            // 요청 Socket 처리 후 다시 수락 대기
            m_ServerSocket.AcceptAsync(e);
        }

        /*
         * Data receive callback function
         */
        private void _Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket clientSocket = (Socket)sender;
            
            // 해당 Socket의 접속 유무 확인 후 false면 Socket을 닫음
            if (clientSocket.Connected && e.BytesTransferred > 0)
            {
                // Data receive                
                byte[] szData = e.Buffer;
                string strData = Encoding.Unicode.GetString(szData);                
                string[] arrayStr = strData.Split(';');
                string sParsingData = arrayStr[0];
                string recvMsg = sParsingData.Replace("\0", "").Trim();
                DisplayText("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + recvMsg);

                _TEMP_PARSING(recvMsg);
                
                for (int i = 0; i < szData.Length; i++)
                {
                    szData[i] = 0;
                }

                for (int i = 0; i < arrayStr.Length; i++)
                {
                    arrayStr[i] = string.Empty;
                }

                e.SetBuffer(szData, 0, 1024);
                clientSocket.ReceiveAsync(e);
            }
            else
            {
                // Socket 재사용 유무
                clientSocket.Disconnect(false);

                // Client socket 리스트에서 해당 Socket 삭제
                m_ClientSocket.Remove(clientSocket);
                DisplayText_ServerStatus("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + " is disconnected");
            }
        }        

        private void DisplayText_ServerStatus(string text)
        {            
            if (richTextBoxServerStatus.InvokeRequired)
            {                
                richTextBoxServerStatus.BeginInvoke(new MethodInvoker(delegate
                {
                    if (richTextBoxServerStatus.Lines.Length > 100)
                    {
                        richTextBoxServerStatus.Clear();
                    }

                    richTextBoxServerStatus.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                    richTextBoxServerStatus.ScrollToCaret();
                }));
            }
            else
            {
                richTextBoxServerStatus.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                richTextBoxServerStatus.ScrollToCaret();
            }
        }

        private void DisplayText(string text) 
        {            
            if (richTextBoxRecvMsg.InvokeRequired) 
            {                
                richTextBoxRecvMsg.BeginInvoke(new MethodInvoker(delegate 
                {
                    if (richTextBoxRecvMsg.Lines.Length > 100)
                    {
                        richTextBoxRecvMsg.Clear();
                    }

                    richTextBoxRecvMsg.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                    richTextBoxRecvMsg.ScrollToCaret();
                })); 
            } 
            else
            {
                richTextBoxRecvMsg.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                richTextBoxRecvMsg.ScrollToCaret();
            }                
        }

        private void _TEMP_PARSING(string strMsg)
        {
            try
            {                
                string[] sWords = strMsg.Split(',');
                
                if (sWords[0].Equals("FR-01", StringComparison.InvariantCultureIgnoreCase))                
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR1] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "1", "Temp1", iDeviceTemp[(byte)MODULE._FR1].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-02", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR2] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "2", "Temp1", iDeviceTemp[(byte)MODULE._FR2].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-03", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR3] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "3", "Temp1", iDeviceTemp[(byte)MODULE._FR3].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-04", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR4] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "4", "Temp1", iDeviceTemp[(byte)MODULE._FR4].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-05", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR5] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "5", "Temp1", iDeviceTemp[(byte)MODULE._FR5].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-06", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR6] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "6", "Temp1", iDeviceTemp[(byte)MODULE._FR6].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-07", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR7] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "7", "Temp1", iDeviceTemp[(byte)MODULE._FR7].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-08", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR8] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "8", "Temp1", iDeviceTemp[(byte)MODULE._FR8].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-09", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR9] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "9", "Temp1", iDeviceTemp[(byte)MODULE._FR9].ToString("0.00"));
                    }
                }
                else if (sWords[0].Equals("FR-10", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool bChk = double.TryParse(sWords[1], out double dVal);
                    if (bChk)
                    {
                        iDeviceTemp[(byte)MODULE._FR10] = dVal;

                        // SQL server upload
                        HostConnection.Host_Set_TempHumi(hostEquipmentInfo, "10", "Temp1", iDeviceTemp[(byte)MODULE._FR10].ToString("0.00"));
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
        }

        private void _Display(object sender, ElapsedEventArgs e)
        {
            if (laDate.InvokeRequired)
            {
                laDate.BeginInvoke(new MethodInvoker(delegate
                {
                    laDate.Text = DateTime.Today.ToShortDateString();
                    laTime.Text = DateTime.Now.ToLongTimeString();
                }));
            }
            
            m_Device1Form._Display();
            m_Device2Form._Display();
            m_Device3Form._Display();
            m_Device4Form._Display();
            m_Device5Form._Display();
            m_Device6Form._Display();
            m_Device7Form._Display();
            m_Device8Form._Display();
            m_Device9Form._Display();
            m_Device10Form._Display();
        }

        private void btnLogView_Click(object sender, EventArgs e)
        {
            m_logForm = new LogForm();
            m_logForm.Show();
        }
    }
}
