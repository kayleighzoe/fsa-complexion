import { validateEmail, validateRequiredText } from '@/utils/validation';

export interface RegisterFormValues {
    firstName: string;
    surname: string;
    email: string;
    username: string;
    password: string;
    shadeName: string;
    undertoneName: string;
}

export type RegisterFormErrors = Partial<Record<keyof RegisterFormValues, string>>;

const MAX_NAME_LENGTH = 50;
const MIN_USERNAME_LENGTH = 5;
const MIN_PASSWORD_LENGTH = 12;
const MAX_PASSWORD_LENGTH = 64;

function isAsciiUppercase(character: string): boolean {
    return character >= 'A' && character <= 'Z';
}

function isAsciiLowercase(character: string): boolean {
    return character >= 'a' && character <= 'z';
}

function isAsciiLetter(character: string): boolean {
    return isAsciiUppercase(character) || isAsciiLowercase(character);
}

function isAsciiDigit(character: string): boolean {
    return character >= '0' && character <= '9';
}

function isAsciiLetterOrDigit(character: string): boolean {
    return isAsciiLetter(character) || isAsciiDigit(character);
}

function validateUsername(value: string): string | undefined {
    if (value.trim() === '') {
        return 'Username is required.';
    }

    if (value.length < MIN_USERNAME_LENGTH) {
        return `Username must be at least ${MIN_USERNAME_LENGTH} characters.`;
    }

    if (value.length > MAX_NAME_LENGTH) {
        return `Username must be ${MAX_NAME_LENGTH} characters or fewer.`;
    }

    if (!isAsciiLetter(value[0])) {
        return 'Username must start with a letter.';
    }

    if (![...value].every(isAsciiLetterOrDigit)) {
        return 'Username can only contain letters and numbers.';
    }

    return undefined;
}

function validatePassword(value: string): string | undefined {
    if (value.trim() === '') {
        return 'Password is required.';
    }

    if (value.length < MIN_PASSWORD_LENGTH) {
        return `Password must be at least ${MIN_PASSWORD_LENGTH} characters.`;
    }

    if (value.length > MAX_PASSWORD_LENGTH) {
        return `Password must be ${MAX_PASSWORD_LENGTH} characters or fewer.`;
    }

    const characters = [...value];

    if (!characters.some(isAsciiUppercase)) {
        return 'Password must contain an uppercase letter.';
    }

    if (!characters.some(isAsciiLowercase)) {
        return 'Password must contain a lowercase letter.';
    }

    if (!characters.some(isAsciiDigit)) {
        return 'Password must contain a number.';
    }

    if (!characters.some((character) => !isAsciiLetterOrDigit(character))) {
        return 'Password must contain a special character.';
    }

    return undefined;
}

export function validateRegister(values: RegisterFormValues): RegisterFormErrors {
    return {
        firstName: validateRequiredText(values.firstName, 'First name', MAX_NAME_LENGTH),
        surname: validateRequiredText(values.surname, 'Surname', MAX_NAME_LENGTH),
        email: validateEmail(values.email, MAX_NAME_LENGTH),
        username: validateUsername(values.username),
        password: validatePassword(values.password),
        shadeName: values.shadeName === '' ? 'Please choose a skin shade.' : undefined,
        undertoneName: values.undertoneName === '' ? 'Please choose a skin undertone.' : undefined,
    };
}