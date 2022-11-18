import React, { Component } from 'react';
/*import { } from 'react-bootstrap';*/

export class Register extends Component {
    static displayName = Register.name;

    render() {
        return (
            <form class="row g-3">
                <div class="col-md-6">
                    <label for="inputEmail1" class="form-label">Email</label>
                    <input type="email" class="form-control" id="inputEmail1"></input>
                </div>
                <div class="col-md-6">
                    <label for="inputPassword1" class="form-label">Password</label>
                    <input type="password" class="form-control" id="inputPassword1"></input>
                </div>
                <div class="col-md-3">
                    <label for="inputFirstname1" class="form-label">First Name</label>
                    <input type="text" class="form-control" aria-label="First name"></input>
                </div>
                <div class="col-md-1">
                    <label for="inputInitial1" class="form-label">Initial</label>
                    <input type="text" class="form-control" aria-label="Initial"></input>
                </div>
                <div class="col-md-3">
                    <label for="inputLastname1" class="form-label">Last Name</label>
                    <input type="text" class="form-control" aria-label="Last name"></input>
                </div>
                <div class="col-md-3">
                    <label for="inputSecondLastname1" class="form-label">Second Last Name</label>
                    <input type="text" class="form-control" aria-label="Second Last name"></input>
                </div>
                <div class="col-md-4">
                    <label for="inputPhone" class="form-label">Phone</label>
                    <input type="tel" class="form-control" id="inputPhone"></input>
                </div>
                <div class="col-12">
                    <button type="submit" class="btn btn-primary">Sign in</button>
                </div>
            </form>);
    }
}