

//方案一(即现有方案)：
//      MsgShowType传递多个值时不支持分页,单个值可分页
//      不支持数据统计
var json1 = {
    "TotalCount": 100,                     //消息总数
    "InfoList": [{                         //消息列表
        "InfoID": "t1921",                 //消息ID
        "SysID": "C01",                    //系统ID
        "SysName": "作业系统",             //系统名称（需进行UTF-8进行解码）
        "AndroidParm": "",                 //启动安卓app的参数（需进行UTF-8进行解码）
        "IosParm": "",                     //启动IOS app的参数（需进行UTF-8进行解码）
        "MsgShowType": "1",                //消息类型
        "MsgContent": "消息内容",          //消息内容（需进行UTF-8进行解码）
        "MsgUrl": "http://..",             //web的Url链接地址（需进行UTF-8进行解码）
        "RemindDate": "提醒开始日期",      //开始提醒日期
        "ExpireDate": "提醒截止时间"       //截止时间
    }]
}

//方案二（仅添加JSON字段）：
//      MsgShowType传递多个值时不支持分页,单个值可分页
//      支持数据统计
var json1 = {
    "NormalCount":  100,                    //普通消息总数
    "TaskCount":    100,                    //任务消息总数
    "SysMemoCount": 100,                    //备忘消息总数
    "DataCount":    100,                    //资料消息总数
    "PsnCount":     100,                    //个人提醒总数
    "PsnMemoCount": 100,                    //个人备忘总数

    "InfoList": [{                         //消息列表
        "InfoID": "t1921",                 //消息ID
        "SysID": "C01",                    //系统ID
        "SysName": "作业系统",             //系统名称（需进行UTF-8进行解码）
        "AndroidParm": "",                 //启动安卓app的参数（需进行UTF-8进行解码）
        "IosParm": "",                     //启动IOS app的参数（需进行UTF-8进行解码）
        "MsgShowType": "1",                //消息类型
        "MsgContent": "消息内容",          //消息内容（需进行UTF-8进行解码）
        "MsgUrl": "http://..",             //web的Url链接地址（需进行UTF-8进行解码）
        "RemindDate": "提醒开始日期",      //开始提醒日期
        "ExpireDate": "提醒截止时间"       //截止时间
    }]
}


//方案三：支持分页、支持数据统计
var json1 = {
    "Status": 1,                           //1 成功，0 失败
    "Message": "请求成功",                 //提示消息

    "Data": {                              //返回的数据
        "NormalMsg": {                     //普通消息
            "Count": 10,                   //数据条数
            "List": { /*见上方*/}          //数据内容列表
        },
        "TaskMsg": {                       //任务消息
            "Count": 10,
            "List": {}
        },
        "SysMemoMsg": {                    //备忘消息
            "Count": 10,
            "List": {}
        },
        "DataMsg": {                       //资料消息
            "Count": 10,
            "List": {}
        },
        "PsnMsg": {                        //个人提醒
            "Count": 10,
            "List": {}
        },
        "PsnMemoMsg": {                    //个人备忘
            "Count": 10,
            "List": {}
        }
    }
}