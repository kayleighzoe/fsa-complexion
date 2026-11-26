import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { beforeEach, describe, expect, it, vi } from 'vitest'
import type { AxiosResponse } from 'axios'
import { ROUTES } from '@/constants/routes'
import { login } from '../../services/api/auth'
import { LoginPage } from '.';

const { mockNavigate, mockRefreshUser } = vi.hoisted(() => ({
    mockNavigate: vi.fn(),
    mockRefreshUser: vi.fn(),
}))

vi.mock('react-router', async (importOriginal) => ({
    ...(await importOriginal<typeof import('react-router')>()),
    useNavigate: () => mockNavigate,
}))
vi.mock('../../services/api/auth', () => ({ login: vi.fn() }))
vi.mock('../../context/AuthContext/index', () => ({
    useAuth: () => ({ refreshUser: mockRefreshUser }),
}))

describe('LoginPage', () => {
    beforeEach(() => {
        vi.clearAllMocks()
    })

    it('logs in and navigates to home screen', async () => {
        vi.mocked(login).mockResolvedValue({} as AxiosResponse)
        mockRefreshUser.mockResolvedValue(undefined)
        const user = userEvent.setup()
        render(<LoginPage />)

        await user.type(screen.getByLabelText('Username / Email'), 'kay')
        await user.type(screen.getByLabelText('Password'), 'secret')
        await user.click(screen.getByRole('button', { name: 'Log in' }))

        await waitFor(() => expect(mockNavigate).toHaveBeenCalledWith(ROUTES.home))
        expect(login).toHaveBeenCalledWith({ usernameOrEmail: 'kay', password: 'secret' })
        expect(mockRefreshUser).toHaveBeenCalledOnce()
    })

    it('navigates to the register screen when Create account is clicked', async () => {
        const user = userEvent.setup()

        render(<LoginPage />)
        await user.click(screen.getByRole('button', { name: 'Create account' }))

        expect(mockNavigate).toHaveBeenCalledWith(ROUTES.register)
    })
})