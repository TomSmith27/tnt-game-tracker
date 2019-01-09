<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" fixed app>
      <v-list dense>
        <v-list-tile :to="{name : 'home'}">
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>Search</v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{name : 'games'}">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Games</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{name : 'game-sessions'}">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Game Sessions</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{name : 'game-ratings'}">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>My Game Ratings</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{name : 'players'}">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Players</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar color="primary" dark fixed app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>Board Games</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu offset-y="offset-y" v-if="loggedIn">
        <v-btn flat="flat" slot="activator" small="small">
          {{user.name}}
          <v-icon>keyboard_arrow_down</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile :to="{name : 'player-profile', params : {id : user.id}}">
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
        <v-btn flat :to="{name : 'login'}">Login</v-btn>
        <v-btn class="hidden-sm-and-down" flat :to="{name : 'register'}">Register</v-btn>
      </div>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-layout>
          <v-flex text-xs-center>
            <router-view/>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer color="secondary" app>
      <span class="pl-4 white--text">&copy; 2019</span>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
export default Vue.extend({
  data: () => ({
    drawer: null,
  }),
  props: {
    source: String,
  },
  methods: {
    logout() {
      this.$store.commit('logout');
      this.$router.push({ name: 'login' });
    },
  },
  computed: {
    user(): any {
      return this.$store.state.user;
    },
    loggedIn(): boolean {
      return this.$store.getters.loggedIn;
    },
  },
});
</script>

<style>
@media only screen and (max-width: 959px) {
  #inspire .container {
    padding: 5px;
  }
}
</style>
