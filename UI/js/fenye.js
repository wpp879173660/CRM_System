

    function sele(i,s,size) {
        $.ajax({
            type: "post",
            url: s,
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            data: JSON.stringify({ "index": i,"size":size }),
            success: function (xrb) {
                $("#tb tbody").empty();
                $("#tb thead").empty();
                var thr = $("<tr>");
                var thead = $("<thead>");
                
                $.each(xrb.d.biao["1"], function (jj, zz) {
                    $("<td>").text(jj).appendTo(thr);
                });
                thead.append(thr);
                $("#tb").append(thead);


                //得到总列数
                var cols = thead.children().children().length;
                //得到总行数
                c = xrb.d.count;
                var tr = $("<tr>");
                var tbody = $("tbody");
                var tr;
                $.each(xrb.d.biao, function (j, z) {
                    tr = $("<tr>");
                    for (var i in z) {
                        $("<td>").text(z[i]).appendTo(tr);
                    }
                    $("tbody").append(tr);
                });
                $("#tb").append(tbody);


                //合并所有列
                $("tfoot td").attr("colspan", cols);

            }
        });
    }

