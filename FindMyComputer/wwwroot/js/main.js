// 1. Define route components.
// These can be imported from other files
import Home from './components/Home.js'
import ComputerRecommendation from './components/ComputerRecommendation.js'
const About = { template: '<div><router-link to="/">Start Over</router-link></div>' }


// 2. Define some routes
// Each route should map to a component.
// We'll talk about nested routes later.
const routes = [
    { path: '/', component: Home },
    { path: '/about', component: About },
    { path: '/recommend', component: ComputerRecommendation,
        props: route => ({ query: route.query.q })
    },
]

const router = VueRouter.createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: VueRouter.createWebHashHistory(),
    routes: routes,
})

const app = Vue.createApp({});
app.use(router)
app.mount('#app')
