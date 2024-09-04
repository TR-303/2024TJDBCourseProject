import Mock from 'mockjs'
//infopage用
let dataList = [
    { id: '1', name: 'boss' },
    { id: '2', name: 'manager' },
    { id: '3', name: 'worker' }
];
let issignList = [
    { id: '1', issign: '0' },
    { id: '2', issign: '0' },
    { id: '3', issign: '0' }
]
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

// 外部投资数据
let investmentList = [
    { investmentId: '1', customerId: '11', orderDate: '2024-08-01', invoiceNumber: '20240801', amount: 5000, expenseType: '广告费用' },
    { investmentId: '2', customerId: '22', orderDate: '2024-08-15', invoiceNumber: '20240815', amount: 10000, expenseType: '制作费用' },
    { investmentId: '3', customerId: '33', orderDate: '2024-08-16', invoiceNumber: '20240816', amount: 2000, expenseType: '广告费用' },
    { investmentId: '4', customerId: '44', orderDate: '2024-08-17', invoiceNumber: '20240817', amount: 30000, expenseType: '制作费用' },
];

// 获取外部投资数据
Mock.mock('/api/externalinvestments/unprocessed', 'get', (params) => {
    // 返回数据
    return {
        code: 200,
        data: investmentList
    };
});

// 设备租赁数据
let leasingList = [
    { projectId: 'P001', dockingManagementId: 'D001', orderDate: '2024-09-01', invoiceNumber: '20240901', amount: 15000, expenseType: '设备租赁费用' },
    { projectId: 'P002', dockingManagementId: 'D002', orderDate: '2024-09-10', invoiceNumber: '20240910', amount: 8000, expenseType: '设备维护费用' }
];

// 获取设备租赁数据(不带请求参数)--仅供自己测试使用
//Mock.mock('/api/equipmentLeasing/unprocessed', 'get', (params) => {
//    return {
//        code: 200,
//        data: leasingList
//    };
//});

//获取设备租赁数据（带请求参数）
Mock.mock(RegExp('/api/equipmentLeasing/unprocessed' + ".*"), 'get', (params) => {
    const url = new URL(params.url, 'http://localhost'); // 创建 URL 对象
    const query = new URLSearchParams(url.search); // 获取查询参数
    const orderStatus = query.get('orderStatus'); // 获取 orderStatus 参数

    console.log('Received query:', { orderStatus }); // 调试信息

    if (orderStatus === 'approved') {
        //console.log("approved!!!");
        return {
            code: 200,
            data: leasingList // 返回符合条件的数据
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
            success:0,
        }
}
)

//签到时间：传了id和time
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

//部门情况需求：传了department，返回员工姓名，电话号码
Mock.mock('/data/departmentuserdata', 'post',(params)=> {
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
    const issigndata = issignList.filter(item => item.id === data.id);
    if (issigndata) {
        return issigndata;
    }
}
)
