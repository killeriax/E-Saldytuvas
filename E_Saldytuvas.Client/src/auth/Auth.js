import history from '../history';
import auth0 from 'auth0-js';
import { getBackEndUrl } from "../environment";

class Auth {
    auth0 = new auth0.WebAuth({
        domain: 'e-saldytuvas.eu.auth0.com',
        clientID: 'Do0KVo9zhqu9Bt0njUyqE7RKh08lIqH4',
        redirectUri: 'http://localhost:3000/authenticate',
        audience: 'https://e-saldytuvas.eu.auth0.com/api/v2/',
        responseType: 'token id_token',
        scope: 'openid'
    });

    constructor() {
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
        //this.handleAuthentication = this.handleAuthentication.bind(this);
        this.isAuthenticated = this.isAuthenticated.bind(this);
        this.scheduleRenewal();
    }

    login() {
        this.auth0.authorize();
    }

    /*handleAuthentication() {
        this.auth0.parseHash((err, authResult) => {
            if (authResult && authResult.accessToken && authResult.idToken) {
                this.setSession(authResult);
                history.replace('/home');
            } else if (err) {
                history.replace('/home');
                console.log(err);
                alert(`Error: ${err.error}. Check the console for further details.`);
            }
        });
    }*/

    handleAuthentication = () => {
        return new Promise((resolve) => {
            this.auth0.parseHash((err, authResult) => {
                if (authResult && authResult.accessToken && authResult.idToken) {
                    this.setSession(authResult);
                    resolve(true);
                } else if (err) {
                    console.error('Could not handle authentication', err);
                    resolve(false);
                }
                resolve(false);
            });
        });
    };

    setSession(authResult) {
        // Set the time that the access token will expire at
        let expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
        localStorage.setItem('access_token', authResult.accessToken);
        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem('expires_at', expiresAt);

        // schedule a token renewal
        this.scheduleRenewal();

        // navigate to the home route
        history.replace('/home');
    }

    logout() {
        // Clear access token and ID token from local storage
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        // navigate to the home route
        history.replace('/home');

        clearTimeout(this.tokenRenewalTimeout);
    }

    isAuthenticated() {
        // Check whether the current time is past the
        // access token's expiry time
        let expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }

    renewToken() {
        this.auth0.renewAuth(
            {
                audience: 'https://e-saldytuvas.eu.auth0.com/api/v2/',
                redirectUri: `${getBackEndUrl()}/api/silent`,
                usePostMessage: true
            }, (err, result) => {
                if (err) {
                    console.log(err);
                } else {
                    this.setSession(result);
                }
            }
        );
    }

    scheduleRenewal() {
        const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        const delay = expiresAt - Date.now();
        if (delay > 0) {
            this.tokenRenewalTimeout = setTimeout(() => {
                this.renewToken();
            }, delay);
        }
    }
}

export default new Auth();