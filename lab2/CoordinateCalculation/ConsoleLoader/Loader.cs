using System;
using Model;

namespace ConsoleLoader
{
    class Loader
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Равномерное движение:");
            Movement move = new UniformMove();
            DataInput(ref move, 0);
            DataInput(ref move, 1);
            DataInput(ref move, 2);
            move.Info();

            Console.WriteLine("Равноускоренное движение:");
            move = new UniformAccek();
            DataInput(ref move, 0);
            DataInput(ref move, 1);
            DataInput(ref move, 2);
            DataInput(ref move, 3);
            move.Info();

            Console.WriteLine("Колебательное движение:");
            move = new Hesitation();
            DataInput(ref move, 0);
            DataInput(ref move, 1);
            DataInput(ref move, 2);
            DataInput(ref move, 4);
            move.Info();

            Console.ReadKey();
        }

        /// <summary>
        /// Ввод данных класса Movement 
        /// </summary>
        /// <param name="movement">Ссылка на абстрактный класс Movement</param>
        /// <param name="param">Вводимый параметр(
        /// 0-начальная точка,
        /// 1-начальная скорость,
        /// 2-время движения,
        /// 3-ускорение(для равноускоренного движения),
        /// 4-амплитуда движения(для колебательного движения))</param>
        public static void DataInput(ref Movement movement, int param)
        {
            bool isCorrectInput = false;
            while (!isCorrectInput)
            {
                try
                {
                    switch (param)
                    {
                        case 0:
                            movement.StartPoint = InputDouble(
                                "Введите начальную точку движения: ");
                            break;
                        case 1:
                            movement.Speed = InputDouble(
                                "Введите начальную скорость движения: ");
                            break;
                        case 2:
                            movement.Period = InputDouble(
                                "Введите время движения: ");
                            break;
                        case 3:
                            ((UniformAccek)movement).Acceleration = InputDouble(
                                "Введите ускорение движения: ");
                            break;
                        case 4:
                            ((Hesitation)movement).Amplitude = InputDouble(
                                "Введите амплитуду движения: ");
                            break;
                    }

                    isCorrectInput = true;
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message + "\n");
                    isCorrectInput = false;
                }
            }
        }

        /// <summary>
        /// Ввод действительного числа
        /// </summary>
        /// <param name="message">Сообщение о вводимом параметре</param>
        /// <returns>Действительное число</returns>
        private static double InputDouble(String message)
        {
            bool isCorrectInput = false;
            double result = 0.0;

            while (!isCorrectInput)
            {
                Console.Write(message);
                try
                {
                    result = Double.Parse(Console.ReadLine());
                    isCorrectInput = true;
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message + "\n");
                    isCorrectInput = false;
                }
            }

            return result;
        }
    }
}
