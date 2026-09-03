import { useState } from 'react';
import backgroundImage from './assets/loginBackground.jpg';

export function LoginPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    function handleLogin() {
        console.log('Logging in as', username);
        //call API here later
    }

    return (
        <div className="min-h-screen flex items-center relative bg-[#645b50]">
            <img
                src={backgroundImage}
                alt=""
                aria-hidden="true"
                className="absolute inset-0 w-full h-full object-cover opacity-10"
            />
            <div className="absolute inset-0 bg-gradient-to-r from-black/45 via-black/0 to-transparent" />


            <div className="relative z-10 w-full max-w-6xl mx-auto px-10 flex items-center gap-12">
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
                    <div className="bg-[#fbeee0] rounded-2xl p-10 max-w-sm ml-auto">
                        <label className="block mb-1.5 text-[#3b2a20]">Username / Email</label>
                        <input
                            type="text"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            className="w-full rounded-lg px-4 py-2.5 bg-white" />

                        <label className="block mb-1.5 mt-5 text-[#3b2a20]">Password</label>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="w-full rounded-lg px-4 py-2.5 bg-white" />

                        <p className="text-right text-sm text-[#8a7060] mt-2">Forgot password?</p>

                        <button
                            onClick={handleLogin}
                            className="w-full mt-6 bg-[#9db4c0] rounded-lg py-3 font-semibold"
                        >
                            Log in
                        </button>

                        <p className="text-center text-sm text-[#8a7060] mt-4">Don't have an account?</p>

                        <button className="w-full mt-2 bg-[#72594a] text-white rounded-lg py-3 font-semibold">
                            Create account
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}