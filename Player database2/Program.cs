using System;
using System.Collections.Generic;

namespace Player_database2
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.Work();
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        string inport = "";
        int checkingForANumber;

        public void Work()
        {
            _players.Add(new Player("Поп", 22));
            _players.Add(new Player("Дорн", 78));
            _players.Add(new Player("Graf", 76));

            string[] menu = { "Добавить игрока", "Заблокировать игрока", "Разблокировать игрока", "Список игроков", "Удалить игрока", "Выход" };
            int index = 0;
            bool launchingTheProgram = true;

            while (launchingTheProgram)
            {
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
                Console.WriteLine("\t\tМеню");

                for (int i = 0; i < menu.Length; i++)
                {
                    if (index == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo userInput = Console.ReadKey(true);

                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index != 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != menu.Length - 1) index++;
                        break;
                    case ConsoleKey.Enter:

                        switch (index)
                        {
                            case 0:
                                AddPlayer();
                                break;
                            case 1:
                                Blocked("Блокировка");
                                break;
                            case 2:
                                Blocked("Разблокировать");
                                break;
                            case 3:
                                Server();
                                break;
                            case 4:
                                DeletePlayer();
                                break;
                            case 5:
                                launchingTheProgram = !launchingTheProgram;
                                break;
                        }
                        break;
                }
            }
        }

        private void DeletePlayer()
        {
            Console.Write($"Введите номер игрока: ");

            TryParse(ref inport, out checkingForANumber);

            _players.RemoveAt(checkingForANumber - 1);

            Clear();
        }
        private void AddPlayer()
        {
            string nickName;
            bool isStringName;

            Console.Write("Введите имя игрока: ");
            nickName = Console.ReadLine();

            Console.Write("Введите лвл игрока: ");
            isStringName = TryParse(ref inport, out checkingForANumber);

            if (isStringName)
            {
                _players.Add(new Player(nickName, checkingForANumber));
            }
            else Console.WriteLine("Вы ввели некорректные данные!");
            Clear();
        }

        private void Blocked(string block)
        {
            bool isBlock;
            Console.Write($"Введите номер игрока: ");
            isBlock = TryParse(ref inport, out checkingForANumber);

            if (isBlock)
            {
                if (block == "Блокировка")
                {
                    _players[checkingForANumber - 1].Blocked();
                }
                else _players[checkingForANumber - 1].UnBlocked();
            }
            Clear();
        }

        private void Server()
        {
            Console.WriteLine("Персонажи на сервере");

            for (int i = 0; i < _players.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _players[i].ShowDetails();
            }
            Clear();
        }
        private bool TryParse(ref string userInput, out int result)
        {
            bool isStringNumber;

            userInput = Console.ReadLine();
            isStringNumber = int.TryParse(userInput, out result);
            return isStringNumber;
        }

        private void Clear()
        {
            Console.ReadKey();
            Console.SetCursorPosition(0, 7);

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\t\t");
            }
        }
    }

    class Player
    {
        private string _nickName;
        private int _lvl;
        public bool IsBanned { get; private set; }

        public Player(string nickName, int lvl)
        {
            _nickName = nickName;

            if (lvl > 0 && lvl <= 100)
            {
                _lvl = lvl;
            }
            else
            {
                _lvl = 1;
                Console.WriteLine("Ввели неверный лвл. Присвоен 1 лвл");
            }
        }

        public void Blocked()
        {
            IsBanned = true;
        }

        public void UnBlocked()
        {
            IsBanned = false;
        }
        public void ShowDetails()
        {
            if (IsBanned == true) Console.WriteLine($"Персонаж - {_nickName}, лвл - {_lvl}, статус бана - заблокирован");
            else Console.WriteLine($"Персонаж - {_nickName}, лвл - {_lvl}, статус бана - не заблокирован");
        }
    }
}
//Задача:
//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть порядковый номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по порядковому номеру, разбана игрока по порядковому номеру и удаление игрока.