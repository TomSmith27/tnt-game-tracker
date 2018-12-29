<template>
  <div>
    <h1>Game Update</h1>
    <v-progress-circular v-if="isLoading" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-form v-else>
      <v-autocomplete
        v-model="selectedGame"
        :items="games"
        item-value="id"
        item-text="name"
        label="Game"
      ></v-autocomplete>
      <v-img max-height="200px" v-if="thumbnail" contain :src="thumbnail" aspect-ratio="1"></v-img>
      <v-flex>
        <v-dialog
          ref="dialog"
          v-model="modal"
          :return-value.sync="date"
          persistent
          lazy
          full-width
          width="290px"
        >
          <v-text-field slot="activator" v-model="date" label="Date Played" readonly></v-text-field>
          <v-date-picker v-model=" date" scrollable>
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="modal = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.dialog.save(date)">OK</v-btn>
          </v-date-picker>
        </v-dialog>
      </v-flex>
      <v-combobox
        v-model="selectedPlayers"
        :items="players"
        label="Players"
        multiple
        item-text="name"
        item-value="name"
        type="text"
      ></v-combobox>
      <v-btn @click="submit" color="primary">Save</v-btn>
    </v-form>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Game } from "../models/Game";
import { httpClient } from "../axios-service";
export default Vue.extend({
  name: "GameSessionCreate",
  props: {
    id: {
      type: Number
    }
  },
  data: () => ({
    isLoading: true,
    modal: false,
    games: [] as Game[],
    players: [],
    selectedGame: 0,
    selectedPlayers: [],
    date: new Date().toISOString().substr(0, 10)
  }),
  async created() {
    this.isLoading = true;
    try {
      this.games = (await httpClient.get("games")).data;
      this.players = (await httpClient.get("users")).data;
      const currentGame = await httpClient.get(`game-session/${this.id}`);
      this.selectedGame = currentGame.data.gameId;
      this.selectedPlayers = currentGame.data.players.map((p: any) => p.player);
    } catch (error) {}

    this.isLoading = false;
  },
  computed: {
    thumbnail(): string {
      if (this.selectedGame != 0) {
        return this.games.filter(g => g.id == this.selectedGame)[0].thumbnail;
      }
      return "";
    }
  },
  methods: {
    async submit() {
      try {
        const sessionId = (await httpClient.post(`game-session/${this.id}`, {
          gameId: this.selectedGame,
          date: this.date,
          players: this.selectedPlayers.map((p: any) => p.id)
        })).data;
        this.$router.push({
          name: "game-sessions"
        });
      } catch (error) {}
    }
  }
});
</script>


<style>
</style>
