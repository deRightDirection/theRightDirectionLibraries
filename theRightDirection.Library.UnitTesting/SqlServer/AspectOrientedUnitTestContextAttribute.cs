using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.UnitTesting.SqlServer
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AspectOrientedUnitTestContextAttribute : ContextAttribute
    {
        #region Constructors

        /// <remarks />
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public AspectOrientedUnitTestContextAttribute()
            : base("AspectOrientedUnitTestContext")
        {
        }

        #endregion // Constructors

        #region Methods

        /// <remarks />
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override void GetPropertiesForNewContext(IConstructionCallMessage msg)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");
            msg.ContextProperties.Add(new SqlExecuteAspect());
        }

        #endregion // Methods
    }
}