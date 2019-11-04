import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Dashboard from './views/Dashboard.vue';
import LoginPage from './views/Login.vue';
import RegisterPage from './views/Register.vue';
import GameList from './views/GameList.vue';
import GameDetail from './views/GameDetail.vue';
import GameSessionCreate from './views/GameSessionCreate.vue';
import GameSessionUpdate from './views/GameSessionUpdate.vue';
import GameSessionList from './views/GameSessionList.vue';
import GameRatings from './views/GameRatings.vue';
import PlayerList from './views/Players.vue';
import PlayerProfile from './views/PlayerProfile.vue';
import PlayerEdit from './views/PlayerEdit.vue';
import ActivityLog from './views/ActivityLog.vue';
import WishList from './views/Wishlist.vue'
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
			path: '/search',
			name: 'home',
			component: Home
		},
		{
			path: '/games',
			name: 'games',
			component: GameList
		},
		{
			path: '/',
			name: 'dashboard',
			component: Dashboard
		},
		{
			path: '/game/detail/:id',
			name: 'game-detail',
			component: GameDetail,
			props: true
		},
		{
			path: '/game-session/create/:gameId?',
			name: 'game-session-create',
			component: GameSessionCreate,
			props: true
		},
		{
			path: '/game-session/edit/:id',
			name: 'game-session-update',
			component: GameSessionUpdate,
			props: true
		},
		{
			path: '/game-session',
			name: 'game-sessions',
			component: GameSessionList
		},
		{
			path: '/game-ratings/:id?',
			name: 'game-ratings',
			component: GameRatings,
			props: true
		},
		{
			path: '/players',
			name: 'players',
			component: PlayerList
		},
		{
			path: '/player/:id',
			name: 'player-profile',
			component: PlayerProfile,
			props: true
		},
		{
			path: '/player/edit/:id',
			name: 'player-edit',
			component: PlayerEdit,
			props: true
		},
		{
			path: '/wishlist',
			name: 'wishlist',
			component: WishList,
			props: true
		},
		{
			path: '/activity-log',
			name: 'activity-log',
			component: ActivityLog,
			props: true
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
			const user = JSON.parse(userJson);
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

	store.dispatch('getNotifications');

	next();
});
