// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace HiddenTruth.Library.Obelisk
{
    public class KnownTypes
    {
        internal static Type[] GetKnownTypes(Assembly[] assemblies)
        {
            var knownTypes = new List<Type>();

            // Examine each assembly
            foreach (Assembly a in assemblies)
            {
#if WINDOWS8
                IEnumerable<TypeInfo> types = a.DefinedTypes;
                foreach (TypeInfo ti in types)
                {
                    IEnumerable<PropertyInfo> pi = ti.DeclaredProperties;
#else
                Type[] types = a.GetTypes();
                foreach (Type t in types)
                {
                    IEnumerable<PropertyInfo> pi = t.GetProperties();
#endif
                    // Examine each property
                    foreach (PropertyInfo p in pi)
                    {
                        // Add any properties with Persist attribute
                        object[] atts = null;
#if WINDOWS8
                        atts = p.GetCustomAttributes(typeof(Persist), true).ToArray();
#else
                        atts = p.GetCustomAttributes(typeof(Persist), true);
#endif
                        if (atts != null && atts.Length > 0 && (atts[0] as Persist).PersistMode != PersistMode.None)
                        {
                            // Add property type
                            if (!knownTypes.Contains(p.PropertyType))
                                knownTypes.Add(p.PropertyType);

                            // If type is an array add it's element type
                            if (p.PropertyType.HasElementType)
                            {
                                Type et = p.PropertyType.GetElementType();

                                if (!knownTypes.Contains(et))
                                    knownTypes.Add(et);
                            }
                        }
                    }
                }
            }

            return knownTypes.ToArray();
        }
    }
}
