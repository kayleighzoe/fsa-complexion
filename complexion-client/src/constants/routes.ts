export const ROUTES = {
    home: '/',
    login: '/login',
    register: '/register',
    guide: '/guide',
    profile: '/profile',
    editProfile: '/profile/edit',
    share: '/share',
    notFound: '*',
    editRecommendation: '/recommendations/:recommendationId/edit',
} as const;