<template>
  <div>
    <h1>Imported Games</h1>
    <v-container grid-list-md text-xs-center>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <v-layout row wrap>
        <v-flex :key="game.objectId" v-for="game in games" xs12 md6 lg4>
          <v-card>
            <v-container fluid grid-list-lg>
              <v-layout row>
                <v-flex xs5>
                  <v-img v-if="game.thumbnail" contain :src="game.thumbnail" aspect-ratio="1"></v-img>
                </v-flex>
                <v-flex xs7>
                  <div>
                    <div class="headline">{{game.name}}</div>
                    <div>
                      <span>
                        <v-icon>people</v-icon>
                        {{game.minPlayers}} - {{game.maxPlayers}}
                      </span>
                    </div>
                    <div>
                      <span>
                        <v-icon>alarm</v-icon>
                        {{game.minPlaytime}}" - {{game.maxPlaytime}}"
                      </span>
                    </div>
                  </div>
                </v-flex>
              </v-layout>
            </v-container>
            <v-card-actions>
              <v-btn small absolute top right fab @click="deleteGame(game.id)" color="error">
                <v-icon>delete</v-icon>
              </v-btn>
              <v-btn
                :to="{name : 'game-session-create', params : {gameId : game.id}}"
                color="primary"
              >Create Session</v-btn>
              <v-btn
                :href="`https://boardgamegeek.com/boardgame/${game.objectId}`"
                color="secondary"
              >BGG Link</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "../axios-service";
export default Vue.extend({
  name: "GameList",
  data: () => ({
    games: null,
    error: ""
  }),
  created() {
    this.getGames();
  },
  methods: {
    async getGames() {
      try {
        this.games = (await httpClient.get("games")).data;
      } catch (error) {
        this.error = error;
      }
    },
    deleteGame(id: number) {
      httpClient.delete(`games/${id}`).then(response => {
        this.getGames();
      });
    }
  }
});
</script>


<style>
</style>
