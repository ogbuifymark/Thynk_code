import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import User from './components/User';
import SpaceAllocation from './components/SpaceAllocation';

import './custom.css'
import Book from './components/Book';
import ViewBooking from './components/ViewBooking';
import Result from './components/Result';
import Report from './components/Report';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/create-user' component={User} />
        <Route path='/space-allocation/:userId' component={SpaceAllocation} />
        <Route path='/book-test/:userId' component={Book} />
        <Route path='/view-book/:userId' component={ViewBooking} />
        <Route path='/view-result/:userId' component={Result} />
        <Route path='/report' component={Report} />
    </Layout>
);
