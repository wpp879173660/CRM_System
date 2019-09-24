
$.extend({
    datetime: function (time) {
        if (time == "" || time == null) {
            return "";
        }
        var str = parseFloat(time.substr(6));
        var t = new Date(str);
        return t.getFullYear() + "-" + (t.getMonth() + 1 < 10 ? '0' + (t.getMonth() + 1) : t.getMonth() + 1) + "-" + t.getDate();
    }
});


$.extend({
    myajax: function (url, data, callBack) {
        $.ajax({
            type: "POST",
            url: url,
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",//
            data: data == null ? "" : JSON.stringify(data),
            success: function (d) {
                callBack(d.d);
            }

        });
        
    }
});

$.fn.extend({
    tbs: function (table, d, ops) {
        var tbs = $(this);
        var tr = $("<tr>");
        $.each(table, function (k, v) {
            $("<th>").text(v).appendTo(tr);
           
        })
        tbs.append(tr);
        $.each(d, function (kk, vv) {
            var tr = $("<tr>").appendTo(tbs);
            $.each(table, function (key, val) {
                var td = $("<td>").text(vv[key]).css("text-align", "center").appendTo(tr);
                if (vv[key] == undefined) {
                    if (ops != undefined) {
                        $.each(ops, function (tit, ckk) {
                            var input = $(ckk.toolname).text(ckk.text)
                                .click(function () {
                                    ckk.click(vv,$(this).parent(). parent());
                                })
                                .appendTo(td);
                            $.each(ckk.attrs, function (k, v) {
                                $(input).attr(k, v);
                            })
                        });
                    }
                };
            })
            $(tbs).append(tr);
        })
        
    },
    setpage: function (thead, d, ops,page) {
        //console.log(page);
        var tbs = $(this);
        tbs.empty();
        tbs.tbs(thead, d, ops);
        //$.each(d.biao, function (k, v) {
        //    var tr = $("<tr>").appendTo(tbs);
        //    $("<td>").text(v.UserID).css("text-align", "center").appendTo(tr);
        //    $("<td>").text(v.UserLName).css("text-align", "center").appendTo(tr);
        //    $("<td>").text(v.UserName).css("text-align", "center").appendTo(tr);
        //    $("<td>").text(v.RoleName).css("text-align", "center").appendTo(tr);
        //    var users = { "RoleName": v.RoleName, "UserLName": v.UserLName, "UserLPWD": v.UserLPWD, "UserName": v.UserName, "RoleID": v.RoleID, "UserID": v.UserID };
        //    $("<td <td><img title=修改 src='../images/edt.gif' onclick='showEdit(" + JSON.stringify(users) + ")' />&nbsp;&nbsp;<img title=删除 src='../images/del.gif' /></td>>").css("text-align", "center").appendTo(tr);
        //});

        //var tbs = $(this);
        //tbs.empty();
        //tbs.tbs(table, ps, shijian, date);
        //var div = $("#divs");
        //if (div.size() > 0) {
        //    div.empty();
        //} else {
        //    div = $("<div id='divs'>")
        //    div.insertAfter(tbs);
           
        //}
        var index = page.pindex;
        var count = page.pcount;
        var callBack = page.pck;
        var size = page.psize;
        
        $("#leftPage").text("共有" + count + "条记录，当前第" + index + " / " + count + "页");
        //    var showSpan = $("<span>").text("当前是" + index + "页/共" + count + "页").appendTo(div);
        $("#a").click(function () {
            callBack(1, size);
        })
        $("#b").click(function () {
            callBack(index = index == 1 ? 1 : index - 1, size);
        })
        $("#c").click(function () {
            callBack(index = index == count ? count : index + 1, size);
        })
        $("#d").click(function () {
            callBack(index = count, size);
        })
        $("#e").click(function () {
            //if ($("#fxm") == undefined) {
              // callBack($("input[type=text]").val() >= count ? count : $("input[type=text]").val(), size);
            //} else {
            callBack($("#fxm").val() >= count ? count : $("#fxm").val(), size);
            
        })
    //    var first = $("<a>").text("首页").attr("href", "#")
    //        .click(function () {
    //            callBack(1, size)
    //        })
    //        .appendTo(showSpan);

    //    var prev = $("<a>").text("上一页").attr("href", "#")
    //        .click(function () {
    //            callBack(index = index == 1 ? 1 : index - 1, size);
    //        })
    //        .appendTo(showSpan);

    //    var next = $("<a>").text("下一页").attr("href", "#")
    //        .click(function () {
    //            callBack(index = index == count ? count : index + 1, size);
    //        })
    //        .appendTo(showSpan);

    //    var last = $("<a>").text("尾页").attr("href", "#")
    //        .click(function () {
    //            callBack(count, size);
    //        })
    //        .appendTo(showSpan);

    //    $(div).find("a").css("margin-left", "5px");
    }

    
    });


