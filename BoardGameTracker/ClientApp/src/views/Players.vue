<template>
  <div>
    <h1>Players</h1>
    <v-container grid-list-md text-xs-center>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <v-card>
        <v-layout pa-4 wrap>
          <v-flex offset-xs6 xs6>
            <v-text-field v-model="search" append-icon="search" label="Search" single-line hide-details></v-text-field>
          </v-flex>
        </v-layout>
        <v-data-table :loading="loading" :pagination.sync="pagination" :headers="headers" :items="players" :search="search">
          <template slot="items" slot-scope="props">
            <tr @click="props.expanded = !props.expanded">
              <td>
                <v-avatar tile class="white--text" :color="props.item.colour">{{props.item.name.charAt(0).toUpperCase()}}</v-avatar>
              </td>
              <td class="pa-0 text-xs-left">
                <router-link :to="{name : 'player-profile', params : { id : props.item.id}}">{{ props.item.name }}</router-link>
              </td>
            </tr>
          </template>

          <v-alert slot="no-results" :value="true" color="error" icon="warning">Your search for "{{ search }}" found no results.</v-alert>
        </v-data-table>
      </v-card>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { httpClient } from "../axios-service";
export default Vue.extend({
  data: () => ({
    players: [],
    error: "",
    search: "",
    loading: false,
    pagination: {
      descending: true,
      rowsPerPage: 25
    },
    headers: [
      { text: "", value: "", sortable: false },
      { text: "Name", value: "name" },
    ]
  }),
  async created() {
    this.loading = true;
    try {
      this.players = (await httpClient.get(`users`)).data;
    } catch (error) {

    }
    this.loading = false;
  }
})
</script>


<style>
</style>
