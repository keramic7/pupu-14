using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14.service
{
    public struct DroneState
    {
        #region Элементы структуры
        public int DroneId { get; } // неизменяемый номер борта
        public GeoCoordinate Position { get; set; } // текущая координата (вложенная структура)
        public Vector3D Velocity { get; set; } //вектор скорости (вложенная структура)
        public int BatteryPercent { get; set; } //процент заряда (0–100)
        public bool IsGrounded => Position.Latitude == 0 && Position.Longitude == 0; // свойство статуса
        #endregion

        #region Конструктор
        /// <summary>
        /// Принимает id, position, velocity и начальный заряд, выполняя полную инициализацию.
        /// </summary>
        /// <param name="droneId">Id</param>
        /// <param name="position">Позиция</param>
        /// <param name="velocity">Скорость</param>
        /// <param name="batteryPercent">Процент батареи</param>
        public DroneState(int droneId, GeoCoordinate position, Vector3D velocity, int batteryPercent)
        {
            DroneId = droneId;
            Position = position;
            Velocity = velocity;

            BatteryPercent = Math.Max(0, Math.Min(100, batteryPercent)); // Ограничиваем заряд диапазоном 0–100
        }
        #endregion

        #region Метод для корректного вывода в консоль
        public override string ToString()
        {
            return $"Drone #{DroneId} | Pos: [Широта: {Position.Latitude:F4}, Долгота: {Position.Longitude:F4}] | " +
                   $"Vel: [{Velocity.X:F1}, {Velocity.Y:F1}, {Velocity.Z:F1}] | Batt: {BatteryPercent}% | Grounded: {IsGrounded}";
        }
        #endregion
    }
}
