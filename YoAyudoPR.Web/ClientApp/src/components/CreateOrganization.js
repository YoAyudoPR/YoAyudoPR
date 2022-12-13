import React, { Component, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Axios from 'axios';
import './CreateEvent.css';


export function CreateOrganizationNavigate(props) {
    return (<CreateOrganization navigate={useNavigate()}></CreateOrganization>)
}


export default class CreateOrganization extends Component {
    static displayName = CreateOrganization.name;

    constructor(props) {
        super(props);
        this.state = { data: [], Name: '', Description: '' };
    }

    handleChange = (event, field) => {
        this.setState({ [field]: event.target.value });
        this.handleChange = this.handleChange.bind(this);
    }
   
    createOrg = (event) => {
        event.preventDefault();
        Axios.post("api/organization/create", {
            userGuid: localStorage.getItem("guid"),
            name: this.state.Name,
            description: this.state.Description,
        }).then((response) => {
            console.log(response.data);
            this.props.navigate("/Home")
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }

    render() {
        return (<>
            <div className="CO-div mt-4">
                <h1>CREATE AN ORGANIZATION!</h1>
                <Form>
                    <Form.Group xs={ 6 } as={Col}>
                        <Form.Label>Organization Name</Form.Label>
                        <Form.Control type="text" placeholder="Organization name" value={this.state.Name} onChange={(e) => this.handleChange(e, "Name")} />
                    </Form.Group>

                    <Form.Group xs={ 6 } as={Col}>
                        <Form.Label>Description</Form.Label>
                        <Form.Control type="text" placeholder="Describe your event!" value={this.state.Description} onChange={(e) => this.handleChange(e, "Description")} />
                    </Form.Group>
                </Form>
                <button type="submit" class="mt-4 btn btn-primary" onClick={this.createOrg}>Create</button>
            </div>
        </>);
    }

}
