import { useState } from 'react';
import { useNavigate } from 'react-router';
import { PageBackground } from './PageBackground';
import createAccountBackground from './assets/createAccountBackground.jpg';
import { TextField } from './TextField';
import { SelectField } from './SelectField';

export function CreateAccountPage() {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [skinShade, setSkinShade] = useState('');
    const [skinUndertone, setSkinUndertone] = useState('');
    const [email, setEmail] = useState('');
    const [hasOliveOvertone, setHasOliveOvertone] = useState(false);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

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
            <PageBackground image={createAccountBackground} />

            <div className="relative z-10 w-full max-w-6xl mx-auto px-10 flex items-center gap-12 page-fade">
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
                                <TextField label="Name" value={name} onChange={setName} />
                            </div>
                            <div className="w-1/2">
                                <TextField label="Surname" value={surname} onChange={setSurname} />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-5">
                            <div className="w-1/2">
                                <SelectField
                                    label="Skin shade"
                                    value={skinShade}
                                    onChange={setSkinShade}
                                    options={['Fair', 'Light', 'Medium', 'Tan', 'Deep']}
                                />
                            </div>
                            <div className="w-1/2">
                                <SelectField
                                    label="Skin undertone"
                                    value={skinUndertone}
                                    onChange={setSkinUndertone}
                                    options={['Warm', 'Cool', 'Neutral']}
                                />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-5">
                            <div className="w-1/2">
                                <TextField
                                    label="Email"
                                    value={email}
                                    onChange={setEmail}
                                    type="email"
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
                                <TextField label="Username" value={username} onChange={setUsername} />
                            </div>
                            <div className="w-1/2">
                                <TextField
                                    label="Password"
                                    value={password}
                                    onChange={setPassword}
                                    type="password"
                                />
                            </div>
                        </div>

                        <div className="flex gap-4 mt-8">
                            <button
                                onClick={() => navigate('/login')}
                                className="w-1/2 bg-[#72594a] text-white rounded-lg py-3 font-semibold"
                            >
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