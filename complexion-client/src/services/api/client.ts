import axios from 'axios';
import type { AxiosError } from 'axios';
import { getErrorMessage } from './errors';
import { toast } from '@/components/ui/toast';

const AUTH_CHECK_URL = '/auth/me';
const LOGIN_URL = '/auth/login';
const SESSION_EXPIRED_MESSAGE = 'Your session has expired. Please log in again.';

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

function showErrorToast(error: AxiosError) {
    const url = error.config?.url?.toLowerCase();
    const isUnauthorized = error.response?.status === 401;

    if (isUnauthorized && url === AUTH_CHECK_URL) {
        return;
    }

    const title = isUnauthorized && url !== LOGIN_URL
        ? SESSION_EXPIRED_MESSAGE
        : getErrorMessage(error);

    toast.add({ title, type: 'error' });
}

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (axios.isAxiosError(error)) {
            if (error.response?.status === 401) {
                onUnauthorized();
            }

            showErrorToast(error);
        }

        return Promise.reject(error);
    }
);