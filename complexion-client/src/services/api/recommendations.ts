import { api } from './client';
import type { ShareRecommendationRequest, MyRecommendation, RecommendationSummary } from '../../types';

export const shareRecommendation = (request: ShareRecommendationRequest) =>
    api.post('/ProductRecommendation/CreateRecommendation', request);

export const updateRecommendation = (recommendationId: string, comment: string) =>
    api.patch(`/ProductRecommendation/UpdateRecommendation/${recommendationId}`, { comment });

export const deleteRecommendation = (recommendationId: string) =>
    api.delete(`/ProductRecommendation/DeleteRecommendation/${recommendationId}`);

export async function getRecommendationsByUserId(userId: string) {
    const { data } = await api.get<MyRecommendation[]>(
        `/ProductRecommendation/GetRecommendationsByUserId/${userId}`
    );
    return data;
}

export async function getRecommendationSummaries() {
    const { data } = await api.get<RecommendationSummary[]>('/ProductRecommendation/GetRecommendationSummaries');
    return data;
}