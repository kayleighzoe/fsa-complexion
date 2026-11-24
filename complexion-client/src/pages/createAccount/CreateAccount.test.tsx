import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router'
import { beforeEach, describe, expect, it, vi } from 'vitest'
import { ROUTES } from '../../constants/routes'
import { getSkinShades, getSkinUndertones } from '../../services/api/skin'
import { CreateAccountPage } from './index';

const { mockNavigate, mockRefreshUser } = vi.hoisted(() => ({
    mockNavigate: vi.fn(),
    mockRefreshUser: vi.fn(),
}))

vi.mock('react-router', async (importOriginal) => ({
    ...(await importOriginal<typeof import('react-router')>()),
    useNavigate: () => mockNavigate,
}))
vi.mock('../../services/api/auth', () => ({ register: vi.fn(), login: vi.fn() }))
vi.mock('../../services/api/skin', () => ({
    getSkinShades: vi.fn(),
    getSkinUndertones: vi.fn(),
}))
vi.mock('../../context/AuthContext/index', () => ({
    useAuth: () => ({ refreshUser: mockRefreshUser }),
}))

function renderPage() {
    return render(
        <MemoryRouter>
            <CreateAccountPage />
        </MemoryRouter>,
    )
}

describe('CreateAccountPage', () => {
    beforeEach(() => {
        vi.clearAllMocks()
        vi.mocked(getSkinShades).mockResolvedValue([])
        vi.mocked(getSkinUndertones).mockResolvedValue([])
    })

    it('shows a message when signing up without choosing a shade and undertone', async () => {
        const user = userEvent.setup()

        renderPage()
        await user.click(screen.getByRole('button', { name: 'Sign up' }))

        expect(
            await screen.findByText('Please choose a skin shade and undertone.'),
        ).toBeInTheDocument()
    })

    it('navigates to the login screen when Return to login is clicked', async () => {
        const user = userEvent.setup()

        renderPage()
        await user.click(screen.getByRole('button', { name: 'Return to login' }))

        expect(mockNavigate).toHaveBeenCalledWith(ROUTES.login)
    })
})