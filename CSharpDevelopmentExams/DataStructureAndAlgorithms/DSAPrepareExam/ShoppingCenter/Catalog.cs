using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Wintellect.PowerCollections;

namespace ShoppingCenter
{
    public class Catalog
    {
        private readonly MultiDictionary<string, Content> name;
        private readonly MultiDictionary<string, Content> producer;
        private readonly OrderedBag<Content> price;
        private readonly MultiDictionary<Tuple<string, string>, Content> nameAndProducer; 

        public Catalog()
        {
            this.name = new MultiDictionary<string, Content>(true);
            this.price = new OrderedBag<Content>();
            this.producer = new MultiDictionary<string, Content>(true);
            this.nameAndProducer = new MultiDictionary<Tuple<string, string>, Content>(true);
        }

        public void Add(Content content)
        {
            this.name.Add(content.Name, content);
            this.producer.Add(content.Producer, content);
            this.nameAndProducer.Add(new Tuple<string, string>(content.Name, content.Producer), content);
            this.price.Add(content);
        }

        public int Delete(string name, string producer)
        {
            var result = 0;
            ICollection<Content> contentToList = new Collection<Content>();

            if (!string.IsNullOrEmpty(name))
            {
                //remove by name and producer
                var searchResult = new Tuple<string, string>(name, producer);
                contentToList = this.nameAndProducer[searchResult];
                foreach (Content content in contentToList)
                {
                    this.name.Remove(content.Name, content);
                    this.price.Remove(content);
                    this.producer.Remove(content.Producer, content);
                    
                }
                result = contentToList.Count;
                this.nameAndProducer.Remove(searchResult);
            }
            else
            {
                //remove by producer only
                contentToList = this.producer[producer];
                foreach (Content content in contentToList)
                {
                    this.name.Remove(content.Name, content);
                    this.price.Remove(content);
                    this.nameAndProducer.Remove(new Tuple<string, string>(content.Name, content.Producer), content);
                }
                result = contentToList.Count;
                this.producer.Remove(producer);
            }

            return result;
        }

        public IEnumerable<Content> GetContentByName(string value)
        {
            return this.name[value].OrderBy(c => c);
        }

        public IEnumerable<Content> GetContentByProducer(string value)
        {
            return this.producer[value].OrderBy(c => c);
        }

        public IEnumerable<Content> GetContentByPriceRange(double minPrice, double maxPrice)
        {
            var result = this.price.Range(new Content() {Price = minPrice}, true, new Content() {Price = maxPrice}, true).OrderBy(s => s.Name).ThenBy(s => s.Producer);
            return result;
        }
    }
}