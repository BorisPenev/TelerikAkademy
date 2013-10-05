using System.Collections.Generic;
using System.Data.Entity;
using Musicstore.Server.Data.Helpers;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Data
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<dynamic>> GetCollections(this object o)
        {
            var result = new List<IEnumerable<dynamic>>();
            foreach (var prop in o.GetType().GetProperties())
            {
                if (typeof(IEnumerable<dynamic>).IsAssignableFrom(prop.PropertyType))
                {
                    var get = prop.GetGetMethod();
                    if (!get.IsStatic && get.GetParameters().Length == 0)
                    {
                        var enumerable = (IEnumerable<dynamic>)get.Invoke(o, null);
                        if (enumerable != null) result.Add(enumerable);
                    }
                }
            }
            return result;
        }

        //public static void ApplyStateChanges(this DbContext context)
        //{
        //    foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
        //    {
        //        IObjectWithState stateInfo = entry.Entity;
        //        entry.State = StateHelpers.ConvertState(stateInfo.State);
        //    }
        //}

        
    }
}
