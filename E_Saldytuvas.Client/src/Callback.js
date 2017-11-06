import React, {Component} from 'react';
import loading from './loading.svg';
//import {getBackEndUrl} from './environment';

class Callback extends Component {
    // Reikia sutvarkyti
    /*componentDidMount() {
        const params = {
            method: 'POST',
            headers: headers || new Headers(),
            mode: 'cors'
        };

        if(isAuthorized) {
            const accessToken = localStorage.getItem('access_token');
            params.headers.set('Authorization', `Bearer ${accessToken}`);
        }

        fetch(`${getBackEndUrl()}/api/users/register`, params);
    }*/

    render() {
        const style = {
            position: 'absolute',
            display: 'flex',
            justifyContent: 'center',
            height: '100vh',
            width: '100vw',
            top: 0,
            bottom: 0,
            left: 0,
            right: 0,
            backgroundColor: 'white',
        }

        return (
            <div style={style}>
                <img src={loading} alt="loading"/>
            </div>
        );
    }
}

export default Callback;