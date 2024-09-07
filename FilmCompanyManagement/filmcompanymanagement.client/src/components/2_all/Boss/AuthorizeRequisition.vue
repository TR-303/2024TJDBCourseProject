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

        <h2>申请批准</h2>

        <div class="header-container">
            <p> </p>
        </div>
        <div class="dataTable">
            <el-table :data="requisition" style="width: 1000">
                <el-table-column prop="id" label="申请编号" width="120"></el-table-column>
                <el-table-column prop="employee" label="申请人姓名" width="auto"></el-table-column>
                <el-table-column prop="type" label="申请类型" width="auto"></el-table-column>
                <el-table-column prop="billAmount" label="申请金额" width="auto"></el-table-column>
                <el-table-column prop="billDate" label="申请日期" width="auto"></el-table-column>
                <el-table-column prop="status" label="申请状态" width="auto"></el-table-column>
                <el-table-column fixed="right" label="操作" width="auto">
                    <template v-slot="scope">
                        <el-button type="text" size="small" @click="viewDetails(scope.row)">详情</el-button>
                        <el-button type="text" size="small" @click="Delete(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        
        <!--表单显示-->
         <el-dialog title="申请表详细信息" v-model="dialogVisible" width="60%" :before-close="handleClose">
            <el-form :model="form" label-width="120px">
                <el-form-item label="申请编号">
                    <el-input v-model="form.id" disabled></el-input>
                </el-form-item>
                <el-form-item label="申请类型">
                    <el-select v-model="form.type" placeholder="请选择申请类型" disabled>
                        <el-option label="维修申请" value="维修申请"></el-option>
                        <el-option label="购买申请" value="购买申请"></el-option>
                        <el-option label="报销申请" value="报销申请"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="申请人">
                    <el-input v-model="form.employee" disabled></el-input>
                </el-form-item>
                <el-form-item v-if="form.type === '维修申请'" label="设备编号">
                    <el-input v-model="form.equipmentId" disabled></el-input>
                </el-form-item>
                <el-form-item v-if="form.type !== '报销申请'" label="设备名称">
                    <el-input v-model="form.equipmentName" disabled></el-input>
                </el-form-item>
                <el-form-item v-if="form.type !== '报销申请'" label="设备型号">
                    <el-input v-model="form.equipmentModel" disabled></el-input>
                </el-form-item>
                <el-form-item label="账单ID">
                    <el-input v-model="form.billId" disabled></el-input>
                </el-form-item>
                <el-form-item label="金额">
                    <el-input-number v-model="form.billAmount" :controls="false" disabled></el-input-number>
                </el-form-item>
                <el-form-item label="账单类型">
                    <el-select v-model="form.billType" placeholder="请选择账单类型" disabled>
                        <el-option label="存款" value="存款"></el-option>
                        <el-option label="拨款" value="拨款"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="账单日期">
                    <el-date-picker v-model="form.billDate" type="date" disabled></el-date-picker>
                </el-form-item>
                <el-form-item label="账单状态">
                    <el-select v-model="form.billStatus" placeholder="请选择账单状态" disabled>
                        <el-option label="发起" value="发起"></el-option>
                        <el-option label="完成" value="完成"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="申请状态">
                    <el-select v-model="form.status" placeholder="请选择状态" >
                        <el-option label="待定" value="待定"></el-option>
                        <el-option label="通过" value="通过"></el-option>
                        <el-option label="拒绝" value="拒绝"></el-option>
                    </el-select>
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
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'AuthorizeRequisition',
        data() {
            return {
                name: '', // 获取登入姓名
                requisition: [], //默认申请数据
                dialogVisible: false,
                form: { id: '' },
                department:'',
            }
        },
        computed: {
            showBossMenu() {
                return this.department === '管理部';
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
                axios
                    .post("/api/data/userdata", { id: userId })
                    .then((result) => {
                        this.name = result.data.name || "未定义"; // 确保 name 有默认值
                        this.department = result.data.department.name;
                    })
                    .catch((error) => {
                        console.error("Error fetching mock data:", error);
                    });
            },
            getRequisition() {
                axios.get('/api/ApplicationApproval/requisition')
                    .then(response => {
                        this.requisition = response.data.requisition || [];
                    })
                    .catch(error => {
                        console.error('Error fetching requisition:', error);
                    });
            },
            //提交表单
            submitForm() {
                axios.post('/api/ApplicationApproval/submit-req-form', this.form)
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
                this.getRequisition();
            },
            //删除申请
            Delete(row) {
                axios.post('/api/ApplicationApproval/delete-form', row)
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
                this.getRequisition();
            },
            // 查看详情
            viewDetails(row) {
                // 根据申请类型发送请求
                this.form_type = row.type
                axios.post('/api/ApplicationApproval/details-req-form', { id: row.id, type: row.type }).then(response => {
                    this.form = response.data[0];
                    // 显示表单
                    this.dialogVisible = true;
                }).catch(error => {
                    console.error('获取维修申请表单数据失败', error);
                });
            },
        },
        mounted() {
            this.getdata();
            this.getRequisition();
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
        height: 10px;
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

    .container {
        display: flex;
        flex-direction: column;
        padding: 10px;
    }

</style>