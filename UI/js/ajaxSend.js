function MyAJAX(method,url,c)
{
    var xmlhttp = null;
    
        //内核  ie，webkit，moz
        if (window.ActiveXObject) {
            //IE
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        else {
            //非IE浏览器
            xmlhttp = new XMLHttpRequest();
        }
        if (!xmlhttp) {
            alert('您的浏览器不支持ajax');
            return false;
        }

        xmlhttp.open(method, url, false);
        xmlhttp.onreadystatechange = function ()
        {
            if(xmlhttp.readyState==4 && xmlhttp.status==200)
            {
                var res = xmlhttp.responseText;
                c(res);
            }
        }
        xmlhttp.send(null);
      //匿名函数
}