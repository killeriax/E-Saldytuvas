import React, { Component } from 'react';
import {Button, Form, FormControl, Modal, Table} from "react-bootstrap";
import UserProductsList from "./UserProductsList";

class UserProducts extends Component {

    state = {
        show: false
    };

    open = () => {
        this.setState({
            show: true
        })
    };

    close = () => {
        this.setState({
            show: false
        })
    };

    goTo(route) {
        this.props.history.replace(`/${route}`)
    }

    render() {
        const { isAuthenticated } = this.props.auth;
        return (
            <div className="container">
                {
                    isAuthenticated() && (
                        <h1>
                            Mano turimi produktai
                        </h1>
                    )
                }
                {
                    isAuthenticated() && (
                        <Button bsStyle="success"
                                className="btn-margin"
                                onClick={() => this.open()}>Naujas produktas</Button>
                    )
                }
                {
                    isAuthenticated() && (
                        <p></p>
                    )
                }

                {
                    isAuthenticated() && (
                        <Table condensed hover>
                            <thead>
                            <tr>
                                <th>#</th>
                                <th>Ingredientas</th>
                                <th>Kiekis</th>
                                <th>Matas</th>
                                <th></th>
                            </tr>
                            </thead>
                            <tbody>
                            <UserProductsList />
                            </tbody>
                        </Table>
                    )
                }
                <Modal show={this.state.show} onHide={() => this.close()}>
                    <Modal.Header closeButton>
                        <Modal.Title> Pridėti produktą </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form inline>
                            <FormControl type="text" placeholder="Pavadinimas" />
                            <FormControl type="number" placeholder="Kiekis" />
                            <FormControl componentClass="select" placeholder="Matas">
                                <option value="kg">kg</option>
                                <option value="other">...</option>
                            </FormControl>
                            {' '}
                            <Button bsStyle="success" onClick={() => {}}>
                                Pridėti
                            </Button>
                        </Form>
                    </Modal.Body>
                </Modal>
            </div>
        );
    }
}

export default UserProducts;
