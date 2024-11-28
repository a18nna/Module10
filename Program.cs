using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    internal class Program
    {

        public interface ISum
        {
            public int Sum(Calculate calculate);
        }

        public class Calculate : ISum
        {
            public int a;
            public int b;
            private readonly ILogger Logger; 

            public Calculate(int a, int b, ILogger logger) 
            {
                this.a = a;
                this.b = b;
                Logger = logger;
            }

            public int Sum(Calculate calculate)
            {
                try
                {
                    Logger.Event("Подсчет суммы");
                    Thread.Sleep(1000);

                    return calculate.a + calculate.b;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public interface ILogger
        {
            void Event(string message);
            void Error(string message);
        }

        public class Logger : ILogger
        {
            public void Event(string message)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
            }

            public void Error(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
            }
        }

        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            try
            {
                while (true)
                {
                    Console.WriteLine("Введите первое число: ");
                    int num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите второе число: ");
                    int num2 = Convert.ToInt32(Console.ReadLine());

                    Calculate calculate = new Calculate(num1, num2, logger);


                    int result = calculate.Sum(calculate);
                    Console.WriteLine(result);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Для выхода из программы нажмите 3\nЧтобы продолжить нажмите Enter");
                    if (Console.ReadLine() == "3") break;

                        continue;                    
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка! Неверный формат ввода. Введите числа.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Программа завершена.");
            }

            Console.ReadKey();
        }
    }
}
