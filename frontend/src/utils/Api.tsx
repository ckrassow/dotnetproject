import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:5175/api/" 
});

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const refreshToken = localStorage.getItem("refreshToken");
        const response = await axiosInstance.post("/refresh-token", { refreshToken });
        const { token: newToken } = response.data;
        localStorage.setItem("token", newToken);
        originalRequest.headers.Authorization = `Bearer ${newToken}`;
        return axiosInstance(originalRequest);
      } catch (error) {
        console.error("Refresh token error:", error);
      }
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;