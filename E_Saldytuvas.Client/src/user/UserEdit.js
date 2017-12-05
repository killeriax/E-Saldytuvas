import React, { Component } from 'react';
import UserEditForm from './UserEditForm';

class UserEdit extends Component {
    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                <h1>
                    Profilio redagavimas
                </h1>
                {
                    isAuthenticated() && (
                        <UserEditForm/>
                    )
                }
            </div>
        );
    }
}

export default UserEdit;
