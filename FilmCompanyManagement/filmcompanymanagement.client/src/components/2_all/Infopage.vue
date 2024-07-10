<template>
    <div>
        <div class="head">
            <div class="head_left">
                <button style="height:30px" @click="count++">
                    目录
                </button>
                <img class="head_logo" src="./logo.png"/>
                <label class="head_center">摄影公司管理系统{{count}}</label>
            </div>
            <div class="head_right">
                <img class="head_logo" src="./User.jpg"/>
                <label class="head_center">你好,{{name1}}</label>
            </div>
        </div>
    </div>
    <div class="aside" v-if="count%2">
        <ul class="ul_menu">
            <li role="menuitem" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="count2++">
                首页{{count2}}
            </li>
            <li role="menuitem" tabindex="-1" class="li_node" aria-haspopup="true" @mouseout="removeShadow($event)" @mouseover="addShadow($event)" @click="count2++">
                部门情况{{count2}}
            </li>
        </ul>
    </div>
    <div class="main">
        <h3>main</h3>
    </div>
</template>
<script>
    export default{
        data(){
            return{
                name1:'xiaoming',
                count: 0,
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
            }
        }
    }
</script>
<style>
    .head{
        display:flex;
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
        gap:var(--bar-size-8,8px);
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
    .aside{
        display:inline-block;
        float:left;
        width:150px;
        height:100vh;
        background-color:lightskyblue;
        box-sizing:border-box;
    }
    .main{
        display:flex;
        height:100%;
        border:5px;
        color:black;
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
</style>