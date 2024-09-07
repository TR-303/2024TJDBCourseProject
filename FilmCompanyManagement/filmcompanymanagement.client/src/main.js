/* eslint-disable no-undef */
import { createApp } from 'vue'
import App from './App.vue'
import router from "./router"
//import './mockjs'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import axios from 'axios'
axios.defaults.baseURL = "http://120.55.92.50:5232";

const app = createApp(App)
app.use(router)
app.use(ElementPlus)
app.mount('#app')

