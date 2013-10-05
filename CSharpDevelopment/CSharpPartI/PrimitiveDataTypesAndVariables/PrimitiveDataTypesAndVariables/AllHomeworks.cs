using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimitiveDataTypesAndVariables
{
    class AllHomeworks
    {
        static void Main(string[] args)
        {
            //ComparesFloating();
            //DeclareHexadecimalInteger();
            //DeclareHexadecimalChar();
            //bool isFemale = false;
            //Console.Write(DeclareHelloWorldString());

            //string firstWay = "The \"use\" of quotations causes difficulties.";
            //string secondWay = @"The ""use"" of quotations causes difficulties.";

            //IsoscelesTriangle();

            //string FirstName;
            //string FamilyName;
            //byte Age;
            //bool IsMale;
            //uint ID;
            //uint UniqueEmployeeNumber;

            //ExchangeValues();

            //PrintASCII();

            //PrintNullValues();

            //string FirstName;
            //string MiddleName;
            //string LastName;
            //decimal Balance;
            //string BankName;
            //string IBAN;
            //string BIC;
            //List<ulong> CreditCardList;


            Console.ReadKey();

            
        }

        private static void PrintNullValues()
        {
            int? i = null;
            double? d = null;
            Console.Write(i + " " + d + Environment.NewLine);
            i = 22;
            d = 33.34;
            Console.Write(i + " " + d);
        }

        private static void PrintASCII()
        {
            Console.OutputEncoding = Encoding.Unicode;

            for (int i = 0; i <= 255; i++)
            {
                System.Console.WriteLine(i + " = " + (char)i);
            }
        }

        private static void ExchangeValues()
        {
            int first = 5;
            int second = 10;

            first += second;
            second = first - second;
            first -= second;
        }

        private static void IsoscelesTriangle()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //int countEmpty;
            int initNumber = 3;
            for (int i = 0; i < initNumber; i++)
            {
                //countEmpty = 0;
                for (int k = 0; k < initNumber - 1 - i; k++)
                {
                    Console.Write(' ');
                    //countEmpty++;
                }

                for (int j = 0; j <= i; j++)
                {
                    Console.Write('\u00A9');
                }

                for (int j = 0; j < i; j++)
                {
                    Console.Write('\u00A9');
                }

                //for (int k = 0; k < countEmpty; k++)
                //{
                //    Console.Write(' ');
                //}

                if (i < initNumber - 1)
                    Console.Write(Environment.NewLine);
            }
            
            Console.WriteLine();
        }

        private static void WriteSpace(int numberOfSpace)
        {
            for (int i = 0; i < numberOfSpace; i++)
                Console.Write(' ');
        }

        static void DeclareFiveVariables()
        {
            ushort first = 52130;
            short two = -155;
            uint three = 4825932;
            byte four = 97;
            short fift = -10000;
        }

        #region ComparesFloating

        static void ComparesFloating()
        {
            float precision = 0.000001f;
            float f1 = 5.3f;
            float f2 = 6.01f;
            float f3 = 5.00000001f;
            float f4 = 5.00000003f;

            Console.Write("Compare of two float numbers " + f1 + " and " + f2 + " return: " + f1.IsFloatPrecision(f2, precision));
            Console.Write(Environment.NewLine);
            Console.Write("Compare of two float numbers " + f3 + " and " + f4 + " return: " + f3.IsFloatPrecision(f4, precision));
        }

        #endregion

        static void DeclareHexadecimalInteger()
        {
            int hexInt = 0xFE;
            Console.Write(hexInt);
        }

        static void DeclareHexadecimalChar()
        {
            char myValue = Char.Parse("\u0048");
            Console.Write(myValue);
        }

        static string DeclareHelloWorldString()
        {
            string hello = "Hello";
            string world = "World";
            object text = hello + " " + world;
            string result = text.ToString();
            return result;
        }
    }
}
