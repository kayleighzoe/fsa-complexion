import { Routes, Route } from 'react-router';
import { AuthProvider } from './AuthContext';
import { ProtectedRoute } from './ProtectedRoute';
import { LoginPage } from './LoginPage';
import { CreateAccountPage } from './CreateAccountPage';
import { HomePage } from './HomePage';
import { ProfilePage } from './ProfilePage';
import { GuidePage } from './GuidePage';
import { SharePage } from './SharePage';
import { EditRecommendationPage } from './EditRecommendationPage';
import { EditProfilePage } from './EditProfilePage';

function App() {
    return (
        <AuthProvider>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<CreateAccountPage />} />
                <Route path="/" element={<HomePage />} />
                <Route path="/guide" element={<GuidePage />} />

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