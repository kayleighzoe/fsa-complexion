import axios from 'axios';

export function getErrorMessage(error: unknown): string {
    if (axios.isAxiosError(error)) {
        const errors = error.response?.data?.errors as Record<string, string[]> | undefined;
        if (errors) {
            return Object.values(errors).flat().join(' ');
        }
        return error.response?.data?.detail ?? 'Something went wrong. Please try again.';
    }
    return 'Something went wrong. Please try again.';
}