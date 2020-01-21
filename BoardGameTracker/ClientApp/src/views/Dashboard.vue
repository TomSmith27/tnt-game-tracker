<template>
  <div>
    <h1>Dashboard</h1>
    <v-progress-circular v-if="loading" :size="50" color="primary" indeterminate></v-progress-circular>

    <v-container v-else grid-list-md>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <!--  {{players}} -->
      <v-layout row wrap>
        <v-flex xs12 md4>
          <dashboard-card :title="'Win Rate'">
            <v-list-tile v-for="player in winLeaderBoard" :key="player.id" avatar>
              <v-list-tile-avatar class="white--text" tile :color="player.colour">{{player.name.charAt(0).toUpperCase()}}</v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>
                  <router-link :to="{name : 'player-profile', params : {id : player.id}}">{{player.name}}</router-link>
                </v-list-tile-title>
              </v-list-tile-content>
              <v-list-tile-action>{{player.gamesWonPercentage | round}}%</v-list-tile-action>
            </v-list-tile>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Average Rating'">
            <v-list-tile v-for="player in averageRating" :key="player.id" avatar>
              <v-list-tile-avatar class="white--text" tile :color="player.colour">{{player.name.charAt(0).toUpperCase()}}</v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>
                  <router-link :to="{name : 'player-profile', params : {id : player.id}}">{{player.name}}</router-link>
                </v-list-tile-title>
              </v-list-tile-content>
              <v-list-tile-action>{{player.averageRating | round}}</v-list-tile-action>
            </v-list-tile>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Highest Rated Games'">
            <game-dashboard-item v-for="game in highestRatedGames" :key="game.id" avatar :game="game">{{game.playersAverageRating | round}}</game-dashboard-item>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Lowest Rated Games'">
            <game-dashboard-item v-for="game in lowestRatedGames" :key="game.id" avatar :game="game">{{game.playersAverageRating | round}}</game-dashboard-item>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Most Overated Games'">
            <game-dashboard-item v-for="game in mostOverratedGames" :key="game.id" avatar :game="game">
              <span v-if="game.difference <= 0" class="error--text">
                <v-icon class="vertical" color="error">arrow_downward</v-icon>
                {{game.difference | round}}
              </span>
              <span v-else class="success--text">
                <v-icon class="vertical" color="success">arrow_upward</v-icon>
                {{game.difference | round}}
              </span>
            </game-dashboard-item>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Most Underated Games'">
            <game-dashboard-item v-for="game in mostUnderatedGames" :key="game.id" avatar :game="game">
              <span v-if="game.difference <= 0" class="error--text">
                <v-icon class="vertical" color="error">arrow_downward</v-icon>
                {{game.difference | round}}
              </span>
              <span v-else class="success--text">
                <v-icon class="vertical" color="success">arrow_upward</v-icon>
                {{game.difference | round}}
              </span>
            </game-dashboard-item>
          </dashboard-card>
        </v-flex>
        <v-flex xs12 md4>
          <dashboard-card :title="'Most Played Games'">
            <game-dashboard-item v-for="game in mostPlayedGames" :key="game.id" avatar :game="game">{{game.timesPlayed }}</game-dashboard-item>
          </dashboard-card>
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
import GameDashboardItem from '../components/game-dashboard-item.vue';
import DashboardCard from '../components/dashboard-card.vue';
export default Vue.extend({
  components: {
    GameDashboardItem,
    DashboardCard
  },
  data: () => ({
    players: [] as Player[],
    games: [] as GameListItem[],
    error: ''
  }),
  async created() {
    try {
      this.players = (await httpClient.get(`dashboard`)).data
      this.games = (await httpClient.get(`games`)).data
      this.games = this.games.filter(
        g =>
          g.lastPlayed != undefined &&
          new Date(g.lastPlayed.toString()).getFullYear() == 2020
      );
    }
    catch (e) {
      this.error = e;
    }
  },
  computed: {
    winLeaderBoard(): Player[] {
      return this.players.map(p => p).sort((p, p2) => p2.gamesWonPercentage - p.gamesWonPercentage)
    },
    averageRating(): Player[] {
      return this.players.map(p => p).sort((p, p2) => p2.averageRating - p.averageRating)
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
    },
    mostPlayedGames(): GameListItem[] {
      return this.games.sort((p, p2) => p2.timesPlayed - p.timesPlayed).slice(0, 3)
    }
  }
})
</script>


<style>
.v-list__tile__title a {
  text-decoration: none;
  color: inherit;
}
</style>
