import type { ReactNode } from 'react';
import { PageBackground } from './PageBackground';

interface AuthLayoutProps {
    image: string;
    title: string;
    subtitle: string;
    children: ReactNode;
}

export function AuthLayout({ image, title, subtitle, children }: AuthLayoutProps) {
    return (
        <div className="min-h-screen flex items-center relative bg-[#645b50]">
            <PageBackground image={image} />

            <div className="relative z-10 w-full max-w-7xl mx-auto px-10 flex items-center gap-12 page-fade">
                <div className="w-2/5">
                    <h1
                        className="text-6xl font-['Cormorant_Garamond'] font-bold text-[#fbeee0]"
                        style={{
                            textShadow:
                                '0 0 12px rgba(251,238,224,0.85), 0 0 32px rgba(251,238,224,0.6), 0 0 64px rgba(251,238,224,0.4)',
                        }}
                    >
                        {title}
                    </h1>
                    <p className="text-xl text-[#fbeee0] mt-3">{subtitle}</p>
                </div>

                <div className="w-3/5">
                    <div className="bg-[#fbeee0] rounded-2xl p-10 ml-auto max-w-xl">{children}</div>
                </div>
            </div>
        </div>
    );
}