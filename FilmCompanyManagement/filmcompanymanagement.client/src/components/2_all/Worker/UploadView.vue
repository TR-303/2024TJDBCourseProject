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
                <button class="head_button" id="exit" @click="jump">
                    退出登录
                </button>
            </div>
        </div>
        <div class="aside">
            <ul class="ul_menu">
                <li role="menuitem" id="menu_0" tabindex="-1" class="li_node" @mouseover="addShadow" @mouseout="removeShadow" @click="jump">
                    首页
                </li>
                <li role="menuitem" id="menu_1" tabindex="-1" class="li_node" @mouseover="addShadow" @mouseout="removeShadow" @click="jump">
                    部门情况
                </li>
                <li v-if="showWorkerMenu" role="menuitem" id="menu_2" tabindex="-1" class="li_node" @mouseover="addShadow" @mouseout="removeShadow" @click="jump">
                    项目查看
                </li>
                <li v-if="showWorkerMenu" role="menuitem" id="menu_3" tabindex="-1" class="li_node" @mouseover="addShadow" @mouseout="removeShadow" @click="jump">
                    事务申请
                </li>
                <li v-if="showWorkerMenu" role="menuitem" id="menu_4" tabindex="-1" class="li_node" @mouseover="addShadow" @mouseout="removeShadow" @click="jump">
                    成果上传
                </li>
            </ul>
        </div>
        <div class="container">
            <!-- 查看列表 -->
            <div class="file-list">
                <h2>历史文件列表</h2>
                <table v-if="fileList.length > 0">
                    <thead>
                        <tr>
                            <th>文件ID</th>
                            <th>文件名</th>
                            <th>文件类型</th>
                            <th>文件说明</th>
                            <th>文件大小</th>
                            <th>上传日期</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="file in fileList" :key="file.id">
                            <td>{{ file.id }}</td>
                            <td>{{ file.name }}</td>
                            <td>{{ file.type }}</td>
                            <td>{{ file.description }}</td>
                            <td>{{ file.size }}</td>
                            <td>{{ file.uploadDate }}</td>
                        </tr>
                    </tbody>
                </table>
                <p v-else>没有文件</p>
            </div>

            <!-- 文件上传区域 -->
            <div class="upload-form">
                <h2>上传文件</h2>
                <button @click="triggerFileInput" class="custom-file-button">选择文件</button>
                <input type="file" ref="fileInput" @change="handleFileUpload" />
                <div>
                    <label for="fileName" class="form-label">文件名称</label>
                    <input type="text" v-model="form.fileName" id="fileName" class="form-input" />
                </div>
                <div>
                    <label for="fileType" class="form-label">文件类型</label>
                    <input type="text" v-model="form.fileType" id="fileType" class="form-input" />
                </div>
                <div>
                    <label for="fileSize" class="form-label">文件大小</label>
                    <input type="text" v-model="form.fileSize" id="fileSize" class="form-input" />
                </div>
                <div>
                    <label for="fileDescription" class="form-label">文件说明</label>
                    <textarea v-model="form.fileDescription" id="fileDescription" class="form-textarea" rows="3" cols="60"></textarea>
                </div>
                <button @click="uploadFile" class="submit-button">确定上传</button>
            </div>
        </div>
    </div>
</template>

<script>
    import { ref, onMounted, computed } from 'vue';
    import axios from 'axios';
    import { useRoute, useRouter } from 'vue-router';

    export default {
        name: 'UploadView',
        data() {
            return {
                name: '', // 获取登入姓名
            }
        },
        computed: {
            showWorkerMenu() {
                return this.$route.query.id === '3';
            }
        },
        setup() {
            const fileList = ref([]);
            const form = ref({
                fileName: '',
                fileType: '',
                fileSize: '',
                fileDescription: ''
            });
            const currentWorkerID = ref(null);
            const name = ref('');
            const route = useRoute();
            const router = useRouter();

            const userID = route.query.id;

            onMounted(async () => {
                try {

                    // 获取所有文件
                    const fileResponse = await axios.get('/api/worker/files');
                    console.log(fileResponse.data);
                    if (fileResponse.data) {
                        // 根据当前 workerID 过滤文件
                        fileList.value = fileResponse.data.filter(file => file.workerID.toString() === userID);
                        console.log("Filtered File List:", fileList.value); // 打印过滤后的文件列表
                        if (fileList.value.length === 0) {
                            alert("没有找到相关的文件");
                        }
                    } else {
                        alert("读取文件列表失败");
                    }

                    // 获取用户姓名
                    const userResponse = await axios.post('/api/data/userdata', { id: route.query.id });
                    if (userResponse.data.name) {
                        name.value = userResponse.data.name;
                    } else {
                        name.value = '未定义';
                    }
                } catch (error) {
                    console.error(error);
                    alert("数据获取错误");
                }
            });

            const handleFileUpload = (event) => {
                const file = event.target.files[0];
                if (file) {
                    form.value.fileName = file.name;
                    form.value.fileType = file.type;
                    form.value.fileSize = (file.size / 1024).toFixed(2) + ' KB';
                }
            };

            const uploadFile = async () => {
                // 检查所有字段是否已填写
                if (!form.value.fileName || !form.value.fileType || !form.value.fileSize || !form.value.fileDescription) {
                    alert("请完成所有内容的填写再提交！");
                    return;
                }

                try {
                    console.log('begin 1');
                    axios.post('/api/worker/upload', {
                        id: userID,
                        name: form.value.fileName,
                        type: form.value.fileType,
                        size: form.value.fileSize,
                        description: form.value.fileDescription
                    }).then(function (res) {
                        if (1) {
                            alert("提交成功")
                        } else {
                            alert("提交失败")
                        }
                    })
                } catch (error) {
                    console.error(error);
                    alert("提交失败");
                }
            };

            const addShadow = (event) => {
                event.target.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            };

            const removeShadow = (event) => {
                event.target.style.boxShadow = 'none';
            };

            const jump = (event) => {
                const id = route.query.id || '3'; // 默认 id 为 '3'
                switch (event.target.id) {
                    case 'menu_0':
                        router.push({ path: '/Infopage', query: { id } });
                        break;
                    case 'menu_1':
                        router.push({ path: '/Department', query: { id } });
                        break;
                    case 'menu_2':
                        router.push({ path: '/projects', query: { id } });
                        break;
                    case 'menu_3':
                        router.push({ path: '/application', query: { id } });
                        break;
                    case 'menu_4':
                        router.push({ path: '/upload', query: { id } });
                        break;
                    case 'exit':
                        router.push({ path: '/', query: { id } });
                        break;
                }
            };

            return { fileList, form, handleFileUpload, uploadFile, addShadow, removeShadow, jump, name };
        }
    };
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

    .container {
        display: flex;
        flex-direction: column;
        padding: 10px;
    }

    .sub-nav {
        margin-bottom: 20px;
    }

        .sub-nav a {
            margin-right: 10px;
            text-decoration: none;
            color: #007bff;
        }

            .sub-nav a:hover {
                text-decoration: underline;
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

    .file-list, .upload-form {
        margin-bottom: 20px;
    }

        .file-list table {
            width: 100%;
            border-collapse: collapse;
        }

        .file-list th, .file-list td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .file-list th {
            background-color: #f2f2f2;
        }

        .upload-form input[type="file"] {
            display: none;
        }

    .custom-file-button {
        background-color: deepskyblue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 18px;
        width: 100px;
        padding: 10px;
        margin: 10px 0;
    }

        .custom-file-button:hover {
            background-color: #1976d2;
        }

    .form-label {
        font-size: 18px;
        margin-bottom: 5px;
        display: block;
    }

    .form-input, .form-textarea {
        display: block;
        margin-bottom: 10px;
        width: calc(100% - 20px);
        margin-right: 10%;
        font-size: 18px;
    }

    .form-textarea {
        resize: vertical;
    }

    .submit-button {
        background-color: deepskyblue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 18px;
        width: 100px;
        padding: 10px;
        margin: 10px 0;
    }

        .submit-button:hover {
            background-color: #1976d2;
        }

</style>
