<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" fixed app>
      <v-list dense>
        <v-list-tile :to="{ name: 'dashboard' }">
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>Home</v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'home' }">
          <v-list-tile-action>
            <v-icon>search</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>Search</v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'games' }">
          <v-list-tile-action>
            <v-icon>gamepad</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Games</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'game-sessions' }">
          <v-list-tile-action>
            <v-icon>play_arrow</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Game Sessions</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'game-ratings' }">
          <v-list-tile-action>
            <v-icon>star</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>My Game Ratings</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'players' }">
          <v-list-tile-action>
            <v-icon>people</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Players</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'activity-log' }">
          <v-list-tile-action>
            <v-icon>trending_up</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Activity Log</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'wishlist' }">
          <v-list-tile-action>
            <v-icon>favorite</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Want to Play List</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{ name: 'recommendation' }">
          <v-list-tile-action>
            <v-icon>lightbulb</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Recommendation</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar color="primary" dark fixed app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>Board Games</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-select v-if="loggedIn" v-model="year" :items="[2019, 2020, 2021]" label="Year Filter" @input="setYearFilter"></v-select>
      <v-menu left offset-y="offset-y" v-if="loggedIn" class="pr-3">
        <v-badge slot="activator" color="red">
          <span v-if="unratedGamesCount > 0" slot="badge">{{ unratedGamesCount }}</span>
          <v-icon color="white">notifications</v-icon>
        </v-badge>
        <v-list>
          <v-list-tile>
            <v-list-tile-title v-if="unratedGamesCount > 0">
              You have {{ unratedGamesCount }} unrated games
              <router-link :to="{ name: 'game-ratings', query: { showOnlyUnrated: true } }">Click here to rate</router-link>
            </v-list-tile-title>
            <v-list-tile-title v-else>You have no unrated games</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>

      <v-menu offset-y="offset-y" v-if="loggedIn">
        <v-btn flat="flat" slot="activator" small="small">
          {{ user.name }}
          <v-icon>keyboard_arrow_down</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile :to="{ name: 'player-profile', params: { id: user.id } }">
            <v-icon class="mr-2">person</v-icon>
            <v-list-tile-title>Profile</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="logout">
            <v-icon class="mr-2">exit_to_app</v-icon>
            <v-list-tile-title>Logout</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
      <div v-else>
        <v-btn flat :to="{ name: 'login' }">Login</v-btn>
        <v-btn class="hidden-sm-and-down" flat :to="{ name: 'register' }">Register</v-btn>
      </div>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-layout>
          <v-flex text-xs-center>
            <router-view />
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer color="secondary" app>
      <span class="pl-4 white--text">&copy; {{ packageInfo.version }}</span>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import version from '../package.json';
import { httpClient } from './axios-service';
export default Vue.extend({
  data: () => ({
    drawer: null,
    year: 2020
  }),
  props: {
    source: String
  },
  methods: {
    logout() {
      this.$store.commit('logout');
      this.$router.push({ name: 'login' });
    },
    async setYearFilter() {
      await this.$store.dispatch('setYearFilter', this.year)
      this.$router.go(0)
    }
  },
  async created() {
    this.year = this.$store.state.user.yearFilter;
  },
  computed: {
    packageInfo() {
      return version;
    },
    unratedGamesCount(): number {
      return this.$store.state.unratedGamesCount;
    },
    user(): any {
      return this.$store.state.user;
    },
    loggedIn(): boolean {
      return this.$store.getters.loggedIn;
    }
  }
});
</script>

<style>
@media only screen and (max-width: 959px) {
  #inspire .container {
    padding: 5px;
  }
}
</style>
