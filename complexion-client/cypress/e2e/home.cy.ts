import Home from '../pages/HomePage';

describe('Home search', () => {

    const home = new Home();

    beforeEach(function () {
        home.NavigateToHomeScreen();
    })

    it('shows the empty message when the search matches no products', () => {
        home.elements.title().should('be.visible');

        home.Search('zzzzqqqq');

        home.elements.emptyMessage().should('be.visible');
        home.elements.productCards().should('not.exist');
    })

})