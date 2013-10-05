using System;
using System.Linq;

namespace DocumentSystem
{
    class AudioDocument: MultimediaDocument
    {
        public int SampleRate
        {
            get { return Convert.ToInt16(this.GetProperty("samplerate")); }
            set { this.SetProperty("samplerate", value.ToString()); }
        }

        public AudioDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
