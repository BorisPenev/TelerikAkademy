using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem3Poker
{
    class Program
    {
        static void Main()
        {
            Dictionary<int, int> cards = new Dictionary<int, int>();
            string temp = string.Empty;
            int tempNumber = 0;
            string result = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                temp = Console.ReadLine();
                switch (temp)
                {
                    case "A":
                        tempNumber = 99;
                        break;
                    case "J":
                        tempNumber = 11;
                        break;
                    case "Q":
                        tempNumber = 12;
                        break;
                    case "K":
                        tempNumber = 13;
                        break;
                    default:
                        tempNumber = int.Parse(temp);
                        break;
                }
                if (cards.ContainsKey(tempNumber))
                    cards[tempNumber]++;
                else
                    cards.Add(tempNumber, 1);
            }

            switch (cards.Count)
            {
                case 1:
                    result = "Impossible";
                    break;
                case 2:
                    result = "Full House";
                    if (cards.Where(c => c.Value == 1).Count() > 0)
                        result = "Four of a Kind";
                    break;
                case 3:
                    result = "Two Pairs";
                    if (cards.Where(c => c.Value == 3).Count() > 0)
                        result = "Three of a Kind";
                    break;
                case 4:
                    result = "One Pair";
                    break;
                case 5:
                    result = "Straight";
                    var tempDict = cards.Keys.OrderBy(c => c).ToList();
                    for (int i = 0; i < 5; i++)
                    {
                        if (i + 1 < 5)
                        {
                            if (tempDict[i] + 1 != tempDict[i + 1])
                            {
                                if (i + 1 == 4 && tempDict[4] == 99)
                                {
                                    continue;
                                }
                                else
                                {
                                    result = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(result))
                result = "Nothing";
            Console.WriteLine(result);
        }
    }
}
