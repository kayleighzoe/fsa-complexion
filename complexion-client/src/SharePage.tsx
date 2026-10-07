import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { PageBackground } from './PageBackground';
import { NavBar } from './NavBar';
import { TextField } from './TextField';
import { SelectField } from './SelectField';
import shareBackground from './assets/shareBackground.png';
import { Button } from './Button';
import { api } from './api/client';
import type { SkinShade, SkinUndertone } from './types';

interface CatalogueCategory {
    categoryId: number;
    name: string;
}

interface ConfigPriceTier {
    priceTierId: number;
    name: string;
}

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
    const [catergories, setCategories] = useState<CatalogueCategory[]>([]);
    const [priceTiers, setPriceTiers] = useState<ConfigPriceTier[]>([]);
    
        useEffect(() => {
            const fetchShades = async () => {
                try {
                    const { data } = await api.get<SkinShade[]>('/SkinShade/GetAllSkinShades');
                    setShades(data);
                } catch (error) {
                    console.error('Could not load skin shades', error);
                }
            };
    
            fetchShades();
        }, []);
    
        useEffect(() => {
            const fetchUndertones = async () => {
                try {
                    const { data } = await api.get<SkinUndertone[]>('/SkinUndertone/GetAllSkinUndertones');
                    setUndertones(data);
                } catch (error) {
                    console.error('Could not load skin undertones', error);
                }
            };
    
            fetchUndertones();
        }, []);

        useEffect(() => {
            const fetchCategories = async () => {
                try {
                    const { data } = await api.get<CatalogueCategory[]>('/CatalogueCategory/GetAllCategories');
                    setCategories(data);
                } catch (error) {
                    console.error('Could not load catagories', error);
                }
            };
    
            fetchCategories();
        }, []);

        useEffect(() => {
            const fetchPriceTiers = async () => {
                try {
                    const { data } = await api.get<ConfigPriceTier[]>('/ConfigPriceTier/GetAllPriceTiers');
                    setPriceTiers(data);
                } catch (error) {
                    console.error('Could not load price tiers', error);
                }
            };
    
            fetchPriceTiers();
        }, []);
    

    function handleShare() {
        console.log({
            category,
            priceTier,
            brandName,
            productName,
            shadeName,
            matchedShade,
            matchedUndertone,
            hasOliveOvertone,
            comment,
        });
        //call API here later
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
                                options={catergories.map((catergories) => catergories.name)}
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

                    <div className="flex gap-4 mt-8">
                        <Button
                            variant="secondary"
                            onClick={() => navigate('/profile')}
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