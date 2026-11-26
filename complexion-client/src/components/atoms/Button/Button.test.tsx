import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { describe, expect, it, vi } from 'vitest'

import { Button } from '.';

describe('Button', () => {

    it('renders its children', () => {
        render(<Button>Save</Button>)

        expect(screen.getByRole('button', { name: 'Save' })).toBeInTheDocument()
    })

    it('calls onClick when clicked', async () => {
        const mockOnClick = vi.fn()
        const user = userEvent.setup()

        render(<Button onClick={mockOnClick}>Save</Button>)

        await user.click(screen.getByRole('button', { name: 'Save' }))

        expect(mockOnClick).toHaveBeenCalledOnce()
    })

    it('applies a custom className', () => {
        render(<Button className="w-full">Save</Button>)

        expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('w-full')
    })

})