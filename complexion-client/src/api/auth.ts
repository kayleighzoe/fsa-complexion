import { api } from './client';
import type { CurrentUser, LoginRequest, RegisterRequest } from '../types';

export const register = (request: RegisterRequest) => api.post('/Auth/Register', request);

export const login = (request: LoginRequest) => api.post('/Auth/Login', request);

export const logout = () => api.post('/Auth/Logout');

export async function getCurrentUser() {
    const { data } = await api.get<CurrentUser>('/Auth/Me');
    return data;
}