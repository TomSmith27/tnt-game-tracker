import axios from "axios";
import store from "./store";
import router from "./router";
let baseURL = process.env.BASE_URL;
if (process.env.NODE_ENV === "development") {
  baseURL = "https://localhost:44309/";
}

export const httpClient = axios.create({
  baseURL: `${baseURL}api/`,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
  },
});

httpClient.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if (error.response.status === 401) {
      store.commit("logout");
      router.go(0);
    }
    return error;
  },
);
