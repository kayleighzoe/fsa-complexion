export function hasErrors(errors: Partial<Record<string, string | undefined>>): boolean {
    return Object.values(errors).some(Boolean);
}

export function validateRequiredText(value: string, label: string, maxLength: number): string | undefined {
    if (value.trim() === '') {
        return `${label} is required.`;
    }

    if (value.length > maxLength) {
        return `${label} must be ${maxLength} characters or fewer.`;
    }

    return undefined;
}

function hasValidEmailShape(value: string): boolean {
    const atIndex = value.indexOf('@');

    return atIndex > 0 && atIndex < value.length - 1 && atIndex === value.lastIndexOf('@');
}

export function validateEmail(value: string, maxLength: number): string | undefined {
    if (value.trim() === '') {
        return 'Email is required.';
    }

    if (!hasValidEmailShape(value)) {
        return 'Email must be a valid email address.';
    }

    if (value.length > maxLength) {
        return `Email must be ${maxLength} characters or fewer.`;
    }

    return undefined;
}

export function validateOptionalText(value: string, label: string, maxLength: number): string | undefined {
    if (value.length > maxLength) {
        return `${label} must be ${maxLength} characters or fewer.`;
    }

    return undefined;
}