using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        static void Main()
        {
            //1. Write a program that reads an integer number and calculates and prints its square root. If the number is invalid or negative, print "Invalid number". In all cases finally print "Good bye". Use try-catch-finally.
            //CalculateSquareRoot();

            //2. Write a method ReadNumber(int start, int end) that enters an integer number in given range [start…end]. If an invalid number or non-number text is entered, the method should throw an exception. Using this method write a program that enters 10 numbers:
			//a1, a2, … a10, such that 1 < a1 < … < a10 < 100
            //ReadNumber(1, 100);

            //3. Write a program that enters file name along with its full file path (e.g. C:\WINDOWS\win.ini), reads its contents and prints it on the console. Find in MSDN how to use System.IO.File.ReadAllText(…). Be sure to catch all possible exceptions and print user-friendly error messages.
            //ReadAndPrintFileContent();

            //4.Write a program that downloads a file from Internet (e.g. http://www.devbg.org/img/Logo-BASD.jpg) and stores it the current directory. Find in Google how to download files in C#. Be sure to catch all exceptions and to free any used resources in the finally block.
            //FileDownload();
        }

        private static void FileDownload()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile("http://www.devbg.org/img/Logo-BASD.jpg", "Logo-BASD.jpg");
                }
                catch (Exception)
                {
                }
            }
        }

        private static void ReadAndPrintFileContent()
        {
            try
            {
                string filePath = Console.ReadLine();
                if (File.Exists(filePath))
                    Console.WriteLine(File.ReadAllText(filePath));
                else
                    Console.WriteLine("The file do not exist");
            }
            catch (AccessViolationException Ex)
            {
                Console.WriteLine("You don't have the permission to open this");
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Something wrong is happened!");
            }
        }

        private static void ReadNumber(int start, int end)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    int n = int.Parse(Console.ReadLine());
                    if (n < start || n > end)
                        throw new ArgumentNullException();
                }
            }
            catch (FormatException)
            {
                throw new ArgumentNullException();
            }
        }

        private static void CalculateSquareRoot()
        {
            try
            {
                uint n = uint.Parse(Console.ReadLine());
                Console.WriteLine(Math.Sqrt(n));
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid number");
            }
            finally
            {
                Console.WriteLine("Good bye");
            }
        }
    }
}
