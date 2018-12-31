import { httpClient } from './axios-service';
import Vue from 'vue';
import Vuex from 'vuex';
import { User } from './models/User';
Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		user: {}
	},
	mutations: {
		setUser(state, user: User) {
			state.user = user;
			localStorage.setItem('user', JSON.stringify(user));
			httpClient.defaults.headers.Authorization = `Bearer ${(state.user as any).token}`;
		},
		logout(state) {
			state.user = {};
			localStorage.removeItem('user');
		}
	},
	actions: {},
	getters: {
		loggedIn(state: any) {
			return state.user.id !== undefined;
		}
	}
});
