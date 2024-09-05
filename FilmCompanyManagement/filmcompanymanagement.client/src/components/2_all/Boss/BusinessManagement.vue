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
            <el-button class="main_button" 
                :style="{ backgroundColor: businessID === '0' ? '#409EFF' : '', color: businessID === '0' ? 'white' : '' }" 
                type="primary" size="large" plain @click="setBusinessID('0')">外部投资</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: businessID === '1' ? '#409EFF' : '', color: businessID === '1' ? 'white' : '' }"
                type="primary" size="large" plain @click="setBusinessID('1')">成片购买</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: businessID === '2' ? '#409EFF' : '', color: businessID === '2' ? 'white' : '' }"
                type="primary" size="large" plain @click="setBusinessID('2')">设备租赁</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: businessID === '3' ? '#409EFF' : '', color: businessID === '3' ? 'white' : '' }"
                type="primary" size="large" plain @click="setBusinessID('3')">公司项目</el-button>
        </div>


        <div v-if="businessID == '0'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>

            <div class="dataTable">
                 <el-table :data="businesses_list" style="width: 100%">
                    <el-table-column prop="id" label="编号" width="120"></el-table-column>
                    <el-table-column prop="date" label="日期" width="120"></el-table-column>
                    <el-table-column prop="client" label="投资人" width="120"></el-table-column>
                    <el-table-column prop="price" label="金额" width="120"></el-table-column>
                    <el-table-column prop="functionary" label="负责人" width="120"></el-table-column>
                    <el-table-column prop="status" label="状态" width="120"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="200">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
            <!--表单显示-->
            <el-dialog title="详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
                <!--根据表单数据结构动态生成表单-->
                <el-form :model="investment_form" label-width="120px">
                    <el-form-item label="投资编号">
                        <el-input v-model="investment_form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="投资日期">
                        <el-date-picker v-model="investment_form.date"
                                        type="date"
                                        placeholder="选择日期">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="投资人">
                        <el-input v-model="investment_form.client"></el-input>
                    </el-form-item>
                    <el-form-item label="投资金额">
                        <el-input-number v-model="investment_form.price"></el-input-number>
                    </el-form-item>
                    <el-form-item label="投资负责人">
                        <el-input v-model="investment_form.functionary"></el-input>
                    </el-form-item>
                    <el-form-item label="投资状态">
                        <el-input v-model="investment_form.status"></el-input>
                    </el-form-item>
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="investment_form.remark"></el-input>
                    </el-form-item>
                </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <div v-if="businessID == '1'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="businesses_list" style="width: 100%">
                    <el-table-column prop="id" label="编号" width="120"></el-table-column>
                    <el-table-column prop="date" label="日期" width="120"></el-table-column>
                    <el-table-column prop="client" label="购买人" width="120"></el-table-column>
                    <el-table-column prop="price" label="金额" width="120"></el-table-column>
                    <el-table-column prop="functionary" label="负责人" width="120"></el-table-column>
                    <el-table-column prop="status" label="状态" width="120"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="200">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!--表单显示-->
                <el-dialog title="详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
                    <!--根据表单数据结构动态生成表单-->
                    <el-form :model="buy_form" label-width="120px">
                        <el-form-item label="投资编号">
                            <el-input v-model="buy_form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="投资日期">
                            <el-date-picker v-model="buy_form.date"
                                            type="date"
                                            placeholder="选择日期">
                            </el-date-picker>
                        </el-form-item>
                        <el-form-item label="投资人">
                            <el-input v-model="buy_form.client"></el-input>
                        </el-form-item>
                        <el-form-item label="投资金额">
                            <el-input-number v-model="buy_form.price"></el-input-number>
                        </el-form-item>
                        <el-form-item label="投资负责人">
                            <el-input v-model="buy_form.functionary"></el-input>
                        </el-form-item>
                        <el-form-item label="投资状态">
                            <el-input v-model="buy_form.status"></el-input>
                        </el-form-item>
                        <el-form-item label="备注">
                            <el-input type="textarea" v-model="buy_form.remark"></el-input>
                        </el-form-item>
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button type="primary" @click="submitForm">保存</el-button>
                        <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                    </span>
                </el-dialog>
            </div>
        </div>
        <div v-if="businessID == '2'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="businesses_list" style="width: 100%">
                    <el-table-column prop="id" label="编号" width="120"></el-table-column>
                    <el-table-column prop="date" label="日期" width="120"></el-table-column>
                    <el-table-column prop="client" label="租赁人" width="120"></el-table-column>
                    <el-table-column prop="price" label="金额" width="120"></el-table-column>
                    <el-table-column prop="functionary" label="负责人" width="120"></el-table-column>
                    <el-table-column prop="status" label="状态" width="120"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="200">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
            <!--表单显示-->
            <el-dialog title="详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
                <!--根据表单数据结构动态生成表单-->
                <el-form :model="lease_form" label-width="120px">
                    <el-form-item label="投资编号">
                        <el-input v-model="lease_form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="投资日期">
                        <el-date-picker v-model="lease_form.date"
                                        type="date"
                                        placeholder="选择日期">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="投资人">
                        <el-input v-model="lease_form.client"></el-input>
                    </el-form-item>
                    <el-form-item label="投资金额">
                        <el-input-number v-model="lease_form.price"></el-input-number>
                    </el-form-item>
                    <el-form-item label="投资负责人">
                        <el-input v-model="lease_form.functionary"></el-input>
                    </el-form-item>
                    <el-form-item label="投资状态">
                        <el-input v-model="lease_form.status"></el-input>
                    </el-form-item>
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="lease_form.remark"></el-input>
                    </el-form-item>
                </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <div v-if="businessID == '3'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                <el-table :data="businesses_list" style="width: 100%">
                    <el-table-column prop="id" label="编号" width="120"></el-table-column>
                    <el-table-column prop="date" label="日期" width="120"></el-table-column>
                    <el-table-column prop="client" label="客户" width="120"></el-table-column>
                    <el-table-column prop="price" label="金额" width="120"></el-table-column>
                    <el-table-column prop="functionary" label="负责人" width="120"></el-table-column>
                    <el-table-column prop="status" label="状态" width="120"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="200">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
            <!--表单显示-->
            <el-dialog title="详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
                <!--根据表单数据结构动态生成表单-->
                <el-form :model="project_form" label-width="120px">
                    <el-form-item label="投资编号">
                        <el-input v-model="project_form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="投资日期">
                        <el-date-picker v-model="project_form.date"
                                        type="date"
                                        placeholder="选择日期">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="投资人">
                        <el-input v-model="project_form.client"></el-input>
                    </el-form-item>
                    <el-form-item label="投资金额">
                        <el-input-number v-model="project_form.price"></el-input-number>
                    </el-form-item>
                    <el-form-item label="投资负责人">
                        <el-input v-model="project_form.functionary"></el-input>
                    </el-form-item>
                    <el-form-item label="投资状态">
                        <el-input v-model="project_form.status"></el-input>
                    </el-form-item>
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="project_form.remark"></el-input>
                    </el-form-item>
                </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
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
                dialogVisible: false,
                businessID: '0',
                businesses: [
                  { id: '0', name: '外部投资'},
                  { id: '1', name: '成片购买'},
                  { id: '2', name: '设备租赁'},
                  { id: '3', name: '公司项目'},
                ],
                businesses_list:[],
                template_form:{id:'0'},
                investment_form:    { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
                buy_form:           { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
                lease_form:         { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
                project_form:       { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
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
                this.getIncome();
            },
            //获取信息
            getIncome(){
                let path;
                switch(this.businessID){
                    case '0':
                        path='/api/get-invest';
                        break;
                    case '1':
                        path='/api/get-buy';
                        break;
                    case '2':
                        path='/api/get-lease';
                        break;
                    case '3':
                        path='/api/get-project';
                        break;
                }
                axios.get(path)
                    .then(response => {
                        this.businesses_list = response.data.businesses_list || [];
                    })
                    .catch(error => {
                        console.error('Error fetching:', error);
                    });
            },
            //提交表单
            submitForm() {
                let path;
                let form;
                switch(this.businessID){
                    case '0':
                        path='/api/submit-invest-form';
                        form=this.investment_form;
                        break;
                    case '1':
                        path='/api/submit-buy-form';
                        form=this.buy_form;
                        break;
                    case '2':
                        path='/api/submit-lease-form';
                        form=this.lease_form;
                        break;
                    case '3':
                        path='/api/submit-project-form';
                        form=this.project_form;
                        break;
                }
                axios.post(path, form)
                    .then(response => {
                        console.log('提交成功:', response.data.message); // 打印消息
                        this.$message({
                            type: 'success',
                            message: response.data.message
                        }); // 显示消息提示
                        this.dialogVisible = false; // 关闭对话框
                    })
                    .catch(error => {
                        console.error('提交表单失败', error);
                        this.$message({
                            type: 'error',
                            message: error.response.data.message // 假设错误信息也在 message 字段中
                        });
                    });
                //重新请求数据
                getIncome();
            },
                        //新建
            createNew(){
                switch(this.businessID){
                    case '0':this.investment_form = this.template_form;break;
                    case '1':this.buy_form = this.template_form;break;
                    case '2':this.lease_form = this.template_form;break;
                    case '3':this.project_form = this.template_form;break;
                }
                this.dialogVisible = true;
            },
            //删除
            Delete(row) {
                let path;
                switch(this.businessID){
                    case '0':path='/api/delete-invest-form';break;
                    case '1':path='/api/delete-buy-form';break;
                    case '2':path='/api/delete-lease-form';break;
                    case '3':path='/api/delete-project-form';break;
                }
                axios.post(path, this.row)
                    .then(response => {
                        console.log('删除成功:', response.data.message); // 打印消息
                        this.$message({
                            type: 'success',
                            message: response.data.message
                        }); // 显示消息提示
                    })
                    .catch(error => {
                        console.error('删除失败', error);
                        this.$message({
                            type: 'error',
                            message: error.response.data.message // 假设错误信息也在 message 字段中
                        });
                    });
            },
            // 查看详情
            viewDetails(row) {      
                // 根据申请类型发送请求
                switch(this.businessID){
                    case '0':
                        axios.post('/api/details-invest', { id: row.id}).then(response => {
                            this.investment_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取外部投资表单数据失败', error);
                        });
                        break;
                    case '1':
                        axios.post('/api/details-buy', { id: row.id}).then(response => {
                            this.buy_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取成片购买表单数据失败', error);
                        });
                        break;
                    case '2':
                        axios.post('/api/details-lease', { id: row.id}).then(response => {
                            this.lease_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取设备租赁表单数据失败', error);
                        });
                        break;
                    case '3':
                        axios.post('/api/details-project', { id: row.id}).then(response => {
                            this.project_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取公司项目表单数据失败', error);
                        });
                        break;
                }
            },
        },
        mounted() {
            this.getdata();
            this.getIncome();
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

    .header-container {
        max-width: 900px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-top: 10px;
        justify-content: right;
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

    .dataTable {
        max-width:1000px;
        display: flex;
        justify-content: center; /* 或 flex-start, flex-end, space-between, space-around */
        align-items: center; /* 或 flex-start, flex-end, stretch */
        width: 85%;
        float: left;
        border-collapse: collapse;
        margin-left: auto;
        margin-right: auto;
    }

    .ul_menu {
        list-style-type: none;
        padding-block-start: 0px;
        padding: 0;
        margin: 0;
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

    .main_button {
        transform: translate(10px, 10px);
    }

    .container {
        display: flex;
        flex-direction: column;
        padding: 10px;
    }

</style>
