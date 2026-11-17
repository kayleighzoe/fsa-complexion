import { Skeleton } from '@/components/atoms/Skeleton';

export function ButtonRowSkeleton() {
    return (
        <div className="flex gap-4 mt-8">
            <Skeleton className="h-11 w-1/2 rounded-lg" />
            <Skeleton className="h-11 w-1/2 rounded-lg" />
        </div>
    );
}