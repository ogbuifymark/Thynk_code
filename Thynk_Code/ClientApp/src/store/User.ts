import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { ToastContainer, toast, ToastContentProps } from 'react-toastify';
import axios from "axios";
import { ReactChild, ReactFragment, ReactPortal, ReactNode } from 'react';




// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface UserState {
    user: {
        userName: string
        email: string
        firstName: string
        lastName: string
        otherName: string
        phoneNumber: string
        password: string
        role: string
    }
    users:[]
    
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface AddUserInfoAction { type: 'ADD_USER_INFO', payload: { prop: '', value:'' } }
export interface Getusers {
    type: 'GET_USERS';
    payload: any;
}
//export interface UpdateUserInfoAction { type: 'UPDATE_USER_INFO' }

export type KnownAction = AddUserInfoAction | Getusers;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    add_user_info: (payload: any) => ({ type: 'ADD_USER_INFO', payload } as AddUserInfoAction),
    saveUserInfo: (user: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.user) {

            axios.post(`api/auth/user`, user).then((response) => {
                toast("user registered successfully");
            }).catch((error) =>{
                console.log(error.response)
                let errors = error.response.data.data[""]
                errors.errors.forEach((error: { errorMessage: boolean | ReactChild | ReactFragment | ReactPortal | ((props: ToastContentProps<{}>) => ReactNode) | null | undefined; }) => {
                    toast(error.errorMessage);

                });
            })
            
        }

    },
    getusers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.user ) {

            axios.get(`api/auth/users`).then((response) => {
                let jsonObj: any= response.data 
                dispatch({ type: 'GET_USERS', payload:jsonObj.data });
                console.log(jsonObj.data)
                toast("user pulled successfully");

            }).catch((error) =>{
                console.log(error.response)
                
            })
            
        }
        
    },
    
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {
    if (state === undefined) {
        return {
            user: {
                userName: '',
                email: '',
                firstName: '',
                lastName: '',
                otherName: '',
                phoneNumber: '',
                password: '',
                role: ''
            },
            users: []
            
        }
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'ADD_USER_INFO':
            const key: any = action.payload.prop
            if (key != '') {
                let user = {
                    ...state.user,
                    [key]: action.payload.value,
                    
                }
                let users = state.users
                console.log(user)
                return {user,users};
                
            }
            else {
                return state;
            }

        
        case 'GET_USERS':
            let user = state.user
            let users = action.payload
            console.log(user)
            return {user,users};
                
            
        default:
            return state;
    }
};


