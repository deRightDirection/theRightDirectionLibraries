﻿using System;

namespace theRightDirection.Attributes
{
    // TODO: overzetten en samenvoegen met WinUX.Common
    /// <summary>
    /// Code from: http://www.ageektrapped.com/blog/the-missing-net-7-displaying-enums-in-wpf/
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DisplayStringAttribute : Attribute
    {
        private readonly string value;

        public string Value
        {
            get { return value; }
        }

        public string ResourceKey { get; set; }

        public DisplayStringAttribute(string v)
        {
            value = v;
        }

        public DisplayStringAttribute()
        {
        }
    }
}