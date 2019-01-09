<template>
  <div>
    <h1>Games</h1>
    <v-container grid-list-md text-xs-center>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <v-card>
        <v-layout pa-4 wrap>
          <v-flex xs6>
            <v-combobox v-model="timeFilter" :items="timeFilters" :label="dateLabel"></v-combobox>
          </v-flex>
          <v-flex xs6>
            <v-text-field v-model="search" append-icon="search" label="Search" single-line hide-details></v-text-field>
          </v-flex>
        </v-layout>
        <v-data-table :loading="loading" :pagination.sync="pagination" :headers="headers" :items="games" :search="search">
          <template slot="items" slot-scope="props">
            <tr @click="props.expanded = !props.expanded">
              <td>
                <v-avatar :tile="true" slot="activator" size="60px">
                  <img v-if="props.item.thumbnail" :src="props.item.thumbnail" alt="Avatar">
                  <v-icon v-else color="danger" v-text="test"></v-icon>
                </v-avatar>
              </td>
              <td class="pa-0 text-xs-left">
                <router-link :to="{name : 'game-detail', params : { id : props.item.id}}">{{ props.item.name }}</router-link>
              </td>
              <td class="pa-0 text-xs-left">{{ props.item.timesPlayed }}</td>
              <td class="pa-0 text-xs-left" v-if="props.item.lastPlayed">{{ props.item.lastPlayed | date }}</td>
              <td class="pa-0 text-xs-left" v-else>Never</td>
              <td class="pa-0 text-xs-left">{{ props.item.averageRating | round }}</td>
              <td class="pa-0 text-xs-left">{{ props.item.playersAverageRating |round }}</td>
              <td class="pa-0 text-xs-left">
                <span v-if="props.item.difference == null"></span>
                <v-btn flat v-else-if="props.item.difference < 0" color="red">
                  <v-icon left>arrow_downward</v-icon>
                  {{props.item.difference | round}}
                </v-btn>
                <v-btn flat v-else color="green">
                  <v-icon left>add</v-icon>
                  {{props.item.difference | round}}
                </v-btn>
              </td>
            </tr>
          </template>
          <template slot="expand" slot-scope="props">
            <v-card flat>
              <v-card-actions>
                <v-btn block :to="{name : 'game-session-create', params : {gameId : props.item.id}}" color="primary">
                  <v-icon>play_arrow</v-icon>
                </v-btn>
                <v-btn block :href="`https://boardgamegeek.com/boardgame/${props.item.objectId}`">BGG Link
                  <v-icon>link</v-icon>
                </v-btn>
                <v-btn block v-if="props.item.lastPlayed == null" color="error">Delete
                  <v-icon right @click="deleteGame(props.item.id)">delete</v-icon>
                </v-btn>
              </v-card-actions>
            </v-card>
          </template>
          <v-alert slot="no-results" :value="true" color="error" icon="warning">Your search for "{{ search }}" found no results.</v-alert>
        </v-data-table>
      </v-card>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "../axios-service";
import moment from "moment";
export default Vue.extend({
  name: "GameList",
  data: () => ({
    games: [],
    error: "",
    search: "",
    loading: false,
    pagination: {
      sortBy: "lastPlayed",
      descending: true,
      rowsPerPage: 25
    },
    timeFilter: {} as any,
    headers: [
      { text: "", sortable: false },
      { text: "Name", value: "name" },
      { text: "Times Played", value: "timesPlayed" },
      { text: "Last Played", value: "lastPlayed" },
      { text: "BGG Rating", value: "averageRating" },
      { text: "Player Rating", value: 'playersAverageRating' },
      { text: "Difference", value: "Difference" }
    ]
  }),
  created() {
    this.timeFilter = this.timeFilters[0];
  },
  computed: {
    timeFilters() {
      return [
        { text: "All Time", value: null },
        { text: "Last Month", value: moment(new Date()).subtract(1, "M") },
        { text: "Last 6 Months", value: moment(new Date()).subtract(6, "M") },
        { text: "Last Year", value: moment(new Date()).subtract(1, "y") }
      ];
    },
    dateLabel(): string {
      if (this.timeFilter.value) {
        return `From : ${(this.$options as any).filters.date(this.timeFilter.value)}`
      }
      else { return '' }
    }
  },
  methods: {
    async getGames() {
      this.loading = true;
      try {
        if (this.timeFilter.value != null) {
          this.games = (await httpClient.get(
            `games?from=${this.timeFilter.value.toISOString()}`
          )).data;
        } else {
          this.games = (await httpClient.get(`games`)).data;
        }
      } catch (error) {

      }
      this.loading = false;
    },
    deleteGame(id: number) {
      httpClient.delete(`games/${id}`).then(response => {
        this.getGames();
      });
    },

  },
  watch: {
    timeFilter() {
      this.getGames();
    }
  }
});
</script>


<style>
</style>
