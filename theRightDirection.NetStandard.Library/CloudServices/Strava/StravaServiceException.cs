using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.CloudServices.Strava
{
    public class StravaServiceException : Exception
    {
        public StravaServiceException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
