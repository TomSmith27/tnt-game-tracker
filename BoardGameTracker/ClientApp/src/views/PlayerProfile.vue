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
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Total Games Played</h4>
                <div class="headline">{{player.totalGamesPlayed}}</div>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Unique Games Played</h4>
                <div class="headline">{{player.uniqueGamesPlayed}}</div>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Average Rating</h4>
                <div class="headline">{{player.averageRating | round}}</div>
              </div>
            </v-card-title>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">Average Difference To BGG</h4>
                <div class="headline">
                  <v-btn flat v-if="player.averageDifferenceToBGG <= 0" color="red">
                    <v-icon left>arrow_downward</v-icon>
                    {{player.averageDifferenceToBGG | round}}
                  </v-btn>
                  <v-btn flat v-else color="green">
                    <v-icon left>add</v-icon>
                    {{player.averageDifferenceToBGG | round}}
                  </v-btn>
                </div>
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
        <v-flex xs12 md3>
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
        <v-flex xs12 md3>
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
        <v-flex xs12 md3>
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
</style>
