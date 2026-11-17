import { Skeleton } from '@/components/atoms/Skeleton';
import { TextAreaSkeleton } from '@/components/molecules/TextAreaSkeleton';
import { ButtonRowSkeleton } from '@/components/molecules/ButtonRowSkeleton';

export function EditRecommendationFormSkeleton() {
    return (
        <>
            <Skeleton className="h-8 w-80" />
            <Skeleton className="h-4 w-64 mt-3" />
            <TextAreaSkeleton className="mt-6" />
            <ButtonRowSkeleton />
        </>
    );
}