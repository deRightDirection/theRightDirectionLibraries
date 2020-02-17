using System.Configuration;

namespace theRightDirection.Library.Configuration
{
    /// <summary>
    /// Base class for creating own configuration elements. The field 'key' is already added to the configuration element
    /// The property 'ElementName' needs to be overriden. This property determines the name of the configuration element
    /// <example><configurationelement key='a'/></example>
    /// 'configurationelement' is the value for the property 'ElementName'
    /// </summary>
    public abstract class ConfigurationElementBase : ConfigurationElement
    {
        #region Properties (2)

        public abstract string ElementName { get; }

        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get { return this["key"] as string; }
            set { this["key"] = value; }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        public override string ToString()
        {
            return Key;
        }

        #endregion Methods
    }
}