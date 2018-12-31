<template>
  <v-container>
    <v-layout row v-if="error">
      <v-flex xs12 sm6 offset-sm3>
        <v-alert :value="error" type="error">{{error}}</v-alert>
      </v-flex>
    </v-layout>
    {{error}}
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-card-text>
            <v-container>
              <form @submit.prevent="signIn">
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="email" label="Username" v-model="username" required></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field
                      name="password"
                      label="Password"
                      id="password"
                      v-model="password"
                      type="password"
                      required
                    ></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-btn type="submit" :disabled="loading" :loading="loading" color="primary">
                      Sign in
                      <span slot="loader" class="custom-loader">
                        <v-icon light>cached</v-icon>
                      </span>
                    </v-btn>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <router-link :to="{name : 'register'}">Dont have an account? Sign up</router-link>
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
  name: 'Login',
  data: () => ({
    error: '',
    loading: false,
    username: '',
    password: '',
  }),
  methods: {
    async signIn() {
      this.loading = true;
      try {
        const authPlayer = (await httpClient.post('Users/authenticate', {
          username: this.username,
          password: this.password,
        })).data;
        const user: User = {
          id: authPlayer.id,
          name: authPlayer.name,
          token: authPlayer.token,
        };
        this.$store.commit('setUser', user);
        if (this.$route.query.redirect) {
          this.$router.push(this.$route.query.redirect.toString());
        } else {
          this.$router.push({ name: 'home' });
        }
      } catch (error) {
        this.error = error;
      }
      this.loading = false;
    },
  },
});
</script>


<style>
</style>
