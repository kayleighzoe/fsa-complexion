class Home {
    elements ={
        homeScreen: () => cy.visit('/'),
        searchBar: () => cy.get('.mt-10 > .w-full'),
        title: () => cy.get('[data-cy="home-title"]'),
        searchInput: () => cy.get('[data-cy="home-search"]'),
        productCards: () => cy.get('[data-cy="product-card"]'),
        emptyMessage: () => cy.get('[data-cy="home-empty-message"]'),
    }

    NavigateToHomeScreen() {
        this.elements.homeScreen();
    }

    Search(text: string) {
        this.elements.searchInput().type(text);
    }
}

export default Home;