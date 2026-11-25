import CreateAccount from '../pages/CreateAccountPage';
import Home from '../pages/HomePage';

describe('Create Account', () => {

    const createAccount = new CreateAccount();
    const home = new Home();

    beforeEach(function () {
        cy.fixture('registrationDetails').as('registerDetails');
        cy.fixture('uiText').as('uiText');
        createAccount.NavigateToRegistration();
    })

    it('shows validation messages when the form is submitted empty', function () {
        createAccount.ClickSignUp();

        cy.contains(this.uiText.firstNameRequired).should('be.visible');
        cy.contains(this.uiText.shadeRequired).should('be.visible');
        cy.contains(this.uiText.undertoneRequired).should('be.visible');
    })

    it('creates account for user', () => {
        const username = `cyreg${Date.now()}`;

        createAccount.CompleteForm({
            firstName: 'Cypress',
            surname: 'Tester',
            email: `${username}@email.com`,
            username,
            password: 'TestP@ssword2026',
            shade: 'Tan',
            undertone: 'Cool',
            hasOliveOvertone: true,
        });
        createAccount.ClickSignUp();

        home.elements.searchBar().should('be.visible');
    })

})