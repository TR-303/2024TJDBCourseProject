import { createRouter, createWebHashHistory } from 'vue-router'
import Login from '@/components/Login/Login.vue'
import InfopageVue from '../components/2_all/Infopage.vue'

const router = createRouter(
    {
    history: createWebHashHistory(),
    routes: [
        {
            path: "/",
            component: Login
        },
        {
            path: "/Department/:id?",
            component: () => import("@/components/2_all/Department.vue")
        },
        {
            path: "/Infopage/:id?",
            component: InfopageVue  
        }
    ]
    }
)

export default router;