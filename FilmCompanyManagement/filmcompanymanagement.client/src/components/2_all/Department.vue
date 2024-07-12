<template>
    <div>
        <div class="head">
            <div class="head_left">
                <label class="head_center" style="height:30px">
                    目录
                </label>
                <img class="head_logo" src="./logo.png" />
                <label class="head_center">摄影公司管理系统</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="./User.jpg" />
                <!--                这里获取登入姓名-->
                <label class="head_center">你好,{{name1}}</label>
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
        </ul>
    </div>
    <div class="container">
        <div class="container_head">
            <label class="container_head_left">选择部门</label>
            <select class="container_head_mid" v-model="choose">
                <option v-for="option in options" :key="option.value" :value="option.value">
                    {{ option.text }}
                </option>
            </select>
            <button class="container_head_right">
                确定
            </button>
        </div>
        <div>
            <p>{{choose}}</p>
        </div>
    </div>
</template>
<script>
    export default{
        data(){
            return{
                name1: 'xiaoming',
                choose: '',
                options: [
                    { value: '管理部', text: '管理部' },
                    { value: '财务部', text: '财务部' },
                    { value: '业务部', text: '业务部' },
                ],
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
                if (event.target.id == 'menu_0')
                    window.location.href = "./Infopage.vue";
                if (event.target.id == 'menu_1')
                    window.location.href='/Department.vue';
            }
        }
    }
</script>
<style>
    .head {
        display: flex;
        width: 100%;
        padding: var(--base-size-16,var(--base-size-16));
        gap: var(--base-size-12,12px);
        background-color: aqua;
        border: 20px;
        padding: 5px;
        box-sizing: border-box;
        color: black;
    }
    .container_head {
        flex:1 1 auto;
        gap:var(--base-size-12,10px);
        display: flex;
    }
    .container_head_left{
        height:30px;
        font-size:20px;
        width:100px;
        display:flex;
        text-align:center;
    }
    .container_head_mid {
        flex: auto;
        width:80%;
        display: flex;
        text-align: left;
        height: 30px;
    }
    .container_head_right {
        width:50px;
        display: flex;
        text-align: center;
        height: 30px;
    }
    .head_left {
        flex: 1 1 auto;
        display: flex;
        gap: var(--bar-size-8,9%);
    }
    .head_center {
        font-size: 20px;
        text-align: center;
    }

    .head_right {
        float: right;
        display: flex;
    }

    .head_logo {
        width: 30px;
        height: 30px;
        border: 0;
        outline-offset: 2px;
    }

    .aside {
        grid-area: sidebar;
        justify-content: center;
        align-items: center;
        border: 1px solid #ccc;
        float: left;
        width: 150px;
        height: 100vh;
        background-color: lightskyblue;
        box-sizing: border-box;
    }

    .container {
        display:flex;
        flex-direction:column;
        padding: 10px;
    }

    .li_node {
        display: flex;
        align-content: center;
        height: 20px;
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
</style>