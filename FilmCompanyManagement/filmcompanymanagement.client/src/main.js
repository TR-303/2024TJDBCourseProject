/* eslint-disable no-undef */
import { createApp } from 'vue'
import App from './App.vue'
import router from "./router"
import './mockjs'
const app = createApp(App)
app.use(router)
app.mount('#app')

