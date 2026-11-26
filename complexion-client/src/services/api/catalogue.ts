import { api } from './client';
import type { CatalogueCategory } from '../../types';

export async function getCategories() {
    const { data } = await api.get<CatalogueCategory[]>('/CatalogueCategory/GetAllCategories');
    return data;
}