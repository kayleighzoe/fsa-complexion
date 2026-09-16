import type { MyRecommendation } from './types';
import { PageBackground } from './PageBackground';
import { Pencil, Trash2 } from 'lucide-react';
import profileBackground from './assets/profileBackground.jpg';
import { NavBar } from './NavBar';
import { useNavigate } from 'react-router';

const USER = {
    name: 'Someone Someone',
    email: 'test@test.com',
    skinShade: 'Light',
    skinUndertone: 'Neutral',
    oliveOvertone: 'No',
    memberSince: 'May 2026',
};

const MY_RECOMMENDATIONS: MyRecommendation[] = [
    {
        id: '1',
        brandName: 'MAC',
        productName: 'Studio Fix',
        category: 'Foundation',
        shadeName: 'NC44.5',
        priceTier: 'High-end',
        comment: 'Leans more warm on warm-neutral undertones',
        sharedOn: '11 May 2026',
    },
];

export function ProfilePage() {
    const navigate = useNavigate();

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
                                <button className="bg-[#f0d5c0] rounded-full px-6 py-2.5">Edit</button>
                                <button className="bg-[#3b2a20] text-white rounded-full px-6 py-2.5">
                                    Logout
                                </button>
                            </div>
                        </div>

                        <div className="grid grid-cols-3 gap-y-6 mt-8">
                            <div>
                                <p className="text-sm text-[#8a7060]">Name</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.name}</p>
                            </div>
                            <div>
                                <p className="text-sm text-[#8a7060]">Email</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.email}</p>
                            </div>
                            <div>
                                <p className="text-sm text-[#8a7060]">Skin shade</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.skinShade}</p>
                            </div>
                            <div>
                                <p className="text-sm text-[#8a7060]">Skin undertone</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.skinUndertone}</p>
                            </div>
                            <div>
                                <p className="text-sm text-[#8a7060]">Olive overtone</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.oliveOvertone}</p>
                            </div>
                            <div>
                                <p className="text-sm text-[#8a7060]">Member since</p>
                                <p className="font-semibold text-[#3b2a20]">{USER.memberSince}</p>
                            </div>
                        </div>
                    </div>

                    <div className="bg-white rounded-2xl p-10 mt-8">
                        <div className="flex justify-between items-center">
                            <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                My perfect shade matches
                            </h2>
                            <button
                                onClick={() => navigate('/share')}
                                className="bg-[#9db4c0] rounded-full px-6 py-2.5">
                                + Share a recommendation
                            </button>
                        </div>

                        {MY_RECOMMENDATIONS.map((item) => (
                            <div
                                key={item.id}
                                className="border border-[#e8ddd4] rounded-xl p-6 mt-6 flex justify-between items-center"
                            >
                                <div>
                                    <h3 className="text-xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                                        {item.brandName} — {item.productName}
                                    </h3>
                                    <p className="text-sm text-[#5a4a3f] mt-1">
                                        {item.category} · Shade {item.shadeName} · {item.priceTier}
                                    </p>
                                    {item.comment && (
                                        <p className="italic text-sm text-[#5a4a3f] mt-2">"{item.comment}"</p>
                                    )}
                                    <p className="text-xs text-[#a89684] mt-3">Shared {item.sharedOn}</p>
                                </div>
                                <div className="flex gap-2">
                                    <button className="bg-[#f0d5c0] rounded-full w-10 h-10 flex items-center justify-center cursor-pointer">
                                        <Pencil size={16} className="text-[#3b2a20]"/>
                                    </button>
                                    <button className="bg-[#f0d5c0] rounded-full w-10 h-10 flex items-center justify-center cursor-pointer">
                                        <Trash2 size={16} className="text-[#3b2a20]"/>
                                    </button>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
}