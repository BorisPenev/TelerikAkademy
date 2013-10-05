using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight.Command;

namespace TheHiddenTruth.Library.Model
{
    [DataContract]
    public class SiteModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int SelectedIndex { get; set; }
        [DataMember]
        public ObservableCollection<ItemModel> Items { get; set; }
        [DataMember]
        public ObservableCollection<PageModel> Pages { get; set; } 

        public SiteModel()
        {
            Id = Guid.NewGuid().ToString();
            SelectedIndex = 0;
            Items = new ObservableCollection<ItemModel>();
            Pages = new ObservableCollection<PageModel>();
        }
    }
}
