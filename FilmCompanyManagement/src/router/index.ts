import { createRouter, createWebHashHistory } from 'vue-router'
import Login from '@/components/Login/Login.vue'
import InfopageVue from '../components/2_all/Infopage.vue'
import ProjectView from '../components/2_all/Worker/ProjectView.vue'
import ApplicationView from '../components/2_all/Worker/ApplicationView.vue'
import UploadView from '../components/2_all/Worker/UploadView.vue'


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
            path: "/Infopage",
            component: InfopageVue  
        },

        //worker
        {
            path: '/projects',
            name: 'projects',
            component: ProjectView
        },
        {
            path: '/application/:id?',
            name: 'application',
            component: ApplicationView,
            children: [
                {
                    path: 'expense/:id?',
                    component: () => import('../components/2_all/Worker/ExpenseForm.vue')
                },
                {
                    path: 'purchase/:id?',
                    component: () => import('../components/2_all/Worker/PurchaseForm.vue')
                },
                {
                    path: 'repair/:id?',
                    component: () => import('../components/2_all/Worker/RepairForm.vue')
                }
            ]
        },
        {
            path: '/upload',
            name: 'upload',
            component: UploadView
        }
    ]
    }
)

export default router;