using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using APP439B.ViewModel;
using APP439B.Properties;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Resources;
using System.Windows;
using System.Globalization;
using APP439B.Model;
using System.Collections;

namespace APP439B
{

    // Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template. 
    // Replace the code in Program.cs with this code. 

    public class MainBoard
    {

        SerialPort serialPort;
        string response;
        Dictionary<string, byte[]> commands;

        public delegate void SerialPortDataReceiveEventArgs(object sender, SerialDataReceivedEventArgs e, byte[] bits);  //委托
        public event SerialPortDataReceiveEventArgs DataReceived; //接收数据事件
        public bool ReceiveEventFlag = true; // 接收事件是否有效

        private Dictionary<string, byte[]> CreatCommands()
        {
            byte[] acld_HandShake = new byte[4] { 0xA3, 0x91, 0x00, 0x00 };
            byte[] acld_Start = new byte[4] { 0xA3, 0x92, 0x01, 0x00 }; //数据区为0~10s之间的数值
            byte[] acld_Stop = new byte[4] { 0xA3, 0x94, 0x00, 0x00 };
            byte[] acld_Query = new byte[4] { 0xA3, 0x95, 0x00, 0x00 };  

            byte cs_HandShake = CheckSum(acld_HandShake);
            byte cs_Start = CheckSum(acld_Start);
            byte cs_Stop = CheckSum(acld_Stop);
            byte cs_Query = CheckSum(acld_Query);

            Dictionary<string, byte[]> commands = new Dictionary<string, byte[]>();  
            commands.Add("HandShake", new byte[] { 0x55, 0x55, 0xA3, 0x91, 0x00, cs_HandShake, 0xFF });
            commands.Add("Start", new byte[] { 0x55, 0x55, 0xA3, 0x92, 0x01, cs_Start, 0xFF });
            commands.Add("Stop", new byte[] { 0x55, 0x55, 0xA3, 0x94, 0x00, cs_Stop, 0xFF });
            commands.Add("Query", new byte[] { 0x55, 0x55, 0xA3, 0x95, 0x00, cs_Query, 0xFF });

            return commands;
        }

        # region Constructor

        public MainBoard()
        {
            serialPort = new SerialPort();
            bool flag = false;
            commands = CreatCommands();
        }


        # endregion // Constructor

        # region Properties
        public bool IsConnect
        {
            get { return serialPort.IsOpen; }
        }

        # endregion //Properties

        #region Public Methods

        public void Set(SettingViewModel settings)
        {
            if (serialPort.IsOpen)
                serialPort.Close();

            serialPort.PortName = settings.PortName;
            serialPort.BaudRate = settings.BaudRate;
            serialPort.DataBits = settings.DataBits;
            serialPort.Parity = settings.Parity;
            serialPort.StopBits = settings.StopBits;
            serialPort.RtsEnable = settings.RtsEnable;
            serialPort.ReadTimeout = settings.TimeOut;
            serialPort.WriteTimeout = settings.TimeOut;
        }
 
        public bool Connect()
        {
            bool flag = false;
            if (serialPort.IsOpen)
                serialPort.Close();
            try
            {
                serialPort.Open();
                // serialPort.DataReceived += new SerialDataReceivedEventHandler(Read);
                flag = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return flag;
        }

        public void Disconnect()
        {
            if(serialPort.IsOpen)
                serialPort.Close();           
        }

        # region Commands

        public string HandShake()
        {
            Write(commands["HandShake"]);
            byte[] data = new byte[1]{0x00};
            try
            {
                data[0] = ReadData()[0];
            }
            catch (Exception)
            {
                return "读取失败";
            }
            if (data[0] != 0)
            {
                ArrayList List = new ArrayList(4);
                if ((data[0] & 0x01) != 0x00) List.Add("1#多普勒测速仪异常 ");
                if ((data[0] & 0x02) != 0x00) List.Add("2#多普勒测速仪异常 ");
                if ((data[0] & 0x04) != 0x00) List.Add("3#多普勒测速仪异常 ");
                if ((data[0] & 0x08) != 0x00) List.Add("环境参数测试仪异常 ");
                foreach (string str in List)
                {
                    response = "";
                    response += str;
                    return response;
                }
            }
            return "设备正常";
        }

        public string TestStart()
        {
            Write(commands["Start"]);
            byte[] data = new byte[1] { 0x00 };
            try
            {
                data[0] = ReadData()[0];
            }
            catch (Exception)
            {
                return "读取失败";
            }
            response = "Null";
            return response;
        }

        public string TestStop()
        {
            Write(commands["Stop"]);
            byte[] data = new byte[1] { 0x00 };
            try
            {
                data[0] = ReadData()[0];
            }
            catch (Exception)
            {
                return "读取失败";
            }
            response = "Null";
            return response;
        }

        public List<EnvParameters> EnvQuery()
        {
            Write(commands["Query"]);
            byte[] data = new byte[4];
            List<EnvParameters> parameters = new List<EnvParameters>();
            try
            {
                data = ReadData();
                string windDirection = System.Text.Encoding.ASCII.GetString(new[] { data[1] });
                EnvParameters parameter = new EnvParameters((double)data[0],
                    (string)windDirection, (double)data[2], (double)data[3]);
                parameters.Add(parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                throw new Exception();
                
            }
            return parameters;
        }

        public List<DopplerParameters> DopplerQuery()
        {
            Write(commands["Query"]);
            byte[] data = new byte[2];
            List<DopplerParameters> parameters = new List<DopplerParameters>();
            try
            {
                data = ReadData();
                DopplerParameters parameter = new DopplerParameters((double)data[0],
                    (double)data[1]);
                parameters.Add(parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
            return parameters;
        }

        # endregion // Commands

        #endregion

        # region Private Helper

        private byte CheckSum(byte[] bytes)
        {
            byte cs = 0;
            foreach (byte b in bytes)
            {
                cs += b;
            }
            return cs;
        }

        private string Write(byte[] bytes)
        {
            try
            {
                serialPort.Write(bytes, 0, bytes.Length);
                return "Write success";
            }
            catch (TimeoutException) 
            {
                return "Write time out!";
            }

        }

        private byte[] ReadData()
        {
            while(serialPort.BytesToRead > 0)
            {
                if (serialPort.ReadByte() == 0x55 && serialPort.ReadByte() == 0x55)
                {
                    break;
                }
            }
            if (serialPort.BytesToRead > 0)
            {
                byte[] acl = new byte[3]; //acl=add+c+l
                serialPort.Read(acl, 0, 3);
                byte[] data = new byte[acl[2]];
                serialPort.Read(data, 0, acl[2]);
                byte cs = (byte)serialPort.ReadByte();

                byte[] acld = new byte[acl.Length + data.Length];
                Array.Copy(acl, acld, acl.Length);
                Array.Copy(data, 0, acld, acl.Length, data.Length);
                if (cs != CheckSum(acld))
                { throw new Exception(); }
                byte ff = (byte)serialPort.ReadByte();
                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        # endregion // Private Helper

    }    
}
