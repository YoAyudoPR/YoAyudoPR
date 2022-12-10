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
        this.state = {organizationID: 0 , name: '', description: '', startDate: '', endDate: '', capacity: 0, createdDate: '', website: '', address: '', categoryID: 0};
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    createEvents = (event) => {
        event.preventDefault();
        Axios.get("api/event/create", {
            organization_id: this.state.organizationID, /*Buscar*/
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
            category_id: this.state.categoryID, /*Buscar*/
        }).then((response) => {
            console.log(response.data);
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
                                <Form.Control type="text" placeholder="Event name" />
                            </Form.Group>
                            <Form.Group xs={6} as={Col} controlId="formCapacity">
                                <CategoryDropdown />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={6} as={Col} controlId="formGridPassword">
                                <Form.Label>Description</Form.Label>
                                <Form.Control as="textarea" placeholder="Describe your event" value={this.state.description } />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={4} as={Col} controlId="formGridAddress1">
                                <Form.Label>Start Date</Form.Label>
                                <Form.Control type="datetime-local" placeholder="" />
                            </Form.Group>

                            <Form.Group xs={4} as={Col} controlId="formGridAddress2">
                                <Form.Label>End Date</Form.Label>
                                <Form.Control type="datetime-local" placeholder="" />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={4} as={Col} controlId="formCapacity">
                                <Form.Label>Capacity</Form.Label>
                                <Form.Control type="text" placeholder="How many can attend?" />
                            </Form.Group>
                            <Form.Group xs={4} as={Col} controlId="formCapacity">
                                <Form.Label>Website Url</Form.Label>
                                <Form.Control type="text" placeholder="Optional" />
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group xs={6} as={Col} controlId="formCapacity">
                                <Form.Label>Address</Form.Label>
                                <Form.Control as="textarea" placeholder="Where is the event?" />
                            </Form.Group>
                        </Row>

                        <Button className="mt-3" variant="primary">Submit Event</Button>
                    </Form>
                </Container>
            </div>
         </>);
            
    }
    
}