import React, { Component } from 'react';
import {Form, FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';
import {getUserId, fetchUserId} from "../actions/UserActions";
import {addRecipe, addNewRecipe} from "../actions/RecipeActions";
import firebase from 'firebase';

class NewRecipeForm extends Component {
    state = {
        title: undefined,
        description: undefined,
        imageUrl: undefined,
        file: undefined
    };

    componentWillMount()
    {
        debugger
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
        debugger
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

    /*handleSubmitt = () => {
        debugger
        const storageRef = firebase.storage().ref();
        const uuid =  ((((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-4" + (((1+Math.random())*0x10000)|0).toString(16).substring(1).substr(0,3) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1)).toLowerCase();
        const that = this;
        const lll =  this.state.file.length;

        for(let i = 0; i < lll; i++)
        {
            debugger
            const uploadTask = storageRef.child(uuid).put(that.state.file[i]).then((snapshot) => {
                const url = snapshot.downloadURL;
                debugger
                console.log('Uploaded a blob or file!');
                debugger
                this.setState({
                    imageUrl: url
                })
            }).catch((e)=>{
                debugger
                console.log(e);
            });

        }
    }*/

    handleSubmit = () => {
        const userId = fetchUserId();
        debugger
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
        debugger
        var storageRef = firebase.storage().ref();
        console.log(firebase.apps.length);
        debugger
        var that = this;
        debugger
        var uuid =  ((((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-4" + (((1+Math.random())*0x10000)|0).toString(16).substring(1).substr(0,3) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + "-" + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1) + (((1+Math.random())*0x10000)|0).toString(16).substring(1)).toLowerCase();
        for(var i = 0; i < that.state.file.length; i++)
        {
            var uploadTask = storageRef.child(uuid).put(that.state.file[i]).then(function(snapshot) {
                var url = snapshot.downloadURL;
                debugger
                console.log('Uploaded a blob or file!');
                debugger
                that.setState({
                    imageUrl: url
                })
                //const userId = fetchUserId();
                addNewRecipe(that.state.title, that.state.description, that.state.imageUrl, 1);
            });
            /*uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
                function(snapshot) {
                    // Get task progress, including the number of bytes uploaded and the total number of bytes to be uploaded
                    var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
                    console.log('Upload is ' + progress + '% done');
                    switch (snapshot.state) {
                        case firebase.storage.TaskState.PAUSED: // or 'paused'
                            console.log('Upload is paused');
                            break;
                        case firebase.storage.TaskState.RUNNING: // or 'running'
                            console.log('Upload is running');
                            break;
                    }});
            uploadTask.pause();
            uploadTask.resume();*/
        }
        /*debugger
        const userId = fetchUserId();
        debugger
        addRecipe(this.state.title, this.state.description, this.state.imageUrl, userId);*/
    };

    render() {
        return (
            <div className="container">
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
            </div>
        );
    }
}

export default NewRecipeForm;
