import axios from "axios";
import { Config } from "./../Config";

export const axiosPublic = axios.create({
  baseURL: Config.API_URL,
  // headers: { "Content-Type": "application/json" },
  withCredentials: false,
});

// Add a request interceptor to include the token
axiosPublic.interceptors.request.use(
  (config) => config,
  (error) => {
    return Promise.reject(error);
  }
);

axiosPublic.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 500) {
      const errorMessage = error.response?.data?.message || error.message || "An error occurred";
      window.location.href = `/servererror?detailed=${encodeURIComponent(errorMessage)}`;
      return Promise.reject(errorMessage);
    }

    console.log(error.response?.data?.errors.toString());
    return Promise.reject(error.response?.data?.errors.toString());
  }
);
