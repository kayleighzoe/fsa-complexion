import { useNavigate } from 'react-router';
import { PageBackground } from './PageBackground';
import { NavBar } from './NavBar';
import { Button } from './Button';
import homeBackground from './assets/homeBackground.webp';

export function NotFoundPage() {
    const navigate = useNavigate();

    return (
        <div className="min-h-screen relative bg-[#645b50] flex flex-col">
            <PageBackground image={homeBackground} />
            <NavBar />

            <div className="relative z-10 flex-1 flex items-center page-fade">
                <div className="w-full max-w-xl mx-auto px-8 py-12">
                    <div className="bg-white rounded-2xl p-10 text-center">
                        <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                            404
                        </h1>
                        <p className="mt-4 text-xl text-[#3b2a20]">We couldn't find that page.</p>
                        <p className="mt-2 text-[#8a7060]">
                            The link may be broken or the page may have moved.
                        </p>
                        <Button onClick={() => navigate('/')} className="mt-8 px-6 py-2.5">
                            Back to home
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
}