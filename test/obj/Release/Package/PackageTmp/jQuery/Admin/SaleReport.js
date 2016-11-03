$(function () {
    var selectData = [], selectKey = [];
    var index = $("#DataTable th[data-name='shopid']").index("#DataTable th") || 0;
    var oTable = $("#DataTable").detailView({
        "order": [[index, 'asc']],
        "rowCallback": function (row, data) {
            var shopid = data["shopid"];
            return row;
        }
    });
    $(".dataTables_scroll").attr("style", "width:520px");
    $(".dataTables_scrollBody").attr("style", "width:450px");
    $("#DataTable_paginate").attr("style", "width:300px");
    $.each(oTable.api().settings()[0].aoColumns, function (i, o) {
        if (o.mData == "checkbox")
            o.bSortable = false;
        else if (o.mData == "viewIcon")
            o.bSortable = false;
    });

    $("#filter-btn").unbind("click").bind("click", function () {
        selectKey = [];
        selectData = [];
        //oTable.api().clearPipeline();
        oTable.fnDraw();
    });

});
var submittype = 1;
var result;
var xcol;
var submiturl;
var title;
//function refresh() {
//    echarts.init(document.getElementById('main')).setOption(option);
//}
require.config({
    paths: {
        echarts: '../../Scripts/echart/'
    }
});
//var d = new Date();
//var n = (d.getMonth() + 1);
//var y = (d.getYear() + 1900);
var head = [];
var body = [{
    name: '',
    type: 'bar',
    data: body,
    markPoint: {
        data: [
            { type: 'max', name: '最大值' },
            { type: 'min', name: '最小值' }
        ]
    }
}];
var option = {};
var echarts;
var total=true;
var income=true;
var final=true;
function checkclick() {
    if ($($("#chart input")[0]).is(":checked")) {
        total=true
    } else {
        total = false;
    }
    if ($($("#chart input")[1]).is(":checked")) {
        income = true
    } else {
        income = false;
    }
    if ($($("#chart input")[2]).is(":checked")) {
        final = true
    } else {
        final = false;
    }
    submitdata(submittype);
    
}
function submitdata(type) {
    if (type == 1) {
        submiturl = "SaleReportMonth";
        title = "月損益比較";
    } else if (type == 2) {
        submiturl = "SaleReportSeason";
        title = "季損益比較";
    } else if (type == 3) {
        submiturl = "SaleReportYear";
        title = "年損益比較";
    }
    $.getJSON(submiturl, { total: total, income: income, final: final }, function (res) {
        result = res;
        if (res.length==1) {
            body = [
            {
                name: '',
                type: 'bar',
                data: ['']
            }
            ];
            head = [];
        } else {
            body = [];
            head = res[0];
            xcol = res[1];
        }
        
        
        for (var i = 2; i < res.length; i++) {
        body.push({
            name: res[0][i-2],
            type: 'bar',
            data: res[i]
        });
        }
        
        option = {
            title: {
                text: title,
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: head
            },
            toolbox: {
                show: true,
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: xcol
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: body
        };
        echarts.init(document.getElementById('main')).setOption(option);

    });
}
require(['echarts',
        'echarts/chart/line',
        'echarts/chart/bar'], function (ec) {
            echarts = ec;
            var myChart = ec.init(document.getElementById('main'));
            submitdata(submittype);
        });
