import { api } from './client';
import type { UserProfile, UpdateProfileRequest } from '../../types';

export async function getMyProfile() {
    const { data } = await api.get<UserProfile>('/User/GetMyProfile');
    return data;
}

export const updateMyProfile = (request: UpdateProfileRequest) =>
    api.put('/User/UpdateMyProfile', request);