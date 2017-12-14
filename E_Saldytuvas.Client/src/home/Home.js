import React, { Component } from 'react';

class Home extends Component {
    render() {
        const { isAuthenticated } = this.props.auth;

        var namePosition = {
            marginTop: "15%",
            marginLeft: "35%"
        }

        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h1 style={namePosition}>
                            Sveiki prisijungę!
                        </h1>
                    )
                }
            </div>
        );
    }
}

export default Home;
