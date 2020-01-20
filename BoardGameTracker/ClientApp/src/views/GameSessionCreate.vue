<template>
  <div>
    <h1>Game Create</h1>
    <v-progress-circular v-if="isLoading" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-form v-else>
      <v-autocomplete v-model="selectedGame" :items="games" item-value="id" item-text="name" label="Game"></v-autocomplete>
      <v-img max-height="200px" v-if="thumbnail" contain :src="thumbnail" aspect-ratio="1"></v-img>
      <v-flex>
        <v-dialog ref="dialog" v-model="modal" :return-value.sync="date" persistent lazy full-width width="290px">
          <v-text-field slot="activator" v-model="dateFormatted" label="Date Played" readonly></v-text-field>
          <v-date-picker v-model="date" scrollable>
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="modal = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.dialog.save(date)">OK</v-btn>
          </v-date-picker>
        </v-dialog>
      </v-flex>
      <v-layout row wrap>
        <v-flex xs12>
          <h2 class="text-xs-left">Players</h2>
        </v-flex>
        <v-flex class="name" v-for="p in players" :key="p.id">
          <v-checkbox v-model="selectedPlayers" :label="p.name" :value="p"></v-checkbox>
        </v-flex>
        <v-flex xs12>
          <v-text-field v-model="guests" label="Guests" type="number"></v-text-field>
        </v-flex>
      </v-layout>

      <v-layout row wrap>
        <v-flex xs12>
          <h2 class="text-xs-left">Winners</h2>
        </v-flex>
        <v-flex class="name" v-for="p in selectedPlayers" :key="p.id">
          <v-checkbox color="warning" v-model="winningPlayers" :label="p.name" :value="p"></v-checkbox>
        </v-flex>
      </v-layout>
      <v-flex xs12>
        <v-textarea no-resize auto-grow v-model="notes" label="Notes" rows="1"></v-textarea>
      </v-flex>

      <v-btn @click="submit" color="primary">Save</v-btn>
    </v-form>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Game } from '../models/Game';
import { httpClient } from '../axios-service';
import moment from 'moment';
export default Vue.extend({
  name: 'GameSessionCreate',
  props: {
    gameId: {
      type: Number,
    },
  },
  data: () => ({
    isLoading: true,
    modal: false,
    games: [] as Game[],
    players: [],
    guests: 0,
    notes: '',
    selectedGame: 0,
    selectedPlayers: [],
    winningPlayers: [],
    date: new Date().toISOString().substr(0, 10),
  }),
  async created() {
    this.isLoading = true;
    try {
      this.games = (await httpClient.get('games')).data;
      this.players = (await httpClient.get('users')).data;
      if (this.gameId) {
        this.selectedGame = this.gameId;
      }
    } catch (error) { }

    this.isLoading = false;
  },
  computed: {
    thumbnail(): string {
      if (this.selectedGame != 0) {
        return this.games.filter(g => g.id == this.selectedGame)[0].thumbnail;
      }
      return '';
    },
    dateFormatted(): string {
      return this.date ? moment(this.date).format('dddd, MMMM Do YYYY') : ''
    }
  },
  methods: {
    async submit() {
      try {
        const sessionId = (await httpClient.post('game-session', {
          gameId: this.selectedGame,
          date: this.date,
          players: this.selectedPlayers.map((p: any) => p.id),
          winners: this.winningPlayers.map((p: any) => p.id),
          guests: this.guests,
          notes: this.notes
        })).data;
        this.$router.push({
          name: 'game-sessions',
        });
      } catch (error) { }
    },
  },
  watch: {
    selectedPlayers() {
      const selectedPlayers = this.selectedPlayers.map(p => (p as any).id)
      this.winningPlayers = this.winningPlayers.filter(value => -1 !== selectedPlayers.indexOf((value as any).id));
    }
  }
});
</script>


<style>
.name {
  min-width: 100px;
  max-width: 150px;
}
</style>
