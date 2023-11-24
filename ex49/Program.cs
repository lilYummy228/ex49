using System;
using System.Collections.Generic;

namespace ex49
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddFish = "1";
            const string CommandRemoveFish = "2";
            const string CommandSkipYear = "3";
            const string CommandExit = "4";

            Aquarium aquarium = new Aquarium();
            bool isOpen = true;

            while (isOpen)
            {
                aquarium.ShowAquarium();

                Console.Write($"{CommandAddFish} - добавить рыбку в аквариум\n" +
                    $"{CommandRemoveFish} - убрать рыбку из аквариума\n" +
                    $"{CommandSkipYear} - подождать год\n" +
                    $"{CommandExit} - выйти\n" +
                    $"Что вы хотите сделать? ");

                switch (Console.ReadLine())
                {
                    case CommandAddFish:
                        aquarium.AddFish();
                        break;

                    case CommandRemoveFish:
                        aquarium.RemoveFish();
                        break;

                    case CommandSkipYear:
                        aquarium.GrowUp();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _aquarium = new List<Fish>();
        private int _maxFishAge = 12;

        public void RemoveFish()
        {
            bool isFound = false;

            Console.Write("Введите имя рыбки, которую хотите убрать: ");
            string name = Console.ReadLine();

            for (int i = 0; i < _aquarium.Count; i++)
            {
                if (_aquarium[i].Name == name)
                {
                    isFound = true;
                    _aquarium.Remove(_aquarium[i]);
                    Console.WriteLine($"Рыбка под именем {name} убрана");
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Такой рыбки нет в аквариуме...");
            }
        }

        public void RemoveDeadFish(Fish fish)
        {
            if (fish.Age == _maxFishAge)
            {
                _aquarium.Remove(fish);
            }
        }

        public void GrowUp()
        {
            for (int i = 0; i < _aquarium.Count; i++)
            {
                _aquarium[i].LiveToDeath();
                RemoveDeadFish(_aquarium[i]);
            }
        }

        public void ShowAquarium()
        {
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Аквариум");

            foreach (Fish fish in _aquarium)
            {
                Console.WriteLine($"{fish.Name}. Возраст - {fish.Age}");
            }

            Console.SetCursorPosition(0, 0);
        }

        public void AddFish()
        {
            int maxFishCount = 10;

            if (_aquarium.Count != maxFishCount)
            {
                Fish fish = CreateFish();

                if (fish != null)
                {
                    _aquarium.Add(fish);
                }
            }
            else
            {
                Console.WriteLine("В аквариуме слишком много рыбок...");
            }
        }

        private Fish CreateFish()
        {
            Console.Write("Введите имя рыбки: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст рыбки: ");

            if (int.TryParse(Console.ReadLine(), out int age))
            {
                if (age < _maxFishAge)
                {
                    return new Fish(name, age);
                }
                else
                {
                    Console.WriteLine("Рыба не может столько жить");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод...");
                return null;
            }
        }
    }

    class Fish
    {
        public Fish(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; protected set; }
        public int Age { get; protected set; }

        public void LiveToDeath()
        {
            Age++;
        }
    }
}
