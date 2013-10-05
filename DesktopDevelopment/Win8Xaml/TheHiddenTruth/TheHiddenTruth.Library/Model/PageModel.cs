using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model
{
    [DataContract]
    public class PageModel
    {
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public string CurrentPageToken { get; set; }
        [DataMember]
        public string NextPageToken { get; set; }
        [DataMember]
        public string PrevPageToken { get; set; }
        [DataMember]
        public ObservableCollection<ItemModel> Items { get; set; }

        public PageModel()
        {
            Items = new ObservableCollection<ItemModel>();
        }
    }
}
