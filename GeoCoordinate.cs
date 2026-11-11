using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    public readonly struct GeoCoordinate
    {
        #region Свойства только для чтения
        public double Latitude { get; }
        public double Longitude { get; }
        #endregion

        #region Конструктор
        /// <summary>
        /// Добавляет валидацию: широта от -90 до 90, долгота от -180 до 180 (при выходе за границы устанавливать крайнее значение);
        /// </summary>
        /// <param name="latitude">Широта</param>
        /// <param name="longitude">Долгота</param>
        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            if (Latitude < -90) Latitude = -90;
            else if (Latitude > 90) Latitude = 90;

            if (Longitude < -180) Longitude = -180;
            else if (Longitude > 180) Longitude = 180;
        }
        #endregion

        #region Метод 
        /// <summary>
        /// Рассчитывает приближенное декартово расстояние до другой точки по формуле:
        /// </summary>
        /// <param name="other">Другая точка</param>
        /// <returns></returns>
        public double DistanceTo(GeoCoordinate other)
        {
            return Math.Sqrt(Math.Pow(Latitude - other.Latitude, 2) + Math.Pow(Longitude - other.Longitude, 2)) * 111.0;
        }
        #endregion

        #region Переопределенный метод
        /// <summary>
        /// Возвращает строку вида $"({Latitude:F4}°, {Longitude:F4}°)".
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({Latitude:F4}°, {Longitude:F4}°)";
        }
        #endregion
    }
}
