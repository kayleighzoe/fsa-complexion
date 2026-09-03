import { useState } from 'react';
import createAccountBackground from './assets/createAccountBackground.jpg';

export function CreateAccountPage() {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [skinShade, setSkinShade] = useState('');
    const [skinUndertone, setSkinUndertone] = useState('');
    const [email, setEmail] = useState('');
    const [hasOliveOvertone, setHasOliveOvertone] = useState(false);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    function handleSignUp() {
        console.log({
            name,
            surname,
            skinShade,
            skinUndertone,
            email,
            hasOliveOvertone,
            username,
            password,
        });
        // call API here later
    }

    return (
        <div className="min-h-screen flex items-center relative bg-[#645b50]">
            <img
                src={createAccountBackground}
                alt=""
                aria-hidden="true"
                className="absolute inset-0 w-full h-full object-cover opacity-10"
            />
            <div className="absolute inset-0 bg-gradient-to-r from-black/45 via-black/0 to-transparent" />

            <div className="relative z-10 w-full max-w-6xl mx-auto px-10 flex items-center gap-12">
                <div className="w-2/5">
                    <h1
                        className="text-6xl font-['Cormorant_Garamond'] font-bold text-[#fbeee0]"
                        style={{
                            textShadow:
                                '0 0 12px rgba(251,238,224,0.85), 0 0 32px rgba(251,238,224,0.6), 0 0 64px rgba(251,238,224,0.4)',
                        }}
                    >
                        Create account
                    </h1>
                    <p className="text-xl text-[#fbeee0] mt-3">
                        Join the Complexion community
                    </p>
                </div>

                <div className="w-3/5">
                    <div className="bg-[#fbeee0] rounded-2xl p-10 ml-auto">
                        <div className="flex gap-4">
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Name</label>
                                <input
                                    type="text"
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Surname</label>
                                <input
                                    type="text"
                                    value={surname}
                                    onChange={(e) => setSurname(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-5">
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Skin shade</label>
                                <input
                                    type="text"
                                    value={skinShade}
                                    onChange={(e) => setSkinShade(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Skin undertone</label>
                                <input
                                    type="text"
                                    value={skinUndertone}
                                    onChange={(e) => setSkinUndertone(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-5">
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Email</label>
                                <input
                                    type="email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                            <div className="w-1/2 flex items-end pb-2.5">
                                <label className="flex items-center gap-2 text-[#3b2a20]">
                                    <input
                                        type="checkbox"
                                        checked={hasOliveOvertone}
                                        onChange={(e) => setHasOliveOvertone(e.target.checked)}
                                    />
                                    Olive overtone
                                </label>
                            </div>
                        </div>

                        <div className="flex gap-4 mt-5">
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Username</label>
                                <input
                                    type="text"
                                    value={username}
                                    onChange={(e) => setUsername(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                            <div className="w-1/2">
                                <label className="block mb-1.5 text-[#3b2a20]">Password</label>
                                <input
                                    type="password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    className="w-full rounded-lg px-4 py-2.5 bg-white"
                                />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-8">
                            <button className="w-1/2 bg-[#72594a] text-white rounded-lg py-3 font-semibold">
                                Return to login
                            </button>
                            <button
                                onClick={handleSignUp}
                                className="w-1/2 bg-[#9db4c0] rounded-lg py-3 font-semibold"
                            >
                                Sign up
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}