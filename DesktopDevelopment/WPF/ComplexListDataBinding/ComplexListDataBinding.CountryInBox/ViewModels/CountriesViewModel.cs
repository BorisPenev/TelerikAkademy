using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ComplexListDataBinding.Models;

namespace ComplexListDataBinding.CountryInBox.ViewModels
{
    public class CountriesViewModel
    {

        private readonly List<Country> _countries = new List<Country>();
        public List<Country> Countries
        {
            get
            {
                if (_countries.Count == 0)
                {
                    _countries.Add(new Country()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bulgaria",
                        Flag = "http://th06.deviantart.net/fs48/PRE/i/2009/194/8/1/Bulgary_Grunge_Flag_by_think0.jpg",
                        Language = "Bulgarian",
                        Cities = new ObservableCollection<City>()
                        {
                            new City()
                            {
                                Id = 1,
                                Name = "Ruse",
                                Population = 149642
                            },
                            new City()
                            {
                                Id = 2,
                                Name = "Burgas",
                                Population = 200271
                            },
                        }
                    });
                    _countries.Add(new Country()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Greece",
                        Flag = "http://www.crwflags.com/fotw/images/g/gr.gif",
                        Language = "Greek",
                        Cities = new ObservableCollection<City>()
                        {
                            new City()
                            {
                                Id = 3,
                                Name = "Thessaloniki",
                                Population = 385406
                            },
                            new City()
                            {
                                Id = 4,
                                Name = "Rhodes",
                                Population = 80000
                            },
                        }
                    });
                }
                return _countries;
            }
        }

        public void Next()
        {
            var collection = this.GetDefaultView(this._countries);
            collection.MoveCurrentToNext();
            if (collection.IsCurrentAfterLast)
            {
                collection.MoveCurrentToLast();
            }
        }

        public void Prev()
        {
            var collection = this.GetDefaultView(this._countries);
            collection.MoveCurrentToPrevious();
            if (collection.IsCurrentBeforeFirst)
            {
                collection.MoveCurrentToFirst();
            }
        }

        private ICollectionView GetDefaultView<T>(IEnumerable<T> collection)
        {
            return CollectionViewSource.GetDefaultView(collection);
        }

    }
}
