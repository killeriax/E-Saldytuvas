import React, { Component } from 'react';
import {getRecipes} from "../actions/RecipeActions";
import {Col, Modal, ProgressBar, Well} from "react-bootstrap";

class RecipesList extends Component {

    state = {
        userRecipes: null,
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
        const recipes = await getRecipes();

        this.setState({
            userRecipes: recipes,
            showModal: recipes.map(() => false)
        });
    }

    paintImage(recipe) {
        if (recipe.imageUrl) {
            return <img alt="Patiekalo nuotrauka" height="100%" width="100%" src={`${recipe.imageUrl}`} />;
        }
        return <img alt="Patiekalo nuotrauka" height="100%" width="100%" src={`https://static.pexels.com/photos/376464/pexels-photo-376464.jpeg`} />;
    }

    render() {
        let recipes = this.state.userRecipes;

        if (!recipes) {
            return <ProgressBar bsStyle="success" active now={45} />
        }
        return recipes.map((recipe, index) => {
                return <div key={index}>
                    <Col md={4}>
                        <Well>
                            {this.paintImage(recipe)}
                            <h2> {recipe.title} </h2>
                            <p>Recepto autorius: {recipe.user.name} {recipe.user.surname}</p>
                            <a className="btn btn-success" onClick={() => this.open(index)}> Detaliau </a>
                        </Well>
                    </Col>
                    <Modal show={this.state.showModal[index]} onHide={() => this.close(index)}>
                        <Modal.Header closeButton>
                            <Modal.Title> {recipe.title} </Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            {this.paintImage(recipe)}
                            <p>Recepto autorius: {recipe.user.name} {recipe.user.surname}</p>
                            <p>Aprašymas:</p>
                            <p>{recipe.description}</p>
                        </Modal.Body>
                    </Modal>
                </div>
            }

        );
    }
}

export default RecipesList;
