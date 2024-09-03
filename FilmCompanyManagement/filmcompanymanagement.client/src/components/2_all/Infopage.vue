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
        </ul>
    </div>
    <div id="container" class="container">
        <div id="message_box" class="window1">
            <p style="font-size: 30px;text-align: center;">通知栏</p>
            <ul class="ul_message">
                <li class="li_message" style="font-size: 20px;" v-for="(message, index) in messages" :key="index">
                    {{ message }}
                </li>
            </ul>
        </div>
        <div class="window2">
            <div>
                <p style="font-size: 18px;text-align: center;margin-top:10px;">近期佳作展示</p>
            </div>
            <div class="image_container">
                <img class="image" src="@/assets/picture2_1.png" />
                <img class="image" src="@/assets/picture2_2.png" />
            </div>
        </div>
        <div class="window3">
            <p style="font-size: 25px;text-align: center;">考勤与绩效评定</p>
            <p style="font-size: 20px;text-align: center;">{{ currentDateTime }}</p>
            <button :class="buttonClass(isClicked_1)" @click="handleClick_1" :disabled="isClicked_1">
                {{ buttonText_1 }}
            </button>
            <p v-if="isClicked_1" style="text-align: center;">{{ tips_1 }}</p>
            <button v-if="isClicked_1" :class="buttonClass(0)" @click="handleClick_2" >
                {{ buttonText_2 }}
            </button>
            <p style="text-align: center;">{{ tips_2 }}</p>
            <ul class="bottom-text">
                <li>项目ID:{{ ProjectID }}</li>
                <li>评定时间:{{ Date }}</li>
                <li>评估者ID:{{ JudgerEmployeeID }}</li>
                <li>评定结果:{{ Result }}</li>
            </ul>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                name: '',
                id:'',
                count: 0,
                count2: 0,
                messages: ['待定', '待定', '待定', '待定', '待定'],
                maxNum: 5,  // 通知栏可存储的最大信息数量
                currentDateTime: '',
                isClicked_1: false,
                isClicked_2: false,
                tips_1: '',
                tips_2: '',
                buttonText_1: '签到',
                buttonText_2: '签退',
                ProjectID: '123',
                Date: '2024',
                JudgerEmployeeID: 'ABC',
                Result: '1',
            }
        },
        computed: {
            showWorkerMenu() {
                return this.$route.query.id === '3';
            }
        },
        watch: {
            '$route.query.id'(newId) {
                this.showWorkerMenu = newId === '3';
            }
        },
        methods: {
            async infomation() {
                try {
                    const response = await axios.post("./api/LoginController.cs", {
                        username: this.username,
                        password: this.password,
                        department: this.department,
                    });
                    console.log(response);
                    if (response.data.success) {
                        // 登录成功，根据用户类型跳转到相应页面
                        if (response.data == '1') {
                            // 跳转到管理员页面
                            if (this.department == '管理部')
                                this.$router.push('/2_all/Boss_');
                            // 跳转到财务页面
                            if (this.department == '财务部')
                                this.$router.push('/2_all/Accounting');
                            // 跳转到业务页面
                            if (this.department == '业务部')
                                this.$router.push('/2_all/Worker');
                        }
                    } else {
                        // 清空输入栏
                        this.username = '';
                        this.password = '';
                        alert('账号或密码有误，请重新输入！');
                    }
                } catch (error) {
                    if (error.response) {
                        alert('服务器错误，请稍后重试！');
                    } else {
                        alert('网络错误，请检查您的网络连接！');
                    }
                }
            },
            // 添加阴影效果
            addShadow(event) {
                event.target.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            },
            // 移除阴影效果
            removeShadow(event) {
                event.target.style.boxShadow = 'none';
            },
            addMessage() {
                // 采用队列结构，先进先出
                if (this.messages.length >= this.maxNum)
                    this.messages.shift();  // 移除queue第一项
                this.messages.push(this.getContent());
            },
            getContent() {
                // 返回值为一个字符串
            },
            updateDateTime() {
                const date = new Date();
                const year = date.getFullYear();
                const month = String(date.getMonth() + 1).padStart(2, '0');
                const day = String(date.getDate()).padStart(2, '0');
                const hours = String(date.getHours()).padStart(2, '0');
                const minutes = String(date.getMinutes()).padStart(2, '0');
                const seconds = String(date.getSeconds()).padStart(2, '0');
                this.currentDateTime = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
            },
            handleClick_1() {
                //这里先向服务器传递签到时间
                axios.post('/data/signdata',{
                    id: this.id,
                    time: this.currentDateTime,
                    state:'1',
                })
                    .then(result => {
                    if (result.data.success) {
                        alert("签到成功!");
                        console.log(result.data);
                        this.isClicked_1 = true;
                        this.buttonText_1 = "已签到";
                        this.tips_1 = `签到时间：${result.data.signtime}`;
                    }
                    else {
                        this.isClicked_1 = true;
                        this.buttonText_1 = "已签到";
                        this.tips_1 = `签到时间：${result.data.signtime}`;
                    }
                }).catch(error => {
                    console.error('Error fetching mock data:', error);
                });
            },
            handleClick_2() {
                axios.post('/data/signdata', {
                    id: this.id,
                    time: this.currentDateTime,
                    state: '0',
                })
                    .then(result => {
                        if (result.data.success) {
                            alert("签退成功!");
                            console.log(result.data);
                            this.isClicked_2 = true;
                            this.buttonText_2 = "已签退";
                            this.tips_2 = `签退时间：${result.data.signouttime}`;
                        }
                        else {
                            alert("已更改签退时间！");
                            this.isClicked_2 = true;
                            this.buttonText_2 = "已签退";
                            this.tips_2 = `签退时间：${result.data.signouttime}`;
                        }
                    }).catch(error => {
                        console.error('Error fetching mock data:', error);
                    });
/*                alert("签退成功!");
                this.isClicked_2 = true;
                this.buttonText_2 = "已签退";
                this.tips_2 = `签退时间：${this.currentDateTime}`;
                // 此时签到签退均完成，向管理员传送考勤信息*/
            },
            buttonClass(isClicked) {
                return isClicked ? 'clicked-button' : 'initial-button';
            },
            // 跳转页面
            jump(event) {
                console.log(event.target.id)
                if (event.target.id == 'menu_0')
                    this.$router.push({ path: '/Infopage', query: { id: this.id } });
                if (event.target.id == 'menu_1')
                    this.$router.push({ path: '/Department', query: { id: this.id } });
                if (event.target.id == 'menu_2') {
                    this.$router.push({ path: '/projects', query: { id: this.id } });
                }
                if (event.target.id == 'menu_3') {
                    this.$router.push({ path: '/application', query: { id: this.id } });
                }
                if (event.target.id == 'menu_4') {
                    this.$router.push({ path: '/upload', query: { id: this.id } });
                }
                if (event.target.id == 'exit') {
                    this.$router.push({ path: '/', query: { id: this.id } });
                }
            },
            //获取数据
            getdata() {
                axios.post('/data/userdata', { id: this.id }).then(result => {
                    this.name = result.data.name;// 将服务器返回的 name 更新到组件的 name 属性
                }).catch(error => {
                    console.error('Error fetching mock data:', error);
                });
            },
            //获取id
            getid() {
                this.id = this.$route.query.id;
            },
            getissign() {
                axios.post('/data/checksign', { id: this.id }).then(result => {
                    console.log(result);
                    if (result.data[0].issignout == '1') { 
                        console.log(result);
                        this.handleClick_2 = true;
                        this.buttonText_2 = "已签退";
                        this.tips_2 = `签退时间：${result.data[0].signouttime}`;
                    }
                    this.handleClick_1();
                }).catch(error => {
                    console.error('Error fetching mock data:', error);
                });
            }
        },
        mounted() {
            this.updateDateTime();
            this.getid();
            this.getdata();
            this.getissign();
            setInterval(this.updateDateTime, 1000);  // 每秒更新一次
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

    .container {
        display: grid;
        grid-template-columns: 2fr 1fr;
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

    .window1 {
        grid-area: left-top;
        margin: 5px;
        padding: 10px;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        justify-content: flex-start;
        border: 2px solid black;
        width: 65vw;
        height: 62vh;
        box-sizing: border-box;
    }

    .window2 {
        grid-area: left-bottom;
        flex-direction: column;
        margin: 5px;
        padding: 10px;
        text-align: center;
        border-radius: 5px;
        display: flex;
        justify-content: center;
        border: 2px solid black;
        width: 65vw;
        height: 33vh;
        box-sizing: border-box;
        overflow: hidden;
        position: relative;
    }

    .window3 {
        grid-area: right;
        margin: 5px;
        padding: 10px;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        justify-content: flex-start;
        border: 2px solid black;
        width: 36vh;
        height: 98vh;
        box-sizing: border-box;
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

    .ul_message {
        padding: 10px;
    }

    .li_message {
        margin: 10px;
    }

    .image_container {
        display: flex;
        justify-content:center;
        width: 100%;
        height: 26vh;
    }

    .image {
        display: inline-block;
        margin: 5px;
        padding:10px;
        width: 45%;
        height: auto;
    }

    .initial-button {
        margin: 5px;
        padding: 10px 20px;
        background-color: #2196f3;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 14px;
        display: inline-block;
    }

    .clicked-button {
        margin: 5px;
        padding: 10px 20px;
        background-color: gray;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 14px;
        display: inline-block;
    }

    .bottom-text {
        /* 将内容推至容器底部 */
        margin-top: auto;
        font-size: 18px;
        padding: 15px;
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