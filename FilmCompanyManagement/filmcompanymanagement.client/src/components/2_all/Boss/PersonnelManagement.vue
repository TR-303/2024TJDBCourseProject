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

        <h2>人员管理</h2>

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

        <!-- 招聘与实习 -->
        <div v-if="employeeID == '0'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>

            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="编号" width="auto"></el-table-column>
                    <el-table-column prop="name" label="姓名" width="auto"></el-table-column>
                    <el-table-column prop="gender" label="性别" width="auto"></el-table-column>
                    <el-table-column prop="positionTitle" label="职位名称" width="auto"></el-table-column>
                    <el-table-column prop="state" label="招聘状态" width="auto"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="auto">
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
                <el-form :model="form" label-width="120px">
                    <el-form-item label="编号">
                      <el-input v-model="form.id" readonly></el-input>
                    </el-form-item>
                    <el-form-item label="姓名">
                      <el-input v-model="form.name"></el-input>
                    </el-form-item>
                    <el-form-item label="性别">
                      <el-input v-model="form.gender"></el-input>
                    </el-form-item>
                    <el-form-item label="出生日期">
                      <el-date-picker v-model="form.dateOfBirth" type="date"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="简历">
                      <el-input v-model="form.resume" type="textarea"></el-input>
                    </el-form-item>
                    <el-form-item label="职位名称">
                      <el-input v-model="form.positionTitle"></el-input>
                    </el-form-item>
                    <el-form-item label="联系电话">
                      <el-input v-model="form.phone"></el-input>
                    </el-form-item>
                    <el-form-item label="电子邮件">
                      <el-input v-model="form.email"></el-input>
                    </el-form-item>
                    <el-form-item label="面试官">
                      <el-input v-model="form.interviewer"></el-input>
                    </el-form-item>
                    <el-form-item label="面试阶段">
                      <el-input v-model="form.interviewerStage"></el-input>
                    </el-form-item>
                    <el-form-item label="招聘状态">
                      <el-input v-model="form.state"></el-input>
                    </el-form-item>

                    <!-- 只有当 state 为 '进入实习' 时才显示以下字段 -->
                    <el-form-item v-if="form.state === '进入实习'" label="实习导师">
                      <el-input v-model="form.adviser"></el-input>
                    </el-form-item>
                    <el-form-item v-if="form.state === '进入实习'" label="实习开始日期">
                      <el-date-picker v-model="form.internshipStartDate" type="date"></el-date-picker>
                    </el-form-item>
                    <el-form-item v-if="form.state === '进入实习'" label="实习结束日期">
                      <el-date-picker v-model="form.internshipEndDate" type="date"></el-date-picker>
                    </el-form-item>
                    <el-form-item v-if="form.state === '进入实习'" label="备注">
                      <el-input v-model="form.remarks" type="textarea"></el-input>
                    </el-form-item>
                </el-form>

                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <!-- 员工总览 -->
        <div v-if="employeeID == '1'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="员工编号" width="auto"></el-table-column>
                    <el-table-column prop="name" label="员工姓名" width="auto"></el-table-column>
                    <el-table-column prop="salary" label="员工薪水" width="auto"></el-table-column>
                    <el-table-column prop="status" label="员工状态" width="auto"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="auto">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!--表单显示-->
                <el-dialog title="详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
                    <!--根据表单数据结构动态生成表单-->
                    <el-form :model="form" label-width="120px">
                        <el-form-item label="员工编号">
                            <el-input v-model="form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="员工姓名">
                            <el-input v-model="form.name"></el-input>
                        </el-form-item>
                        <el-form-item label="员工薪水">
                            <el-input-number v-model="form.salary"></el-input-number>
                        </el-form-item>
                        <el-form-item label="员工状态">
                            <el-input v-model="form.status"></el-input>
                        </el-form-item>
                        <el-form-item label="备注">
                            <el-input type="textarea" v-model="form.remark"></el-input>
                        </el-form-item>
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button type="primary" @click="submitForm">保存</el-button>
                        <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                    </span>
                </el-dialog>
            </div>
        </div>
        <!-- 员工培训 -->
        <div v-if="employeeID == '2'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="培训编号" width="auto"></el-table-column>
                    <el-table-column prop="teacher" label="授课老师" width="auto"></el-table-column>
                    <el-table-column prop="dateTime" label="授课日期" width="auto"></el-table-column>
                    <el-table-column prop="timeSpan" label="持续时间" width="auto"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="auto">
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
                <el-form :model="form" label-width="auto">
                  <el-form-item label="培训编号">
                    <el-input v-model="form.id" disabled></el-input>
                  </el-form-item>
                  <el-form-item label="授课老师">
                    <el-input v-model="form.teacher"></el-input>
                  </el-form-item>
                  <el-form-item label="授课日期">
                    <el-date-picker v-model="form.date" type="date" placeholder="选择日期"></el-date-picker>
                  </el-form-item>
                  <el-form-item label="参与人员">
                    <el-table :data="form.drillEmployees">
                      <el-table-column label="姓名">
                        <template v-slot="scope">
                          <el-input v-model="scope.row" @input="updateStudent(scope.row, scope.$index)"></el-input>
                        </template>
                      </el-table-column>
                      <el-table-column label="操作">
                        <template v-slot="scope">
                          <el-button type="danger" @click="removeStudent(scope.$index)">删除</el-button>
                        </template>
                      </el-table-column>
                    </el-table>
                    <el-button type="primary" @click="addStudent">添加人员</el-button>
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
                template_form:  { id: '0',drillEmployees:[''] },
                form:           { id: '' },
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

            //表单用
            addStudent() {
              this.form.drillEmployees.push(''); // 添加一个新的空行
            },
            updateStudent(value, index) {
              this.form.drillEmployees[index] = value; // 更新学生信息
            },
            removeStudent(index) {
              this.form.drillEmployees.splice(index, 1); // 删除指定索引的学生
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
                switch(this.employeeID){
                    case '0':
                        path='/api/submit-invite-form';
                        break;
                    case '1':
                        path='/api/submit-overview-form';
                        break;
                    case '2':
                        path='/api/submit-train-form';
                        break;
                }
                axios.post(path, this.form)
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
                this.form = this.template_form;
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
                let path;
                switch(this.employeeID){
                    case '0':path='/api/details-invite';break;
                    case '1':path='/api/details-overview';break;
                    case '2':path='/api/details-train';break;
                }
                axios.post(path, { id: row.id}).then(response => {
                    this.form = response.data[0];
                // 显示表单
                this.dialogVisible = true;
                }).catch(error => {
                    console.error('获取表单数据失败', error);
                });
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
