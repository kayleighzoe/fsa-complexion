import type { ReactNode } from 'react';

interface ButtonProps {
    children: ReactNode;
    onClick?: () => void;
    variant?: 'primary' | 'secondary' | 'pale';
    rounded?: 'lg' | "full",
    size?: 'default' | 'icon',
    className?: string;
}

export function Button({
    children,
    onClick,
    variant = 'primary',
    rounded = 'lg',
    size = 'default',
    className = '',
}: ButtonProps) {
    const colours = {
        primary: 'bg-gradient-to-b from-[#adc2cc] to-[#8ba5b2] text-[#20343c]',
        secondary: 'bg-gradient-to-b from-[#8a6b58] to-[#5f4638] text-white',
        pale: 'bg-gradient-to-b from-[#f7e4d4] to-[#eccdb4] text-[#3b2a20]',
    }[variant];

    const shape = rounded === 'full' ? 'rounded-full' : 'rounded-lg';
    const sizing = size === 'icon'
        ? 'w-10 h-10 flex items-center justify-center'
        : 'py-3 px-6';

    return (
        <button
            onClick={onClick}
            className={`${shape} ${sizing} font-semibold cursor-pointer shadow-md hover:scale-105 active:scale-95 transition-transform ${colours} ${className}`}
        >
            {children}
        </button>
    );
}