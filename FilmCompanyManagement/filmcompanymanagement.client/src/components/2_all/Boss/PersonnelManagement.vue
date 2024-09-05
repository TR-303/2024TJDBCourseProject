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
                :style="{ backgroundColor: employeeID === '0' ? '#409EFF' : '', color: employeeID === '0' ? 'white' : '' }" 
                type="primary" size="large" plain @click="setEmployeeID('0')">招聘实习</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: employeeID === '1' ? '#409EFF' : '', color: employeeID === '1' ? 'white' : '' }"
                type="primary" size="large" plain @click="setEmployeeID('1')">人员总览</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: employeeID === '2' ? '#409EFF' : '', color: employeeID === '2' ? 'white' : '' }"
                type="primary" size="large" plain @click="setEmployeeID('2')">员工培训</el-button>
        </div>


        <div v-if="employeeID == '0'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>

            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 100%">
                    <el-table-column prop="id" label="编号" width="120"></el-table-column>
                    <el-table-column prop="name" label="姓名" width="120"></el-table-column>
                    <el-table-column prop="type" label="招聘类型" width="120"></el-table-column>
                    <el-table-column prop="salary" label="实习工资" width="120"></el-table-column>
                    <el-table-column prop="status" label="招聘状态" width="120"></el-table-column>
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
                <el-form :model="invite_form" label-width="120px">
                    <el-form-item label="编号">
                        <el-input v-model="invite_form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="姓名">
                        <el-input v-model="invite_form.name"></el-input>
                    </el-form-item>
                    <el-form-item label="招聘类型">
                        <el-input v-model="invite_form.type"></el-input>
                    </el-form-item>
                    <el-form-item label="实习工资">
                        <el-input-number v-model="invite_form.salary"></el-input-number>
                    </el-form-item>
                    <el-form-item label="招聘状态">
                        <el-input v-model="invite_form.status"></el-input>
                    </el-form-item>
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="invite_form.remark"></el-input>
                    </el-form-item>
                </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <div v-if="employeeID == '1'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 100%">
                    <el-table-column prop="id" label="员工编号" width="120"></el-table-column>
                    <el-table-column prop="name" label="员工姓名" width="120"></el-table-column>
                    <el-table-column prop="salary" label="员工薪水" width="120"></el-table-column>
                    <el-table-column prop="status" label="员工状态" width="120"></el-table-column>
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
                    <el-form :model="overview_form" label-width="120px">
                        <el-form-item label="员工编号">
                            <el-input v-model="overview_form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="员工姓名">
                            <el-input v-model="overview_form.name"></el-input>
                        </el-form-item>
                        <el-form-item label="员工薪水">
                            <el-input-number v-model="overview_form.salary"></el-input-number>
                        </el-form-item>
                        <el-form-item label="员工状态">
                            <el-input v-model="overview_form.status"></el-input>
                        </el-form-item>
                        <el-form-item label="备注">
                            <el-input type="textarea" v-model="overview_form.remark"></el-input>
                        </el-form-item>
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button type="primary" @click="submitForm">保存</el-button>
                        <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                    </span>
                </el-dialog>
            </div>
        </div>
        <div v-if="employeeID == '2'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 100%">
                    <el-table-column prop="id" label="培训编号" width="120"></el-table-column>
                    <el-table-column prop="teacher" label="授课老师" width="120"></el-table-column>
                    <el-table-column prop="date" label="授课日期" width="120"></el-table-column>
                    <el-table-column prop="student" label="学生" width="120"></el-table-column>
                    <el-table-column prop="status" label="培训状态" width="120"></el-table-column>
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
                <el-form :model="train_form" label-width="120px">
                    <el-form-item label="培训编号">
                        <el-input v-model="train_form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="授课老师">
                        <el-input v-model="train_form.teacher"></el-input>
                    </el-form-item>
                    <el-form-item label="授课日期">
                        <el-date-picker v-model="train_form.date"
                                        type="date"
                                        placeholder="选择日期">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="学生">
                        <el-input v-model="train_form.student"></el-input>
                    </el-form-item>
                    <el-form-item label="培训状态">
                        <el-input v-model="train_form.status"></el-input>
                    </el-form-item>
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="train_form.remark"></el-input>
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
                employeeID: '0',
                employees: [
                    { id: '0', name: '招聘实习' },
                    { id: '1', name: '人员总览' },
                    { id: '2', name: '员工培训' },
                ],
                employee_list:[],
                template_form:{id:'0'},
                invite_form:    { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
                overview_form:  { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
                train_form:     { id: '', date: '', client:'', price: '', functionary: '', status: '', remark: '' },
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
                this.getIncome();
            },
            //获取信息
            getIncome(){
                let path;
                switch(this.employeeID){
                    case '0':
                        path='/api/get-invite';
                        break;
                    case '1':
                        path='/api/get-overview';
                        break;
                    case '2':
                        path='/api/get-train';
                        break;
                }
                axios.get(path)
                    .then(response => {
                        this.employee_list = response.data.employee_list || [];
                    })
                    .catch(error => {
                        console.error('Error fetching:', error);
                    });
            },
            //提交表单
            submitForm() {
                let path;
                let form;
                switch(this.employeeID){
                    case '0':
                        path='/api/submit-invite-form';
                        form=this.invite_form;
                        break;
                    case '1':
                        path='/api/submit-overview-form';
                        form=this.overview_form;
                        break;
                    case '2':
                        path='/api/submit-train-form';
                        form=this.train_form;
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
                switch(this.employeeID){
                    case '0':this.invite_form = this.template_form;break;
                    case '1':this.overview_form = this.template_form;break;
                    case '2':this.train_form = this.template_form;break;
                }
                this.dialogVisible = true;
            },
            //删除
            Delete(row) {
                let path;
                switch(this.employeeID){
                    case '0':path='/api/delete-invite-form';break;
                    case '1':path='/api/delete-overview-form';break;
                    case '2':path='/api/delete-train-form';break;
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
                switch(this.employeeID){
                    case '0':
                        axios.post('/api/details-invite', { id: row.id}).then(response => {
                            this.invite_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取招聘实习表单数据失败', error);
                        });
                        break;
                    case '1':
                        axios.post('/api/details-overview', { id: row.id}).then(response => {
                            this.overview_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取人员总览表单数据失败', error);
                        });
                        break;
                    case '2':
                        axios.post('/api/details-train', { id: row.id}).then(response => {
                            this.train_form = response.data[0];
                        // 显示表单
                        this.dialogVisible = true;
                        }).catch(error => {
                            console.error('获取员工培训表单数据失败', error);
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
