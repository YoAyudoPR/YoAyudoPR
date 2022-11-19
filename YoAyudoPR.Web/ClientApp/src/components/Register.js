import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom'
import Axios from 'axios';

export function WithNavigate(props) {
    return (<Register navigate={useNavigate()}></Register>)
}

export default class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props);
        this.state = { Email: '', Firstname: '', Initial: '', Lastname: '', Secondlastname: '', Phone: '', Password: '' };

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    render() { 
        
        const register = () => {
            Axios.post("api/user/create", {
                email: this.state.Email,
                firstname: this.state.Firstname,
                initial: this.state.Initial,
                lastname: this.state.Lastname,
                secondlastname: this.state.Secondlastname,
                phone: this.state.Phone,
                password: this.state.Password,
            }).then((response) => {
                console.log(response.data);
            });
            this.props.navigate('/Login');
        }

        return (
            <form class="row g-3">
                <div class="col-md-6">
                    <label for="Email" class="form-label">Email</label>
                    <input type="email" class="form-control" value={this.state.Email} onChange={(e) => this.handleChange(e, "Email")} ></input>
                </div>
                <div class="col-md-6">
                    <label for="Password" class="form-label">Password</label>
                    <input type="password" class="form-control" value={this.state.Password} onChange={(e) => this.handleChange(e, "Password")}></input>
                </div>
                <div class="col-md-3">
                    <label for="FirstName" class="form-label">First Name</label>
                    <input type="text" class="form-control" value={this.state.Firstname} onChange={(e) => this.handleChange(e, "Firstname")} aria-label="First name"></input>
                </div>
                <div class="col-md-1">
                    <label for="Initial" class="form-label">Initial</label>
                    <input type="text" class="form-control" value={this.state.Initial} onChange={(e) => this.handleChange(e, "Initial")} aria-label="Initial" ></input>
                </div>
                <div class="col-md-3">
                    <label for="LastName" class="form-label">Last Name</label>
                    <input type="text" class="form-control" value={this.state.Lastname} onChange={(e) => this.handleChange(e, "Lastname")} aria-label="Last name"></input>
                </div>
                <div class="col-md-3">
                    <label for="SecondLastName" class="form-label">Second Last Name</label>
                    <input type="text" class="form-control" value={this.state.Secondlastname} onChange={(e) => this.handleChange(e, "Secondlastname")} aria-label="Second Last name"></input>
                </div>
                <div class="col-md-4">
                    <label for="Phone" class="form-label">Phone</label>
                    <input type="tel" class="form-control" value={this.state.Phone} onChange={(e) => this.handleChange(e, "Phone")}></input>
                </div>
                <div class="col-12">
                    <button type="submit" class="btn btn-primary" onClick={register}>Sign Up</button>  
                </div>
            </form>
        );
    }
}