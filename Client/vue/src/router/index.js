import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import PostView from '../views/Post/PostView.vue'
import PostCreateView from '../views/Post/CRUD/PostCreateView.vue'
import PostUpdateView from '../views/Post/CRUD/PostUpdateView.vue'
import PostDeleteView from '../views/Post/CRUD/PostDeleteView.vue'


const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/index',
    name: 'index',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  },
  {
    path: '/post/:id',
    name: 'view post',
    component: PostView,
    props: true
  },
  {
    path: '/post/create',
    name: 'create post',
    component: PostCreateView
  },
  {
    path: '/post/edit/:id',
    name: 'edit post',
    component: PostUpdateView,
    props: true
  },
  {
    path: '/post/delete/:id',
    name: 'delete post',
    component: PostDeleteView,
    props: true
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
