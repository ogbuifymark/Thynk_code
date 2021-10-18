import * as React from 'react';
import { FC, useEffect, useState} from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from "react-router-dom";

import * as SpaceAllocationStore from '../store/SpaceAllocation';
import 'bootstrap/dist/css/bootstrap.min.css';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { SpaceAllocationState } from '../store/SpaceAllocation';









const Booking:FC<SpaceAllocationState> = ({}) => {
    const params:any = useParams()
    const dispatch = useDispatch();
    const [userId, setUserId] = useState('');
    

    let { actionCreators } = SpaceAllocationStore;
    let { bookspace,  getAvailableSpace} = actionCreators;
    const {spaceAllocation,availableSpaces} = useSelector((store:any) => store.spaceAllocation)

    
    useEffect(() => {
        const user = params.userId;
        setUserId(user)
        dispatch(getAvailableSpace())
        console.log(availableSpaces)
    }, [dispatch])

    const handleSubmit = (spaceId:string, time:string, date:Date)=>{
        const bookData:any = {
            space_id: spaceId,
            userId: userId,
            testDate: date,
            time: time
        }
        dispatch(bookspace(bookData))
    }
    
        return (
            <React.Fragment>
                <h1>Available spaces</h1>
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Location</th>
            <th>Date</th>
            <th>Time</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
            
          {availableSpaces.length > 0?availableSpaces.map((space: any) =>
            <tr key={space.id}>
              <td>{space.location}</td>
              <td>{space.testDate}</td>
              <td>{space.time}</td>
              <td>{
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => handleSubmit(space.id,space.time,space.testDate)}>
                    Allocate space
                </button>
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

export default Booking;