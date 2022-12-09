
import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Axios from 'axios';
import './EventDetails.css';




export function EventDetailsNavigate(props) {
    return (<EventDetails navigate={useNavigate()}></EventDetails>)
}

export default class EventDetails extends Component {
    

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    

    render() {
        return (<>
            <div className="mb-4 ED-container">
                <h1>Event Details</h1>

                <Row>
                    <Col>
                        <h5>Name</h5>
                        <p>*EVENT NAME HERE*</p>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <h5>Desciption</h5>
                        <p className="ED-make-block">*Description Here!*</p>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h5>Timeline</h5>
                        <p className="ED-make-block">*EVENT START DATE*</p>
                        <p>*EVENT END DATE*</p>
                    </Col>
                </Row>

            </div>
        </>);
    }
}