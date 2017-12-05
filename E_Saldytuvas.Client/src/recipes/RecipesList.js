import React, { Component } from 'react';
import {getRecipes} from "../actions/RecipeActions";
import {Col, ProgressBar, Well} from "react-bootstrap";

class RecipesList extends Component {

    state = {
        userRecipes: null
    }

    async componentWillMount() {
        const recipes = await getRecipes();
        debugger
        this.setState({
            userRecipes: recipes
        });
    }

    paintImage(recipe) {
        if (recipe.imageUrl) {
            return <img alt="Patiekalo nuotrauka" height="200" width="100%" src={`${recipe.imageUrl}`} />;
        }
        return <img alt="Patiekalo nuotrauka" height="100%" width="100%" src={`https://static.pexels.com/photos/376464/pexels-photo-376464.jpeg`} />;
    }

    render() {
        let recipes = this.state.userRecipes;
        debugger;
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
                            <a className="btn btn-success" onClick=""> Detaliau </a>
                        </Well>
                    </Col>
                </div>
            }

        );
    }
}

export default RecipesList;
