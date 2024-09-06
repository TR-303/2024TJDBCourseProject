<template>
    <div>
        <div class="head">
            <div class="head_left">
                <img class="head_logo" src="@/assets/logo.png" />
                <label class="head_center">摄影公司管理系统</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="@/assets/User.jpg" />
                <!--                这里获取登入姓名-->
                <label class="head_center">你好,{{name}} </label>
                <button class="head_button" id="exit" @click="jump($event)">
                    退出登录
                </button>
            </div>
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

            <li v-if="showWorkerMenu" role="menuitem" id="menu_2" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                项目查看
            </li>
            <li v-if="showWorkerMenu" role="menuitem" id="menu_3" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                事务申请
            </li>
            <li v-if="showWorkerMenu" role="menuitem" id="menu_4" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                成果上传
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
            <li v-if="showFinanceMenu" role="menuitem" id="menu_8" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="jump($event)">
                查看财政表
            </li>
        </ul>
    </div>
    <div class="container">
        <div class="container_head">
            <label class="container_head_left">选择部门</label>
            <select class="container_head_mid" v-model="choose_department">
                <option v-for="option in options" :key="option.value" :value="option.value">
                    {{ option.text }}
                </option>
            </select>
            <button class="container_head_right" @click="getdatalist()">
                确定
            </button>
        </div>
        <div>
            <el-table :data="userdataList" style="width: 100%">
                <el-table-column prop="department" label="部门" />
                <el-table-column prop='id' label='员工id' />
                <el-table-column prop='phone' label='员工电话' />
            </el-table>
        </div>
    </div>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                name: '',
                id: '2',
                choose_department: '',
                options: [
                    { value: '管理部', text: '管理部' },
                    { value: '财务部', text: '财务部' },
                    { value: '业务部', text: '业务部' },
                ],
                userdataList: [],
            }
        },
        computed: {
            showBossMenu() {
                return this.$route.query.id === '1';
            },
            showFinanceMenu() {
                return this.$route.query.id === '2';
            },
            showWorkerMenu() {
                return this.$route.query.id === '3';
            }
        },
        watch: {
            '$route.query.id'(newId) {
                this.showBossMenu = newId === '1';
                this.showFinanceMenu = newId === '2';
                this.showWorkerMenu = newId === '3';
            }
        },
        methods: {
            // 添加阴影效果
            addShadow(event) {
                event.target.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            },
            // 移除阴影效果
            removeShadow(event) {
                event.target.style.boxShadow = 'none';
            },
            // 跳转页面
            jump(event) {
                console.log(event.target.id)
                if (event.target.id == 'menu_0') {
                    console.log(this.id);
                    this.$router.push({ path: '/Infopage', query: { id: this.id } });
                }
                if (event.target.id == 'menu_1') {
                    console.log(this.id);
                    this.$router.push({ path: '/Department', query: { id: this.id } });
                }
                if (event.target.id == 'menu_2') {
                    console.log(this.id);
                    this.$router.push({ path: '/projects', query: { id: this.id } });
                }
                if (event.target.id == 'menu_3') {
                    console.log(this.id);
                    this.$router.push({ path: '/application', query: { id: this.id } });
                }
                if (event.target.id == 'menu_4') {
                    console.log(this.id);
                    this.$router.push({ path: '/upload', query: { id: this.id } });
                }
                if (event.target.id == 'menu_5') {
                    this.$router.push({ path: '/PersonnelM', query: { id: this.id } });
                }
                if (event.target.id == 'menu_6') {
                    this.$router.push({ path: '/AuthorizeR', query: { id: this.id } });
                }
                if (event.target.id == 'menu_7') {
                    this.$router.push({ path: '/BusinessM', query: { id: this.id } });
                }
                if (event.target.id == 'menu_8') {
                    this.$router.push({ path: '/FinancialStatements', query: { id: this.id } });
                }
                if (event.target.id == 'exit') {
                    console.log(this.id);
                    this.$router.push({ path: '/', query: { id: this.id } });
                }
            },
            //获取数据
            getdata() {
                axios.post('/api/data/userdata', { id: this.id }).then(result => {
                    this.name = result.data.name;// 将服务器返回的 name 更新到组件的 name 属性
                }).catch(error => {
                    console.error('Error fetching mock data:', error);
                });
            },
            //获取id
            getid() {
                this.id = this.$route.query.id;
            },
            getdatalist() {
                axios.post('/api/data/departmentuserdata', { department: this.choose_department }).then(result => {
                    console.log(result.data);
                    this.userdataList = result.data;
                    console.log(this.userdataList);
                }).catch(error => {
                    console.error('Error fetching mock data:', error);
                });
            }
        },
        mounted() {
            this.getid();
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

    .container_head {
        flex: 1 1 auto;
        gap: var(--base-size-12,10px);
        display: flex;
    }

    .container_head_left {
        height: 30px;
        font-size: 20px;
        width: 100px;
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
        border-radius: 5px;
        cursor: pointer;
        font-size: 13px;
        width: 70px;
        text-align: center;
        height: 30px;
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
</style>