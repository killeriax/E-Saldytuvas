import React from 'react';
import { Route, Router } from 'react-router-dom';
import App from './App';
import Home from './home/Home';
import auth from './auth/Auth';
import history from './history';
import Authenticate from "./auth/Authenticate";
import UserEdit from "./user/UserEdit";
import UserRecipes from "./recipes/UserRecipes";
import NewRecipe from "./recipes/NewRecipe";
import Recipes from "./recipes/Recipes";

export const makeMainRoutes = () => {
    return (
        <Router history={history} component={App}>
            <div>
                <Route path="/" render={(props) => <App auth={auth} {...props} />} />
                <Route path="/home" render={(props) => <Home auth={auth} {...props} />} />
                <Route path="/authenticate" component={Authenticate}/>
                <Route path="/useredit" render={(props) => <UserEdit auth={auth} {...props} />} />
                <Route path="/recipes" render={(props) => <Recipes auth={auth} {...props} />} />
                <Route path="/userrecipes" render={(props) => <UserRecipes auth={auth} {...props} />} />
                <Route path="/newrecipe" render={(props) => <NewRecipe auth={auth} {...props} />} />
            </div>
        </Router>
    );
}
