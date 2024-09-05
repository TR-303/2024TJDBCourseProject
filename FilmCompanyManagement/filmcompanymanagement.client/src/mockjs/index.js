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