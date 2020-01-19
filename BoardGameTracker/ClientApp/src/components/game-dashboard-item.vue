<template>
  <v-list-tile>
    <v-list-tile-avatar tile>
      <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar" />
    </v-list-tile-avatar>
    <v-list-tile-content>
      <v-list-tile-title>
        <router-link :to="{name : 'game-detail', params : {id : game.id}}">{{game.name}}</router-link>
      </v-list-tile-title>
    </v-list-tile-content>
    <v-list-tile-action>
      <slot></slot>
    </v-list-tile-action>
  </v-list-tile>
</template>

<script lang="ts">
import Vue from 'vue'
import { Game } from '../models/Game'
export default Vue.extend({
  name: 'game-dashboard-item',
  props: {
    game: {
      type: Object as () => Game
    }
  }
})
</script>

<style>
</style>