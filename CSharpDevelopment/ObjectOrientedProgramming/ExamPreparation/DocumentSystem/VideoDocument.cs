using System;
using System.Linq;

namespace DocumentSystem
{
    class VideoDocument:MultimediaDocument
    {
        public int FrameRate
        {
            get { return Convert.ToInt16(this.GetProperty("framerate")); }
            set { this.SetProperty("framerate", value.ToString()); }
        }

        public VideoDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
