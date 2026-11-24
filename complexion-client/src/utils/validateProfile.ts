import { validateEmail, validateRequiredText } from '@/utils/validation';

export interface ProfileFormValues {
    firstName: string;
    surname: string;
    email: string;
    shadeName: string;
    undertoneName: string;
}

export type ProfileFormErrors = Partial<Record<keyof ProfileFormValues, string>>;

const MAX_LENGTH = 50;

export function validateProfile(values: ProfileFormValues): ProfileFormErrors {
    return {
        firstName: validateRequiredText(values.firstName, 'First name', MAX_LENGTH),
        surname: validateRequiredText(values.surname, 'Surname', MAX_LENGTH),
        email: validateEmail(values.email, MAX_LENGTH),
        shadeName: values.shadeName === '' ? 'Please choose a skin shade.' : undefined,
        undertoneName: values.undertoneName === '' ? 'Please choose a skin undertone.' : undefined,
    };
}