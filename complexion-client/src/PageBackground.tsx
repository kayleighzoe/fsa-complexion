interface PageBackgroundProps {
    image: string;
    position?: string;
}

export function PageBackground({ image, position = 'object-center' }: PageBackgroundProps) {
    return (
        <>
            <img
                src={image}
                alt=""
                aria-hidden="true"
                className={`fixed inset-0 w-full h-full object-cover ${position} opacity-10`}
            />
            <div className="fixed inset-0 bg-gradient-to-r from-[#3b2a20]/70 via-[#645b50]/30 to-[#645b50]/0" />
        </>
    );
}