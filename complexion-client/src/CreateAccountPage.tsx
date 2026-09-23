import { useState } from 'react';
import { useNavigate } from 'react-router';
import createAccountBackground from './assets/createAccountBackground.jpg';
import { TextField } from './TextField';
import { SelectField } from './SelectField';
import { Button } from './Button';
import { AuthLayout } from './AuthLayout';
import { Link } from 'react-router'

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
        <AuthLayout
            image={createAccountBackground}
            title="Create account"
            subtitle="Join the Complexion community"
        >
            <h2 className="text-sm font-semibold uppercase tracking-wide text-[#8a7060]">
                About you
            </h2>
            <div className="flex gap-4 mt-3">
                <div className="w-1/2">
                    <TextField
                        label="Name"
                        value={name}
                        onChange={setName}
                    />
                </div>
                <div className="w-1/2">
                    <TextField
                        label="Surname"
                        value={surname}
                        onChange={setSurname}
                    />
                </div>
            </div>
            <div className="mt-4">
                <TextField
                    label="Email"
                    value={email}
                    onChange={setEmail}
                    type="email"
                />
            </div>

            <hr className="my-7 border-[#e8d5c4]" />

            <div className="flex items-baseline justify-between">
                <h2 className="text-sm font-semibold uppercase tracking-wide text-[#8a7060]">
                    Your skin profile
                </h2>
                <Link
                    to="/guide"
                    className="text-sm text-[#8a7060] underline hover:text-[#3b2a20]"
                >
                    Not sure? Read the guide
                </Link>
            </div>
            <div className="flex gap-4 mt-3">
                <div className="w-1/2">
                    <SelectField
                        label="Skin shade"
                        value={skinShade}
                        onChange={setSkinShade}
                        options={['Fair', 'Light', 'Medium', 'Tan', 'Deep', 'Very deep']}
                    />
                </div>
                <div className="w-1/2">
                    <SelectField
                        label="Skin undertone"
                        value={skinUndertone}
                        onChange={setSkinUndertone}
                        options={['Cool', 'Cool-neutral', 'True neutral', 'Warm-neutral', 'Warm']}
                    />
                </div>
            </div>
            <label className="flex items-center gap-2 text-[#3b2a20] mt-4">
                <input
                    type="checkbox"
                    checked={hasOliveOvertone}
                    onChange={(e) => setHasOliveOvertone(e.target.checked)}
                    className="accent-[#8ba5b2]"
                />
                Olive overtone
            </label>

            <hr className="my-6 border-[#e8d5c4]" />

            <h2 className="text-sm font-semibold uppercase tracking-wide text-[#8a7060]">
                Login details
            </h2>
            <div className="flex gap-4 mt-3">
                <div className="w-1/2">
                    <TextField
                        label="Username"
                        value={username}
                        onChange={setUsername}
                    />
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
                <Button
                    onClick={handleSignUp}
                    className="w-1/2"
                >
                    Sign up
                </Button>
                <Button
                    variant="secondary"
                    onClick={() => navigate('/login')}
                    className="w-1/2"
                >
                    Return to login
                </Button>
            </div>
        </AuthLayout>
    );
}