using System;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            House water = new House(3);

            water.Up();
            water.Down();
            water.Down();
            water.Up();
            water.Up();
            water.Up();
            water.Up();
            water.Down();


            Console.Read();
        }
    }

    class House
    {
        public IHouseState State { get; set; }
        public int AmountFloors { get; set; }
        public int CurrentFloor { get; set; }

        public House(int amountFloors)
        {
            State = new GroundFloor();
            AmountFloors = amountFloors;
            CurrentFloor = 1;
        }

        public void Up()
        {
            State.Up(this);
        }
        public void Down()
        {
            State.Down(this);
        }
    }

    interface IHouseState
    {
        void Up(House water);
        void Down(House water);
    }

    class GroundFloor : IHouseState
    {
        public void Up(House house)
        {
            if (++house.CurrentFloor == house.AmountFloors + 1)
            {
                Console.WriteLine("Вы поднялись на крышу");
                house.State = new Roof();
                return;
            }

            Console.WriteLine($"Поднимаемся на {house.CurrentFloor} этаж.");
            house.State = new Floor();
        }

        public void Down(House water)
        {
            Console.WriteLine("Вниз идти некуда. Остаемся на первом этаже.");
        }
    }

    class Floor : IHouseState
    {
        public void Up(House house)
        {
            if (++house.CurrentFloor == house.AmountFloors + 1)
            {
                Console.WriteLine("Вы поднялись на крышу");
                house.State = new Roof();
                return;
            }

            Console.WriteLine($"Поднимаемся на {house.CurrentFloor} этаж.");
        }

        public void Down(House house)
        {
            if (--house.CurrentFloor == 1)
            {
                Console.WriteLine("Вы спустились на первый этаж!");
                house.State = new GroundFloor();
                return;
            }

            Console.WriteLine($"Спускаемся на {house.CurrentFloor} этаж.");
        }
    }

    class Roof : IHouseState
    {
        public void Up(House water)
        {
            Console.WriteLine("Подниматься некуда!");
        }

        public void Down(House house)
        {
            if (--house.CurrentFloor == 1)
            {
                Console.WriteLine("Вы спустились на первый этаж!");
                house.State = new GroundFloor();
                return;
            }

            Console.WriteLine($"Спускаемся на {house.CurrentFloor} этаж.");
        }
    }

}