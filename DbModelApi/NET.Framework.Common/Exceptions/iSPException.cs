using NET.Framework.Common.Enums;
using System;

namespace NET.Framework.Common.Exceptions
{
    public class iSPException : Exception
    {
        /// <summary>
        ///     使用指定错误消息初始化<see cref="iSPException" />类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public iSPException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     使用异常消息与一个内部异常实例化一个<see cref="iSPException" />类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在<see cref="iSPException" />内部的异常实例</param>
        public iSPException(string message, Exception inner)
            : base(message, inner)
        {
        }


        public iSPException(MessageType type, string message, Exception inner)
            : base(type+message, inner)
        {
        }
    }
}