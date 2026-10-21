import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import axios from 'axios';
import { PageBackground } from './PageBackground';
import { NavBar } from './NavBar';
import { TextField } from './TextField';
import { SelectField } from './SelectField';
import { Button } from './Button';
import profileBackground from './assets/profileBackground.jpg';
import { getMyProfile, updateMyProfile } from './api/profile';
import { getSkinShades, getSkinUndertones } from './api/lookups';
import { getErrorMessage } from './api/errors';
import type { SkinShade, SkinUndertone } from './types';

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
    const [error, setError] = useState('');

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
                if (axios.isAxiosError(err) && err.response?.status === 401) {
                    navigate('/login');
                    return;
                }
                setError(getErrorMessage(err));
            } finally {
                setLoading(false);
            }
        };

        load();
    }, [navigate]);

    async function handleSave() {
        setError('');

        const shade = shades.find((s) => s.name === shadeName);
        const undertone = undertones.find((u) => u.name === undertoneName);

        if (!shade || !undertone) {
            setError('Please choose a skin shade and undertone.');
            return;
        }

        try {
            await updateMyProfile({
                firstName,
                surname,
                email,
                shadeId: shade.shadeId,
                undertoneId: undertone.undertoneId,
                hasTint,
            });
            navigate('/profile');
        } catch (err) {
            if (axios.isAxiosError(err) && err.response?.status === 401) {
                navigate('/login');
                return;
            }
            setError(getErrorMessage(err));
        }
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
                        <p className="text-[#3b2a20]">Loading...</p>
                    ) : (
                        <>
                            <div className="flex gap-4">
                                <div className="w-1/2">
                                    <TextField label="First name" value={firstName} onChange={setFirstName} />
                                </div>
                                <div className="w-1/2">
                                    <TextField label="Surname" value={surname} onChange={setSurname} />
                                </div>
                            </div>

                            <div className="mt-5">
                                <TextField label="Email" value={email} onChange={setEmail} />
                            </div>

                            <div className="flex gap-4 mt-5">
                                <div className="w-1/2">
                                    <SelectField
                                        label="Skin shade"
                                        value={shadeName}
                                        onChange={setShadeName}
                                        options={shades.map((s) => s.name)}
                                    />
                                </div>
                                <div className="w-1/2">
                                    <SelectField
                                        label="Skin undertone"
                                        value={undertoneName}
                                        onChange={setUndertoneName}
                                        options={undertones.map((u) => u.name)}
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

                            {error && <p className="text-red-600 mt-5">{error}</p>}

                            <div className="flex gap-4 mt-8">
                                <Button variant="secondary" onClick={() => navigate('/profile')} className="w-1/2">
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