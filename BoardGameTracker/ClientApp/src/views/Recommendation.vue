<template>
  <div>
    <h1>Recommendation</h1>
    <v-select v-model="minPlayers" :items="[1, 2, 3, 4 , 5, 6, 7, 8, 9, 10]" label="Players">
      <template slot="item" slot-scope="data">{{ data.item }}+</template>
    </v-select>

    <ul>
      <li :key="game.id" v-for="game in filteredGames">
        <v-avatar :tile="true" slot="activator" size="60px">
          <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar" />
          <v-icon v-else color="danger" v-text="test"></v-icon>
        </v-avatar>
        {{game.name}}
        {{game.averageRating | round}}
        <span>
          <v-icon>people</v-icon>
          {{game.minPlayers}} - {{game.maxPlayers}}
        </span>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { httpClient } from '../axios-service';
import { Game } from '../models/Game';
export default Vue.extend({
    name: 'Recommendation',
    data() {
        return {
            minPlayers: 4,
            recommendations: [] as Game[]
        };
    },
    async created() {
        try {
            this.recommendations = (await httpClient.get(
                'games/recommendations'
            )).data;
        } catch (error) {
            console.error(error);
        }
    },
    computed: {
        filteredGames(): Game[] {
            return this.recommendations.filter(
                g => g.maxPlayers >= this.minPlayers
            );
        }
    }
});
</script>

<style>
</style>