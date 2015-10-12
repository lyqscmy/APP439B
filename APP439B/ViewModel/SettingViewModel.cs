using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Input;
using APP439B.Properties;

namespace APP439B.ViewModel
{
    public class SettingViewModel
    {
        # region fields

        private string portName;
        int baudRate;
        int dataBits;
        Parity parity;
        StopBits stopBits;
        bool rtsEnable;
        int timeOut;

        string[] portNameOptions;
        int[] baudRateOptions;
        int[] dataBitsOptions;
        Parity[] parityOptions;
        StopBits[] stopBitsOptions;
        bool[] rtsEnableOptions;
        int[] timeOutOptions;

        # endregion // fields

        # region Constructor

        public SettingViewModel()
        {
            portName=Settings.Default.PortName;
            baudRate=Settings.Default.BaudRate;
            dataBits=Settings.Default.DataBits;
            parity=Settings.Default.Parity;
            stopBits=Settings.Default.StopBits;
            rtsEnable=Settings.Default.RtsEnable;
            timeOut = Settings.Default.TimeOut;
        }

        # endregion // Constructor

        # region Properties

        public string PortName 
        {
            get { return portName; }
            set
            {
                portName = value;
            }
        }
        
        public int BaudRate
        {
            get { return baudRate; }
            set
            {
                baudRate = value;
            }
        }

        public int DataBits
        {
            get { return dataBits; }
            set
            {

                dataBits = value;

            }
        }

        public Parity Parity
        {
            get { return parity; }
            set
            {
                parity = value;
            }
        }

        public StopBits StopBits
        {
            get { return stopBits; }
            set
            {
                stopBits = value;

            }
        }

        public bool RtsEnable
        {
            get { return rtsEnable; }
            set
            {
                rtsEnable = value;
            }
        }

        public int TimeOut
        {
            get { return timeOut; }
            set
            {
                timeOut = value;
            }
        }

        public string[] PortNameOptions
        {
            get
            {
                if (portNameOptions == null)
                {
                    portNameOptions = SerialPort.GetPortNames();
                   
                }
                return portNameOptions;
            }
        }

        public int[] DataBitsOptions
        {
            get
            {
                if (dataBitsOptions == null)
                {
                   dataBitsOptions = new int[]
                    {
                        5,6,7,8
                    };

                }
                return dataBitsOptions;
            }
        }

        public int[] BaudRateOptions
        {
            get
            {
                if (baudRateOptions == null)
                {
                    baudRateOptions = new int[]
                    {
                        9600,19200,38400,115200
                    };

                }
                return baudRateOptions;
            }
        }

        public Parity[] ParityOptions
        {
            get
            {
                if (parityOptions == null)
                {
                    parityOptions = new Parity[]
                    {
                        Parity.None,Parity.Odd,Parity.Even,Parity.Mark,Parity.Space
                    };

                }
                return parityOptions;
            }
        }

        public StopBits[] StopBitsOptions
        {
            get
            {
                if (stopBitsOptions == null)
                {
                    stopBitsOptions = new StopBits[]
                    {
                        StopBits.One,StopBits.OnePointFive,StopBits.Two
                    };

                }
                return stopBitsOptions;
            }
        }

        public bool[] RtsEnableOptions
        {
            get
            {
                if (rtsEnableOptions == null)
                {
                    rtsEnableOptions = new bool[]
                    {
                        false,true
                    };

                }
                return rtsEnableOptions;
            }
        }

        public int[] TimeOutOptions
        {
            get
            {
                if (timeOutOptions == null)
                {
                    timeOutOptions = new int[]
                    {
                        100,200,300,400
                    };

                }
                return timeOutOptions;
            }
        }
        # endregion //Properties
    }
}