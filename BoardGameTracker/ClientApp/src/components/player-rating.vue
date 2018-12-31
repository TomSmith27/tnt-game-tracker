<template>
  <v-layout fill-height row wrap>
    <v-flex pa-3 xs4>
      <strong>{{playerRating.player.name}}:</strong>
    </v-flex>
    <v-flex xs8>
      <v-select :readonly="playerRating.player.id != currentPlayerId" v-model="playerRating.rating" class="hidden-md-and-up" :items="[1, 2, 3, 4 , 5, 6, 7, 8, 9, 10]" label="Rating"></v-select>
      <v-rating class="hidden-sm-and-down" :large="playerRating.player.id === currentPlayerId" :readonly="playerRating.player.id != currentPlayerId" :length="10" v-model="playerRating.rating"></v-rating>
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
    currentPlayerId(): number {
      return this.$store.state.user.id;
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
