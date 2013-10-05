using System;
using System.Xml.Serialization;

namespace CommandBinding.ImageGallery.Models
{
    public class ImageModel
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("imageSource")]
        public string ImageSource { get; set; }

        public ImageModel()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
