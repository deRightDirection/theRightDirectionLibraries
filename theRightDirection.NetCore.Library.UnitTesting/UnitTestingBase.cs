using Microsoft.Extensions.Configuration;
using System;

namespace theRightDirection.UnitTesting
{
    public class UnitTestingBase
    {
        private IConfiguration _configuration;
        public IConfiguration GetConfiguration()
        {
            var x = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json");
            if (_configuration == null)
            {
                _configuration = x.Build();
            }
            return _configuration;
        }
    }
}