// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;

namespace HiddenTruth.Library.Obelisk
{
    public enum PersistMode
    {
        None,
        TombstonedOnly,
        ClosedOnly,
        Both
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Persist : Attribute
    {
        public readonly PersistMode PersistMode = PersistMode.TombstonedOnly;

        /// <summary>
        /// Default is true
        /// </summary> 
        public Persist()
        {

        }

        public Persist(PersistMode mode)
        {
            this.PersistMode = mode;
        }
    }
}
