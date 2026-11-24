import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router'
import { beforeEach, describe, expect, it, vi } from 'vitest'
import { ROUTES } from '../../constants/routes'
import { getSkinShades, getSkinUndertones } from '../../services/api/skin'
import { CreateAccountPage } from './index';
import type { AxiosResponse } from 'axios'
import { login, register } from '../../services/api/auth'

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
        sessionStorage.clear()
        vi.clearAllMocks()
        vi.mocked(getSkinShades).mockResolvedValue([])
        vi.mocked(getSkinUndertones).mockResolvedValue([])
    })

    it('shows validation messages when signing up with an empty form', async () => {
        const user = userEvent.setup()

        renderPage()
        expect(screen.queryByText('First name is required.')).not.toBeInTheDocument()
        await user.click(screen.getByRole('button', { name: 'Sign up' }))

        expect(await screen.findByText('First name is required.')).toBeInTheDocument()
        expect(screen.getByText('Please choose a skin shade.')).toBeInTheDocument()
        expect(screen.getByText('Please choose a skin undertone.')).toBeInTheDocument()
    })

    it('navigates to the login screen when Return to login is clicked', async () => {
        const user = userEvent.setup()

        renderPage()
        await user.click(screen.getByRole('button', { name: 'Return to login' }))

        expect(mockNavigate).toHaveBeenCalledWith(ROUTES.login)
    })

    it('registers the user and navigates home when the form is filled in correctly', async () => {
        vi.mocked(getSkinShades).mockResolvedValue([
            { shadeId: 1, name: 'Fair' },
            { shadeId: 2, name: 'Medium' },
        ])
        vi.mocked(getSkinUndertones).mockResolvedValue([
            { undertoneId: 1, name: 'Cool' },
            { undertoneId: 2, name: 'Warm' },
        ])
        vi.mocked(register).mockResolvedValue({} as AxiosResponse)
        vi.mocked(login).mockResolvedValue({} as AxiosResponse)
        mockRefreshUser.mockResolvedValue(undefined)
        const user = userEvent.setup()

        renderPage()
        await screen.findByRole('option', { name: 'Medium' })
        await user.type(screen.getByLabelText('Name'), 'Kay')
        await user.type(screen.getByLabelText('Surname'), 'James')
        await user.type(screen.getByLabelText('Email'), 'kay@example.com')
        await user.selectOptions(screen.getByLabelText('Skin shade'), 'Medium')
        await user.selectOptions(screen.getByLabelText('Skin undertone'), 'Cool')
        await user.click(screen.getByLabelText('Olive overtone'))
        await user.type(screen.getByLabelText('Username'), 'kayleigh')
        await user.type(screen.getByLabelText('Password'), 'Str0ng!Passw0rd#')
        await user.click(screen.getByRole('button', { name: 'Sign up' }))

        await waitFor(() => expect(mockNavigate).toHaveBeenCalledWith(ROUTES.home))
        expect(register).toHaveBeenCalledWith({
            firstName: 'Kay',
            surname: 'James',
            email: 'kay@example.com',
            username: 'kayleigh',
            password: 'Str0ng!Passw0rd#',
            shadeId: 2,
            undertoneId: 1,
            hasTint: true,
        })
        expect(login).toHaveBeenCalledWith({
            usernameOrEmail: 'kayleigh',
            password: 'Str0ng!Passw0rd#',
        })
    })
})