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