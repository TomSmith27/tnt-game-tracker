<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <span>weatherForecasts {{weatherForecasts}}</span>
    <HelloWorld msg="Welcome to Your Vue.js + TypeScript App sssssssss"/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
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
    var ax = axios.create({
      baseURL: `https://localhost:44375/api/`,
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
      }
    });
    ax.get("sampledata/weatherForecasts")
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
