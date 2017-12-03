import React, { Component } from 'react';

class Home extends Component {
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
                            Sveiki prisijungę!
                        </h4>
                    )
                }
                {
                    /*!isAuthenticated() && (
                        <h4>
                          Jūs neprisijungę! Norėdami tęsti, {' '}
                          <a
                            style={{ cursor: 'pointer' }}
                            onClick={this.login.bind(this)}
                          >
                            prisijunkite.
                          </a>
                        </h4>
                      )*/
                }
            </div>
        );
    }
}

export default Home;
