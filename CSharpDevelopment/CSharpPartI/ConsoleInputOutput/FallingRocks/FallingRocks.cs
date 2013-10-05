using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FallingRocks
{
    class FallingRocks
    {
        static List<Rock> rocks = new List<Rock>();
        static int dwarfX, dwarfY;
        static int points = 0;
        static int gameLevel = 1;
        static int count = 0;

        static void Main()
        {
            bool isDie = GameInit();

            while (!isDie)
            {
                AddNewRock();
                isDie = rocks.TakeWhile(r => r != null).ForEach(r =>
                {
                    if (r.Y >= 0)
                    {
                        if (!r.IsActive)
                            DrawRock(r);
                        else
                        {
                            if ((dwarfX == r.X && dwarfY == r.Y + 1) ||
                                (dwarfX == r.X - 1 && dwarfY == r.Y + 1) ||
                                (dwarfX == r.X - 2 && dwarfY == r.Y + 1))
                                return true;
                            else
                                Console.MoveBufferArea(r.X, r.Y, 1, 1, r.X, r.Y + 1);
                        }
                    }

                    if (r.Y == Console.BufferHeight - 1)
                    {
                        ResetRock(r);
                    }

                    if (r.Y < 24)
                        r.Y++;

                    return false;
                });

                if (!isDie)
                {
                    MoveDwarf();

                    Thread.Sleep(200);
                    if (gameLevel < 30)
                        gameLevel++;
                }
            }

            DieScreen();
            RestartGame();
        }

        private static void DrawDrawf()
        {
            dwarfX = Console.BufferWidth / 2;
            dwarfY = Console.BufferHeight - 2;

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(dwarfX, dwarfY);
            Console.WriteLine("(0)");
        }

        private static bool GameInit()
        {
            rocks = new List<Rock>();
            points = 0;
            gameLevel = 1;
            count = 0;

            Console.Clear();
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            DrawDrawf();

            return false;
        }

        private static void DieScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You die at points: " + points);
            Console.WriteLine("Press any key to restart the game.");
        }

        private static void AddNewRock()
        {
            count = rocks.Count;
            for (int i = 0; i < gameLevel - count; i++)
            {
                Rock r = new Rock() { X = Helpers.RandomNumber(0, Console.BufferWidth - 1), Y = Helpers.RandomNumber(-10, 1) };
                rocks.Add(r);
            }
        }

        private static void ResetRock(Rock r)
        {
            r.X = Helpers.RandomNumber(0, Console.BufferWidth - 1);
            r.Y = 0;
            r.IsActive = false;
            points++;
        }

        private static void DrawRock(Rock r)
        {
            Console.ForegroundColor = r.Color;
            Console.SetCursorPosition(r.X, r.Y);
            Console.WriteLine(r.Type.ToString());
            r.IsActive = true;
            r.Y--;
        }

        private static void MoveDwarf()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (dwarfX >= 1)
                        {
                            Console.MoveBufferArea(dwarfX, dwarfY, 3, 1, dwarfX - 1, dwarfY);
                            dwarfX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (dwarfX <= Console.BufferWidth - 4)
                        {
                            Console.MoveBufferArea(dwarfX, dwarfY, 3, 1, dwarfX + 1, dwarfY);
                            dwarfX++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RestartGame()
        {
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    RestartGame();
                    break;
                case ConsoleKey.RightArrow:
                    RestartGame();
                    break;
                default:
                    Main();
                    break;
            }
        }
    }
}