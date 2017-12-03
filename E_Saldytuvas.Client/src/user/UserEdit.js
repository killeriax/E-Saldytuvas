import React, { Component } from 'react';
import UserEditForm from './UserEditForm';

class UserEdit extends Component {
    /*login() {
        this.props.auth.login();
    }*/
    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h4>
                            Profilio redagavimas
                        </h4>
                    ) &&
                    (
                        <UserEditForm/>
                    )
                }
            </div>
        );
    }
}

export default UserEdit;
