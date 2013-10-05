using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Phonebook
{
    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly OrderedSet<Content> sortedContent;
        private readonly Dictionary<string, Content> namesDict;
        private readonly MultiDictionary<string, Content> numbersDict;
        
        public int Count
        {
            get
            {
                return this.namesDict.Count;
            }
        }

        public PhonebookRepository()
        {
            sortedContent = new OrderedSet<Content>();
            namesDict = new Dictionary<string, Content>(); 
            numbersDict = new MultiDictionary<string, Content>(false);
        }

        /// <summary>
        /// Add New Phone Record
        /// </summary>
        /// <param name="name">Name of the person</param>
        /// <param name="numbers">Phone number list for this person</param>
        /// <returns>return true if person exist and make a merging operation
        ///  else return false and make adding operation</returns>
        public bool AddPhone(string name, IEnumerable<string> numbers)
        {
            string nameLower = name.ToLowerInvariant();
            Content content;
            
            bool isExist = this.namesDict.TryGetValue(nameLower, out content);
            if (!isExist)
            {
                content = new Content();
                content.Name = name;
                this.namesDict.Add(nameLower, content);
                this.sortedContent.Add(content);
            }

            foreach (var num in numbers)
            {
                this.numbersDict.Add(num, content);
            }

            content.Numbers.UnionWith(numbers);
            return isExist;
        }

        /// <summary>
        /// Change Phone number to all maches
        /// </summary>
        /// <param name="oldNumber">old phone number that we will change</param>
        /// <param name="newNumber">new phone number</param>
        /// <returns>count of how much numbers is changed to the new</returns>
        public int ChangePhone(string oldNumber, string newNumber)
        {
            var foundNumbers = this.numbersDict[oldNumber].ToList();
            foreach (var number in foundNumbers)
            {
                number.Numbers.Remove(oldNumber);
                this.numbersDict.Remove(oldNumber, number);

                number.Numbers.Add(newNumber);
                this.numbersDict.Add(newNumber, number);
            }
            return foundNumbers.Count;
        }

        /// <summary>
        /// List Phone Contacts
        /// </summary>
        /// <param name="startIndex">Start search from index</param>
        /// <param name="count">Return number of records</param>
        /// <returns>if startIndex + count is over total content then return ArgumentOutOfRangeException
        /// else return total numbers</returns>
        public Content[] ListEntries(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex + count > this.namesDict.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid start index or count.");
            }

            var list = new Content[count];
            for (int i = startIndex; i <= startIndex + count - 1; i++)
            {
                var entry = this.sortedContent[i];
                list[i - startIndex] = entry;
            }
            return list;
        }
    }
}