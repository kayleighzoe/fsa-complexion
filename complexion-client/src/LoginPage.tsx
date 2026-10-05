import { useState } from 'react';
import { useNavigate } from 'react-router';
import loginBackground from './assets/loginBackground.jpg';
import { TextField } from './TextField';
import { Button } from './Button';
import { AuthLayout } from './AuthLayout';

export function LoginPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    function handleLogin() {
        console.log('Logging in as', username);
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

            <p className="text-right text-sm text-[#8a7060] mt-2">Forgot password?</p>

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