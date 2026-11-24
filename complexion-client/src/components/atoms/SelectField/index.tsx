import { useId } from 'react';

interface SelectFieldProps {
    label: string;
    value: string;
    onChange: (value: string) => void;
    options: string[];
    placeholder?: string;
    error?: string;
}

export function SelectField({
    label,
    value,
    onChange,
    options,
    placeholder = 'Choose…',
    error,
}: SelectFieldProps) {
    const errorId = useId();

    return (
        <div>
            <label className="block mb-1.5 text-[#3b2a20]">{label}</label>
            <select
                value={value}
                onChange={(e) => onChange(e.target.value)}
                aria-invalid={error ? true : undefined}
                aria-describedby={error ? errorId : undefined}
                className={`w-full rounded-lg px-4 py-2.5 bg-white border ${
                    error ? 'border-red-600' : 'border-[#e8ddd4]'
                }`}
            >
                <option value="">{placeholder}</option>
                {options.map((option) => (
                    <option key={option} value={option}>
                        {option}
                    </option>
                ))}
            </select>

            {error && (
                <p id={errorId} className="mt-1.5 text-sm text-red-600">
                    {error}
                </p>
            )}
        </div>
    );
}