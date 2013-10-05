using System.Collections.Generic;

namespace Phonebook
{
    interface IPhonebookRepository
    {
        bool AddPhone(string name, IEnumerable<string> phoneNumbers);

        int ChangePhone(string oldPhoneNumber, string newPhoneNumber);

        Content[] ListEntries(int startIndex, int count);
    }
}