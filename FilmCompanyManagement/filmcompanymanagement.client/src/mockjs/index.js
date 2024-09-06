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
    {
        id: '001', // 招聘人员的唯一标识符
        name: '张三', // 招聘人员姓名
        gender: '男', // 性别
        dateOfBirth: '1990-01-01', // 出生日期
        resume: '详见附件简历.pdf', // 简历
        positionTitle: '实习生', // 职位名称
        phone: '13800000000', // 联系电话
        email: 'zhangsan@example.com', // 电子邮件
        interviewer: '面试官1', // 面试官
        interviewerStage: '一面', // 面试阶段
        state: '招聘中', // 状态
        adviser: '导师1', // 实习导师
        internshipStartDate: '2023-06-01', // 实习开始日期
        internshipEndDate: '2023-08-31', // 实习结束日期
        remarks: '表现优秀，考虑转正' // 实习相关的备注
      },
      {
        id: '002', // 招聘人员的唯一标识符
        name: '李四', // 招聘人员姓名
        gender: '女', // 性别
        dateOfBirth: '1992-04-22', // 出生日期
        resume: '详见附件简历.pdf', // 简历
        positionTitle: '助理', // 职位名称
        phone: '13911223344', // 联系电话
        email: 'lisi@example.com', // 电子邮件
        interviewer: '面试官2', // 面试官
        interviewerStage: '二面', // 面试阶段
        state: '进入实习', // 状态
        adviser: '导师2', // 实习导师
        internshipStartDate: '2023-09-01', // 实习开始日期
        internshipEndDate: '2023-12-31', // 实习结束日期
        remarks: '专业技能强，团队合作佳' // 实习相关的备注
      },
      {
        id: '003', // 招聘人员的唯一标识符
        name: '王五', // 招聘人员姓名
        gender: '男', // 性别
        dateOfBirth: '1988-07-05', // 出生日期
        resume: '详见附件简历.pdf', // 简历
        positionTitle: '分析师', // 职位名称
        phone: '13799887766', // 联系电话
        email: 'wangwu@example.com', // 电子邮件
        interviewer: '面试官3', // 面试官
        interviewerStage: '终面', // 面试阶段
        state: '待定', // 状态
        adviser: '导师3', // 实习导师
        internshipStartDate: '2024-01-01', // 实习开始日期
        internshipEndDate: '2024-04-30', // 实习结束日期
        remarks: '沟通能力强，有创新思维' // 实习相关的备注
      }
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
    {
        id: '001', // 演练的唯一标识符
        teacher: '张三', // 主持演练的教师姓名
        dateTime: '2023-09-05 10:00', // 演练的时间
        timeSpan: '2小时', // 演练持续时间
        drillEmployees: ['老张', '老王'] // 参与演练的员工姓名数组
      },
      {
        id: '002', // 演练的唯一标识符
        teacher: '李四', // 主持演练的教师姓名
        dateTime: '2023-09-12 14:00', // 演练的时间
        timeSpan: '3小时', // 演练持续时间
        drillEmployees: ['小李', '小陈'] // 参与演练的员工姓名数组
      },
      {
        id: '003', // 演练的唯一标识符
        teacher: '王五', // 主持演练的教师姓名
        dateTime: '2023-09-19 09:30', // 演练的时间
        timeSpan: '1.5小时', // 演练持续时间
        drillEmployees: ['老王', '小赵'] // 参与演练的员工姓名数组
      },
      {
        id: '004', // 演练的唯一标识符
        teacher: '赵六', // 主持演练的教师姓名
        dateTime: '2023-09-22 15:00', // 演练的时间
        timeSpan: '2.5小时', // 演练持续时间
        drillEmployees: ['赵工', '钱师傅'] // 参与演练的员工姓名数组
      }
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
      {
        id: '008', // 维修申请的唯一标识符
        type: '维修申请', // 维修申请类型
        employee: '张三', // 员工姓名
        billId: 'B005', // 关联的账单ID
        amount: '1500', // 账单金额
        date: '2023-11-10', // 账单日期
        accountId: 'ACC-202', // 关联的账户ID
        photoEquipmentId: 'PE005', // 需要维修的摄影设备ID
        name: '相机电池', // 设备名称
        model: 'BT-102', // 设备型号
        description: '电池无法充电', // 维修描述
        status: '通过', // 申请的处理状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },
      {
        id: '009', // 报销申请的唯一标识符
        type: '报销申请', // 报销申请类型
        
        employee: '李四', // 员工姓名
        billId: 'B006', // 关联的账单ID
        amount: '2000', // 账单金额
        date: '2023-11-15', // 账单日期
        accountId: 'ACC-303', // 关联的账户ID
        status: '拒绝', // 申请的处理状态
        remark: '报销申请不符合公司政策。' // 额外的备注信息
      },
      {
        id: '010', // 购买申请的唯一标识符
        type: '购买申请', // 购买申请类型
        employee: '王五', // 员工姓名
        billId: 'B007', // 关联的账单ID
        amount: '5000', // 账单金额
        date: '2023-11-20', // 账单日期
        accountId: 'ACC-404', // 关联的账户ID
        photoEquipmentId: 'PE006', // 需要购买的摄影设备ID
        name: '专业镜头', // 设备名称
        model: 'LN-PRO', // 设备型号
        description: '购买新镜头', // 购买描述
        status: '等待', // 申请的处理状态
        remark: '建议购买此镜头以提升拍摄质量。' // 额外的备注信息
      }
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
    {
        id: "002",
        customer: "银行b",
        date: "2024-09-07",
        billStatus: "待支付",
        accountStatus: "已冻结",
        billId: "B654321",
        amount: "15000",
        type: "活期存款",
        billDate: "2024-09-11",
        accountId: "ACC987654"
      },
      {
        id: "003",
        customer: "银行c",
        date: "2024-09-08",
        billStatus: "已支付",
        accountStatus: "活跃",
        billId: "B111222",
        amount: "20000",
        type: "定期存款",
        billDate: "2024-09-12",
        accountId: "ACC333666"
      },
      {
        id: "004",
        customer: "银行d",
        date: "2024-09-09",
        billStatus: "已支付",
        accountStatus: "活跃",
        billId: "B777888",
        amount: "35000",
        type: "定期存款",
        billDate: "2024-09-13",
        accountId: "ACC555999"
      },
      {
        id: "005",
        customer: "银行e",
        date: "2024-09-10",
        billStatus: "待支付",
        accountStatus: "已冻结",
        billId: "B999000",
        amount: "10000",
        type: "活期存款",
        billDate: "2024-09-14",
        accountId: "ACC444777"
      }
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
    {
        id: "P001",
        type: "电子产品",
        date: "2024-09-06",
        customer: "aaa",
        

        status: "待发货",

        paymentStatus: "已支付",
        billId: 'B123499',
        billType: "订单",
        amount: "500",
        billDate: "2024-09-07",
        accountId: "ACC456",

        fileName: "产品说明书",
        fileType: "PDF",
        contentType: "application/pdf",
        size: "2MB",
        path: "/documents/product/P001.pdf",
        uploadDate: "2024-09-05",
        fileStatus: "已上传",
        storageEquipmentId: "SE123",

        remark:'无',
      }
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