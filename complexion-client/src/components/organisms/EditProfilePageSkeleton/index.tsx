import { Skeleton } from '@/components/atoms/Skeleton';
import { FieldSkeleton } from '@/components/molecules/FieldSkeleton';
import { FieldRowSkeleton } from '@/components/molecules/FieldRowSkeleton';
import { ButtonRowSkeleton } from '@/components/molecules/ButtonRowSkeleton';
import { PageSkeletonLayout } from '@/components/organisms/PageSkeletonLayout';
import profileBackground from '@/assets/images/profileBackground.webp';

export function EditProfilePageSkeleton() {
    return (
        <PageSkeletonLayout image={profileBackground} widthClass="max-w-3xl">
            <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                Edit profile
            </h1>

            <div className="bg-white rounded-2xl p-10 mt-10">
                <FieldRowSkeleton />
                <div className="mt-5">
                    <FieldSkeleton />
                </div>
                <FieldRowSkeleton className="mt-5" />
                <Skeleton className="h-4 w-32 mt-5" />
                <ButtonRowSkeleton />
            </div>
        </PageSkeletonLayout>
    );
}