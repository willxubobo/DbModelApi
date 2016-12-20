using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NET.Framework.Common.Models
{
    public class DataTables
    {
        /// <summary>
        /// 客户端传来的值
        /// </summary>
        public string sEcho {get;set;}
        /// <summary>
        /// 筛选后总条数
        /// </summary>
        public int iTotalRecords { get; set; }
       /// <summary>
       /// 总条数
       /// </summary>
        public int iTotalDisplayRecords{get;set;}
        /// <summary>
        /// 返回数据
        /// </summary>
        public Object aaData { get; set; }
    }
}
