import React, { Component } from 'react';
import {getUserRecipes} from "../actions/RecipeActions";
import {Col, Glyphicon, ProgressBar, Well} from "react-bootstrap";
import {getUserId} from "../actions/UserActions";

class UserRecipesList extends Component {

    state = {
        userRecipes: null
    }

    async componentWillMount() {
        const userId = await getUserId();
        const recipes = await getUserRecipes(userId);
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
                            <p><a className="btn btn-success" onClick=""><Glyphicon glyph="edit"/> Redaguoti </a>
                                <a className="btn btn-danger" onClick=""><Glyphicon glyph="remove"/></a></p>
                        </Well>
                    </Col>
                </div>
        }

        );
    }
}

export default UserRecipesList;
