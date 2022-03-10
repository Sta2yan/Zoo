using System;
using System.Collections.Generic;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            bool isTravel = true;
            

            while (isTravel)
            {
                Console.WriteLine("Добро пожаловать в зоопарк");
                Console.SetCursorPosition(0, 20);
                Console.WriteLine($"{(int)MenuCommands.ComeToValerie}. {MenuCommands.ComeToValerie}" +
                                  $"\n{(int)MenuCommands.MoveAwayFromValerie}. {MenuCommands.MoveAwayFromValerie}" +
                                  $"\n{(int)MenuCommands.Exit}. {MenuCommands.Exit}");
                MenuCommands command = (MenuCommands)GetNumber(Console.ReadLine());
                Console.SetCursorPosition(0, 3);

                switch (command)
                {
                    case MenuCommands.ComeToValerie:
                        user.ComeToValerie();
                        break;
                    case MenuCommands.MoveAwayFromValerie:
                        user.MoveAwayFromValerie();
                        break;
                    case MenuCommands.Exit:
                        isTravel = false;
                        break;
                }

                Console.Clear();
            }

            Console.WriteLine("До скорых встреч!");
        }

        public static int GetNumber(string numberText)
        {
            int number;

            while (int.TryParse(numberText, out number) == false)
            {
                Console.WriteLine("Повторите попытку:");
                numberText = Console.ReadLine();
            }

            return number;
        }
    }

    enum MenuCommands
    {
        ComeToValerie = 1,
        MoveAwayFromValerie,
        Exit
    }

    enum AnimalType
    {
        Bear,
        Cat,
        Wolf,
        Lion
    }

    enum Gender
    {
        Male,
        Female,
    }

    enum Color
    {
        Black,
        White,
        Brown,
        Grey,
        Ginger
    }

    interface IShowInfo
    {
        public void ShowInfo();
    }

    class User : IShowInfo
    {
        private List<Aviary> _aviaries = new List<Aviary>();

        public Aviary Aviary { get; private set; }
        public bool IsNearAviary => Aviary != null;

        public User(List<Aviary> aviaries = null)
        {
            if (aviaries == null)
                SetDefaultAviaries();
            else
                _aviaries = aviaries;

        }

        public void ComeToValerie()
        {
            if (IsNearAviary == false)
            {
                Console.WriteLine("Выберите вальер к которому хотите подойти:");
                ShowInfo();
                Console.WriteLine();
                Aviary = GetAviary(Program.GetNumber(Console.ReadLine()) - 1);
                Console.Clear();
                Console.WriteLine($"Вы подошли к вальеру номер {Aviary.Id}" +
                                  $"\nИнформация о вальере:\n");
                Console.WriteLine(Aviary.AnimalType);
                Aviary.ShowInfo();
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Вы около вальера");
                Console.ReadKey(true);
            }
        }
        
        public void MoveAwayFromValerie()
        {
            if (IsNearAviary)
            {
                Console.WriteLine($"Вы отошли от вальера под номером {Aviary.Id}");
                Aviary = null;
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Вы не около вальера");
                Console.ReadKey(true);
            }
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Вальер с животными: {_aviaries[i].AnimalType}");
            }
        }

        private void SetDefaultAviaries()
        {
            _aviaries.Add(new Aviary(AnimalType.Bear));
            _aviaries.Add(new Aviary(AnimalType.Cat));
            _aviaries.Add(new Aviary(AnimalType.Wolf));
            _aviaries.Add(new Aviary(AnimalType.Lion));
        }

        private Aviary GetAviary(int index)
        {
            while (index < 0 || index >= _aviaries.Count)
            {
                Console.WriteLine("Такого вальера нет, повторите попытку: ");
                index = Program.GetNumber(Console.ReadLine()) - 1;
            }

            return _aviaries[index];
        }
    }

    class Aviary : IShowInfo
    {
        private static int _ids;

        private Random _random = new Random();
        private List<Animal> _animals = new List<Animal>();
        private int _maximumDefaultAnimals = 8;
        private int _minimumDefaultAnimals = 2;
        private int _maximumDefaultGender = 2;

        public int Id { get; private set; }
        public AnimalType AnimalType { get; private set; }

        public Aviary(AnimalType animalType)
        {
            AnimalType = animalType;
            Id = ++_ids;
            SetDefaultListAnimals();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Вальер с животными: {AnimalType}" +
                              $"\nКоличество: {_animals.Count}" +
                              $"\n\nЖивотные:\n");

            for (int i = 0; i < _animals.Count; i++)
            {
                _animals[i].ShowInfo();
            }

            _animals[_random.Next(0, _animals.Count)].MakeSound();
        }

        private void SetDefaultListAnimals()
        {
            switch (AnimalType)
            {
                case AnimalType.Bear:
                    SetDefaultListBears();
                    break;
                case AnimalType.Cat:
                    SetDefaultListCats();
                    break;
                case AnimalType.Wolf:
                    SetDefaultListWolfs();
                    break;
                case AnimalType.Lion:
                    SetDefaultListLions();
                    break;
            }
        }

        private void SetDefaultListBears()
        {
            for (int i = 0; i < _random.Next(_minimumDefaultAnimals, _maximumDefaultAnimals); i++)
            {
                _animals.Add(new Bear($"Мишка_{i + 1}", (Gender)_random.Next(0, _maximumDefaultGender)));
            }
        }

        private void SetDefaultListCats()
        {
            for (int i = 0; i < _random.Next(_minimumDefaultAnimals, _maximumDefaultAnimals); i++)
            {
                _animals.Add(new Cat($"Котик_{i + 1}", (Gender)_random.Next(0, _maximumDefaultGender)));
            }
        }

        private void SetDefaultListWolfs()
        {
            for (int i = 0; i < _random.Next(_minimumDefaultAnimals, _maximumDefaultAnimals); i++)
            {
                _animals.Add(new Wolf($"Волк_{i + 1}", (Gender)_random.Next(0, _maximumDefaultGender)));
            }
        }

        private void SetDefaultListLions()
        {
            for (int i = 0; i < _random.Next(_minimumDefaultAnimals, _maximumDefaultAnimals); i++)
            {
                _animals.Add(new Lion($"Лев_{i + 1}", (Gender)_random.Next(0, _maximumDefaultGender)));
            }
        }
    }

    abstract class Animal : IShowInfo
    {
        private Random _random = new Random();
        private int _maximumColor = 5;

        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public Color Сolor { get; private set; }

        public Animal(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            Сolor = (Color)_random.Next(0, _maximumColor);
        }

        public virtual void MakeSound()
        {
            Console.Write(Name + ": ");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name} | Пол - {Gender}");
        }
    }

    class Bear : Animal
    {
        public Bear(string name, Gender gender) :
            base(name, gender) { }

        public override void MakeSound()
        {
            base.MakeSound();
            Console.WriteLine("рычит");
        }
    }

    class Cat : Animal
    {
        public Cat(string name, Gender gender) :
            base(name, gender) { }

        public override void MakeSound()
        {
            base.MakeSound();
            Console.WriteLine("мяукает");
        }
    }

    class Wolf : Animal
    {
        public Wolf(string name, Gender gender) :
            base(name, gender) { }

        public override void MakeSound()
        {
            base.MakeSound();
            Console.WriteLine("воет");
        }
    }

    class Lion : Animal
    {
        public Lion(string name, Gender gender) :
            base(name, gender) { }

        public override void MakeSound()
        {
            base.MakeSound();
            Console.WriteLine("рычит по королевски");
        }
    }
}