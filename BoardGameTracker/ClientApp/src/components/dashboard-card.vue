<template>
  <v-card>
    <v-card-title primary-title>
      <h3 class="headline mb-0">{{title}}</h3>
    </v-card-title>
    <v-card-text>
      <v-list>
        <slot></slot>
      </v-list>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
  name: 'dashboard-card',
  props: {
    title: String
  }
})
</script>

<style>
</style>