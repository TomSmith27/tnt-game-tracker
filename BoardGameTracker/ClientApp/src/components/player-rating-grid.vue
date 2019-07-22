<template>
  <span>
    <v-select v-model="rating" @change="rate" class="hidden-md-and-up" :items="[1, 2, 3, 4 , 5, 6, 7, 8, 9, 10]" label="Rating"></v-select>
    <v-rating class="hidden-sm-and-down" :length="10" v-model="rating"></v-rating>
  </span>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from "../axios-service";
export default Vue.extend({
  props: {
    gameId: {
      type: Number,
    },
    propRating: {
      type: Number
    }
  },
  data: () => ({
    rating: 0
  }),
  methods: {
    async rate(value: number) {
      (await httpClient.post(`games/${this.gameId}/rate`, {
        rating: value
      }))
      this.$emit('rated', this.rating)
    }
  },
  computed: {
    currentPlayerId(): number {
      return this.$store.state.user.id;
    }
  },
  watch: {
    propRating: {
      handler() {
        this.rating = this.propRating;
      },
      immediate: true
    },

  }
})
</script>


<style>
</style>
