import auth0 from 'auth0-js';

export default class Auth {
    auth0 = new auth0.WebAuth({
        domain: 'e-saldytuvas.eu.auth0.com',
        clientID: 'Do0KVo9zhqu9Bt0njUyqE7RKh08lIqH4',
        redirectUri: 'http://localhost:3000/callback',
        audience: 'https://e-saldytuvas.eu.auth0.com/api/v2/',
        responseType: 'token id_token',
        scope: 'openid'
    });

    login() {
        this.auth0.authorize();
    }
}