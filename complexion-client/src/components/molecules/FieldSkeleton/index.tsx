import { Skeleton } from '@/components/atoms/Skeleton';

export function FieldSkeleton() {
    return (
        <div>
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-11 w-full mt-2 rounded-lg" />
        </div>
    );
}