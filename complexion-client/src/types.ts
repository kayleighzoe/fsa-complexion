export interface Comment {
    id: string;
    text: string;
    author: string;
    skinProfile: string;
}

export interface Recommendation {
    id: string;
    brandName: string;
    productName: string;
    category: string;
    undertone: string;
    shade: string;
    recommendedByCount: number;
    priceTier: string;
    comments: Comment[];
}

export interface MyRecommendation {
    id: string;
    brandName: string;
    productName: string;
    category: string;
    shadeName: string;
    priceTier: string;
    comment?: string;
    sharedOn: string;
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