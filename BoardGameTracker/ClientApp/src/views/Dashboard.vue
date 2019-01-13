<template>
  <div>
    <h1>Dashboard</h1>
    <v-container grid-list-md>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <!--  {{players}} -->
      <v-layout row wrap>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <h3 class="headline mb-0">Win Rate</h3>
            </v-card-title>
            <v-card-text>
              <v-list>
                <v-list-tile @click v-for="player in winLeaderBoard" :key="player.id" avatar>
                  <v-list-tile-avatar class="white--text" tile :color="player.colour">{{player.name.charAt(0).toUpperCase()}}</v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title :to="{name : 'player-profile', params : {id : player.id}}">{{player.name}}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>{{player.gamesWonPercentage | round}}%</v-list-tile-action>
                </v-list-tile>
              </v-list>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <h3 class="headline mb-0">Highest Rated Games</h3>
            </v-card-title>
            <v-card-text>
              <v-list>
                <v-list-tile @click v-for="game in highestRatedGames" :key="game.id" avatar>
                  <v-list-tile-avatar tile>
                    <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar">
                  </v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title>{{game.name}}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>{{game.playersAverageRating | round}}</v-list-tile-action>
                </v-list-tile>
              </v-list>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <h3 class="headline mb-0">Lowest Rated Games</h3>
            </v-card-title>
            <v-card-text>
              <v-list>
                <v-list-tile @click v-for="game in lowestRatedGames" :key="game.id" avatar>
                  <v-list-tile-avatar tile>
                    <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar">
                  </v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title>{{game.name}}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>{{game.playersAverageRating | round}}</v-list-tile-action>
                </v-list-tile>
              </v-list>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <h3 class="headline mb-0">Most Overated Games</h3>
            </v-card-title>
            <v-card-text>
              <v-list>
                <v-list-tile @click v-for="game in mostOverratedGames" :key="game.id" avatar>
                  <v-list-tile-avatar tile>
                    <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar">
                  </v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title>{{game.name}}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>
                    <span v-if="game.difference <= 0" class="error--text">
                      <v-icon class="vertical" color="error">arrow_downward</v-icon>
                      {{game.difference | round}}
                    </span>
                    <span v-else class="success--text">
                      <v-icon class="vertical" color="success">arrow_upward</v-icon>
                      {{game.difference | round}}
                    </span>
                  </v-list-tile-action>
                </v-list-tile>
              </v-list>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 md4>
          <v-card>
            <v-card-title primary-title>
              <h3 class="headline mb-0">Most Underated Games</h3>
            </v-card-title>
            <v-card-text>
              <v-list>
                <v-list-tile @click v-for="game in mostUnderatedGames" :key="game.id" avatar>
                  <v-list-tile-avatar tile>
                    <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar">
                  </v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title>{{game.name}}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>
                    <span v-if="game.difference <= 0" class="error--text">
                      <v-icon class="vertical" color="error">arrow_downward</v-icon>
                      {{game.difference | round}}
                    </span>
                    <span v-else class="success--text">
                      <v-icon class="vertical" color="success">arrow_upward</v-icon>
                      {{game.difference | round}}
                    </span>
                  </v-list-tile-action>
                </v-list-tile>
              </v-list>
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from "../axios-service";
import { Player } from '../models/Player'
import { GameListItem } from '../models/GameListItem'
export default Vue.extend({
  data: () => ({
    players: [] as Player[],
    games: [] as GameListItem[],
    error: ''
  }),
  async created() {
    try {
      this.players = (await httpClient.get(`dashboard`)).data
      this.games = (await httpClient.get(`games`)).data

    }
    catch (e) {
      this.error = e;
    }
  },
  computed: {
    winLeaderBoard(): Player[] {
      return this.players.sort((p, p2) => p2.gamesWonPercentage - p.gamesWonPercentage)
    },
    highestRatedGames(): GameListItem[] {
      return this.games.filter(g => g.playersAverageRating != null).sort((p, p2) => p2.playersAverageRating - p.playersAverageRating).slice(0, 3)
    },
    lowestRatedGames(): GameListItem[] {
      return this.games.filter(g => g.playersAverageRating != null).sort((p, p2) => p.playersAverageRating - p2.playersAverageRating).slice(0, 3)
    },
    mostOverratedGames(): GameListItem[] {
      return this.games.filter(g => g.difference != null).sort((p, p2) => p2.difference - p.difference).slice(0, 3)
    },
    mostUnderatedGames(): GameListItem[] {
      return this.games.filter(g => g.difference != null).sort((p, p2) => p.difference - p2.difference).slice(0, 3)
    }
  }
})
</script>


<style>
</style>
