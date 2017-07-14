(function(window){

    var tool = {
        closeModalWindow: function () {
            $(tempObj.selector.modal_window).window('close');
            window.frames[modalFrame].location.replace('about:blank');
        },
        reloadDatagrid: function (reload) {
            $(tempObj.selector.list).datagrid(reload ? 'reload' : 'load');
        },
        showMsgBottomRight: function (txt) {
            $.messager.show({
                title: '提示',
                msg: txt,
                showType: 'slide',
                timeout: 5000
            });
        }
    };

    var tempObj ={
        reader: function () {

            $(tempObj.selector.list).datagrid({
                url: tempObj.apiUrl.sysSampleUrl,
                width: $(window).width(),
                methord: 'post',
                height: $(window).height() - 50,
                fitColumns: true,
                sortName: 'Id',
                sortOrder: 'desc',
                idField: 'Id',
                pageSize: 15,
                pageList: [15, 20, 30, 40, 50],
                pagination: true,
                striped: true, //奇偶行是否区分
                singleSelect: true, //单选模式
                rownumbers: true, //行号
                loadFilter: $.zc.easyui.loadFilter,
                columns: [
                    [
                        { field: 'Id', title: 'ID', width: 80 },
                        { field: 'Name', title: '名称', width: 120 },
                        { field: 'Age', title: '年龄', width: 80, align: 'right' },
                        { field: 'Bir', title: '生日', width: 80, align: 'right' },
                        { field: 'Photo', title: '照片', width: 250 },
                        { field: 'Note', title: '说明', width: 60, align: 'center' },
                        { field: 'CreateTime', title: '创建时间', width: 60, align: 'center' }
                    ]
                ]
            });

            $(tempObj.selector.btnAdd).click(tempObj.registerEle.click_add);
            $(tempObj.selector.btnEdit).click(tempObj.registerEle.click_edit);
            $(tempObj.selector.btnRemove).click(tempObj.registerEle.click_remove);
            $(tempObj.selector.btnDetail).click(tempObj.registerEle.click_detail);
        },

        selector:{
            btnAdd: '#btn-add',
            btnEdit:'#btn-edit',
            btnRemove: '#btn-remove',
            btnDetail: '#btn-detail',
            list: '#List',
            modalWindow: '#modal-window',
            modalFrame: 'modal-frame'
        },
       
        apiUrl:{
            sysSampleUrl: '/SysSample/GetSysSample'
        },

        registerEle:{
            click_add: function () {
                window.frames[tempObj.selector.modalFrame].location.replace('/SysSample/Create');
                $(tempObj.selector.modalWindow).window({
                    title: "新增",
                    width: 700,
                    height: 400,
                    iconCls: "icon-add"
                }).window("open");
            },

            click_edit: function () {
                var row = $(tempObj.selector.list).datagrid('getSelected');
                if (row == null) { return; }

                window.frames[tempObj.selector.modalFrame].location.replace('/SysSample/Edit?Id=' + row.Id);
                $(tempObj.selector.modalWindow).window({
                    title: "编辑",
                    width: 700,
                    height: 400,
                    iconCls: "icon-edit"
                }).window("open");
            },

            click_detail: function () {
                var row = $(tempObj.selector.list).datagrid('getSelected');
                if (row == null) { return; }

                window.frames[tempObj.selector.modalFrame].location.replace('/SysSample/Detail?Id=' + row.Id);
                $(tempObj.selector.modalWindow).window({
                    title: "详情",
                    width: 700,
                    height: 400,
                    iconCls: "icon-detail"
                }).window("open");
            },

            click_remove: function () {
               
            }

        },

        ajaxRequest:{
           
        },

        callback:{
            
        },

        tool:{
            
        },
       
        temp:{

        },
       
        business: {
            frameReturnByCreate: function (msg, reload, close) {
                tool.showMsgBottomRight(msg);
                tool.reloadDatagrid(reload);
                close ? tool.closeModalWindow() : null;
            }
        }
    };

    var outputObj =function(){
        return tempObj;
    }

    window.pageObj = new outputObj();
})(this);

$(function () {
    pageObj.reader();
})