import React, { Component } from 'react';
import {getUserProducts} from "../actions/ProductActions";
import {Button, Form, FormControl, Glyphicon, Modal, ProgressBar} from "react-bootstrap";
import {getUserId} from "../actions/UserActions";

class UserProductsList extends Component {

    state = {
        userProducts: [],
        showModal: []
    };

    open = index => {
        this.setState({showModal:{
                [index] : true
            }});
    };

    close = index => {
        this.setState({showModal: {
                [index] : false
            }});
    };

    async componentWillMount() {
        const userId = await getUserId();
        let products = await getUserProducts(userId);
        if(products === undefined)
            products = [];
        this.setState({
            userProducts: products,
            showModal: products.map(() => false)
        });
    }

    deleteProduct(productId) {
        let approve = window.confirm("Paspaudus OK, produktas bus ištrintas!");

        if (approve === true) {
            alert("Produktas pašalintas sėkmingai.");
            this.setState({
                userProducts: this.state.userProducts.filter(userProduct => userProduct.id !== productId)
            });
        }
    }

    render() {
        let products = this.state.userProducts;

        if (!products) {
            return <ProgressBar bsStyle="success" active now={45} />
        }

        if (products.length <= 0) {
            return (
                <tr>
                    <td colSpan={7}>
                        Nėra nurodytų produktų.
                    </td>
                </tr>
            );
        }

        return products.map((product, index) => {
                return <tr key={index}>
                    <td>{index + 1}</td>
                    <td> {product.name} </td>
                    <td> {product.amount} </td>
                    <td> {product.measure.name} </td>
                    <td><p><a className="btn btn-success" onClick={() => this.open(index)}><Glyphicon glyph="edit"/></a>
                        <a className="btn btn-danger" onClick={() => this.deleteProduct(product.id)}><Glyphicon glyph="remove"/></a></p></td>
                    <Modal show={this.state.showModal[index]} onHide={() => this.close(index)}>
                        <Modal.Header closeButton>
                            <Modal.Title> Redaguoti produktą </Modal.Title>
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
                                    Išsaugoti
                                </Button>
                            </Form>
                        </Modal.Body>
                    </Modal>
                </tr>
            }
        );
    }
}

export default UserProductsList;
