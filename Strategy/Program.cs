using System;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatterns.Strategy.Conceptual
{
    // Контекст определяет интерфейс, представляющий интерес для клиентов.
    class Fight
    {
        // Контекст хранит ссылку на один из объектов Стратегии. Контекст не
        // знает конкретного класса стратегии. Он должен работать со всеми
        // стратегиями через интерфейс Стратегии.
        private IStrategy _strategy;

        public Fight()
        { }

        // Обычно Контекст принимает стратегию через конструктор, а также
        // предоставляет сеттер для её изменения во время выполнения.
        public Fight(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Обычно Контекст позволяет заменить объект Стратегии во время
        // выполнения.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Вместо того, чтобы самостоятельно реализовывать множественные
        // версии алгоритма, Контекст делегирует некоторую работу объекту
        // Стратегии.
        public void BeginFight()
        {
            Console.WriteLine("Бой начинается!");
            var result = this._strategy.DoAlgorithm();

            Console.WriteLine(result);
        }
    }

    // Интерфейс Стратегии объявляет операции, общие для всех поддерживаемых
    // версий некоторого алгоритма.
    //
    // Контекст использует этот интерфейс для вызова алгоритма, определённого
    // Конкретными Стратегиями.
    public interface IStrategy
    {
        string DoAlgorithm();
    }

    // Конкретные Стратегии реализуют алгоритм, следуя базовому интерфейсу
    // Стратегии. Этот интерфейс делает их взаимозаменяемыми в Контексте.
    class LineStrategy : IStrategy
    {
        public string DoAlgorithm()
        {
            string result = string.Empty;

            result += "Вражеская конница пробила строй!\n";
            Thread.Sleep(1000);
            result += "Вражеская конница окружила оставшихся всадников\n";
            Thread.Sleep(1000);
            result += "Бой проигран!";

            return result;
        }
    }

    class WedgeStrategy : IStrategy
    {
        public string DoAlgorithm()
        {
            string result = string.Empty;

            result += "Удар клин клином уничтожил основные сили врага!\n";
            Thread.Sleep(1000);
            result += "Вражеская конница потеряла боевой дух!\n";
            Thread.Sleep(1000);
            result += "Враги бегут с поля боя!\n";
            Thread.Sleep(1000);
            result += "Бой выигран!";

            return result;
        }
    }

    class AllAroundStrategy : IStrategy
    {
        public string DoAlgorithm()
        {
            string result = string.Empty;

            result += "Вражеская конница, пройдя сквозь наши силы, уничтожила значительную часть врагов!\n";
            Thread.Sleep(1000);
            result += "Союзные всадники бежали с поля боя!\n";
            Thread.Sleep(1000);
            result += "Бой проигран!";

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new Fight();

            Console.WriteLine("На вашу конницу надвигается вражеская конница, построенная клином!");
            Console.WriteLine("Командуйте армией! Как построить конницу?");
            int chouse;

            Console.WriteLine("1 - построить линией.");
            Console.WriteLine("2 - построить клином.");
            Console.WriteLine("3 - в разброс.");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out chouse) && chouse > 0 && chouse < 4)
                {
                    break;
                }
                Console.WriteLine("Неправильный ввод!");
            }

            switch (chouse)
            {
                case 1:
                    context.SetStrategy(new LineStrategy());
                    context.BeginFight();
                    break;
                case 2:
                    context.SetStrategy(new WedgeStrategy());
                    context.BeginFight();
                    break;
                case 3:
                    context.SetStrategy(new AllAroundStrategy());
                    context.BeginFight();
                    break;
            }

            Console.ReadLine();
        }
    }
}