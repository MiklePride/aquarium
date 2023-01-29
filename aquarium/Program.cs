using System;
using System.Collections.Generic;

namespace aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Start();
        }
    }
}

class Aquarium
{
    private List<Fish> _fish = new List<Fish>();

    private bool _isWork = true;
    private int _maximumFish = 5;

    public void Start()
    {
        const int CommandKey1 = 1;
        const int CommandKey2 = 2;
        const int CommandKey3 = 3;
        const int CommandKey4 = 4;

        Console.WriteLine("Добро подаловать в аквариум!");

        while (_isWork)
        {
            ShowInfoFish();

            Console.WriteLine("Выберите действие:\n" +
                "1. Добавить рыбку.\n" +
                "2. Убрать рыбку.\n" +
                "3. Следующий день\n" +
                "4. Выход\n");

            int userInput = UserUtils.GetNumber();

            switch (userInput)
            {
                case CommandKey1:
                    AddFish();
                    break;
                case CommandKey2:
                    DeleteFish();
                    break;
                case CommandKey3:
                    GoNextDay();
                    break;
                case CommandKey4:
                    _isWork = false;
                    break;
            }
        }
    }

    private void ShowInfoFish()
    {
        if (_fish.Count == 0)
        {
            Console.WriteLine("Аквариум пуст! Добавьте рыб");
        }
        else
        {
            int fishCount = _fish.Count;

            Console.WriteLine($"рыб в аквариуме {fishCount} из {_maximumFish}");

            foreach (Fish fish in _fish)
            {
                fish.ShowInfo();
            }
        }
    }

    private void AddFish()
    {
        if (_fish.Count == _maximumFish)
        {
            Console.WriteLine("Аквариум переполнен!");
        }
        else
        {
            Console.Write("Дайте имя новой рыбке: ");

            string fishName = Console.ReadLine();

            _fish.Add(new Fish(fishName));

            Console.WriteLine($"Рыбка по имени {fishName} добавлена в аквариум.\n\n");
        }
    }

    private void DeleteFish()
    {
        if (_fish.Count == 0)
        {
            Console.WriteLine("Аквариум и так пуст.\n\n");
        }
        else
        {
            bool isWork = true;
            int fishIndex = 0;

            foreach (Fish fish in _fish)
            {
                fishIndex++;

                Console.Write(fishIndex);
                fish.ShowInfo();
            }

            Console.WriteLine("Введите номер рыбки, которую хотите убрать: ");

            while (isWork)
            {
                int userNumber = UserUtils.GetNumber();

                if (userNumber > _fish.Count || userNumber < 1)
                {
                    Console.WriteLine("Некорректный ввод, попробуйте снова!\n\n");
                }
                else
                {
                    _fish.RemoveAt(userNumber - 1);

                    Console.WriteLine("Рыбка удалена\n\n");

                    isWork = false;
                }
            }
        }
    }

    private void GoNextDay()
    {
        if (_fish.Count > 0)
        {
            foreach (Fish fish in _fish)
            {
                fish.GrowOld();
            }
        }
        else
        {
            Console.WriteLine("Аквариум пуст. Наступает следующий день.\n\n");
        }
    }
}

class Fish
{
    private static Random _random = new Random();

    private string _name;
    private int _health;
    private int _obsolescence = 1;

    public Fish(string name)
    {
        int minimumHealth = 5;
        int maximumHealth = 16;

        _name = name;
        _health = _random.Next(minimumHealth, maximumHealth);
        IsAlive = true;
    }

    public bool IsAlive { get; private set; }

    public void ShowInfo()
    {
        if (IsAlive)
        {
            Console.WriteLine($"Имя рыбки: {_name}\nЖизней осталось: {_health}");
        }
        else
        {
            Console.WriteLine($"{_name} мертва.");
        }
    }

    public void GrowOld()
    {
        if (IsAlive)
            _health -= _obsolescence;

        if (_health == 0 && IsAlive)
            IsAlive = false;
    }
}

static class UserUtils
{
    public static int GetNumber()
    {
        bool isNumberWork = true;
        int userNumber = 0;

        while (isNumberWork)
        {
            bool isNumber;
            string userInput = Console.ReadLine();

            if (isNumber = int.TryParse(userInput, out int number))
            {
                userNumber = number;
                isNumberWork = false;
            }
            else
            {
                Console.WriteLine($"Не правильный ввод данных!!!  Повторите попытку");
            }
        }
        return userNumber;
    }
}
