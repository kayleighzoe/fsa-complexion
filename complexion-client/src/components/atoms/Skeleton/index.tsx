interface SkeletonProps {
    className?: string;
}

export function Skeleton({ className = '' }: SkeletonProps) {
    return <div aria-hidden="true" className={`animate-pulse rounded bg-[#e8ddd4] ${className}`} />;
}