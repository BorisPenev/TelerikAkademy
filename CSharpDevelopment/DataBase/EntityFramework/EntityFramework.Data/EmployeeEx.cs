using System.Data.Linq;

namespace EntityFramework.Data
{
    public partial class Employee
    {
        public EntitySet<Territory> TerritoriesSet
        {
            get
            {
                var territoriesSet = new EntitySet<Territory>();
                territoriesSet.AddRange(this.Territories);
                return territoriesSet;
            }
        }
    }
}
