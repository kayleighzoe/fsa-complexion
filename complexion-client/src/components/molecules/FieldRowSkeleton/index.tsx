import { FieldSkeleton } from '@/components/molecules/FieldSkeleton';

interface FieldRowSkeletonProps {
    className?: string;
}

export function FieldRowSkeleton({ className = '' }: FieldRowSkeletonProps) {
    return (
        <div className={`flex gap-4 ${className}`}>
            <div className="w-1/2">
                <FieldSkeleton />
            </div>
            <div className="w-1/2">
                <FieldSkeleton />
            </div>
        </div>
    );
}