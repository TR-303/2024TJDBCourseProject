import { createRouter, createWebHashHistory } from 'vue-router'
import Login from '@/components/Login/Login.vue'
import InfopageVue from '../components/2_all/Infopage.vue'

import ProjectView from '../components/2_all/Worker/ProjectView.vue'
import ApplicationView from '../components/2_all/Worker/ApplicationView.vue'
import UploadView from '../components/2_all/Worker/UploadView.vue'
import AuthorizeRequisition from '../components/2_all/Boss/AuthorizeRequisition.vue'
import BusinessManagement from '../components/2_all/Boss/BusinessManagement.vue'
import PersonnelManagement from '../components/2_all/Boss/PersonnelManagement.vue'


import FinancialStatements from '@/components/2_all/Finance/FinancialStatements.vue';


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

        //boss
        {
            path: '/PersonnelM/:id?',
            name: 'PersonnelM',
            component: PersonnelManagement,
        },
        {
            path: '/AuthorizeR/:id?',
            name: 'AuthorizeR',
            component: AuthorizeRequisition,
        },
        {
            path: '/BusinessM/:id?',
            name: 'BusinessM',
            component: BusinessManagement,
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
        },
        {
            path: "/FinancialStatements/:id?",
            name: "FinancialStatements",
            component: FinancialStatements
        },

    ]
    }
)

export default router;