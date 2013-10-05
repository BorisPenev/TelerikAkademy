using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoInOne
{
    class Program
    {
        static bool isRightDirection = false;
        static void Main()
        {
            Console.SetIn(File.OpenText("TextFile1.txt"));
            int n = int.Parse(Console.ReadLine());
            string move1 = Console.ReadLine();
            string move2 = Console.ReadLine();
            Dictionary<int, Turn> Turns = new Dictionary<int, Turn>();
            for (int i = 1; i <= n; i++)
            {
                Turns.Add(i, new Turn() { Number = i} );
            }
            int lastNumber = 0;
            int count = 0;
            int multiplayer = 1;
            while (Turns.Where(t => !t.Value.IsCaptured).ToList().Count > 0)
            {
                var newTurns = Turns.Where(t => !t.Value.IsCaptured).ToList();
                count = newTurns.Count;
                for (int i = 0; i < count; i++)
                {
                    lastNumber = newTurns[i].Value.Number;
                    Turns[i].IsCaptured = true;
                    i += multiplayer;
                }
                multiplayer++;
            }
            Console.WriteLine(lastNumber);
            Console.WriteLine(MakeMoves(move1));
            Console.WriteLine(MakeMoves(move2));
            //work :)
            //Console.WriteLine("bounded");
            //Console.WriteLine("bounded");
        }

        private static string MakeMoves(string move1)
        {
            var commands = move1.ToCharArray().Select(s => s.ToString()).ToList();
            Dictionary<string, int> moves = new Dictionary<string, int>();
            if (commands.Count == 0)
            {
                if (commands[0] != "S")
                    return "bounded";
                else
                    return "unbounded";
            }
            else
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    switch (commands[i])
                    {
                        case "L":
                            isRightDirection = false;
                            break;
                        case "R":
                            isRightDirection = true;
                            break;
                        default:
                            //moveforward
                            break;
                    }
                }
            }
            return "bounded";
        }
    }
}
