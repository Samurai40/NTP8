using System;

namespace Model
{
    /// <summary>
    /// Интерфейс Движение
    /// </summary>
    [Serializable]
    public abstract class Movement
    {
        /// <summary>
        /// Конечная точка
        /// </summary>
        protected double _finalPoint = 0.0;

        /// <summary>
        /// Начальная скорость движения
        /// </summary>
        private double _speed = 1.0;

        /// <summary>
        /// Время движения
        /// </summary>
        private double _time = 1.0;

        /// <summary>
        /// Информации об объекте
        /// </summary>
        public abstract void Info();

        /// <summary>
        /// Начальная точка
        /// </summary>
        public double StartPoint { get; set; }

        /// <summary>
        /// Начальная скорость движения
        /// </summary>
        public double Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                if (value == 0.0)
                {
                    throw new ArgumentException(
                        "Скорость не может быть нулевой!",
                        "Speed");
                }
                _speed = value;
            }
        }

        /// <summary>
        /// Время движения
        /// </summary>
        public double Period
        {
            get
            {
                return _time;
            }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(
                        "Время должно быть больше 0!",
                        "Period");
                }
                _time = value;
            }
        }

        /// <summary>
        /// Конечная точка
        /// </summary>
        /// <returns>Конечная точка</returns>
        public abstract double FinalPoint();
    }
}
