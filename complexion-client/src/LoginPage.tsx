import { useState } from 'react';
import { useNavigate } from 'react-router';
import loginBackground from './assets/loginBackground.jpg';
import { TextField } from './TextField';
import { Button } from './Button';
import { AuthLayout } from './AuthLayout';
import { getErrorMessage } from './api/errors';
import { login } from './api/auth';
import { useAuth } from './AuthContext';

export function LoginPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const [error, setError] = useState('');
    const { refreshUser } = useAuth();

    async function handleLogin() {
        setError('');
        try {
            await login({ usernameOrEmail: username, password });
            await refreshUser();
            navigate('/');
        } catch (err) {
            setError(getErrorMessage(err));
        }
    }

    return (
        <AuthLayout
            image={loginBackground}
            title="Complexion"
            subtitle="Your community based skin shade matcher"
        >
            <TextField label="Username / Email" value={username} onChange={setUsername} />

            <div className="mt-5">
                <TextField
                    label="Password"
                    value={password}
                    onChange={setPassword}
                    type="password"
                />
            </div>

            {error && <p className="text-sm text-red-700 mt-3">{error}</p>}

            <Button onClick={handleLogin} className="w-full mt-6">
                Log in
            </Button>

            <p className="text-center text-sm text-[#8a7060] mt-4">
                Don't have an account?
            </p>

            <Button
                variant="secondary"
                onClick={() => navigate('/register')}
                className="w-full mt-2"
            >
                Create account
            </Button>
        </AuthLayout>
    );
}