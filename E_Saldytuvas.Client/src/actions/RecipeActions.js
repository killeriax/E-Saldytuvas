import * as api from "../api/apiService";
import {ADD_RECIPE_URL, USER_RECIPES_URL, RECIPES_URL} from "../api/apiConstants";

export const addRecipe = async (title, description, imageUrl, userId) => {
    try {
        debugger
        const response = await api.post(ADD_RECIPE_URL, {body: {title, description, imageUrl, userId}, authorized: true});
        const json = await response.json();

        if (!response.ok) {
            throw new Error(json);
        }

    } catch (err) {
        console.error('Could not add a recipe', err);
    }
};

export const addNewRecipe = (title, description, imageUrl, userId) => {
    try {
        debugger
        const response = api.post(ADD_RECIPE_URL, {body: {title, description, imageUrl, userId}, authorized: true});
        const json = response.json();

        if (!response.ok) {
            throw new Error(json);
        }

    } catch (err) {
        console.error('Could not add a recipe', err);
    }
};

export const getUserRecipes = async (userId) => {
    try {
        const response = await api.get(USER_RECIPES_URL.replace(':userId',userId), {authorized: true});
        const json = await response.json();
        console.log(json);
        debugger

        if (!response.ok) {
            throw new Error(json);
        }
        return json;
    } catch (err) {
        console.error('Could not fetch user recipes.', err);
    }
};

export const getRecipes = async () => {
    try {
        const response = await api.get(RECIPES_URL, {authorized: true});
        const json = await response.json();
        console.log(json);
        debugger

        if (!response.ok) {
            throw new Error(json);
        }
        return json;
    } catch (err) {
        console.error('Could not fetch recipes.', err);
    }
};