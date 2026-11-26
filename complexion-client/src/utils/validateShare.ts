import { validateRequiredText } from '@/utils/validation';

export interface ShareFormValues {
    category: string;
    priceTier: string;
    brandName: string;
    productName: string;
    shadeName: string;
    matchedShade: string;
    matchedUndertone: string;
}

export type ShareFormErrors = Partial<Record<keyof ShareFormValues, string>>;

const MAX_PRODUCT_TEXT_LENGTH = 50;

export function validateShare(values: ShareFormValues): ShareFormErrors {
    return {
        category: values.category === '' ? 'Please choose a product category.' : undefined,
        priceTier: values.priceTier === '' ? 'Please choose a price tier.' : undefined,
        brandName: validateRequiredText(values.brandName, 'Brand name', MAX_PRODUCT_TEXT_LENGTH),
        productName: validateRequiredText(values.productName, 'Product name', MAX_PRODUCT_TEXT_LENGTH),
        shadeName: validateRequiredText(values.shadeName, 'Shade name', MAX_PRODUCT_TEXT_LENGTH),
        matchedShade: values.matchedShade === '' ? 'Please choose a skin shade.' : undefined,
        matchedUndertone: values.matchedUndertone === '' ? 'Please choose a skin undertone.' : undefined,
    };
}