export interface MyRecommendation {
    recommendationId: string;
    brand: string;
    productName: string;
    categoryName: string;
    shadeName: string;
    priceTierName: string;
    comment?: string | null;
    createdAt: string;
}

export interface SkinShade {
    shadeId: number;
    name: string;
}

export interface SkinUndertone {
    undertoneId: number;
    name: string;
}

export interface RegisterRequest {
    firstName: string;
    surname: string;
    email: string;
    username: string;
    password: string;
    shadeId: number;
    undertoneId: number;
    hasTint: boolean;
}

export interface LoginRequest {
    usernameOrEmail: string;
    password: string;
}

export interface CurrentUser {
    userId: string;
    username: string;
}

export interface UserProfile {
    firstName: string;
    surname: string;
    email: string;
    shadeName: string;
    undertoneName: string;
    hasTint: boolean;
}

export interface UpdateProfileRequest {
    firstName: string;
    surname: string;
    email: string;
    shadeId: number;
    undertoneId: number;
    hasTint: boolean;
}

export interface ShareRecommendationRequest {
    categoryName: string;
    priceTierName: string;
    brand: string;
    productName: string;
    shadeName: string;
    matchedShadeName: string;
    matchedUndertoneName: string;
    hasTint: boolean;
    comment?: string;
}

export interface CatalogueCategory {
    categoryId: number;
    name: string;
}

export interface ConfigPriceTier {
    priceTierId: number;
    name: string;
}

export interface RecommendationComment {
    username: string;
    text: string;
}

export interface RecommendationSummary {
    brand: string;
    productName: string;
    productShadeName: string;
    categoryName: string;
    priceTierName: string;
    shadeName: string;
    undertoneName: string;
    hasTint: boolean;
    recommendedByCount: number;
    comments: RecommendationComment[];
}