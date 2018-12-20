<template>
  <div class="home">
    <HelloWorld msg="Welcome to the board game tracking app i am a test am"/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
import { httpClient } from "../axios-service";
import HelloWorld from "@/components/HelloWorld.vue"; // @ is an alias to /src

export default Vue.extend({
  name: "home",
  components: {
    HelloWorld
  },
  data: () => {
    return {
      count: 0,
      weatherForecasts: {}
    };
  },
  created() {
    httpClient
      .get("sampledata/weatherForecasts")
      .then(data => {
        console.log(data);
        this.weatherForecasts = data.data;
      })
      .catch(() => {
        console.error("error");
      });
  }
});
</script>
