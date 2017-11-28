import React from 'react';
import { Route, Router } from 'react-router-dom';
import App from './App';
import Home from './home/Home';
import auth from './auth/Auth';
import history from './history';
import Authenticate from "./auth/Authenticate";


export const makeMainRoutes = () => {
    return (
        <Router history={history} component={App}>
            <div>
                <Route path="/" render={(props) => <App auth={auth} {...props} />} />
                <Route path="/home" render={(props) => <Home auth={auth} {...props} />} />
                <Route path="/authenticate" component={Authenticate}/>
            </div>
        </Router>
    );
}
