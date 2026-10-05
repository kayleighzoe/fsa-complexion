import { useState } from 'react';
import type { Recommendation } from './types';
import { ProductCard } from './ProductCard';
import { PageBackground } from './PageBackground';
import homeBackground from './assets/homeBackground.jpg';
import { NavBar } from './NavBar';
import { Search } from 'lucide-react'

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
        comments: [
            {
                id: 'c1',
                text: 'Oxidises slightly after a few hours, go half a shade lighter.',
                author: 'thandi_m',
                skinProfile: 'Warm, Medium',
            },
        ],
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
        comments: [
            {
                id: 'c1',
                text: 'Oxidises slightly after a few hours, go half a shade lighter.',
                author: 'thandi_m',
                skinProfile: 'Warm, Medium',
            },
        ],
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
        comments: [
            {
                id: 'c1',
                text: 'Oxidises slightly after a few hours, go half a shade lighter.',
                author: 'thandi_m',
                skinProfile: 'Warm, Medium',
            },
        ],
    },
];

export function HomePage() {
    const [query, setQuery] = useState('');

    const filtered = RESULTS.filter((item) =>
        `${item.undertone} ${item.shade}`.toLowerCase().includes(query.toLowerCase())
    );

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={homeBackground}/>
            <NavBar />

            <div className="relative z-10 w-full max-w-6xl mx-auto px-8 py-12 page-fade">
                <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center">
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

                <div className="relative mt-10 mb-8">
                    <Search
                        className="absolute left-6 top-1/2 -translate-y-1/2 w-5 h-5 text-[#8a7060]"
                    />
                    <input
                        type="text"
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        placeholder="Search by skin profile - e.g. warm, medium…"
                        className="w-full rounded-full pl-14 pr-8 py-4 bg-[#fbeee0]"
                    />
                </div>

                <div className="min-h-[500px]">
                    {filtered.length === 0 ? (
                        <p className="text-center text-[#fbeee0] py-12">
                            No products match "{query}"
                        </p>
                    ) : (
                        filtered.map((item) => (
                            <ProductCard key={item.id} recommendation={item} />
                        ))
                    )}
                </div>
            </div>
        </div>
    );
}