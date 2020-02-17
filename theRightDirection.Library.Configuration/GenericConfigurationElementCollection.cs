using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace theRightDirection.Library.Configuration
{
    /// <summary>
    /// ConfigurationElement Collection which is generic. The elements need to inherit from BaseConfigurationElement
    /// </summary>
    /// <typeparam name="T">type for the collection which inherits from BaseConfigurationElement</typeparam>
    public class GenericConfigurationElementCollection<T> : ConfigurationElementCollection where T : ConfigurationElementBase, new()
    {
        #region Properties (5)

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection"/> which is always a "BasicMap"
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return new T().ElementName;
            }
        }

        public IEnumerable<T> Items
        {
            get
            {
                List<T> items = new List<T>();
                foreach (string key in Keys)
                {
                    items.Add(this[key]);
                }
                return items;
            }
        }

        /// <summary>
        /// Gets the keys of alle elements in de collection
        /// </summary>
        /// <value>The keys as a list of string</value>
        public List<string> Keys
        {
            get
            {
                object[] keys = BaseGetAllKeys();
                IEnumerable<string> elementKeys = keys.Cast<string>();
                return elementKeys.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified idx
        /// </summary>
        /// <value></value>
        public T this[int idx]
        {
            get { return (T)BaseGet(idx); }
            set
            {
                if (base.BaseGet(idx) != null) base.BaseRemoveAt(idx);
                this.BaseAdd(idx, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified key
        /// </summary>
        /// <value></value>
        new public T this[string key]
        {
            get { return (T)BaseGet(key); }
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (2) 

        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)(element)).ToString();
        }

        /// <summary>
        /// Adds the specified configuration element.
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        public void Add(ConfigurationElementBase configurationElement)
        {
            BaseAdd(configurationElement);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        #endregion Methods
    }
}