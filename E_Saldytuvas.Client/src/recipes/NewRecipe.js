import React, { Component } from 'react';
import NewRecipeForm from './NewRecipeForm';

class NewRecipe extends Component {
    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                <h1>
                    Naujas receptas
                </h1>
                {
                    isAuthenticated() && (
                        <NewRecipeForm/>
                    )
                }
            </div>
        );
    }
}

export default NewRecipe;
