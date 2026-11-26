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

    it('creates account for user', function () {
        createAccount.CompleteForm({
            firstName: this.registerDetails.firstName,
            surname: this.registerDetails.surname,
            email: this.registerDetails.email,
            username: this.registerDetails.username,
            password: this.registerDetails.password,
            shade: this.registerDetails.shade,
            undertone: this.registerDetails.undertone,
            hasOliveOvertone: this.registerDetails.hasOliveOvertone,
        });
        createAccount.ClickSignUp();

        home.elements.searchBar().should('be.visible');
    })

})