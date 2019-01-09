<template>
  <div>
    <h1>Player Update</h1>
    <v-progress-circular v-if="isLoading" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-form v-else>
      <v-text-field label="Display Name" v-model="player.name"></v-text-field>
      <swatches colors="material-basic" inline v-model="player.colour"/>
      <v-btn @click="submit" color="primary">Save</v-btn>
    </v-form>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from '../axios-service'
import { Player } from '../models/Player'
import Swatches from 'vue-swatches'
export default Vue.extend({
  name: 'PlayerEdit',
  components: {
    Swatches
  },
  props: {
    id: {
      type: Number,
    },
  },
  data: () => ({
    player: {} as Player,
    error: '',
    isLoading: false
  }),
  async created() {
    this.isLoading = true;
    try {
      this.player = (await httpClient.get(`users/${this.id}`)).data
    }
    catch (e) {
      this.error = e;
    }
    this.isLoading = false;
  },
  methods: {
    async submit() {
      this.isLoading = true;
      try {
        var response = await httpClient.put(`users/${this.id}`, {
          name: this.player.name,
          colour: parseInt(this.player.colour.replace('#', ''), 16)
        })
        if (response.status == 200) {
          var user = this.$store.state.user;
          user.name = this.player.name;
          this.$store.commit('setUser', user)
          this.$router.push({ name: 'player-profile', params: { id: this.id.toString() } })
        }
      }
      catch (e) {
        this.error = e;
        console.log(e)
      }
      this.isLoading = false;
    }
  }
})
</script>


<style>
</style>
