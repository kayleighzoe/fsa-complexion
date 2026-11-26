import { describe, expect, it } from 'vitest'
import { validateRegister } from './validateRegister'
import type { RegisterFormValues } from './validateRegister'

function validValues(overrides: Partial<RegisterFormValues> = {}): RegisterFormValues {
    return {
        firstName: 'Kay',
        surname: 'James',
        email: 'kay@example.com',
        username: 'kayleigh',
        password: 'Str0ng!Passw0rd#',
        shadeName: 'Medium',
        undertoneName: 'Cool',
        ...overrides,
    }
}

describe('validateRegister', () => {
    it('returns no errors when every field is valid', () => {
        const errors = validateRegister(validValues())

        expect(Object.values(errors).filter(Boolean)).toEqual([])
    })

    it('rejects a username that does not start with a letter', () => {
        const errors = validateRegister(validValues({ username: '1kayleigh' }))

        expect(errors.username).toBe('Username must start with a letter.')
    })

    it('rejects a password that is too short', () => {
        const errors = validateRegister(validValues({ password: 'Sh0rt!Pw' }))

        expect(errors.password).toBe('Password must be at least 12 characters.')
    })

    it('rejects a password with no uppercase letter', () => {
        const errors = validateRegister(validValues({ password: 'str0ng!passw0rd#' }))

        expect(errors.password).toBe('Password must contain an uppercase letter.')
    })
})