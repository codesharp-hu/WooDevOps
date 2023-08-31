import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';


const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/:projectId/runs',
        name: 'Runs',
        component: () => import('../views/RunsView.vue')
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;