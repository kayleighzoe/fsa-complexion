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
import { shareRecommendation } from '../../services/api/recommendations';
import { getErrorMessage } from '../../services/api/errors';
import { getCategories } from '../../services/api/catalogue';
import { getPriceTiers } from '../../services/api/config';
import { ROUTES } from '@/constants/routes';
import { validateShare } from '@/utils/validateShare';
import type { ShareFormErrors } from '@/utils/validateShare';
import { hasErrors } from '@/utils/validation';

const MAX_COMMENT_LENGTH = 255;

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
    const [submitted, setSubmitted] = useState(false);

    const validationErrors = validateShare({
        category,
        priceTier,
        brandName,
        productName,
        shadeName,
        matchedShade,
        matchedUndertone,
    });
    const errors: ShareFormErrors = submitted ? validationErrors : {};

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
        setSubmitted(true);

        if (hasErrors(validationErrors)) {
            return;
        }

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
            setError(getErrorMessage(err));
        }
    }

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={shareBackground} />
            <NavBar />

            <div className="relative z-10 w-full max-w-4xl mx-auto px-8 py-12 page-fade">
                <h1
                    data-cy="share-title"
                    className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]"
                >
                    Share your recommendation
                </h1>
                <p className="text-xl text-center text-[#fbeee0] mt-3">
                    Help others find their perfect match
                </p>

                <div className="bg-white rounded-2xl p-10 mt-10">
                    <div className="flex gap-4">
                        <div className="w-1/2">
                            <SelectField
                                data-cy="share-category"
                                label="Product category"
                                value={category}
                                onChange={setCategory}
                                options={categories.map((categories) => categories.name)}
                                error={errors.category}
                            />
                        </div>
                        <div className="w-1/2">
                            <SelectField
                                data-cy="share-price-tier"
                                label="Price tier"
                                value={priceTier}
                                onChange={setPriceTier}
                                options={priceTiers.map((priceTiers) => priceTiers.name)}
                                error={errors.priceTier}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <TextField
                                data-cy="share-brand-name"
                                label="Brand name"
                                value={brandName}
                                onChange={setBrandName}
                                error={errors.brandName}
                            />
                        </div>
                        <div className="w-1/2">
                            <TextField
                                data-cy="share-product-name"
                                label="Product name"
                                value={productName}
                                onChange={setProductName}
                                error={errors.productName}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <TextField
                                data-cy="share-shade-name"
                                label="Shade name"
                                value={shadeName}
                                onChange={setShadeName}
                                error={errors.shadeName}
                            />
                        </div>
                        <div className="w-1/2">
                            <SelectField
                                data-cy="share-matched-shade"
                                label="Matched skin shade"
                                value={matchedShade}
                                onChange={setMatchedShade}
                                options={shades.map((shades) => shades.name)}
                                error={errors.matchedShade}
                            />
                        </div>
                    </div>

                    <div className="flex gap-4 mt-5">
                        <div className="w-1/2">
                            <SelectField
                                data-cy="share-matched-undertone"
                                label="Matched skin undertone"
                                value={matchedUndertone}
                                onChange={setMatchedUndertone}
                                options={undertones.map((undertones) => undertones.name)}
                                error={errors.matchedUndertone}
                            />
                        </div>
                        <div className="w-1/2" />
                        <div className="w-1/2 flex items-end pb-2.5">
                            <label className="flex items-center gap-2 text-[#3b2a20]">
                                <input
                                    data-cy="share-olive-overtone"
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
                            data-cy="share-comment"
                            value={comment}
                            onChange={(e) => setComment(e.target.value)}
                            rows={4}
                            maxLength={MAX_COMMENT_LENGTH}
                            className="w-full rounded-lg px-4 py-2.5 bg-[#faf6f2] border border-[#e8ddd4]"
                        />
                        <p
                            data-cy="share-comment-count"
                            className={`mt-1.5 text-sm text-right ${comment.length >= MAX_COMMENT_LENGTH ? 'text-red-600' : 'text-[#8a7060]'
                                }`}
                        >
                            {comment.length}/{MAX_COMMENT_LENGTH}
                        </p>
                    </div>

                    {error && (
                        <p data-cy="share-error" className="mt-5 text-sm text-red-600">
                            {error}
                        </p>
                    )}

                    <div className="flex gap-4 mt-8">
                        <Button
                            data-cy="share-cancel"
                            variant="secondary"
                            onClick={() => navigate(ROUTES.profile)}
                            className="w-1/2"
                        >
                            Cancel
                        </Button>
                        <Button
                            data-cy="share-submit"
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