
$(function () {
    $.fn.typeahead.Constructor.prototype.blur = function () {
        var that = this;
        setTimeout(function () { that.hide(); }, 250);
    };



    $('#txtDisputeParty').typeahead({
        items: 15,
        minLength: 0,
        fitToElement:true,
        source: function (query, process) {

            $.ajax({
                url: '/DisputeApi/getSearchSupplier',
                type: 'GET',
                data: { searchName: query },
                dataType: 'json',
                success: function(data) {
                    // 这里省略resultList的处理过程，处理后resultList是一个字符串列表，
                    // 经过process函数处理后成为能被typeahead支持的字符串数组，作为搜索的源
                    //alert(data);
                    //return process(result);
                    //var results = $.map(testData, function (supplier) {
                    //    return supplier.Supplier1;
                    //});
                    //process(results);

                    var json = JSON.parse(data);
                    var resultList = json.map(function (item) {
                        var aItem = { id: item.Code, name: item.Supplier1 };
                        return JSON.stringify(aItem);
                    });
                    process(resultList);
                }
            });
           
        },
        matcher: function (obj) {
            var item = JSON.parse(obj);
            return ~item.name.toLowerCase().indexOf(this.query.toLowerCase());
        },

        highlighter: function (item) {
            //return "==>" + item + "<==";
            var itemObj = JSON.parse(item);
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');
            return itemObj.name.replace(new RegExp('(' + query + ')', 'ig'), function($1, match) {
                return '<strong>' + match + '</strong>';
            });
        },

        updater: function (item) {
            var itemObj = JSON.parse(item);
            //Store Id & name of supplier
            $(".hiddenSelectSupplierId").val(itemObj.id);
            $(".hiddenSelectSupplierText").val(itemObj.name);
            return itemObj.name;
        }
    });


    $('#txtDisputeContract').typeahead({
        items: 15,
        minLength: 0,
        fitToElement: true,
        source: function (query, process) {

            $.ajax({
                url: '/DisputeApi/getApprovedContract',
                type: 'GET',
                data: { searchName: query },
                dataType: 'json',
                success: function (data) {
                    // 这里省略resultList的处理过程，处理后resultList是一个字符串列表，
                    // 经过process函数处理后成为能被typeahead支持的字符串数组，作为搜索的源
                    //alert(data);
                    //return process(result);
                    //var results = $.map(testData, function (supplier) {
                    //    return supplier.Supplier1;
                    //});
                    //process(results);

                    var json = JSON.parse(data);
                    var resultList = json.map(function (item) {
                        var aItem = { id: item.ApplyId, name: item.Topic };
                        return JSON.stringify(aItem);
                    });
                    process(resultList);
                }
            });

        },
        matcher: function (obj) {
            var item = JSON.parse(obj);
            return ~item.name.toLowerCase().indexOf(this.query.toLowerCase());
        },

        highlighter: function (item) {
            //return "==>" + item + "<==";
            var itemObj = JSON.parse(item);
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');
            return itemObj.name.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                return '<strong>' + match + '</strong>';
            });
        },

        updater: function (item) {
            var itemObj = JSON.parse(item);

            //Store Id & name of supplier
            $(".hiddenSelectContractId").val(itemObj.id);
            $(".hiddenSelectContractText").val(itemObj.name);
            return itemObj.name;
        }
    });


    BindDatePick();
    
});


function BindDatePick() {
    $(".base-date").datetimepicker({
        language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        showMeridian: 1
    });
}





var btnCtrl = myApp.controller('formCtr', [
    '$scope', '$http', function($scope, $http) {

        var disputeId = getUrlParam('formId');
        var viewType = getUrlParam('viewType');

        if (disputeId != undefined && disputeId != "") {
            //Load Data


            var currentEditAuth = $(".hiddenWhetherUserCanEdit").val();
            var loadDataFlag = false;
            if (viewType == "allread") {
                loadDataFlag = true;
            } else {
                if (currentEditAuth == "0" && viewType == "onlyread") {
                    //用户没有编辑权限的时候，只能查看
                    loadDataFlag = true;
                }

                if (currentEditAuth == "1") {
                    //用户有编辑权限的时候，能看能编辑
                    loadDataFlag = true;
                }
            }
            if (!loadDataFlag) {
                layer.confirm('您无权限编辑该页面，请联系管理员！',
               {
                   btn: ['确定'],

               },
               function (index) {



                   window.location.href = "List.aspx";
                   layer.close(index);

               });
                return;
            }

            var loadUrl = "/DisputeApi/getDispute";
            $http({
                method: 'GET',
                url: loadUrl,
                params: { "disputeId": disputeId },
                cache: false

            }).success(function (data) {

                $scope.disputeInfo = data;
                $scope.disputeInfo.ActionStatus = "edit";
                $("#tdBatchNo").text($scope.disputeInfo.Attribute1);
                $(".hiddenSelectContractId").val($scope.disputeInfo.DisputeContract);
                $(".hiddenSelectContractText").val($scope.disputeInfo.DisputeContractText);
                $("#txtDisputeContract").val($scope.disputeInfo.DisputeContractText);
                $(".hiddenSelectSupplierId").val($scope.disputeInfo.DisputeParty);
                $(".hiddenSelectSupplierText").val($scope.disputeInfo.DisputePartyText);
                $("#txtDisputeParty").val($scope.disputeInfo.DisputePartyText);
                ShowPage(viewType);
                if (viewType == null ) {
                    $("#btnPageSaveBtn").hide();
                    $("#btnPageEditBtn").show();
                }


            }).error(function (data, header, config, status) {
                layer.alert(data.Message);
            });

        } else {
            //页面加载时，初始化
            $scope.disputeInfo = new Object();
            $scope.disputeInfo.DisputePartyType = "0";
            $scope.disputeInfo.ActionStatus = "add";
            $("#tdBatchNo").text($(".hiddenBatchNo").val());
            ShowPage(viewType);


        }



        $scope.SaveData = function (validate) {
            var arrMsg = new Array();

            var cusValid = true;
            //争议方
            if ($.trim($(".hiddenSelectSupplierId").val()) == "" || $.trim($("#txtDisputeParty").val()) == "") {
                arrMsg.push("请选择一个争议方！");
            } else {
                //Check whether suppier is valid
                if ($.trim($(".hiddenSelectSupplierText").val()) != $.trim($("#txtDisputeParty").val())) {
                    arrMsg.push("您输入的争议方有误，请选择一个正确的争议方！");
                }
            }

            //争议合同
            if ($.trim($("#txtDisputeContract").val()) != "") {
                if ($.trim($(".hiddenSelectContractText").val()) != $.trim($("#txtDisputeContract").val())) {
                    arrMsg.push("您输入的争议合同有误，请选择一个正确的争议合同！");
                }
            }


            if (arrMsg.length > 0) {
                cusValid = false;
            }


            if (validate || !cusValid) {
                checkform(arrMsg);
                return;
            }

            $scope.disputeInfo.DisputeParty = $.trim($(".hiddenSelectSupplierId").val());
            $scope.disputeInfo.DisputePartyText = $.trim($(".hiddenSelectSupplierText").val());
            if ($.trim($("#txtDisputeContract").val()) != "") {
                $scope.disputeInfo.DisputeContract = $.trim($(".hiddenSelectContractId").val());
                $scope.disputeInfo.DisputeContractText = $.trim($(".hiddenSelectContractText").val());
            }

            
            $scope.disputeInfo.DisputeId = $(".hiddenDisputeId").val();

            var shadeIndex = layer.load(2, { shade: [0.2, '#393D49'] });
            var actionUrl = "";
            

            actionUrl = "/DisputeApi/addDispute";
            
            var strData = "";
            strData = JSON.stringify($scope.disputeInfo);
            SaveFiles();//保存附件
            $http({
                method: 'POST',
                url: actionUrl,
                data: strData
            }).success(function (data) {
                layer.closeAll('loading');
                if (data == "success") {
                    var propMsg = '';
                    if (disputeId != undefined && disputeId != "") {
                        propMsg = '保存成功!';
                    } else {
                        propMsg = '提交成功!';
                    }

                    layer.confirm(propMsg, {
                        btn: ['确定'] //按钮
                    }, function () {


                        layer.closeAll();
                        window.location.href = "List.aspx";
                    });
                } else {
                    layer.alert('提交失败');
                }


            }).error(function (data, header, config, status) {
                layer.closeAll('loading');
                layer.alert(data.Message);
            });


        }


        $scope.cancelAction = function() {

            window.location.href = "List.aspx";

        }

        $scope.closeAction = function() {
            window.opener = null;
            window.open('', '_self', '');
            window.close();
        }
    }
]);


function ShowPage(viewType) {
    if (viewType != undefined && viewType != "") {
        if (viewType == "allread" || viewType == "allread") {
            //只读页面
            $("#tableContent").addClass("onlyForRead");
            $("#tableContent").find("input[type='text']").attr("readonly", "readonly");
            $("#tableContent").find("input[type='radio']").attr("disabled", "disabled");
            $("#txtSetupTime").hide();
            $("#txtReleaseTime").hide();
            $("#btnPageEditBtn").hide();
            $("#btnPageSaveBtn").hide();
            $("#btnPageCancelBtn").hide();
            $("#btnPageCloseBtn").show();
            $("#lbltxtSetupTime").show();
            $("#lbltxtReleaseTime").show();
           
            $(".red-x").text("");

        }
    } else {
        $("#btnPageCloseBtn").hide();
        $("#btnPageEditBtn").hide();
        $("#lbltxtSetupTime").hide();
        $("#lbltxtReleaseTime").hide();


    }

}

