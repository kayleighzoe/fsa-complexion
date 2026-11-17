import { Skeleton } from '@/components/atoms/Skeleton';
import { PageSkeletonLayout } from '@/components/organisms/PageSkeletonLayout';
import profileBackground from '@/assets/images/profileBackground.webp';

export function ProfilePageSkeleton() {
    return (
        <PageSkeletonLayout image={profileBackground} widthClass="max-w-6xl" centered>
            <div className="bg-white rounded-2xl p-10">
                <div className="flex justify-between items-start">
                    <h1 className="text-4xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                        My profile
                    </h1>
                    <div className="flex gap-3">
                        <Skeleton className="h-10 w-20 rounded-full" />
                        <Skeleton className="h-10 w-24 rounded-full" />
                    </div>
                </div>

                <div className="grid grid-cols-3 gap-y-6 mt-8">
                    {[1, 2, 3, 4, 5].map((n) => (
                        <div key={n}>
                            <Skeleton className="h-3 w-16" />
                            <Skeleton className="h-5 w-32 mt-2" />
                        </div>
                    ))}
                </div>
            </div>

            <div className="bg-white rounded-2xl p-10 mt-8">
                <div className="flex justify-between items-center">
                    <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                        My perfect shade matches
                    </h2>
                    <Skeleton className="h-10 w-56 rounded-full" />
                </div>

                {[1, 2].map((n) => (
                    <div
                        key={n}
                        className="border border-[#e8ddd4] rounded-xl p-6 mt-6 flex justify-between items-center"
                    >
                        <div>
                            <Skeleton className="h-6 w-64" />
                            <Skeleton className="h-4 w-80 mt-3" />
                            <Skeleton className="h-3 w-24 mt-4" />
                        </div>
                        <div className="flex gap-2">
                            <Skeleton className="h-10 w-10 rounded-full" />
                            <Skeleton className="h-10 w-10 rounded-full" />
                        </div>
                    </div>
                ))}
            </div>
        </PageSkeletonLayout>
    );
}