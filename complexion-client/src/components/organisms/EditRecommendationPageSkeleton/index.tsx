import { Skeleton } from '@/components/atoms/Skeleton';
import { TextAreaSkeleton } from '@/components/molecules/TextAreaSkeleton';
import { ButtonRowSkeleton } from '@/components/molecules/ButtonRowSkeleton';
import { PageSkeletonLayout } from '@/components/organisms/PageSkeletonLayout';
import profileBackground from '@/assets/images/profileBackground.webp';

export function EditRecommendationPageSkeleton() {
    return (
        <PageSkeletonLayout image={profileBackground} widthClass="max-w-4xl">
            <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                Edit your recommendation
            </h1>

            <div className="bg-white rounded-2xl p-10 mt-10">
                <Skeleton className="h-8 w-80" />
                <Skeleton className="h-4 w-64 mt-3" />
                <TextAreaSkeleton className="mt-6" />
                <ButtonRowSkeleton />
            </div>
        </PageSkeletonLayout>
    );
}