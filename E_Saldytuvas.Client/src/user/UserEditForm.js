import React, { Component } from 'react';
import {Form, FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';
import {editUser, getUserId, getCurrentUser} from "../actions/UserActions";

class UserEditForm extends Component {
    state = {
        firstName: '',
        lastName: '',
        email: ''
    };

    async componentDidMount() {
        const data = await getCurrentUser();

        this.setState({
            firstName: data.name,
            lastName: data.surname
        })
    }

    handleSubmit = async () => {
        const userId = await getUserId();
        editUser(userId, this.state.firstName, this.state.lastName);
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

    render() {
        return (
            <div className="container">
                <h1>Hello</h1>
                <Form>
                    <FormGroup controlId="firstName">
                        <ControlLabel>
                            Vardas
                        </ControlLabel>
                        <FormControl type="text" placeholder="Vardas" value={this.state.firstName} onChange={this.handleFirstNameChange} />
                    </FormGroup>

                    <FormGroup controlId="lastName">
                        <ControlLabel>
                            Pavardė
                        </ControlLabel>
                        <FormControl type="text" placeholder="Pavardė" value={this.state.lastName} onChange={this.handleLastNameChange}/>
                    </FormGroup>
                    <Button bsStyle="primary" onClick={this.handleSubmit}>
                        Išsaugoti
                    </Button>
                </Form>
            </div>
        );
    }
}

export default UserEditForm;
