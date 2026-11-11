using ConsoleApp14.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //#region Эксперимент 1 (Доказательство независимости копий)
            //var v1 = new Vector3D(10, 20, 30);   // Создаём первую структуру (10, 20, 30)
            //var v2 = v1;                         // Копируем v1 в v2

            //v2.X = 999;                          // Меняем поле X у копии v2

            //Console.WriteLine($"v1: {v1}");      // Вывод[X: 10.0, Y: 20.0, Z: 30.0]
            //Console.WriteLine($"v2: {v2}");      // Вывод[X: 999.0, Y: 20.0, Z: 30.0]
            //#endregion

            //#region Эксперимент 2 (Передача по значению vs ref)
            //var drone = new DroneState(101, new GeoCoordinate(55.7558, 37.6173), new Vector3D(15, 0, 5), 100);

            //Console.WriteLine("Исходное состояние:");
            //Console.WriteLine(drone); //[15.0, 0.0, 5.0]

            ////Ошибочный вариант - передача по значению (копия)
            //TelemetryProcessor.ApplyDrift(drone, new Vector3D(5, 5, 0));

            //Console.WriteLine("\nПосле ApplyDrift (без ref):");
            //Console.WriteLine(drone); //скорость НЕ изменилась, метод модифицировал копию структуры

            ////Правильный вариант - передача по ссылке  (ref)
            //TelemetryProcessor.ApplyDriftCorrect(ref drone, new Vector3D(5, 5, 0));

            //Console.WriteLine("\nПосле ApplyDriftCorrect (ref):");
            //Console.WriteLine(drone); // скорость обновилась, метод изменил исходную структуру
            //#endregion

            #region Эксперимент 3 (Массив структур)

            DroneState[] swarm = new DroneState[3];

            //Инициализируем 3 дрона с разными координатами и векторами скорости
            swarm[0] = new DroneState(201, new GeoCoordinate(55.7558, 37.6173), new Vector3D(10, 5, 0), 90);
            swarm[1] = new DroneState(202, new GeoCoordinate(59.9386, 30.3141), new Vector3D(-8, 12, 0), 75);
            swarm[2] = new DroneState(203, new GeoCoordinate(56.8389, 60.6057), new Vector3D(3, -6, 0), 60);

            //3 шага полёта по 10 секунд для каждого дрона
            for (int step = 0; step < 3; step++)
            {
                for (int i = 0; i < swarm.Length; i++)
                {
                    TelemetryProcessor.SimulateFlightStep(ref swarm[i], 10.0);
                }
            }

            //Итоговая телеметрия всех дронов
            Console.WriteLine("Итог полёта:");
            for (int i = 0; i < swarm.Length; i++)
            {
                Console.WriteLine(swarm[i]);
            }
            #endregion

            #region
            var drone = new DroneState(101, new GeoCoordinate(55.7558, 37.6173), new Vector3D(15, 0, 5), 100);

            //Упаковка (boxing): структура копируется в объект на куче (heap).
            //boxedDrone — это НЕ та же память, что drone, а снимок её состояния на момент упаковки.
            object boxedDrone = drone;

            //Неправильный вариант:
            //((DroneState)boxedDrone).BatteryPercent = 50; - При распаковке создаётся ВРЕМЕННАЯ копия структуры, 50 пишется в неё,копия сразу выбрасывается — boxedDrone и drone остаются без изменений.

            //Правильный вариант:
            DroneState unboxed = (DroneState)boxedDrone; //распаковка
            unboxed.BatteryPercent = 50;                 //меняем свою локальную копию
            boxedDrone = unboxed;                        //упаковываем обратно

            Console.WriteLine($"Оригинал drone:  {drone}");
            Console.WriteLine($"Упакованный:     {boxedDrone}");
            #endregion 
        }
    }
}
