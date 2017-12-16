import * as api from "../api/apiService";
import {USER_PRODUCTS_URL} from "../api/apiConstants";

export const getUserProducts = async (userId) => {
    try {
        const response = await api.get(USER_PRODUCTS_URL.replace(':userId',userId), {authorized: true});
        const json = await response.json();

        if (!response.ok) {
            throw new Error(json);
        }
        return json;
    } catch (err) {
        console.error('Could not fetch user recipes.', err);
    }
};