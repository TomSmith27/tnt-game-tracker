import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import LoginPage from './views/Login.vue';
import RegisterPage from './views/Register.vue';
import GameList from './views/GameList.vue';
import GameSessionCreate from './views/GameSessionCreate.vue';
import GameSessionList from './views/GameSessionList.vue';
import store from './store';
import { User } from './models/User';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      name: 'login',
      path: '/login',
      component: LoginPage
    },
    {
      name: 'register',
      path: '/register',
      component: RegisterPage
    },
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/games',
      name: 'games',
      component: GameList
    },
    {
      path: '/game-session/create/:gameId?',
      name: 'game-session-create',
      component: GameSessionCreate,
      props: true
    },
    {
      path: '/game-session',
      name: 'game-sessions',
      component: GameSessionList
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    }
  ]
});

export default router;

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/register'];
  const authRequired = !publicPages.includes(to.path);
  if ((store.state.user as User).token === undefined) {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      var user = JSON.parse(userJson);
      store.commit('setUser', user);
    }
  }
  const loggedIn = (store.state.user as User).token != undefined;
  if (authRequired && !loggedIn) {
    return next({
      path: '/login',
      query: { redirect: to.fullPath }
    });
  }

  next();
});
