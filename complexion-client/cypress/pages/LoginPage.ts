class Login {
    elements = {
        loginScreen: () => cy.visit('/login'),
        usernameInput: () => cy.get('[data-cy="login-username"]'),
        passwordInput: () => cy.get('[data-cy="login-password"]'),
        submitButton: () => cy.get('[data-cy="login-submit"]'),
        createAccountButton: () => cy.get('[data-cy="login-create-account"]'),
        errorMessage: () => cy.get('.group\/toast > .h-full'),
        usernameError: () => cy.get('[data-cy="login-username-error"]'),
        passwordError: () => cy.get('[data-cy="login-password-error"]'),
    }

    NavigateToLoginScreen() {
        this.elements.loginScreen();
    }

    EnterCredentials(usernameOrEmail: string, password: string) {
        this.elements.usernameInput().type(usernameOrEmail);
        this.elements.passwordInput().type(password);
    }

    ClickLogin() {
        this.elements.submitButton().click();
    }

    Login(usernameOrEmail: string, password: string) {
       this.EnterCredentials(usernameOrEmail, password);
       this.ClickLogin(); 
    }
}

export default Login;