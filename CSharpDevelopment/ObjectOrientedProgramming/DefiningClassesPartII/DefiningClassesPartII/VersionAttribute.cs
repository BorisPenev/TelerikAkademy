using System;
using System.Linq;

namespace DefiningClassesPartII
{
    [AttributeUsage(AttributeTargets.Struct |
    AttributeTargets.Class |
    AttributeTargets.Interface |
    AttributeTargets.Enum |
    AttributeTargets.Method)]
    public sealed class VersionAttribute : Attribute
    {
        private readonly string version = string.Empty;
        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public VersionAttribute(string version)
        {
            this.version = version;
        }
    }
}
