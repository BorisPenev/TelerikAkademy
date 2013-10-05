using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClassesPartI
{
    public class GSM
    {
        private string owner = string.Empty;
        public string Owner
        {
            get { return owner; }
            set { if(value != null) owner = value; }
        }

        private Models model = Models.LG;
        private Models Model
        {
            get { return model; }
            set { model = value; }
        }

        private ManufacturerType manufacturer = ManufacturerType.Default;
        private ManufacturerType Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        private double price = 0.0;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private Battery battery = new Battery();
        public Battery Battery
        {
            get { return battery; }
            set { battery = value; }
        }

        private Display display = new Display();
        public Display Display
        {
            get { return display; }
            set { display = value; }
        }

        private List<Call> callHistory = new List<Call>();
        public List<Call> CallHistory
        {
            get { return callHistory; }
            set { callHistory = value; }
        }

        private static GSM iPhone4S = new GSM(Models.iPhone4S, ManufacturerType.Apple) { price = 99, Battery = new Battery() { BatteryType = BatteryType.LiIon, HoursIdle = 40, HoursTalk = 4 }, Display = new Display() };
        public static GSM IPhone4S
        {
            get { return iPhone4S; }
            set { iPhone4S = value; }
        }

        public GSM(Models model, ManufacturerType manufacturer)
        {
            this.Model = model;
            this.Manufacturer = manufacturer;
        }

        public GSM(Models model, ManufacturerType manufacturer, double? price, Battery battery, Display display): this(model, manufacturer)
        {
            if (price.HasValue)
                this.price = price.Value;
            if(battery != null)
                this.Battery = battery;
            if (display != null)
                this.Display = display;
        }

        //old
        public void Add(Call call)
        {
            if (call != null)
            {
                //logikata na tova da proverqva i ako nameri obekta da mu go dava e da overridne obekta v spisaka.
                //toy veche go ima, nqma kak da go dobavim - Imame unikalno ID. Trqbva da go updeitnem samo.
                var result = this.CallHistory.Where(c => c.CallID == call.CallID).FirstOrDefault();
                if (result != null)
                    result = call;
                else
                    this.CallHistory.Add(call);
            }
            else
                throw new ArgumentNullException();
        }

        public void Remove(Call call)
        {
            //if (call != null && this.CallHistory.Where(c => c.CallID == call.CallID).FirstOrDefault() != null)
                this.CallHistory.Remove(call);
        }

        public void Clear()
        {
            this.CallHistory.Clear();
        }

        public double TotalPrice(double pricePerMin)
        {
             return (this.CallHistory.Sum(c => c.DurationInSeconds) / 60) * pricePerMin;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Owner: " + this.owner);
            sb.AppendLine("Model: " + this.Model);
            sb.AppendLine("Manufacturer: " + this.Manufacturer);
            sb.AppendLine("Price: " + this.Price);
            if (this.Battery != null)
            {
                sb.AppendLine("Battery Information");
                sb.AppendLine("Battery Type: " + this.Battery.BatteryType);
                sb.AppendLine("Battery Hours Idle: " + this.Battery.HoursIdle);
                sb.AppendLine("Battery Hours Talk: " + this.Battery.HoursTalk);
            }
            if (this.Display != null)
            {
                sb.AppendLine("Display Information");
                sb.AppendLine("Display Size: " + this.Display.Size);
                sb.AppendLine("Display NumberOfColors: " + this.Display.NumberOfColors);
            }
            return sb.ToString();
        }
    }
}
