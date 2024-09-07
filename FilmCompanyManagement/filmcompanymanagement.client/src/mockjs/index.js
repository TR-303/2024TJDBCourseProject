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

// 未处理的外部投资数据
let unprocessedInvestmentList = [
    { investmentId: '1', customerId: '11', orderDate: '2024-08-01', invoiceNumber: '20240801', amount: 5000, expenseType: '广告费用' },
    { investmentId: '2', customerId: '22', orderDate: '2024-08-15', invoiceNumber: '20240815', amount: 10000, expenseType: '制作费用' },
    { investmentId: '3', customerId: '33', orderDate: '2024-08-16', invoiceNumber: '20240816', amount: 2000, expenseType: '广告费用' },
    { investmentId: '4', customerId: '44', orderDate: '2024-08-17', invoiceNumber: '20240817', amount: 30000, expenseType: '制作费用' },
];

// 获取未处理的外部投资数据（带请求参数）
Mock.mock(RegExp('/api/externalInvestments/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const orderStatus = query.get('orderStatus'); // 获取 orderStatus 参数

    console.log('Received External Investments query:', { orderStatus }); // 调试信息

    if (orderStatus === 'approved') {
        //console.log("approved!!!");
        return {
            code: 200,
            data: unprocessedInvestmentList // 返回符合条件的数据
        };
    } else {
        //console.log("Not approved!!!");
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});

// 拦截 POST 请求，路径为 /api/externalInvestments/markProcessed
Mock.mock('/api/externalInvestments/markProcessed', 'post', (params) => {
    // 解析请求数据
    const requestBody = JSON.parse(params.body);
    const investmentId = requestBody.investmentId;

    // 查找并移除未处理列表中的记录
    const investmentIndex = unprocessedInvestmentList.findIndex(item => item.investmentId === investmentId);
    if (investmentIndex !== -1) {
        // 从未处理列表中移除记录
        const investment = unprocessedInvestmentList.splice(investmentIndex, 1)[0];
        // 添加到已处理列表中
        const processedInvestment = {
            ...investment,
            processedDate: requestBody.processedDate // 返回请求中传递的处理日期
        };
        processedInvestmentList.push(processedInvestment);

        return {
            code: 200,
            message: '处理成功',
            data: processedInvestment // 返回处理后的数据
        };
    } else {
        return {
            code: 404,
            message: '投资记录未找到'
        };
    }
});

// 模拟已处理的外部投资数据
let processedInvestmentList = [
    //{ investmentId: '5', customerId: '55', orderDate: '2024-08-18', invoiceNumber: '20240818', amount: 6000, expenseType: '广告费用', processedDate: '2024-09-01' },
    //{ investmentId: '6', customerId: '66', orderDate: '2024-08-19', invoiceNumber: '20240819', amount: 12000, expenseType: '制作费用', processedDate: '2024-09-02' }
];

// 获取已处理外部投资数据
Mock.mock(RegExp('/api/externalInvestments/processed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const financialStatus = query.get('financialStatus'); // 获取 financialStatus 参数

    console.log('Received External Investments query22:', { financialStatus }); // 调试信息

    if (financialStatus === 'processed') {
        //console.log("approved!!!");
        return {
            code: 200,
            data: processedInvestmentList // 返回符合条件的数据
        };
    } else {
        //console.log("Not approved!!!");
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});



// 未处理的设备租赁数据
let unprocessedLeasingList = [
    { projectId: 'P001', dockingManagementId: 'D001', orderDate: '2024-09-01', invoiceNumber: '20240901', amount: 15000, expenseType: '设备租赁费用' },
    { projectId: 'P002', dockingManagementId: 'D002', orderDate: '2024-09-10', invoiceNumber: '20240910', amount: 8000, expenseType: '设备维护费用' }
];

// 获取设备租赁数据（带请求参数）
Mock.mock(RegExp('/api/equipmentLeasing/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const orderStatus = query.get('orderStatus'); // 获取 orderStatus 参数

    console.log('Received Equipment Leasing query:', { orderStatus }); // 调试信息

    if (orderStatus === 'approved') {
        //console.log("approved!!!");
        return {
            code: 200,
            data: unprocessedLeasingList // 返回符合条件的数据
        };
    } else {
        //console.log("Not approved!!!");
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});

// 拦截 POST 请求，路径为 /api/equipmentLeasing/markProcessed
Mock.mock('/api/equipmentLeasing/markProcessed', 'post', (params) => {
    // 解析请求数据
    const requestBody = JSON.parse(params.body);
    const projectId = requestBody.projectId;

    // 查找并移除未处理列表中的记录
    const leasingIndex = unprocessedLeasingList.findIndex(item => item.projectId === projectId);
    if (leasingIndex !== -1) {
        // 从未处理列表中移除记录
        const leasing = unprocessedLeasingList.splice(leasingIndex, 1)[0];
        // 添加到已处理列表中
        const processedLeasing = {
            ...leasing,
            processedDate: requestBody.processedDate // 返回请求中传递的处理日期
        };
        processedLeasingList.push(processedLeasing);

        return {
            code: 200,
            message: '处理成功',
            data: processedLeasing // 返回处理后的数据
        };
    } else {
        return {
            code: 404,
            message: '设备租赁记录未找到'
        };
    }
});

// 模拟已处理的设备租赁数据
let processedLeasingList = [
    //{ projectId: 'P003', dockingManagementId: 'D003', orderDate: '2024-09-05', invoiceNumber: '20240905', amount: 20000, expenseType: '设备租赁费用', processedDate: '2024-09-01' },
    //{ projectId: 'P004', dockingManagementId: 'D004', orderDate: '2024-09-12', invoiceNumber: '20240912', amount: 10000, expenseType: '设备维护费用', processedDate: '2024-09-02' }
];

// 获取已处理设备租赁数据
Mock.mock(RegExp('/api/equipmentLeasing/processed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const financialStatus = query.get('financialStatus'); // 获取 financialStatus 参数

    console.log('Received Equipment Leasing query22:', { financialStatus }); // 调试信息

    if (financialStatus === 'processed') {
        return {
            code: 200,
            data: processedLeasingList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});



// 成片购买订单数据
let unprocessedBlockPurchaseOrderList = [
    { orderId: 'O001', blockFileId: 'B001', orderDate: '2024-09-01', invoiceNumber: '20240901', amount: 25000, expenseType: '成片购买费用' },
    { orderId: 'O002', blockFileId: 'B002', orderDate: '2024-09-10', invoiceNumber: '20240910', amount: 12000, expenseType: '成片制作费用' }
];

// 获取成片购买订单数据（带请求参数）
Mock.mock(RegExp('/api/blockPurchaseOrders/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const orderStatus = query.get('orderStatus'); // 获取 orderStatus 参数

    console.log('Received Block Purchase Order query:', { orderStatus }); // 调试信息

    if (orderStatus === 'approved') {
        return {
            code: 200,
            data: unprocessedBlockPurchaseOrderList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});

// 拦截 POST 请求，路径为 /api/blockPurchaseOrders/markProcessed
Mock.mock('/api/blockPurchaseOrders/markProcessed', 'post', (params) => {
    // 解析请求数据
    const requestBody = JSON.parse(params.body);
    const orderId = requestBody.orderId;

    // 查找并移除未处理列表中的记录
    const blockPurchaseOrderIndex = unprocessedBlockPurchaseOrderList.findIndex(item => item.orderId === orderId);
    if (blockPurchaseOrderIndex !== -1) {
        // 从未处理列表中移除记录
        const blockPurchaseOrder = unprocessedBlockPurchaseOrderList.splice(blockPurchaseOrderIndex, 1)[0];
        // 添加到已处理列表中
        const processedBlockPurchaseOrder = {
            ...blockPurchaseOrder,
            processedDate: requestBody.processedDate // 返回请求中传递的处理日期
        };
        processedBlockPurchaseOrderList.push(processedBlockPurchaseOrder);

        return {
            code: 200,
            message: '处理成功',
            data: processedBlockPurchaseOrder // 返回处理后的数据
        };
    } else {
        return {
            code: 404,
            message: '设备租赁记录未找到'
        };
    }
});

// 模拟已处理的成片购买订单数据
let processedBlockPurchaseOrderList = [
    //{ projectId: 'P003', dockingManagementId: 'D003', orderDate: '2024-09-05', invoiceNumber: '20240905', amount: 20000, expenseType: '设备租赁费用', processedDate: '2024-09-01' },
    //{ projectId: 'P004', dockingManagementId: 'D004', orderDate: '2024-09-12', invoiceNumber: '20240912', amount: 10000, expenseType: '设备维护费用', processedDate: '2024-09-02' }
];

// 获取已处理成片购买订单数据
Mock.mock(RegExp('/api/blockPurchaseOrders/processed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const financialStatus = query.get('financialStatus'); // 获取 financialStatus 参数

    console.log('Received Block Purchase Order query22:', { financialStatus }); // 调试信息

    if (financialStatus === 'processed') {
        return {
            code: 200,
            data: processedBlockPurchaseOrderList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});


// 工资数据
let unprocessedSalaryList = [
    { payrollNumber: 'S001', ratingRecordId: 'R001', ratingResult: '优秀', rateeId: '1', basePay: 5000 },
    { payrollNumber: 'S002', ratingRecordId: 'R002', ratingResult: '良好', rateeId: '2', basePay: 4500 },
    { payrollNumber: 'S003', ratingRecordId: 'R003', ratingResult: '合格', rateeId: '3', basePay: 4000 },
    { payrollNumber: 'S004', ratingRecordId: 'R004', ratingResult: '优秀', rateeId: '4', basePay: 5500 },
];

// 获取工资数据（带请求参数）
Mock.mock(RegExp('/api/salary/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const evaluationStatus = query.get('evaluationStatus'); // 获取 evaluationStatus 参数

    console.log('Received Salary query:', { evaluationStatus }); // 调试信息

    if (evaluationStatus === 'complete') {
        return {
            code: 200,
            data: unprocessedSalaryList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});

// 拦截 POST 请求，路径为 /api/salary/markProcessed
Mock.mock('/api/salary/markProcessed', 'post', (params) => {
    // 解析请求数据
    const requestBody = JSON.parse(params.body);
    const payrollNumber = requestBody.payrollNumber;

    // 查找并移除未处理列表中的记录
    const salaryIndex = unprocessedSalaryList.findIndex(item => item.payrollNumber === payrollNumber);
    if (salaryIndex !== -1) {
        // 从未处理列表中移除记录
        const salary = unprocessedSalaryList.splice(salaryIndex, 1)[0];
        // 添加到已处理列表中
        const processedSalary = {
            ...salary,
            processedDate: requestBody.processedDate // 返回请求中传递的处理日期
        };
        processedSalaryList.push(processedSalary);

        return {
            code: 200,
            message: '处理成功',
            data: processedSalary // 返回处理后的数据
        };
    } else {
        return {
            code: 404,
            message: '设备租赁记录未找到'
        };
    }
});

// 模拟已处理的设备租赁数据
let processedSalaryList = [
    //{ projectId: 'P003', dockingManagementId: 'D003', orderDate: '2024-09-05', invoiceNumber: '20240905', amount: 20000, expenseType: '设备租赁费用', processedDate: '2024-09-01' },
    //{ projectId: 'P004', dockingManagementId: 'D004', orderDate: '2024-09-12', invoiceNumber: '20240912', amount: 10000, expenseType: '设备维护费用', processedDate: '2024-09-02' }
];

// 获取已处理设备租赁数据
Mock.mock(RegExp('/api/salary/processed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const financialStatus = query.get('financialStatus'); // 获取 financialStatus 参数

    console.log('Received Equipment Leasing query22:', { financialStatus }); // 调试信息

    if (financialStatus === 'processed') {
        return {
            code: 200,
            data: processedSalaryList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});




// 项目收入数据
let unprocessedProjectIncomeList = [
    { projectId: 'P001', dockingManagementId: 'D001', orderDate: '2024-09-01', invoiceNumber: '20240901', amount: 30000, expenseType: '项目收入' },
    { projectId: 'P002', dockingManagementId: 'D002', orderDate: '2024-09-15', invoiceNumber: '20240915', amount: 20000, expenseType: '项目收入' }
];

// 获取项目收入数据（带请求参数）
Mock.mock(RegExp('/api/projectIncome/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const orderStatus = query.get('orderStatus'); // 获取 orderStatus 参数

    console.log('Received Project Income query:', { orderStatus }); // 调试信息

    if (orderStatus === 'approved') {
        return {
            code: 200,
            data: unprocessedProjectIncomeList // 返回符合条件的数据
        };
    } else {
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});

// 拦截 POST 请求，路径为 /api/externalInvestments/markProcessed
Mock.mock('/api/projectIncome/markProcessed', 'post', (params) => {
    // 解析请求数据
    const requestBody = JSON.parse(params.body);
    const projectId = requestBody.projectId;

    // 查找并移除未处理列表中的记录
    const projectIncomeIndex = unprocessedProjectIncomeList.findIndex(item => item.projectId === projectId);
    if (projectIncomeIndex !== -1) {
        // 从未处理列表中移除记录
        const projectIncome = unprocessedProjectIncomeList.splice(projectIncomeIndex, 1)[0];
        // 添加到已处理列表中
        const processedProjectIncome = {
            ...projectIncome,
            processedDate: requestBody.processedDate // 返回请求中传递的处理日期
        };
        processedProjectIncomeList.push(processedProjectIncome);

        return {
            code: 200,
            message: '处理成功',
            data: processedProjectIncome // 返回处理后的数据
        };
    } else {
        return {
            code: 404,
            message: '投资记录未找到'
        };
    }
});

// 模拟已处理的项目收入数据
let processedProjectIncomeList = [
    //{ investmentId: '5', customerId: '55', orderDate: '2024-08-18', invoiceNumber: '20240818', amount: 6000, expenseType: '广告费用', processedDate: '2024-09-01' },
    //{ investmentId: '6', customerId: '66', orderDate: '2024-08-19', invoiceNumber: '20240819', amount: 12000, expenseType: '制作费用', processedDate: '2024-09-02' }
];

// 获取已处理项目收入数据
Mock.mock(RegExp('/api/projectIncome/processed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const financialStatus = query.get('financialStatus'); // 获取 financialStatus 参数

    console.log('Received External Investments query22:', { financialStatus }); // 调试信息

    if (financialStatus === 'processed') {
        //console.log("approved!!!");
        return {
            code: 200,
            data: processedProjectIncomeList // 返回符合条件的数据
        };
    } else {
        //console.log("Not approved!!!");
        return {
            code: 200,
            data: [] // 如果不符合条件，则返回空数组
        };
    }
});



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
//招聘员工
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
      {
        id: '002',
        name: '李四',
        gender: '男',
        positionTitle: '软件工程师',
        salary: '8000.00',
        phone: '13911111111',
        email: 'lisi@example.com',
        interviewer: '面试官2',
        interviewerStage: '二面',
        state: '未录用'
    },
    {
        id: '003',
        name: '王五',
        gender: '女',
        positionTitle: '产品经理',
        salary: '10000.00',
        phone: '13722222222',
        email: 'wangwu@example.com',
        interviewer: '面试官3',
        interviewerStage: '初试',
        state: '已录用'
    },
    {
        id: '004',
        name: '赵六',
        gender: '男',
        positionTitle: '设计师',
        salary: '7000.00',
        phone: '13633333333',
        email: 'zhaoliu@example.com',
        interviewer: '面试官4',
        interviewerStage: '终面',
        state: '待定'
    },
    {
        id: '005',
        name: '周七',
        gender: '女',
        positionTitle: '市场专员',
        salary: '6000.00',
        phone: '13544444444',
        email: 'zhouqi@example.com',
        interviewer: '面试官5',
        interviewerStage: '一面',
        state: '未录用'
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

//实习员工
let internList = [
    {
        //指导人员
        advicerId: '123456', // 顾问
        advicer: '张三', // 顾问姓名
        //实习生
        internId: '789012', // 实习生
        intern: '李四', // 实习生姓名

        internshipStartDate: '2024-09-01', // 实习开始日期
        internshipEndDate: '2025-09-01', // 实习结束日期
        remarks: '无', // 备注信息
    },
    {
        advicerId: '123457',
        advicer: '王五',
        internId: '789013',
        intern: '赵六',
        internshipStartDate: '2024-10-01',
        internshipEndDate: '2025-10-01',
        remarks: '表现优秀'
    },
    {
        advicerId: '123458',
        advicer: '周七',
        internId: '789014',
        intern: '吴八',
        internshipStartDate: '2024-11-01',
        internshipEndDate: '2025-11-01',
        remarks: '需加强培训'
    },
    {
        advicerId: '123459',
        advicer: '郑九',
        internId: '789015',
        intern: '王十',
        internshipStartDate: '2024-12-01',
        internshipEndDate: '2025-12-01',
        remarks: '有潜力'
    },
    {
        advicerId: '123460',
        advicer: '冯十一',
        internId: '789016',
        intern: '陈十二',
        internshipStartDate: '2025-01-01',
        internshipEndDate: '2026-01-01',
        remarks: '良好'
    }
]

Mock.mock('/api/get-intern', 'get', {
    employee_list: internList,
})

Mock.mock('/api/details-intern', 'post', (params) => {
    let data = JSON.parse(params.body);
    const employeedata = internList.filter(item => item.id === data.id);
    if (employeedata) {
        console.log(employeedata);
        return employeedata;
    }
})

Mock.mock('/api/submit-intern-form', 'post', {
    message: 'intern提交成功'
})

Mock.mock('/api/delete-intern-form', 'post', {
    message: 'intern删除成功'
})

//员工总览
let overviewList = [
    {
        id: '123456', // 员工ID——PK
        name: '张三', // 姓名
        gender: '男', // 性别
        position: '软件工程师', // 职位

        phone: '13800138000', // 联系电话
        email: 'zhangsan@example.com', // 电子邮箱
        salary: '5000.00', // 工资
        
        //bill
        billId: 'ACC123456', // 编号
        billAmount: 1000.00, // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        interns: [  { internId: 'I001', intern: '李四'},
                    { internId: 'I004', intern: '孙七' },], // 实习生集合

        projects: [{ id: 'P001', manager: '王总'},
                    { id: 'P002', manager: '李经理', status: '进行中'},
                    { id: 'P003', manager: '张经理', status: '已完成' },
                    { id: 'P004', manager: '刘经理', status: '待确认' },
                    { id: 'P005', manager: '陈经理', status: '进行中' },
        ], // 项目集合

        manageProjects: [{ id: 'P006', status: '进行中'},
                        { id: 'P007', status: '待确认' },
                        { id: 'P008', status: '已完成' },
                        { id: 'P009', status: '进行中' }
        ], // 管理的项目集合

        department: '管理部', // 部门
        
        kpi:'10', // KPI集合

        attendances: [{ date: '2024-09-01', status: '出勤' },
            { date: '2024-09-02', status: '迟到' }], // 考勤集合

        drills: [{ id: 'T001', teacher: '王老师', dateTime: '2024-08-20'},
            { id: 'T002', teacher: '张老师', dateTime: '2024-08-25'}] // 培训
    },
    {
    id: '123457', // 员工ID——PK
    name: '李四', // 姓名
    gender: '男', // 性别
    position: '项目经理', // 职位
    phone: '13900139000', // 联系电话
    email: 'lisi@example.com', // 电子邮箱
    salary: '6000.00', // 工资
    billId: 'ACC123457', // 编号
    billAmount: 1200.00, // 金额
    billType: '报销', // 类型
    billDate: '2024-09-02', // 日期
    billStatus: '进行中',
    interns: [{ internId: 'I002', intern: '王五' }],
    projects: [{ id: 'P006', manager: '赵经理', status: '待确认' }],
    manageProjects: [{ id: 'P010', status: '待确认' }],
    department: '研发部', // 部门
    kpi: '15', // KPI
    attendances: [{ date: '2024-09-03', status: '出勤' }],
    drills: [{ id: 'T003', teacher: '刘老师', dateTime: '2024-08-30'}]
},
{
    id: '123458',
    name: '王五',
    gender: '女',
    position: '设计师',
    phone: '13700137000',
    email: 'wangwu@example.com',
    salary: '5500.00',
    billId: 'ACC123458',
    billAmount: 1500.00,
    billType: '借款',
    billDate: '2024-09-03',
    billStatus: '已完成',
    interns: [{ internId: 'I003', intern: '赵六' }],
    projects: [{ id: 'P007', manager: '钱经理', status: '进行中' }],
    manageProjects: [{ id: 'P011', status: '已完成' }],
    department: '设计部',
    kpi: '12',
    attendances: [{ date: '2024-09-04', status: '请假' }],
    drills: [{ id: 'T004', teacher: '孙老师', dateTime: '2024-09-05'}]
},
{
    id: '123459',
    name: '赵六',
    gender: '男',
    position: '市场专员',
    phone: '13600136000',
    email: 'zhaoliu@example.com',
    salary: '4800.00',
    billId: 'ACC123459',
    billAmount: 800.00,
    billType: '报销',
    billDate: '2024-09-04',
    billStatus: '未开始',
    interns: [{ internId: 'I005', intern: '钱七' }],
    projects: [{ id: 'P008', manager: '孙经理', status: '已完成' }],
    manageProjects: [{ id: 'P012', status: '待确认' }],
    department: '市场部',
    kpi: '8',
    attendances: [{ date: '2024-09-05', status: '出勤' }],
    drills: [{ id: 'T005', teacher: '李老师', dateTime: '2024-09-10'}]
},
{
    id: '123460',
    name: '钱七',
    gender: '女',
    position: '财务',
    phone: '13500135000',
    email: 'qianqi@example.com',
    salary: '6200.00',
    billId: 'ACC123460',
    billAmount: 1600.00,
    billType: '借款',
    billDate: '2024-09-05',
    billStatus: '已完成',
    interns: [{ internId: 'I006', intern: '周八' }],
    projects: [{ id: 'P009', manager: '周经理', status: '待确认' }],
    manageProjects: [{ id: 'P013', status: '进行中' }],
    department: '财务部',
    kpi: '9',
    attendances: [{ date: '2024-09-06', status: '迟到' }],
    drills: [{ id: 'T006', teacher: '吴老师', dateTime: '2024-09-15'}]
},
{
    id: '123461',
    name: '周八',
    gender: '男',
    position: '人事',
    phone: '13400134000',
    email: 'zhouba@example.com',
    salary: '5200.00',
    billId: 'ACC123461',
    billAmount: 1200.00,
    billType: '存款',
    billDate: '2024-09-06',
    billStatus: '进行中',
    interns: [{ internId: 'I007', intern: '吴九' }],
    projects: [{ id: 'P010', manager: '郑经理', status: '已完成' }],
    manageProjects: [{ id: 'P014', status: '已完成' }],
    department: '人事部',
    kpi: '7',
    attendances: [{ date: '2024-09-07', status: '出勤' }],
    drills: [{ id: 'T007', teacher: '郑老师', dateTime: '2024-09-20'}]
},
{
    id: '123462',
    name: '吴九',
    gender: '女',
    position: '销售',
    phone: '13300133000',
    email: 'wujiu@example.com',
    salary: '5800.00',
    billId: 'ACC123462',
    billAmount: 1000.00,
    billType: '报销',
    billDate: '2024-09-07',
    billStatus: '已完成',
    interns: [{ internId: 'I008', intern: '郑十' }],
    projects: [{ id: 'P011', manager: '王经理', status: '待确认' }],
    manageProjects: [{ id: 'P015', status: '进行中' }],
    department: '销售部',
    kpi: '11',
    attendances: [{ date: '2024-09-08', status: '请假' }],
    drills: [{ id: 'T008', teacher: '冯老师', dateTime: '2024-09-25'}]
},
{
    id: '123463',
    name: '郑十',
    gender: '男',
    position: '技术支持',
    phone: '13200132000',
    email: 'zhengshi@example.com',
    salary: '5600.00',
    billId: 'ACC123463',
    billAmount: 1300.00,
    billType: '借款',
    billDate: '2024-09-08',
    billStatus: '未开始',
    interns: [{ internId: 'I009', intern: '冯十一' }],
    projects: [{ id: 'P012', manager: '陈经理', status: '已完成' }],
    manageProjects: [{ id: 'P016', status: '待确认' }],
    department: '技术支持部',
    kpi: '13',
    attendances: [{ date: '2024-09-09', status: '出勤' }],
    drills: [{ id: 'T009', teacher: '陈老师', dateTime: '2024-09-30'}]
},
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

//员工培训
let trainList = [
      {
        id: '123456', // 编号

        teacher: '王老师', // 培训讲师

        dateTime: '2024-09-10T09:00:00', // 培训开始时间
        endTime: '2024-09-10T17:00:00', // 培训结束时间，假设培训时长为8小时

        employees: [
            { id: "100001", name: "老李" },
            { id: "100002", name: "老赵" },
            { id: "100003", name: "老钱" },
            { id: "100004", name: "老孙" },
            { id: "100005", name: "老周" },
            { id: "100006", name: "老吴" },
            { id: "100007", name: "老郑" },
            { id: "100008", name: "老王" },
            { id: "100009", name: "老冯" },
            { id: "100010", name: "老陈" }
        ], // 参与者集合
      },
      {
        id: '123457',
        teacher: '李老师',
        dateTime: '2024-09-11T09:30:00',
        endTime: '2024-09-11T17:30:00',
        employees: [
            { id: "100011", name: "马云" },
            { id: "100012", name: "马化腾" },
            { id: "100013", name: "丁磊" },
            { id: "100014", name: "雷军" },
            { id: "100015", name: "周鸿祎" }
          ]
    },
    {
        id: '123458',
        teacher: '张老师',
        dateTime: '2024-09-12T10:00:00',
        endTime: '2024-09-12T18:00:00',
        employees: [
            { id: "100016", name: "张一鸣" },
            { id: "100017", name: "黄铮" },
            { id: "100018", name: "刘强东" },
            { id: "100019", name: "王兴" },
            { id: "100020", name: "程维" }
          ]
    },
    {
        id: '123459',
        teacher: '刘老师',
        dateTime: '2024-09-13T09:00:00',
        endTime: '2024-09-13T17:00:00',
        employees: [
            { id: "100021", name: "任正非" },
            { id: "100022", name: "柳传志" },
            { id: "100023", name: "董明珠" },
            { id: "100024", name: "张近东" },
            { id: "100025", name: "李书福" }
          ]
    },
    {
        id: '123460',
        teacher: '陈老师',
        dateTime: '2024-09-14T10:30:00',
        endTime: '2024-09-14T18:30:00',
        employees: [
            { id: "100026", name: "赵钱孙" },
            { id: "100027", name: "李周吴" },
            { id: "100028", name: "郑王冯" },
            { id: "100029", name: "陈褚卫" },
            { id: "100030", name: "蒋沈韩" },
            { id: "100031", name: "杨秦尤" },
            { id: "100032", name: "许何吕" },
            { id: "100033", name: "施张孔" },
            { id: "100034", name: "曹严华" },
            { id: "100035", name: "金魏陶" }
          ]
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
        type: '维修申请', // 维修申请类型

        id: 'P001', // 编号
        employee: '张三', // 员工姓名
        //设备
        equipmentId: '打印机', // 设备编号
        equipmentName: '打印机', // 设备名称
        equipmentModel: 'MX-310', // 设备型号
        //bill
        billId: 'ACC123456', // 编号
        billAmount: '1000.00', // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '通过', // 申请的处理状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },
      {
        type: '购买申请', // 维修申请类型

        id: 'EQ001', // 设备编号
        employee: '张三', // 申请人
        //设备
        equipmentName: '打印机', // 设备名称
        equipmentModel: 'MX-310', // 设备型号
        //bill
        billId: 'ACC123456', // 编号
        billAmount: '1000.00', // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '通过', // 审核状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },
      {
        type: '报销申请', // 维修申请类型

        id: '123456', // 设备编号
        employee: '张三', // 员工
        //bill
        billId: 'ACC123456', // 编号
        billAmount: '1000.00', // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '通过', // 审核状态
        remark: '请尽快处理，电池已影响正常工作。' // 额外的备注信息
      },

      {
        type: "维修申请",
        id: "P003",
        employee: "张三",
        equipmentId: "打印机",
        equipmentName: "打印机",
        equipmentModel: "MX-310",
        billId: "ACC123460",
        billAmount: "1000.00",
        billType: "存款",
        billDate: "2024-09-01",
        billStatus: "已完成",
        status: "通过",
        remark: "请尽快处理，电池已影响正常工作。"
    },
    {
        type: "购买申请",
        id: "EQ003",
        employee: "李四",
        equipmentName: "复印机",
        equipmentModel: "FX-500",
        billId: "ACC123461",
        billAmount: "2000.00",
        billType: "购买",
        billDate: "2024-09-04",
        billStatus: "进行中",
        status: "待定",
        remark: "需要购买新的复印机。"
    },
    {
        type: "报销申请",
        id: "RB003",
        employee: "王五",
        billId: "ACC123462",
        billAmount: "300.00",
        billType: "报销",
        billDate: "2024-09-05",
        billStatus: "未开始",
        status: "拒绝",
        remark: "之前的报销申请未通过。"
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
//投资
let investList = [
      {
        id: 'BILL123456', // 文件ID

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        customerBusinessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        billId: 'ACC123456', // 编号
        billAmount: '1000.00', // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '完成'
      },
      {
        id: 'BILL123460',
        customerid: 'CUST123460',
        customerType: '企业',
        customerName: '蓝海科技',
        customerBusinessType: 'IT服务',
        customerPhone: '12345678905',
        customerEmail: 'contact@blueoceantech.com',
        customerAddress: '科技园区创新路3号',
        billId: 'ACC123460',
        billAmount: '1500.00',
        billType: '存款',
        billDate: '2024-09-05',
        billStatus: '完成'
    },
    {
        id: 'BILL123461',
        customerid: 'CUST123461',
        customerType: '政府',
        customerName: '城市发展局',
        customerBusinessType: '城市规划',
        customerPhone: '12345678906',
        customerEmail: 'info@citydevelopment.gov',
        customerAddress: '市政大道1号',
        billId: 'ACC123461',
        billAmount: '2500.00',
        billType: '拨款',
        billDate: '2024-09-06',
        billStatus: '发起'
    },
    {
        id: 'BILL123462',
        customerid: 'CUST123462',
        customerType: '个人',
        customerName: '王晓明',
        customerBusinessType: '教育咨询',
        customerPhone: '12345678907',
        customerEmail: 'xiaoming.wang@example.com',
        customerAddress: '光明街45号',
        billId: 'ACC123462',
        billAmount: '800.00',
        billType: '服务费',
        billDate: '2024-09-07',
        billStatus: '完成'
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

//成片购买
let buyList = [
    {

        id: 'ORD123456', // 订单编号
        type: '标准', // 订单类型

        //file
        fileId: 'FILE123456', // 文件ID
        fileName: '报告总结', // 文件名
        fileType: '文档', // 文件类型
        fileContentType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // 内容类型
        fileSize: 2048, // 文件大小，单位为KB
        filePath: '/uploads/2024/09/', // 文件路径
        fileUploadDate: '2024-09-06', // 上传日期
        filestatus: '已上传', // 状态

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        customerBusinessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        billId: 'ACC123456', // 编号
        billAmount: 1000.00, // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '待发货', // 申请的处理状态
    },

    {
        id: 'ORD123457',
        type: '加急',
    
        fileId: 'FILE123457',
        fileName: '项目计划书',
        fileType: '文档',
        fileContentType: 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
        fileSize: 1024,
        filePath: '/uploads/2024/09/',
        fileUploadDate: '2024-09-07',
        filestatus: '已上传',
    
        customerid: 'CUST123457',
        customerType: '政府',
        customerName: '城市发展部',
        customerBusinessType: '城市规划',
        customerPhone: '12345678902',
        customerEmail: 'contact@citydevelopment.gov',
        customerAddress: '市政大道2号',
    
        billId: 'ACC123457',
        billAmount: 2500.00,
        billType: '拨款',
        billDate: '2024-09-08',
        billStatus: '进行中',
    
        status: '待确认'
    },
    {
        id: 'ORD123458',
        type: '特殊',
    
        fileId: 'FILE123458',
        fileName: '市场分析报告',
        fileType: 'PDF',
        fileContentType: 'application/pdf',
        fileSize: 512,
        filePath: '/uploads/2024/09/',
        fileUploadDate: '2024-09-09',
        filestatus: '未上传',
    
        customerid: 'CUST123458',
        customerType: '个人',
        customerName: '周梅',
        customerBusinessType: '市场分析',
        customerPhone: '12345678903',
        customerEmail: 'zhoumei@example.com',
        customerAddress: '花园小区3单元',
    
        billId: 'ACC123458',
        billAmount: 1200.00,
        billType: '服务费',
        billDate: '2024-09-10',
        billStatus: '已完成',
    
        status: '已发货'
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

//设备租赁
let leaseList = [
    {
        id: 'ORD789012', // 编号
        employee: '员工001', // 对接管理ID——FK员工id

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        customerBusinessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        billId: 'ACC123456', // 编号
        billAmount: 1000.00, // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '待确认', // 订单状态
    },
    {
        id: 'ORD789013',
        employee: '员工002',
        customerid: 'CUST123457',
        customerType: '政府',
        customerName: '城市发展局',
        customerBusinessType: '公共管理',
        customerPhone: '12345678902',
        customerEmail: 'contact@citygov.com',
        customerAddress: '市政府大楼',
        billId: 'ACC123457',
        billAmount: 5000.00,
        billType: '拨款',
        billDate: '2024-09-02',
        billStatus: '完成',
        status: '待发货'
    },
    {
        id: 'ORD789014',
        employee: '员工003',
        customerid: 'CUST123458',
        customerType: '个人',
        customerName: '李华',
        customerBusinessType: '自由职业',
        customerPhone: '12345678903',
        customerEmail: 'lihua@example.com',
        customerAddress: '长安街2号',
        billId: 'ACC123458',
        billAmount: 200.00,
        billType: '存款',
        billDate: '2024-09-03',
        billStatus: '发起',
        status: '已确认'
    },
    {
        id: 'ORD789015',
        employee: '员工004',
        customerid: 'CUST123459',
        customerType: '企业',
        customerName: '绿源能源',
        customerBusinessType: '可再生能源',
        customerPhone: '12345678904',
        customerEmail: 'service@greenenergy.com',
        customerAddress: '能源路8号',
        billId: 'ACC123459',
        billAmount: 3000.00,
        billType: '项目资金',
        billDate: '2024-09-04',
        billStatus: '已完成',
        status: '待确认'
    },
    {
        id: 'ORD789016',
        employee: '员工005',
        customerid: 'CUST123460',
        customerType: '个人',
        customerName: '张伟',
        customerBusinessType: '写作与出版',
        customerPhone: '12345678905',
        customerEmail: 'zhangwei@author.com',
        customerAddress: '文学街15号',
        billId: 'ACC123460',
        billAmount: 500.00,
        billType: '稿费',
        billDate: '2024-09-05',
        billStatus: '进行中',
        status: '已发货'
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

//公司项目
let projectList = [
    {
        id: 'PRO123456', // 项目编号

        manager: '项目经理001', // 对接管理ID——FK员工id

        employees: [
            { id: "100011", name: "老蒋" },
            { id: "100012", name: "老沈" },
            { id: "100013", name: "老韩" }
          ], // 项目员工集合
        kpiDate: '2024-09-06', // 绩效评定时间
        result: 1, // 评定结果打分
        judger: '评定员001', // 导航属性，追究评定者信息

        //file
        fileId: 'FILE123456', // 文件ID
        fileName: '报告总结', // 文件名
        fileType: '文档', // 文件类型
        fileContentType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // 内容类型
        fileSize: 2048, // 文件大小，单位为KB
        filePath: '/uploads/2024/09/', // 文件路径
        fileUploadDate: '2024-09-06', // 上传日期
        fileStatus: '已上传', // 状态

        //customer
        customerid: 'CUST123456', // 客户方ID——PK
        customerType: '企业', // 客户类型
        customerName: '蓝天科技', // 客户名称
        customerBusinessType: 'IT服务', // 业务类型
        customerPhone: '12345678901', // 联系电话
        customerEmail: 'info@blueskytech.com', // 电子邮箱
        customerAddress: '科技园区创新路1号', // 客户地址

        //bill
        billId: 'ACC123456', // 编号
        billAmount: 1000.00, // 金额
        billType: '存款', // 类型
        billDate: '2024-09-01', // 日期
        billStatus: '已完成',

        status: '进行中', // 订单状态
    },
    {
        id: 'PRO123457',
        manager: '项目经理002',
        employees: [
            { id: "100014", name: "老杨" },
            { id: "100015", name: "老朱" },
            { id: "100016", name: "老秦" },
            { id: "100017", name: "老尤" }
          ],
        kpiDate: '2024-09-07',
        result: 2,
        judger: '评定员002',
        fileId: 'FILE123457',
        fileName: '市场分析报告',
        fileType: '文档',
        fileContentType: 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
        fileSize: 1024,
        filePath: '/uploads/2024/09/',
        fileUploadDate: '2024-09-07',
        fileStatus: '已上传',
        customerid: 'CUST123457',
        customerType: '政府',
        customerName: '市政建设部',
        customerBusinessType: '城市建设',
        customerPhone: '12345678902',
        customerEmail: 'contact@cityconstruction.gov',
        customerAddress: '市政府路1号',
        billId: 'ACC123457',
        billAmount: 2500.00,
        billType: '拨款',
        billDate: '2024-09-08',
        billStatus: '进行中',
        status: '已完成'
    },
    {
        id: 'PRO123458',
        manager: '项目经理003',
        employees: [
            { id: "100018", name: "老许" },
            { id: "100019", name: "老何" },
            { id: "100020", name: "老吕" },
            { id: "100021", name: "老施" },
            { id: "100022", name: "老张" }
          ],
        kpiDate: '2024-09-08',
        result: 3,
        judger: '评定员003',
        fileId: 'FILE123458',
        fileName: '财务报表',
        fileType: '表格',
        fileContentType: 'application/vnd.ms-excel',
        fileSize: 512,
        filePath: '/uploads/2024/09/',
        fileUploadDate: '2024-09-08',
        fileStatus: '未上传',
        customerid: 'CUST123458',
        customerType: '个人',
        customerName: '孙小姐',
        customerBusinessType: '咨询服务',
        customerPhone: '12345678903',
        customerEmail: 'sunshine@example.com',
        customerAddress: '阳光小区2号',
        billId: 'ACC123458',
        billAmount: 1500.00,
        billType: '服务费',
        billDate: '2024-09-09',
        billStatus: '已完成',
        status: '待确认'
    },
    {
        id: 'PRO123459',
        manager: '项目经理004',
        employees: [
            { id: "100023", name: "老孔" },
            { id: "100024", name: "老曹" },
            { id: "100025", name: "老严" },
            { id: "100026", name: "老华" },
            { id: "100027", name: "老金" },
            { id: "100028", name: "老魏" },
            { id: "100029", name: "老陶" }
          ],
        kpiDate: '2024-09-09',
        result: 4,
        judger: '评定员004',
        fileId: 'FILE123459',
        fileName: '项目计划书',
        fileType: '文档',
        fileContentType: 'application/pdf',
        fileSize: 3072,
        filePath: '/uploads/2024/09/',
        fileUploadDate: '2024-09-09',
        fileStatus: '已上传',
        customerid: 'CUST123459',
        customerType: '企业',
        customerName: '红星制造',
        customerBusinessType: '机械制造',
        customerPhone: '12345678904',
        customerEmail: 'info@redstarmfg.com',
        customerAddress: '工业大道3号',
        billId: 'ACC123459',
        billAmount: 3000.00,
        billType: '项目款',
        billDate: '2024-09-10',
        billStatus: '已完成',
        status: '进行中'
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