using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP439B.Model
{
    public class EnvParameters
    {
        private double windSpeed;
        public double WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }

        private string windDirection;
        public string WindDirection
        {
            get { return windDirection; }
            set { windDirection = value; }
        }

        private double temperature;
        public double temperatureValue
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private double humidity;
        public double humidityValue
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public EnvParameters(double windSpeed, string windDirection, double temperature, double humidity)
        {
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            temperatureValue = temperature;
            humidityValue = humidity;
        }
    }
}
