namespace NET.Framework.Common.ValidHelper
{
    public static class Validator
    {
        /// <summary>
        ///     验证指定值的断言<paramref name="assertion" />是否为真，如果不为真，返回指定消息<paramref name="message" />
        /// </summary>
        /// <param name="assertion">要验证的断言。</param>
        /// <param name="message">异常消息。</param>
        public static string Valid(string message, bool assertion)
        {
            if (assertion)
            {
                return string.Empty;
            }
            return message;
        }
    }
}