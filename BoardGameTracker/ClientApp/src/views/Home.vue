<template>
  <div>
    <v-text-field
      v-model="search"
      append-icon="search"
      solo
      single-line
      placeholder="Search..."
      v-on:keyup.enter="fastSearch"
    ></v-text-field>
    <v-progress-circular v-if="searching" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-alert :value="searchError" type="error">{{searchError}}</v-alert>
    <v-container grid-list-md text-xs-center>
      <v-layout row wrap>
        <v-flex v-for="game in boardGames" :key="game.objectid" xs6>
          <v-card>
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">{{game.name}}</h3>
              </div>
            </v-card-title>
            <v-card-text>
              <v-list-tile>
                <v-list-tile-action>
                  <v-icon>event</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                  <v-list-tile-title>{{ game.yearPublished }}</v-list-tile-title>
                  <v-list-tile-sub-title>Year Published</v-list-tile-sub-title>
                </v-list-tile-content>
              </v-list-tile>
            </v-card-text>
            <v-card-actions>
              <v-btn
                v-if="!game.imported"
                block
                color="primary"
                @click="importGame(game.objectId)"
              >Import</v-btn>
              <v-btn v-else block color="secondary" @click="importGame(game.objectId)">ReImport</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
import _ from "lodash";
import { httpClient } from "../axios-service";
import router from "../router";
export default Vue.extend({
  name: "home",
  components: {},
  data: () => ({
    search: "",
    searching: false,
    searchError: "",
    boardGames: []
  }),
  methods: {
    slowSearch: _.debounce(async function(this: any) {
      this.fastSearch();
    }, 2000),
    async fastSearch() {
      this.searching = true;
      try {
        const result = await httpClient.get(
          `games/search-import?search=${this.search}`
        );
        this.boardGames = result.data;
      } catch (e) {
        if (e.message === "Network Error") {
          this.searchError = "Network Error";
        }
      }
      this.searching = false;
    },
    importGame(objectid: number) {
      httpClient
        .post("games", {
          objectid
        })
        .then(response => {
          router.push("games");
        });
    }
  },
  watch: {
    async search() {
      if (this.search.length > 2) {
        this.slowSearch();
      }
    }
  }
});
</script>
