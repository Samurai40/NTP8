using System;

namespace Model
{
    /// <summary>
    /// Класс Равномерное движение
    /// </summary>
    [Serializable]
    public class UniformMove : Movement
    {
        /// <summary>
        /// Вывод информации об объекте
        /// </summary>
        public override void Info()
        {
            Console.WriteLine("Расчет для равномерного движения:");
            Console.WriteLine("Начальная точка: " + StartPoint);
            Console.WriteLine("Начальная скорость: " + Speed);
            Console.WriteLine("Время движения: " + Period);
            Console.WriteLine("Конечная точка: " + FinalPoint());
        }

        /// <summary>
        /// Конечная точка
        /// </summary>
        /// <returns>Конечная точка</returns>
        public override double FinalPoint()
        {
            _finalPoint = StartPoint + Speed * Period;
            return Math.Round(_finalPoint,1);
        }
    }
}