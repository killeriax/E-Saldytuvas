import React, { Component } from 'react';
import {getUserRecipes} from "../actions/RecipeActions";
import {Col, Glyphicon, Modal, ProgressBar, Well} from "react-bootstrap";
import {getUserId} from "../actions/UserActions";
import NewRecipeForm from "./NewRecipeForm";

class UserRecipesList extends Component {

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
        const userId = await getUserId();
        const recipes = await getUserRecipes(userId);
        this.setState({
            userRecipes: recipes,
            showModal: recipes.map(() => false)
        });
    }

    deleteRecipe(recipeId) {
        let approve = window.confirm("Paspaudus OK, receptas bus ištrintas!");

        if (approve === true) {
            alert("Receptas pašalintas sėkmingai.");
            this.setState({
                userRecipes: this.state.userRecipes.filter(userRecipe => userRecipe.id !== recipeId)
            });
        }
    }

    paintImage(recipe) {
        if (recipe.imageUrl) {
            return <img alt="Patiekalo nuotrauka" height="200" width="100%" src={`${recipe.imageUrl}`} />;
        }
        else
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
                            <p><a className="btn btn-success" onClick={() => this.open(index)}><Glyphicon glyph="edit"/> Redaguoti </a>
                                <a className="btn btn-danger" onClick={() => this.deleteRecipe(recipe.id)}><Glyphicon glyph="remove"/></a></p>
                        </Well>
                    </Col>
                    <Modal show={this.state.showModal[index]} onHide={() => this.close(index)}>
                        <Modal.Header closeButton>
                            <Modal.Title> Redaguoti receptą </Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <NewRecipeForm edit={true} title={recipe.title} description={recipe.description} {...this.props}/>
                        </Modal.Body>
                    </Modal>
                </div>
        }

        );
    }
}

export default UserRecipesList;
