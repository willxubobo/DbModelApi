using System;
using System.Collections.Generic;
using System.Configuration;
using NET.Framework.Common.Extensions;

namespace NET.Framework.Common.ConfigHelper
{
    public class AppConfig : IConfiguration
    {
        private static Dictionary<string, string> _allConfigs;
        private const string RepeatKey = "AppSettings包含重复Key";

        public Dictionary<string, string> AllConfigs
        {
            get
            {
                if (_allConfigs == null)
                {
                    _allConfigs = new Dictionary<string, string>();
                    foreach (string key in ConfigurationManager.AppSettings)
                    {
                        if (!_allConfigs.ContainsKey(key))
                        {
                            _allConfigs.Add(key, ConfigurationManager.AppSettings[key]);
                        }
                        else
                        {
                            throw new Exception(RepeatKey);
                        }
                    }
                }
                return _allConfigs;
            }
        }

        public string GetValue(string key)
        {
            key.CheckNotNullOrEmpty("key");
            return AllConfigs.ContainsKey(key) ? AllConfigs[key] : "";
        }
    }
}