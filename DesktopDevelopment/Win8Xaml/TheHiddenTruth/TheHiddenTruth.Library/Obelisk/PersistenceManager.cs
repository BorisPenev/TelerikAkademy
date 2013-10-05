// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

#if WINDOWS8
using Windows.ApplicationModel.Activation;
using System.Threading.Tasks;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace HiddenTruth.Library.Obelisk
{
    public enum ApplicationMode
    {
        Unknown,
        Launching,
        Activated,
        Deactivated,
        Closing
    }

    public sealed class PersistenceManager
    {
        private readonly Dictionary<string, object> _persistentObjects = new Dictionary<string, object>();
        private List<PersistenceProperties> _rehydratedProperties = null;
        private readonly object _synchLock = new object();
        private ApplicationMode _mode = ApplicationMode.Unknown;
        private const string _persistenceProperties = "PersistenceProperties";
        private bool _appReady = false;

        internal List<PersistenceProperties> RehydratedProperties
        {
            get { return this._rehydratedProperties; }
        }

        // Singleton
        public static readonly PersistenceManager _instance = new PersistenceManager();

        public static PersistenceManager Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Internal constructor used for unit testing instances
        /// </summary>
        internal PersistenceManager()
        {

        }

        /// <summary>
        /// Attach with assemblies containing known types to serialise
        /// </summary>
        /// <param name="a"></param>
        public void AttachPersistenceManager(Assembly[] a)
        {
            Type[] knownTypes = KnownTypes.GetKnownTypes(a);

            this.AttachPersistenceManager(knownTypes);
        }

        /// <summary>
        /// Attach with known types to serialise
        /// </summary>
        /// <param name="knownTypes"></param>
        public void AttachPersistenceManager(Type[] knownTypes)
        {
            PersistenceStorage.KnownTypes = knownTypes;
        }

#if WINDOWS8

        public async Task OnLaunching()
        {
            this._mode = ApplicationMode.Launching;

            this._appReady = true;

            // Load properties
            await this.GetPropertiesFromStorage();

            // Initialise regiestered
            this.InitialiseRegistered(false);
        }

        public async Task OnActivated(bool preserved)
        {
            this._mode = ApplicationMode.Activated;

            this._appReady = true;

            // If app instance is not preserved load names so we know whether instances need rehydrating or not
            if (!preserved)
            {
                await this.GetPropertiesFromStorage();
            }

            // Initialise regiestered
            this.InitialiseRegistered(preserved);
        }

        public async Task OnDeactivated()
        {
            this._mode = ApplicationMode.Deactivated;

            // Dehydrate all objects
            await DehydrateObjects();
        }

        public async Task OnClosing()
        {
            this._mode = ApplicationMode.Closing;

            // Dehydrate all objects
            await DehydrateObjects();
        }
#else
        public void OnLaunching()
        {
            this._mode = ApplicationMode.Launching;

            this._appReady = true;

            // Load properties
            this.GetPropertiesFromStorage();

            // Initialise regiestered
            this.InitialiseRegistered(false);
        }

        public void OnActivated(bool preserved)
        {
            this._mode = ApplicationMode.Activated;

            this._appReady = true;

            // If app instance is not preserved load names so we know whether instances need rehydrating or not
            if (!preserved)
            {
                this.GetPropertiesFromStorage();                
            }

            // Initialise regiestered
            this.InitialiseRegistered(preserved);
        }

        public void OnDeactivated()
        {
            this._mode = ApplicationMode.Deactivated;

            // Dehydrate all objects
            DehydrateObjects();
        }

        public void OnClosing()
        {
            this._mode = ApplicationMode.Closing;

            // Dehydrate all objects
            DehydrateObjects();
        }
#endif

        /// <summary>
        /// Register IPersistent for monitoring
        /// </summary>
        /// <param name="persistent"></param>
        public void Register(string uniqueName, object o)
        {
            lock (_synchLock)
            {
                // New instantiation
                if (!_persistentObjects.ContainsKey(uniqueName))
                {
                    _persistentObjects.Add(uniqueName, o);

                    // Notify object to initialise if the app is ready
                    if (o is IPersistent && _appReady)
                    {
                        if (_mode == ApplicationMode.Launching)
                        {
                            (o as IPersistent).OnLaunched();
                        }
                        else if (_mode == ApplicationMode.Activated)
                        {
                            (o as IPersistent).OnActivated(false);
                        }
                    }
                }
                else // Rehydration
                {
                    _persistentObjects[uniqueName] = o;

                    Rehydrate(uniqueName, o);
                }
            }
        }

        public void UnRegister(string uniqueName)
        {
            lock (_synchLock)
            {
                if (_persistentObjects.ContainsKey(uniqueName))
                {
                    _persistentObjects.Remove(uniqueName);
                }
            }
        }

        private void InitialiseRegistered(bool preserved = false)
        {
            if (_persistentObjects != null)
            {
                foreach (object o in _persistentObjects.Values)
                {
                    // Notify object to initialise
                    if (o is IPersistent)
                    {
                        if (_mode == ApplicationMode.Launching)
                        {
                            (o as IPersistent).OnLaunched();
                        }
                        else if (_mode == ApplicationMode.Activated)
                        {
                            (o as IPersistent).OnActivated(preserved);
                        }
                    }
                }
            }
        }

#if WINDOWS8
        private async Task GetPropertiesFromStorage()
        {
            // Get properties from storage
            _rehydratedProperties = await PersistenceStorage.GetObjectFromIsolated<List<PersistenceProperties>>(_persistenceProperties);

            LoadProperties();
        }
#else
        private void GetPropertiesFromStorage()
        {
            // Get properties from storage
            _rehydratedProperties = PersistenceStorage.GetObjectFromIsolated<List<PersistenceProperties>>(_persistenceProperties);

            LoadProperties();
        }
#endif

        private void LoadProperties()
        {
            lock (_synchLock)
            {
                if (_rehydratedProperties != null)
                {
                    _rehydratedProperties.TrimExcess();

                    // Add object place holders to indicate they need re-hydrating once they are instantiated and registered
                    foreach (string name in _rehydratedProperties.Select(p => p.UniqueName).Distinct())
                    {
                        if (!_persistentObjects.ContainsKey(name))
                        {
                            _persistentObjects.Add(name, null);
                        }
                        else
                        {
                            // Rehydrate
                            Rehydrate(name, _persistentObjects[name]);
                        }
                    }
                }
            }
        }

        private void CloseObjects()
        {
            foreach (object o in _persistentObjects.Values)
            {
                // Notify object to initialise
                if (o is IPersistent)
                    (o as IPersistent).OnClosing();
            }
        }

#if WINDOWS8
        private async Task DehydrateObjects()
#else
        private void DehydrateObjects()
#endif
        {
            List<PersistenceProperties> dehydrated = new List<PersistenceProperties>();

            // Examine each object
            foreach (KeyValuePair<string, object> obj in _persistentObjects)
            {
                if (obj.Value != null)
                {
                    // Notify object that it is about to be dehydrated
                    if (obj.Value is IPersistent && _mode == ApplicationMode.Deactivated)
                    {
                        (obj.Value as IPersistent).OnDeactivated();
                    }
                    else if (obj.Value is IPersistent && _mode == ApplicationMode.Closing)
                    {
                        (obj.Value as IPersistent).OnClosing();
                    }

                    IEnumerable<PropertyInfo> pi = null;

#if WINDOWS8
                    pi = obj.Value.GetType().GetTypeInfo().DeclaredProperties;
#else
                    pi = obj.Value.GetType().GetProperties();
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

                        if (atts != null && atts.Length > 0)
                        {
                            PersistMode mode = (atts[0] as Persist).PersistMode;
                            if ((this._mode == ApplicationMode.Deactivated &&
                                (mode == PersistMode.TombstonedOnly || mode == PersistMode.Both)) ||
                                (this._mode == ApplicationMode.Closing &&
                                (mode == PersistMode.ClosedOnly || mode == PersistMode.Both)))
                            {
                                var tsp = new PersistenceProperties(obj.Key, obj.Value.GetType().Name, p.Name, p.GetValue(obj.Value, null));

                                dehydrated.Add(tsp);
                            }
                        }
                    }
                }
            }

            dehydrated.TrimExcess();

#if WINDOWS8
            await PersistenceStorage.StoreObjectToIsolated<List<PersistenceProperties>>(_persistenceProperties, dehydrated, true);
#else
            PersistenceStorage.StoreObjectToIsolated<List<PersistenceProperties>>(_persistenceProperties, dehydrated, true);
#endif
        }

        private void Rehydrate(string uniqueName, object o)
        {
            try
            {
                IEnumerable<PropertyInfo> pi = null;

#if WINDOWS8
                pi = o.GetType().GetTypeInfo().DeclaredProperties;
#else
                pi = o.GetType().GetProperties();
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
                    if (atts != null && atts.Length > 0)
                    {
                        PersistMode mode = (atts[0] as Persist).PersistMode;
                        if ((this._mode == ApplicationMode.Activated &&
                            (mode == PersistMode.TombstonedOnly || mode == PersistMode.Both)) ||
                            (this._mode == ApplicationMode.Launching &&
                            (mode == PersistMode.ClosedOnly || mode == PersistMode.Both)))
                        {

                            // Get value
                            object value = _rehydratedProperties.Single(r => r.UniqueName == uniqueName &&
                            r.ClassName == o.GetType().Name && r.PropertyName == p.Name).Value;

                            // Assign value
                            p.SetValue(o, value, null);
                        }
                    }
                }

                // Notify object that it has been hydrated
                if (o is IPersistent)
                {
                    if (this._mode == ApplicationMode.Activated)
                    {
                        (o as IPersistent).OnActivated(false);
                    }
                    else if (this._mode == ApplicationMode.Launching)
                    {
                        (o as IPersistent).OnLaunched();
                    }
                }
            }
            catch (Exception)
            {
                // If anything has gone wrong notify the object to initialise normally
                if (o is IPersistent)
                    (o as IPersistent).OnLaunched();
            }
        }
    }
}
