import { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router';
import { PageBackground } from './PageBackground';
import { NavBar } from './NavBar';
import { Button } from './Button';
import profileBackground from './assets/profileBackground.jpg';
import type { MyRecommendation } from './types';
import { getMyRecommendations, updateRecommendation } from './api/profile';
import { getErrorMessage } from './api/errors';

export function EditRecommendationPage() {
    const { recommendationId } = useParams();
    const navigate = useNavigate();
    const [recommendation, setRecommendation] = useState<MyRecommendation | null>(null);
    const [comment, setComment] = useState('');
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState('');

    useEffect(() => {
        async function loadRecommendation() {
            try {
                const mine = await getMyRecommendations();
                const match = mine.find(
                    (item) => item.recommendationId.toLowerCase() === recommendationId?.toLowerCase()
                );

                if (!match) {
                    setError('Recommendation not found.');
                    return;
                }

                setRecommendation(match);
                setComment(match.comment ?? '');
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

        loadRecommendation();
    }, [recommendationId, navigate]);

    async function handleSave() {
        if (!recommendationId || saving) return;

        setSaving(true);
        setError('');

        try {
            await updateRecommendation(recommendationId, comment);
            navigate('/profile');
        } catch (err) {
            setError(getErrorMessage(err));
            setSaving(false);
        }
    }

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={profileBackground} />
            <NavBar />

            <div className="relative z-10 w-full max-w-4xl mx-auto px-8 py-12 page-fade">
                <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                    Edit your recommendation
                </h1>

                <div className="bg-white rounded-2xl p-10 mt-10">
                    {loading && <p className="text-[#8a7060]">Loading...</p>}

                    {recommendation && (
                        <>
                            <h2 className="text-2xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                {recommendation.brand} — {recommendation.productName}
                            </h2>
                            <p className="text-sm text-[#5a4a3f] mt-1">
                                {recommendation.categoryName} · Shade {recommendation.shadeName} · {recommendation.priceTierName}
                            </p>

                            <div className="mt-6">
                                <label className="block mb-1.5 text-[#3b2a20]">
                                    Comment <span className="text-[#8a7060]">(optional)</span>
                                </label>
                                <textarea
                                    value={comment}
                                    onChange={(e) => setComment(e.target.value)}
                                    rows={4}
                                    maxLength={255}
                                    className="w-full rounded-lg px-4 py-2.5 bg-[#faf6f2] border border-[#e8ddd4]"
                                />
                            </div>
                        </>
                    )}

                    {error && <p className="mt-4 text-sm text-red-600">{error}</p>}

                    <div className="flex gap-4 mt-8">
                        <Button
                            variant="secondary"
                            onClick={() => navigate('/profile')}
                            className={recommendation ? 'w-1/2' : 'w-full'}
                        >
                            Cancel
                        </Button>
                        {recommendation && (
                            <Button onClick={handleSave} className="w-1/2">
                                Save changes
                            </Button>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}