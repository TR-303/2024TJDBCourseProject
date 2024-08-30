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