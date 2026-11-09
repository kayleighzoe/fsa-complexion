import { useEffect, useState } from 'react';
import type { RecommendationSummary } from '../../types';
import { ProductCard } from '../../components/molecules/ProductCard/index';
import { PageBackground } from '../../components/organisms/PageBackground';
import homeBackground from '../../assets/images/homeBackground.webp';
import { NavBar } from '../../components/organisms/NavBar';
import { Search } from 'lucide-react';
import { getRecommendationSummaries } from '../../services/api/recommendations';
import { getErrorMessage } from '../../services/api/errors';

function toSearchText(item: RecommendationSummary) {
    return [
        item.brand,
        item.productName,
        item.productShadeName,
        item.categoryName,
        item.priceTierName,
        item.shadeName,
        item.undertoneName,
        item.hasTint ? 'olive' : '',
    ]
        .join(' ')
        .toLowerCase();
}

function toKey(item: RecommendationSummary) {
    return [
        item.brand,
        item.productName,
        item.productShadeName,
        item.shadeName,
        item.undertoneName,
        item.hasTint,
    ].join('|');
}

export function HomePage() {
    const [query, setQuery] = useState('');
    const [results, setResults] = useState<RecommendationSummary[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        async function loadResults() {
            try {
                setResults(await getRecommendationSummaries());
            } catch (err) {
                setError(getErrorMessage(err));
            } finally {
                setLoading(false);
            }
        }

        loadResults();
    }, []);

    const words = query
        .toLowerCase()
        .split(' ')
        .filter((word) => word !== '');

    const filtered = results.filter((item) => {
        const text = toSearchText(item);
        return words.every((word) => text.includes(word));
    });

    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={homeBackground} />
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
                        placeholder="Search products, brands or skin profile - e.g. warm medium…"
                        className="w-full rounded-full pl-14 pr-8 py-4 bg-[#fbeee0]"
                    />
                </div>

                <div className="min-h-[500px]">
                    {loading && <p className="text-center text-[#fbeee0] py-12">Loading...</p>}
                    {error && <p className="text-center text-red-200 py-12">{error}</p>}
                    {!loading && !error && filtered.length === 0 && (
                        <p className="text-center text-[#fbeee0] py-12">
                            {results.length === 0
                                ? 'No recommendations have been shared yet.'
                                : `No products match "${query}"`}
                        </p>
                    )}
                    {filtered.map((item) => (
                        <ProductCard key={toKey(item)} recommendation={item} />
                    ))}
                </div>
            </div>
        </div>
    );
}