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
    { id: '1', issign: '0', signtime: '' },
    { id: '2', issign: '0', signtime: '' },
    { id: '3', issign: '0', signtime: '' },
]
let userdataList = [
    { id: '1', department: '管理部', phone: '12345' },
    { id: '2', department: '财务部', phone: '67890' },
    { id: '3', department: '业务部', phone: '13579' },
    { id: '4', department: '业务部', phone: '22222' },
] 
//传了id
Mock.mock('/data/userdata', 'post', (params) => {
    let user = JSON.parse(params.body);
    const userdata = dataList.find(item => item.id === user.id);
    return {
        name: userdata.name
    };
}
)

//传了username，password，department
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
            success:0,
        }
}
)

//传了id和time
Mock.mock('/data/signdata', 'post', (params) => {
    let user = JSON.parse(params.body);
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
)

//传了department
Mock.mock('/data/departmentuserdata', 'post',(params)=> {
    let data = JSON.parse(params.body);
    const departmentdata = userdataList.filter(item => item.department === data.department);
    if (departmentdata) {
        console.log(departmentdata);
        return departmentdata;
    }
}
)


//boss
//人员管理部分
Mock.mock('/api/employeeInvite', 'get', {
    employee_invite: [{
        id: '001',
        name: '张三',
        type: '实习生',
        status: '实习中',
    }, {
        id: '002',
        name: '李四',
        type: '求职者',
        status: '未招聘',
    }, {
        id: '003',
        name: '王二',
        type: '求职者',
        status: '已拒绝',
    }],
})

Mock.mock('/api/detailsInvite', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeInvite', 'post', {
    message: '修改成功'
})

Mock.mock('/api/employeeOverview', 'get', {
    employee_overview: [{
        id: '001',
        name: '张三',
        salary: '15660',
    }, {
        id: '002',
        name: '李四',
        salary: '12354',
    }, {
        id: '003',
        name: '王二',
        salary: '6663',
    }],
})

Mock.mock('/api/detailsOverview', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeOverview', 'post', {
    message: '修改成功'
})

Mock.mock('/api/employeeTrain', 'get', {
    employee_train: [{
        id: '001',
        teacher: '张三',
        date: '10.7-10.30',
        student: '小王',
        status: '等待批准',
    }, {
        id: '001',
        teacher: '张三',
        date: '9.10-9.17',
        student: '小刘',
        status: '等待批准',
    }],
})

Mock.mock('/api/detailsTrain', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeTrain', 'post', {
    message: '修改成功'
})


//申请管理部分
Mock.mock('/api/requisition', 'get', {
    requisition: [{
        id:'001',
        name: '张三',
        type: '维修申请',
        status: '等待批准',
        ideas: '无',
    }, {
        id: '002',
        name: '李四',
        type: '购买申请',
        status: '已拒绝',
        ideas: '经费不足',
        }, {
        id: '003',
        name: '王二',
        type: '报销申请',
        status: '已同意',
        ideas: '无',
    }],
})

Mock.mock('/api/detailsRequisition', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeRequisition', 'post', {
    message: '修改成功'
})

//业务管理部分
Mock.mock('/api/businessInvestment', 'get', {
    businesses_investment: [{
        id: '001',
        date: '9.1',
        money: '1000',
        functionary: '张四',
        status: '等待批准',
        details: '无',
    }, {
        id: '005',
        date: '3.4',
        money: '1500',
        functionary: '张四',
        status: '等待批准',
        details: '无',
    }],
})

Mock.mock('/api/detailsInvestment', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeInvestment', 'post', {
    message: '修改成功'
})

Mock.mock('/api/businessBuy', 'get', {
    businesses_buy: [{
        id: '011',
        date: '9.11',
        money: '160',
        functionary: '张两',
        status: '进行中',
        details: '无',
    }, {
        id: '006',
        date: '3.14',
        money: '1500',
        functionary: '张四',
        status: '已完成',
        details: '无',
    }],
})

Mock.mock('/api/detailsBuy', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeBuy', 'post', {
    message: '修改成功'
})

Mock.mock('/api/businessLease', 'get', {
    businesses_lease: [{
        id: '033',
        date: '6.1',
        money: '1000',
        functionary: 'xx',
        status: '等待批准',
        details: '无',
    }, {
        id: '022',
        date: '3.5',
        money: '50000',
        functionary: 'aa',
        status: 'ok',
        details: '无',
    }],
})

Mock.mock('/api/detailsLease', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeLease', 'post', {
    message: '修改成功'
})

Mock.mock('/api/businessProject', 'get', {
    businesses_project: [{
        id: '101',
        date: '1.1',
        money: '1111',
        functionary: 'kk',
        status: '等待验收',
        details: '6',
    }, {
        id: '605',
        date: '11.5',
        money: '1500',
        functionary: 'sy',
        status: '未开始',
        details: '无',
    }],
})

Mock.mock('/api/detailsProject', 'post', {
    message: '查询申请'
})

Mock.mock('/api/changeProject', 'post', {
    message: '修改成功'
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