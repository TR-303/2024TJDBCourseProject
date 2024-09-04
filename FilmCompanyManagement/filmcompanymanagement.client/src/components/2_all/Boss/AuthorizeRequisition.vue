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

        <div class="container">
            <h1>项目查看</h1>
            <table>
                <thead>
                    <tr>
                        <th>申请人</th>
                        <th>申请类型</th>
                        <th>申请状态</th>
                        <th>意见</th>
                        <th>详情</th>
                    </tr>
                </thead>
                <tbody v-show="requisition.length > 0">
                    <tr v-for="requisitionData in requisition" :key="requisitionData.id">
                        <td>{{ requisitionData.name }}</td>
                        <td>{{ requisitionData.type }}</td>
                        <td>{{ requisitionData.status }}</td>
                        <td>{{ requisitionData.ideas }}</td>
                        <td><button @click="viewDetails(requisitionData.id)">详情</button></td>
                    </tr>
                </tbody>
            </table>
            <p v-if="requisition.length <= 0" style="text-align: center;">没有申请数据</p>
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
                requisition:[], //默认申请数据
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
            getRequisition() {
                axios.get('/api/requisition')
                    .then(response => {
                        this.requisition = response.data.requisition || [];
                    })
                    .catch(error => {
                        console.error('Error fetching requisition:', error);
                    });
            },
            // 查看详情的方法
            viewDetails(id) {
                alert('查看详情:');
                alert(id);

                //请求信息
                getDetailsRequisition(id);
                //弹出对话框                 //这边建议使用element来实现，会简单许多
                //.....
                //可选 保存、删除、取消三种退出方式，最后一种不change
                //再次拉取信息一览
                getRequisition();
            },
            //查看申请的详细信息
            getDetailsRequisition(id) {
                axios.post('/api/detailsRequisition', { id })
                    .then(result => {
                        this.requisition = response.data.requisition || [];
                    }).catch(error => {
                        console.error('Error fetching requisition:', error);

                        if (1) {
                            alert("查询成功")
                        } else {
                            alert("查询失败")
                        }
                    });
            },
            //修改申请信息
            changeRequisition(id) {
                axios.post('/api/changeRequisition', { id })
                    .then(result => {
                        this.requisition = response.data.requisition || [];
                    }).catch(error => {
                        console.error('Error fetching requisition:', error);
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
