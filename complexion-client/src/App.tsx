import { Routes, Route } from 'react-router';
import { LoginPage } from './LoginPage';
import { CreateAccountPage } from './CreateAccountPage';
import { HomePage } from './HomePage';
import { ProfilePage } from './ProfilePage';
import { GuidePage } from './GuidePage';
import { SharePage } from './SharePage';

function App() {
    return (
        <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<CreateAccountPage />} />
            <Route path="/" element={<HomePage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/guide" element={<GuidePage />} />
            <Route path="/share" element={<SharePage />} />
        </Routes>
    );
}

export default App;