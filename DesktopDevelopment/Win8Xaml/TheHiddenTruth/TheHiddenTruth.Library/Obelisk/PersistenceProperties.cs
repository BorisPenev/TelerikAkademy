// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace HiddenTruth.Library.Obelisk
{
    /// <summary>
    /// This class is used to store object properties marked with the Persist attribute
    /// </summary>
    public class PersistenceProperties
    {
        public string UniqueName { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }

        public PersistenceProperties()
        {

        }

        public PersistenceProperties(string uniqueName, string className, string propertyName, object value)
        {
            this.UniqueName = uniqueName;
            this.ClassName = className;
            this.PropertyName = propertyName;
            this.Value = value;
        }
    }
}
