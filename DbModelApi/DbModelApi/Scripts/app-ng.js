var myApp = angular.module('myApp', []);

//日历控件
myApp.directive('idatepicker', function () {
    var link = function (scope, element, attrs, ngModel) {
        var modelName = attrs['ngModel'];
        $(element).datetimepicker({
            language: 'en',
            weekStart: 7,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            startView: 'month',
            minView: 'month',
            forceParse: 0,
            showMeridian: 1,
            format: 'yyyy-mm-dd',
        }).on('changeDate', function (ev) {
            scope.$apply(function () {
                ngModel.$setViewValue(ev.currentTarget.value);
            });
        });
    };
    return {
        require: 'ngModel',
        restrict: 'A',
        link: link
    }
});

//select2控件
myApp.directive('iselect2', function ($compile) {
    var link = function (scope, element, attrs, ngModel) {

        $(element).css("width","100%");
        $(element).select2({
            minimumResultsForSearch: Infinity
        });
        
        $compile(function () {
            $(element).trigger('change');
        });
    };
    return {
        restrict: 'A',
        require: 'ngModel',
        link: link
    }
});

//select2控件
myApp.directive('iselect22', function ($compile) {
    var link = function (scope, element, attrs, ngModel) {

        $(element).css("width", "100%");
        $(element).select2({
            minimumResultsForSearch: Infinity
        });

    };
    return {
        restrict: 'A',
        require: 'ngModel',
        link: link
    }
});


myApp.filter('fDateTime', function () {
    return function (input) {
        if (input == null || input == undefined)
            return "";
        
        return formatDateTime(input);
    }
});

myApp.filter('fDate', function () {
    return function (input) {
        if (input == null || input == undefined)
            return "";
       
        return formatDateTime(input);
    }
});

myApp.filter('fAmoutUpper', function () {
    return function (input) {
        return NumToChinese(input);
    }
});



myApp.filter('fBusinessType', function () {
    return function (input) {
        var result = '';
        if (input == '0')
            result = "供应链";
        else if (input == '1')
            result = '按揭世茂';
        else if (input == '2')
            result = '按揭非世茂';
        else if (input == '3')
            result = '其他';
        return result;
    }
});

myApp.filter('fPercent', function () {
    return function (input, NUM_QUANTILES) {
        if (input == null || input == 0 || input == "")
            return "0.00%";
        if (NUM_QUANTILES == null || NUM_QUANTILES == "")
            NUM_QUANTILES = 2;
        return (Number(input) * 100).toFixed(NUM_QUANTILES) + "%";
    }
});
//放款状态
myApp.filter('fLoanStatus', function () {
    return function (input) {
        if (input == "1")
            return "未申请";
        else if (input == "2")
            return "审批中";
        else if (input == "3")
            return "待放款";
        else if (input == "4")
            return "已办结";
        else if (input == "5")
            return "已作废";
        return "";
    }
});
//回款状态
myApp.filter('fReceivablePlanStatus', function () {
    return function (input) {
       if (input == "1")
           return "未申请";
        else if (input == "2")
            return "审批中";
        else if (input == "3")
            return "待回款";
        else if (input == "4")
            return "已办结";
        return "";
    }
});

myApp.filter('fYesOrNo', function () {
    return function (input) {
        if (input == true||input=="true")
            return "是";
        else if (input == false || input == "false")
            return "否";
        return "";
    }
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
            if (cFix == 0)
            {
                if ($(element).val().indexOf(".") != -1) {
                        $(element).val($(element).val().substr(0, $(element).val().length - 1));
                        $(element).trigger("change");
                }
            }
            else
            {
                if ($(element).val().indexOf(".") != -1) {
                    var v = $(element).val();
                    if (v.split(".")[1].length > cFix) {
                        $(element).val($(element).val().substr(0, $(element).val().length - 1));
                        $(element).trigger("change");
                    }
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