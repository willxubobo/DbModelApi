using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace NET.Framework.Common.SerializerHelper
{
    /// <summary>
    /// 序列化对象帮助类
    /// </summary>
    public class SerializerHelper
    {
        ///<summary>
        ///把对象序列化为XML
        ///</summary>
        ///<typeparam name="T">对象类型</typeparam>
        ///<param name="t">要序列化的对象</param>
        ///<returns>XML内容</returns>
        public static string XMLSerialize<T>(T t)
        {
            string context;
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xs.Serialize(ms, t, ns);
                context = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);//256位，没有达到长度时，后面默认加\0，取实际长度；
            }
            catch
            {
                throw;
            }
            finally
            {
                ms.Close();
            }

            return context;
        }
        /// <summary>
        /// 把对象XML反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="context">XML内容</param>
        /// <returns>对象</returns>
        public static T DeXMLSerialize<T>(string context)
        {
            T t;
            //针对utf-16编码的反序列化
            if (context.Contains("encoding=\"utf-16\""))
            {
                using (TextReader reader = new StringReader(context))
                {
                    XmlSerializer xmlSerial = new XmlSerializer(typeof(T));
                    t = (T)xmlSerial.Deserialize(reader);
                }
            }
            else
            {                
                byte[] bytes = Encoding.UTF8.GetBytes(context);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(bytes);
                try
                {
                    t = (T)xs.Deserialize(ms);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    ms.Close();
                }
            }
            return t;
        }

        /// <summary>
        ///   把对象序列化为string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, obj);
            return Convert.ToBase64String(stream.GetBuffer());

        }

        /// <summary>
        /// 把对象string反序列化为对象
        /// </summary>
        /// <param name="str">对象string</param>
        /// <returns></returns>
        public static object DeSerialize(string str)
        {
            Stream stream = GetStream(str);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }

        /// <summary>
        /// 获取MemoryStream
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static MemoryStream GGetStream(string str)
        {
            byte[] strbyte = Convert.FromBase64String(str);
            //byte[] strbyte = System.Text.Encoding.Default.GetBytes(str);
            MemoryStream stream = new MemoryStream(strbyte);
            return stream;
        }

        /// <summary> 
        /// 获取string
        /// </summary>
        /// <param name="stream">MemoryStream</param>
        /// <returns></returns>
        public static string GGetString(MemoryStream stream)
        {
            string Result = System.Text.Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
            return Result;
        }

        private static string GetString(MemoryStream stream)
        {
            return Convert.ToBase64String(stream.GetBuffer());
        }

        private static MemoryStream GetStream(string str)
        {
            byte[] strbyte = Convert.FromBase64String(str);
            //byte[] strbyte = System.Text.Encoding.UTF8.GetBytes(str);
            MemoryStream stream = new MemoryStream(strbyte);
            return stream;
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T entity)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(entity);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonString);
        }

        public  static string DataTableToJson(object dt)
        {
            return JsonConvert.SerializeObject(dt, new DataTableConverter());
        }

        public static string JasonEntitySerializer<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, Formatting.Indented);
            
        }


        public  T JasonDeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string ObjectToJson<T>(T obj)
        {
            var dateTimeFormat = new IsoDateTimeConverter();
            dateTimeFormat.DateTimeFormat = "MM/dd/yyyy";

            return JsonConvert.SerializeObject(obj, new DataTableConverter(), dateTimeFormat);
        }
    }
}
