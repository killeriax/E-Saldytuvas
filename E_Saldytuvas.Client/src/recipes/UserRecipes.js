import React, { Component } from 'react';
import UserRecipesList from "./UserRecipesList";
import {Button} from "react-bootstrap";

class UserRecipes extends Component {

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
                            Mano receptai
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
                        <UserRecipesList {...this.props}/>
                    )
                }
            </div>
        );
    }
}

export default UserRecipes;
