import React, { Component } from 'react';
import {Form, FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';
import {editUser, getUserId, getCurrentUser} from "../actions/UserActions";

class UserEditForm extends Component {
    state = {
        firstName: undefined,
        lastName: undefined,
        email: undefined
    };

    async componentDidMount() {
        const data = await getCurrentUser();

        this.setState({
            firstName: data.name,
            lastName: data.surname,
            email: data.email
        })
    }

    handleSubmit = async () => {
        const userId = await getUserId();
        editUser(userId, this.state.firstName, this.state.lastName, this.state.email);
    };

    handleFirstNameChange = e => {
        this.setState({
            firstName: e.target.value
        });
    };

    handleLastNameChange = e => {
        this.setState({
            lastName: e.target.value
        });
    };

    handleEmailChange = e => {
        this.setState({
            email: e.target.value
        });
    };

    render() {
        return (
            <div className="container">
                <Form>
                    <FormGroup controlId="firstName">
                        <ControlLabel>
                            Vardas
                        </ControlLabel>
                        <FormControl type="text" placeholder="Vardas" value={this.state.firstName || ''} onChange={this.handleFirstNameChange} />
                    </FormGroup>

                    <FormGroup controlId="lastName">
                        <ControlLabel>
                            Pavardė
                        </ControlLabel>
                        <FormControl type="text" placeholder="Pavardė" value={this.state.lastName || ''} onChange={this.handleLastNameChange}/>
                    </FormGroup>

                    <FormGroup controlId="email">
                        <ControlLabel>
                            Elektroninis paštas
                        </ControlLabel>
                        <FormControl type="email" placeholder="Elektroninis paštas" value={this.state.email || ''} onChange={this.handleEmailChange}/>
                    </FormGroup>
                    <Button bsStyle="success" onClick={this.handleSubmit}>
                        Išsaugoti
                    </Button>
                </Form>
            </div>
        );
    }
}

export default UserEditForm;
