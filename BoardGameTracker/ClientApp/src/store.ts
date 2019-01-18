import { httpClient } from './axios-service';
import Vue from 'vue';
import Vuex from 'vuex';
import { User } from './models/User';
Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		user: {},
		unratedGamesCount: 0
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
		},
		setUnratedGamesCount(state, count) {
			state.unratedGamesCount = count;
		}
	},
	actions: {
		async getNotifications(context) {
			try {
				if (context.getters.loggedIn) {
					const unratedGamesCount = (await httpClient.get(`games/player-unrated-games`)).data;
					context.commit('setUnratedGamesCount', unratedGamesCount);
				}
			} catch (e) {}
		}
	},
	getters: {
		loggedIn(state: any): boolean {
			return state.user.id !== undefined;
		}
	}
});
