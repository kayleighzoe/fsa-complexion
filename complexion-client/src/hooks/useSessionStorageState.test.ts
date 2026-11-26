import { act, renderHook } from '@testing-library/react'
import { beforeEach, describe, expect, it } from 'vitest'
import { clearSessionStorageKeys, useSessionStorageState } from './useSessionStorageState'

describe('useSessionStorageState', () => {
    beforeEach(() => {
        sessionStorage.clear()
    })

    it('starts with the initial value when nothing is stored', () => {
        const { result } = renderHook(() => useSessionStorageState('draft.name', ''))

        expect(result.current[0]).toBe('')
    })

    it('starts with the stored value when one exists', () => {
        sessionStorage.setItem('draft.name', JSON.stringify('Kay'))

        const { result } = renderHook(() => useSessionStorageState('draft.name', ''))

        expect(result.current[0]).toBe('Kay')
    })

    it('saves the new value to sessionStorage when it changes', () => {
        const { result } = renderHook(() => useSessionStorageState('draft.name', ''))

        act(() => {
            result.current[1]('Kay')
        })

        expect(result.current[0]).toBe('Kay')
        expect(sessionStorage.getItem('draft.name')).toBe(JSON.stringify('Kay'))
    })

    it('falls back to the initial value when the stored value is not valid JSON', () => {
        sessionStorage.setItem('draft.name', 'not json{')

        const { result } = renderHook(() => useSessionStorageState('draft.name', ''))

        expect(result.current[0]).toBe('')
    })
})

describe('clearSessionStorageKeys', () => {
    it('removes only the keys it is given', () => {
        sessionStorage.setItem('a', '1')
        sessionStorage.setItem('b', '2')
        sessionStorage.setItem('c', '3')

        clearSessionStorageKeys(['a', 'b'])

        expect(sessionStorage.getItem('a')).toBeNull()
        expect(sessionStorage.getItem('b')).toBeNull()
        expect(sessionStorage.getItem('c')).toBe('3')
    })
})