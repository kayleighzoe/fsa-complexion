import Login from '../pages/LoginPage';
import loginDetails from '../fixtures/loginDetails.json';
import Home from '../pages/HomePage';

describe('Complexion Login', () => {

  const login = new Login();
  const home = new Home();

  beforeEach(function () {
    cy.fixture('loginDetails').as('loginDetails');
    cy.fixture('uiText').as('uiText');
    login.NavigateToLoginScreen();
  });

  it('logs user in', () => {
    login.Login(loginDetails.username, loginDetails.password);
    home.elements.searchBar().should('be.visible');
  });
})