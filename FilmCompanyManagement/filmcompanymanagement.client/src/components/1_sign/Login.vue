<template>
    <div class="image-container">
        <div class="background-overlay"></div>
        <!-- 阻止默认事件 -->
        <div class="login-box">
            <h1>欢迎来到影视公司!</h1>
            <form @submit.prevent="login">
                <h2>
                    <label for="username">账号：</label>
                    <input type="text" class="input-field" v-model="username" required />
                </h2>
                <h2>
                    <label for="password">密码：</label>
                    <input type="password" class="input-field" v-model="password" required />
                </h2>
                <h2>
                    <label for="department">部门：</label>
                    <select class="select" v-model="department">
                        <option v-for="option in options" :key="option.value" :value="option.value">
                            {{ option.text }}
                        </option>
                    </select>
                </h2>
                <button type="submit">登陆</button>
            </form>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'
export default {
    data() {
        return {
            username: '',
            password: '',
            department:'',
            options: [
                { value: '管理部', text: '管理部' },
                { value: '财务部', text: '财务部' },
                { value: '业务部', text: '业务部' },
            ],
        };
    },
    methods: {
        async login() {
            try {
                const response = await axios.post('/api/auth/login', {
                    username: this.username,
                    password: this.password,
                });
                if (response.data.success) {
                    // 登录成功，根据用户类型跳转到相应页面
                    this.usertype = this.username[0];
                    if (this.usertype == 'B') {
                        // 跳转到管理员页面
                        this.$router.push('/2_all/Boss');
                    } else if (this.usertype == 'A') {
                        // 跳转到财务页面
                        this.$router.push('/2_all/Accounting');
                    } else if (this.usertype == 'W') {
                        // 跳转到业务页面
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
    },
};
</script>

<style scoped>
.image-container {
    position: relative;
    width: 100vw;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    overflow: hidden;
}

.background-overlay {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: url('picture1_1.png');
    background-size: cover;
    background-position: center;
    z-index: -1;
    /* 置于底层 */
}

.login-box {
    background: rgba(0, 0, 0, 0.7);
    padding: 20px;
    border-radius: 10px;
    text-align: center;
    width: 400px;
}

.select{
    width:100px;
    height:30px;
}

h1 {
    font-size: 35px;
    color: white;
    font-weight: bold;
    margin-bottom: 20px;
}

label {
    font-size: 20px;
    color: white;
    font-weight: bold;
    margin-bottom: 10px;
}

.input-field {
    font-size: 20px;
    padding: 5px;
    margin-bottom: 15px;
    width: 200px;
    border-radius: 5px;
    border: 1px solid #ccc;
}

button {
    padding: 10px 20px;
    background-color: #2196f3;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 18px;
    width: 100px;
}

button:hover {
    background-color: #1976d2;
    /* 按钮悬浮效果 */
}
</style>
