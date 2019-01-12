<template>
  <v-layout row wrap>
    <v-flex xs12>
      <h1 :style="{ color : player.colour}">Player Profile</h1>
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
            <v-card-actions v-if="user.id == id">
              <v-btn :to="{name : 'player-edit', params : {id : id}}" color="primary">
                <v-icon>edit</v-icon>
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
        <v-flex xs6 md3 lg2>
          <v-card class="text-xs-center">
            <v-card-text>
              <div class="display-1 mb-2">{{player.totalGamesPlayed}}</div>
              <span>Total Games Played</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs6 md3 lg2>
          <v-card class="text-xs-center">
            <v-card-text>
              <div class="display-1 mb-2">{{player.uniqueGamesPlayed}}</div>
              <span>Unique Games Played</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md3 lg2>
          <v-card class="text-xs-center">
            <v-card-text>
              <div class="display-1 mb-2">{{player.gamesWonPercentage}}%</div>
              <span>Game Win Rate</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs6 md3 lg2>
          <v-card class="text-xs-center">
            <v-card-text>
              <div class="display-1 mb-2">{{player.averageRating | round}}</div>
              <span>Average Rating</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs6 md12 lg4>
          <v-card class="text-xs-center">
            <v-card-text>
              <div class="display-1 mb-2">
                <span v-if="player.averageDifferenceToBGG <= 0" class="error--text">
                  <v-icon class="vertical" color="error">arrow_downward</v-icon>
                  {{player.averageDifferenceToBGG | round}}
                </span>
                <span v-else class="success--text">
                  <v-icon class="vertical" color="success">arrow_upward</v-icon>
                  {{player.averageDifferenceToBGG | round}}
                </span>
              </div>
              <span>Average Difference To BGG</span>
            </v-card-text>
          </v-card>
        </v-flex>

        <v-flex xs6 md4 lg3>
          <v-card class="text-xs-center">
            <v-card-text>
              <v-img v-if="player.mostPlayedGame" :src="player.mostPlayedGame.image" contain height="200px" aspect-ratio="1"></v-img>
              <div class="display-1 mb-2">
                <span v-if="player.mostPlayedGame">Plays : {{player.mostPlayedGame.plays}}</span>
                <span v-else>None</span>
              </div>
              <span>Most Played Game</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs6 md4 lg3>
          <v-card class="text-xs-center">
            <v-card-text>
              <v-img v-if="player.leastPlayedGame" :src="player.leastPlayedGame.image" contain height="200px" aspect-ratio="1"></v-img>
              <div class="display-1 mb-2">
                <span v-if="player.leastPlayedGame">Plays : {{player.leastPlayedGame.plays}}</span>
                <span v-else>None</span>
              </div>
              <span>Least Played Game</span>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md4 lg3>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Highest Rated Games</h3>
                <v-list>
                  <v-list-tile v-for="r in player.highestRatedGames" :key="r.title" avatar>
                    <v-list-tile-avatar tile>
                      <img :src="r.thumbnail">
                    </v-list-tile-avatar>
                    <v-list-tile-content>
                      <v-list-tile-title>{{r.name}}</v-list-tile-title>
                    </v-list-tile-content>
                    <v-list-tile-action>{{r.rating}}</v-list-tile-action>
                  </v-list-tile>
                </v-list>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md4 lg3>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">Lowest Rated Games</h3>
                <v-list>
                  <v-list-tile v-for="r in player.lowestRatedGames" :key="r.title" avatar>
                    <v-list-tile-avatar tile>
                      <img :src="r.thumbnail">
                    </v-list-tile-avatar>
                    <v-list-tile-content>
                      <v-list-tile-title>{{r.name}}</v-list-tile-title>
                    </v-list-tile-content>
                    <v-list-tile-action>{{r.rating}}</v-list-tile-action>
                  </v-list-tile>
                </v-list>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md3 lg12>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Rating Distribution</h4>
                <apexchart type="bar" :options="chartOptions" :series="ratingsDistribution"/>
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
import { Player } from '../models/Player'
export default Vue.extend({
  components: {
  },
  props: {
    id: {
      type: Number,
    },
  },
  data: () => ({
    player: {} as Player,
    error: '',
    chartOptions: {
      plotOptions: {
        bar: {
          horizontal: false,
        },
      },
      stroke: {
        show: true,
        colors: ['transparent']
      },

      xaxis: {
        categories: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
      },

    }

  }),
  async created() {
    try {
      this.player = (await httpClient.get(`users/${this.id}`)).data
    }
    catch (e) {
      this.error = e;
    }
  },
  computed: {
    user(): any {
      return this.$store.state.user;
    },
    ratingsDistribution(): any {
      if (this.player.ratingsDistribution == undefined) {
        return [{ name: 'ratings', data: [] }]
      }
      else {
        return [{
          name: 'Ratings',
          data: Object.values(this.player.ratingsDistribution)
        }]
      }
    }
  }
})
</script>


<style>
.vertical {
  vertical-align: middle;
}
</style>
