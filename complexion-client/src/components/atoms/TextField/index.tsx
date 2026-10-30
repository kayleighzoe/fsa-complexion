import { useId, useState } from 'react';
import type { ReactNode } from 'react';
import { Eye, EyeOff, Info } from 'lucide-react';

interface TextFieldProps {
    label: string;
    value: string;
    onChange: (value: string) => void;
    type?: string;
    hint?: ReactNode;
}

export function TextField({ label, value, onChange, type = 'text', hint }: TextFieldProps) {
    const hintId = useId();
    const [showPassword, setShowPassword] = useState(false);

    const isPassword = type === 'password';
    const inputType = isPassword && showPassword ? 'text' : type;

    return (
        <div>
            <div className="flex items-center gap-1.5 mb-1.5">
                <label className="text-[#3b2a20]">{label}</label>
                {hint && (
                    <span className="relative group">
                        <button
                            type="button"
                            aria-label={`${label} requirements`}
                            aria-describedby={hintId}
                            className="text-[#8a7060] cursor-help"
                        >
                            <Info size={16} aria-hidden="true" />
                        </button>
                        <span
                            id={hintId}
                            role="tooltip"
                            className="hidden group-hover:block group-focus-within:block absolute left-0 top-full mt-1 z-30 w-64 rounded-lg bg-[#3b2a20] text-[#fbeee0] text-sm p-3 shadow-lg"
                        >
                            {hint}
                        </span>
                    </span>
                )}
            </div>

            <div className="relative">
                <input
                    type={inputType}
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className={`w-full rounded-lg px-4 py-2.5 bg-white border border-[#e8ddd4] outline-none focus:ring-2 focus:ring-[#8a7060]/50 focus:border-[#8a7060] ${isPassword ? 'pr-11' : ''}`}
                />
                {isPassword && (
                    <button
                        type="button"
                        onClick={() => setShowPassword((current) => !current)}
                        aria-label="Show password"
                        aria-pressed={showPassword}
                        className="absolute inset-y-0 right-0 flex items-center px-3 text-[#8a7060] cursor-pointer"
                    >
                        {showPassword ? <EyeOff size={18} aria-hidden="true" /> : <Eye size={18} aria-hidden="true" />}
                    </button>
                )}
            </div>
        </div>
    );
}