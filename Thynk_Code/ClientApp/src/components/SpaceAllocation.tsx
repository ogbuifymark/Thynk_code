import * as React from 'react';
import { FC, useEffect, useState} from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from "react-router-dom";
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as SpaceAllocationStore from '../store/SpaceAllocation';
import 'bootstrap/dist/css/bootstrap.min.css';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Flatpicker from 'react-flatpickr'
import moment from 'moment'
import { SpaceAllocationState } from '../store/SpaceAllocation';









const SpaceAllocation:FC<SpaceAllocationState> = ({}) => {
    const params:any = useParams()
    const dispatch = useDispatch();
    const [startTime, setStartTime] = useState('');
    const [endTime, setEndTime] = useState('');
    const [testTime, setTestTime] = useState(new Date);
    const [location, setLocation] = useState('');

    let { actionCreators } = SpaceAllocationStore;
    let { add_space_alloc,  saveSpaceAllocation} = actionCreators;
    const {spaceAllocation} = useSelector((store:any) => store.spaceAllocation)
    let {AllocatedBy } = spaceAllocation

    const handleSubmit = (): void => {
        // console.log(this.props.match.params)
        if (startTime != '' && endTime != '' && location!=''){
            const spaceData:any = {
                testDate: testTime,
                time: startTime+'-'+endTime,
                location: location,
                userId: AllocatedBy
            }
            dispatch(saveSpaceAllocation(spaceData))
        }
        else{
            toast("please fill all the fields");
        }

        

        
       
    }

    useEffect(() => {
        const userId = params.userId;
        dispatch(add_space_alloc({ 'prop':'AllocatedBy','value': userId }))

    }, [dispatch])
    
        return (
            <React.Fragment>
                <h1>Allocate space</h1>

                <form onSubmit={() => { }}>
                <div className="row">
                    <div className="col-sm-5">
                            <div className="form-group">
                                <label htmlFor="test">Test Date</label>
                                <Flatpicker
                                className="form-control"
                                    value={testTime} 
                                    onChange={(date: any) => {
                                        let newDate = new Date(date)
                                        setTestTime( moment(newDate).toDate())
                                        add_space_alloc({ 'prop':'TestDate','value': date })}
                                    }
                                        />
                               
                            </div>
                    </div>
                    <div className="col-sm-2">
                            <div className="form-group">
                                <label htmlFor="startTime">start time</label>
                                <Flatpicker
                                data-enable-time
                                className="form-control"
                                    value={startTime} 
                                    onChange={(date: any) => {
                                        let newDate = new Date(date)
                                        setStartTime(moment(newDate).format("HH:mm"))
                                    }}
                                    options={{noCalendar: true,time_24hr: true,dateFormat: "H:i",}}

                                        />
                               
                            </div>
                    </div>
                    <div className="col-sm-2">
                            <div className="form-group">
                                <label htmlFor="endTime">end time</label>
                                <Flatpicker
                                data-enable-time
                                className="form-control"
                                    value={endTime} 
                                    onChange={(date: any) => {
                                        let newDate = new Date(date)
                                        setEndTime(moment(newDate).format("HH:mm"))
                                    }}

                                    options={{noCalendar: true,time_24hr: true,dateFormat: "H:i",}}

                                        />
                               
                            </div>
                    </div>
    
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="Location">Location</label>
                            <input
                                type='text'
                                className="form-control"
                                name='Location'
                                value={location}
                                onChange={(e) => setLocation(e.target.value)}
                                required={true}
                            />
                        </div>
                    </div>
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => handleSubmit()}>
                    Save
                </button>
                <ToastContainer />
                </div>
                </form>
               <br/>
                 
            </React.Fragment>
        );
    
};

export default SpaceAllocation;