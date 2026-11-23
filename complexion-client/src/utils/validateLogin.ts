import { validateRequiredText } from '@/utils/validation';

export interface LoginFormValues {
    usernameOrEmail: string;
    password: string;
}

export type LoginFormErrors = Partial<Record<keyof LoginFormValues, string>>;

const MAX_USERNAME_OR_EMAIL_LENGTH = 50;
const MAX_PASSWORD_LENGTH = 64;

export function validateLogin(values: LoginFormValues): LoginFormErrors {
    return {
        usernameOrEmail: validateRequiredText(values.usernameOrEmail, 'Username or email', MAX_USERNAME_OR_EMAIL_LENGTH),
        password: validateRequiredText(values.password, 'Password', MAX_PASSWORD_LENGTH),
    };
}