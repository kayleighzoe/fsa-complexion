interface SelectFieldProps {
    label: string;
    value: string;
    onChange: (value: string) => void;
    options: string[];
    placeholder?: string;
}

export function SelectField({
    label,
    value,
    onChange,
    options,
    placeholder = 'Choose…',
}: SelectFieldProps) {
    return (
        <div>
            <label className="block mb-1.5 text-[#3b2a20]">{label}</label>
            <select
                value={value}
                onChange={(e) => onChange(e.target.value)}
                className="w-full rounded-lg px-4 py-2.5 bg-white border border-[#e8ddd4]"
            >
                <option value="">{placeholder}</option>
                {options.map((option) => (
                    <option key={option} value={option}>
                        {option}
                    </option>
                ))}
            </select>
        </div>
    );
}