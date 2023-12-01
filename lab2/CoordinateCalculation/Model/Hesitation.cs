using System;

namespace Model
{
    /// <summary>
    /// Класс Колебательное движение
    /// </summary>
    [Serializable]
    public class Hesitation : Movement
    {
        /// <summary>
        /// Амплитуда движения
        /// </summary>
        private double _amplitude = 0.0;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Hesitation()
        { }

        /// <summary>
        /// Вывод информации об объекте
        /// </summary>
        public override void Info()
        {
            Console.WriteLine("Расчет для колебательного движения");
            Console.WriteLine("Начальная точка: " + StartPoint);
            Console.WriteLine("Начальная скорость: " + Speed);
            Console.WriteLine("Время движения: " + Period);
            Console.WriteLine("Амплитуда движения: " + Amplitude);
            Console.WriteLine("Конечная точка: " + FinalPoint());
        }

        /// <summary>
        /// Амплитуда движения
        /// </summary>
        public double Amplitude
        {
            get
            {
                return _amplitude;
            }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(
                        "Амплитуда должна быть больше нуля!",
                        "Amplitude");
                }
                _amplitude = value;
            }
        }

        /// <summary>
        /// Конечная точка
        /// </summary>
        /// <returns>Конечная точка</returns>
        public override double FinalPoint()
        {
            _finalPoint = _amplitude * Math.Cos(Speed * Period + StartPoint);
            return Math.Round(_finalPoint,1);
        }
    }
}
