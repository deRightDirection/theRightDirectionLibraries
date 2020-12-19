using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcludeFromCopyPropertyAttribute : Attribute
    {
    }
}