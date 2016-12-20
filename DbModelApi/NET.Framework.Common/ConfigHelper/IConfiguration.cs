using System.Collections.Generic;

namespace NET.Framework.Common.ConfigHelper
{
    public interface IConfiguration
    {
        Dictionary<string, string> AllConfigs { get; }
        string GetValue(string key);
    }
}