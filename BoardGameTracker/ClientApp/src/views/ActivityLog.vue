<template>
  <div>
    <h1>Activity log</h1>
    <v-container grid-list-md text-xs-center>
      <v-alert :value="error" type="error">{{error}}</v-alert>
      <v-card>
        <v-layout pa-4 wrap>
          <v-flex offset-xs6 xs6>
            <v-text-field v-model="search" append-icon="search" label="Search" single-line hide-details></v-text-field>
          </v-flex>
        </v-layout>
        <v-data-table :loading="loading" :pagination.sync="pagination" :headers="headers" :items="activities" :search="search">
          <template slot="items" slot-scope="props">
            <tr @click="props.expanded = !props.expanded">
              <td>{{props.item.player.name }} {{props.item.message}}</td>
              <td>{{props.item.date | date('ddd Do MMM YYYY HH:mm')}}</td>
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
import { httpClient } from '@/axios-service';
export default Vue.extend({
  name: 'ActivityLog',
  data: () => ({
    activities: [],
    error: "",
    search: "",
    loading: false,
    pagination: {
      sortBy: "date",
      descending: true,
      rowsPerPage: 25
    },
    headers: [
      { text: "Message", value: "message" },
      { text: "Date", value: "date" },
    ]
  }),
  async created() {
    this.loading = true;
    try {
      this.activities = (await httpClient.get(`activites`)).data;
    } catch (error) {

    }
    this.loading = false;
  }
})
</script>


<style>
</style>
