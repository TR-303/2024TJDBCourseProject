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
                :style="{ backgroundColor: employeeID === '2' ? '#409EFF' : '', color: employeeID === '2' ? 'white' : '' }"
                type="primary" size="large" plain @click="setEmployeeID('2')">人员总览</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: employeeID === '0' ? '#409EFF' : '', color: employeeID === '0' ? 'white' : '' }" 
                type="primary" size="large" plain @click="setEmployeeID('0')">招聘管理</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: employeeID === '1' ? '#409EFF' : '', color: employeeID === '1' ? 'white' : '' }"
                type="primary" size="large" plain @click="setEmployeeID('1')">实习总览</el-button>
            <el-button class="main_button" 
                :style="{ backgroundColor: employeeID === '3' ? '#409EFF' : '', color: employeeID === '3' ? 'white' : '' }"
                type="primary" size="large" plain @click="setEmployeeID('3')">员工培训</el-button>
        </div>

        <!-- 招聘管理 -->
        <div v-if="employeeID == '0'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>

            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="招聘编号" width="auto"></el-table-column>
                    <el-table-column prop="name" label="姓名" width="auto"></el-table-column>
                    <el-table-column prop="gender" label="性别" width="auto"></el-table-column>
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
                <el-form :model="form" label-width="120px">
                    <el-form-item label="招聘编号">
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="招聘人员姓名">
                        <el-input v-model="form.name"></el-input>
                    </el-form-item>
                    <el-form-item label="性别">
                        <el-radio-group v-model="form.gender">
                            <el-radio label="男"></el-radio>
                            <el-radio label="女"></el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="职位名称">
                        <el-input v-model="form.positionTitle"></el-input>
                    </el-form-item>
                    <el-form-item label="工资">
                        <el-input-number v-model="form.salary" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="联系电话">
                        <el-input-number v-model="form.phone" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="电子邮件">
                        <el-input v-model="form.email"></el-input>
                    </el-form-item>
                    <el-form-item label="面试官">
                        <el-select v-model="form.interviewer" placeholder="请选择员工" filterable clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.name" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.name }}</span>
                                    <span>{{ employee.id }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item>   
                    <el-form-item label="面试阶段">
                        <el-select v-model="form.interviewerStage" placeholder="请选择状态">
                            <el-option label="一面" value="一面"></el-option>
                            <el-option label="二面" value="二面"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="录用状态">
                        <el-select v-model="form.state" placeholder="请选择状态">
                            <el-option label="未录用" value="未录用"></el-option>
                            <el-option label="已录用" value="已录用"></el-option>
                        </el-select>
                    </el-form-item>
                </el-form>

                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <!-- 实习总览 -->
        <div v-if="employeeID == '1'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>

            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="internId" label="实习生编号" width="auto"></el-table-column>
                    <el-table-column prop="intern" label="姓名" width="auto"></el-table-column>
                    <el-table-column prop="advicer" label="指导老师" width="auto"></el-table-column>
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
                <el-form :model="form" label-width="120px">
                    <el-form-item label="实习生ID">
                        <el-select v-model="form.internId" placeholder="请选择员工" filterable @change="changeEmployee('intern')" clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.id" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.id }}</span>
                                    <span>{{ employee.name }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="实习生姓名"> 
                        <el-input v-model="form.intern" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="指导老师编号">
                        <el-select v-model="form.advicerId" placeholder="请选择员工" filterable @change="changeEmployee('adbicer')" clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.id" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.id }}</span>
                                    <span>{{ employee.name }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="指导老师姓名">
                        <el-input v-model="form.advicer" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="实习开始日期">
                        <el-date-picker v-model="form.internshipStartDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="实习结束日期">
                        <el-date-picker v-model="form.internshipEndDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="备注信息">
                        <el-input type="textarea" v-model="form.remarks"></el-input>
                    </el-form-item>
                </el-form>

                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="submitForm">保存</el-button>
                    <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                </span>
            </el-dialog>
        </div>
        <!-- 员工总览 -->
        <div v-if="employeeID == '2'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="员工编号" width="auto"></el-table-column>
                    <el-table-column prop="name" label="员工姓名" width="auto"></el-table-column>
                    <el-table-column prop="salary" label="员工薪水" width="auto"></el-table-column>
                    <el-table-column fixed="right" label="操作" width="auto">
                        <template v-slot="scope">
                            <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                            <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!--表单显示-->
                <el-dialog title="详细信息" v-model="dialogVisible" width="80%" :before-close="handleClose">
                    <!--根据表单数据结构动态生成表单-->
                    <el-form :model="form" label-width="120px">
                        <el-form-item label="员工编号">
                            <el-input v-model="form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="员工姓名">
                            <el-input v-model="form.name"></el-input>
                        </el-form-item>
                        <el-form-item label="性别">
                            <el-radio-group v-model="form.gender">
                                <el-radio label="男"></el-radio>
                                <el-radio label="女"></el-radio>
                            </el-radio-group>
                        </el-form-item>
                        <el-form-item label="职位">
                            <el-input v-model="form.position"></el-input>
                        </el-form-item>
                        <el-form-item label="联系电话">
                            <el-input-number v-model="form.phone" :controls="false"></el-input-number>
                        </el-form-item>
                        <el-form-item label="邮箱">
                            <el-input v-model="form.email"></el-input>
                        </el-form-item>
                        <el-form-item label="薪水">
                            <el-input-number v-model="form.salary" :controls="false"></el-input-number>
                        </el-form-item>
                        <el-collapse v-model="activeNames" @change="handleChange">
                            <el-collapse-item title="账单" name="1">
                                <el-form-item label="账单编号">
                                    <el-input v-model="form.billId" disabled></el-input>
                                </el-form-item>
                                <el-form-item label="账单金额">
                                    <el-input-number v-model="form.billAmount" :controls="false"></el-input-number>
                                </el-form-item>
                                <el-form-item label="账单类型">
                                    <el-select v-model="form.billType" placeholder="请选择部门">
                                        <el-option label="存款" value="存款"></el-option>
                                        <el-option label="拨款" value="拨款"></el-option>
                                    </el-select>
                                </el-form-item>
                                <el-form-item label="账单日期">
                                    <el-date-picker v-model="form.billDate" type="date" disabled></el-date-picker>
                                </el-form-item>
                                <el-form-item label="账单状态">
                                    <el-select v-model="form.billStatus" placeholder="请选择部门">
                                        <el-option label="发起" value="发起"></el-option>
                                        <el-option label="完成" value="完成"></el-option>
                                    </el-select>
                                </el-form-item>
                            </el-collapse-item>
                        <el-form-item label="KPI">
                            <el-input v-model="form.kpi" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="部门">
                            <el-select v-model="form.department" placeholder="请选择部门">
                                <el-option label="管理部" value="管理部"></el-option>
                                <el-option label="财务部" value="财务部"></el-option>
                                <el-option label="业务部" value="业务部"></el-option>
                            </el-select>
                        </el-form-item>

                            <el-collapse-item title="其它" name="2">
                                <el-form-item label="实习生">
                                    <el-table :data="form.interns" style="width: 50%">
                                        <el-table-column prop="internId" label="编号" width="auto"></el-table-column>
                                        <el-table-column prop="intern" label="姓名" width="auto"></el-table-column>
                                    </el-table>
                                </el-form-item>

                                <!-- Projects -->
                                <el-form-item label="项目">
                                    <el-table :data="form.projects" style="width: 75%">
                                        <el-table-column prop="id" label="编号" width="auto"></el-table-column>
                                        <el-table-column prop="manager" label="经理" width="auto"></el-table-column>
                                        <el-table-column prop="status" label="项目状态" width="auto"></el-table-column>
                                    </el-table>
                                </el-form-item>

                                <!-- Manage Projects -->
                                <el-form-item label="管理项目">
                                    <el-table :data="form.manageProjects" style="width: 50%">
                                        <el-table-column prop="id" label="编号" width="auto"></el-table-column>
                                        <el-table-column prop="status" label="项目状态" width="auto"></el-table-column>
                                    </el-table>
                                </el-form-item>

                                <!-- Attendances -->
                                <el-form-item label="考勤记录">
                                    <el-table :data="form.attendances" style="width: 50%">
                                        <el-table-column prop="date" label="日期" width="auto"></el-table-column>
                                        <el-table-column prop="status" label="考勤状态" width="auto"></el-table-column>
                                    </el-table>
                                </el-form-item>

                                <!-- Drills -->
                                <el-form-item label="培训记录">
                                    <el-table :data="form.drills" style="width: 75%">
                                        <el-table-column prop="id" label="名称" width="auto"></el-table-column>
                                        <el-table-column prop="teacher" label="日期" width="auto"></el-table-column>
                                        <el-table-column prop="dateTime" label="日期" width="auto"></el-table-column>
                                    </el-table>
                                </el-form-item>
                            </el-collapse-item>
                        </el-collapse>    
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button type="primary" @click="submitForm">保存</el-button>
                        <el-button type="primary" plain @click="dialogVisible = false">取消</el-button>
                    </span>
                </el-dialog>

            </div>
        </div>
        <!-- 员工培训 -->
        <div v-if="employeeID == '3'">
            <div class="header-container">
                <el-button type="primary" size="medium" @click="createNew">新建</el-button>
            </div>
            <div class="dataTable">
                 <el-table :data="employee_list" style="width: 1000">
                    <el-table-column prop="id" label="培训编号" width="auto"></el-table-column>
                    <el-table-column prop="teacher" label="授课老师" width="auto"></el-table-column>
                    <el-table-column prop="dateTime" label="授课日期" width="auto"></el-table-column>
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
                    <el-form-item label="培训编号">
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="培训讲师">
                        <el-select v-model="form.teacher" placeholder="请选择员工" filterable clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.name" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.name }}</span>
                                    <span>{{ employee.id }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item> 
                    <el-form-item label="培训开始时间">
                        <el-date-picker v-model="form.dateTime" type="datetime" placeholder="选择日期时间"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="培训结束时间">
                        <el-date-picker v-model="form.endTime" type="datetime" placeholder="选择日期时间"></el-date-picker>
                    </el-form-item>
                    <el-collapse v-model="activeNames" @change="handleChange">
                        <el-collapse-item title="参与人员" name="1">
                            <el-form-item label="">
                                <el-table :data="form.employees" style="width: 100%">
                                    <el-table-column label="编号" width="180">
                                      <template v-slot="scope">
                                        <el-select v-model="scope.row.id" placeholder="请选择职位" filterable clearable @change="updateEmployee(scope.row, scope.$index)">
                                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.id" :value="employee.id">
                                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                                    <span>{{ employee.id }}</span>
                                                    <span>{{ employee.name }}</span>
                                                </span>
                                            </el-option>
                                        </el-select>
                                      </template>
                                    </el-table-column>
                                    <el-table-column prop="name" label="姓名" width="180"></el-table-column>
                                    <el-table-column prop="right" label="操作" >
                                        <template v-slot="scope">
                                            <el-button type="danger" @click="removeEmployee(scope.$index)">删除</el-button>
                                        </template>
                                    </el-table-column>
                                </el-table>
                              <el-button type="primary" @click="addEmployee">添加人员</el-button>
                            </el-form-item>   
                        </el-collapse-item>
                    </el-collapse>   
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
                employeeID: '2',
                employee_list:[],
                overview_employee_list:[],
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
                axios.post('/api/data/userdata', { id: userId })
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
            changeEmployee(type){        //同步id的修改
                switch(type)
                {
                    case 'intern': this.form.intern = this.overview_employee_list.find(emp => emp.id === this.form.internId).name;break;
                    case 'adbicer': this.form.advicer = this.overview_employee_list.find(emp => emp.id === this.form.advicerId).name;break;
                }
            },
            addEmployee() {
                this.form.employees.push({id:'', name:''}); // 添加一个新的空行              
            },
            updateEmployee(value, index) {
                this.form.employees[index] = value; // 更新学生信息
                if(this.form.employees[index].id){
                    value.name = this.overview_employee_list.find(emp => emp.id === this.form.employees[index].id).name;
                }
                else{
                    value.name = '';
                }
            },
            removeEmployee(index) {
              this.form.employees.splice(index, 1); // 删除指定索引的学生
            },
            //获取信息
            getIncome(){
                let path;
                switch(this.employeeID){
                    case '0':
                        path='/api/PersonnelManagement/get-invite';
                        break;
                    case '1':
                        path='/api/PersonnelManagement/get-intern';
                        break;
                    case '2':
                        path='/api/PersonnelManagement/get-overview';
                        break;
                    case '3':
                        path='/api/PersonnelManagement/get-train';
                        break;
                }
                axios.get(path)
                    .then(response => {
                        this.employee_list = response.data|| [];
                        if(this.employeeID == '2')
                        {
                            this.overview_employee_list = this.employee_list;
                        }
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
                        if ( !this.form.name || !this.form.gender || !this.form.positionTitle || !this.form.salary || 
                            !this.form.phone || !this.form.email || !this.form.interviewer || !this.form.interviewerStage || !this.form.state) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/PersonnelManagement/submit-invite-form';
                        break;
                    case '1':
                        if ( !this.form.advicer || !this.form.internId || !this.form.intern || 
                            !this.form.internshipStartDate || !this.form.internshipEndDate || !this.form.remarks) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/PersonnelManagement/submit-intern-form';
                        break;
                    case '2':
                        if (!this.form.id || !this.form.name || !this.form.gender || !this.form.position || !this.form.salary || 
                            !this.form.phone || !this.form.email || !this.form.department || !this.form.kpi) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/PersonnelManagement/submit-overview-form';
                        break;
                    case '3':
                        if ( !this.form.teacher || !this.form.dateTime || !this.form.endTime ||  !this.form.employees[0].id) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/PersonnelManagement/submit-train-form';
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
                this.form = {id:'0', customerid: '0', fileId:'0', kpi:'0', employees:[{id: '',name: ''}] };
                this.dialogVisible = true;
            },
            //删除
            Delete(row) {
                let path;
                switch(this.employeeID){
                    case '0':path='/api/PersonnelManagement/delete-invite-form';break;
                    case '1':path='/api/PersonnelManagement/delete-intern-form';break;
                    case '2':path='/api/PersonnelManagement/delete-overview-form';break;
                    case '3':path='/api/PersonnelManagement/delete-train-form';break;
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
                //重新请求数据
                getIncome();
            },
            // 查看详情
            viewDetails(row) {      
                let path;
                switch(this.employeeID){
                    case '0':path='/api/PersonnelManagement/details-invite';break;
                    case '1':path='/api/PersonnelManagement/details-intern';break;
                    case '2':path='/api/PersonnelManagement/details-overview';break;
                    case '3':path='/api/PersonnelManagement/details-train';break;
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
