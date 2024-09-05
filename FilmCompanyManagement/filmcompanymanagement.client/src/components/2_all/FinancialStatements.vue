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
                <li v-if="showSubMenu" class="sub_menu " role="menuitem" id="submenu_1" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="toggleInvesSubMenu">
                    查看外部投资
                </li>
                <!-- 三级导航项 -->
                <li v-if="showInvesSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_investment_unprocessed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showUnprocessedInvestmentF">
                    未处理
                </li>
                <li v-if="showInvesSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_investment_processed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showProcessedInvestmentF">
                    已处理
                </li>

                <!-- 二级导航项 -->
                <li v-if="showSubMenu" class="sub_menu" role="menuitem" id="submenu_2" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="toggleLeasingSubMenu">
                    查看设备租赁
                </li>
                <!-- 三级导航项 -->
                <li v-if="showLeasingSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_leasing_unprocessed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showUnprocessedLeasingF">
                    未处理
                </li>
                <li v-if="showLeasingSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_leasing_processed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showProcessedLeasingF">
                    已处理
                </li>

                <!-- 二级导航项 -->
                <li v-if="showSubMenu" class="sub_menu" role="menuitem" id="submenu_3" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="toggleBlockPurchaseOrderSubMenu">
                    查看成片购买订单
                </li>
                <!-- 三级导航项 -->
                <li v-if="showBlockPurchaseOrderSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_blockPurchaseOrder_unprocessed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showUnprocessedBlockPurchaseOrderF">
                    未处理
                </li>
                <li v-if="showBlockPurchaseOrderSubMenu" class="sub_menu sub_sub_menu" role="menuitem" id="submenu_blockPurchaseOrder_processed" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showProcessedBlockPurchaseOrderF">
                    已处理
                </li>

                <!-- 二级导航项 -->
                <li v-if="showSubMenu" class="sub_menu" role="menuitem" id="submenu_4" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showSalaryData">
                    查看工资
                </li>

                <!-- 二级导航项 -->
                <li v-if="showSubMenu" class="sub_menu" role="menuitem" id="submenu_5" tabindex="-1" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="showProjectIncomeData">
                    查看项目收入
                </li>

            </ul>
        </div>

        <!-- 外部投资未处理数据显示部分 -->
        <div class="container" v-if="showUnprocessedInvestment">
            <div class="container_head">
                <label class="container_head_left">未处理外部投资</label>
                <button class="container_head_right" @click="refreshUnprocessedInvestmentData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="unprocessedInvestmentList" style="width: 100%">
                    <el-table-column prop="investmentId" label="投资编号" />
                    <el-table-column prop="customerId" label="投资客户ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column label="操作">
                        <template #default="scope">
                            <el-button @click="markInvestmentAsProcessed(scope.row)">处理完成</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
        </div>

        <!-- 外部投资已处理数据显示部分 -->
        <div class="container" v-if="showProcessedInvestment">
            <div class="container_head">
                <label class="container_head_left">已处理外部投资</label>
                <button class="container_head_right" @click="refreshProcessedInvestmentData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="processedInvestmentList" style="width: 100%">
                    <el-table-column prop="investmentId" label="投资编号" />
                    <el-table-column prop="customerId" label="投资客户ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column prop="processedDate" label="处理完成日期" />
                </el-table>
            </div>
        </div>

        <!-- 设备租赁未处理数据显示部分 -->
        <div class="container" v-if="showUnprocessedLeasing">
            <div class="container_head">
                <label class="container_head_left">未处理设备租赁</label>
                <button class="container_head_right" @click="refreshUnprocessedLeasingData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="unprocessedLeasingList" style="width: 100%">
                    <el-table-column prop="projectId" label="项目编号" />
                    <el-table-column prop="dockingManagementId" label="对接管理ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column label="操作">
                        <template #default="scope">
                            <el-button @click="markLeasingAsProcessed(scope.row)">处理完成</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
        </div>

        <!-- 设备租赁已处理数据显示部分 -->
        <div class="container" v-if="showProcessedLeasing">
            <div class="container_head">
                <label class="container_head_left">已处理设备租赁</label>
                <button class="container_head_right" @click="refreshProcessedLeasingData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="processedLeasingList" style="width: 100%">
                    <el-table-column prop="projectId" label="项目编号" />
                    <el-table-column prop="dockingManagementId" label="对接管理ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column prop="processedDate" label="处理完成日期" />
                </el-table>
            </div>
        </div>

        <!-- 成片购买订单未处理数据显示部分 -->
        <div class="container" v-if="showUnprocessedBlockPurchaseOrder">
            <div class="container_head">
                <label class="container_head_left">未处理成片购买订单</label>
                <button class="container_head_right" @click="refreshUnprocessedBlockPurchaseOrderData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="unprocessedBlockPurchaseOrderList" style="width: 100%">
                    <el-table-column prop="orderId" label="订单编号" />
                    <el-table-column prop="blockFileId" label="对接管理ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column label="操作">
                        <template #default="scope">
                            <el-button @click="markBlockPurchaseOrderAsProcessed(scope.row)">处理完成</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
        </div>

        <!-- 成片购买订单已处理数据显示部分 -->
        <div class="container" v-if="showProcessedBlockPurchaseOrder">
            <div class="container_head">
                <label class="container_head_left">已处理成片购买订单</label>
                <button class="container_head_right" @click="refreshProcessedBlockPurchaseOrderData">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="processedBlockPurchaseOrderList" style="width: 100%">
                    <el-table-column prop="orderId" label="订单编号" />
                    <el-table-column prop="blockFileId" label="对接管理ID" />
                    <el-table-column prop="orderDate" label="订单日期" />
                    <el-table-column prop="invoiceNumber" label="账单编号" />
                    <el-table-column prop="amount" label="费用数额" />
                    <el-table-column prop="expenseType" label="费用类型" />
                    <el-table-column prop="processedDate" label="处理完成日期" />
                </el-table>
            </div>
        </div>

        <!-- 工资数据显示部分 -->
        <div class="container" v-if="showSalary">
            <div class="container_head">
                <label class="container_head_left">查看工资</label>
                <button class="container_head_right" @click="refreshSalaryData()">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="salaryList" style="width: 100%">
                    <el-table-column prop="payrollNumber" label="工资表编号" />
                    <el-table-column prop="ratingRecordId" label="评定记录ID" />
                    <el-table-column prop="ratingResult" label="评定结果" />
                    <el-table-column prop="rateeId" label="被评定者ID" />
                    <el-table-column prop="basePay" label="基本工资" />
                </el-table>
            </div>
        </div>

        <!-- 项目收入数据显示部分 -->
        <div class="container" v-if="showProjectIncome">
            <div class="container_head">
                <label class="container_head_left">查看项目收入</label>
                <button class="container_head_right" @click="refreshProjectIncomeData()">
                    刷新数据
                </button>
            </div>
            <div>
                <el-table :data="projectIncomeList" style="width: 100%">
                    <el-table-column prop="projectId" label="项目编号" />
                    <el-table-column prop="dockingManagementId" label="对接管理ID" />
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
                unprocessedInvestmentList: [], // 未处理投资数据列表
                processedInvestmentList: [], // 已处理投资数据列表
                
                unprocessedLeasingList: [], // 未处理设备租赁数据列表
                processedLeasingList: [], // 已处理设备租赁数据列表

                unprocessedBlockPurchaseOrderList: [], // 未处理成片购买订单数据列表
                processedBlockPurchaseOrderList: [], // 已处理成片购买订单数据列表


                //旧数据
                salaryList: [], // 工资数据列表
                projectIncomeList: [], // 项目收入数据列表


                showSubMenu: false, // 控制二级导航显示

                showInvesSubMenu: false, //控制外部投资三级导航显示
                showUnprocessedInvestment: false, // 控制未处理投资数据显示
                showProcessedInvestment: false, // 控制已处理投资数据显示

                showLeasingSubMenu: false, // 控制设备租赁三级导航显示
                showUnprocessedLeasing: false, // 控制未处理设备租赁数据显示
                showProcessedLeasing: false, // 控制已处理设备租赁数据显示

                showBlockPurchaseOrderSubMenu: false, // 控制成片购买订单三级导航显示
                showUnprocessedBlockPurchaseOrder: false, // 控制未处理成片购买订单数据显示
                showProcessedBlockPurchaseOrder: false, // 控制已处理成片购买订单数据显示

                //旧数据
                showSalary: false, // 控制工资数据显示
                showProjectIncome: false // 控制项目收入数据显示
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
                // 关闭三级菜单
                if (this.showInvesSubMenu === true)
                    this.showInvesSubMenu = false;
                if (this.showLeasingSubMenu === true)
                    this.showLeasingSubMenu = false;
                if (this.showBlockPurchaseOrderSubMenu === true)
                    this.showBlockPurchaseOrderSubMenu = false;
            },
            toggleInvesSubMenu() {
                // 切换显示外部投资的三级菜单
                this.showInvesSubMenu = !this.showInvesSubMenu;
            },
            toggleLeasingSubMenu() {
                // 切换显示设备租赁的三级菜单
                this.showLeasingSubMenu = !this.showLeasingSubMenu;
            },
            toggleBlockPurchaseOrderSubMenu() {
                // 切换显示成片购买订单的三级菜单
                this.showBlockPurchaseOrderSubMenu = !this.showBlockPurchaseOrderSubMenu;
            },
            // 显示未处理外部投资数据
            showUnprocessedInvestmentF() {
                this.showUnprocessedInvestment = true;
                this.showProcessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;
                this.showProcessedBlockPurchaseOrder = false;
                this.showUnprocessedBlockPurchaseOrder = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshUnprocessedInvestmentData(); // 获取未处理外部投资数据
            },
            // 显示已处理外部投资数据
            showProcessedInvestmentF() {
                this.showProcessedInvestment = true;
                this.showUnprocessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;
                this.showProcessedBlockPurchaseOrder = false;
                this.showUnprocessedBlockPurchaseOrder = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshProcessedInvestmentData(); // 获取已处理外部投资数据
            },

            // 显示未处理设备租赁数据
            showUnprocessedLeasingF() {
                this.showUnprocessedLeasing = true;
                this.showProcessedLeasing = false;
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showProcessedBlockPurchaseOrder = false;
                this.showUnprocessedBlockPurchaseOrder = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshUnprocessedLeasingData(); // 获取未处理设备租赁数据
            },
            // 显示已处理设备租赁数据
            showProcessedLeasingF() {
                this.showProcessedLeasing = true;
                this.showUnprocessedLeasing = false;
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showProcessedBlockPurchaseOrder = false;
                this.showUnprocessedBlockPurchaseOrder = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshProcessedLeasingData(); // 获取已处理设备租赁数据
            },

            // 显示未处理成片购买订单数据
            showUnprocessedBlockPurchaseOrderF() {
                this.showUnprocessedBlockPurchaseOrder = true;
                this.showProcessedBlockPurchaseOrder = false;
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshUnprocessedBlockPurchaseOrderData(); // 获取未处理成片购买订单数据
            },
            // 显示已处理成片购买订单数据
            showProcessedBlockPurchaseOrderF() {
                this.showProcessedBlockPurchaseOrder = true;
                this.showUnprocessedBlockPurchaseOrder = false;
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;

                this.showSalary = false; // 关闭工资数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.refreshProcessedBlockPurchaseOrderData(); // 获取已处理成片购买订单数据
            },

            showSalaryData() {
                this.showSalary = true; // 显示工资数据
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;


                this.showBlockPurchaseOrder = false; // 关闭成片购买订单数据
                this.showProjectIncome = false; // 关闭项目收入数据
                this.getSalaryData(); // 获取工资数据
            },
            showProjectIncomeData() {
                this.showProjectIncome = true; // 显示项目收入数据
                this.showUnprocessedInvestment = false;
                this.showProcessedInvestment = false;
                this.showUnprocessedLeasing = false;
                this.showProcessedLeasing = false;

                this.showBlockPurchaseOrder = false; // 关闭成片购买订单数据
                this.showSalary = false; // 关闭工资数据
                this.getProjectIncomeData(); // 获取项目收入数据
            },
            /**************** 外部投资 ****************/
            // 获取未处理外部投资数据
            // 带请求参数`orderStatus` (string): 订单状态，值为“approved”表示管理员已批准
            refreshUnprocessedInvestmentData() {
                axios.get('/api/externalInvestments/unprocessed', {
                    params: { orderStatus: 'approved' }
                }).then(response => {
                    this.unprocessedInvestmentList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching investment data:', error);
                });
            },
            // 获取财务已处理的外部投资数据
            refreshProcessedInvestmentData() {
                axios.get('/api/externalInvestments/processed', {
                    params: { financialStatus: 'processed' } // 添加请求参数
                }).then(response => {
                    this.processedInvestmentList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching processed investment data:', error);
                });
            },
            // 将未处理投资标记为已处理
            markInvestmentAsProcessed(row) {
                const currentDate = new Date().toISOString().split('T')[0]; // 获取当前日期
                // 发送 API 请求将该投资标记为处理完成
                axios.post('/api/externalInvestments/markProcessed', {
                    investmentId: row.investmentId, // 根据当前行的数据传递 investmentId
                    processedDate: currentDate // 添加 processedDate
                }).then(response => {
                    if (response.data.success) {
                        // 请求成功后移除该项
                        this.unprocessedInvestmentList = this.unprocessedInvestmentList.filter(item => item.investmentId !== row.investmentId);
                        // 也可以在这里调用刷新数据的方法来重新获取列表
                        this.refreshUnprocessedInvestmentData();
                        this.refreshProcessedInvestmentData();
                    } else {
                        console.error('标记处理失败:', response.data.message);
                    }
                }).catch(error => {
                    console.error('请求失败:', error);
                });
            },

            /**************** 设备租赁 ****************/
            // 获取未处理设备租赁数据
            refreshUnprocessedLeasingData() {
                axios.get('/api/equipmentLeasing/unprocessed', {
                    params: { orderStatus: 'approved' } // 添加请求参数
                }).then(response => {
                    this.unprocessedLeasingList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching unprocessed leasing data:', error);
                });
            },
            // 获取财务已处理的设备租赁数据
            refreshProcessedLeasingData() {
                axios.get('/api/equipmentLeasing/processed', {
                    params: { financialStatus: 'processed' } // 添加请求参数
                }).then(response => {
                    this.processedLeasingList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching processed leasing data:', error);
                });
            },
            // 将未处理设备租赁标记为已处理
            markLeasingAsProcessed(row) {
                const currentDate = new Date().toISOString().split('T')[0]; // 获取当前日期
                axios.post('/api/equipmentLeasing/markProcessed', {
                    projectId: row.projectId, // 根据当前行的数据传递 projectId
                    processedDate: currentDate // 添加 processedDate
                }).then(response => {
                    if (response.data.success) {
                        this.unprocessedLeasingList = this.unprocessedLeasingList.filter(item => item.projectId !== row.projectId);
                        this.refreshUnprocessedLeasingData();
                        this.refreshProcessedLeasingData();
                    } else {
                        console.error('标记处理失败:', response.data.message);
                    }
                }).catch(error => {
                    console.error('请求失败:', error);
                });
            },

            /**************** 成片购买订单 ****************/
            // 获取未处理成片购买订单数据
            refreshUnprocessedBlockPurchaseOrderData() {
                axios.get('/api/blockPurchaseOrders/unprocessed', {
                    params: { orderStatus: 'approved' } // 添加请求参数
                }).then(response => {
                    this.unprocessedBlockPurchaseOrderList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching unprocessed block purchase order data:', error);
                });
            },
            // 获取已处理成片购买订单数据
            refreshProcessedBlockPurchaseOrderData() {
                axios.get('/api/blockPurchaseOrders/processed', {
                    params: { financialStatus: 'processed' } // 添加请求参数
                }).then(response => {
                    this.processedBlockPurchaseOrderList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching processed block purchase order data:', error);
                });
            },
            // 将未处理成片购买订单标记为已处理
            markBlockPurchaseOrderAsProcessed(row) {
                const currentDate = new Date().toISOString().split('T')[0]; // 获取当前日期
                axios.post('/api/blockPurchaseOrders/markProcessed', {
                    orderId: row.orderId, // 根据当前行的数据传递 orderId
                    processedDate: currentDate // 添加 processedDate
                }).then(response => {
                    if (response.data.success) {
                        this.unprocessedBlockPurchaseOrderList = this.unprocessedBlockPurchaseOrderList.filter(item => item.orderId !== row.orderId);
                        this.refreshUnprocessedBlockPurchaseOrderData();
                        this.refreshProcessedBlockPurchaseOrderData();
                    } else {
                        console.error('标记处理失败:', response.data.message);
                    }
                }).catch(error => {
                    console.error('请求失败:', error);
                });
            },

            refreshSalaryData() {
                this.getSalaryData();
            },
            refreshProjectIncomeData() {
                this.getProjectIncomeData();
            },

            /**************** 工资 ****************/
            getSalaryData() {
                axios.get('/api/salary/unprocessed', {
                    params: { evaluationStatus: 'complete' } // 添加请求参数
                }).then(response => {
                    this.salaryList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching salary data:', error);
                });
            },

            /**************** 项目收入 ****************/
            getProjectIncomeData() {
                axios.get('/api/projectIncome/unprocessed', {
                    params: { orderStatus: 'approved' } // 添加请求参数
                }).then(response => {
                    this.projectIncomeList = response.data.data;
                }).catch(error => {
                    console.error('Error fetching project income data:', error);
                });
            }
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
        justify-content: space-between;
        padding: 0 10px;
    }

    .container_head_left {
        height: 30px;
        font-size: 20px;
        width: 180px;
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
        border: none;
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
        margin: 4px 0;
/*        box-shadow: 0px 0px 10px 1.5px rgba(199, 198, 198, 0.893);*/
        height: 20px;
        cursor: pointer;
        font-size: 14px;
        padding: 5px 0 5px 10px;
        list-style-type: none;
        width: 130px;
        margin-left: 3px;
    }

    #submenu_1 {
        margin-top: -2px;
    }

    .sub_sub_menu{
        width: 100px;
        margin: 0 auto 3px auto;
    }

    #submenu_investment_processed{
        margin-bottom: 8px;
    }
</style>
