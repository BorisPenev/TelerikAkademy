using System;

namespace AdvancedDataBinding.PhoneStore.Models
{
    public class PhoneFeaturesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PhoneFeaturesModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
