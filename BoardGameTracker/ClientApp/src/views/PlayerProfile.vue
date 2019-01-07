<template>
  <v-layout row wrap>
    <v-flex xs12>
      <h1>Player Profile</h1>
    </v-flex>
    <v-alert :value="error" type="error">{{error}}</v-alert>
    <v-container grid-list-md text-xs-center>
      <v-layout row wrap>
        <v-flex xs12>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">{{player.name}}</h3>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md6>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Total Games Played</h4>
                <div class="headline">{{player.totalGamesPlayed}}</div>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md6>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Unique Games Played</h4>
                <div class="headline">{{player.uniqueGamesPlayed}}</div>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs6 md3>
          <v-card>
            <v-img v-if="player.mostPlayedGame" :src="player.mostPlayedGame.image" contain height="200px" aspect-ratio="1"></v-img>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Most Played Game</h3>
                <span v-if="player.mostPlayedGame">Plays : {{player.mostPlayedGame.plays}}</span>
                <span v-else>None</span>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs6 md3>
          <v-card>
            <v-img v-if="player.leastPlayedGame" :src="player.leastPlayedGame.image" contain height="200px" aspect-ratio="1"></v-img>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Least Played Game</h3>
                <span v-if="player.leastPlayedGame">Plays : {{player.leastPlayedGame.plays}}</span>
                <span v-else>None</span>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs6 md3>
          <v-card>
            <v-img v-if="player.highestRatedGame" :src="player.highestRatedGame.image" contain height="200px" aspect-ratio="1"></v-img>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Hightest Rated Game</h3>
                <span v-if="player.highestRatedGame">Rating : {{player.highestRatedGame.rating}}</span>
                <span v-else>None</span>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs6 md3>
          <v-card>
            <v-img v-if="player.lowestRatedGame" :src="player.lowestRatedGame.image" contain height="200px" aspect-ratio="1"></v-img>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Lowest Rated Game</h3>
                <span v-if="player.lowestRatedGame">Rating : {{player.lowestRatedGame.rating}}</span>
                <span v-else>None</span>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from '../axios-service'
export default Vue.extend({
  props: {
    id: {
      type: Number,
    },
  },
  data: () => ({
    player: {},
    error: ''
  }),
  async created() {
    try {
      this.player = (await httpClient.get(`users/${this.id}`)).data
    }
    catch (e) {
      this.error = e;
    }
  }
})
</script>


<style>
</style>
