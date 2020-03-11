using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.Interfaces
{
    public interface INetworkInterface
    {
        bool IsNetworkAvailable(long minimumSpeed = 0);
    }
}