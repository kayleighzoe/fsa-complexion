import { api } from './client';
import type { MyRecommendation, UserProfile, UpdateProfileRequest } from '../types';

export async function getMyProfile() {
    const { data } = await api.get<UserProfile>('/User/GetMyProfile');
    return data;
}

export async function getMyRecommendations() {
    const { data } = await api.get<MyRecommendation[]>('/ProductRecommendation/GetMyRecommendations');
    return data;
}

export const updateRecommendation = (recommendationId: string, comment: string) =>
    api.patch(`/ProductRecommendation/UpdateRecommendation/${recommendationId}`, { comment });

export const deleteRecommendation = (recommendationId: string) =>
    api.delete(`/ProductRecommendation/DeleteRecommendation/${recommendationId}`);

export const updateMyProfile = (request: UpdateProfileRequest) =>
    api.put('/User/UpdateMyProfile', request);