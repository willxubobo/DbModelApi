$(document).ready(function () {
    minHeight();
    //userOperate();
    //navHover();
    //sideNav();
    //btnClick();

    //InitFactor();
    //setMenuSelected();
    //initUserName();

    //$(".nav-select").on("click", function (e) {
    //    if ($(".sub-nav").is(":hidden")) {
    //        $(".sub-nav").show();
    //    } else {
    //        $(".sub-nav").hide();
    //    }

    //    $(document).one("click", function () {
    //        $(".sub-nav").hide();
    //    });

    //    e.stopPropagation();
    //});
    //$(".sub-nav").on("click", function (e) {
    //    e.stopPropagation();
    //});
});

function initUserName() {
    if ($("#divUserName")) {
        if ($(".welcome-img")) {
            $(".welcome-img").html("欢迎" + $("#divUserName").html() + "，登录世茂保理系统！");
        }
    }
}

function minHeight() {
    $(".side-content,.main-content").css("min-height", ($(window).height() - 100));
    $("html,body,.wrap").resize(function () {
        $(".side-content,.main-content").css("min-height", ($(window).height() - 100));
    });
}

function userOperate() {
    $(document).on("mouseenter", ".user-info", function () {
        $(this).addClass("on");
    });
    $(document).on("mouseleave", ".user-info", function () {
        $(this).removeClass("on");
    });
}

function navHover() {
    $(document).on("mouseenter", ".nav-list li", function () {
        $(this).addClass("on");
    });
    $(document).on("mouseleave", ".nav-list li", function () {
        $(this).removeClass("on");
    });
}
function sideNav() {
    $(document).on("click", ".side-link", function () {
        $(this).parent().siblings().children(".side-link").each(function () {
            $(this).next("ul").hide();
            $(this).children(".nav-open").removeClass("nav-close");
            $(this).children(".side-nav-open").removeClass("side-nav-close");
        });
        $(this).next("ul").slideToggle("fast").find("ul").hide();
        $(this).children(".nav-open").toggleClass("nav-close");
        if ($(this).children().last().hasClass("nav-open")) {
            $(this).parent().find(".side-nav-open").removeClass("side-nav-close");
        }
        else {
            $(this).children(".side-nav-open").toggleClass("side-nav-close");
        }
    });
}

function btnClick() {
    $(document).on("mousedown", ".side-link", function (e) {
        if (e.which == 1) {
            $(this).find(".side-link-txt").stop(true).animate({ top: "1px", left: "1px" }, 0);
        }
    });
    $(document).on("mouseup", ".side-link", function (e) {
        if (e.which == 1) {
            $(this).find(".side-link-txt").stop(true).animate({ top: "0px", left: "0px" }, 0);
        }
    });

    $(document).on("mousedown", ".btn", function (e) {
        if (e.which == 1) {
            $(this).find("span").stop(true).animate({ top: "1px", left: "1px" }, 0);
        }
    });
    $(document).on("mouseup", ".btn", function (e) {
        if (e.which == 1) {
            $(this).find("span").stop(true).animate({ top: "0px", left: "0px" }, 0);
        }
    });
    $(document).on("mouseup", "#businessProgram", function (e) {
        window.location.href = "businessProgram.html";
    });
    $(document).on("mouseup", "#baoliadd", function (e) {
        window.location.href = "baoliadd.html";
    });
}


//»ñÈ¡urlÖÐµÄ²ÎÊý
function getUrlParam(name) {
    name = name.toLowerCase();
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //¹¹ÔìÒ»¸öº¬ÓÐÄ¿±ê²ÎÊýµÄÕýÔò±í´ïÊ½¶ÔÏó
    var r = window.location.search.toLowerCase().substr(1).match(reg);  //Æ¥ÅäÄ¿±ê²ÎÊý
    if (r != null) return unescape(r[2]); return null; //·µ»Ø²ÎÊýÖµ
}

function checkform(array) {
    $("#divCheck").find(".tmpMsg").remove();
    if (array != null) {
        $.each(array, function () {
            var msg = '<div class="w-tip iconerror tmpMsg">' + this + '</div>';
            $("#divCheck").append(msg);
        });
    }
    layer.open({
        type: 1,
        area: ['600px'],
        shadeClose: true,
        shade: [0.4, '#393D49'],
        title: '以下信息填写错误',
        content: $("#divCheck")
    });
    $(".layui-layer").addClass("layui-layer2");

}
//add by will
function checkformdetail(array, obj) {
    $("#" + obj).find(".tmpMsg").remove();
    if (array != null) {
        $.each(array, function () {
            var msg = '<div class="w-tip iconerror tmpMsg">' + this + '</div>';
            $("#" + obj).append(msg);
        });
    }
    layer.open({
        type: 1,
        area: ['600px'],
        shadeClose: true,
        shade: [0.4, '#393D49'],
        title: '以下信息填写错误',
        content: $("#" + obj)
    });
    $(".layui-layer").addClass("layui-layer2");

}

function closeLayer() {
    layer.closeAll();
}

function redirectUrl(url) {
    window.location.href = url;
}

function setMenuSelected() {
    //获取当前url去掉参数、去掉host的纯路径
    var curpath = window.location.pathname.toLowerCase();
    if (curpath == "/") {
        $(".side-nav-list:first").show();
        return;
    }
    //获取当前路径对应的菜单li元素
    var curMenu = $("li[linkurl*='" + curpath + "']");
    if (curMenu.length <= 0) {
        if ($(".HD_MAINPAGE")) {
            curpath = $(".HD_MAINPAGE").val();
            if (curpath) {
                curpath = curpath.toLowerCase();
                curMenu = $("li[linkurl*='" + curpath + "']");
            }
        }
        else {
            curpath = document.referrer.split('pages')[1];
            if (curpath) {
                curpath = curpath.toLowerCase();
                curMenu = $("li[linkurl*='" + curpath + "']");
            }
        }
    }
    //如果找到元素则设置相关的菜单显示及展开
    if (curMenu.length > 0) {
        //获取根ul
        var rootul = curMenu.closest(".side-nav-list");
        if (rootul) {
            rootul.show();
            var ulid = rootul.attr("id");
            var topliid = ulid.replace("ul", "li");
            var topmenu = $("#" + topliid + "");
            if (topmenu) {
                $(".nav-list").children().each(function () {
                    $(this).removeClass("selected");
                });
                topmenu.addClass("selected");
            }
        }
        //设置父ul显示
        curMenu.parent().show();
        //设置父菜单图标展开样式
        if (!curMenu.parent().hasClass("side-nav-list")) {
            if (curMenu.parent().prev().children().last().hasClass("nav-open")) {
                curMenu.parent().prev().children().last().addClass("nav-close");
            }
            else {
                curMenu.parent().parent().parent().show();
                curMenu.parent().parent().parent().prev().children().last().addClass("nav-close");
                curMenu.parent().prev().children().last().addClass("side-nav-close");
            }
        }
    }
    else {
        $(".side-nav-list:first").show();
    }
}

function getParas() {
    var result = "";

    $(".listFilter").each(function () {
        if ($(this).prop('nodeName') == "SELECT") {
            if ($(this).val() == "-1")
                return;
        }
        result += $(this).prop('name') + '=' + $(this).val() + "&"

    });

    return encodeURI(result);
}

function filterReset() {
    $(".listFilter").each(function () {
        $(this).val("");
    });

    $(".iselect2").val("-1").trigger("change");
    loadlist();
    // window.location.reload();
}
function getJsonData(obj) {
    if (obj.MessageType == 1)
        return obj.Data;
    else
        layer.alert(obj.Data);
}
//add by will
//关闭页面
function CloseWin() {
    window.opener = null; window.open('', '_self'); window.close();
}
//修改或添加返回上一页
function goback() {
    history.go(-1);
}
//保存附件并关闭页面
function CloseWinOnUploaded() {
    showloading();
    if (SaveFiles()) {
        hideloading();
        layer.alert('保存成功',
        {
            end: function(index) {
                window.opener = null;
                window.open('', '_self');
                window.close();
            }
        });


    } else {
        hideloading();
    }
}
//数字转换成大写
function Arabia_to_Chinese(Num) {
    for (i = Num.length - 1; i >= 0; i--) {
        Num = Num.replace(",", "")//替换tomoney()中的“,”
        Num = Num.replace(" ", "")//替换tomoney()中的空格
    }
    Num = Num.replace("￥", "")//替换掉可能出现的￥字符
    if (isNaN(Num)) { //验证输入的字符是否为数字
        alert("请检查小写金额是否正确");
        return;
    }
    //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
    part = String(Num).split(".");
    newchar = "";
    //小数点前进行转化
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) { alert("位数过大，无法计算"); return ""; } //若数量超过拾亿单位，提示
        tmpnewchar = ""
        perchar = part[0].charAt(i);
        switch (perchar) {
            case "0": tmpnewchar = "零" + tmpnewchar; break;
            case "1": tmpnewchar = "壹" + tmpnewchar; break;
            case "2": tmpnewchar = "贰" + tmpnewchar; break;
            case "3": tmpnewchar = "叁" + tmpnewchar; break;
            case "4": tmpnewchar = "肆" + tmpnewchar; break;
            case "5": tmpnewchar = "伍" + tmpnewchar; break;
            case "6": tmpnewchar = "陆" + tmpnewchar; break;
            case "7": tmpnewchar = "柒" + tmpnewchar; break;
            case "8": tmpnewchar = "捌" + tmpnewchar; break;
            case "9": tmpnewchar = "玖" + tmpnewchar; break;
        }
        switch (part[0].length - i - 1) {
            case 0: tmpnewchar = tmpnewchar + "元"; break;
            case 1: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 2: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 3: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 4: tmpnewchar = tmpnewchar + "万"; break;
            case 5: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 6: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 7: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 8: tmpnewchar = tmpnewchar + "亿"; break;
            case 9: tmpnewchar = tmpnewchar + "拾"; break;
        }
        newchar = tmpnewchar + newchar;
    }
    //小数点之后进行转化
    if (Num.indexOf(".") != -1) {
        if (part[1].length > 2) {
            alert("小数点之后只能保留两位,系统将自动截段");
            part[1] = part[1].substr(0, 2)
        }
        for (i = 0; i < part[1].length; i++) {
            tmpnewchar = ""
            perchar = part[1].charAt(i)
            switch (perchar) {
                case "0": tmpnewchar = "零" + tmpnewchar; break;
                case "1": tmpnewchar = "壹" + tmpnewchar; break;
                case "2": tmpnewchar = "贰" + tmpnewchar; break;
                case "3": tmpnewchar = "叁" + tmpnewchar; break;
                case "4": tmpnewchar = "肆" + tmpnewchar; break;
                case "5": tmpnewchar = "伍" + tmpnewchar; break;
                case "6": tmpnewchar = "陆" + tmpnewchar; break;
                case "7": tmpnewchar = "柒" + tmpnewchar; break;
                case "8": tmpnewchar = "捌" + tmpnewchar; break;
                case "9": tmpnewchar = "玖" + tmpnewchar; break;
            }
            if (i == 0) tmpnewchar = tmpnewchar + "角";
            if (i == 1) tmpnewchar = tmpnewchar + "分";
            newchar = newchar + tmpnewchar;
        }
    }
    //替换所有无用汉字
    while (newchar.search("零零") != -1)
        newchar = newchar.replace("零零", "零");
    newchar = newchar.replace("零亿", "亿");
    newchar = newchar.replace("亿万", "亿");
    newchar = newchar.replace("零万", "万");
    newchar = newchar.replace("零元", "元");
    newchar = newchar.replace("零角", "");
    newchar = newchar.replace("零分", "");
    if (newchar.charAt(newchar.length - 1) == "元" || newchar.charAt(newchar.length - 1) == "角")
        newchar = newchar + "整"
    //  document.write(newchar);
    return newchar;
}
function checkTel() {
    var isPhone = /^([0-9]{3,4}-)?[0-9]{7,8}$/;
    var isMob = /^((\+?86)|(\(\+86\)))?(13[012356789][0-9]{8}|15[012356789][0-9]{8}|18[02356789][0-9]{8}|147[0-9]{8}|1349[0-9]{7})$/;
    var value = document.getElementById("ss").value.trim();
    if (isMob.test(value) || isPhone.test(value)) {
        return true;
    }
    else {
        return false;
    }
}
Date.prototype.format = function (format) {

    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

//format date
function formatDate(data) {
    if (data != null && data != undefined && data != "") {
        data = data.replace("T", " ").split('.')[0];
        data = data.replace(/-/g, "/");
        var date = new Date(data);
        return date.format('yyyy-MM-dd hh:mm');
    } else {
        return "";
    }
}

//format date
function formatDateTime(data) {
    if (data != null && data != undefined && data != "") {
    data = data.replace("T", " ").split('.')[0];
    data = data.replace(/-/g, "/");
    var date = new Date(data);
    return date.format('yyyy-MM-dd');
    } else {
        return "";
    }
}



//解析错误信息
function ErrorResponse(obj, url) {
    layer.closeAll();
    var errormes = "";
    if (typeof (obj) != "object") {
        if (obj.toLowerCase().indexOf("<title>401</title>") >= 0) {
            layer.alert("请重新登录！", function () {
                window.location.href = "/pages/login.aspx";
            });
            return;
        }
        else {
            try {
                errormes = JSON.parse(obj.responseText);
            }
            catch (e) {
                layer.alert("系统异常，请联系系统管理员！异常信息:" + obj);
                return;
            }
        }
    }
    else {
        errormes = obj;
        if (errormes.responseText && errormes.responseText.toLowerCase().indexOf("<title>401</title>") >= 0) {
            layer.alert("请重新登录！", function () {
                window.location.href = "/pages/login.aspx";
            });
            return;
        }
    }

    var returnerrorcode = "";
    var errorMsg = "";

    if (errormes.HttpStatusCode) {
        returnerrorcode = errormes.HttpStatusCode;
        errorMsg = errormes.Message;
    }
    else if (errormes.responseText) {
        var response = JSON.parse(errormes.responseText);
        returnerrorcode = response.HttpStatusCode;
        errorMsg = response.Message;
    }

    if (errormes.ExceptionMessage) {
        errorMsg += " " + errormes.ExceptionMessage;
    }

    if (returnerrorcode == "401" || returnerrorcode == undefined) {
        var locationUrl = window.location.href;
        if (errorMsg.length > 0) {
            var layindex = layer.alert(errorMsg, function () {
                window.location.href = "/pages/login.aspx";
            });
        }
    }
    else if (returnerrorcode == "303") {
        if (errorMsg.length > 0) {
            var layindex = layer.alert(errorMsg, function () {
                if (url && url.length > 0) {
                    window.location.href = url;
                }
                layer.close(layindex);
            });
        }
    }
    else {
        if (errorMsg.length > 0) {
            layer.alert(errorMsg);
        }
    }
}
//angularjs error response
//解析错误信息
function AngularErrorResponse(obj) {
    layer.closeAll();
    layer.alert(obj);
}
//////////////////
function MessageAlert(msg, url) {
    layer.alert(msg, 
    {
        end: function (index) {
            if (url) {
                window.location.href = url;
            }
        }
    });
}

function clearNoNum(obj) {
    obj.value = obj.value.replace(/[^\d.]/g, ""); //清除"数字"和"."以外的字符
    obj.value = obj.value.replace(/^\./g, ""); //验证第一个字符是数字而不是
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'); //只能输入两个小数
}

function selectMenu(menu) {
    $(".nav-list").children().each(function () {
        $(this).removeClass("selected");
    });

    $(menu).addClass("selected");

    $(".side-nav").children().each(function () {
        $(this).hide();
    });

    var topMID = $(menu).attr("id");
    var menuID = topMID.replace("li", "ul");

    $(".side-nav").find("ul[id=" + menuID + "]").show();
}

//add by tim
function logout() {
    layer.confirm('您确定要退出吗？', {
        btn: ['确定', '取消'] //按钮
    }, function () {
        window.location.href = '/Pages/Login.aspx';

    });

}

function CurrentUserIsAdmin() {
    if ($(".HD_CURRENT_IS_ADMIN").val() == "Y")
        return true;
    return false;
}

function CurrentUserIsAreaUser() {
    if ($(".HD_CURRENT_IS_AREA_USER").val() == "Y")
        return true;
    return false;
}

function CurrentUser() {
    return $(".HD_CURRENT_USER").val();
}

function InitFactor() {
    $("#divFactorName").html($("#HD_CURFACTOR").val());
    var url = "/api/AllFactor?r=" + Math.random();
    $(".sub-nav-list").html("");
    $.ajax({
        type: "GET",  //提交方式  
        url: url,//路径  
        success: function (result) {//返回数据根据结果进行相应的处理  
            var data = result.Data;
            $.each(data, function (i, o) {
                $(".sub-nav-list").append('<li fid="' + o.FactoringId + '" onclick="SelectFactor($(this).attr(\'fid\'))">' + o.FactoringName + '</li>');
            });
        }
    });
}

function SelectFactor(id) {
    var url = "/api/SelectFactor?factorID=" + id;
    $.ajax({
        type: "POST",  //提交方式  
        url: url,//路径  
        success: function (result) {//返回数据根据结果进行相应的处理  
            window.location.href = '/Pages/Index.aspx';
        }
    });
}

function GetCurrentFactor() {
    return $(".HD_FACTORID").val();
}

function CheckData(obj) {
    if (obj.MessageType != undefined && obj.MessageType != 1) {
        layer.alert(obj.Data);
        return false;
    }

    return true;
}

function NumToChinese(num) {
    if (!/^\d*(\.\d*)?$/.test(num)) {
        return null;
    }
    var addZero = false;
    var AA = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");
    var BB = new Array("", "拾", "佰", "仟", "万", "亿", "圆", "", "角", "分");
    var a = ("" + num).replace(/(^0*)/g, "").split("."), k = 0, re = "";
    for (var i = a[0].length - 1; i >= 0; i--) {
        switch (k) {
            case 0: re = BB[7] + re; break;
            case 4: if (!new RegExp("0{4}\\d{" + (a[0].length - i - 1) + "}$").test(a[0]))
                re = BB[4] + re; break;
            case 8: re = BB[5] + re; BB[7] = BB[5]; k = 0; break;
        }

        if (k % 4 != 0) {
            if (a[0][i] == 0) {
                if (!addZero) {
                    re = AA[0] + re;
                    addZero = true;
                }
            }
        }
        else {
            addZero = false;
        }

        if (a[0].charAt(i) != 0) {
            re = AA[a[0].charAt(i)] + BB[k % 4] + re;
        }
        k++;
    }

    if (a.length > 1) //加上小数部分(如果有小数部分) 
    {
        re += BB[6];
        var bindex = 8;
        for (var i = 0; i < a[1].length; i++) {
            if (a[1][i] != 0) {
                re += AA[a[1].charAt(i)] + BB[bindex];
            }
            if (bindex < BB.length) {
                bindex++;
            }
        }
    }
    else {
        re += "圆整";
    }
    return re;
}
//生成Guid
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

function showloading() {
    var loadingshadeindex = layer.load(2, { shade: [0.2, '#393D49'] });
}

function hidloading() {
    layer.closeAll('loading');
}