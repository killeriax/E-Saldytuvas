import React, { Component } from 'react';
import {Form, FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';
import {getUserId, fetchUserId} from "../actions/UserActions";
import {addRecipe, addNewRecipe} from "../actions/RecipeActions";
import firebase from 'firebase';

class NewRecipeForm extends Component {
    state = {
        title: this.props.title || null,
        description: this.props.description || null,
        imageUrl: undefined,
        file: undefined
    };

    goTo(route) {
        this.props.history.replace(`/${route}`)
    }

    componentWillMount()
    {
        if(firebase.apps.length === 0) {
            firebase.initializeApp({
                apiKey: "AIzaSyBeS-FyyeO0gXuiMyXkiqAMCObvTvOL9vo",
                authDomain: "e-saldytuvas.firebaseapp.com",
                databaseURL: "https://e-saldytuvas.firebaseio.com",
                projectId: "e-saldytuvas",
                storageBucket: "e-saldytuvas.appspot.com",
                messagingSenderId: "835499054604"
            });
        }
    }

    handleTitleChange = e => {
        this.setState({
            title: e.target.value
        });
    };

    handleDescriptionChange = e => {
        this.setState({
            description: e.target.value
        });
    };

    handleFile = e => {
        this.setState({
            file: e.target.files
        })
    };

    handleSubmit = () => {
        const userId = fetchUserId();
        if(firebase.apps.length === 0) {
            firebase.initializeApp({
                apiKey: "AIzaSyBeS-FyyeO0gXuiMyXkiqAMCObvTvOL9vo",
                authDomain: "e-saldytuvas.firebaseapp.com",
                databaseURL: "https://e-saldytuvas.firebaseio.com",
                projectId: "e-saldytuvas",
                storageBucket: "e-saldytuvas.appspot.com",
                messagingSenderId: "835499054604"
            });
        }
        const storageRef = firebase.storage().ref();
        const that = this;
        const uuid =  ((((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-4" + (((1+Math.random())*0x10000)|0).toString(16).substring(1).substr(0,3) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1)).toLowerCase();
        for(let i = 0; i < that.state.file.length; i++)
        {
            const uploadTask = storageRef.child(uuid).put(that.state.file[i]).then(function(snapshot) {
                const url = snapshot.downloadURL;
                console.log('Uploaded a blob or file!');
                that.setState({
                    imageUrl: url
                });
                addNewRecipe(that.state.title, that.state.description, that.state.imageUrl, 1);
            });
        }
        this.goTo.bind(this, 'userrecipes');
    };

    render() {
        return (
                <Form>
                    <FormGroup controlId="title">
                        <ControlLabel>
                            Patiekalo pavadinimas
                        </ControlLabel>
                        <FormControl type="text" placeholder="Patiekalo pavadinimas" value={this.state.title || ''} onChange={this.handleTitleChange} />
                    </FormGroup>

                    <FormGroup controlId="description">
                        <ControlLabel>
                            Aprašymas
                        </ControlLabel>
                        <FormControl componentClass="textarea" placeholder="Aprašymas" value={this.state.description || ''} onChange={this.handleDescriptionChange}/>
                    </FormGroup>

                    <FormGroup controlId="imageUrl">
                        <ControlLabel>
                            Patiekalo nuotrauka
                        </ControlLabel>
                        <FormControl type="file" placeholder="Patiekalo nuotrauka" onChange={this.handleFile}/>
                    </FormGroup>
                    <Button bsStyle="success" onClick={this.handleSubmit}>
                        Sukurti
                    </Button>
                </Form>
        );
    }
}

export default NewRecipeForm;
