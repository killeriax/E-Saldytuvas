import React from 'react';
import auth from './Auth';
import {Redirect} from 'react-router-dom';
import {ProgressBar} from 'react-bootstrap';

import {registerUser} from '../actions/UserActions';

class Authenticate extends React.Component {
    state = {
        isAuthenticated: false
    };

    async componentDidMount() {
        const isAuthenticated = await auth.handleAuthentication();

        if (isAuthenticated) {
            await registerUser();
        }

        this.setState({isAuthenticated});
    }

    render() {
        const {isAuthenticated} = this.state;

        return isAuthenticated ? <Redirect to={{
            pathname: '/home'
        }}/> : <ProgressBar bsStyle="success" active now={50} />;
    }
}

export default Authenticate;