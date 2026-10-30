import { api } from './client';
import type { ShareRecommendationRequest } from '../../types';

export const shareRecommendation = (request: ShareRecommendationRequest) =>
    api.post('/ProductRecommendation/CreateRecommendation', request);