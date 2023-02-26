using System;

namespace theRightDirection.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcludeFromCopyPropertyAttribute : Attribute
    {
    }
}