import { useState } from 'react';
import { useNavigate } from 'react-router';
import { PageBackground } from './PageBackground';
import loginBackground from './assets/loginBackground.jpg';
import { TextField } from './TextField';
import { Button } from './Button';

export function LoginPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    function handleLogin() {
        console.log('Logging in as', username);
        //call API here later
    }

    return (
        <div className="min-h-screen flex items-center relative bg-[#645b50]">
            <PageBackground image={loginBackground} />

            <div className="relative z-10 w-full max-w-6xl mx-auto px-10 flex items-center gap-12 page-fade">
                <div className="w-1/2">
                    <h1
                        className="text-6xl font-['Cormorant_Garamond'] font-bold text-[#fbeee0]"
                        style={{
                            textShadow:
                                '0 0 12px rgba(251,238,224,0.85), 0 0 32px rgba(251,238,224,0.6), 0 0 64px rgba(251,238,224,0.4)',
                        }}
                    >
                        Complexion
                    </h1>
                    <p className="text-xl text-[#fbeee0] mt-3">
                        Your community based skin shade matcher
                    </p>
                </div>

                <div className="w-1/2">
                    <div className="bg-[#fbeee0] rounded-2xl p-10 max-w-6xl ml-auto">
                        <TextField
                            label="Username / Email"
                            value={username}
                            onChange={setUsername}
                        />

                        <div className="mt-5">
                            <TextField
                                label="Password"
                                value={password}
                                onChange={setPassword}
                                type="password"
                            />
                        </div>

                        <p className="text-right text-sm text-[#8a7060] mt-2">Forgot password?</p>

                        <Button
                            onClick={handleLogin}
                            className="w-full mt-2"
                        >
                            Login
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
                    </div>
                </div>
            </div>
        </div>
    );
}