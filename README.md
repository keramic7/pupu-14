# Практическая работа №14: Структуры данных (struct) в C#, размерные типы и оптимизация памяти

Шаг 1. Неизменяемая структура GeoCoordinate

<img width="1919" height="1007" alt="image" src="https://github.com/user-attachments/assets/81db9eb1-c9f7-4117-a4df-61ce40d5a87a" />

~~~C#
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
~~~
Шаг 2. Изменяемая структура Vector3D

<img width="1919" height="1003" alt="image" src="https://github.com/user-attachments/assets/0518bc24-b86a-43db-85c2-b3fea797e36e" />

~~~C#
<img width="1919" height="1004" alt="image" src="https://github.com/user-attachments/assets/3f700e1d-3e03-47bc-87b2-1ddbc405fd1e" />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14.service
{
    public struct Vector3D
    {
        #region Открытые поля или автосвойства
        public double X;
        public double Y;
        public double Z;
        #endregion

        #region Конструктор
        /// <summary>
        /// Инициализирует все 3 оси
        /// </summary>
        /// <param name="x">Ось X</param>
        /// <param name="y">Ось Y</param>
        /// <param name="z">Ось Z</param>
        public Vector3D(double x, double y, double z)
        {  X = x;
           Y = y;
           Z = z; 
        }
        #endregion

        #region Вычисляемое свойство
        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);
        #endregion

        #region Метод экземпляра 1
        /// <summary>
        /// Прибавляет координаты другого вектора к текущему объекту.
        /// </summary>
        /// <param name="other">Другой вектор</param>
        public void Add(Vector3D other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }
        #endregion

        #region Метод экземпляра 2
        /// <summary>
        /// Умножает каждую ось на коэффициент.
        /// </summary>
        /// <param name="factor">Коэффициент</param>
        public void Scale(double factor)
        {  X *= factor;
           Y *= factor;
           Z *= factor;
        }
        #endregion

        #region Переопределенный метод
        /// <summary>
        /// Выводит $"[X: {X:F1}, Y: {Y:F1}, Z: {Z:F1}]".
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[X: {X:F1}, Y: {Y:F1}, Z: {Z:F1}]";
        }
        #endregion
    }
}
~~~
Шаг 3. Составная структура DroneState

<img width="1919" height="1003" alt="image" src="https://github.com/user-attachments/assets/b752f11e-f72b-4fa5-923e-c5de4eea50f2" />

~~~C#
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
    }
}
~~~
Шаг 4. Расчетный класс TelemetryProcessor

<img width="1919" height="1004" alt="image" src="https://github.com/user-attachments/assets/d155aecf-d601-448c-b19c-a6232863d9f3" />

~~~C#
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
~~~

