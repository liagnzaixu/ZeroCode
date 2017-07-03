(function(window){

    var tool = {
        
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
        },

        selector:{
            btnAdd: '#btn-add',
            list: '#List',
            modal_window:'#modal-window'
        },
       
        apiUrl:{
            sysSampleUrl: '/SysSample/GetSysSample'
        },

        registerEle:{
            click_add:function(){
                $(tempObj.selector.modal_window).html("<iframe width='100%' height='98%' scrolling='no' frameborder='0'' src='/SysSample/Create'></iframe>");
                $(tempObj.selector.modal_window).window({
                    title: "新增",
                    width: 700,
                    height: 400,
                    iconCls: "icon-add"
                }).window("open");
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
       
        firm:{

        }
    };

    var outputObj =function(){
        return tempObj;
    }

    window.pageObj = new outputObj();
})(this);

$(function() {
    pageObj.reader();
})