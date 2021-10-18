import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Hello, world!</h1>
    <p>Welcome to your covid 19 pcr testing app</p>
    
    <p>this app do the following:</p>
    <ul>
      <li>An administrator can allocate spaces for tests at specific locations and days.</li>
      <li>An individual can book at PCR or Rapid Test at a specific day and location, based on availability.</li>
      <li>An individual can cancel a booked test, thus making space for someone else to use that slot.</li>
      <li>A lab admin can set the outcome of tests.</li>
      <li>A tabular report which shows: booking capacity / bookings / tests / positive cases / negative cases.</li>
    </ul>
  </div>
);

export default connect()(Home);
