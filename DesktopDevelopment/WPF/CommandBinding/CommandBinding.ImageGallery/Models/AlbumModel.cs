using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CommandBinding.ImageGallery.Models
{
    [XmlRoot("album")]
    public class AlbumModel
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlElement("imageModels")]
        public ObservableCollection<ImageModel> ImageModels { get; set; }

        public AlbumModel()
        {
            Id = Guid.NewGuid();
            ImageModels = new ObservableCollection<ImageModel>();
        }
    }
}
