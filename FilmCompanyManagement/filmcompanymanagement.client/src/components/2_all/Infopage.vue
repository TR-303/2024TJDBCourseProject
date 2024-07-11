<template>
    <div>
        <div class="head">
            <div class="head_left">
                <label class="head_center" style="height:30px">
                    目录
                </label>
                <img class="head_logo" src="./logo.png"/>
                <label class="head_center">摄影公司管理系统</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="./User.jpg"/>
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
        <div class="window1">
            <p>1</p>
        </div>
        <div class="window2">
            <p>2</p>
        </div>
        <div class="window3">
            <p>3</p>
        </div>
    </div>
</template>
<script>
    export default{
        data(){
            return{
                name1: 'xiaoming',
                count:0,
                count2: 0,
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
                console.log(event.target.id);
/*                window.location.href = './Infopage.vue';
*/          
            }
        }
    }
</script>
<style>
    .head{
        display:flex;
        width:100%;
        padding:var(--base-size-16,var(--base-size-16));
        gap:var(--base-size-12,12px);
        background-color: aqua;
        border: 20px;
        padding:5px;
        box-sizing: border-box;
        color: black;
    }
    .head_left{
        flex:1 1 auto;
        display:flex;
        gap:var(--bar-size-8,9%);
    }
    .head_center {
        font-size:20px;
        text-align:center;
    }
    .head_right {
        float:right;
        display: flex;
    }
    .head_logo{
        width:30px;
        height:30px;
        border:0;
        outline-offset:2px;
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
        display: grid;
        grid-template-columns:2fr 1fr;
        /* 左右两部分 */
        grid-template-rows: 1fr 1fr;
        /* 两行 */
        grid-template-areas:
            "left-top right"
            "left-bottom right";
        grid-gap: 10px;
        height: 100vh;
        padding: 10px;
    }
    .li_node{
        display:flex;
        align-content:center;
        height:20px;
        font-size:20px;
        padding:10px 0 20px 10px;
    }
    .ul_menu{
        list-style-type: none;
        padding-block-start:0px;
        padding: 0;
        margin: 0;
    }
    .window1 {
        grid-area: left-top;
        margin: 5px;
        padding: 10px;
        text-align: center;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: space-between;
        border: 2px solid black;
        width: 600px;
        height: 400px;
        box-sizing: border-box;
    }

    .window2 {
        grid-area: left-bottom;
        margin: 5px;
        padding: 10px;
        text-align: center;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: space-between;
        border: 2px solid black;
        width: 600px;
        height: 225px;
        box-sizing: border-box;
    }

    .window3 {
        grid-area: right;
        margin: 5px;
        padding: 10px;
        text-align: center;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: space-between;
        border: 2px solid black;
        width: 300px;
        height: 645px;
        box-sizing: border-box;
    }
</style>