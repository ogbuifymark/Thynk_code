import * as React from 'react';
import { FC, useEffect, useState} from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from "react-router-dom";

import * as SpaceAllocationStore from '../store/SpaceAllocation';
import 'bootstrap/dist/css/bootstrap.min.css';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { SpaceAllocationState } from '../store/SpaceAllocation';









const Result:FC<SpaceAllocationState> = ({}) => {
    const params:any = useParams()
    const dispatch = useDispatch();
    const [userId, setUserId] = useState('');
    

    let { actionCreators } = SpaceAllocationStore;
    let {  viewMyBookings, cancelBooking} = actionCreators;
    const {mybookings} = useSelector((store:any) => store.spaceAllocation)

    
    useEffect(() => {
        
        dispatch(viewMyBookings(params.userId))
    }, [dispatch])

    const handleCancel = (bookingId:string)=>{
        const updateBooking:any = {
            bookingId: bookingId,
            userId: params.userId,
        }
        dispatch(cancelBooking(updateBooking))
    }
    
    
        return (
            <React.Fragment>
                <h1>Result</h1>

                <p>Your result details </p>
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
          <th>name</th>
            <th>Date</th>
            <th>Time</th>
            <th>Result</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
            
          {mybookings.length > 0?mybookings.map((booking: any) =>
            <tr key={booking.id}>
              <td>{booking.user.firstName}{booking.user.lastName}{booking.user.otherName}</td>
              <td>{booking.testDate}</td>
              <td>{booking.time}</td>
              <td>{booking.testStatus === 1?"positive":booking.testStatus === 2?"negative": "pending"}</td>
              <td>{booking.testStatus === 0?
                <button type="button"
                        className="btn btn-primary btn-sm"
                        onClick={() =>handleCancel(booking.id)}>
                        cancel 
                    </button>:
                    <button type="button" disabled={true}
                    className="btn btn-primary btn-sm"
                    onClick={() =>handleCancel(booking.id)}>
                    cancel 
                </button>}</td>
            </tr>
          ):''}
        </tbody>
      </table>

               
      <ToastContainer />
               <br/>
                 
            </React.Fragment>
        );
    
};

export default Result;