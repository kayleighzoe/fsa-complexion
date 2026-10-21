import { useEffect, useState } from 'react';
import axios from 'axios';
import type { MyRecommendation, UserProfile } from './types';
import { PageBackground } from './PageBackground';
import { Pencil, Trash2 } from 'lucide-react';
import profileBackground from './assets/profileBackground.jpg';
import { NavBar } from './NavBar';
import { useNavigate } from 'react-router';
import { Button } from './Button';
import { logout } from './api/auth';
import { getMyProfile, getMyRecommendations, deleteRecommendation } from './api/profile';
import { getErrorMessage } from './api/errors';

function formatDate(value: string) {
    return new Date(value).toLocaleDateString('en-GB', {
        day: 'numeric',
        month: 'long',
        year: 'numeric',
    });
}

export function ProfilePage() {
    const navigate = useNavigate();
    const [profile, setProfile] = useState<UserProfile | null>(null);
    const [recommendations, setRecommendations] = useState<MyRecommendation[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [actionError, setActionError] = useState('');

    useEffect(() => {
        async function loadProfile() {
            try {
                const [profileData, recommendationData] = await Promise.all([
                    getMyProfile(),
                    getMyRecommendations(),
                ]);
                setProfile(profileData);
                setRecommendations(recommendationData);
            } catch (err) {
                if (axios.isAxiosError(err) && err.response?.status === 401) {
                    navigate('/login');
                    return;
                }
                setError(getErrorMessage(err));
            } finally {
                setLoading(false);
            }
        }

        loadProfile();
    }, [navigate]);

    async function handleLogout() {
        try {
            await logout();
            navigate('/login');
        } catch (error) {
            console.error('Could not log out', error);
        }
    }

    async function handleDelete(recommendationId: string) {
    if (!window.confirm('Delete this recommendation? This cannot be undone.')) {
        return;
    }

    setActionError('');

    try {
        await deleteRecommendation(recommendationId);
        setRecommendations((current) =>
            current.filter((r) => r.recommendationId !== recommendationId)
        );
    } catch (err) {
        if (axios.isAxiosError(err) && err.response?.status === 401) {
            navigate('/login');
            return;
        }
        setActionError(getErrorMessage(err));
    }
}

    return (
        <div className="min-h-screen relative bg-[#645b50] flex flex-col">
            <PageBackground image={profileBackground} />
            <NavBar />

            <div className="relative z-10 flex-1 flex items-center page-fade">
                <div className="w-full max-w-6xl mx-auto px-8 py-12">
                    <div className="bg-white rounded-2xl p-10">
                        <div className="flex justify-between items-start">
                            <h1 className="text-4xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                My profile
                            </h1>
                            <div className="flex gap-3">
                                <Button
                                    onClick={() => navigate('/profile/edit')}
                                    variant="pale"
                                    rounded="full"
                                    className="px-6 py-2.5">
                                    Edit
                                </Button>
                                <Button
                                    onClick={handleLogout}
                                    variant="secondary"
                                    rounded="full"
                                    className="px-6 py-2.5">
                                    Logout
                                </Button>
                            </div>
                        </div>

                        {loading && <p className="mt-8 text-[#8a7060]">Loading...</p>}
                        {error && <p className="mt-8 text-sm text-red-600">{error}</p>}

                        {profile && (
                            <div className="grid grid-cols-3 gap-y-6 mt-8">
                                <div>
                                    <p className="text-sm text-[#8a7060]">Name</p>
                                    <p className="font-semibold text-[#3b2a20]">
                                        {profile.firstName} {profile.surname}
                                    </p>
                                </div>
                                <div>
                                    <p className="text-sm text-[#8a7060]">Email</p>
                                    <p className="font-semibold text-[#3b2a20]">{profile.email}</p>
                                </div>
                                <div>
                                    <p className="text-sm text-[#8a7060]">Skin shade</p>
                                    <p className="font-semibold text-[#3b2a20]">{profile.shadeName}</p>
                                </div>
                                <div>
                                    <p className="text-sm text-[#8a7060]">Skin undertone</p>
                                    <p className="font-semibold text-[#3b2a20]">{profile.undertoneName}</p>
                                </div>
                                <div>
                                    <p className="text-sm text-[#8a7060]">Olive overtone</p>
                                    <p className="font-semibold text-[#3b2a20]">
                                        {profile.hasTint ? 'Yes' : 'No'}
                                    </p>
                                </div>
                            </div>
                        )}
                    </div>

                    <div className="bg-white rounded-2xl p-10 mt-8">
                        <div className="flex justify-between items-center">
                            <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                My perfect shade matches
                            </h2>
                            <Button
                                onClick={() => navigate('/share')}
                                rounded="full"
                                className="px-6 py-2.5">
                                + Share a recommendation
                            </Button>
                        </div>

                        {!loading && !error && recommendations.length === 0 && (
                            <p className="mt-6 text-[#8a7060]">
                                You haven't shared any recommendations yet.
                            </p>
                        )}

                        {recommendations.map((item) => (
                            <div
                                key={item.recommendationId}
                                className="border border-[#e8ddd4] rounded-xl p-6 mt-6 flex justify-between items-center"
                            >
                                <div>
                                    <h3 className="text-xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                        {item.brand} — {item.productName}
                                    </h3>
                                    <p className="text-sm text-[#5a4a3f] mt-1">
                                        {item.categoryName} · Shade {item.shadeName} · {item.priceTierName}
                                    </p>
                                    {item.comment && (
                                        <p className="italic text-sm text-[#5a4a3f] mt-2">"{item.comment}"</p>
                                    )}
                                    <p className="text-xs text-[#a89684] mt-3">
                                        Shared {formatDate(item.createdAt)}
                                    </p>
                                </div>
                                <div className="flex gap-2">
                                    <Button
                                        onClick={() => navigate(`/recommendations/${item.recommendationId}/edit`)}
                                        variant="pale"
                                        rounded="full"
                                        size="icon"
                                        className="w-10 h-10 flex items-center justify-center cursor-pointer">
                                        <Pencil size={16} className="text-[#3b2a20]"/>
                                    </Button>
                                    <Button
                                        onClick={() => handleDelete(item.recommendationId)}
                                        variant="pale"
                                        rounded="full"
                                        size="icon"
                                        className="w-10 h-10 flex items-center justify-center cursor-pointer">
                                        <Trash2 size={16} className="text-[#3b2a20]"/>
                                    </Button>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
}