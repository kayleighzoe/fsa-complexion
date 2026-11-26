import { FieldSkeleton } from '@/components/molecules/FieldSkeleton';
import { FieldRowSkeleton } from '@/components/molecules/FieldRowSkeleton';
import { TextAreaSkeleton } from '@/components/molecules/TextAreaSkeleton';
import { ButtonRowSkeleton } from '@/components/molecules/ButtonRowSkeleton';
import { PageSkeletonLayout } from '@/components/organisms/PageSkeletonLayout';
import shareBackground from '@/assets/images/shareBackground.webp';

export function SharePageSkeleton() {
    return (
        <PageSkeletonLayout image={shareBackground} widthClass="max-w-4xl">
            <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                Share your recommendation
            </h1>
            <p className="text-xl text-center text-[#fbeee0] mt-3">
                Help others find their perfect match
            </p>

            <div className="bg-white rounded-2xl p-10 mt-10">
                <FieldRowSkeleton />
                <FieldRowSkeleton className="mt-5" />
                <FieldRowSkeleton className="mt-5" />
                <div className="flex gap-4 mt-5">
                    <div className="w-1/2">
                        <FieldSkeleton />
                    </div>
                    <div className="w-1/2" />
                </div>
                <TextAreaSkeleton className="mt-5" />
                <ButtonRowSkeleton />
            </div>
        </PageSkeletonLayout>
    );
}