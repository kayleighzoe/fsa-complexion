import { Navigate, Outlet } from 'react-router';
import { useAuth } from '../../context/AuthContext/index';

export function ProtectedRoute() {
    const { user, loading } = useAuth();

    if (loading) {
        return <p className="p-8 text-[#8a7060]">Loading...</p>;
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}