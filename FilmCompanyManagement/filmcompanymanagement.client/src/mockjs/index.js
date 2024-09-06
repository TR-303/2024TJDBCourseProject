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


//！！！！！实习生是员工的追加
let inviteList = [
    {
        id: '001', // 招聘人员的唯一标识符
        name: '张三', // 招聘人员姓名
        gender: '男', // 性别
        positionTitle: '实习生', // 职位名称
        salary: '5000.00', // 工资
        phone: '13800000000', // 联系电话
        email: 'zhangsan@example.com', // 电子邮件

        interviewer: '面试官1', // 面试官

        interviewerStage: '一面', // 面试阶段
        state:'未录用'             //
      },
]
let internshipList = [
    {
        //
        advicerId: '123456', // 顾问
        advicer: '张三', // 顾问姓名
        //
        internId: '789012', // 实习生
        intern: '李四', // 实习生姓名

        internshipStartDate: '2024-09-01', // 实习开始日期
        internshipEndDate: '2025-09-01', // 实习结束日期
        remarks: '无', // 备注信息
    },
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
    {
        id: '123456', // 员工ID——PK
        name: '张三', // 姓名
        gender: '男', // 性别
        position: '软件工程师', // 职位

        phone: '13800138000', // 联系电话
        email: 'zhangsan@example.com', // 电子邮箱
        salary: '5000.00', // 工资
        salaryStatus: '正常', // 工资状态
        
        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        type: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        interns: [], // 实习生集合

        projects: [], // 项目集合

        manageProjects: [], // 管理的项目集合

        department: '管理部', // 部门
        
        kpi:'10', // KPI集合

        attendances: [], // 考勤集合

        drills: [] // 演练集合
    }
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
        id: '123456', // 编号

        teacher: '王老师', // 培训讲师

        dateTime: '2024-09-10T09:00:00', // 培训开始时间
        endTime: '2024-09-10T17:00:00', // 培训结束时间，假设培训时长为8小时

        employees: ['老李', '老赵', '老钱', '老孙', '老周', '老吴', '老郑', '老王', '老冯', '老陈'], // 参与者集合
      },
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
        type: '维修申请', // 维修申请类型

        id: 'P001', // 编号
        employee: '张三', // 员工姓名
        //设备
        photoEquipment: '数码相机', // 摄影设备
        description: '高性能数码相机，适合专业摄影。', // 描述
        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        type: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '通过', // 申请的处理状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },
      {
        type: '维修申请', // 维修申请类型

        id: 'EQ001', // 设备编号
        employee: '张三', // 申请人
        //设备
        name: '打印机', // 设备名称
        model: 'MX-310', // 设备型号
        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        type: '存款', // 类型
        date: '2024-09-01', // 日期

        status: true, // 审核状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },
      {
        type: '维修申请', // 维修申请类型

        id: '123456', // 设备编号
        employee: '张三', // 员工
        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        type: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        status: true, // 审核状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
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
        id: 'BILL123456', // 文件ID

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        businessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        billtype: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成'
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
    {

        id: 'ORD123456', // 订单编号
        type: '标准', // 订单类型

        //file
        fileid: 'FILE123456', // 文件ID
        name: '报告总结', // 文件名
        fileType: '文档', // 文件类型
        contentType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // 内容类型
        size: 2048, // 文件大小，单位为KB
        path: '/uploads/2024/09/', // 文件路径
        uploadDate: '2024-09-06', // 上传日期
        filestatus: '已上传', // 状态

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        businessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        billtype: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '待发货', // 申请的处理状态
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
    {
        id: 'ORD789012', // 编号
        employee: '员工001', // 对接管理ID——FK员工id

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        businessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        billtype: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        Status: '待确认', // 订单状态
    }
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
    {
        id: 'PRO123456', // 项目编号

        manager: '项目经理001', // 对接管理ID——FK员工id

        employees: [], // 项目员工集合
        kpiDate: '2024-09-06', // 绩效评定时间
        result: 1, // 评定结果打分
        judger: '评定员001', // 导航属性，追究评定者信息

        //file
        fileid: 'FILE123456', // 文件ID
        name: '报告总结', // 文件名
        fileType: '文档', // 文件类型
        contentType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // 内容类型
        size: 2048, // 文件大小，单位为KB
        path: '/uploads/2024/09/', // 文件路径
        uploadDate: '2024-09-06', // 上传日期
        filestatus: '已上传', // 状态

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        businessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        bill_id: 'ACC123456', // 编号
        amount: 1000.00, // 金额
        billtype: '存款', // 类型
        date: '2024-09-01', // 日期
        billStatus: '已完成',

        Status: '进行中', // 订单状态
    }
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