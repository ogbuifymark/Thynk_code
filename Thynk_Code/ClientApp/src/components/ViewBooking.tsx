import * as React from 'react';
import { FC, useEffect, useState} from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from "react-router-dom";

import * as SpaceAllocationStore from '../store/SpaceAllocation';
import 'bootstrap/dist/css/bootstrap.min.css';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { SpaceAllocationState } from '../store/SpaceAllocation';









const ViewBooking:FC<SpaceAllocationState> = ({}) => {
    const params:any = useParams()
    const dispatch = useDispatch();
    const [userId, setUserId] = useState('');
    

    let { actionCreators } = SpaceAllocationStore;
    let { updateBookingaction,  viewBookings} = actionCreators;
    const {spaceAllocation,bookings} = useSelector((store:any) => store.spaceAllocation)

    
    useEffect(() => {
        const user = params.userId;
        setUserId(user)
        dispatch(viewBookings())
    }, [dispatch])

    const handleResult = (bookingId:string, status: number)=>{
        const updateBooking:any = {
            bookingId: bookingId,
            userId: userId,
            testStatus: status,
        }
        dispatch(updateBookingaction(updateBooking))
    }
    
    
        return (
            <React.Fragment>
                <h1>Set Result</h1>
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>name</th>
            <th>Date</th>
            <th>Time</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
            
          {bookings.length > 0?bookings.map((booking: any) =>
            <tr key={booking.id}>
              <td>{booking.user.firstName}{booking.user.lastName}{booking.user.otherName}</td>
              <td>{booking.testDate}</td>
              <td>{booking.time}</td>
              <td>{
                <><button type="button"
                          className="btn btn-primary btn-lg"
                          onClick={() => handleResult(booking.id, 1)}>
                          set positive
                      </button>
                      |
                      <button type="button"
                          className="btn btn-primary btn-lg"
                          onClick={() => handleResult(booking.id, 2)}>
                              set negative
                          </button></> 
            }</td>
            </tr>
          ):''}
        </tbody>
      </table>
      <ToastContainer />
               <br/>
                 
            </React.Fragment>
        );
    
};

export default ViewBooking;