import * as React from 'react';
import { createBrowserHistory } from 'history';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as UserStore from '../store/User';
import 'bootstrap/dist/css/bootstrap.min.css';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


interface State {
    userLength: any
  }
type UserProps =
    UserStore.UserState &
    typeof UserStore.actionCreators &
    RouteComponentProps<{}>;

const roles = [{key:'Admin', value: 1},
{key: 'User',value:2},
{key: 'Lab_Admin',value:3}]
class User extends React.PureComponent<UserProps,State> {
    constructor(props:any) {
        super(props);
        this.state = {
            userLength: 0
        }
    }
    public componentDidMount() {
        this.fetchUsers();
      }
    
      // This method is called when the route parameters change
      public componentDidUpdate() {
        // this.fetchUsers();
      }
      private fetchUsers() {
        this.props.getusers();
        console.log(this.props.users)
      }
      handleSubmit(): void {
          
        this.props.saveUserInfo(this.props.user)
        this.props.getusers();
    }

    routeChange=(path:string)=> {
        createBrowserHistory().push(path);
        createBrowserHistory().go(0)
        
      }
    renderRoleItems =() => {
        var items = [];

        for (var i = 0; i < roles.length; i++) {
            var item = roles[i];
            items.push(
                <option className="" key={i} value={item.key}>
                    {item.key}
                </option>
            );
        }
        return items;
    }
    public render() {
        return (
            <React.Fragment>
                <h1>Register User</h1>

                <form onSubmit={() => { }}>
                <div className="row">
                    <div className="col-sm-6">
                            <div className="form-group">
                                <label htmlFor="firstName">firstName</label>
                                <input
                                    type='text'
                                    className="form-control"
                                    name='firstName'
                                    value={this.props.user.firstName}
                                    onChange={(e) => this.props.add_user_info({ 'prop':'firstName','value': e.target.value })}
                                    required={true}
                                />
                            </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="lastName">lastName</label>
                            <input
                                type='text'
                                className="form-control"
                                name='lastName'
                                value={this.props.user.lastName}
                                onChange={(e) => this.props.add_user_info({ 'prop':'lastName','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="otherName">otherName</label>
                            <input
                                type='text'
                                className="form-control"
                                name='otherName'
                                value={this.props.user.otherName}
                                onChange={(e) => this.props.add_user_info({ 'prop':'otherName','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="phoneNumber">phoneNumber</label>
                            <input
                                type='text'
                                className="form-control"
                                name='phoneNumber'
                                value={this.props.user.phoneNumber}
                                onChange={(e) => this.props.add_user_info({ 'prop':'phoneNumber','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="email">email</label>
                            <input
                                type='email'
                                className="form-control"
                                name='email'
                                value={this.props.user.email}
                                onChange={(e) => this.props.add_user_info({ 'prop':'email','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="userName">userName</label>
                            <input
                                type='text'
                                className="form-control"
                                name='userName'
                                value={this.props.user.userName}
                                onChange={(e) => this.props.add_user_info({ 'prop':'userName','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label htmlFor="password">password</label>
                            <input
                                minLength= {6}
                                type='password'
                                className="form-control"
                                name='password'
                                value={this.props.user.password}
                                onChange={(e) => this.props.add_user_info({ 'prop':'password','value': e.target.value })}
                                required={true}
                            />
                        </div>
                    </div>
                    <div className="col-sm-6">
                          <div className="form-group">
                            <label htmlFor="role">role </label>
                            <select
                            //   style={this.state.selectAttribute}
                              name="role"
                              className="form-control"
                              value={this.props.user.role}
                              onChange={(e) => this.props.add_user_info({ 'prop':'role','value': e.target.value })}
                              required
                            >
                              <option className="" value="">
                                Select role
                              </option>
                              {this.renderRoleItems()}
                            </select>
                          </div>
                        </div>

                    
                </div>
                    
                    
                    
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => this.handleSubmit()}>
                    Save
                </button>
                <ToastContainer />
                </form>
               <br/>
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Phone Number</th>
            <th>Email</th>
            <th>Role</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
            
          {this.props.users.length > 0?this.props.users.map((user: any) =>
            <tr key={user.id}>
              <td>{user.firstName}{user.lastName}{user.otherName}</td>
              <td>{user.phoneNumber}</td>
              <td>{user.email}</td>
              <td>{user.role}</td>
              <td>{user.role === 'Admin'?
                <button type="button"
                    className="btn btn-primary btn-sm"
                    onClick={() => this.routeChange('space-allocation/' +user.id)}>
                    Allocate space
                </button>:user.role === 'Lab_Admin'?
                <button type="button"
                    className="btn btn-primary btn-sm"
                    onClick={() => this.routeChange('view-book/'+user.id)}>
                    view booking 
                </button>:
                <>
                <button type="button"
                    className="btn btn-primary btn-sm"
                    onClick={() => this.routeChange('book-test/'+user.id)}>
                    Book test 
                </button>|
                <button type="button"
                    className="btn btn-primary btn-sm"
                    onClick={() => this.routeChange('view-result/'+user.id)}>
                    view result
                </button>

                </>
            }</td>
            </tr>
          ):''}
        </tbody>
      </table>
               
            </React.Fragment>
        );
    }
    
};
  
  

export default connect(
    (state: ApplicationState) => state.user,
    UserStore.actionCreators
)(User);
