using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP439B.Model
{
    public class DopplerParameters
    {
        private double speed;
        public double speedValue
        {
            get { return speed; }
            set { speed = value; }
        }

        private double time;
        public double timeValue
        {
            get { return time; }
            set { time = value; }
        }

        public DopplerParameters(double speed, double time)
        {
            speedValue = speed;
            timeValue = time;
        }
    }
}
