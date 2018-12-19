<template>
  <v-app id="inspire">
    <v-navigation-drawer
      v-model="drawer"
      fixed
      app
    >
      <v-list dense>
        <v-list-tile @click="">
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <router-link :to="{name : 'home'}">Search</router-link>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile @click="">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>
              <router-link :to="{name : 'games'}">Games</router-link>
            </v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile @click="">
          <v-list-tile-action>
            <v-icon>contact_mail</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>
              <router-link :to="{name : 'game-sessions'}">Game Sessions</router-link>
            </v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar
      color="primary"
      dark
      fixed
      app
    >
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>Board Games</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu offset-y="offset-y">
        <v-btn
          flat="flat"
          slot="activator"
          small="small"
        >{{userName}}
          <v-icon>keyboard_arrow_down</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile @click="">
            <v-icon class="mr-2">settings</v-icon>
            <v-list-tile-title>Settings</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="logout">
            <v-icon class="mr-2">exit_to_app</v-icon>
            <v-list-tile-title>Logout</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
      <v-avatar
        class="mr-2"
        size="36"
      >
        <img src="http://i.pravatar.cc/150?img=53" />
      </v-avatar>
    </v-toolbar>
    <v-content>
      <v-container
        fluid
        fill-height
      >
        <v-layout>
          <v-flex text-xs-center>
            <router-view />
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer
      color="secondary"
      app
    >
      <span class="white--text">&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  data: () => ({
    drawer: null
  }),
  props: {
    source: String
  },
  methods: {
    logout() {
      this.$store.commit("logout");
      this.$router.push({ name: "login" });
    }
  },
  computed: {
    userName(): string {
      return this.$store.state.user.name;
    }
  }
});
</script>