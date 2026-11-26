import { useState } from 'react';
import { useNavigate } from 'react-router';
import loginBackground from '../../assets/images/loginBackground.webp';
import { TextField } from '../../components/atoms/TextField';
import { Button } from '../../components/atoms/Button';
import { AuthLayout } from '../../components/templates/AuthLayout/index';
import { login } from '../../services/api/auth';
import { useAuth } from '../../context/AuthContext/index';
import { ROUTES } from '@/constants/routes';
import { validateLogin } from '@/utils/validateLogin';
import type { LoginFormErrors } from '@/utils/validateLogin';
import { hasErrors } from '@/utils/validation';

export function LoginPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [submitted, setSubmitted] = useState(false);
    const navigate = useNavigate();
    const { refreshUser } = useAuth();

    const validationErrors = validateLogin({ usernameOrEmail: username, password });
    const errors: LoginFormErrors = submitted ? validationErrors : {};

    async function handleLogin() {
        setSubmitted(true);

        if (hasErrors(validationErrors)) {
            return;
        }

        await login({ usernameOrEmail: username, password });
        await refreshUser();
        navigate(ROUTES.home);
    }

    return (
        <AuthLayout
            image={loginBackground}
            title="Complexion"
            subtitle="Your community based skin shade matcher"
        >
            <TextField
                data-cy="login-username"
                label="Username / Email"
                value={username}
                onChange={setUsername}
                error={errors.usernameOrEmail}
            />

            <div className="mt-5">
                <TextField
                    data-cy="login-password"
                    label="Password"
                    value={password}
                    onChange={setPassword}
                    type="password"
                    error={errors.password}
                />
            </div>

            <Button 
                data-cy="login-submit"
                onClick={handleLogin} 
                className="w-full mt-6"
            >
                Log in
            </Button>

            <p className="text-center text-sm text-[#8a7060] mt-4">
                Don't have an account?
            </p>

            <Button
                data-cy="login-create-account"
                variant="secondary"
                onClick={() => navigate(ROUTES.register)}
                className="w-full mt-2"
            >
                Create account
            </Button>
        </AuthLayout>
    );
}