import { Skeleton } from '@/components/atoms/Skeleton';
import { FieldSkeleton } from '@/components/molecules/FieldSkeleton';
import { FieldRowSkeleton } from '@/components/molecules/FieldRowSkeleton';
import { ButtonRowSkeleton } from '@/components/molecules/ButtonRowSkeleton';

export function EditProfileFormSkeleton() {
    return (
        <>
            <FieldRowSkeleton />
            <div className="mt-5">
                <FieldSkeleton />
            </div>
            <FieldRowSkeleton className="mt-5" />
            <Skeleton className="h-4 w-32 mt-5" />
            <ButtonRowSkeleton />
        </>
    );
}