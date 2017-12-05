import React, { Component } from 'react';
import { Navbar, Button, Nav, NavItem } from 'react-bootstrap';
import './App.css';
import Background from './images/index.jpeg';
import Icon from './images/refrigerator.svg';
import windowSize from 'react-window-size';

class App extends Component {
    goTo(route) {
        this.props.history.replace(`/${route}`)
    }

    login() {
        this.props.auth.login();
    }

    logout() {
        this.props.auth.logout();
    }

    render() {
        const { isAuthenticated } = this.props.auth;

        var divStyle = {};

        if(!isAuthenticated()) {
            divStyle = {
                width: this.props.windowWidth,
                height: this.props.windowHeight,
                backgroundImage: "url(" + Background + ")",
                backgroundSize: "cover"
            };
        }

        var namePosition = {
            marginTop: "15%",
            marginLeft: "30%"
        }

        var titleStyle = {
            fontFamily: "'Press Start 2P', cursive"
        }

        return (
            <div style={ divStyle }>
                <Navbar inverse collapseOnSelect>
                    <Navbar.Header>
                        <Navbar.Brand>
                            <p><a href="" style={titleStyle}><img alt="Puslapio ikona" src={Icon} height="40px"></img> e-Šaldytuvas</a></p>
                        </Navbar.Brand>
                        <Navbar.Toggle />
                    </Navbar.Header>
                    <Navbar.Collapse>
                        <Nav>
                            <NavItem><Button
                                bsStyle="success"
                                className="btn-margin"
                                onClick={this.goTo.bind(this, 'home')}
                            >
                                Pradžia
                            </Button></NavItem>
                            {
                                !isAuthenticated() && (
                                    <NavItem><Button
                                        bsStyle="success"
                                        className="btn-margin"
                                        onClick={this.login.bind(this)}
                                    >
                                        Prisijungti
                                    </Button>
                                    </NavItem>
                                )
                            }
                            {
                                isAuthenticated() && (
                                    <NavItem><Button
                                        bsStyle="success"
                                        className="btn-margin"
                                        onClick={this.goTo.bind(this, 'useredit')}
                                    >
                                        Profilis</Button>
                                    </NavItem>
                                )
                            }
                            {
                                isAuthenticated() && (
                                    <NavItem><Button
                                        bsStyle="success"
                                        className="btn-margin"
                                        onClick={this.goTo.bind(this, 'products')}
                                    >
                                        Produktai</Button>
                                    </NavItem>
                                )
                            }
                            {
                                isAuthenticated() && (
                                    <NavItem><Button
                                        bsStyle="success"
                                        className="btn-margin"
                                        onClick={this.goTo.bind(this, 'recipes')}
                                    >
                                        Receptai</Button>
                                    </NavItem>
                                )
                            }
                            {
                                isAuthenticated() && (
                                    <NavItem><Button
                                        bsStyle="success"
                                        className="btn-margin"
                                        onClick={this.goTo.bind(this, 'userrecipes')}
                                    >
                                        Mano receptai</Button>
                                    </NavItem>
                                )
                            }
                        </Nav>
                        {
                            isAuthenticated() && (
                                <Nav pullRight>
                                    <NavItem><Button
                                        bsStyle="warning"
                                        className="btn-margin"
                                        onClick={this.logout.bind(this)}
                                    >
                                        Atsijungti
                                    </Button></NavItem>
                                </Nav>
                            )
                        }
                    </Navbar.Collapse>
                </Navbar>
                {
                    !isAuthenticated() && (
                        <h1 style={namePosition}>e-Šaldytuvas</h1>
                    )
                }
            </div>
        );
    }
}

//export default App;
export default windowSize(App);