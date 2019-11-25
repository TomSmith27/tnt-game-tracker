<template>
  <div>
    <h1>Recommendation</h1>
    <v-layout row wrap>
      <v-flex xs2>
        <v-select v-model="minPlayers" :items="[1, 2, 3, 4 , 5, 6, 7, 8, 9, 10]" label="Players">
          <template slot="item" slot-scope="data">{{ data.item }}+</template>
        </v-select>
      </v-flex>
      <v-flex xs4>
        <v-select item-text="name" v-model="gameLength" :items="[{name : 'Short < 30', value : 30},{name : 'Medium < 90', value : 90},{name : 'Long > 90', value : 300}]" label="Game Length"></v-select>
      </v-flex>
      <v-flex xs6>
        <v-checkbox v-model="onlyShowUnplayed" :label="'Only show Unplayed'"></v-checkbox>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex>
        <div class="text-xs-left" :key="game.id" v-for="game in filteredGames">
          <v-avatar :tile="true" slot="activator" size="60px">
            <img v-if="game.thumbnail" :src="game.thumbnail" alt="Avatar" />
            <v-icon v-else color="danger" v-text="test"></v-icon>
          </v-avatar>
          <router-link :to="{name : 'game-detail', params : { id : game.id}}">{{game.name}}</router-link>
          <v-btn small fab color="primary">{{game.averageRating | round}}</v-btn>
          <span>
            <v-icon>people</v-icon>
            {{game.minPlayers}} - {{game.maxPlayers}}
          </span>
          <span>
            <v-icon>alarm</v-icon>
            {{game.minPlaytime}}" - {{game.maxPlaytime}}"
          </span>
          <!--         <v-btn small @click="addToWishList(game.id)" color="success">
            <v-icon>favorite</v-icon>
          </v-btn>-->
        </div>
      </v-flex>
    </v-layout>
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
            gameLength: 90,
            onlyShowUnplayed: true,
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
            return this.recommendations
                .filter(g => g.maxPlayers >= this.minPlayers)
                .filter(g => g.maxPlaytime < this.gameLength)
                .filter(g => g.havePlayed != this.onlyShowUnplayed)
                .sort((a, b) => (a.averageRating > b.averageRating ? -1 : 1));
        }
    },
    methods: {
        async addToWishList(gameId: Number) {
            await httpClient.post(`games/${gameId}/add-to-wishlist`);
        }
    }
});
</script>

<style>
</style>