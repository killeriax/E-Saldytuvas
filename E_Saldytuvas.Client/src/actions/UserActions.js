import {
    CURRENT_USER_URL,
    REGISTER_USER_URL
} from '../api/apiConstants';
import * as api from '../api/apiService';
import auth from '../auth/Auth';


export const fetchCurrentUser = () => async () => {
    try {
        const response = await api.get(CURRENT_USER_URL, {authorized: true});
        const json = await response.json();

        if (!response.ok) {
            throw new Error(json);
        }

    } catch (err) {
        console.error('Could not fetch a user.', err);
    }
};

export const registerUser = () => async () => {
    try {
        const response = await api.post(REGISTER_USER_URL, {authorized: true});
        const json = await response.json();

        if (!response.ok) {
            throw new Error(json);
        }

    } catch (err) {
        console.error('Could not register a user', err);
    }
};

export const logout = () => {
    auth.logout();
};