<template>
  <div>
    <h1>Game Sessions</h1>
    <router-link :to="{name : 'game-session-create'}">
      <v-btn color="primary">New</v-btn>
    </router-link>
    <v-timeline dense>
      <v-timeline-item :key="gameSession.id" v-for="gameSession in gameSessions" color="primary lighten-1" fill-dot left small>
        <v-card>
          <v-card-title class="primary lighten-1">
            <h2 class="white--text font-weight-light">{{gameSession.key | date}}</h2>
          </v-card-title>
          <v-container>
            <v-layout row wrap>
              <v-flex xs12>
                <div class="pa-2 text-xs-left" :key="game.id" v-for="game in gameSession.g">
                  <img :src="game.game.thumbnail" style="width : 50px">
                  <router-link :to="{name : 'game-detail', params : { id : game.game.id}}">{{ game.game.name }}</router-link>
                  <div class="pl-2">
                    <strong>Players</strong>
                  </div>
                  <div class="pl-4" :key="player.player.id" v-for="player in game.game.playerRatings">{{player.player.name}}</div>
                </div>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
    </v-timeline>
    <!--    <v-timeline dense fill-dot left small>
      <v-timeline-item :key="gameSession.id" v-for="gameSession in gameSessions" small>
        <div>
          <h2>{{gameSession.key}}</h2>
          <div :key="game.id" v-for="game in gameSession.g">{{game.game.name}}</div>
        </div>
      </v-timeline-item>
      <v-card>
        <v-card-title class="amber lighten-1 justify-end">
          <h2 class="display-1 mr-3 white--text font-weight-light">Title 2</h2>
          <v-icon dark size="42">mdi-home-outline</v-icon>
        </v-card-title>
        <v-container>
          <v-layout>
            <v-flex xs8>Lorem ipsum dolor sit amet, no nam oblique veritus. Commune scaevola imperdiet nec ut, sed euismod convenire principes at. Est et nobis iisque percipit.</v-flex>
            <v-flex xs4>Lorem ipsum dolor sit amet, no nam oblique veritus.</v-flex>
          </v-layout>
        </v-container>
      </v-card>
    </v-timeline>-->
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "@/axios-service";
import moment from "moment";
export default Vue.extend({
  name: "GameSessionList",
  data: () => ({
    gameSessions: [],
    error: ""
  }),
  filters: {
    bigDate: (value: string) => {
      return moment(value).format("Do MMM YYYY");
    }
  },
  created() {
    httpClient.get("game-session").then(response => {
      response.data.forEach((element: any) => {
        element.show = false;
      });
      this.gameSessions = response.data;
      this.gameSessions.reverse();
    });
  }
});
</script>


<style>
</style>
