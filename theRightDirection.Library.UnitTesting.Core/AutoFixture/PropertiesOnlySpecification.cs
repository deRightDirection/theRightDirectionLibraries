using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Kernel;

namespace theRightDirection.Library.UnitTesting.AutoFixture
{
    internal sealed class PropertiesOnlySpecification : IRequestSpecification
    {
        public bool IsSatisfiedBy(object request)
        {
            return request is PropertyInfo;
        }
    }
}