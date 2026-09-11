interface TextFieldProps {
    label: string;
    value: string;
    onChange: (value: string) => void;
    type?: string;
}

export function TextField({ label, value, onChange, type = 'text' }: TextFieldProps) {
    return (
        <div>
            <label className="block mb-1.5 text-[#3b2a20]">{label}</label>
            <input
                type={type}
                value={value}
                onChange={(e) => onChange(e.target.value)}
                className="w-full rounded-lg px-4 py-2.5 bg-white border border-[#e8ddd4]"
            />
        </div>
    );
}