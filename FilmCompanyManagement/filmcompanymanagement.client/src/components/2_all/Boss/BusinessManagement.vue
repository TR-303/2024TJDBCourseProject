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

        <h2>业务管理</h2>

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
                 <el-table :data="businesses_list" style="width: 1000">
                    <el-table-column prop="id" label="投资编号" width="auto"></el-table-column>
                    <el-table-column prop="billDate" label="投资日期" width="auto"></el-table-column>
                    <el-table-column prop="customerName" label="投资方" width="auto"></el-table-column>
                    <el-table-column prop="billAmount" label="投资金额" width="auto"></el-table-column>
                    <el-table-column prop="billStatus" label="账单状态" width="auto"></el-table-column>
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
                    <el-form-item label="投资编号">
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="客户类型">
                        <el-select v-model="form.customerType" placeholder="请选择客户类型">
                            <el-option label="企业" value="企业"></el-option>
                            <el-option label="政府" value="政府"></el-option>
                            <el-option label="个人" value="个人"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="客户名称">
                        <el-input v-model="form.customerName"></el-input>
                    </el-form-item>
                    <el-form-item label="业务类型">
                        <el-select v-model="form.customerBusinessType" placeholder="请选择客户类型">
                            <el-option label="直接投资" value="直接投资"></el-option>
                            <el-option label="间接投资" value="间接投资"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="联系电话">
                        <el-input-number v-model="form.customerPhone" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="电子邮箱">
                        <el-input v-model="form.customerEmail"></el-input>
                    </el-form-item>
                    <el-form-item label="客户地址">
                        <el-input v-model="form.customerAddress"></el-input>
                    </el-form-item>
                    <el-form-item label="账单编号">
                        <el-input v-model="form.billId" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="金额">
                        <el-input-number v-model="form.billAmount" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="账单类型">
                        <el-select v-model="form.billType" placeholder="请选择部门">
                            <el-option label="存款" value="存款"></el-option>
                            <el-option label="拨款" value="拨款"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="账单日期">
                        <el-date-picker v-model="form.billDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="账单状态">
                        <el-select v-model="form.billStatus" placeholder="请选账单状态">
                            <el-option label="发起" value="发起"></el-option>
                            <el-option label="完成" value="完成"></el-option>
                        </el-select>
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
                 <el-table :data="businesses_list" style="width: 1000">
                    <el-table-column prop="id" label="购买编号" width="auto"></el-table-column>
                    <el-table-column prop="billDate" label="购买日期" width="auto"></el-table-column>
                    <el-table-column prop="customerName" label="购买人" width="auto"></el-table-column>
                    <el-table-column prop="billAmount" label="购买金额" width="auto"></el-table-column>
                    <el-table-column prop="status" label="订单状态" width="auto"></el-table-column>
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
                        <el-form-item label="订单编号">
                            <el-input v-model="form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="订单类型">
                            <el-select v-model="form.type" placeholder="请选择订单类型">
                                <el-option label="标准" value="标准"></el-option>
                                <el-option label="加急" value="加急"></el-option>
                                <el-option label="特殊" value="特殊"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="文件ID">
                            <el-input v-model="form.fileId" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="文件名">
                            <el-input v-model="form.fileName"></el-input>
                        </el-form-item>
                        <el-form-item label="文件类型">
                            <el-select v-model="form.fileType" placeholder="请选账单状态">
                                <el-option label="图片" value="图片"></el-option>
                                <el-option label="视频" value="视频"></el-option>
                                <el-option label="文档" value="文档"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="内容类型">
                            <el-select v-model="form.fileContentType" placeholder="请选账单状态">
                                <el-option label="项目报告" value="项目报告"></el-option>
                                <el-option label="成片" value="成片"></el-option>
                                <el-option label="项目文档" value="项目文档"></el-option>
                                <el-option label="会议记录" value="会议记录"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="文件大小">
                            <el-input-number v-model="form.fileSize"></el-input-number>
                        </el-form-item>
                        <el-form-item label="文件路径">
                            <el-input v-model="form.filePath"></el-input>
                        </el-form-item>
                        <el-form-item label="上传日期">
                            <el-date-picker v-model="form.fileUploadDate" type="date" placeholder="选择日期"></el-date-picker>
                        </el-form-item>
                        <el-form-item label="文件状态">
                            <el-select v-model="form.filestatus" placeholder="请选择文件状态">
                                <el-option label="已上传" value="已上传"></el-option>
                                <el-option label="未上传" value="未上传"></el-option>
                                <el-option label="上传失败" value="上传失败"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="客户类型">
                            <el-select v-model="form.customerType" placeholder="请选择客户类型">
                                <el-option label="企业" value="企业"></el-option>
                                <el-option label="政府" value="政府"></el-option>
                                <el-option label="个人" value="个人"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="客户名称">
                            <el-input v-model="form.customerName"></el-input>
                        </el-form-item>
                        <el-form-item label="业务类型">
                            <el-select v-model="form.customerBusinessType" placeholder="请选账单状态">
                                <el-option label="短期租赁" value="短期租赁"></el-option>
                                <el-option label="长期租赁" value="长期租赁"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="联系电话">
                            <el-input-number v-model="form.customerPhone" :controls="false"></el-input-number>
                        </el-form-item>
                        <el-form-item label="电子邮箱">
                            <el-input v-model="form.customerEmail"></el-input>
                        </el-form-item>
                        <el-form-item label="客户地址">
                            <el-input v-model="form.customerAddress"></el-input>
                        </el-form-item>
                        <el-form-item label="账单编号">
                            <el-input v-model="form.billId" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="金额">
                            <el-input-number v-model="form.billAmount" :controls="false"></el-input-number>
                        </el-form-item>
                        <el-form-item label="账单类型">
                            <el-select v-model="form.billType" placeholder="请选择部门">
                                <el-option label="存款" value="存款"></el-option>
                                <el-option label="拨款" value="拨款"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="账单日期">
                            <el-date-picker v-model="form.billDate" type="date" placeholder="选择日期"></el-date-picker>
                        </el-form-item>
                        <el-form-item label="账单状态">
                            <el-select v-model="form.billStatus" placeholder="请选账单状态">
                                <el-option label="发起" value="发起"></el-option>
                                <el-option label="完成" value="完成"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="申请的处理状态">
                            <el-select v-model="form.status" placeholder="请选择处理状态">
                                <el-option label="待发货" value="待发货"></el-option>
                                <el-option label="已发货" value="已发货"></el-option>
                                <el-option label="发货中" value="发货中"></el-option>
                            </el-select>
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
                 <el-table :data="businesses_list" style="width: 1000">
                    <el-table-column prop="id" label="租赁编号" width="auto"></el-table-column>
                    <el-table-column prop="billDate" label="租赁日期" width="auto"></el-table-column>
                    <el-table-column prop="customerName" label="租赁人" width="auto"></el-table-column>
                    <el-table-column prop="billAmount" label="租赁金额" width="auto"></el-table-column>
                    <el-table-column prop="status" label="租赁状态" width="auto"></el-table-column>
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
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="对接员工">
                        <el-select v-model="form.employee" placeholder="请选择员工" filterable clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.name" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.name }}</span>
                                    <span>{{ employee.id }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item> 
                    <el-form-item label="客户类型">
                        <el-select v-model="form.customerType" placeholder="请选择客户类型">
                            <el-option label="企业" value="企业"></el-option>
                            <el-option label="政府" value="政府"></el-option>
                            <el-option label="个人" value="个人"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="客户名称">
                        <el-input v-model="form.customerName"></el-input>
                    </el-form-item>
                    <el-form-item label="业务类型">
                        <el-select v-model="form.customerBusinessType" placeholder="请选账单状态">
                            <el-option label="短期租赁" value="短期租赁"></el-option>
                            <el-option label="长期租赁" value="长期租赁"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="联系电话">
                        <el-input-number v-model="form.customerPhone" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="电子邮箱">
                        <el-input v-model="form.customerEmail"></el-input>
                    </el-form-item>
                    <el-form-item label="客户地址">
                        <el-input v-model="form.customerAddress"></el-input>
                    </el-form-item>
                    <el-form-item label="账单编号">
                        <el-input v-model="form.billId" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="金额">
                        <el-input-number v-model="form.billAmount" :controls="false"></el-input-number>
                    </el-form-item>
                    <el-form-item label="账单类型">
                        <el-select v-model="form.billType" placeholder="请选择部门">
                            <el-option label="存款" value="存款"></el-option>
                            <el-option label="拨款" value="拨款"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="账单日期">
                        <el-date-picker v-model="form.billDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-form-item>
                    <el-form-item label="账单状态">
                        <el-select v-model="form.billStatus" placeholder="请选账单状态">
                            <el-option label="发起" value="发起"></el-option>
                            <el-option label="完成" value="完成"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="订单状态">
                        <el-select v-model="form.status" placeholder="请选择订单状态">
                            <el-option label="待确认" value="待确认"></el-option>
                            <el-option label="已确认" value="已确认"></el-option>
                            <el-option label="已发货" value="已发货"></el-option>
                        </el-select>
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
                <el-table :data="businesses_list" style="width: 1000">
                    <el-table-column prop="id" label="项目编号" width="auto"></el-table-column>
                    <el-table-column prop="billDate" label="项目日期" width="auto"></el-table-column>
                    <el-table-column prop="customerName" label="项目客户" width="auto"></el-table-column>
                    <el-table-column prop="billAmount" label="项目金额" width="auto"></el-table-column>
                    <el-table-column prop="manager" label="项目负责人" width="auto"></el-table-column>
                    <el-table-column prop="status" label="项目状态" width="auto"></el-table-column>
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
                    <el-form-item label="项目编号">
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item label="对接经理">
                        <el-select v-model="form.manager" placeholder="请选择员工" filterable clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.name" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.name }}</span>
                                    <span>{{ employee.id }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item> 
                    <el-collapse v-model="activeNames" @change="handleChange">
                        <el-collapse-item title="客户" name="1">
                            <el-form-item label="客户类型">
                                <el-select v-model="form.customerType" placeholder="请选择客户类型">
                                    <el-option label="企业" value="企业"></el-option>
                                    <el-option label="政府" value="政府"></el-option>
                                    <el-option label="个人" value="个人"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="客户名称">
                                <el-input v-model="form.customerName"></el-input>
                            </el-form-item>
                            <el-form-item label="业务类型">
                                <el-select v-model="form.customerBusinessType" placeholder="请选择客户类型">
                                    <el-option label="照片拍摄" value="照片拍摄"></el-option>
                                    <el-option label="视频制作" value="视频制作"></el-option>
                                    <el-option label="后期处理" value="后期处理"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="联系电话">
                                <el-input-number v-model="form.customerPhone" :controls="false"></el-input-number>
                            </el-form-item>
                            <el-form-item label="电子邮箱">
                                <el-input v-model="form.customerEmail"></el-input>
                            </el-form-item>
                            <el-form-item label="客户地址">
                                <el-input v-model="form.customerAddress"></el-input>
                            </el-form-item>
                        </el-collapse-item>
                        <el-collapse-item title="账单" name="2">
                            <el-form-item label="账单编号">
                                <el-input v-model="form.billId" disabled></el-input>
                            </el-form-item>
                            <el-form-item label="金额">
                                <el-input-number v-model="form.billAmount" :controls="false"></el-input-number>
                            </el-form-item>
                            <el-form-item label="账单类型">
                                <el-select v-model="form.billType" placeholder="请选择部门">
                                    <el-option label="存款" value="存款"></el-option>
                                    <el-option label="拨款" value="拨款"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="账单日期">
                                <el-date-picker v-model="form.billDate" type="date" placeholder="选择日期"></el-date-picker>
                            </el-form-item>
                            <el-form-item label="账单状态">
                                <el-select v-model="form.billStatus" placeholder="请选账单状态">
                                    <el-option label="发起" value="发起"></el-option>
                                    <el-option label="完成" value="完成"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-collapse-item>
                        <el-collapse-item title="员工" name="3">
                            <el-form-item label="项目员工">
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
                    <el-form-item label="绩效评定时间">
                        <el-date-picker v-model="form.kpiDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-form-item>
                    <div class="block">
                        <span class="demonstration" style="margin-left: 25px;">评定结果打分</span>
                         <el-rate v-model="form.result" :colors="['#99A9BF', '#F7BA2A', '#FF9900']" style="margin-left: 25px;"></el-rate>
                    </div>
                    <el-form-item label="评定者">
                        <el-select v-model="form.judger" placeholder="请选择员工" filterable clearable>
                            <el-option v-for="employee in overview_employee_list" :key="employee.id" :label="employee.name" :value="employee.id">
                                <span style="display: flex; justify-content: space-between; width: 100%;">
                                    <span>{{ employee.name }}</span>
                                    <span>{{ employee.id }}</span>
                                </span>
                            </el-option>
                        </el-select>
                    </el-form-item> 
                        <el-collapse-item title="文件" name="4">
                            <el-form-item label="文件名">
                                <el-input v-model="form.fileName"></el-input>
                            </el-form-item>
                            <el-form-item label="文件类型">
                                <el-select v-model="form.fileType" placeholder="请选账单状态">
                                    <el-option label="图片" value="图片"></el-option>
                                    <el-option label="视频" value="视频"></el-option>
                                    <el-option label="文档" value="文档"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="内容类型">
                                <el-select v-model="form.fileContentType" placeholder="请选账单状态">
                                    <el-option label="项目报告" value="项目报告"></el-option>
                                    <el-option label="成片" value="成片"></el-option>
                                    <el-option label="项目文档" value="项目文档"></el-option>
                                    <el-option label="会议记录" value="会议记录"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="文件大小">
                                <el-input-number v-model="form.fileSize"></el-input-number>
                            </el-form-item>
                            <el-form-item label="文件路径">
                                <el-input v-model="form.filePath"></el-input>
                            </el-form-item>
                            <el-form-item label="上传日期">
                                <el-date-picker v-model="form.fileUploadDate" type="date" placeholder="选择日期"></el-date-picker>
                            </el-form-item>
                            <el-form-item label="文件状态">
                                <el-select v-model="form.fileStatus" placeholder="请选择文件状态">
                                    <el-option label="已上传" value="已上传"></el-option>
                                    <el-option label="未上传" value="未上传"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-collapse-item>
                    </el-collapse>                   
                    <el-form-item label="项目状态">
                        <el-select v-model="form.status" placeholder="请选择订单状态">
                            <el-option label="进行中" value="进行中"></el-option>
                            <el-option label="已完成" value="已完成"></el-option>
                            <el-option label="已取消" value="已取消"></el-option>
                        </el-select>
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
                businesses_list:[],
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
            
            //表单用
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
            gitEmployNameList(){
                axios.get('/api/get-overview')
                    .then(response => {
                        this.overview_employee_list = response.data.employee_list || [];
                    })
                    .catch(error => {
                        console.error('Error fetching:', error);
                    });
            },
            getIncome(){
                let path;
                switch(this.businessID){
                    case '0':path='/api/get-invest';break;
                    case '1':path='/api/get-buy';break;
                    case '2':path='/api/get-lease';break;
                    case '3':path='/api/get-project';break;
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
                switch(this.businessID){
                    case '0':
                        if (!this.form.id || !this.form.customerid || !this.form.customerType || !this.form.customerName || 
                            !this.form.customerBusinessType || !this.form.customerPhone || !this.form.customerEmail || 
                            !this.form.customerAddress || !this.form.billId || !this.form.billAmount || 
                            !this.form.billType || !this.form.billDate || !this.form.billStatus) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/submit-invest-form';break;
                    case '1':
                        if (!this.form.id || !this.form.type || !this.form.fileId || !this.form.fileName || 
                            !this.form.fileType || !this.form.fileContentType || !this.form.fileSize || 
                            !this.form.filePath || !this.form.fileUploadDate || !this.form.filestatus || 
                            !this.form.customerid || !this.form.customerType || !this.form.customerName || 
                            !this.form.customerBusinessType || !this.form.customerPhone || !this.form.customerEmail || 
                            !this.form.customerAddress || !this.form.billId || !this.form.billAmount || 
                            !this.form.billType || !this.form.billDate || !this.form.billStatus || !this.form.status) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/submit-buy-form';break;
                    case '2':
                        if (!this.form.id || !this.form.employee || !this.form.customerid || !this.form.customerType || 
                            !this.form.customerName || !this.form.customerBusinessType || !this.form.customerPhone || 
                            !this.form.customerEmail || !this.form.customerAddress || !this.form.billId || 
                            !this.form.billAmount || !this.form.billType || !this.form.billDate || !this.form.billStatus || 
                            !this.form.status) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/submit-lease-form';break;
                    case '3':
                        if (!this.form.id || !this.form.manager || !this.form.fileId || !this.form.fileName || 
                            !this.form.fileType || !this.form.fileContentType || !this.form.fileSize || 
                            !this.form.filePath || !this.form.fileUploadDate || !this.form.fileStatus || 
                            !this.form.customerid || !this.form.customerType || !this.form.customerName || 
                            !this.form.customerBusinessType || !this.form.customerPhone || !this.form.customerEmail || 
                            !this.form.customerAddress || !this.form.billId || !this.form.billAmount || 
                            !this.form.billType || !this.form.billDate || !this.form.billStatus || 
                            !this.form.status || !this.form.employees[0].id) {
                            alert("请完成所有内容的填写再提交！");
                            return;
                        }
                        path='/api/submit-project-form';break;
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
                this.form = {id:'0', customerid: '0', billId:'0', billDate: '2024-09-08', fileId:'0', employees:[{id: '',name: ''}] };
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
                //重新请求数据
                getIncome();
            },
            // 查看详情
            viewDetails(row) {      
                let path;
                switch(this.businessID){
                    case '0':path='/api/details-invest';break;
                    case '1':path='/api/details-buy';break;
                    case '2':path='/api/details-lease';break;
                    case '3':path='/api/details-project';break;
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
            this.gitEmployNameList();
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

    .block {
      display: flex;
      align-items: center;
      justify-content: flex-start;
    }

    .block .demonstration {
      margin-right: 10px; /* 为标签和评分组件之间添加一些间距 */
    }

    .block .el-rate {
      margin-left: 20px; /* 向右移动 20px */
    }

</style>
