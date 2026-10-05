import { PageBackground } from './PageBackground';
import { NavBar } from './NavBar';
import homeBackground from './assets/homeBackground.jpg';
import fitzpatrickScale from './assets/fitzpatrickScale.jpg';

export function GuidePage() {
    return (
        <div className="min-h-screen bg-[#645b50] flex flex-col relative">
            <PageBackground image={homeBackground} />
            <NavBar />

            <div className="relative z-10 w-full max-w-6xl mx-auto px-8 py-12 page-fade">
                <h1 className="text-6xl font-['Cormorant_Garamond'] font-bold text-center text-[#fbeee0]">
                    Find your skin profile
                </h1>
                <p className="text-center text-[#fbeee0] mt-3">
                    Three things describe your skin. Knowing all three gets you a better match
                </p>

                <div className="bg-white rounded-2xl p-10 mt-10">
                    <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                        Skin shade
                    </h2>
                    <p className="mt-3 text-[#5a4a3f]">
                        The depth of skin shade. Following the Fitzpatrick scale there's{' '}
                        <strong>fair, light, medium, tan, deep</strong> and{' '}
                        <strong>very deep</strong>.
                    </p>
                    <img
                        src={fitzpatrickScale}
                        alt="The Fitzpatrick scale, showing six skin types from very fair to deeply pigmented dark brown"
                        className="rounded-xl mt-6 mx-auto"
                    />
                </div>

                <div className="bg-white rounded-2xl p-10 mt-8">
                    <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                        Skin undertone
                    </h2>
                    <p className="mt-3 text-[#5a4a3f]">
                        The temperature of skin underneath its depth.{' '}
                        <strong>Cool, cool-neutral, true neutral, warm-neutral</strong> and{' '}
                        <strong>warm</strong>.
                    </p>
                </div>

                <div className="bg-white rounded-2xl p-10 mt-8">
                    <h2 className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#3b2a20]">
                        Skin overtone
                    </h2>
                    <p className="mt-3 text-[#5a4a3f]">
                        The tint over the skin. Can be greyish or green and is referred to as an{' '}
                        <strong>olive overtone</strong> or tint.
                    </p>
                </div>
            </div>
        </div>
    );
}