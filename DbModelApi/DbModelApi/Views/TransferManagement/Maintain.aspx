<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Default.Master" AutoEventWireup="true" CodeBehind="Maintain.aspx.cs" Inherits="DbModelApi.Views.TransferManagement.Maintain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHead" runat="server">
    <title>添加付款转账</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="side-content page" ng-controller="formCtr">
        <div class="main-content">
            <div class="icon4 tip_url">当前位置：<a>保理业务</a>&gt;<a>业务资金管理</a>&gt;<a class="active">付款转账管理</a></div>
            <div class="content">
                <div class="this-title">
                    基础信息
                </div>

                <div id="divCheck" style="display: none">
                    <div class="w-tip iconerror" ng-show="myForm.CompanyName.$error.required">
                        公司不能为空
                    </div>
                    <div class="w-tip iconerror" ng-show="myForm.Attribute2.$error.required">
                        日期不能为空
                    </div>
                    <div class="w-tip iconerror" ng-show="myForm.Topic.$error.required">
                        主题不能为空
                    </div>
                </div>

                <div class="this-content">
                    <table class="content-table">
                        <tbody>
                            <tr>
                                <td><span class="red-x">*</span>批次：</td>
                                <td>{{formdata.Attribute1}}</td>
                                <td><span class="red-x">*</span>公司：</td>
                                <td>
                                    <input type="text" name="CompanyName" ng-model="formdata.CompanyName" required maxlength="200"></td>
                            </tr>
                            <tr>
                                <td><span class="red-x">*</span>部门：</td>
                                <td>{{formdata.DeptName}}</td>
                                <td><span class="red-x">*</span>职位：</td>
                                <td>{{formdata.JobName}}</td>
                            </tr>
                            <tr>
                                <td><span class="red-x">*</span>类型：</td>
                                <td>
                                    <div class="checkbox check_left">
                                        <input id="radsupplier" class="magic-radio" type="radio" ng-model="formdata.TransferType" name="radtype" value="0" /><label for="radsupplier">付款</label>
                                    </div>
                                    <div class="checkbox check_left">
                                        <input id="radcustomer" class="magic-radio" type="radio" ng-model="formdata.TransferType" name="radtype" value="1" /><label for="radcustomer">调拨</label>
                                    </div>
                                </td>
                                <td><span class="red-x">*</span>日期：</td>
                                <td>
                                    <input type="text" name="Attribute2" ng-model="formdata.Attribute2" maxlength="200" class="base-date date form_datetime datetimepicker listFilter" id="txtStartDate" data-date-format="yyyy-mm-dd" required></td>
                            </tr>
                            <tr>
                                <td><span class="red-x">*</span>主题：</td>
                                <td>
                                    <input type="text" name="Topic" ng-model="formdata.Topic" required maxlength="100"></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <ng-form name="details">
                <div class="this-title">
                    付款/转账详情
                </div>
                <div class="this-content">

                    <table class="content-table2">
                        <thead>
                            <tr>
                                <th style="width: 5%;">编号</th>
                                <th style="width: 8%;">发票号码</th>
                                <th style="width: 8%;"><span class="red-x">*</span>金额</th>
                                <th><span class="red-x">*</span>货币</th>
                                <th><span class="red-x">*</span>收款单位名称</th>
                                <th><span class="red-x">*</span>收款单位银行账号</th>
                                <th><span class="red-x">*</span>收款单位开户行</th>
                                <th style="width: 20%;"><span class="red-x">*</span>付款事项</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="{{tm.$$hashKey}}paytr" ng-repeat="tm in formdata.TransferDetails" style="vertical-align: middle !important;" on-finish-render>
                                <td style="vertical-align: middle !important;">{{$index+1}}</td>
                                <td style="vertical-align: middle !important;">
                                    <input type="text" ng-model="tm.InvoiceNo" ng-disabled="tm.isreadonly" maxlength="100" name="InvoiceNo" placeholder="发票号码">
                                </td>
                                <td style="vertical-align: middle !important;">
                                    <input type="text" ng-model="tm.Amount" name="Amount" style="text-align: right;" ng-disabled="tm.isreadonly" placeholder="金额" required decimal>
                                </td>
                                <td style="vertical-align: middle !important;">{{tm.Currency}}</td>
                                <td style="vertical-align: middle !important;">
                                    <input type="text" ng-model="tm.ReceiveAccountName" name="ReceiveAccountName" ng-disabled="tm.isreadonly" maxlength="100" placeholder="收款单位名称" required></td>
                                <td style="vertical-align: middle !important;">
                                    <input type="text" ng-model="tm.ReceiveAccount" name="ReceiveAccount" ng-disabled="tm.isreadonly" maxlength="100" placeholder="收款单位银行账号" required></td>
                                <td style="vertical-align: middle !important;">
                                    <input type="text" ng-model="tm.ReceiveOpeningBank" name="ReceiveOpeningBank" ng-disabled="tm.isreadonly" maxlength="100" placeholder="收款单位开户行" required></td>
                                <td style="vertical-align: middle !important;">
                                    <textarea ng-model="tm.Attribute1" name="dAttribute1" ng-disabled="tm.isreadonly" style="width: 98%; height: 40px;border: 1px #ccc solid;" ng-maxlength="500" placeholder="付款事项" required></textarea></td>
                                <td style="vertical-align: middle !important;">
                                    <!-- 编辑 -->
                                    <a class="icon-change" ng-click="edittm(details.$invalid,tm.$$hashKey)" ng-show="tm.isreadonly">修改</a>
                                    <a class="icon-change" ng-click="savetm(details.$invalid,tm.$$hashKey)" ng-hide="tm.isreadonly">保存</a>
                                    <!-- 取消 -->
                                    <a ng-hide="tm.isreadonly" ng-click='resettm(tm.$$hashKey)' ng-hide="tm.isreadonly">取消</a>
                                    <!-- 删除当前行 -->
                                    <a class="icon-delete" ng-click="removetm($index)" ng-show="tm.isreadonly">删除</a>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                        <tr>
                            <td colspan="2" style="text-align: left;">合计(小写)：</td><td colspan="2">{{formdata.subtotal}}</td>
                            <td style="text-align: left;">合计(大写)：</td><td colspan="4">{{formdata.subtotalbig}}</td>
                        </tr>
                        </tfoot>
                    </table>
                    <table>
                        <tr>
                            <!-- 增加一行 -->
                            <td>
                                <div class="btn-div" style="text-align: left">
                                    <div class="btn_save" style="cursor: pointer" ng-click='addChild(details.$invalid)'>增加</div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="divDetailCheck" style="display: none">
                        <div class="w-tip iconerror" ng-show="details.Amount.$error.required">
                            金额不能为空
                        </div>
                        <div class="w-tip iconerror" ng-show="details.ReceiveAccountName.$error.required">
                            收款单位名称不能为空
                        </div>
                        <div class="w-tip iconerror" ng-show="details.ReceiveAccount.$error.required">
                            收款单位银行账号不能为空
                        </div>
                        <div class="w-tip iconerror" ng-show="details.ReceiveOpeningBank.$error.required">
                            收款单位开户行不能为空
                        </div>
                        <div class="w-tip iconerror" ng-show="details.dAttribute1.$error.required">
                            付款事项不能为空
                        </div>
                        <div class="w-tip iconerror" ng-show="details.dAttribute1.$error.maxlength">
                            付款事项最大允许输入500个字符
                        </div>
                    </div>
                </div>
                </ng-form>
                <div class="this-title">
                    附件
                </div>
                <div class="this-content">
                    
                </div>
            </div>
            <div class="btn-div">
                <div class="btn_save" ng-click="addFin(myForm.$invalid)">提交</div>
                <div class="btn_cancel" onclick="goback()">取消</div>
            </div>
        </div>
        <div>
        </div>
        <a href="javascript:void(0)" ng-click="GetFinOrgName()" style="display: none;" id="sav"></a>
    </div>

    <input type="hidden" id="hidFname" class="hidFname" runat="server" />
    <input type="hidden" id="hidjobcode" class="hidjobcode" runat="server" />
    <input type="hidden" id="hidjobname" class="hidjobname" runat="server" />
    <input type="hidden" id="hiddeptname" class="hiddeptname" runat="server" />
    <input type="hidden" id="hiddeptid" class="hiddeptid" runat="server" />
    <script>
        $(function () {
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
                format: 'yyyy-mm-dd'
            });
        });
        myApp.directive('decimal', function () {
            var link = function (scope, element, attrs, ngModel) {
                var cFix = attrs['decimal'];
                if (cFix == "" || cFix == null)
                    cFix = 2;
                $(element).on('input', function (e) {

                    if (isNaN($(element).val()) && $(element).val().length > 0) {
                        $(element).val($(element).val().substr(0, $(element).val().length - 1));
                        $(element).trigger("change");
                    }
                    if ($(element).val().indexOf(".") != -1) {
                        var v = $(element).val();
                        if (v.split(".")[1].length > cFix) {
                            $(element).val($(element).val().substr(0, $(element).val().length - 1));
                            $(element).trigger("change");
                        }
                    }
                    $(element).val($.trim($(element).val()));

                    scope.$apply(function () {
                        ngModel.$setViewValue($(element).val());
                    });
                });


            };
            return {
                require: 'ngModel',
                restrict: 'A',
                link: link
            }
        });
        var btnCtrl = myApp.controller('formCtr', ['$scope', '$http', function ($scope, $http) {

            var url = '/api/transfer';
            var id = getUrlParam("formId");
            $scope.formdata = new Object();
            $scope.formdata.TransferType = "0";
            $scope.formdata.subtotal = "0.00";
            $scope.formdata.subtotalbig = "零元整";
            $scope.tempdata = new Object();
            if (id != null && id != undefined && id != "") {
                $http({ method: 'GET', url: url + '/' + id }).success(function (data) {
                    var user = getJsonData(data);
                    $scope.formdata = user;
                    //$scope.formdata.TransferDetails = user.tdetails;
                    if ($scope.formdata.TransferDetails != undefined) {
                        $.each($scope.formdata.TransferDetails, function (index, content) {
                            content.isreadonly = true;
                        });
                    }
                    $scope.subtotal();
                    var isView = getUrlParam("isview");
                    if (isView != null && isView != undefined && isView == "true") {
                        $(".btn_save").hide();
                        $(".selectnamea").hide();

                    }
                    $http({ method: 'GET', url: "/api/transfer/user/" + $scope.formdata.CreatedBy }).success(function (data) {
                        var user = getJsonData(data);
                        $scope.formdata.DeptName = user.Attribute2;
                        $scope.formdata.JobName = user.Attribute1;
                    }).error(function (data) {
                        layer.alert(data);
                        //处理响应失败
                    });
                });

            } else {//新增时
                $scope.formdata.DeptName = $(".hiddeptname").val();
                $scope.formdata.JobName = $(".hidjobname").val();
                $scope.formdata.DeptCode = $(".hiddeptid").val();
                $scope.formdata.JobCode = $(".hidjobcode").val();
                //$http({ method: 'GET', url: "/api/transfer/batch" }).success(function (data) {
                //    var slist = data.split(';');
                //    $scope.formdata.Attribute1 = slist[0];
                //    $scope.formdata.CompanyName = slist[1];
                //}).error(function (data) {
                //    layer.alert(data);
                //    //处理响应失败
                //});
            }
            //取消明细
            $scope.resettm = function (empKey) {
                $.each($scope.formdata.TransferDetails, function (index, content) {
                    if (content.$$hashKey == empKey) {
                        if (content.issave == false) {
                            $scope.formdata.TransferDetails.splice(index, 1);
                        } else {
                            angular.copy($scope.tempdata, content);
                            content.isreadonly = true;

                        }
                    }
                });
            }
            //修改明细
            $scope.edittm = function (valid, empKey) {
                if (valid) {
                    checkformdetail(null, "divDetailCheck");
                    return;
                }
                $.each($scope.formdata.TransferDetails, function (index, content) {
                    if (content.$$hashKey == empKey) {
                        content.isreadonly = false;
                        $scope.tempdata = angular.copy(content);
                    }
                });
            }
            //保存付款明细行
            $scope.savetm = function (valid, empKey) {
                if (valid) {
                    checkformdetail(null, "divDetailCheck");
                    return;
                }
                $.each($scope.formdata.TransferDetails, function (index, content) {
                    if (content.$$hashKey == empKey) {
                        content.isreadonly = true;
                        content.issave = true;
                    }
                });
                $scope.subtotal();
            }
            //计算合计
            $scope.subtotal = function () {
                var subt = 0.00;
                $.each($scope.formdata.TransferDetails, function (index, content) {
                    if (content.Amount != "") {
                        subt += parseFloat(content.Amount);
                    }
                });
                $scope.formdata.subtotal = $.formatMoney(subt, 2);
                if (subt > 0) {
                    $scope.formdata.subtotalbig = Arabia_to_Chinese($scope.formdata.subtotal);
                } else {
                    $scope.formdata.subtotalbig = "零元整";
                }
            }
            //删除明细行
            $scope.removetm = function (empKey) {
                layer.confirm('您确认要删除吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    
                    $scope.formdata.TransferDetails.splice(empKey, 1);
                       
                    $scope.subtotal();
                    $scope.$apply();
                    layer.closeAll();
                });
            }
            //添加付款明细行
            $scope.addChild = function (valid) {
                var emp = new Object();
                if ($scope.formdata.TransferDetails == undefined) {
                    $scope.formdata.TransferDetails = new Array();
                } else {
                    if (valid) {
                        checkformdetail(null, "divDetailCheck");
                        return;
                    }

                }
                emp.isreadonly = false;
                emp.Currency = "人民币";
                emp.issave = false;
                $scope.formdata.TransferDetails.push(emp);
            }
            $scope.addFin = function (valid) {
                var alist = new Array();
                if ($scope.formdata.TransferDetails == undefined || $scope.formdata.TransferDetails.length == 0) {
                    alist.push("付款/转账明细不能为空");
                    valid = true;
                }
                if (valid) {
                    checkform(alist);
                    return;
                }
                //if (!SaveFiles()) {//保存附件
                //    return;
                //}
                showloading();
                $scope.formdata.Attribute5 = $(".hidFormID").val();
                var fin = $scope.formdata;

                if (id == null || id == undefined || id == "") {
                    $http({ method: 'POST', url: url, data: fin }).success(function (data) {
                        MessageAlert('提交成功', 'List.aspx');

                    }).error(function (data, header, config, status) {
                        AngularErrorResponse(data.Message);
                        hidloading();
                    });

                }
                else {
                    fin.ApplyId = id;
                    $http({ method: 'PUT', url: url, data: fin }).success(function (data) {

                        MessageAlert('提交成功', 'List.aspx');

                    }).error(function (data, header, config, status) {
                        AngularErrorResponse(data.Message);
                        hideloading();
                    });;
                }

            }

        }]);


    </script>

</asp:Content>
