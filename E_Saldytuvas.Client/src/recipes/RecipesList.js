import React, { Component } from 'react';

class RecipesList extends Component {

    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h4>
                            Receptai
                        </h4>
                    )
                }
            </div>
        );
    }
}

export default RecipesList;
