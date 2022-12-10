import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Axios from 'axios';
import './CreateEvent.css';

export function CreateEventNavigate(props) {
    return (<CreateEvent navigate={useNavigate()}></CreateEvent>)
}

export default class CreateEvent extends Component {
    static displayName = CreateEvent.name;

    constructor(props) {
        super(props);
        this.state = {organizationId: 0 , description: '', startDate: 0, endDate: 0, Capacity: 0, createdDate: 0};
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    createEvents = (event) => {
        event.preventDefault();
        Axios.get("api/event/create", {
            organization_id: this.state.organizationId,
            description: this.state.description,
            startdate: this.state.startDate,
            enddate: this.state.endDate,
            capacity: this.state.Capacity,
            createdat: this.state.createdDate,
            isactive: true,
            isdeleted: false,
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
                                </Row>

                                <Row className="mb-3">
                                        <Form.Group xs={6} as={Col} controlId="formGridPassword">
                                        <Form.Label>Description</Form.Label>
                                    <Form.Control as="textarea" placeholder="Describe your event" />
                                    </Form.Group>
                                 </Row>

                                <Row className="mb-3">
                                        <Form.Group xs={3} as={Col} controlId="formGridAddress1">
                                        <Form.Label>Start Date</Form.Label>
                                        <Form.Control type="date" placeholder="" />
                                    </Form.Group>

                                    <Form.Group xs={3} as={Col} controlId="formGridAddress2">
                                        <Form.Label>End Date</Form.Label>
                                        <Form.Control type="date" placeholder="" />
                                    </Form.Group>
                    
                                </Row>

                               <Row className="mb-3">
                                     <Form.Group xs={4} as={Col} controlId="formCapacity">
                                        <Form.Label>Capacity</Form.Label>
                                        <Form.Control type="text" placeholder="How many can attend?" />
                                    </Form.Group>
                                </Row>
                               <Button className="mt-3" variant="primary">Submit Event</Button>
                         </Form>
                </Container>
            </div>
         </>);
            
    }
    
}