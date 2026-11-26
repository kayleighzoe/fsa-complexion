import { Skeleton } from '../../atoms/Skeleton';

export function ProductCardSkeleton() {
    return (
        <div className="bg-white rounded-2xl px-8 py-6 mb-4">
            <div className="flex justify-between items-start">
                <div>
                    <Skeleton className="h-3 w-24" />
                    <Skeleton className="h-7 w-64 mt-3" />
                    <Skeleton className="h-4 w-32 mt-3" />
                    <Skeleton className="h-4 w-80 mt-4" />
                </div>
                <Skeleton className="h-8 w-24 rounded-full" />
            </div>

            <hr className="my-4 border-[#e8ddd4]" />

            <div className="flex justify-between">
                <Skeleton className="h-4 w-40" />
                <Skeleton className="h-4 w-12" />
            </div>

            <Skeleton className="h-5 w-48 mt-5" />
        </div>
    );
}