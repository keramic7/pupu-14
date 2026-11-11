using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    public readonly struct GeoCoordinate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            if (Latitude < -90) Latitude = -90;
            else if (Latitude > 90) Latitude = 90;

            if (Longitude < -180) Longitude = -180;
            else if (Longitude > 180) Longitude = 180;
        }

        public double DistanceTo(GeoCoordinate other)
        {
            return Math.Sqrt(Math.Pow(Latitude - other.Latitude, 2) + Math.Pow(Longitude - other.Longitude, 2)) * 111.0;
        }

        public override string ToString()
        {
            return $"({Latitude:F4}°, {Longitude:F4}°)";
        }
    }
}
    
    

