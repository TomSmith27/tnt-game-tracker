<template>
  <div>
    <h1>WishList</h1>
    {{wishlist}}
  </div>
</template>

<script>
import { httpClient } from "../axios-service";
export default {
  name: "WishList",
  data() {
    return {
      wishlist: {}
    };
  },
  async created() {
    this.wishlist = (await httpClient.get("wishlist")).data;
  }
};
</script>

<style>
</style>