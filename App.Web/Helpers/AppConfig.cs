using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace App.Web.Helpers
{

    public class AppConfig
    {
        private static IServiceProvider _services;
        private const string UnexpectedServiceResponse = "Unexpected service response";
        private static readonly string _configFile = "appsettings.json";
        private static IConfigurationRoot _config;
        private static IDistributedCache _cache;

        public static IServiceProvider Service
        {
            get { return _services; }
            set
            {
                if (_services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }

                _services = value;

                _cache = Service.GetService(typeof(IDistributedCache)) as IDistributedCache;
            }

        }

        private static IConfigurationRoot Config
        {
            get
            {
                if (_config == null)
                {
                    var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile(_configFile);

                    _config = builder.Build();
                }

                return _config;
            }
        }

        #region info from memory
        public static Exception LastException { get; set; }
        #endregion
    }
}
