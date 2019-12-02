<template>
  <div>
    <h1>Want to Play list</h1>
    <v-layout row wrap>
      <v-flex xs12>
        <h2 class="text-xs-left">Players</h2>
      </v-flex>
      <v-flex class="name" v-for="p in players" :key="p.id">
        <v-checkbox v-model="selectedPlayers" :label="p.name" :value="p"></v-checkbox>
      </v-flex>
    </v-layout>
    <v-progress-circular v-if="isLoading" :size="50" color="primary" indeterminate></v-progress-circular>
    <v-container v-if="!isLoading" grid-list-md>
      <v-layout row wrap>
        <v-flex xs12 :key="voteGroup" v-for="voteGroup in filteredWishList">
          <v-card>
            <v-card-title class="primary lighten-1">
              <h2 class="white--text font-weight-light">{{voteGroup[0].numPlayers}} Vote/s</h2>
            </v-card-title>
            <v-container>
              <v-layout :key="game.id" v-for="({game, players}) in voteGroup">
                <v-flex xs2>
                  <v-img :src="game.thumbnail" height="100px" contain></v-img>
                </v-flex>
                <v-flex xs9>
                  <v-card-title primary-title>
                    <div>
                      <div class="headline">
                        <router-link :to="{name : 'game-detail', params : { id : game.id}}">{{ game.name }}</router-link>
                      </div>
                      <div>
                        <v-chip outline label :key="player.id" v-for="player in players">
                          <v-avatar tile class="white--text" :color="player.colour">{{player.name.charAt(0).toUpperCase()}}</v-avatar>
                          <router-link :to="{name : 'player-profile', params : {id : player.id}}">{{player.name}}</router-link>
                        </v-chip>
                      </div>
                    </div>
                  </v-card-title>
                </v-flex>
                <v-flex xs1></v-flex>
              </v-layout>
            </v-container>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import { httpClient } from '../axios-service';
export default {
    name: 'WishList',
    data() {
        return {
            wishlist: [],
            isLoading: false,
            players: [],
            selectedPlayers: []
        };
    },
    computed: {
        filteredWishList() {
            if (this.wishlist.length > 0) {
                return this.groupBy(
                    this.wishlist
                        .map(w => ({
                            game: w.game,
                            players: w.players.filter(p =>
                                this.selectedPlayers.some(s => s.id == p.id)
                            ),
                            numPlayers: w.players.filter(p =>
                                this.selectedPlayers.some(s => s.id == p.id)
                            ).length
                        }))
                        .filter(w => w.players.length > 0),
                    'numPlayers'
                );
            }
        }
    },
    methods: {
        groupBy(xs, key) {
            return xs.reduce((rv, x) => {
                (rv[x[key]] = rv[x[key]] || []).push(x);
                return rv;
            }, {});
        }
    },
    async created() {
        this.isLoading = true;
        this.players = (await httpClient.get('users')).data;
        this.selectedPlayers = this.players.map(p => p);
        this.wishlist = (await httpClient.get('wishlist')).data;
        this.isLoading = false;
    }
};
</script>

<style>
</style>