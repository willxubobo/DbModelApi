using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbModelApi.BLL;
using NET.Framework.Common.Enums;
using NET.Framework.Common.Models;

namespace DbModelApi.Controllers
{
    [RoutePrefix("api")]
    public class TransferController : BaseController
    {
        [HttpGet]
        [Route("transfers")]
        public IHttpActionResult Get()
        {
            TransferManagementBLL com = new TransferManagementBLL();
            string TransferType = GetPara("TransferType");
            string ProcessStatus = GetPara("ProcessStatus");
            string sDate = GetPara("startTime");
            string eDate = GetPara("endTime");
            string factorID = GetPara("factorID");
            if (string.IsNullOrEmpty(sDate))
            {
                sDate = "1900-01-01";
            }
            if (string.IsNullOrEmpty(eDate))
            {
                eDate = "2060-01-01";
            }
            int iDisplayStart = Convert.ToInt32(GetPara("iDisplayStart"));
            int iDisplayLength = Convert.ToInt32(GetPara("iDisplayLength"));
            int count = 0;
            DataTables dts = new DataTables();
            dts.aaData = com.GetAll(iDisplayStart, iDisplayLength, out count);
            dts.sEcho = GetPara("sEcho");
            dts.iTotalDisplayRecords = count;
            dts.iTotalRecords = count;
            var result = new ApiResult();
            result.MessageType = MessageType.Success;
            result.Data = dts;
            return Ok(result);
        }
    }
}
