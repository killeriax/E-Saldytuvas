import React, {Component} from 'react';
import logo from './logo.svg';
import './App.css';
import Auth from './auth/Auth';
import {getBackEndUrl} from './environment';
import {Button} from 'react-bootstrap';

class App extends Component {
    state = {
        zmones: [{name: 'zzz'}]
    };
    auth = new Auth();

    async componentDidMount() {
        const params = {
          mode: 'cors'
        };
        const response = await fetch(`${getBackEndUrl()}/api/users`, params);
        const json = await response.json();
        console.log(json);

        this.setState({zmones: json});
    }

    render() {
        return (
            <div className="App">
                <header className="App-header">
                    <img src={logo} className="App-logo" alt="logo"/>
                    <h1 className="App-title">Welcome to React</h1>
                </header>
                <p className="App-intro">
                    To get started, edit <code>src/App.js</code> and save to reload.
                </p>
                <div>
                    {this.state.zmones.map((zmogus, i) => (
                        <div key={i}>{zmogus.name}</div>
                    ))}
                </div>
                <Button bsStyle="primary" bsSize="large" onClick={() => this.auth.login()}>Click me</Button>
            </div>
        );
    }
}

export default App;
