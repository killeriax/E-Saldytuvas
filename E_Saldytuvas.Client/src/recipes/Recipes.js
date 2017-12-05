import React, { Component } from 'react';
import RecipesList from "./RecipesList";
import {Button} from "react-bootstrap";

class Recipes extends Component {

    goTo(route) {
        this.props.history.replace(`/${route}`)
    }

    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h1>
                            Receptai
                        </h1>
                    )
                }
                {
                    isAuthenticated() && (
                        <Button bsStyle="success"
                                className="btn-margin"
                                onClick={this.goTo.bind(this, 'newrecipe')}>Naujas receptas</Button>
                    )
                }
                {
                    isAuthenticated() && (
                        <p></p>
                    )
                }
                {
                    isAuthenticated() && (
                        <RecipesList/>
                    )
                }
            </div>
        );
    }
}

export default Recipes;
