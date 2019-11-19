<template>
  <v-layout>
    <v-flex xs12>
      <v-card>
        <v-card-title primary-title>
          <v-layout row wrap>
            <v-flex xs12 md2 lg2>
              <img :src="game.thumbnail" />
              <v-btn block :to="{name : 'game-session-create', params : {gameId : game.id}}" color="primary">
                <v-icon>play_arrow</v-icon>
              </v-btn>
              <v-btn v-if="!isOnWishList" block @click="addToWishList" color="success">
                <v-icon>favorite</v-icon>
              </v-btn>
              <v-btn v-if="isOnWishList" block @click="removeFromWishList" color="error">
                <v-icon>favorite</v-icon>
              </v-btn>
            </v-flex>
            <v-flex xs12 md3 lg2>
              <v-flex md12>
                <h4>Bgg Rating</h4>
                <div class="rating">{{game.averageRating | round}}</div>
              </v-flex>
              <v-flex md12>
                <h4>Our Rating</h4>
                <v-btn color="primary" block @click="ratingsPanelOpen = true">{{ourRating | round}}</v-btn>
                <div class="rating"></div>
                <v-dialog v-model="ratingsPanelOpen" max-width="90%">
                  <v-card>
                    <v-card-title>
                      <h4>Ratings</h4>
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-list dense>
                      <player-rating :key="playerRating.id" v-for="playerRating in game.playerRatings" :id="id" :player-rating="playerRating"></player-rating>
                    </v-list>
                    <v-card-actions>
                      <v-btn color="primary" block @click="ratingsPanelOpen=false">Close</v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-flex>
              <v-flex xs12>
                <h5>Categories</h5>
                <v-chip :key="category.id" v-for="category in game.categories">{{category.boardGameCategory.name}}</v-chip>
              </v-flex>
            </v-flex>
            <v-flex md7 lg8>
              <div class="headline">{{game.name}}</div>
              <div>
                <span>
                  <v-icon>people</v-icon>
                  {{game.minPlayers}} - {{game.maxPlayers}}
                </span>
              </div>
              <div>
                <span>
                  <v-icon>alarm</v-icon>
                  {{game.minPlaytime}}" - {{game.maxPlaytime}}"
                </span>
              </div>
              <p class="description" v-html="game.description"></p>
            </v-flex>
          </v-layout>
        </v-card-title>

        <v-card-actions>
          <!--    <v-btn flat color="orange">Share</v-btn>
          <v-btn flat color="orange">Explore</v-btn>-->
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script lang="ts">
import Vue from 'vue';
import { httpClient } from '../axios-service';
import PlayerRating from '../components/player-rating.vue';
import { Game } from '@/models/Game';
export default Vue.extend({
    components: {
        PlayerRating
    },
    props: {
        id: {
            type: Number
        }
    },
    data: () => ({
        game: {} as Game,
        isOnWishList: false,
        ratingsPanelOpen: false
    }),
    async created() {
        var response = (await httpClient.get(`games/${this.id}`)).data;
        this.game = response.game;
        this.isOnWishList = response.isOnWishList;
    },
    computed: {
        ourRating(): number {
            if (
                this.game &&
                this.game.playerRatings != null &&
                this.game.playerRatings.length > 0
            ) {
                var ratings = this.game.playerRatings
                    .filter(f => f.rating)
                    .map(r => r.rating);
                return ratings.reduce((a, b) => a + b, 0) / ratings.length;
            }
            return 0;
        }
    },
    methods: {
        async addToWishList() {
            await httpClient.post(`games/${this.id}/add-to-wishlist`);
            this.isOnWishList = !this.isOnWishList;
        },
        async removeFromWishList() {
            await httpClient.post(`games/${this.id}/remove-from-wishlist`);
            this.isOnWishList = !this.isOnWishList;
        }
    }
});
</script>


<style>
.rating {
    font-size: 20px;
    background-color: #424242;
    color: white;
}

.description {
    max-height: 100px;
    overflow: scroll;
}
</style>
