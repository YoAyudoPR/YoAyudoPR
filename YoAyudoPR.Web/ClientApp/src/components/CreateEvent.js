import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';

export function CreateEventNavigate(props) {
    return (<CreateEvent navigate={useNavigate()}></CreateEvent>)
}

export default class CreateEvent extends Component {
    static displayName = CreateEvent.name;

    constructor(props) {
        super(props);
        this.state = { name: '', description: '', startDate: new Date().toISOString().substring(0, 16), endDate: new Date().toISOString().substring(0, 16), capacity: 0, createdDate: '', website: '', address: '',};
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    createEvents = (event) => {
        event.preventDefault();
        Axios.post("api/event/create", {
            organizationGuid: localStorage.getItem("orgGuid"),
            name: this.state.name,
            description: this.state.description,
            startdate: this.state.startDate,
            enddate: this.state.endDate,
            capacity: this.state.capacity,
            createdat: this.state.createdDate,
            isactive: true,
            isdeleted: false,
            websiteurl: this.state.website,
            address: this.state.address,
            categoryId: localStorage.getItem("category")
        }).then((response) => {
            console.log(response.data);
            this.props.navigate("/OrganizationProfile")
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }
    render() {
        return (<>
            <div className="img">
                <h1 class="CE-center CE-h1">Register the event bellow!</h1>
                <Container className="container-properties">
                    <Form className="form">        
                        <Row className="mb-3">
                            <Form.Group xs={6} as={Col} controlId="formGridEmail">
                                <Form.Label>Name</Form.Label>
                                <Form.Control type="text" placeholder="Event name" value={this.state.name} onChange={(e) => this.handleChange(e, "name")} />
                            </Form.Group>
                            <Form.Group xs={6} as={Col} controlId="formCapacity">
                                <CategoryDropdown />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={6} as={Col} controlId="formGridPassword">
                                <Form.Label>Description</Form.Label>
                                <Form.Control as="textarea" placeholder="Describe your event" value={this.state.description} onChange={(e) => this.handleChange(e, "description")} />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={4} as={Col} controlId="formGridAddress1">
                                <Form.Label>Start Date</Form.Label>
                                <Form.Control type="datetime-local" placeholder="" value={this.state.startDate} onChange={(e) => this.handleChange(e, "startDate")} />
                            </Form.Group>

                            <Form.Group xs={4} as={Col} controlId="formGridAddress2">
                                <Form.Label>End Date</Form.Label>
                                <Form.Control type="datetime-local" placeholder="" value={this.state.endDate} onChange={(e) => this.handleChange(e, "endDate")} />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={4} as={Col} controlId="formCapacity">
                                <Form.Label>Capacity</Form.Label>
                                <Form.Control type="text" placeholder="How many can attend?" value={this.state.capacity} onChange={(e) => this.handleChange(e, "capacity")} />
                            </Form.Group>
                            <Form.Group xs={4} as={Col} controlId="formCapacity">
                                <Form.Label>Website Url</Form.Label>
                                <Form.Control type="text" placeholder="Optional" />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={6} as={Col} controlId="formCapacity">
                                <Form.Label>Address</Form.Label>
                                <Form.Control as="textarea" placeholder="Where is the event?" value={this.state.address} onChange={(e) => this.handleChange(e, "address")} />
                            </Form.Group>
                        </Row>

                        <Button className="mt-3" variant="primary" onClick={this.createEvents}>Submit Event</Button>
                    </Form>
                </Container>
            </div>
         </>);
            
    }
    
}