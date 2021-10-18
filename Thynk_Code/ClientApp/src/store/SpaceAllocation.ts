import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { ToastContainer, toast, ToastContentProps } from 'react-toastify';
import axios from "axios";
import { ReactChild, ReactFragment, ReactPortal, ReactNode } from 'react';




// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface SpaceAllocationState {
    spaceAllocation: {
        TestDate: Date
        time: string
        Location: string
        AllocatedBy: string
       
    }
    availableSpaces: []
    bookings: []
    mybookings: []
    report: any
    
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface AddAllocationAction { type: 'ADD_ALLOC_INFO', payload: { prop: '', value:'' } }
export interface GetAllocations {
    type: 'GET_ALLOC';
    payload: any;
}
export interface GetBookings {
    type: 'GET_BOOKING';
    payload: any;
}
export interface GetMyBookings {
    type: 'GET_MY_BOOKING';
    payload: any;
}
export interface GetReport {
    type: 'GET_REPORT';
    payload: any;
}
//export interface UpdateUserInfoAction { type: 'UPDATE_USER_INFO' }

export type KnownAction = AddAllocationAction | GetAllocations|GetBookings|GetMyBookings|GetReport;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    add_space_alloc: (payload: any) => ({ type: 'ADD_ALLOC_INFO', payload } as AddAllocationAction),
    saveSpaceAllocation: (spaceInfo: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation) {
            console.log(spaceInfo)
            axios.post(`api/pcr/allocateSpace`, spaceInfo).then((response) => {
                toast("space saved successfully");
            }).catch((error) =>{
                console.log(error.response)
                toast(error.response.data.data);
            })
            
        }

    },
    getAvailableSpace: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {

            axios.get(`api/pcr/get_all_availablespace`).then((response) => {
                let jsonObj: any= response.data 
                dispatch({ type: 'GET_ALLOC', payload:jsonObj.data });
                console.log(jsonObj.data)
                toast("user pulled successfully");

            }).catch((error) =>{
                console.log(error.response)
                toast(error.response.data.data);

            })
            
        }
        
    },
    bookspace: (bookingInfo:any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {

            axios.post(`api/pcr/bookSpace`,bookingInfo).then((response) => {
                
                toast("space booked successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);

            })
            
        }
        
    },
    updateBookingaction: (bookingInfo:any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {

            axios.put(`api/pcr/setLabResult`,bookingInfo).then((response) => {
                
                toast("booking updated successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);

            })
            
        }
        
    },
    viewBookings: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {

            axios.get(`api/pcr/get_bookings`).then((response) => {
                let jsonObj: any= response.data 
                dispatch({ type: 'GET_BOOKING', payload:jsonObj.data });
                console.log(jsonObj.data)
                toast("bookings pulled successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);

            })
            
        }
        
    },
    cancelBooking: (cancelDetail: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.user ) {

            axios.put(`api/pcr/cancelbooking`, cancelDetail).then((response) => {
                
                toast("cancelled booking successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);
                
            })
            
        }
        
    },

    viewMyBookings: (userId:string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {
            console.log(userId)
            axios.get(`api/pcr/get_bookings_user?userId=${userId}`).then((response) => {
                let jsonObj: any= response.data 
                dispatch({ type: 'GET_MY_BOOKING', payload:jsonObj.data });
                console.log(jsonObj.data)
                toast("bookings pulled successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);

            })
            
        }
        
    },
    reportAction: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.spaceAllocation ) {
            axios.get(`api/pcr/report`).then((response) => {
                let jsonObj: any= response.data 
                dispatch({ type: 'GET_REPORT', payload:jsonObj.data });
                console.log(jsonObj.data)
                toast("report pulled successfully");

            }).catch((error) =>{
                console.log(error.response.data)
                toast(error.response.data.data);

            })
            
        }
        
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<SpaceAllocationState> = (state: SpaceAllocationState | undefined, incomingAction: Action): SpaceAllocationState => {
    if (state === undefined) {
        return {
            spaceAllocation: {
                TestDate: new Date,
                time: '',
                Location: '',
                AllocatedBy: ''
               
            },
            availableSpaces: [],
            bookings:[],
            mybookings: [],
            report:{}
            
        }
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'ADD_ALLOC_INFO':
            const key: any = action.payload.prop
            console.log(key)
            if (key != '') {
                let spaceAllocation = {
                    ...state.spaceAllocation,
                    [key]: action.payload.value,
                    
                }
                return {...state,spaceAllocation};
                
            }
            else {
                return state;
            }
        case 'GET_ALLOC':
            
            return {...state,availableSpaces:action.payload};
        case 'GET_BOOKING':
            return {...state,bookings:action.payload}; 
        case 'GET_MY_BOOKING':
            return {...state,mybookings:action.payload};
        case 'GET_REPORT':
            return {...state,report:action.payload};
        default:
            return state;
    }
};


