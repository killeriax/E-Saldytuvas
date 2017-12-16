import {
    REGISTER_USER_URL,
    USER_ID_URL,
    USER_URL
} from '../api/apiConstants';
import * as api from '../api/apiService';
import auth from '../auth/Auth';


export const getCurrentUser = async () => {
    try {
        const userId = await getUserId();

        const response = await api.get(USER_URL.replace(':userId',userId), {authorized: true});

        const json = await response.json();

        if (!response.ok) {
            throw new Error(json);
        }
        return json;
    } catch (err) {
        console.error('Could not fetch a user.', err);
    }
};

export const registerUser = async () => {
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

export const editUser = async (userId, name, surname, email) => {
    try {
        await api.put(USER_URL.replace(':userId', userId), {body: {name, surname, email}, authorized: true});
    } catch (err) {
        console.error('Could not edit a user', err);
    }
};

export const getUserId = async () => {
    try {
        const response = await api.get(USER_ID_URL, {authorized: true});
        const json = await response.json();
        if (!response.ok) {
            throw new Error(json);
        }
        return json;
    } catch (err) {
        console.error('Could not register a user', err);
    }
};

export const logout = () => {
    auth.logout();
};