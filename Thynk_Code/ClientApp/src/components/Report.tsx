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
    let {  reportAction} = actionCreators;
    const {report} = useSelector((store:any) => store.spaceAllocation)

    
    useEffect(() => {
        
        dispatch(reportAction())
    }, [dispatch])

   
    
    
        return (
            <React.Fragment>
                <h1>Report</h1>

                
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
          <th>booking capacity</th>
            <th>bookings</th>
            <th>tests</th>
            <th>positive cases</th>
            <th>negative cases</th>
          </tr>
        </thead>
        <tbody>
            
            <tr key={0}>
              <td>{report.bookingCapacity}</td>
              <td>{report.bookings}</td>
              <td>{report.test}</td>
              <td>{report.positiveCases}</td>
              <td>{report.negativeCases}</td>
            </tr>
        </tbody>
      </table>

               
      <ToastContainer />
               <br/>
                 
            </React.Fragment>
        );
    
};

export default Result;