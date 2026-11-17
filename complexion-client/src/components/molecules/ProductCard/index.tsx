import {
    Accordion,
    AccordionItem,
    AccordionTrigger,
    AccordionContent,
} from '@/components/ui/accordion';
import type { RecommendationSummary } from '../../../types';

interface ProductCardProps {
    recommendation: RecommendationSummary;
}

export function ProductCard({ recommendation }: ProductCardProps) {
    return (
        <div className="bg-white rounded-2xl px-8 py-6 mb-4">
            <div className="flex justify-between items-start">
                <div>
                    <p className="text-sm tracking-wide text-[#8a7060] uppercase">
                        {recommendation.brand}
                    </p>
                    <h3 className="text-2xl font-serif mt-1">
                        {recommendation.productName}
                    </h3>
                    <p className="text-sm text-[#5a4a3f] mt-1">
                        Shade {recommendation.productShadeName}
                    </p>
                    <p className="mt-3">
                        <strong>Skin Profile:</strong> {recommendation.undertoneName} Undertone,{' '}
                        {recommendation.shadeName}
                        {recommendation.hasTint && ', Olive overtone'}
                    </p>
                </div>
                <span className="bg-[#f5e0d0] rounded-full px-4 py-1.5 text-sm">
                    {recommendation.categoryName}
                </span>
            </div>

            <hr className="my-4 border-[#e8ddd4]" />

            <div className="flex justify-between">
                <p>
                    Recommended by <strong>{recommendation.recommendedByCount}</strong>{' '}
                    {recommendation.recommendedByCount === 1 ? 'user' : 'users'}
                </p>
                <p>{recommendation.priceTierName}</p>
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
                            recommendation.comments.map((comment, index) => (
                                <div
                                    key={`${comment.username}-${index}`}
                                    className="bg-[#faf6f2] rounded-xl px-5 py-4 mb-3 border border-[#f0e8e0]"
                                >
                                    <span className="font-semibold text-[#3b2a20]">
                                        {comment.username}
                                    </span>
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