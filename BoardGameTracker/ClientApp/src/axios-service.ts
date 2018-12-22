import axios from "axios";
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
