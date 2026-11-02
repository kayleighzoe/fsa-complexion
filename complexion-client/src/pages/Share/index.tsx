import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { PageBackground } from '../../components/organisms/PageBackground';
import { NavBar } from '../../components/organisms/NavBar';
import { TextField } from '../../components/atoms/TextField';
import { SelectField } from '../../components/atoms/SelectField';
import shareBackground from '../../assets/images/shareBackground.webp';
import { Button } from '../../components/atoms/Button';
import type { SkinShade, SkinUndertone, CatalogueCategory, ConfigPriceTier } from '../../types';
import { getSkinShades, getSkinUndertones } from '../../services/api/skin';
import axios from 'axios';
import { shareRecommendation } from '../../services/api/recommendations';
import { getErrorMessage } from '../../services/api/errors';
import { getCategories } from '../../services/api/catalogue';
import { getPriceTiers } from '../../services/api/config';
import { ROUTES } from '@/constants/routes';

export function SharePage() {
    const [category, setCategory] = useState('');
    const [priceTier, setPriceTier] = useState('');
    const [brandName, setBrandName] = useState('');
    const [productName, setProductName] = useState('');
    const [shadeName, setShadeName] = useState('');
    const [matchedShade, setMatchedShade] = useState('');
    const [matchedUndertone, setMatchedUndertone] = useState('');
    const [hasOliveOvertone, setHasOliveOvertone] = useState(false);
    const [comment, setComment] = useState('');
    const navigate = useNavigate();
    const [shades, setShades] = useState<SkinShade[]>([]);
    const [undertones, setUndertones] = useState<SkinUndertone[]>([]);
    const [categories, setCategories] = useState<CatalogueCategory[]>([]);
    const [priceTiers, setPriceTiers] = useState<ConfigPriceTier[]>([]);
    const [error, setError] = useState('');

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

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                setCategories(await getCategories());
            } catch (error) {
                console.error('Could not load categories', error);
            }
        };

        fetchCategories();
    }, []);

    useEffect(() => {
        const fetchPriceTiers = async () => {
            try {
                setPriceTiers(await getPriceTiers());
            } catch (error) {
                console.error('Could not load price tiers', error);
            }
        };

        fetchPriceTiers();
    }, []);


    async function handleShare() {
        setError('');

        try {
            await shareRecommendation({
                categoryName: category,
                priceTierName: priceTier,
                brand: brandName,
                productName,
                shadeName,
                matchedShadeName: matchedShade,
                matchedUndertoneName: matchedUndertone,
                hasTint: hasOliveOvertone,
                comment,
            });
            navigate(ROUTES.profile);
        } catch (err) {
            if (axios.isAxiosError(err) && err.response?.status === 401) {
                navigate(ROUTES.login);
                return;
            }
            setError(getErrorMessage(err));
        }
    }

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={shareBackground} />
            <NavBar />

            <div className="relative z-10 w-full max-w-4xl mx-auto px-8 py-12 page-fade">
                <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                    Share your recommendation
                </h1>
                <p className="text-xl text-center text-[#fbeee0] mt-3">
                    Help others find their perfect match
                </p>

                <div className="bg-white rounded-2xl p-10 mt-10">
                    <div className="flex gap-4">
                        <div className="w-1/2">
                            <SelectField
                                label="Product category"
                                value={category}
                                onChange={setCategory}
                                options={categories.map((categories) => categories.name)}
                            />
                        </div>
                        <div className="w-1/2">
                            <SelectField
                                label="Price tier"
                                value={priceTier}
                                onChange={setPriceTier}
                                options={priceTiers.map((priceTiers) => priceTiers.name)}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <TextField
                                label="Brand name"
                                value={brandName}
                                onChange={setBrandName}
                            />
                        </div>
                        <div className="w-1/2">
                            <TextField
                                label="Product name"
                                value={productName}
                                onChange={setProductName}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <TextField
                                label="Shade name"
                                value={shadeName}
                                onChange={setShadeName}
                            />
                        </div>
                        <div className="w-1/2">
                            <SelectField
                                label="Matched skin shade"
                                value={matchedShade}
                                onChange={setMatchedShade}
                                options={shades.map((shades) => shades.name)}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <SelectField
                                label="Matched skin undertone"
                                value={matchedUndertone}
                                onChange={setMatchedUndertone}
                                options={undertones.map((undertones) => undertones.name)}
                            />
                        </div>
                        <div className="w-1/2" />
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

                    <div className="mt-5">
                        <label className="block mb-1.5 text-[#3b2a20]">
                            Comment <span className="text-[#8a7060]">(optional)</span>
                        </label>
                        <textarea
                            value={comment}
                            onChange={(e) => setComment(e.target.value)}
                            rows={4}
                            className="w-full rounded-lg px-4 py-2.5 bg-[#faf6f2] border border-[#e8ddd4]"
                        />
                    </div>

                    {error && <p className="mt-5 text-sm text-red-600">{error}</p>}

                    <div className="flex gap-4 mt-8">
                        <Button
                            variant="secondary"
                            onClick={() => navigate(ROUTES.profile)}
                            className="w-1/2"
                        >
                            Cancel
                        </Button>
                        <Button
                            onClick={handleShare}
                            className="w-1/2"
                        >
                            Share recommendation
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
}