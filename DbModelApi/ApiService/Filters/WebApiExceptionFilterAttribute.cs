using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using NET.Framework.Common.Enums;
using NET.Framework.Common.Exceptions;
using NET.Framework.Common.Models;
using NET.Framework.Common.SerializerHelper;

namespace ApiService.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is iSPException)
            {
                var ex = actionExecutedContext.Exception as iSPException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.OK);

                var result = new ApiResult();
                result.MessageType = MessageType.Failure;
                result.Data = ex.Message;

                actionExecutedContext.Response.Content = new StringContent(SerializerHelper.JasonEntitySerializer(result));

                return;
            }
            HttpException exHttp = actionExecutedContext.Exception as HttpException;
            string message = null;
            if (exHttp != null)
            {
                message = exHttp.Message;

                HttpStatusCode httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), exHttp.GetHttpCode().ToString());
                string errorContent = JsonConvert.SerializeObject(new ResponseError(exHttp.GetHttpCode(), message));

                throw new HttpResponseException(new HttpResponseMessage(httpStatusCode)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent(errorContent),
                    ReasonPhrase = httpStatusCode.ToString()
                });
            }

            //数据库字段验证异常处理
            DbEntityValidationException vex = actionExecutedContext.Exception as DbEntityValidationException;
            if (vex != null)
            {
                //获取到所有EntityFramework验证的数据库字段异常错误信息
                StringBuilder sb = new StringBuilder();
                foreach (DbEntityValidationResult result in vex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                }

                string errorContent = JsonConvert.SerializeObject(new ResponseError(HttpStatusCode.BadRequest.GetHashCode(), sb.ToString()));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent(errorContent),
                    ReasonPhrase = HttpStatusCode.BadRequest.ToString()
                });
            }

            UnauthorizedAccessException unauth = actionExecutedContext.Exception as UnauthorizedAccessException;
            if (unauth != null)
            {
                message = unauth.Message.Substring(unauth.Message.IndexOf(']') + 1).Trim();

                string errorContent = JsonConvert.SerializeObject(new ResponseError(401, message));

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent(errorContent),
                    ReasonPhrase = HttpStatusCode.Unauthorized.ToString()
                });
            }

            ExecutionEngineException workflow = actionExecutedContext.Exception as ExecutionEngineException;
            if (workflow != null)
            {
                message = workflow.Message.Substring(workflow.Message.IndexOf(']') + 1).Trim();
                string errorContent = JsonConvert.SerializeObject(new ResponseError(303, message));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.SeeOther)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent(errorContent),
                    ReasonPhrase = HttpStatusCode.SeeOther.ToString()
                });
            }

            //一般异常的处理                
            message = actionExecutedContext.Exception.Message;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(message),
                ReasonPhrase = "InternalServerError"
            });
        }
    }
    public class ResponseError
    {
        public ResponseError(int httpStatusCode, string message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.Message = message;
        }

        /// <summary>
        /// HTTP状态代码
        /// </summary>
        public int HttpStatusCode { get; set; }
        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        public string Message { get; set; }
    }

    public class WorkflowStartFaildException : SystemException
    {
        public WorkflowStartFaildException(string message)
        {
            this.Message = message;
        }
        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        public string Message { get; set; }
    }
}