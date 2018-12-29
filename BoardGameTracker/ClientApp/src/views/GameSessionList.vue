<template>
  <div>
    <h1>Game Sessions</h1>
    <router-link :to="{name : 'game-session-create'}">
      <v-btn color="primary">New</v-btn>
    </router-link>
    <v-timeline dense>
      <v-timeline-item :key="gameSession.id" v-for="gameSession in gameSessions" small>
        <div class="py-4">
          <v-card>
            <v-img
              max-height="200px"
              v-if="gameSession.game.thumbnail"
              contain
              :src="gameSession.game.thumbnail"
              aspect-ratio="1"
            ></v-img>

            <v-card-title primary-title>
              <div>
                <div class="headline">{{gameSession.game.name}}</div>
                <h3 class="grey--text">{{gameSession.date | bigDate}}</h3>
              </div>
            </v-card-title>

            <v-card-actions>
              <v-subheader>Players : {{gameSession.players.length}}</v-subheader>
              <v-spacer></v-spacer>
              <v-btn @click="gameSession.show = !gameSession.show" icon>
                <v-icon>{{ gameSession.show ? 'keyboard_arrow_down' : 'keyboard_arrow_up' }}</v-icon>
              </v-btn>
            </v-card-actions>
            <v-slide-y-transition>
              <v-card-text v-show="gameSession.show">
                <li :key="player.id" v-for="player in gameSession.players">
                  {{player.player.name}}
                  <v-icon>star_border</v-icon>
                  <v-icon>star_border</v-icon>
                  <v-icon>star_border</v-icon>
                  <v-icon>star_border</v-icon>
                  <v-icon>star_border</v-icon>
                </li>
              </v-card-text>
            </v-slide-y-transition>
          </v-card>
        </div>
      </v-timeline-item>
    </v-timeline>
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
    error: "",
  }),
  filters: {
    bigDate: (value: string) => {
      return moment(value).format("Do MMM YYYY");
    },
  },
  created() {
    httpClient.get("game-session").then(response => {
      response.data.forEach((element: any) => {
        element.show = false;
      });
      this.gameSessions = response.data;
    });
  },
});
</script>


<style>
</style>
