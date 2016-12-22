using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbModelApi.BLL;
using DbModelApi.Model;
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

        [HttpPost]
        [Route("transfer")]
        public IHttpActionResult Post(TransferManagement transfer)
        {
            TransferManagementBLL com = new TransferManagementBLL();
            transfer.ApplyId = new Guid(transfer.Attribute5);
            transfer.Attribute5 = "";
            transfer.Created = DateTime.Now;
            //transfer.CreatedBy = CurrentUserInfo.ID;
           // transfer.ProcessStatus = WFStatus.未提交.GetHashCode().ToString();
            //transfer.FactoringId = CurrentFactoring;
            foreach (var tran in transfer.TransferDetails)
            {
                tran.Id = Guid.NewGuid();
                tran.ApplyId = transfer.ApplyId;
                tran.Created = DateTime.Now;
                //tran.CreatedBy = CurrentUserInfo.ID.ToString();
            }
            com.Create(transfer);
            string processid = "";
            if (!string.IsNullOrEmpty(processid))
            {
                transfer.ProcessId = processid;
                transfer.ProcessStatus = "0";
            }
            com.Update(transfer);
            return Ok();
        }

        [HttpGet]
        [Route("transfer/{id:Guid}")]
        public IHttpActionResult Get(Guid id)
        {
            TransferManagementBLL com = new TransferManagementBLL();
            var result = new ApiResult();
            result.MessageType = MessageType.Success;
            result.Data = com.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("transfer")]
        public IHttpActionResult Put(TransferManagement fin)
        {
            TransferManagementBLL com = new TransferManagementBLL();
            fin.Modified = DateTime.Now;
            //fin.ModifiedBy = CurrentUserInfo.ID;
            fin.ProcessStatus = "0";
            foreach (var detail in fin.TransferDetails)
            {
                detail.Id = Guid.NewGuid();
                detail.ApplyId = fin.ApplyId;
                //detail.CreatedBy = CurrentUserInfo.ID.ToString();
                detail.Created = DateTime.Now;
            }
            com.Update(fin);
            
            return Ok();
        }
    }
}
