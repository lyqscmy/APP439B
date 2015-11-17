using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP439B.Model
{
    public class DeviceState
    {
        private bool envirnoment;
        public bool Envirnoment
        {
            get { return envirnoment; }
            set { envirnoment = value; }
        }

        private bool motor;
        public bool Motor
        {
            get { return motor; }
            set { motor = value; }
        }
        
        private bool video;
        public bool Video
        {
            get { return video; }
            set { video = value; }
        }

        private bool cesu;
        public bool Cesu
        {
            get { return cesu; }
            set { cesu = value; }
        }

        private bool jilu;
        public bool Jilu
        {
            get { return jilu; }
            set { jilu = value; }
        }

        public DeviceState()
        {
            Envirnoment = false;
            Motor = false;
            Video = false;
            Cesu = false;
            Jilu = false;
        }
    }
}
