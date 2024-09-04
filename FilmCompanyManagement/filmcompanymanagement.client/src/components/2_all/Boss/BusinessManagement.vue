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
            <button @click="setBusinessID('0')">外部投资</button>
            <button @click="setBusinessID('1')">成片购买</button>
            <button @click="setBusinessID('2')">设备租赁</button>
            <button @click="setBusinessID('3')">公司项目</button>
        </div>

        <div v-for="business in businesses" :key="business.id">
            <div v-if="business.id === businessID && businessID == '0'">
                <h3>外部投资</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>日期</th>
                                <th>金额</th>
                                <th>负责人</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="businesses_investment.length > 0">
                            <tr v-for="investmentData in businesses_investment" :key="investmentData.id">
                                <td>{{ investmentData.id }}</td>
                                <td>{{ investmentData.date }}</td>
                                <td>{{ investmentData.money }}</td>
                                <td>{{ investmentData.functionary }}</td>
                                <td>{{ investmentData.status }}</td>
                                <td>{{ investmentData.details }}</td>
                                <td><button @click="viewInvestmentDetails(investmentData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="businesses_investment.length <= 0" style="text-align: center;">没有外部投资数据</p>
                </div>
            </div>
            <div v-if="business.id === businessID && businessID == '1'">
                <h3>成片购买</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>日期</th>
                                <th>金额</th>
                                <th>负责人</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="businesses_buy.length > 0">
                            <tr v-for="buyData in businesses_buy" :key="buyData.id">
                                <td>{{ buyData.id }}</td>
                                <td>{{ buyData.date }}</td>
                                <td>{{ buyData.money }}</td>
                                <td>{{ buyData.functionary }}</td>
                                <td>{{ buyData.status }}</td>
                                <td>{{ buyData.details }}</td>
                                <td><button @click="viewBuyDetails(buyData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="businesses_buy.length <= 0" style="text-align: center;">没有成片购买数据</p>
                </div>
            </div>
            <div v-if="business.id === businessID && businessID == '2'">
                <h3>设备租赁</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>日期</th>
                                <th>金额</th>
                                <th>负责人</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="businesses_lease.length > 0">
                            <tr v-for="leaseData in businesses_lease" :key="leaseData.id">
                                <td>{{ leaseData.id }}</td>
                                <td>{{ leaseData.date }}</td>
                                <td>{{ leaseData.money }}</td>
                                <td>{{ leaseData.functionary }}</td>
                                <td>{{ leaseData.status }}</td>
                                <td>{{ leaseData.details }}</td>
                                <td><button @click="viewLeaseDetails(leaseData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="businesses_lease.length <= 0" style="text-align: center;">没有设备租赁数据</p>
                </div>
            </div>
            <div v-if="business.id === businessID && businessID == '3'">
                <h3>公司项目</h3>
                <div class="container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>日期</th>
                                <th>金额</th>
                                <th>负责人</th>
                                <th>状态</th>
                                <th>详情</th>
                            </tr>
                        </thead>
                        <tbody v-show="businesses_project.length > 0">
                            <tr v-for="projectData in businesses_project" :key="projectData.id">
                                <td>{{ projectData.id }}</td>
                                <td>{{ projectData.date }}</td>
                                <td>{{ projectData.money }}</td>
                                <td>{{ projectData.functionary }}</td>
                                <td>{{ projectData.status }}</td>
                                <td>{{ projectData.details }}</td>
                                <td><button @click="viewProjectDetails(projectData.id)">详情</button></td>
                            </tr>
                        </tbody>
                    </table>
                    <p v-if="businesses_project.length <= 0" style="text-align: center;">没有公司项目数据</p>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import axios from 'axios';

    export default {
        name: 'BusinessManagement',
        data() {
            return {
                name: '', // 获取登入姓名
                businessID: 0,
                businesses: [
                  { id: '0', name: '外部投资'},
                  { id: '1', name: '成片购买'},
                  { id: '2', name: '设备租赁'},
                  { id: '3', name: '公司项目'},
                ],
                businesses_investment:[],
                businesses_buy:[],
                businesses_lease:[],
                businesses_project:[],
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
            setBusinessID(id) {
                this.businessID = id;
                switch(this.businessID){
                    case '0':this.getInvestment();
                        break;
                    case '1':this.getBuy();
                        break;
                    case '2':this.getLease();
                        break;
                    case '3':this.getProject();
                        break;
                }
            },
           

            getInvestment() {
                axios.get('/api/businessInvestment')
                    .then(response => {
                        this.businesses_investment = response.data.businesses_investment || [];
                    })
                    .catch(error => {
                        console.error('Error fetching investment:', error);
                    });
            },
            // 查看详情的方法
            viewInvestmentDetails(id) {
                alert('查看投资详情:');
                alert(id);

                //请求信息
                getDetailsInvestment(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getInvestment();
            },
            //查看申请的详细信息
            getDetailsInvestment(id) {
                axios.post('/api/detailsInvestment', { id })
                    .then(result => {
                        this.businesses_investment = response.data.businesses_investment || [];
                    }).catch(error => {
                        console.error('Error fetching investment:', error);

                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeInvestment(id) {
                axios.post('/api/changeInvestment', { id })
                    .then(result => {
                        this.businesses_investment = response.data.businesses_investment || [];
                    }).catch(error => {
                        console.error('Error fetching investment:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            },


            getBuy() {
                axios.get('/api/businessBuy')
                    .then(response => {
                        this.businesses_buy = response.data.businesses_buy || [];
                    })
                    .catch(error => {
                        console.error('Error fetching buy:', error);
                    });
            },
            // 查看详情的方法
            viewBuyDetails(id) {
                alert('查看购买投资详情:');
                alert(id);
            
                //请求信息
                getDetailsBuy(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getBuy();
            },
            //查看申请的详细信息
            getDetailsBuy(id) {
                axios.post('/api/detailsBuy', { id })
                    .then(result => {
                        this.businesses_buy = response.data.businesses_buy || [];
                    }).catch(error => {
                        console.error('Error fetching buy:', error);
            
                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeBuy(id) {
                axios.post('/api/changeBuy', { id })
                    .then(result => {
                        this.businesses_buy = response.data.businesses_buy || [];
                    }).catch(error => {
                        console.error('Error fetching buy:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            },


            getLease() {
                axios.get('/api/businessLease')
                    .then(response => {
                        this.businesses_lease = response.data.businesses_lease || [];
                    })
                    .catch(error => {
                        console.error('Error fetching lease:', error);
                    });
            },
            // 查看详情的方法
            viewLeaseDetails(id) {
                alert('查看租赁详情:');
                alert(id);
            
                //请求信息
                getDetailsLease(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getLease();
            },
            //查看申请的详细信息
            getDetailsLease(id) {
                axios.post('/api/detailsLease', { id })
                    .then(result => {
                        this.businesses_lease = response.data.businesses_lease || [];
                    }).catch(error => {
                        console.error('Error fetching lease:', error);
            
                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeLease(id) {
                axios.post('/api/changeLease', { id })
                    .then(result => {
                        this.businesses_lease = response.data.businesses_lease || [];
                    }).catch(error => {
                        console.error('Error fetching lease:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            },


            getProject() {
                axios.get('/api/businessProject')
                    .then(response => {
                        this.businesses_project = response.data.businesses_project || [];
                    })
                    .catch(error => {
                        console.error('Error fetching project:', error);
                    });
            },
            // 查看详情的方法
            viewProjectDetails(id) {
                alert('查看项目详情:');
                alert(id);
            
                //请求信息
                getDetailsProject(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getProject();
            },
            //查看申请的详细信息
            getDetailsProject(id) {
                axios.post('/api/detailsProject', { id })
                    .then(result => {
                        this.businesses_project = response.data.businesses_project || [];
                    }).catch(error => {
                        console.error('Error fetching project:', error);
            
                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeProject(id) {
                axios.post('/api/changeProject', { id })
                    .then(result => {
                        this.businesses_project = response.data.businesses_project || [];
                    }).catch(error => {
                        console.error('Error fetching project:', error);
                        if (1) {
                            alert("修改成功")
                        } else {
                            alert("修改失败")
                        }
                    });
            }
        },
        mounted() {
            this.getdata();
        }
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
