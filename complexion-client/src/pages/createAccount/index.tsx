import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { ROUTES } from '../../constants/routes'
import createAccountBackground from '../../assets/images/createAccountBackground.webp';
import { TextField } from '../../components/atoms/TextField';
import { SelectField } from '../../components/atoms/SelectField';
import { Button } from '../../components/atoms/Button';
import { AuthLayout } from '../../components/templates/AuthLayout/index';
import { Link } from 'react-router';
import type { SkinShade, SkinUndertone } from '../../types';
import { register, login } from '../../services/api/auth';
import { useAuth } from '../../context/AuthContext/index';
import { getSkinShades, getSkinUndertones } from '../../services/api/skin';
import { passwordRequirements } from '../../utils/passwordRequirements';
import { clearSessionStorageKeys, useSessionStorageState } from '@/hooks/useSessionStorageState';
import { validateRegister } from '@/utils/validateRegister';
import type { RegisterFormErrors } from '@/utils/validateRegister';
import { hasErrors } from '@/utils/validation';

const DRAFT_KEYS = {
    name: 'createAccount.name',
    surname: 'createAccount.surname',
    email: 'createAccount.email',
    skinShade: 'createAccount.skinShade',
    skinUndertone: 'createAccount.skinUndertone',
    hasOliveOvertone: 'createAccount.hasOliveOvertone',
    username: 'createAccount.username',
} as const;

export function CreateAccountPage() {
    const [name, setName] = useSessionStorageState(DRAFT_KEYS.name, '');
    const [surname, setSurname] = useSessionStorageState(DRAFT_KEYS.surname, '');
    const [skinShade, setSkinShade] = useSessionStorageState(DRAFT_KEYS.skinShade, '');
    const [skinUndertone, setSkinUndertone] = useSessionStorageState(DRAFT_KEYS.skinUndertone, '');
    const [email, setEmail] = useSessionStorageState(DRAFT_KEYS.email, '');
    const [hasOliveOvertone, setHasOliveOvertone] = useSessionStorageState(DRAFT_KEYS.hasOliveOvertone, false);
    const [username, setUsername] = useSessionStorageState(DRAFT_KEYS.username, '');
    const [password, setPassword] = useState('');
    const [submitted, setSubmitted] = useState(false);
    const navigate = useNavigate();
    const [shades, setShades] = useState<SkinShade[]>([]);
    const [undertones, setUndertones] = useState<SkinUndertone[]>([]);
    const { refreshUser } = useAuth();

    const validationErrors = validateRegister({
        firstName: name,
        surname,
        email,
        username,
        password,
        shadeName: skinShade,
        undertoneName: skinUndertone,
    });
    const errors: RegisterFormErrors = submitted ? validationErrors : {};

    useEffect(() => {
        const fetchShades = async () => {
            try {
                setShades(await getSkinShades());
            } catch (error) {
                console.error('Could not load skin shades', error);
            }
        };

        fetchShades();
    }, []);

    useEffect(() => {
        const fetchUndertones = async () => {
            try {
                setUndertones(await getSkinUndertones());
            } catch (error) {
                console.error('Could not load skin undertones', error);
            }
        };

        fetchUndertones();
    }, []);

    async function handleSignUp() {
        setSubmitted(true);

        if (hasErrors(validationErrors)) {
            return;
        }

        const shade = shades.find((s) => s.name === skinShade);
        const undertone = undertones.find((u) => u.name === skinUndertone);

        if (!shade || !undertone) {
            return;
        }

        const { shadeId } = shade;
        const { undertoneId } = undertone;

        await register({
            firstName: name,
            surname,
            email,
            username,
            password,
            shadeId,
            undertoneId,
            hasTint: hasOliveOvertone,
        });

        clearSessionStorageKeys(Object.values(DRAFT_KEYS));

        try {
            await login({ usernameOrEmail: username, password });
            await refreshUser();
            navigate(ROUTES.home);
        } catch {
            navigate(ROUTES.login);
        }
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
                        data-cy="register-first-name"
                        label="Name"
                        value={name}
                        onChange={setName}
                        error={errors.firstName}
                    />
                </div>
                <div className="w-1/2">
                    <TextField
                        data-cy="register-surname"
                        label="Surname"
                        value={surname}
                        onChange={setSurname}
                        error={errors.surname}
                    />
                </div>
            </div>
            <div className="mt-4">
                <TextField
                    data-cy="register-email"
                    label="Email"
                    value={email}
                    onChange={setEmail}
                    type="email"
                    error={errors.email}
                />
            </div>

            <hr className="my-7 border-[#e8d5c4]" />

            <div className="flex items-baseline justify-between">
                <h2 className="text-sm font-semibold uppercase tracking-wide text-[#8a7060]">
                    Your skin profile
                </h2>
                <Link
                    to={ROUTES.guide}
                    state={{ from: ROUTES.register }}
                    className="text-sm text-[#8a7060] underline hover:text-[#3b2a20]"
                >
                    Not sure? Read the guide
                </Link>
            </div>
            <div className="flex gap-4 mt-3">
                <div className="w-1/2">
                    <SelectField
                        data-cy="register-skin-shade"
                        label="Skin shade"
                        value={skinShade}
                        onChange={setSkinShade}
                        options={shades.map((shade) => shade.name)}
                        error={errors.shadeName}
                    />
                </div>
                <div className="w-1/2">
                    <SelectField
                        data-cy="register-skin-undertone"
                        label="Skin undertone"
                        value={skinUndertone}
                        onChange={setSkinUndertone}
                        options={undertones.map((undertone) => undertone.name)}
                        error={errors.undertoneName}
                    />
                </div>
            </div>
            <label className="flex items-center gap-2 text-[#3b2a20] mt-4">
                <input
                    data-cy="register-olive-overtone"
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
                        data-cy="register-username"
                        label="Username"
                        value={username}
                        onChange={setUsername}
                        error={errors.username}
                    />
                </div>
                <div className="w-1/2">
                    <TextField
                        data-cy="register-password"
                        label="Password"
                        value={password}
                        onChange={setPassword}
                        type="password"
                        error={errors.password}
                        hint={
                            <>
                                <p className="font-semibold mb-1">Your password needs:</p>
                                <ul className="list-disc pl-5 space-y-0.5">
                                    {passwordRequirements.map((requirement) => (
                                        <li key={requirement}>{requirement}</li>
                                    ))}
                                </ul>
                            </>
                        }
                    />
                </div>
            </div>

            <div className="flex gap-4 mt-8">
                <Button
                    data-cy="register-submit"
                    onClick={handleSignUp}
                    className="w-1/2"
                >
                    Sign up
                </Button>
                <Button
                    data-cy="register-return-to-login"
                    variant="secondary"
                    onClick={() => navigate(ROUTES.login)}
                    className="w-1/2"
                >
                    Return to login
                </Button>
            </div>
        </AuthLayout>
    );
}