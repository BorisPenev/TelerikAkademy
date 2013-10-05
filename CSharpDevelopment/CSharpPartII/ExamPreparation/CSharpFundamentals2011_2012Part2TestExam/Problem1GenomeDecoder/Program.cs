using System;
using System.Linq;
using System.Text;

namespace Problem1GenomeDecoder
{
    class Program
    {
        static void Main()
        {
            string[] line = Console.ReadLine().Split(' ');
            long n = long.Parse(line[0]);
            long m = long.Parse(line[1]);
            string codedGenom = Console.ReadLine();
            long count = 0;
            long countLine = 1;

            StringBuilder sbNumber = new StringBuilder();
            StringBuilder sbMain = new StringBuilder();
            StringBuilder sbLine = new StringBuilder();
            int temp = 0;
            for (int i = 0; i < codedGenom.Length; i++)
            {
                if (int.TryParse(codedGenom[i].ToString(), out temp))
                {
                    sbNumber.Append(temp);
                }
                else
                {
                    if (int.TryParse(sbNumber.ToString(), out temp))
                        sbMain.AppendFormat(new string(codedGenom[i], temp));
                    else
                        sbMain.AppendFormat(new string(codedGenom[i], 1));
                    sbNumber.Clear();
                }
            }

            long formatter = (sbMain.Length / n).ToString().Length;
            if (sbMain.Length % n > 0)
                formatter++;

            while (sbMain.Length > 0)
            {
                sbLine.AppendFormat("{0, "+ formatter +"}", countLine);
                while (count < n && sbMain.Length > 0)
                {
                    sbLine.Append(" ");
                    for (long j = 0; j < m; j++)
                    {
                        if (count < n && sbMain.Length > 0)
                        {
                            sbLine.Append(sbMain[0].ToString());
                            sbMain.Remove(0, 1);
                            count++;
                        }
                    }
                }
                count = 0;
                sbLine.Append(Environment.NewLine);
                countLine++;
            }
            sbLine.Remove(sbLine.Length - 1, 1);
            Console.WriteLine(sbLine.ToString());
        }
    }
}
