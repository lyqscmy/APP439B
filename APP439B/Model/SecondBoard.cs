using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace APP439B.Model
{
    public class SecondBoard
    {
        private Dictionary<string, byte[]> commands;
        static string ip_address = "192.168.0.101";
        static int port = 8080;

        static IPAddress ip = IPAddress.Parse(ip_address);  //定义主机的IP及端口
        IPEndPoint ipEnd = new IPEndPoint(ip, port);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void Connect()
        {
            try
            {
                socket.Connect(ipEnd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void Disconnect()
        {
            try
            {
                // Release the socket.
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        private Dictionary<string, byte[]> CreatCommands()
        {
            byte[] acld_HandShake = new byte[4] { 0xA2, 0x91, 0x00, 0x00 };
            byte[] acld_Start = new byte[4] { 0xA2, 0x92, 0x01, 0x00 }; //数据区为0~10s之间的数值
            byte[] acld_Stop = new byte[4] { 0xA2, 0x94, 0x00, 0x00 };

            byte cs_HandShake = CheckSum(acld_HandShake);
            byte cs_Start = CheckSum(acld_Start);
            byte cs_Stop = CheckSum(acld_Stop);

            Dictionary<string, byte[]> commands = new Dictionary<string, byte[]>();
            commands.Add("HandShake", new byte[] { 0x55, 0x55, 0xA2, 0x91, 0x00, cs_HandShake, 0xFF });
            commands.Add("Start", new byte[] { 0x55, 0x55, 0xA2, 0x92, 0x01, cs_Start, 0xFF });
            commands.Add("Stop", new byte[] { 0x55, 0x55, 0xA2, 0x94, 0x00, cs_Stop, 0xFF });

            return commands;
        }

        # region Constructor

        public SecondBoard()
        {
            commands = CreatCommands();
        }

        # endregion // Constructor

        private byte CheckSum(byte[] bytes)
        {
            byte cs = 0;
            foreach (byte b in bytes)
            {
                cs += b;
            }
            return cs;
        }
        
        public string HandShake()
        {
            string response = "设备正常";
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;
                //string stringRecv;

                try
                {
                    Array.Copy(commands["HandShake"], input, commands["HandShake"].Length);
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    data[3] = recv[5];  //data
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(recv, 0, len_recv));
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    response = "读取异常";
                    return response;
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    response = "读取异常";
                    return response;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    response = "读取异常";
                    return response;
                }

                if (data[4] != CheckSum(data)) //cs
                { throw new Exception(); }
                else if (data[3] != 0x00)
                {
                    ArrayList List = new ArrayList(4);
                    if ((data[3] & 0x01) != 0x00) List.Add("1#多普勒测速仪异常 ");
                    if ((data[3] & 0x02) != 0x00) List.Add("2#多普勒测速仪异常 ");
                    if ((data[3] & 0x04) != 0x00) List.Add("3#多普勒测速仪异常 ");
                    if ((data[3] & 0x08) != 0x00) List.Add("环境参数测试仪异常 ");
                    foreach(string str in List)
                    {
                        response = "";
                        response += str;
                        return response;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                response = "读取异常";
                return response;
            }
            return response;
        }

        public bool TestStart()
        {
            try
            {
                byte[] input = new byte[8];
                byte[] recv = new byte[8];
                byte[] data = new byte[8];
                int len_recv;
                //string stringRecv;

                try
                {
                    Array.Copy(commands["Start"], input, commands["Start"].Length);
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(recv, 0, len_recv));

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    return false;
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return false;
                }
                if (data[3] != CheckSum(data)) //cs
                { throw new Exception(); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public bool TestStop()
        {
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;
                //string stringRecv;

                try
                {
                    Array.Copy(commands["Stop"], input, commands["Stop"].Length);
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(recv, 0, len_recv));

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    return false;
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return false;
                }
                if (data[3] != CheckSum(data)) //cs
                { throw new Exception(); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public List<EnvParameters> EnvQuery()
        {
            List<EnvParameters> parameters = new List<EnvParameters>();
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;

                //string stringRecv;
                IPAddress ip = IPAddress.Parse("127.0.0.1");  //定义主机的IP及端口
                IPEndPoint ipEnd = new IPEndPoint(ip, port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEnd);

                    input = commands["Stop"];
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    data[3] = recv[5];  //data
                    data[4] = recv[5];  //data
                    data[5] = recv[5];  //data
                    data[6] = recv[5];  //data
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(recv, 0, len_recv));

                    // Release the socket.
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
                if (data[3] != CheckSum(data)) //cs
                { throw new Exception(); }

                string windDirection = System.Text.Encoding.ASCII.GetString(new[] { data[4] });
                EnvParameters parameter = new EnvParameters((double)data[3],
                    (string)windDirection, (double)data[5], (double)data[6]);
                parameters.Add(parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return parameters;
        }

        public List<DopplerParameters> DopplerQuery()
        {
            List<DopplerParameters> parameters = new List<DopplerParameters>();
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;

                //string stringRecv;
                IPAddress ip = IPAddress.Parse(ip_address);  //定义主机的IP及端口
                IPEndPoint ipEnd = new IPEndPoint(ip, port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEnd);

                    input = commands["Stop"];
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    data[3] = recv[5];  //data
                    data[4] = recv[5];  //data
                    data[5] = recv[5];  //data
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(recv, 0, len_recv));

                    // Release the socket.
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
                if (data[3] != CheckSum(data)) //cs
                { throw new Exception(); }

                DopplerParameters parameter = new DopplerParameters((double)data[3],
                    (double)data[4]);
                parameters.Add(parameter);
            }
            catch (Exception e)      
            {
                Console.WriteLine(e.ToString());
            }
            return parameters;
        }
    }
}
