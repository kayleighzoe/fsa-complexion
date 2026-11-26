interface RegistrationDetails {
    firstName: string;
    surname: string;
    email: string;
    username: string;
    password: string;
    shade: string;
    undertone: string;
    hasOliveOvertone: boolean;
}

class CreateAccount {
    elements = {
        createAccountScreen: () => cy.visit('/register'),
        firstNameInput: () => cy.get('[data-cy="register-first-name"]'),
        surnameInput: () => cy.get('[data-cy="register-surname"]'),
        emailInput: () => cy.get('[data-cy="register-email"]'),
        skinShadeSelect: () => cy.get('[data-cy="register-skin-shade"]'),
        skinUndertoneSelect: () => cy.get('[data-cy="register-skin-undertone"]'),
        oliveOvertoneCheckbox: () => cy.get('[data-cy="register-olive-overtone"]'),
        usernameInput: () => cy.get('[data-cy="register-username"]'),
        passwordInput: () => cy.get('[data-cy="register-password"]'),
        signUpButton: () => cy.get('[data-cy="register-submit"]'),
        returnToLoginButton: () => cy.get('[data-cy="register-return-to-login"]'),
    }

    NavigateToRegistration() {
        this.elements.createAccountScreen();
    }

    CompleteForm(details: RegistrationDetails) {
        this.elements.firstNameInput().type(details.firstName);
        this.elements.surnameInput().type(details.surname);
        this.elements.emailInput().type(details.email);
        this.elements.skinShadeSelect().select(details.shade);
        this.elements.skinUndertoneSelect().select(details.undertone);
        if (details.hasOliveOvertone) {
            this.elements.oliveOvertoneCheckbox().check();
        }
        this.elements.usernameInput().type(details.username);
        this.elements.passwordInput().type(details.password);
    }

    ClickSignUp() {
        this.elements.signUpButton().click();
    }

    ClickReturnToLogin() {
        this.elements.returnToLoginButton().click();
    }
}

export default CreateAccount;