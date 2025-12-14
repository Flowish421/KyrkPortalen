import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5297/api",
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token && token !== "undefined") {
    config.headers.Authorization = `Bearer ${token}`;
  } else {
    console.warn("Ingen giltig token hittades i localStorage");
  }
  return config;
});

export default api;
