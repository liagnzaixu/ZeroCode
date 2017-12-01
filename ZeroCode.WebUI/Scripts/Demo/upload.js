
var lastTime;
var loadSize;

//jcrop 所需参数
var jcrop_api;
var boundx;
var boundy;

var $pimg_small;
var $pimg_large; 
        
var xsize_small = 48;
var ysize_small = 48;
var xsize_large = 180;
var ysize_large = 180;

$(function () {
    $pimg_small = $('#preview_small_img');
    $pimg_large = $('#preview_large_img');
})

window.onload = function () {
    var messager = document.getElementById('messager');
    messager.onclick = closeMessager;

    var btnReset = document.getElementById('btn_resetProgress');
    btnReset.onclick = function () { setProgressBar(0, "kb/s", 'hide', "0%"); }

    var btnUpload = document.getElementById('btnUpload');
    btnUpload.onclick = function () {
        var fileInput = document.getElementById('File2');
        if (!fileInput.value) { return; }

        var feature = {};

        feature.fileapi = fileInput.files !== undefined;
        feature.formdata = (typeof window.FormData !== 'undefined');

        if (!feature.fileapi || !feature.formdata) {
            showMessage('当前浏览器不支持Html5，请更换高级浏览器');
            return;
        }

        var xhr = new XMLHttpRequest();
        var formData = new FormData();
        formData.append("UserPhoto", fileInput.files[0]);
        xhr.open("post", './UploadFile_2');

        //超时时间，单位是毫秒
        xhr.timeout = 60 * 1000;

        //请求结束
        xhr.onload = function (event) {
            if (xhr.status == 200) {
                showMessage("上传成功");
                return;
            }

            if (xhr.status == 404) {
                showMessage("请求路径错误");
                setProgressBar(0, 'kb/s', 'hide', '0%');
                return;
            }

            if (xhr.status == 500) {
                console.error('xhr.status == 500');
                showMessage("服务器异常");
            }
        };

        //请求异常(不包括404)
        xhr.onerror = function () {
            console.error('xhr.onerror');
            showMessage("请求发生异常");
        };

        //上传开始事件
        xhr.upload.onloadstart = function () {
            setProgressBar(0, 'kb/s', 'hide', '0%');
            lastTime = new Date().getTime();
            loadSize = 0;
        }

        //上传过程事件，间歇调用该方法用来获取上传过程中的信息
        xhr.upload.onprogress = function (event) {
            //ProgressEvent.lengthComputable：boolean类型，表示能否计算出文件长度；如果为false，那么ProgressEvent.total则为0.
            //简而言之，lengthComputabl等于true就可以做进度计算
            if (!event.lengthComputable) {
                return;
            }

            //计算上传速度
            var now = new Date().getTime();
            var timeInterval = (now - lastTime) / 1000;
            lastTime = now;

            var sizeInterval = event.loaded - loadSize;
            loadSize = event.loaded;

            var speed = sizeInterval / timeInterval; // 单位b/s
            var bspeed = speed;
            var units = "b/s";

            if (speed / 1024 > 1) {
                speed = speed / 1024;
                units = 'kb/s';
            }
            if (speed / 1024 > 1) {
                speed = speed / 1024;
                units = 'mb/s';
            }

            speed = speed.toFixed(1);

            //计算剩余时间
            var resttime = ((event.total - event.loaded) / bspeed).toFixed(1); //单位 秒

            //计算进度百分比
            var precent = (100 * event.loaded / event.total);
            pre = Math.floor(precent);

            if (bspeed == 0) {
                setProgressBar(0, "b/s", 'infinity', 'noChange');
            } else {
                setProgressBar(speed, units, resttime, precent + '%');
            }
        };

        //上传终止
        xhr.upload.onabort = function () {
            console.info('xhr.upload.onabort');
        }

        //上传异常事件
        //当网络环境正常时，XMLHttpRequest.status等于404、500并不能触发upload.onerror事件
        //当网络环境异常时（offline）,会触发upload.onerror事件
        xhr.upload.onerror = function () {
            console.error('xhr.upload.onerror');
        }

        //文件发送完毕事件
        //当网络环境正常时，即使XMLHttpRequest.status等于404、500，仍会被触发upload.onload事件
        //当网络环境异常时（offline）,不会触发upload.onload事件
        xhr.upload.onload = function () {
            console.info('xhr.upload.onload');
        }

        //上传超时
        xhr.upload.ontimeout = function () {
            console.error('xhr.upload.ontimeout');
        }

        //发送请求
        xhr.send(formData);
    }

    $('#btnUpload3').click(function () {
        var $fileInput = $('#File3');
        if (!$fileInput.val()) { return; }

        var feature = {};
        //检查是否支持html5 api
        feature.fileapi = $fileInput.get(0).files !== undefined;
        feature.formdata = (typeof window.FormData !== 'undefined');

        if (!feature.fileapi || !feature.formdata) {
            showMessage('当前浏览器不支持Html5，请更换高级浏览器');
            return;
        }

        var formData = new FormData();
        formData.append("UserPhoto", $fileInput[0].files[0]);
        $.ajax({
            type: 'post',
            url: './UploadFile_2',
            data: formData,
            processData: false,//设置为false。因为data值是FormData对象，不需要对数据做处理。
            contentType: false, //设置为false。告诉jQuery不要去设置Content-Type请求头
            xhr: function () {
                var xhr = $.ajaxSettings.xhr();

                if (xhr.upload) {
                    xhr.upload.onloadstart = uploadStart;
                    xhr.upload.onprogress = uploadProgress;
                    return xhr;
                }
            },
            success: function (data, textStatus, jqXHR) {
                showMessage("上传成功");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showMessage("上传失败");
            }
        });
    });

    $('#btnUpload4').click(function () {
        var $fileInput = $('#File4');
        if (!$fileInput.val()) { return; }

        var options = {
            type: 'post',
            url: './UploadFile_2',
            success: function (data, textStatus, jqXHR) {
                showMessage("上传成功");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showMessage("上传失败");
            }
        }

        var feature = {};

        //检查是否支持html5 api
        feature.fileapi = $fileInput.get(0).files !== undefined;
        feature.formdata = (typeof window.FormData !== 'undefined');

        //如果支持html5，那么就使用进度条
        if (feature.fileapi && feature.formdata) {
            options = $.extend(options, {
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();

                    if (xhr.upload) {
                        xhr.upload.onloadstart = uploadStart;
                        xhr.upload.onprogress = uploadProgress;
                        return xhr;
                    }
                }
            });
        }

        $("#form4").ajaxSubmit(options);
    });

    //$('#File5').change(function () {
    //    preview(document.getElementById('File5'));
    //});

    //var img1 = document.getElementById("previewImg");
    ////火狐和IE兼容的获得鼠标滚轮信息
    //if (img1 && img1.addEventListener) {
    //    img1.onmousewheel = scrollfun;
    //    img1.addEventListener("DOMMouseScroll", scrollfun, false);
    //}

    $("#crop_img").load(function () {
        $("#crop_img").Jcrop({
            onChange: updatePreview,
            onSelect: updateCropData,
            aspectRatio: 1
        }, function () {
            jcrop_api = this;
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            var size = Math.min(boundx, boundy);
            jcrop_api.setSelect([0, 0, size, size]);
        });
    });

    $('#form_upload').ajaxForm({
        dataType: "json",
        data:$('#form_upload').serialize(),
        success: function (data, textStatus, jqXHR) {
            if (data.Status == 'success') {
                $("#crop_img,#preview_small_img,#preview_large_img").attr('src', data.ImgUrl);
                $("#url").val(data.ImgUrl);
                if (jcrop_api != null) {
                    jcrop_api.setImage(data.ImgUrl, function () {
                        var bounds = jcrop_api.getBounds();
                        boundx = bounds[0];
                        boundy = bounds[1];
                        var size = Math.min(boundx, boundy);
                        jcrop_api.setSelect([0,0,size,size]);
                    });
                }
                return;
            }
            if (data.Status == 'error') {
                showMessage(data.Msg);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            showMessage('上传失败');
            console.error(textStatus);
        }
    });

    $('#form_crop').ajaxForm({
        dataType: "json",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == 'success') {
                showMessage('保存成功');
                jcrop_api.destroy();
                return;
            }
            if (data.Status == 'error') {
                showMessage(data.Msg);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            showMessage('请求错误');
            console.error(textStatus);
        }
    });


}

function showMessage(txt, time) {
    var time = time || 2000;
    var msger = document.getElementById('messager');
    var msgerTxt = document.getElementById('messagerTxt');

    msgerTxt.innerHTML = txt;
    msger.style.display = "block";
    setTimeout('closeMessager()', time);
}

function closeMessager() {
    var msger = document.getElementById('messager');
    msger.style.display = "none";
}

function uploadStart() {
    setProgressBar(0, 'kb/s', 'hide', '0%');
    lastTime = new Date().getTime();
    loadSize = 0;
}

function uploadProgress(event) {
    //ProgressEvent.lengthComputable：boolean类型，表示能否计算出文件长度；如果为false，那么ProgressEvent.total则为0.
    //简而言之，lengthComputabl等于true就可以做进度计算
    if (!event.lengthComputable) {
        return;
    }

    //计算上传速度
    var now = new Date().getTime();
    var timeInterval = (now - lastTime) / 1000;
    lastTime = now;

    var sizeInterval = event.loaded - loadSize;
    loadSize = event.loaded;

    var speed = sizeInterval / timeInterval; // 单位b/s
    var bspeed = speed;
    var units = "b/s";

    if (speed / 1024 > 1) {
        speed = speed / 1024;
        units = 'kb/s';
    }
    if (speed / 1024 > 1) {
        speed = speed / 1024;
        units = 'mb/s';
    }

    speed = speed.toFixed(1);

    //计算剩余时间
    var resttime = ((event.total - event.loaded) / bspeed).toFixed(1); //单位 秒

    //计算进度百分比
    var precent = (100 * event.loaded / event.total);
    pre = Math.floor(precent);

    if (bspeed == 0) {
        setProgressBar(0, "b/s", 'infinity', 'noChange');
    } else {
        setProgressBar(speed, units, resttime, precent + '%');
    }
};

function setProgressBar(speed, units, restTime, precent) {
    var txtSpead = document.getElementById('speed');
    var txtUnits = document.getElementById('units');
    var progressBar = document.getElementById('progressBar');
    var progressPre = progressBar.firstElementChild;

    txtSpead.innerText = speed;
    txtUnits.innerText = units;

    if (precent == 'noChange') {
        return;
    }

    progressBar.style.width = precent;

    if (precent == "0%") {
        precent = "";
    }
    progressPre.innerText = precent;
}

function preview(file) {
    var prevDiv = document.getElementById('preview');
    if (file.files && file.files[0]) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            var imgMM = document.getElementById('previewImg');
            imgMM.src = evt.target.result;
            imgMM.style.display = 'inline';
        }
        reader.readAsDataURL(file.files[0]);
    }
    else {
        prevDiv.innerHTML = '<div class="img" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>';
    }
}

function scrollfun(e) {
    var ev = e || window.event;//兼容分别为IE和火狐
    var num = ev.wheelDelta || ev.detail;//兼容分别为IE和火狐
    //alert(num);
    if ((num == -3 || num == 120) && img1.offsetWidth < 900) {
        img1.style.width = (img1.offsetWidth) * 1.2 + "px";
        img1.style.height = (img1.offsetHeight) * 1.2 + "px";
    }
    else if ((num == 3 || num == -120) && img1.offsetWidth > 60) {
        img1.style.width = (img1.offsetWidth) / 1.2 + "px";
        img1.style.height = (img1.offsetHeight) / 1.2 + "px";
    }
    ev.preventDefault();
}

function updatePreview(c) {
    if (parseInt(c.w) > 0) {
        var rx_large = xsize_large / c.w;
        var ry_large = ysize_large / c.h;

        $pimg_large.css({
            width: Math.round(rx_large * boundx) + 'px',
            height: Math.round(ry_large * boundy) + 'px',
            marginLeft: '-' + Math.round(rx_large * c.x) + 'px',
            marginTop: '-' + Math.round(ry_large * c.y) + 'px'
        });

        var rx_small = xsize_small / c.w;
        var ry_small = ysize_small / c.h;

        $pimg_small.css({
            width: Math.round(rx_small * boundx) + 'px',
            height: Math.round(ry_small * boundy) + 'px',
            marginLeft: '-' + Math.round(rx_small * c.x) + 'px',
            marginTop: '-' + Math.round(ry_small * c.y) + 'px'
        });
    }
};

function updateCropData(c) {
    jQuery("#x").val(c.x);
    jQuery("#y").val(c.y);
    jQuery("#w").val(c.w);
    jQuery("#h").val(c.h)

    console.group('updateCropData');
    console.info(c.x)
    console.info(c.y)
    console.info(c.w)
    console.info(c.h)
    console.groupEnd('updateCropData');
}


