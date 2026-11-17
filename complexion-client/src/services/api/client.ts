import axios from 'axios';

let onUnauthorized: () => void = () => {};

export function setUnauthorizedHandler(handler: () => void) {
    onUnauthorized = handler;
}

export const api = axios.create({
  baseURL: 'http://localhost:8080/api',
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
  withCredentials: true,
});

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (axios.isAxiosError(error) && error.response?.status === 401) {
            onUnauthorized();
        }
        return Promise.reject(error);
    }
);