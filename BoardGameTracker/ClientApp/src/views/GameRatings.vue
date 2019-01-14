<template>
  <div>
    <h1>Game Ratings</h1>
    <v-card>
      <v-layout pa-4 wrap>
        <v-flex md6>
          <v-checkbox label="Show Only Unrated" v-model="showOnlyUnrated"></v-checkbox>
        </v-flex>
        <v-flex md6>
          <v-text-field v-model="search" append-icon="search" label="Search" single-line hide-details></v-text-field>
        </v-flex>
      </v-layout>
      <v-data-table :loading="loading" :pagination.sync="pagination" :headers="headers" :items="ratingsFiltered" :search="search" :item-key="'game.id'">
        <template slot="items" slot-scope="props">
          <tr>
            <td>
              <v-avatar :tile="true" slot="activator" size="60px">
                <img v-if="props.item.game.thumbnail" :src="props.item.game.thumbnail" alt="Avatar">
              </v-avatar>
            </td>
            <td class="pa-0 text-xs-left">
              <router-link :to="{name : 'game-detail', params : { id : props.item.game.id}}">{{ props.item.game.name }}</router-link>
            </td>
            <td class="pa-0 text-xs-left">
              <player-rating-grid v-if="id === undefined" @rated="ratedSuccess(props.item, $event)" :game-id="props.item.game.id" :prop-rating="props.item.rating"></player-rating-grid>
            </td>
          </tr>
        </template>
        <v-alert slot="no-results" :value="true" color="error" icon="warning">Your search for "{{ search }}" found no results.</v-alert>
      </v-data-table>
    </v-card>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "@/axios-service";
import PlayerRatingGrid from '../components//player-rating-grid.vue';
import moment from "moment";
import { Rating } from '../models/Rating'
export default Vue.extend({
  name: "GameRatings",
  components: {
    PlayerRatingGrid
  },
  props: {
    id: {
      type: Number,
    },
  },
  data: () => ({
    ratings: [] as Rating[],
    search: '',
    loading: false,
    showOnlyUnrated: false,
    pagination: {
      sortBy: "game.name",
      descending: true,
      rowsPerPage: 25
    },
    headers: [
      { text: "", sortable: false },
      { text: "Name", value: "game.name" },
      { text: "Rating", value: "rating" }
    ],
    error: ""
  }),
  filters: {
    bigDate: (value: string) => {
      return moment(value).format("Do MMM YYYY");
    }
  },
  methods: {
    ratedSuccess(item: Rating, rating: number) {
      item.rating = rating;
    }
  },
  created() {
    this.loading = true;
    var route = "games/player-ratings";
    if (this.id) {
      route += `?userId=${this.id}`
    }
    httpClient.get(route).then(response => {
      this.ratings = response.data.ratings;
      this.loading = false;
    });
  },
  computed: {
    ratingsFiltered(): any[] {
      return this.ratings.filter(s => !this.showOnlyUnrated || s.rating == null)
    }
  },

});
</script>


<style>
</style>
