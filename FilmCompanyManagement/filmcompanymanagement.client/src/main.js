/* eslint-disable no-undef */
import { createApp } from 'vue'
import App from './App.vue'
import router from "./router"
//import './mockjs'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import axios from 'axios'
axios.defaults.baseURL = "https://localhost:7142";

const app = createApp(App)
app.use(router)
app.use(ElementPlus)
app.mount('#app')

