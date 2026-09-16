import { Link } from 'react-router';
import { House, Upload, CircleUser, BookOpenText } from 'lucide-react';

export function NavBar() {
    return (
        <nav className="relative z-20 bg-[#3b2a20] px-8 py-5 flex justify-between items-center p-4 border-b-2 border-slate-200">
            <Link
                to="/"
                className="text-3xl font-['Cormorant_Garamond'] font-bold text-[#fbeee0]"
                style={{
                    textShadow:
                        '0 0 12px rgba(251,238,224,0.85), 0 0 32px rgba(251,238,224,0.6)',
                }}
            >
                Complexion
            </Link>
            <div className="flex gap-8 text-[#fbeee0]">
                <Link to="/" className="flex items-center gap-1.5">
                    <House size={16}/>
                    Home
                </Link>
                <Link to="/guide" className="flex items-center gap-1.5">
                    <BookOpenText size={16}/>
                    Complexion Guide
                </Link>
                <Link to="/share" className="flex items-center gap-1.5">
                    <Upload size={16}/>
                    Share
                </Link>
                <Link to="/profile" className="flex items-center gap-1.5">
                    <CircleUser size={16}/>
                    Profile
                </Link>
            </div>
        </nav>
    );
}