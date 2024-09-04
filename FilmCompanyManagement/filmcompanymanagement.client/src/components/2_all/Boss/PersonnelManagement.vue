<template>
    <div>
        <div class="head">
            <div class="head_left">
                <img class="head_logo" src="@/assets/logo.png" />
                <label class="head_center">摄影公司管理系统</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="@/assets/User.jpg" />
                <label class="head_center">你好, {{ name }} </label>
                <button class="head_button" id="exit" @click="jump($event)">
                    退出登录
                </button>
            </div>
        </div>
        <div class="aside">
            <ul class="ul_menu">
                <li role="menuitem" id="menu_0" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                    首页
                </li>
                <li role="menuitem" id="menu_1" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                    部门情况
                </li>
                <li v-if="showBossMenu" role="menuitem" id="menu_5" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                    人员管理
                </li>
                <li v-if="showBossMenu" role="menuitem" id="menu_6" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                    申请批准
                </li>
                <li v-if="showBossMenu" role="menuitem" id="menu_7" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                    业务管理
                </li>
            </ul>
        </div>

        <div>
            <button @click="setEmployeeID('0')">招聘实习</button>
            <button @click="setEmployeeID('1')">人员总览</button>
            <button @click="setEmployeeID('2')">员工培训</button>
        </div>
        
        <div v-for="employee in employees" :key="employee.id">
            <div v-if="employee.id === employeeID && employeeID == '0'">
                <h3>外部投资</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>姓名</th>
                                <th>类型</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="employee_invite.length > 0">
                            <tr v-for="inviteData in employee_invite" :key="inviteData.id">
                                <td>{{ inviteData.id }}</td>
                                <td>{{ inviteData.name }}</td>
                                <td>{{ inviteData.type }}</td>
                                <td>{{ inviteData.status }}</td>
                                <td><button @click="viewInviteDetails(inviteData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="employee_invite.length <= 0" style="text-align: center;">没有招聘数据</p>
                </div>
            </div>
            <div v-if="employee.id === employeeID && employeeID == '1'">
                <h3>成片购买</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>姓名</th>
                                <th>薪水</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="employee_overview.length > 0">
                            <tr v-for="overviewData in employee_overview" :key="overviewData.id">
                                <td>{{ overviewData.id }}</td>
                                <td>{{ overviewData.name }}</td>
                                <td>{{ overviewData.salary }}</td>
                                <td><button @click="viewOverviewDetails(overviewData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="employee_overview.length <= 0" style="text-align: center;">没有人员总览数据</p>
                </div>
            </div>
            <div v-if="employee.id === employeeID && employeeID == '2'">
                <h3>设备租赁</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>教师</th>
                                <th>日期</th>
                                <th>学生</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="employee_train.length > 0">
                            <tr v-for="trainData in employee_train" :key="trainData.id">
                                <td>{{ trainData.id }}</td>
                                <td>{{ trainData.teacher }}</td>
                                <td>{{ trainData.date }}</td>
                                <td>{{ trainData.student }}</td>
                                <td>{{ trainData.status }}</td>
                                <td><button @click="viewTrainDetails(trainData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="employee_train.length <= 0" style="text-align: center;">没有员工培训数据</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'AuthorizeRequisition',
        data() {
            return {
                name: '', // 获取登入姓名
                employeeID: 0,
                employees: [
                    { id: '0', name: '招聘实习' },
                    { id: '1', name: '人员总览' },
                    { id: '2', name: '员工培训' },
                ],
                employee_invite: [],
                employee_overview: [],
                employee_train: [],
            }
        },
        computed: {
            showBossMenu() {
                return this.$route.query.id === '1';
            }
        },
        methods: {
            addShadow(event) {
                event.target.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            },
            removeShadow(event) {
                event.target.style.boxShadow = 'none';
            },
            jump(event) {
                const id = this.$route.query.id || '1'; // 默认 id 为 '1'
                switch (event.target.id) {
                    case 'menu_0':
                        this.$router.push({ path: '/Infopage', query: { id } });
                        break;
                    case 'menu_1':
                        this.$router.push({ path: '/Department', query: { id } });
                        break;
                    case 'menu_5':
                        this.$router.push({ path: '/PersonnelM', query: { id } });
                        break;
                    case 'menu_6':
                        this.$router.push({ path: '/AuthorizeR', query: { id } });
                        break;
                    case 'menu_7':
                        this.$router.push({ path: '/BusinessM', query: { id } });
                        break;
                    case 'exit':
                        this.$router.push({ path: '/', query: { id } });
                        break;
                }
            },
            getdata() {
                const userId = this.$route.query.id;
                axios.post('/data/userdata', { id: userId })
                    .then(result => {
                        this.name = result.data.name || '未定义'; // 确保 name 有默认值
                    })
                    .catch(error => {
                        console.error('Error fetching mock data:', error);
                    });
            },
            setEmployeeID(id) {
                this.employeeID = id;
                switch (this.employeeID) {
                    case '0': this.getInvite();
                        break;
                    case '1': this.getOverview();
                        break;
                    case '2': this.getTrain();
                        break;
                }
            },

            getInvite() {
                axios.get('/api/employeeInvite')
                    .then(response => {
                        this.employee_invite = response.data.employee_invite || [];
                    })
                    .catch(error => {
                        console.error('Error fetching Invite:', error);
                    });
            },
            // 查看详情的方法
            viewInviteDetails(id) {
                alert('查看招聘详情:');
                alert(id);

                //请求信息
                getDetailsInvite(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getInvite();
            },
            //查看申请的详细信息
            getDetailsInvite(id) {
                axios.post('/api/detailsInvite', { id })
                    .then(result => {
                        this.employee_invite = response.data.employee_invite || [];
                    }).catch(error => {
                        console.error('Error fetching Invite:', error);

                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeInvite(id) {
                axios.post('/api/changeInvite', { id })
                    .then(result => {
                        this.employee_invite = response.data.employee_invite || [];
                    }).catch(error => {
                        console.error('Error fetching Invite:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            },


            getOverview() {
                axios.get('/api/employeeOverview')
                    .then(response => {
                        this.employee_overview = response.data.employee_overview || [];
                    })
                    .catch(error => {
                        console.error('Error fetching Overview:', error);
                    });
            },
            // 查看详情的方法
            viewOverviewDetails(id) {
                alert('查看人员详情:');
                alert(id);

                //请求信息
                getDetailsOverview(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getOverview();
            },
            //查看申请的详细信息
            getDetailsOverview(id) {
                axios.post('/api/detailsOverview', { id })
                    .then(result => {
                        this.employee_overview = response.data.employee_overview || [];
                    }).catch(error => {
                        console.error('Error fetching Overview:', error);

                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeOverview(id) {
                axios.post('/api/changeOverview', { id })
                    .then(result => {
                        this.employee_overview = response.data.employee_overview || [];
                    }).catch(error => {
                        console.error('Error fetching Overview:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            },


            getTrain() {
                axios.get('/api/employeeTrain')
                    .then(response => {
                        this.employee_train = response.data.employee_train || [];
                    })
                    .catch(error => {
                        console.error('Error fetching Train:', error);
                    });
            },
            // 查看详情的方法
            viewTrainDetails(id) {
                alert('查看培训详情:');
                alert(id);

                //请求信息
                getDetailsTrain(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getTrain();
            },
            //查看申请的详细信息
            getDetailsTrain(id) {
                axios.post('/api/detailsTrain', { id })
                    .then(result => {
                        this.employee_train = response.data.employee_train || [];
                    }).catch(error => {
                        console.error('Error fetching Train:', error);

                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeTrain(id) {
                axios.post('/api/changeTrain', { id })
                    .then(result => {
                        this.employee_train = response.data.employee_train || [];
                    }).catch(error => {
                        console.error('Error fetching Train:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });

            },
            mounted() {
                this.getdata();
            },
        },
    }
</script>

<style scoped>
    .head {
        display: flex;
        width: 100%;
        background: linear-gradient(aqua,70%,blue);
        border: 20px;
        padding: 5px;
        box-sizing: border-box;
        color: black;
    }

    .head_left {
        flex: 1 1 auto;
        display: flex;
        gap: var(--bar-size-8,5%);
    }

    .head_center {
        font-size: 20px;
        text-align: center;
    }

    .head_right {
        float: right;
        display: flex;
        margin-right: 10%;
        gap: var(--bar-size-5,0%);
    }

    .head_logo {
        width: 30px;
        height: 30px;
        border: 0;
        margin: 0 5px;
        outline-offset: 2px;
    }

    .head_button {
        margin: 0 10px;
        background-color: deepskyblue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 18px;
        width: 100px;
    }

        .head_button:hover {
            background-color: #1976d2;
            /* 按钮悬浮效果 */
        }

    .aside {
        justify-content: center;
        align-items: center;
        border: 2px solid rgba(239, 242, 252, 0.801);
        float: left;
        width: 150px;
        height: 100vh;
        background-color: lightskyblue;
        box-sizing: border-box;
    }

    .li_node {
        display: flex;
        text-align: center;
        background-color: rgba(229, 242, 252, 0.801);
        margin: 8px 0;
        box-shadow: 0px 0px 10px 1.5px rgba(199, 198, 198, 0.893);
        height: 20px;
        cursor: pointer;
        font-size: 20px;
        padding: 10px 0 20px 10px;
    }

    .ul_menu {
        list-style-type: none;
        padding-block-start: 0px;
        padding: 0;
        margin: 0;
    }

    .container {
        display: flex;
        flex-direction: column;
        padding: 10px;
    }

    table {
        width: 100%;
        border-collapse: collapse;
    }

    th, td {
        padding: 8px;
        text-align: left;
        border-bottom: 1px solid #ddd;
    }
</style>
