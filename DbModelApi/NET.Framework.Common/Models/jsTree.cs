using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.Framework.Common.Models
{
    public class jsTree
    {
        //显示名
        public string text { get; set; }
        //Class名或Icon地址
        public string icon { get; set; }
        //其他属性
        public liAttribute li_attr { get; set; }
        //子节点
        public jsTree[] children { get; set; }
    }
    public class liAttribute
    {
        public string id { get; set; }
        public string type { get; set; }
    }
}
