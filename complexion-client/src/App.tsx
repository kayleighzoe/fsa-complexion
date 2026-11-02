import { Routes, Route } from 'react-router';
import { AuthProvider } from './context/AuthContext/index';
import { ProtectedRoute } from './routes/ProtectedRoute';
import { LoginPage } from './pages/Login';
import { CreateAccountPage } from './pages/createAccount';
import { HomePage } from './pages/Home';
import { ProfilePage } from './pages/Profile';
import { GuidePage } from './pages/Guide';
import { SharePage } from './pages/Share';
import { EditRecommendationPage } from './pages/EditRecommendation';
import { EditProfilePage } from './pages/EditProfile';
import { NotFoundPage } from './pages/NotFound';
import { ROUTES } from './constants/routes';

function App() {
    return (
        <AuthProvider>
            <Routes>
                <Route path={ROUTES.login} element={<LoginPage />} />
                <Route path={ROUTES.register} element={<CreateAccountPage />} />
                <Route path={ROUTES.home} element={<HomePage />} />
                <Route path={ROUTES.guide} element={<GuidePage />} />
                <Route path={ROUTES.notFound} element={<NotFoundPage />} />

                <Route element={<ProtectedRoute />}>
                    <Route path={ROUTES.profile} element={<ProfilePage />} />
                    <Route path={ROUTES.editProfile} element={<EditProfilePage />} />
                    <Route path={ROUTES.share} element={<SharePage />} />
                    <Route path={ROUTES.editRecommendation} element={<EditRecommendationPage />} />
                </Route>
            </Routes>
        </AuthProvider>
    );
}

export default App;