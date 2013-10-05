using System;

namespace ShoppingCenter
{
    public class Content : IComparable<Content>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Producer { get; set; }

        //public Content(string name, double price, string producer)
        //{
        //    this.Name = name;
        //    this.Price = price;
        //    this.Producer = producer;
        //}

        public int CompareTo(Content other)
        {
            int result = 0;

            //if (this.Name == null)
            //{
            //    return 1;
            //}
            //else
            //{
            //    result = this.Name.CompareTo(other.Name);
            //    if (result != 0) return result;
            //}

            result = this.Price.CompareTo(other.Price);
            if (result != 0) return result;

            //if (this.Producer != null)
            //{
            //    return 1;
            //}
            //else
            //{
            //    result = this.Producer.CompareTo(other.Producer);
            //    if (result != 0) return result;
            //}


            return result;
        }

        public override string ToString()
        {
            var output = "{" + String.Format("{0};{1};{2}",
                                        this.Name, this.Producer,
                                        this.Price.ToString("0.00")) + "}";
            return output;
        }
    }
}