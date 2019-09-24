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