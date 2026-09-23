import {
    Accordion,
    AccordionItem,
    AccordionTrigger,
    AccordionContent,
} from '@/components/ui/accordion';
import type { Recommendation } from './types';

interface ProductCardProps {
    recommendation: Recommendation;
}

export function ProductCard({ recommendation }: ProductCardProps) {
    return (
        <div className="bg-white rounded-2xl px-8 py-6 mb-4">
            <div className="flex justify-between items-start">
                <div>
                    <p className="text-sm tracking-wide text-[#8a7060] uppercase">
                        {recommendation.brandName}
                    </p>
                    <h3 className="text-2xl font-serif mt-1">
                        {recommendation.productName}
                    </h3>
                    <p className="mt-3">
                        <strong>Skin Profile:</strong> {recommendation.undertone} Undertone,{' '}
                        {recommendation.shade}
                    </p>
                </div>
                <span className="bg-[#f5e0d0] rounded-full px-4 py-1.5 text-sm">
                    {recommendation.category}
                </span>
            </div>

            <hr className="my-4 border-[#e8ddd4]" />

            <div className="flex justify-between">
                <p>
                    Recommended by <strong>{recommendation.recommendedByCount}</strong> users
                </p>
                <p>{recommendation.priceTier}</p>
            </div>

            <Accordion>
                <AccordionItem value="comments">
                    <AccordionTrigger className="text-[#3b2a20]">
                        Community comments ({recommendation.comments.length})
                    </AccordionTrigger>
                    <AccordionContent>
                        {recommendation.comments.length === 0 ? (
                            <p className="text-[#8a7060]">No comments yet.</p>
                        ) : (
                            recommendation.comments.map((comment) => (
                                <div
                                    key={comment.id}
                                    className="bg-[#faf6f2] rounded-xl px-5 py-4 mb-3 border border-[#f0e8e0]"
                                >
                                    <div className="flex items-baseline gap-2">
                                        <span className="font-semibold text-[#3b2a20]">{comment.author}</span>
                                        <span className="text-xs text-[#a89684]">({comment.skinProfile})</span>
                                    </div>
                                    <p className="text-[#5a4a3f] mt-1.5">{comment.text}</p>
                                </div>
                            ))
                        )}
                    </AccordionContent>
                </AccordionItem>
            </Accordion>
        </div>
    );
}