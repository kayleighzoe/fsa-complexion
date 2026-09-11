export interface Recommendation {
    id: string;
    brandName: string;
    productName: string;
    category: string;
    undertone: string;
    shade: string;
    recommendedByCount: number;
    priceTier: string;
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