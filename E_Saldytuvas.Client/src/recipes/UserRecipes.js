import React, { Component } from 'react';
import RecipesList from "./RecipesList";

class UserRecipes extends Component {
    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h4>
                            Mano receptai
                        </h4>
                    ) &&
                    (
                        <RecipesList/>
                    )
                }
            </div>
        );
    }
}

export default UserRecipes;
