using NET.Framework.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.Framework.Common.Models
{
    public class ApiResult
    {
        public MessageType MessageType { get; set; }

        public object Data { get; set; }
    }
}
