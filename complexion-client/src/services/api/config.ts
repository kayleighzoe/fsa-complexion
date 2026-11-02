import { api } from './client';
import type { ConfigPriceTier } from '../../types';

export async function getPriceTiers() {
    const { data } = await api.get<ConfigPriceTier[]>('/ConfigPriceTier/GetAllPriceTiers');
    return data;
}