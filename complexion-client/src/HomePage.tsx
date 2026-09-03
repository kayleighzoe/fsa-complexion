import { useState } from 'react';
import type { Recommendation } from './types';
import { ProductCard } from './ProductCard';
import homeBackground from './assets/homeBackground.jpg';

const RESULTS: Recommendation[] = [
    {
        id: '1',
        brandName: 'Fenty Beauty',
        productName: "Pro Filt'r Soft Matte Foundation",
        category: 'Foundation',
        undertone: 'Warm',
        shade: 'Medium',
        recommendedByCount: 342,
        priceTier: 'High-end',
    },
    {
        id: '2',
        brandName: 'MAC',
        productName: 'Studio Fix Fluid',
        category: 'Foundation',
        undertone: 'Neutral',
        shade: 'Light',
        recommendedByCount: 189,
        priceTier: 'High-end',
    },
    {
        id: '3',
        brandName: 'Maybelline',
        productName: 'Fit Me Matte + Poreless',
        category: 'Concealer',
        undertone: 'Cool',
        shade: 'Deep',
        recommendedByCount: 517,
        priceTier: 'Drugstore',
    },
];

export function HomePage() {
    const [query, setQuery] = useState('');

    const filtered = RESULTS.filter((item) =>
        item.productName.toLowerCase().includes(query.toLowerCase())
    );

    return (
        <div className="min-h-screen bg-[#645b50]">
            <nav className="bg-[#3b2a20] px-8 py-5 flex justify-between items-center relative z-20">
                <span
                    className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#fbeee0]"
                    style={{
                        textShadow:
                            '0 0 12px rgba(251,238,224,0.85), 0 0 32px rgba(251,238,224,0.6)',
                    }}
                >
                    Complexion
                </span>
                <div className="flex gap-8 text-[#fbeee0]">
                    <span>Home</span>
                    <span>Share</span>
                    <span>Profile</span>
                </div>
            </nav>

            <div className="relative">
                <img
                    src={homeBackground}
                    alt=""
                    aria-hidden="true"
                    className="absolute inset-0 w-full h-full object-cover object-top opacity-10"
                />
                <div className="absolute inset-0 bg-gradient-to-r from-[#3b2a20]/70 via-[#645b50]/30 to-transparent" />

                <div className="relative z-10 max-w-4xl mx-auto px-8 py-12">
                    <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                        <span className="text-[#fbeee0]">Find your </span>
                        <span className="text-[#3b2a20]">perfect shade</span>
                    </h1>
                    <p className="text-xl text-center text-[#fbeee0] mt-3">
                        Your community based skin shade matcher
                    </p>
                    <p className="text-center text-[#fbeee0] mt-4 max-w-xl mx-auto">
                        Search thousands of makeup products matched to real skin tones and get
                        personalized foundation and concealer recommendations from our community
                    </p>

                    <input
                        type="text"
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        placeholder="Search products, brands or shades…"
                        className="w-full rounded-full px-8 py-4 mt-10 mb-8 bg-[#fbeee0]"
                    />

                    {filtered.map((item) => (
                        <ProductCard key={item.id} recommendation={item} />
                    ))}
                </div>
            </div>
        </div>
    );
}