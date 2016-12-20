using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NET.Framework.Common.Enums
{
    public enum MessageType
    {
        Failure=0,
        Success = 1,
        Warning = 2,
        Notice = 3
    }

    public enum ApproveStatus
    {
        [Description("审批通过")]
        Approved = 0,
        [Description("已作废")]
        Obsoleted = 1,
        [Description("退回修改")]
        Back = 2,
    }

    public class CommonHelper
    {
        /// <summary>
        /// 获取枚举属性
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }
}
