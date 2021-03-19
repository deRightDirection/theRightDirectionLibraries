using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public class EnumHelperException : Exception
    {
        public EnumHelperException(string errormessage) : base(errormessage)
        {
        }
    }
}