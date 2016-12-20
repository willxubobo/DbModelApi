using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using log4net;
using log4net.Config;

namespace NET.Framework.Common.LogHelper
{
    public class Log4NetLogger : ILogger
    {
        #region Enum

        /// <summary>
        ///     Enum the Log Type.
        /// </summary>
        public enum LoggerType
        {
            /// <summary>
            ///     Record the system log.
            /// </summary>
            SystemLog,

            /// <summary>
            ///     Record the active log.
            /// </summary>
            ActivityLog,
        }

        #endregion

        #region Members

        private static bool _hasCreate;
        private readonly string _currentLoggerName = string.Empty;
        private readonly ILog _log;

        #endregion

        #region Constructor

        public Log4NetLogger(string name)
        {
            //Only configure the config file one time
            if (_hasCreate == false)
            {
                Configure();
                _hasCreate = true;
            }
            _currentLoggerName = name;
            _log = LogManager.GetLogger(name);
        }

        #endregion

        #region Configure Log4Net Configure File

        /// <summary>
        ///     Read default config
        /// </summary>
        public static void Configure()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        ///     Set configuration to log service.
        /// </summary>
        public static void Configure(string configXml)
        {
            if (string.IsNullOrEmpty(configXml)) return;
            var reader = new StringReader(configXml);
            var document = new XmlDocument();
            document.Load(reader);
            XmlElement logElement = document["log4net"];
            XmlConfigurator.Configure(logElement);
        }

        #endregion

        #region Debug

        /// <summary>
        ///     Log a debug message.
        /// </summary>
        public void Debug(string message)
        {
            if (IsDebugEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Debug(CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className,
                    message, null, null, null));
            }
        }

        #endregion

        #region Info

        /// <summary>
        ///     Log a Info message.
        /// </summary>
        public void Info(string message)
        {
            if (IsInfoEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Info(CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className,
                    message, null, null, null));
            }
        }

        #endregion

        #region Warn

        /// <summary>
        ///     Log a Warn message.
        /// </summary>
        public void Warn(string message)
        {
            if (IsWarnEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Warn(CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className,
                    message, null, null, null));
            }
        }

        #endregion

        #region Error

        /// <summary>
        ///     Log a Error message.
        /// </summary>
        public void Error(string message)
        {
            if (IsErrorEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Error(CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className,
                    message, null, null, null));
            }
        }

        /// <summary>
        ///     Log a Error message.
        /// </summary>
        public void Error(string message, Exception ex)
        {
            if (IsErrorEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Error(
                    CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className, message,
                        null, null, null), ex);
            }
        }

        #endregion

        #region Fatal

        /// <summary>
        ///     Log a Fatal message.
        /// </summary>
        public void Fatal(string message)
        {
            if (IsFatalEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Fatal(CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className,
                    message, null, null, null));
            }
        }

        /// <summary>
        ///     Log a Fatal message.
        /// </summary>
        public void Fatal(string message, Exception ex)
        {
            if (IsFatalEnabled())
            {
                var stackTrance = new StackTrace();
                StackFrame stackSelfFrame = stackTrance.GetFrame(0); //[0]self method; [1]call method
                string loggerLevel = stackSelfFrame.GetMethod().Name;
                StackFrame stackFrame = stackTrance.GetFrame(1);
                string methodName = stackFrame.GetMethod().Name;
                string className = stackFrame.GetMethod().ReflectedType.Name;

                _log.Fatal(
                    CreateMessage(LoggerType.SystemLog, _currentLoggerName, loggerLevel, methodName, className, message,
                        null, null, null), ex);
            }
        }

        #endregion

        #region Checkers

        public bool IsFatalEnabled()
        {
            return _log.IsFatalEnabled;
        }

        public bool IsErrorEnabled()
        {
            return _log.IsErrorEnabled;
        }

        public bool IsInfoEnabled()
        {
            return _log.IsInfoEnabled;
        }

        public bool IsWarnEnabled()
        {
            return _log.IsWarnEnabled;
        }

        public bool IsDebugEnabled()
        {
            return _log.IsDebugEnabled;
        }

        #endregion

        #region Private Function

        /// <summary>
        ///     CreateMessage
        /// </summary>
        /// <param name="loggerType">loggerType</param>
        /// <param name="loggerName">loggerName</param>
        /// <param name="loggerLevel">loggerLevel</param>
        /// <param name="methodName">methodName</param>
        /// <param name="className">className</param>
        /// <param name="finalMessage">finalMessage</param>
        /// <param name="dictionaryValues">dictionaryValues</param>
        /// <param name="paramValues">paramValues</param>
        /// <returns>Message</returns>
        private string CreateMessage(LoggerType loggerType, string loggerName, string loggerLevel, string methodName,
            string className, string finalMessage, Dictionary<string, string> dictionaryValues,
            ArrayList arraylistValues, params string[] paramValues)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendFormat("{0};", loggerType);
            logMessage.AppendFormat("{0};", loggerName);
            logMessage.AppendFormat("{0};", loggerLevel);
            logMessage.AppendFormat("{0};", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            logMessage.AppendFormat("{0};", className);
            logMessage.AppendFormat("{0};", methodName);
            if (finalMessage != null)
            {
                logMessage.AppendFormat("{0};", finalMessage.Replace(";", ","));
            }

            return logMessage.ToString();
        }

        #endregion
    }
}