import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { ROUTES } from '@/constants/routes';
import { PageBackground } from '../../components/organisms/PageBackground';
import { NavBar } from '../../components/organisms/NavBar';
import { TextField } from '../../components/atoms/TextField';
import { SelectField } from '../../components/atoms/SelectField';
import { Button } from '../../components/atoms/Button';
import profileBackground from '../../assets/images/profileBackground.webp';
import { getMyProfile, updateMyProfile } from '../../services/api/profile';
import { getSkinShades, getSkinUndertones } from '../../services/api/skin';
import type { SkinShade, SkinUndertone } from '../../types';
import { EditProfileFormSkeleton } from '@/components/molecules/EditProfileFormSkeleton';
import { validateProfile } from '@/utils/validateProfile';
import { hasErrors } from '@/utils/validation';
import type { ProfileFormErrors } from '@/utils/validateProfile';

export function EditProfilePage() {
    const navigate = useNavigate();

    const [firstName, setFirstName] = useState('');
    const [surname, setSurname] = useState('');
    const [email, setEmail] = useState('');
    const [shadeName, setShadeName] = useState('');
    const [undertoneName, setUndertoneName] = useState('');
    const [hasTint, setHasTint] = useState(false);

    const [shades, setShades] = useState<SkinShade[]>([]);
    const [undertones, setUndertones] = useState<SkinUndertone[]>([]);
    const [loading, setLoading] = useState(true);
    const [submitted, setSubmitted] = useState(false);

    const validationErrors = validateProfile({ firstName, surname, email, shadeName, undertoneName });
    const errors: ProfileFormErrors = submitted ? validationErrors : {};

    useEffect(() => {
        const load = async () => {
            try {
                const [profile, shadeList, undertoneList] = await Promise.all([
                    getMyProfile(),
                    getSkinShades(),
                    getSkinUndertones(),
                ]);

                setShades(shadeList);
                setUndertones(undertoneList);
                setFirstName(profile.firstName);
                setSurname(profile.surname);
                setEmail(profile.email);
                setShadeName(profile.shadeName);
                setUndertoneName(profile.undertoneName);
                setHasTint(profile.hasTint);
            } catch (err) {
                navigate(ROUTES.profile);
            } finally {
                setLoading(false);
            }
        };

        load();
    }, [navigate]);

    async function handleSave() {
        setSubmitted(true);

        if (hasErrors(validationErrors)) {
            return;
        }

        const shade = shades.find((s) => s.name === shadeName);
        const undertone = undertones.find((u) => u.name === undertoneName);

        if (!shade || !undertone) {
            return;
        }

        await updateMyProfile({
            firstName,
            surname,
            email,
            shadeId: shade.shadeId,
            undertoneId: undertone.undertoneId,
            hasTint,
        });
        navigate(ROUTES.profile);
    }

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={profileBackground} />
            <NavBar />

            <div className="relative z-10 w-full max-w-3xl mx-auto px-8 py-12 page-fade">
                <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                    Edit profile
                </h1>

                <div className="bg-white rounded-2xl p-10 mt-10">
                    {loading ? (
                        <EditProfileFormSkeleton />
                    ) : (
                        <>
                            <div className="flex gap-4">
                                <div className="w-1/2">
                                    <TextField
                                        label="First name"
                                        value={firstName}
                                        onChange={setFirstName}
                                        error={errors.firstName}
                                    />
                                </div>
                                <div className="w-1/2">
                                    <TextField
                                        label="Surname"
                                        value={surname}
                                        onChange={setSurname}
                                        error={errors.surname}
                                    />
                                </div>
                            </div>

                            <div className="mt-5">
                                <TextField
                                    label="Email"
                                    value={email}
                                    onChange={setEmail}
                                    error={errors.email}
                                />
                            </div>

                            <div className="flex gap-4 mt-5">
                                <div className="w-1/2">
                                    <SelectField
                                        label="Skin shade"
                                        value={shadeName}
                                        onChange={setShadeName}
                                        options={shades.map((s) => s.name)}
                                        error={errors.shadeName}
                                    />
                                </div>
                                <div className="w-1/2">
                                    <SelectField
                                        label="Skin undertone"
                                        value={undertoneName}
                                        onChange={setUndertoneName}
                                        options={undertones.map((u) => u.name)}
                                        error={errors.undertoneName}
                                    />
                                </div>
                            </div>

                            <label className="flex items-center gap-2 text-[#3b2a20] mt-5">
                                <input
                                    type="checkbox"
                                    checked={hasTint}
                                    onChange={(e) => setHasTint(e.target.checked)}
                                />
                                Olive overtone
                            </label>

                            <div className="flex gap-4 mt-8">
                                <Button variant="secondary" onClick={() => navigate(ROUTES.profile)} className="w-1/2">
                                    Cancel
                                </Button>
                                <Button onClick={handleSave} className="w-1/2">
                                    Save
                                </Button>
                            </div>
                        </>
                    )}
                </div>
            </div>
        </div>
    );
}