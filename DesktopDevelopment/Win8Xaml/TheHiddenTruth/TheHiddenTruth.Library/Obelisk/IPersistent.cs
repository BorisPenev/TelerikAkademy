// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace HiddenTruth.Library.Obelisk
{
    public interface IPersistent
    {
        /// <summary>
        /// Occurs after a class has been instantiated for the first time
        /// </summary>
        void OnLaunched();

        /// <summary>
        /// Occurs after an instance has been rehydrated after an activation
        /// </summary>
        void OnActivated(bool preserved);

        /// <summary>
        /// Occurs before an instance is dehydrated the tombstoned
        /// </summary>
        void OnDeactivated();

        /// <summary>
        /// Occurs when the application is closing
        /// </summary>
        void OnClosing();
    }
}
