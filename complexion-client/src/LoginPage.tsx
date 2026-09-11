import { useState } from 'react';
import { useNavigate } from 'react-router';
import { PageBackground } from './PageBackground';
import loginBackground from './assets/loginBackground.jpg';
import { TextField } from './TextField';

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

                        <button
                            onClick={handleLogin}
                            className="w-full mt-6 bg-[#9db4c0] rounded-lg py-3 font-semibold"
                        >
                            Log in
                        </button>

                        <p className="text-center text-sm text-[#8a7060] mt-4">
                            Don't have an account?
                        </p>

                        <button
                            onClick={() => navigate('/register')}
                            className="w-full mt-2 rounded-lg py-3 font-semibold text-white cursor-pointer bg-gradient-to-b from-[#8a6b58] to-[#5f4638] shadow-md hover:shadow-lg hover:-translate-y-0.5 active:translate-y-0 transition-all"
                        >
                            Create account
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}