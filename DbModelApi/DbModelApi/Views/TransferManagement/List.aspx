<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Default.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DbModelApi.Views.TransferManagement.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHead" runat="server">
    <title>test api</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="side-content page">
        <div class="main-content">
            <div class="tip_url">
                当前位置：<a>保理业务</a>&gt;<a>业务资金管理</a>&gt;<a class="active">付款转账管理</a>
            </div>
            <div class="add-top-new">
                <a class="icon-add" target="_parent" href="Maintain.aspx?isview=false">添加付款转账</a>
            </div>
            <div class="content">
                <div class="search">
                    <div class="left-search">
                        <table class="content-table">
                            <tr>
                                <td class="tds">
                                    <span>申请人：</span>
                                </td>
                                <td class="tdi">
                                    <input class="base-ipt listFilter SName" name="SName" type="text">
                                </td>
                                <td class="tds">
                                    <span>类型：</span>
                                </td>
                                <td>
                                     <select id="ddlType" class="base-ipt select-ipt">
                                        <option value="">全部</option>
                                         <option value="0">付款</option>
                                         <option value="1">调拨</option>
                                    </select>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="tds">
                                    <span>表单状态：</span>
                                </td>
                                <td class="tdi">
                                    <select id="ddlWfStatus" class="base-ipt select-ipt">
                                        <option value="">全部</option>
                                    </select>
                                </td>
                                <td class="tds">
                                    <span>创建时间：</span>
                                </td>
                                <td class="tdi">
                                    <input class="base-date date form_datetime datetimepicker listFilter" style="width: 43%;" id="txtStartDate" name="Created" data-date-format="yyyy-mm-dd" type="text" />
                                    <hr class="hr-style" style="width: 4%;" />
                                    <input class="base-date date form_datetime datetimepicker listFilter" style="width: 43%;" id="txtEndDate" name="EndCreated" data-date-format="yyyy-mm-dd" type="text" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right-search">
                        <div class="ax_html_button" style="cursor: pointer">
                            <a class="sa-btn search-btn" onclick="loadlist()" href="javascript:void(0)">搜索</a>
                        </div>
                        <div class="ax_html_button" style="cursor: pointer">
                            <a class="sa-btn reset-btn" onclick="Reset()" href="javascript:void(0)">重置</a>
                        </div>
                    </div>
                </div>
                <div class="this-content">
                    <table class="content-table2" id="tabList">
                        <thead>
                            <tr>
                                <th>序号
                                </th>
                                <th>主题
                                </th>
                                
                                <th>公司
                                </th>
                                
                                <th>创建时间
                                </th>
                                
                                <th>操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script>
        
        $(function () {
            loadlist();
            $("#txtStartDate").datetimepicker({
                language: 'en',
                weekStart: 7,
                todayBtn: 1,
                autoclose: 1,
                clearBtn: true,
                todayHighlight: 1,
                startView: 2,
                startView: 'month',
                minView: 'month',
                forceParse: 0,
                showMeridian: 1,
                format: 'yyyy-mm-dd',
            }).on("changeDate", function (ev) {
                $("#txtEndDate").datetimepicker("setStartDate", $("#txtStartDate").val());
            })

            $("#txtEndDate").datetimepicker({
                language: 'en',
                weekStart: 7,
                todayBtn: 1,
                autoclose: 1,
                clearBtn: true,
                todayHighlight: 1,
                startView: 2,
                startView: 'month',
                minView: 'month',
                forceParse: 0,
                showMeridian: 1,
                format: 'yyyy-mm-dd',
            }).on("changeDate", function (ev) {
                $("#txtStartDate").datetimepicker("setEndDate", $("#txtEndDate").val());
            });
            $("#ddlWfStatus,#ddlType").select2({
                minimumResultsForSearch: Infinity
            });
            //GetWfStatus();
        });
        function Reset() {
            filterReset();
            $("#ddlWfStatus").find("option[value='']").attr("selected", true);

            $("#ddlWfStatus").val("").trigger("change");
            $("#ddlType").find("option[value='']").attr("selected", true);

            $("#ddlType").val("").trigger("change");

            loadlist();
        }
        function GetWfStatus() {
            $.ajax({
                type: "GET",
                url: "/api/transfer/wfstatus",
                contentType: "application/json; charset=utf-8",
                success: function (results) {
                    var data = results;
                    if (data != null && data != undefined && data != "") {
                        $(data).each(function () {
                            $("#ddlWfStatus").append(" <option value='" + this.Value + "'>" + this.Des + "</option>");
                        });
                    }
                },
                error: function (err) {
                    ErrorResponse(err);
                }
            });
        }
        function loadlist() {
            var url = '/api/transfers';
            var stype = $("#ddlType").val();
            if (stype == undefined || stype == null) {
                stype = "";
            }
            var paras = 'TransferType=' + stype + '&UserName=' + $(".SName").val() + '&ProcessStatus=' + $("#ddlWfStatus").val() + '&startTime=' + $("#txtStartDate").val() + '&endTime=' + $("#txtEndDate").val() + '&';


            var t = $('#tabList').dataTable({
                //"sDom": 't<<"bottom-left"l><"bottom-right"p>>',
                "oLanguage": {
                    "sEmptyTable": "未能查找到记录"
                },
                "bDestroy": true,
                "bFilter": false,//显示搜索区域
                "bLengthChange": false,//显示是否可更改每页显示条数
                "bPaginate": true,//是否需分页控件
                "bInfo": false,//显示XX条，当前第X页
                "iDisplayLength": 10,
                "searching": false,
                "bProcessing": false,
                "bServerSide": true,
                "bAutoWidth": false,
                "sAjaxSource": url + encodeURI("?" + paras) + "&fresh=" + Math.random(),
                "sServerMethod": "GET",
                "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                    oSettings.jqXHR = $.ajax({
                        "dataType": 'json',
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success": function(data) {
                            var obj = getJsonData(data);

                            fnCallback(obj);
                        },
                        "error": function(data) {
                            ErrorResponse(data);
                        }
                    });
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    var index = $('#tabList').dataTable().fnSettings()._iDisplayStart + iDisplayIndex + 1;
                    $('td:eq(0)', nRow).html(index);
                    return nRow;
                },
                "aoColumns": [
                    {
                        "mData": "Topic", "bSortable": false, "sType": 'chinese-string'
                    },
                    {
                        "mData": "Topic", "bSortable": false, "sType": 'chinese-string',
                        "mRender": function (data, type, full) {
                            return '<a target=\"_blank\" href=\"View.aspx?isview=true&formId=' + full.ApplyId + '\">' + data + '</a>';
                        }
                    },
                    
                    {
                        "mData": "Attribute6", "bSortable": false, "sType": 'chinese-string'
                    },
                    
                    {
                        "mData": "Created", "bSortable": false, "sType": 'chinese-string', "mRender": function (data, type, full) {
                            return formatDate(data);
                        }
                    },
                    {
                        "mData": "ApplyId", "bSortable": false, "sType": 'chinese-string',
                        "mRender": function (data, type, full) {
                            
                                    return '<a class="icon-change" target=\"_parent\" href="Maintain.aspx?formId=' + data + '">修改</a>';
                                
                        }
                    }
                ]
            });


        }

    </script>

</asp:Content>
