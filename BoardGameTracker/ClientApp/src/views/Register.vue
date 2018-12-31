<template>
  <v-container>
    <v-layout row v-if="error">
      <v-flex xs12 sm6 offset-sm3>
        <v-alert :value="error" type="error">{{error}}</v-alert>
      </v-flex>
    </v-layout>
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-card-title>
            <h3>Sign up</h3>
          </v-card-title>
          <v-card-text>
            <v-container>
              <form @submit.prevent="signUp">
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="display" label="Display Name" v-model="displayName" required></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="email" label="Username" v-model="username" required></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="password" label="Password" id="password" v-model="password" type="password" required></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-btn type="submit" :disabled="loading" :loading="loading" color="primary">
                      Sign up
                      <span slot="loader" class="custom-loader">
                        <v-icon light>cached</v-icon>
                      </span>
                    </v-btn>
                  </v-flex>
                </v-layout>
              </form>
            </v-container>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import { httpClient } from '@/axios-service';
import { User } from '../models/User';
export default Vue.extend({
  name: 'Register',
  data: () => ({
    error: '',
    loading: false,
    displayName: '',
    username: '',
    password: ',,,',
  }),
  methods: {
    async signUp() {
      this.loading = true;
      try {
        const authPlayer = (await httpClient.post('Users/register', {
          name: this.displayName,
          username: this.username,
          password: this.password,
        })).data;

        this.$router.push({ name: 'home' });
      } catch (error) {
        this.error = error.response.data.message;
      }
      this.loading = false;
    }
  },
});
</script>


<style>
</style>
