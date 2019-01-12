<template>
  <div>
    <h1>Game Sessions</h1>
    <router-link :to="{name : 'game-session-create'}">
      <v-btn color="primary">New</v-btn>
    </router-link>
    <v-progress-circular v-if="isLoading" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-container grid-list-md>
      <v-layout row wrap>
        <v-flex xs12 :key="gameSessionGroup.id" v-for="gameSessionGroup in gameSessionGroups">
          <v-card>
            <v-card-title class="primary lighten-1">
              <h2 class="white--text font-weight-light">{{gameSessionGroup.key | date}}</h2>
            </v-card-title>
            <v-container>
              <v-layout row wrap>
                <v-flex class="pa-2" xs12 :key="gameSession.id" v-for="gameSession in gameSessionGroup.g">
                  <v-layout>
                    <v-flex xs2>
                      <v-img :src="gameSession.game.thumbnail" height="100px" contain></v-img>
                    </v-flex>
                    <v-flex xs9>
                      <v-card-title primary-title>
                        <div>
                          <div class="headline">
                            <v-btn flat small :to="{name : 'game-session-update', params : { id : gameSession.id}}" color="secondary">
                              <v-icon>edit</v-icon>
                            </v-btn>
                            <router-link :to="{name : 'game-detail', params : { id : gameSession.game.id}}">{{ gameSession.game.name }}</router-link>
                          </div>
                          <div>
                            <v-chip outline label :key="player.id" v-for="player in gameSession.players">
                              <v-avatar tile class="white--text" :color="player.colour">{{player.name.charAt(0).toUpperCase()}}</v-avatar>
                              <router-link :to="{name : 'player-profile', params : {id : player.id}}">{{player.name}}</router-link>
                              <v-icon v-if="player.isWinner" color="warning">star</v-icon>
                            </v-chip>
                          </div>
                        </div>
                      </v-card-title>
                    </v-flex>
                    <v-flex xs1></v-flex>
                  </v-layout>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "@/axios-service";
import moment from "moment";
export default Vue.extend({
  name: "GameSessionList",
  data: () => ({
    gameSessionGroups: [],
    error: "",
    isLoading: false
  }),
  filters: {
    bigDate: (value: string) => {
      return moment(value).format("Do MMM YYYY");
    }
  },
  created() {
    this.isLoading = true;
    httpClient.get("game-session").then(response => {
      response.data.forEach((element: any) => {
        element.show = false;
      });
      this.gameSessionGroups = response.data;
      this.isLoading = false;
    });
  }
});
</script>


<style>
</style>
