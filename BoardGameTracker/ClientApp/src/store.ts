import { httpClient } from './axios-service';
import Vue from 'vue';
import Vuex from 'vuex';
import { User } from './models/User';
Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		user: {},
		unratedGamesCount: 0,
		friends: [],
		followers: []
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
		},
		setFriends(state, { friends, followers }) {
			state.friends = friends;
			state.followers = followers;
		}
	},
	actions: {
		async setYearFilter(context, year: number) {
			await httpClient.post(`users/year-filter/${year}`, null);
			let user = context.state.user as any;
			user.yearFilter = year;
			context.commit('setUser', user);
		},
		async getNotifications(context) {
			try {
				if (context.getters.loggedIn) {
					const unratedGamesCount = (await httpClient.get(`games/player-unrated-games`)).data;
					context.commit('setUnratedGamesCount', unratedGamesCount);
				}
			} catch (e) {}
		},
		async addFriend(context, friend) {
			await httpClient.post(`users/add-friend/${friend.id}`);
			context.commit('setFriends', { friends: [ friend, ...context.state.friends ] });
		}
	},
	getters: {
		loggedIn(state: any): boolean {
			return state.user.id !== undefined;
		},
		friendIds(state): Number[] {
			return state.friends.map((f: any) => f.id);
		}
	}
});
