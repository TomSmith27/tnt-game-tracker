<template>
  <div>
    <h1>Game Sessions</h1>
    <router-link :to="{name : 'game-session-create'}">
      <v-btn color="primary">New</v-btn>
    </router-link>
    <v-timeline dense>
      <v-timeline-item :key="gameSession.id" v-for="gameSession in gameSessions" small>
        <div class="py-4">
          <v-card>
            <v-img
              max-height="200px"
              v-if="gameSession.game.thumbnail"
              contain
              :src="gameSession.game.thumbnail"
              aspect-ratio="1"
            ></v-img>

            <v-card-title primary-title>
              <div>
                <div class="headline">{{gameSession.game.name}}</div>
                <h3 class="grey--text">{{gameSession.date | bigDate}}</h3>
              </div>
            </v-card-title>
            <v-btn
              color="success"
              :to="{name : 'game-session-update', params : { id : gameSession.id}}"
            >Edit</v-btn>
            <v-list>
              <v-list-group v-model="gameSession.show">
                <v-list-tile slot="activator">
                  <v-list-tile-content>
                    <v-list-tile-title>Players : {{gameSession.players.length}}</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>

                <v-list-tile :key="player.id" v-for="player in gameSession.players">
                  <v-list-tile-content>
                    <v-list-tile-title>
                      {{ player.player.name }}
                      <v-icon>star_border</v-icon>
                      <v-icon>star_border</v-icon>
                      <v-icon>star_border</v-icon>
                      <v-icon>star_border</v-icon>
                      <v-icon>star_border</v-icon>
                    </v-list-tile-title>
                  </v-list-tile-content>

                  <v-list-tile-action>
                    <!--     <v-icon>{{ subItem.action }}</v-icon> -->
                  </v-list-tile-action>
                </v-list-tile>
              </v-list-group>
            </v-list>
          </v-card>
        </div>
      </v-timeline-item>
    </v-timeline>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { httpClient } from "@/axios-service";
import moment from "moment";
export default Vue.extend({
  name: "GameSessionList",
  data: () => ({
    gameSessions: [],
    error: "",
    items: [
      {
        action: "local_activity",
        title: "Attractions",
        items: [{ title: "List Item" }]
      },
      {
        action: "restaurant",
        title: "Dining",
        active: true,
        items: [
          { title: "Breakfast & brunch" },
          { title: "New American" },
          { title: "Sushi" }
        ]
      },
      {
        action: "school",
        title: "Education",
        items: [{ title: "List Item" }]
      },
      {
        action: "directions_run",
        title: "Family",
        items: [{ title: "List Item" }]
      },
      {
        action: "healing",
        title: "Health",
        items: [{ title: "List Item" }]
      },
      {
        action: "content_cut",
        title: "Office",
        items: [{ title: "List Item" }]
      },
      {
        action: "local_offer",
        title: "Promotions",
        items: [{ title: "List Item" }]
      }
    ]
  }),
  filters: {
    bigDate: (value: string) => {
      return moment(value).format("Do MMM YYYY");
    }
  },
  created() {
    httpClient.get("game-session").then(response => {
      response.data.forEach((element: any) => {
        element.show = false;
      });
      this.gameSessions = response.data;
      this.gameSessions.reverse();
    });
  }
});
</script>


<style>
</style>
