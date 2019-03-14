using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcludeFromCopyPropertyAttribute : Attribute
    {
    }
}