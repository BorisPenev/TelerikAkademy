using System;
using System.Linq;
using System.Text;

namespace Phonebook
{
    class CommandExecutor
    {
        private const string COUNTRY_CODE = "+359";

        public void ExecuteCommand(PhonebookRepository phonebookRepository, Command command, StringBuilder output)
        {
            switch (command.Type)
            {
                case CommandTypes.AddPhone:
                    AddPhoneCommand(phonebookRepository, command, output);
                    break;
                case CommandTypes.ChangePhone:
                    ChangePhoneCommand(phonebookRepository, command, output);
                    break;
                case CommandTypes.List:
                    ListCommand(phonebookRepository, command, output);
                    break;
                default:
                    throw new ArgumentException("Unknown command: " + command.Type.ToString());
            }
        }

        private static void AddPhoneCommand(PhonebookRepository phonebookRepository, Command command, StringBuilder output)
        {
            var name = command.Parameters[0];
            var numbers = command.Parameters.Skip(1).ToList();

            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = Convertnumber(numbers[i]);
            }

            var isExist = phonebookRepository.AddPhone(name, numbers);
            switch (isExist)
            {
                case true:
                    output.AppendLine("Phone entry merged");
                    break;
                default:
                    output.AppendLine("Phone entry created");
                    break;
            }
        }

        private static void ChangePhoneCommand(PhonebookRepository phonebookRepository, Command command, StringBuilder output)
        {
            var changeResult = phonebookRepository.ChangePhone(Convertnumber(command.Parameters[0]),
                                                                Convertnumber(command.Parameters[1]));
            output.AppendLine("" + changeResult + " numbers changed");
        }

        private static void ListCommand(PhonebookRepository phonebookRepository, Command command, StringBuilder output)
        {
            try
            {
                var entries = phonebookRepository.ListEntries(int.Parse(command.Parameters[0]),
                                                                int.Parse(command.Parameters[1]));
                foreach (var entry in entries)
                {
                    output.AppendLine(entry.ToString());
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                output.AppendLine("Invalid range");
            }
        }

        private static string Convertnumber(string number)
        {
            //Bottleneck Found !!!!
            //Remove from this method next line from the code to fix it.
            //input was used to collect the result and print it on the console.
            //it is nonsence to do for loop for it here.
            //All tests passed under 1sec after remove this meaningless loop.
            //for (int i = 0; i <= input.Length; i++)
            //{
            //    sb.Clear();
            //}
            var sbConvertedNumber = new StringBuilder();

            foreach (char ch in number)
            {
                if (char.IsDigit(ch) || (ch == '+'))
                {
                    sbConvertedNumber.Append(ch);
                }
            }
            
            if (sbConvertedNumber.Length >= 2 && sbConvertedNumber[0] == '0' && sbConvertedNumber[1] == '0')
            {
                sbConvertedNumber.Remove(0, 1);
                sbConvertedNumber[0] = '+';
            }

            while (sbConvertedNumber.Length > 0 && sbConvertedNumber[0] == '0')
            {
                sbConvertedNumber.Remove(0, 1);
            }

            if (sbConvertedNumber.Length > 0 && sbConvertedNumber[0] != '+')
            {
                sbConvertedNumber.Insert(0, COUNTRY_CODE);
            }
            return sbConvertedNumber.ToString();
        }
    }
}