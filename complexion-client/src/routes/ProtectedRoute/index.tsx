import { Navigate, Outlet, matchPath, useLocation } from 'react-router';
import { useAuth } from '../../context/AuthContext/index';
import { ROUTES } from '@/constants/routes';
import { ProfilePageSkeleton } from '@/components/organisms/ProfilePageSkeleton';
import { EditProfilePageSkeleton } from '@/components/organisms/EditProfilePageSkeleton';
import { SharePageSkeleton } from '@/components/organisms/SharePageSkeleton';
import { EditRecommendationPageSkeleton } from '@/components/organisms/EditRecommendationPageSkeleton';

function getPageSkeleton(pathname: string) {
    if (matchPath(ROUTES.editProfile, pathname)) return <EditProfilePageSkeleton />;
    if (matchPath(ROUTES.share, pathname)) return <SharePageSkeleton />;
    if (matchPath(ROUTES.editRecommendation, pathname)) return <EditRecommendationPageSkeleton />;

    return <ProfilePageSkeleton />;
}

export function ProtectedRoute() {
    const { user, loading } = useAuth();
    const { pathname } = useLocation();

    if (loading) {
        return getPageSkeleton(pathname);
    }

    if (!user) {
        return <Navigate to={ROUTES.login} replace />;
    }

    return <Outlet />;
}