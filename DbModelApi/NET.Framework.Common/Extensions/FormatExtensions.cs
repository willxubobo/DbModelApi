using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NET.Framework.Common.Extensions
{
    public static class FormatExtensions
    {
        /// <summary>
        ///     支持汉字的字符串长度，汉字长度计为2
        /// </summary>
        /// <param name="value">参数字符串</param>
        /// <returns>当前字符串的长度，汉字长度为2</returns>
        public static int ExTextLength(this string value)
        {
            var ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] bytes = ascii.GetBytes(value);
            foreach (byte b in bytes)
            {
                if (b == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }

        /// <summary>
        ///     将JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">要转换的目标类型</typeparam>
        /// <param name="json">JSON字符串 </param>
        /// <returns></returns>
        public static T ExToModel<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        ///     Url拼接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ExContactUrl(this string url, params string[] values)
        {
            string result = url.TrimEnd('/');
            return values.Aggregate(result, (current, value) => current + (@"/" + value.TrimStart('/')));
        }

        /// <summary>
        ///     Object转化为String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ExToString(this object value)
        {
            return value == null ? "" : Convert.ToString(value);
        }

        /// <summary>
        ///     Object转化为String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ExToInt(this string value)
        {
            return value.ExIsEmpty() ? 0 : Convert.ToInt32(value);
        }

        /// <summary>
        ///     Object转化为String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ExToInt(this object value)
        {
            return value.ExIsEmpty() ? 0 : Convert.ToInt32(value);
        }


        public static bool ExToBool(this object value)
        {
            string val = value.ExToString();
            return !val.ExIsEmpty() && val.ToLower() != "false" && val.ExToInt() > 0;
        }

        /// <summary>
        ///     Object转化为Double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ExToDouble(this string value)
        {
            return value.ExIsEmpty() ? 0 : Convert.ToDouble(value);
        }

        /// <summary>
        ///     Object转化为Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ExToDecimal(this string value)
        {
            return value.ExIsEmpty() ? 0 : Convert.ToDecimal(value);
        }

        public static decimal ExToDecimal(this object value)
        {
            return value.ExIsEmpty() ? 0 : Convert.ToDecimal(value);
        }

        /// <summary>
        ///     Object转化为DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ExToDateTime(this string value)
        {
            return value.ExIsEmpty() ? DateTime.MinValue : Convert.ToDateTime(value);
        }

        /// <summary>
        ///     转化为时间默认格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ExDefaultDateTimeFormat(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///     转化为日期默认格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ExDefaultDateFormat(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        public static bool ExEqual(this object obj, string s)
        {
            return obj.ExToString() == s;
        }
    }
}