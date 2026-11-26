import { useEffect, useState } from 'react';

export function useSessionStorageState<T>(key: string, initialValue: T) {
    const [value, setValue] = useState<T>(() => {
        try {
            const stored = sessionStorage.getItem(key);

            return stored === null ? initialValue : (JSON.parse(stored) as T);
        } catch {
            return initialValue;
        }
    });

    useEffect(() => {
        try {
            sessionStorage.setItem(key, JSON.stringify(value));
        } catch {
        }
    }, [key, value]);

    return [value, setValue] as const;
}

export function clearSessionStorageKeys(keys: string[]) {
    try {
        keys.forEach((key) => sessionStorage.removeItem(key));
    } catch {
    }
}