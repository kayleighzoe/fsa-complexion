import { Skeleton } from '@/components/atoms/Skeleton';

interface TextAreaSkeletonProps {
    className?: string;
}

export function TextAreaSkeleton({ className = '' }: TextAreaSkeletonProps) {
    return (
        <div className={className}>
            <Skeleton className="h-4 w-32" />
            <Skeleton className="h-28 w-full mt-2 rounded-lg" />
        </div>
    );
}