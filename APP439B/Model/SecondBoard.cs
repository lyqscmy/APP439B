using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace APP439B.Model
{
    class SecondBoard
    {
        Dictionary<string, byte[]> commands;

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

        private byte CheckSum(byte[] bytes)
        {
            byte cs = 0;
            foreach (byte b in bytes)
            {
                cs += b;
            }
            return cs;
        }

        public void HandShake()
        {
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;
                //string stringRecv;
                IPAddress ip = IPAddress.Parse("127.0.0.1");  //定义主机的IP及端口
                IPEndPoint ipEnd = new IPEndPoint(ip, 5566);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEnd);

                    input = commands["HandShake"];
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
                    data[3] = recv[5];  //data
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

                if (data[4] != CheckSum(data)) //cs
                { throw new Exception(); }
                else if (data[3] == 0xFF)
                { throw new Exception(); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void TestStart()
        {
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;
                //string stringRecv;
                IPAddress ip = IPAddress.Parse("127.0.0.1");  //定义主机的IP及端口
                IPEndPoint ipEnd = new IPEndPoint(ip, 5566);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEnd);

                    input = commands["Start"];
                    socket.Send(input, input.Length, SocketFlags.None);

                    len_recv = socket.Receive(recv);
                    data[0] = recv[2];  //add
                    data[1] = recv[3];  //c
                    data[2] = recv[4];  //l
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void TestStop()
        {
            try
            {
                byte[] input = new byte[1024];
                byte[] recv = new byte[1024];
                byte[] data = new byte[1024];
                int len_recv;
                //string stringRecv;
                IPAddress ip = IPAddress.Parse("127.0.0.1");  //定义主机的IP及端口
                IPEndPoint ipEnd = new IPEndPoint(ip, 5566);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
