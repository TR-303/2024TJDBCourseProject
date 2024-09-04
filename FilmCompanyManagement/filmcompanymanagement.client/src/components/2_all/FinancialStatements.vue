<template>
    <div>
        <div class="head">
            <div class="head_left">
                <img class="head_logo" src="@/assets/logo.png" />
                <label class="head_center">摄影公司管理系统</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="@/assets/User.jpg" />
                <!-- 这里获取登入姓名 -->
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
                <li role="menuitem" id="menu_2" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="toggleSubMenu">
                    查看财政表

                </li>
                <!-- 二级导航项 -->
                
                    <li  v-if="showSubMenu" class="sub_menu " role="menuitem" id="submenu_1" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showInvestmentData">
                        查看外部投资
                    </li>
                
            </ul>
        </div>
        <div class="container" v-if="showInvestment">
            <div class="container_head">
                <label class="container_head_left">查看外部投资</label>
                <button class="container_head_right" @click="refreshData()">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="investmentList" style="width: 100%">
                    <el-table-column prop="investmentId" label="投资编号" />
                    <el-table-column prop="customerId" label="投资客户ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                </el-table>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                name: '',
                investmentList: [],
                showSubMenu: false, // 控制二级导航显示
                showInvestment: false // 控制投资数据显示
            };
        },
        methods: {
            addShadow(event) {
                event.target.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            },
            removeShadow(event) {
                event.target.style.boxShadow = 'none';
            },
            jump(event) {
                console.log(event.target.id)
                if (event.target.id === 'menu_0') {
                    this.$router.push({ path: '/Infopage', query: { id: this.id } });
                }
                if (event.target.id === 'menu_1') {
                    this.$router.push({ path: '/Department', query: { id: this.id } });
                }
                
                if (event.target.id === 'exit') {
                    this.$router.push({ path: '/', query: { id: this.id } });
                }
            },
            toggleSubMenu() {
                // 切换显示二级菜单
                this.showSubMenu = !this.showSubMenu;
            },
            showInvestmentData() {
                this.showInvestment = true; // 显示投资数据
                this.getInvestmentData(); // 获取投资数据
            },
            refreshData() {
                this.getInvestmentData();
            },
            getInvestmentData() {
                axios.get('/api/externalinvestments/unprocessed')
                .then(response => {
                    //console.log(response.data);
                    this.investmentList = response.data.data;
                    //console.log(this.investmentList); 
                }).catch(error => {
                    console.error('Error fetching investment data:', error);
                });
            }
            //带请求参数`orderStatus` (string): 订单状态，值为“approved”表示管理员已批准
            //getInvestmentData() {
            //    axios.get('/api/externalinvestments/unprocessed', {
            //        params: { orderStatus: 'approved' }
            //    }).then(response => {
            //        this.investmentList = response.data;
            //    }).catch(error => {
            //        console.error('Error fetching investment data:', error);
            //    });
            //}
        },
        mounted() {
            // 可以在页面加载时预加载投资数据，或留给用户手动触发
            // this.getInvestmentData();
        }
    };
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

    .container_head {
        flex: 1 1 auto;
/*        gap: var(--base-size-12,10px);*/
        display: flex;
        justify-content:space-between;
        padding: 0 10px;
    }

    .container_head_left {
        height: 30px;
        font-size: 20px;
        width: 120px;
        display: flex;
        text-align: center;
    }

    .container_head_mid {
        flex: auto;
        width: 80%;
        display: flex;
        text-align: left;
        height: 30px;
    }

    .container_head_right {
        background-color: deepskyblue;
        color: white;
        border:none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 13px;
        width: 70px;
        text-align: center;
        height: 30px;
        font-weight: 600px;
    }

    .container_head_right:hover {
            background-color: #1976d2;
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

    .container {
        display: flex;
        flex-direction: column;
        padding: 10px;
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

    .window1 {
        margin: 5px;
        padding: 10px;
        text-align: center;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: space-between;
        border: 2px solid black;
        width: 6px;
        height: 10px;
        box-sizing: border-box;
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

    .sub_menu {
        display: flex;
        text-align: center;
        background-color: rgba(229, 242, 252, 0.801);
        margin: 8px 0;
        box-shadow: 0px 0px 10px 1.5px rgba(199, 198, 198, 0.893);
        height: 20px;
        cursor: pointer;
        font-size: 14px;
        padding: 5px 0 5px 10px;
        list-style-type: none;
    }

    .sub_menu .li_node {
/*            padding: 5px 10px;
            cursor: pointer;*/
    }

</style>
