using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace TradeCompany
{
    class Program
    {
        static void Main()
        {
            OrderedMultiDictionary<double, Article> articles = new OrderedMultiDictionary<double, Article>(true);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string line;
            using (StreamReader reader = new StreamReader(@"..\..\data1500000.csv"))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] content = line.Split(new char[] { ',', '"' }, StringSplitOptions.RemoveEmptyEntries);
                    Article article = new Article(content[0], content[1], content[2], double.Parse(content[3]));
                    if (articles.ContainsKey(article.Price))
                    {
                        articles[article.Price].Add(article);
                    }
                    else
                    {
                        articles.Add(article.Price, article);
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Create and add 15k Articles: {0}", stopwatch.Elapsed);

            stopwatch.Reset();
            stopwatch.Restart();
            var result = articles.Range(14556.0, true, 4665542.0, true);
            stopwatch.Stop();
            Console.WriteLine("Search: {0}", stopwatch.Elapsed);
            Console.WriteLine(result.Count);

            //To test with 1 500 000 mil Articles download 100mb file: 
            //http://www.4shared.com/rar/kBeJdHzA/data1500000.html
            //http://www.filedropper.com/data1500000
            //Create and add 1 500 000 mil Articles: 00:00:19.8488240
            //Search: 00:00:00.0015390
            //1394252
            //Press any key to continue . . .
        }
    }
}
