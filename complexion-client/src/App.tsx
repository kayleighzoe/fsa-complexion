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

function App() {
    return (
        <AuthProvider>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<CreateAccountPage />} />
                <Route path="/" element={<HomePage />} />
                <Route path="/guide" element={<GuidePage />} />
                <Route path="*" element={<NotFoundPage />} />

                <Route element={<ProtectedRoute />}>
                    <Route path="/profile" element={<ProfilePage />} />
                    <Route path="/profile/edit" element={<EditProfilePage />} />
                    <Route path="/share" element={<SharePage />} />
                    <Route path="/recommendations/:recommendationId/edit" element={<EditRecommendationPage />} />
                </Route>
            </Routes>
        </AuthProvider>
    );
}

export default App;