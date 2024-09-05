import Mock from 'mockjs'
//infopage用
let dataList = [
    { id: '1', name: 'boss' },
    { id: '2', name: 'manager' },
    { id: '3', name: 'worker' }
];
//login用
let loginList = [
    { username: '1', password: '1', department: '管理部' },
    { username: '2', password: '2', department: '财务部' },
    { username: '3', password: '3', department: '业务部' },
]
//department用

let signList = [
    { id: '1', issign: '0', signtime: '', issignout: '0', signouttime: '' },
    { id: '2', issign: '0', signtime: '', issignout: '0', signouttime: '' },
    { id: '3', issign: '0', signtime: '', issignout: '0', signouttime: '' },
]
let userdataList = [
    { id: '1', department: '管理部', phone: '12345' },
    { id: '2', department: '财务部', phone: '67890' },
    { id: '3', department: '业务部', phone: '13579' },
    { id: '4', department: '业务部', phone: '22222' },
] 
//身份：传了id
Mock.mock('/data/userdata', 'post', (params) => {
    let user = JSON.parse(params.body);
    const userdata = dataList.find(item => item.id === user.id);
    return {
        name: userdata.name
    };
}
)

//登录使用：传了username，password，department
Mock.mock('api/Login/IsUserUni', 'post', (params) => {
    let login = JSON.parse(params.body);
    const logindata = loginList.find(item => item.username === login.username && item.password === login.password && item.department === login.department);
    if (logindata)
        return {
            department: logindata.department,
            success: 1,
        };
    else
        return {
            success: 0,
        }
}
)

//签到时间：传了id和time
Mock.mock('/data/signdata', 'post', (params) => {
    let user = JSON.parse(params.body);
    if (user.state === '1') {
        const signdata = signList.find(item => item.id === user.id);
        if (signdata.issign === '0') {
            signdata.issign = '1';
            signdata.signtime = user.time;
            console.log(signdata);
            return {
                signtime: signdata.signtime,
                success: 1,
            }
        }
        else
            return {
                success: 0,
                signtime: signdata.signtime,
            }
    }
    else {
        const signdata = signList.find(item => item.id === user.id);
        if (signdata.issignout === '0') {
            signdata.issignout = '1';
            signdata.signouttime = user.time;
            console.log(signdata);
            return {
                signouttime: signdata.signouttime,
                success: 1,
            }
        }
        else {
            signdata.signouttime = user.time;
            return {
                success: 0,
                signouttime: signdata.signouttime,
            }
        }
    }
}
)

//部门情况需求：传了department，返回员工姓名，电话号码
Mock.mock('/data/departmentuserdata', 'post', (params) => {
    let data = JSON.parse(params.body);
    const departmentdata = userdataList.filter(item => item.department === data.department);
    if (departmentdata) {
        console.log(departmentdata);
        return departmentdata;
    }
}
)

//查看是否签到:传了id
Mock.mock('/data/checksign', 'post', (params) => {
    let data = JSON.parse(params.body);
    const signdata = signList.filter(item => item.id === data.id);
    if (signdata) {
        return signdata;
    }
}
)


//boss
//人员管理部分
let inviteList = [
    { id: '001', name: '张三', type:'实习生', salary: '10000', status: '实习中', remark: '即将结束考察' },
    { id: '002', name: '李四', type:'求职者', salary: '0', status: '未招聘', remark: '可以考虑' },
    { id: '003', name: '王二', type:'求职者', salary: '0', status: '已拒绝', remark: '不能胜任' },
] 

Mock.mock('/api/get-invite', 'get', {
    employee_list: inviteList,
})

Mock.mock('/api/details-invite', 'post', (params) => {
    let data = JSON.parse(params.body);
    const employeedata = inviteList.filter(item => item.id === data.id);
    if (employeedata) {
        console.log(employeedata);
        return employeedata;
    }
})

Mock.mock('/api/submit-invite-form', 'post', {
    message: 'invite提交成功'
})

Mock.mock('/api/delete-invite-form', 'post', {
    message: 'invite删除成功'
})

let overviewList = [
    { id: '001', name: '张三', salary: '10000', status: '休假中', remark: '无' },
    { id: '002', name: '李四', salary: '0', status: '在岗', remark: '无' },
    { id: '003', name: '王二', salary: '0', status: '出差', remark: '长期外派' },
] 


Mock.mock('/api/get-overview', 'get', {
    employee_list: overviewList,
})

Mock.mock('/api/details-overview', 'post', (params) => {
    let data = JSON.parse(params.body);
    const employeedata = overviewList.filter(item => item.id === data.id);
    if (employeedata) {
        console.log(employeedata);
        return employeedata;
    }
})

Mock.mock('/api/submit-overview-form', 'post', {
    message: 'overview提交成功'
})

Mock.mock('/api/delete-overview-form', 'post', {
    message: 'overview删除成功'
})

let trainList = [
    { id: '001', teacher: '张三', date:'9.5', student: '小王', status: '等待批准', remark: '时间不足' },
    { id: '002', teacher: '李四', date:'10.8', student: '小刘', status: '正在开课', remark: '无' },
    { id: '003', teacher: '王二', date:'6.6', student: '小王、小刘', status: '已结课', remark: '需要进行效果评估' },
] 

Mock.mock('/api/get-train', 'get', {
    employee_list: trainList,
})

Mock.mock('/api/details-train', 'post', (params) => {
    let data = JSON.parse(params.body);
    const employeedata = trainList.filter(item => item.id === data.id);
    if (employeedata) {
        console.log(employeedata);
        return employeedata;
    }
})

Mock.mock('/api/submit-train-form', 'post', {
    message: 'train提交成功'
})

Mock.mock('/api/delete-train-form', 'post', {
    message: 'train删除成功'
})

//申请管理部分
let requiredataList = [
    { id: '001', type: '维修申请', name: '张a', status: '等待批准', date: '9.1', price: '1000', remark: '照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机照相机' },
    { id: '002', type: '报销申请', name: '张b', status: '维修完成', date: '9.1', price: '1000', remark: '无人机' },
    { id: '003', type: '维修申请', name: '张c', status: '维修中', date: '9.1', price: '1000', remark: '麦克风' },
    { id: '004', type: '购买申请', name: '张d', status: '等待批准', date: '9.1', price: '1000', remark: '闪光灯' },
] 

Mock.mock('/api/requisition', 'get', {
    requisition: requiredataList,
})

Mock.mock('/api/details-req-form', 'post', (params) => {
    let data = JSON.parse(params.body);
    const requiredata = requiredataList.filter(item => item.id === data.id && item.type === data.type);
    if (requiredata) {
        console.log(requiredata);
        return requiredata;
    }
}
)

Mock.mock('/api/submit-req-form', 'post', {
    message: 'req提交成功'
})

Mock.mock('/api/delete-form', 'post', {
    message: 'req删除成功'
})


//业务管理部分
let investList = [
    { id: '003', date: '9.5', client:'银行a', price: '10000', functionary: '老李', status: '进行中', remark: '注意随时跟进' },
    { id: '002', date: '12.5', client:'yj', price: '10000', functionary: 'oceancat', status: '已经完成', remark: '无' },
    { id: '005', date: '6.6', client:'银行a', price: '50000', functionary: 'bvvd', status: '进行中', remark: '时间紧迫' },
    { id: '001', date: '2.28', client:'abcd', price: '10600', functionary: '老李', status: '进行中', remark: '6' },
] 

Mock.mock('/api/get-invest', 'get', {
    businesses_list: investList,
})

Mock.mock('/api/details-invest', 'post', (params) => {
    let data = JSON.parse(params.body);
    const businessesdata = investList.filter(item => item.id === data.id);
    if (businessesdata) {
        console.log(businessesdata);
        return businessesdata;
    }
}
)

Mock.mock('/api/submit-invest-form', 'post', {
    message: 'invest提交成功'
})

Mock.mock('/api/delete-invest-form', 'post', {
    message: 'invest删除成功'
})

let buyList = [
    { id: '003', date: '9.5', client:'tim', price: '10000', functionary: '员工2', status: '进行中', remark: '即将交付' },
    { id: '002', date: '12.5', client:'安东', price: '10000', functionary: '员工6', status: '已经完成', remark: '无' },
    { id: '005', date: '6.6', client:'chy', price: '50000', functionary: '员工4', status: '已撤销', remark: '时间紧迫' },
    { id: '001', date: '2.28', client:'yc', price: '10600', functionary: '员工1', status: '发起流程', remark: '6' },
] 

Mock.mock('/api/get-buy', 'get', {
    businesses_list: buyList,
})

Mock.mock('/api/details-buy', 'post', (params) => {
    let data = JSON.parse(params.body);
    const businessesdata = buyList.filter(item => item.id === data.id);
    if (businessesdata) {
        console.log(businessesdata);
        return businessesdata;
    }
}
)

Mock.mock('/api/submit-buy-form', 'post', {
    message: 'buy提交成功'
})

Mock.mock('/api/delete-buy-form', 'post', {
    message: 'buy删除成功'
})

let leaseList = [
    { id: '003', date: '9.5', client:'团队m', price: '10000', functionary: '老李', status: '已租赁', remark: '刚开始' },
    { id: '002', date: '12.5', client:'yj', price: '10000', functionary: 'oceancat', status: '已经完成', remark: '无' },
    { id: '005', date: '6.6', client:'老六', price: '50000', functionary: 'bvvd', status: '已租赁', remark: '即将稻妻' },
    { id: '001', date: '2.28', client:'abcd', price: '10600', functionary: '老李', status: '发起租赁', remark: '6' },
] 

Mock.mock('/api/get-lease', 'get', {
    businesses_list: leaseList,
})

Mock.mock('/api/details-lease', 'post', (params) => {
    let data = JSON.parse(params.body);
    const businessesdata = leaseList.filter(item => item.id === data.id);
    if (businessesdata) {
        console.log(businessesdata);
        return businessesdata;
    }
}
)

Mock.mock('/api/submit-lease-form', 'post', {
    message: 'lease提交成功'
})

Mock.mock('/api/delete-lease-form', 'post', {
    message: 'lease删除成功'
})

let projectList = [
    { id: '003', date: '9.5', client:'aaa', price: '10000', functionary: 'GB', status: '进行中', remark: '成员1，成员2，成员3' },
    { id: '002', date: '12.5', client:'kkk', price: '10000', functionary: '内斯塔', status: '已经完成', remark: '成员1，成员2，成员3' },
    { id: '005', date: '6.6', client:'xxx', price: '50000', functionary: 'bvvd', status: '进行中', remark: '成员a，成员b，成员c' },
    { id: '001', date: '2.28', client:'abcd', price: '10600', functionary: '乪', status: '进行中', remark: '无' },
] 

Mock.mock('/api/get-project', 'get', {
    businesses_list: projectList,
})

Mock.mock('/api/details-project', 'post', (params) => {
    let data = JSON.parse(params.body);
    const businessesdata = projectList.filter(item => item.id === data.id);
    if (businessesdata) {
        console.log(businessesdata);
        return businessesdata;
    }
}
)

Mock.mock('/api/submit-project-form', 'post', {
    message: 'project提交成功'
})

Mock.mock('/api/delete-project-form', 'post', {
    message: 'project删除成功'
})
//worker


Mock.mock('/api/workers', 'get', {
    projects: [{
        workerID: '3'
    }]
})

Mock.mock('/api/projects', 'get', {
    projects: [{
        id: '001',
        managerID: '2150001',
        orderDate: '2024.7.1',
        orderStatus: '进行中',
        paymentStatus: '进行中',
        workerID: '3'
    }, {
        id: '002',
        managerID: '2150002',
        orderDate: '2024.7.2',
        orderStatus: '进行中',
        paymentStatus: '进行中',
        workerID: '3'
    }],
})

Mock.mock('/api/expense', 'post', {
    message: '经费申请已提交'
})

Mock.mock('/api/purchase', 'post', {
    message: '设备购买已提交'
})

Mock.mock('/api/repair', 'post', {
    message: '设备维修已提交'
})

Mock.mock('/api/files', 'get', {
    files: [{
        id: '001',
        name: '入职通知书',
        type: 'pdf',
        description: '欢迎新人！',
        size: '114 KB',
        uploadDate: '2024.7.1',
        status: '收件人',
        workerID: '3'
    }, {
        id: '002',
        name: '解职通知书',
        type: 'pdf',
        description: '你被开了！',
        size: '514 KB',
        uploadDate: '2024.7.1',
        status: '收件人',
        workerID: '3'
    },],
})

Mock.mock('/api/upload', 'post', {
    message: '文件已上传'
})