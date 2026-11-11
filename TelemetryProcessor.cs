using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14.service
{
    public static class TelemetryProcessor
    {
        #region Статические элементы

        #region Метод 1
        /// <summary>
        ///  Принимает дрон по значению внутри прибавляет ветер к вектору скорости.
        /// </summary>
        /// <param name="drone">Дрон</param>
        /// <param name="wind">Ветер</param>
        public static void ApplyDrift(DroneState drone, Vector3D wind)
        {
            drone.Velocity.Add(wind);
        }
        #endregion

        #region Метод 2
        /// <summary>
        /// Метод принимает дрон по ссылке (ref) прибавляет силу ветра к вектору скорости дрона, модифицируя исходную структуру в стеке.
        /// </summary>
        /// <param name="drone">Дрон</param>
        /// <param name="wind">Ветер</param>
        public static void ApplyDriftCorrect(ref DroneState drone, Vector3D wind)
        {
            drone.Velocity.Add(wind);
        }
        #endregion

        #region Метод 3
        /// <summary>
        /// Уменьшает заряд батареи на 1% за каждые 5 секунд полета; Смещает широту и долготу пропорционально проекциям скорости; аписывает обновленный объект GeoCoordinate обратно в поле drone.Position.
        /// </summary>
        /// <param name="drone">Дрон</param>
        /// <param name="timeSeconds">Время</param>
        public static void SimulateFlightStep(ref DroneState drone, double timeSeconds)
        {
            drone.BatteryPercent = Math.Max(0, drone.BatteryPercent - (int)(timeSeconds / 5));

            double newLatitude = drone.Position.Latitude + (drone.Velocity.Y * timeSeconds) / 111000.0;
            double newLongitude = drone.Position.Longitude + (drone.Velocity.X * timeSeconds) / 111000.0;

            drone.Position = new GeoCoordinate(newLatitude, newLongitude);
        }
        #endregion

        #endregion
    }
}
