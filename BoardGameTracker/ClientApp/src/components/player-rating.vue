<template>
  <v-layout fill-height row wrap>
    <v-flex pa-3 xs4>
      <strong>{{playerRating.player.name}}:</strong>
    </v-flex>
    <v-flex xs8>
      <v-select :readonly="playerRating.player.id != currentPlayer.id" v-model="playerRating.rating" class="hidden-md-and-up" :items="[1, 2, 3, 4 , 5, 6, 7, 8, 9, 10]" label="Rating"></v-select>
      <v-rating
        class="hidden-sm-and-down"
        :color="playerRating.player.id === currentPlayer.id ? 'warning' : 'primary'"
        :readonly="playerRating.player.id != currentPlayer.id && !currentPlayer.isAdmin"
        :length="10"
        v-model="playerRating.rating"
      ></v-rating>
    </v-flex>
  </v-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from "../axios-service";
export default Vue.extend({
  props: {
    id: {
      type: Number,
    },
    playerRating: {}
  },
  methods: {
    async rate(rating: number) {
      (await httpClient.post(`games/${this.id}/rate`, {
        rating
      }))
    }
  },
  computed: {
    currentPlayer(): number {
      return this.$store.state.user;
    }

  },
  watch: {
    'playerRating.rating'() {
      this.rate((this.playerRating as any).rating)
    }
  }

})
</script>


<style>
</style>
