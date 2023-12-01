using System;

namespace Model
{
    /// <summary>
    /// Класс Равноускоренное движение
    /// </summary>
    [Serializable]
    public class UniformAccek : Movement
    {
        /// <summary>
        /// Ускорение
        /// </summary>
        private double _acceleration = 1.0;

        /// <summary>
        /// Вывод информации об объекте
        /// </summary>
        public override void Info()
        {
            Console.WriteLine("Расчет для равноускоренного движения:");
            Console.WriteLine("Начальная точка: " + StartPoint);
            Console.WriteLine("Начальная скорость: " + Speed);
            Console.WriteLine("Время движения: " + Period);
            Console.WriteLine("Ускорение движения: " + Acceleration);
            Console.WriteLine("Конечная точка: " + FinalPoint());
        }

        /// <summary>
        /// Ускорение
        /// </summary>
        public double Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                if (value == 0.0)
                {
                    throw new ArgumentException(
                        "Ускорение не может быть равным нулю!",
                        "Acceleration");
                }
                _acceleration = value;
            }
        }

        /// <summary>
        /// Конечная точка
        /// </summary>
        /// <returns>Конечная точка</returns>
        public override double FinalPoint()
        {

            _finalPoint = StartPoint + Speed * Period +
                          (Acceleration * Math.Pow(Period, 2) / 2);
            return Math.Round(_finalPoint,1);
        }
    }
}