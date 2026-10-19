import axios from 'axios';

export function getErrorMessage(error: unknown): string {
    if (axios.isAxiosError(error)) {
        return error.response?.data?.detail ?? 'Something went wrong. Please try again.';
    }
    return 'Something went wrong. Please try again.';
}