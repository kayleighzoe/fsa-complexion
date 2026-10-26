import { api } from './client';
import type { SkinShade, SkinUndertone } from '../types';

export async function getSkinShades() {
    const { data } = await api.get<SkinShade[]>('/SkinShade/GetAllSkinShades');
    return data;
}

export async function getSkinUndertones() {
    const { data } = await api.get<SkinUndertone[]>('/SkinUndertone/GetAllSkinUndertones');
    return data;
}