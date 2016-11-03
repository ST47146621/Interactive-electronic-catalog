$(function () {
    var selectData = [], selectKey = [];
    var index = $("#DataTable th[data-name='productid']").index("#DataTable th") || 0;
    var oTable = $("#DataTable").detailView({
        "order": [[index, 'asc']],
        "rowCallback": function (row, data) {
            var productid = data["productid"];
            
            $("td.td_productimgurl1", row).attr("ondrop", "Drop(event)");
            $("td.td_productimgurl1", row).attr("draggable", "true");

            //$("td.td_productimgurl1", row).draggable({ revert: "invalid", drag: function () { revert: true }, stop: function () { /*alert("stop");*/} });
            //$("#chart").droppable({ drop: function (b, a) { $(a.draggable[0]).attr("style", "positon:relative;"); $(a.draggable[0]).draggable({ revert: "invalid" }) } });
            

            $("td.td_productimgurl1", row).attr("ondragstart", "Drag(event)");
            $("td.td_productimgurl1", row).attr("onmouseover", "BPic($(this))");
            $("td.td_productimgurl1", row).attr("onmouseout", "SPic($(this))");
            //$("td.td_productimgurl1", row).attr("onmousedown", "if ($(this).data('drag')!='1'){Drag(event);$(this).attr('data-drag','1');}else{DropPic($(this));$(this).attr('data-drag','0')}");


            $("td.td_productimgurl1", row).attr("data-id", $("td.td_productimgurl1", row).children("div").data("id"));
            $("td.td_productimgurl1", row).attr("data-img", $("td.td_productimgurl1", row).children("div").data("img"));
            $("td.td_productimgurl1", row).attr("id", $("td.td_productimgurl1", row).children("div").data("id"));
            var ischeck = 0;
            for (var i = 0; i < submitproduct.length; i++) {
                console.log(submitproduct[i] + ", " + $("td.td_productimgurl1", row).data("id"));
                if ($("td.td_productimgurl1", row).data("id") == submitproduct[i]) {
                    $("td.td_checkbox", row).html("<input type='checkbox' class='select_row' onclick='checkclick($(this));' checked />"); ischeck = 1;
                }
            }
            if (ischeck == 0) {
                $("td.td_checkbox", row).html("<input type='checkbox' class='select_row' onclick='checkclick($(this));'/>");
            }
            if (selectKey.indexOf(productid) > -1) {
                $("td.td_checkbox", row).children("input[type='checkbox']").prop("checked", true);
            }

            return row;
        }
    });
    $(".dataTables_scroll").attr("style", "width:430px");
    $(".dataTables_scrollBody").attr("style", "width:500px");
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

    $("#DataTable tbody").on("change", "tr input[type='checkbox']", function () {
        var $this = $(this);
        var productid = $(this).closest("tr").children(".td_productid").text();
        if ($this.is(":checked")) {
            selectKey.push(productid);
            selectData.push(do_PushReturnData($(this).closest("tr")));
        }
        else {
            selectKey.splice(selectKey.indexOf(productid), 1);
            for (var i = 0; i < selectData.length; i++) {
                if (selectData[i][0] == productid) {
                    selectData.splice(i, 1);
                }
            }
        }
    });

    $("#selectAll-btn").click(function () {
        $("#unselectAll-btn, #multiSelect-btn").attr("disabled", "disabled");
        $("#DataTable tbody tr").each(function () {
            var get_tr = $(this);
            var productid = get_tr.children(".td_productid").text();
            var checkbox = get_tr.children(".td_checkbox").children("input[type='checkbox']");
            if (!checkbox.is(":checked")) {
                checkbox.prop("checked", true);
                selectKey.push(productid);
                selectData.push(do_PushReturnData(get_tr));
            }
        });
        $("#unselectAll-btn, #multiSelect-btn").removeAttr("disabled");
    });

    $("#unselectAll-btn").click(function () {
        $("#selectAll-btn, #multiSelect-btn").attr("disabled", "disabled");
        $("#DataTable tbody tr").each(function () {
            var get_tr = $(this);
            var productid = get_tr.children(".td_productid").text();
            var checkbox = get_tr.children(".td_checkbox").children("input[type='checkbox']");
            if (checkbox.is(":checked")) {
                checkbox.prop("checked", false);
                selectKey.splice(selectKey.indexOf(org), 1);
                for (var i = 0; i < selectData.length; i++) {
                    if (selectData[i][0] == org) {
                        selectData.splice(i, 1);
                    }
                }
            }
        });
        $("#selectAll-btn, #multiSelect-btn").removeAttr("disabled");
    });

    $("#multiSelect-btn").click(function () {
        console.log(selectData);
        if (selectData.length < 1) {
            return false;
        }

        var target = opener.getSearchTarget();

        opener.SetMultiRowData(target, selectData);

        oTable = null;
        window.close();
    });

});

function do_PushReturnData($this) {
    var productid = $this.children(".td_productid").text();
    var productname = $this.children(".td_productname").text();
    return [productid, { productid: productid, productname: productname }];
}
function checkclick(inp) {
    if (inp.is(":checked")) {
        submitproduct.push($(inp.parent().parent().children("td")[1]).text());
        submitdata(submittype);
    } else {
        for (var i = 0; i < submitproduct.length; i++) {
            if (submitproduct[i] == $(inp.parent().parent().children("td")[1]).text()) {
                submitproduct.splice(i, 1);
            }
        }
        submitdata(submittype);
    }
}
var result;
var xcol = ['周一', '周二', '周三', '周四', '周五'];
var submiturl;
var title;
function submitdata(type) {
    switch (type) {
        case 1:
            submiturl = "WeekChart";
            xcol = ['周一', '周二', '周三', '周四', '周五'];
            title = n + '月份-周銷售量比較';
            break;
        case 2:
            submiturl = "MonthChart";
            xcol = ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'];
            title = y + "年-月銷售量比較";
            break;
        case 3:
            submiturl = "YearChart";
            xcol = ["" + (y - 2) + "年", "" + (y - 1) + "年", "" + y + "年"];
            title = (y - 2) + "~" + y + "年銷售量比較";
            break;
    }
    $.ajaxSettings.async = false;
    $.getJSON(submiturl, { data: JSON.stringify(submitproduct) }, function (res) {
        
        result = res;
        head = [];
        
        if (res == "null") {

            body = [
            {
                name: '',
                type: 'line',
                data: [],
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                }
            }
            ];
            head = [];
        } else {
            body = [];
            head = res[0];

        }
        switch (submittype) {
            case 1:
                for (var i = 1; i < res[1].length; i++) {
                    body.push({
                        name: res[0][i - 1],
                        type: 'line',
                        data: [res[1][i], res[2][i], res[3][i], res[4][i], res[5][i]],
                        markPoint: {
                            data: [
                                { type: 'max', name: '最大值' },
                                { type: 'min', name: '最小值' }
                            ]
                        }
                    });
                }
                break;
            case 2:
                for (var i = 1; i < res[1].length; i++) {
                    body.push({
                        name: res[0][i - 1],
                        type: 'line',
                        data: [res[1][i], res[2][i], res[3][i], res[4][i], res[5][i], res[6][i], res[7][i], res[8][i], res[9][i], res[10][i], res[11][i], res[12][i]],
                        markPoint: {
                            data: [
                                { type: 'max', name: '最大值' },
                                { type: 'min', name: '最小值' }
                            ]
                        }
                    });
                }
                break;
            case 3:
                for (var i = 1; i < res[1].length; i++) {
                    body.push({
                        name: res[0][i - 1],
                        type: 'line',
                        data: [res[1][i], res[2][i], res[3][i]],
                        markPoint: {
                            data: [
                                { type: 'max', name: '最大值' },
                                { type: 'min', name: '最小值' }
                            ]
                        }
                    });
                }
                break;
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
                            boundaryGap: false,
                            data: xcol
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            axisLabel: {
                                formatter: '{value}'
                            }
                        }
                    ],
                    series: body
                };
                refresh();
            
    });
}
function refresh() {
    echarts.init(document.getElementById('main')).setOption(option);
}
require.config({
    paths: {
        echarts: '../../Scripts/echart/'
    }
});
var d = new Date();
var n = (d.getMonth() + 1);
var y = (d.getYear()+1900);
var head = [];
var body = [{
    name: '',
    type: 'line',
    data: body,
    markPoint: {
        data: [
            { type: 'max', name: '最大值' },
            { type: 'min', name: '最小值' }
        ]
    }
}];
var option = {
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
            boundaryGap: false,
            data: xcol
        }
    ],
    yAxis: [
        {
            type: 'value',
            axisLabel: {
                formatter: '{value}'
            }
        }
    ],
    series: [
            {
                name: '',
                type: 'line',
                data: [],
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                }
            }
    ]
};
var echarts;
require(['echarts',
        'echarts/chart/line',
        'echarts/chart/bar'], function (ec) {
            echarts = ec;
            var myChart = ec.init(document.getElementById('main'));
            myChart.setOption(option);
            body = [];
        });
