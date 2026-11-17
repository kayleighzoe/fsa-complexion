import type { ReactNode } from 'react';
import { PageBackground } from '@/components/organisms/PageBackground';
import { NavBar } from '@/components/organisms/NavBar';

interface PageSkeletonLayoutProps {
    image: string;
    widthClass: string;
    centered?: boolean;
    children: ReactNode;
}

export function PageSkeletonLayout({ image, widthClass, centered = false, children }: PageSkeletonLayoutProps) {
    return (
        <div role="status" aria-label="Loading page" className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={image} />
            <NavBar />
            <div className={`relative z-10 flex-1 ${centered ? 'flex items-center' : ''}`}>
                <div className={`w-full mx-auto px-8 py-12 ${widthClass}`}>{children}</div>
            </div>
        </div>
    );
}