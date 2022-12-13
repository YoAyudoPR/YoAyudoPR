import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import './CreateEvent.css';
import Axios from 'axios';

export function LogNavigate() {
    return (<Login navigate={useNavigate()}></Login>)
}

export var LogInStatus;

export default class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { Email: '', Password: ''};
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange = (event, field) => {
        this.setState({ [field]: event.target.value });
    }

    login = (event) => {
        event.preventDefault();
        Axios.post("api/user/auth", {
            email: this.state.Email,
            password: this.state.Password,
        }).then((response) => {
            console.log(response.data);
            localStorage.setItem('guid', response.data.userGuid)
            this.props.navigate("/Home")
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }

    render() {
        return (
            <div className="login-bg-img pt-4">
                <div className=" login-div p-4 mb-4">
                    <h1 className="login-h1">Login</h1>
                <form>
                <div class="mb-3">
                    <label for="Email" class="form-label">Email address</label>
                    <input type="email" size="4" class="form-control" id="Email" aria-describedby="emailHelp" value={this.state.Email} onChange={(e) => this.handleChange(e, "Email")}></input>
                    <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
                </div>
                <div class="mb-3">
                    <label for="Password" class="form-label">Password</label>
                    <input size="5" type="password" class="form-control" id="Password" value={this.state.Password} onChange={(e) => this.handleChange(e, "Password")}></input>
                </div>
                    <div class="mt-3">
                        <p class="login-p">New to YoAyudoPR?</p>
                     <a href='/Register'>Register</a>
                </div>
                <button type="submit" class="mt-4 btn btn-primary" onClick={this.login}>Login</button>
                </form>
            </div>
            </div>
        );
    }
}