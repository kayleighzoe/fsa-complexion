import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { describe, expect, it } from 'vitest'
import type { RecommendationSummary } from '../../../types'
import { ProductCard } from '.'

function makeSummary(overrides: Partial<RecommendationSummary> = {}): RecommendationSummary {
    return {
        brand: 'Fenty',
        productName: 'Soft Foundation',
        productShadeName: '240',
        categoryName: 'Foundation',
        priceTierName: 'Premium',
        shadeName: 'Medium',
        undertoneName: 'Warm',
        hasTint: false,
        recommendedByCount: 3,
        comments: [],
        ...overrides,
    }
}

const comments = [
    { username: 'kayleigh', text: 'Matches my skin perfectly' },
    { username: 'sam', text: 'A bit pricey' },
]

describe('ProductCard', () => {
    it('shows the product details', () => {
        render(<ProductCard recommendation={makeSummary()} />)

        expect(screen.getByText('Fenty')).toBeInTheDocument()
        expect(screen.getByRole('heading', { name: 'Soft Foundation' })).toBeInTheDocument()
        expect(screen.getByText('Shade 240')).toBeInTheDocument()
        expect(screen.getByText('Foundation')).toBeInTheDocument()
        expect(screen.getByText('Premium')).toBeInTheDocument()
    })

    it('shows the skin profile without the olive overtone by default', () => {
        render(<ProductCard recommendation={makeSummary()} />)

        expect(screen.getByText('Warm Undertone, Medium')).toBeInTheDocument()
        expect(screen.queryByText(/Olive overtone/)).not.toBeInTheDocument()
    })

    it('adds the olive overtone to the skin profile when hasTint is true', () => {
        render(<ProductCard recommendation={makeSummary({ hasTint: true })} />)

        expect(screen.getByText('Warm Undertone, Medium, Olive overtone')).toBeInTheDocument()
    })

it('says "user" when recommended by one person', () => {
    render(<ProductCard recommendation={makeSummary({ recommendedByCount: 1 })} />)

    const line = screen.getByText('1').parentElement

    expect(line).toHaveTextContent('Recommended by 1 user')
    expect(line).not.toHaveTextContent('users')
})

it('says "users" when recommended by several people', () => {
    render(<ProductCard recommendation={makeSummary({ recommendedByCount: 3 })} />)

    const line = screen.getByText('3').parentElement

    expect(line).toHaveTextContent('Recommended by 3 users')
})

    it('shows how many comments there are', () => {
        render(<ProductCard recommendation={makeSummary({ comments })} />)

        expect(
            screen.getByRole('button', { name: /Community comments \(2\)/ }),
        ).toBeInTheDocument()
    })

    it('reveals the comments when the trigger is clicked', async () => {
        const user = userEvent.setup()

        render(<ProductCard recommendation={makeSummary({ comments })} />)
        const trigger = screen.getByRole('button', { name: /Community comments/ })
        expect(trigger).toHaveAttribute('aria-expanded', 'false')
        await user.click(trigger)

        expect(trigger).toHaveAttribute('aria-expanded', 'true')
        expect(await screen.findByText('Matches my skin perfectly')).toBeVisible()
        expect(screen.getByText('kayleigh')).toBeVisible()
    })

    it('shows a message when there are no comments', async () => {
        const user = userEvent.setup()

        render(<ProductCard recommendation={makeSummary()} />)
        await user.click(screen.getByRole('button', { name: /Community comments/ }))

        expect(await screen.findByText('No comments yet.')).toBeVisible()
    })
})