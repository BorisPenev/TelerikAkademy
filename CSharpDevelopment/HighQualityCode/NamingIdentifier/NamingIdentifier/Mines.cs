using System;
using System.Collections.Generic;

namespace NamingIdentifier
{
    partial class Mines
    {
        private const int totalRows = 5;
        private const int totalCols = 10;
        private const int max = 35;

        private static string command = string.Empty;
        private static char[,] fields = CreateGameField();
        private static char[,] bombs = PutBombs();
        private static int openFields = 0;
        private static bool isBomb = false;
        private static List<Champion> champions = new List<Champion>(6);
        private static int rowTurn = 0;
        private static int colTurn = 0;
        private static bool showMenu = true;
        private static bool isAllFieldsOpened = false;

        static void Main()
        {
            StartGame();
        }

        private static void StartGame()
        {
            do
            {
                ShowMenu();
                ReadCommand();
                SetCommandTurn();
                ExecuteCommand();
            } while (command != "exit");
            PrintCredits();
            Console.Read();
        }

        private static void PrintCredits()
        {
            Console.WriteLine("Made in Bulgaria - wowwwwwwwwww!");
            Console.WriteLine("Good luck.");
        }

        private static void ResultCommand()
        {
            if (isBomb)
            {
                GameOver();
            }
            if (isAllFieldsOpened)
            {
                WinGame();
            }
        }

        private static void WinGame()
        {
            Console.WriteLine("\nContratulations! You find all 35 fields without any blood.");
            DrawGameField(bombs);
            Console.WriteLine("Set your username, please: ");
            string username = Console.ReadLine();
            Champion point = new Champion(username, openFields);
            champions.Add(point);
            Highscore(champions);
            fields = CreateGameField();
            bombs = PutBombs();
            openFields = 0;
            isAllFieldsOpened = false;
            showMenu = true;
        }

        private static void GameOver()
        {

            DrawGameField(bombs);
            Console.Write("\nHrrrrrr! You die with {0} points. " +
                          "Please set your username: ", openFields);
            string username = Console.ReadLine();
            Champion point = new Champion(username, openFields);
            if (champions.Count < 5)
            {
                champions.Add(point);
            }
            else
            {
                for (int i = 0; i < champions.Count; i++)
                {
                    if (champions[i].Points < point.Points)
                    {
                        champions.Insert(i, point);
                        champions.RemoveAt(champions.Count - 1);
                        break;
                    }
                }
            }
            champions.Sort((p1, p2) => p2.Name.CompareTo(p1.Name));
            champions.Sort((p1, p2) => p2.Points.CompareTo(p1.Points));
            Highscore(champions);

            fields = CreateGameField();
            bombs = PutBombs();
            openFields = 0;
            isBomb = false;
            showMenu = true;
        }

        private static void ExecuteCommand()
        {
            switch (command)
            {
                case "top":
                    Highscore(champions);
                    break;
                case "restart":
                    RestartCommand();
                    break;
                case "exit":
                    Console.WriteLine("Bay, Bay, Bay!");
                    break;
                case "turn":
                    PlayTurnCommand();
                    break;
                default:
                    Console.WriteLine(Environment.NewLine + "Error! Unknown command" + Environment.NewLine);
                    break;
            }
            ResultCommand();
        }

        private static void PlayTurnCommand()
        {
            if (bombs[rowTurn, colTurn] != '*')
            {
                if (bombs[rowTurn, colTurn] == '-')
                {
                    PlayTurn(rowTurn, colTurn);
                    openFields++;
                }
                if (max == openFields)
                {
                    isAllFieldsOpened = true;
                }
                else
                {
                    DrawGameField(fields);
                }
            }
            else
            {
                isBomb = true;
            }
        }

        private static void RestartCommand()
        {
            fields = CreateGameField();
            bombs = PutBombs();
            DrawGameField(fields);
            isBomb = false;
            showMenu = false;
        }

        private static void SetCommandTurn()
        {
            if (command.Length >= 3)
            {
                if (int.TryParse(command[0].ToString(), out rowTurn) &&
                    int.TryParse(command[2].ToString(), out colTurn) &&
                    rowTurn <= fields.GetLength(0) && colTurn <= fields.GetLength(1))
                {
                    command = "turn";
                }
            }
        }

        private static void ReadCommand()
        {
            Console.Write("Choise row and col: ");
            command = Console.ReadLine().Trim();
        }

        private static void ShowMenu()
        {
            if (showMenu)
            {
                Console.WriteLine("Let's play \"Mines\". Try your luck to find fields without mines." +
                                  " Command 'top' show hightscore, 'restart' start new game, 'exit' Exit game!");
                DrawGameField(fields);
                showMenu = false;
            }
        }

        private static void Highscore(List<Champion> points)
        {
            Console.WriteLine("Highscores:");
            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} fields",
                        i + 1, points[i].Name, points[i].Points);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Empty highscores!" + Environment.NewLine);
            }
        }

        private static void PlayTurn(int row, int col)
        {
            var bombCount = GetBombCount(bombs, row, col);
            bombs[row, col] = bombCount;
            fields[row, col] = bombCount;
        }

        private static void DrawGameField(char[,] field)
        {
            int row = field.GetLength(0);
            int col = field.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < row; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < col; j++)
                {
                    Console.Write(string.Format("{0} ", field[i, j]));
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] CreateGameField()
        {
            var gameField = new char[totalRows, totalCols];

            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalCols; col++)
                {
                    gameField[row, col] = '?';
                }
            }

            return gameField;
        }

        private static char[,] PutBombs()
        {
            var gameField = new char[totalRows, totalCols];

            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalCols; col++)
                {
                    gameField[row, col] = '-';
                }
            }

            CreateBombs(gameField);

            return gameField;
        }

        private static void CreateBombs(char[,] gameField)
        {
            var bombList = new List<int>();
            while (bombList.Count < 15)
            {
                var random = new Random();
                int asfd = random.Next(50);
                if (!bombList.Contains(asfd))
                {
                    bombList.Add(asfd);
                }
            }

            foreach (int bomb in bombList)
            {
                int col = (bomb/totalCols);
                int row = (bomb%totalCols);
                if (row == 0 && bomb != 0)
                {
                    col--;
                    row = totalCols;
                }
                else
                {
                    row++;
                }
                gameField[col, row - 1] = '*';
            }
        }

        private static void Calculations(char[,] field)
        {
            var col = field.GetLength(0);
            var row = field.GetLength(1);

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (field[i, j] != '*')
                    {
                        char bombCount = GetBombCount(field, i, j);
                        field[i, j] = bombCount;
                    }
                }
            }
        }

        private static char GetBombCount(char[,] bombs, int row, int col)
        {
            var count = 0;
            var rows = bombs.GetLength(0);
            var cols = bombs.GetLength(1);

            if (row - 1 >= 0 && bombs[row - 1, col] == '*')
            {
                count++;
            }
            if (row + 1 < rows && bombs[row + 1, col] == '*')
            {
                count++;
            }
            if (col - 1 >= 0 && bombs[row, col - 1] == '*')
            {
                count++;
            }
            if (col + 1 < cols && bombs[row, col + 1] == '*')
            {
                count++;
            }
            if ((row - 1 >= 0) && (col - 1 >= 0) && bombs[row - 1, col - 1] == '*')
            {
                count++;
            }
            if ((row - 1 >= 0) && (col + 1 < cols) && bombs[row - 1, col + 1] == '*')
            {
                count++;
            }
            if ((row + 1 < rows) && (col - 1 >= 0) && bombs[row + 1, col - 1] == '*')
            {
                count++;
            }
            if ((row + 1 < rows) && (col + 1 < cols) && bombs[row + 1, col + 1] == '*')
            {
                count++;
            }
            return char.Parse(count.ToString());
        }
    }
}
