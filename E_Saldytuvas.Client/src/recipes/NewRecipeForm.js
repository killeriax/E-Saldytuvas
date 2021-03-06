import React, { Component } from 'react';
import {Form, FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';
import {getUserId} from "../actions/UserActions";
import {addNewRecipe} from "../actions/RecipeActions";
import firebase from 'firebase';

class NewRecipeForm extends Component {
    state = {
        title: this.props.title || null,
        description: this.props.description || null,
        imageUrl: undefined,
        file: [],
        userId: undefined,
        edit: this.props.edit || false
    };

    goTo(route) {
        this.props.history.replace(`/${route}`)
    }

    async componentWillMount()
    {
        const usrId = await getUserId();
        this.setState({
            userId: usrId
        });
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

    buttonName = () => {
        if(this.state.edit === true)
            return "Pakeisti";
        else
            return "Sukurti";
    };

    handleSubmit = () => {
        const storageRef = firebase.storage().ref();
        //const that = this;
        const uuid =  ((((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-4" + (((1+Math.random())*0x10000)|0).toString(16).substring(1).substr(0,3) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1)).toLowerCase();
        debugger
        if(this.state.edit === false)
        {
            if(this.state.file.length > 0)
            {
                for(let i = 0; i < this.state.file.length; i++)
                {
                    storageRef.child(uuid).put(this.state.file[i]).then((snapshot) => {
                        const url = snapshot.downloadURL;
                        this.setState({
                            imageUrl: url
                        });
                        debugger
                        addNewRecipe(this.state.title, this.state.description, this.state.imageUrl, this.state.userId);
                        this.forceUpdate();
                        this.goTo('userrecipes');
                    });
                }
            }
            /*else
                addNewRecipe(that.state.title, that.state.description, that.state.imageUrl, that.state.userId);*/
        }
        else
        {
            // updateRecipe(that.state.title, that.state.description, that.state.imageUrl, that.state.userId);
        }
        debugger

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
                    {this.buttonName()}
                </Button>
            </Form>
        );
    }
}

export default NewRecipeForm;
