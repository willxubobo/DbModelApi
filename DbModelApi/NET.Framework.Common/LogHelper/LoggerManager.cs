namespace NET.Framework.Common.LogHelper
{
    public class LoggerManager
    {
        public static LoggerManager Instance;

        /// <summary>
        ///     Create a new logger instance.
        /// </summary>
        /// <returns>Return the instance of the Logger by creating the Logger.</returns>
        public static LoggerManager GetInstance()
        {
            if (null == Instance)
            {
                lock (typeof (LoggerManager))
                {
                    if (null == Instance)
                    {
                        Instance = new LoggerManager();
                    }
                }
            }

            return Instance;
        }

        /// <summary>
        ///     Create the instance of the Logger by the Logger Type and Logger Category
        /// </summary>
        /// <param name="name">The name of logger.</param>
        /// <returns>Return the instance of the Logger by the Logger type and Logger Category.</returns>
        public static ILogger GetLogger(string name)
        {
            return new Log4NetLogger(name);
        }
    }
}