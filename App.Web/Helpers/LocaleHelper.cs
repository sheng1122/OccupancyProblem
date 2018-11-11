using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace App.Web.Helpers
{
    public class LocaleHelper
    {
        private readonly Dictionary<string, IStringLocalizer> _localizers = new Dictionary<string, IStringLocalizer>();
        private readonly IStringLocalizerFactory _factory;

        public LocaleHelper(IStringLocalizerFactory factory)
        {
            _factory = factory;
        }

        public IStringLocalizer GetLocalizer(string resourceKey = "")
        {
            resourceKey = resourceKey.ToLower();

            if (_localizers.ContainsKey(resourceKey) == false)
            {
                _localizers[resourceKey] = _factory.Create(resourceKey, "");
            }

            return _localizers[resourceKey];
        }
    }
}
