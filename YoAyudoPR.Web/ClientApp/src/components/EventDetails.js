import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Row, Card, } from 'react-bootstrap/';
import Axios from 'axios';
import './CreateEvent.css';

export function EventDetailsNavigate(props) {
    const event_info = JSON.parse(localStorage.getItem("event_data"))
    return (<EventDetails info={event_info} navigate={useNavigate()} ></EventDetails>)
}

export default class EventDetails extends Component {
    static displayName = EventDetails.name;

    constructor(props) {
        super(props);
    }

    goHome = (event) => {
        this.props.navigate("/Home");
    }

    reqActivity = (event) => {
        event.preventDefault();
        console.log(event.target.value);
        Axios.post("api/activitylog/requestparticipation", {
            eventGuid: event.target.value,
            userGuid: localStorage.getItem("guid"),
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
        let startdate = new Date(this.props.info.startdate).toUTCString();
        let enddate = new Date(this.props.info.enddate).toUTCString();
        return (<>
            <div className="ED-bg-img">
                <div className="mb-4 pt-4 ED-container">
                    <Row >
                        <Col xs={5}>
                            <img className="ED-img" src="../images/beach2.jpg" alt="Event Picture" />
                        </Col>
                        <Col xs={4} className="ED-info-column">
                            <Card className="p-4" style={{ width: '18rem' }}>
                            <Row>
                                <h1>Event Details</h1>
                                <Col>
                                    <h5>Name</h5>
                                    <p>{this.props.info.name}</p>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <h5>Timeline</h5>
                                    <p className="ED-make-block">{startdate}</p>
                                    <p>{enddate}</p>
                                </Col>
                            </Row>
                            </Card>
                        </Col>
                    </Row>
                    <Row className="mt-4 mb-4">
                        <Col>
                            <h5 className="ED-white-text">Desciption</h5>
                            <p className="ED-white-text ED-make-block">{this.props.info.description}</p>
                        </Col>
                    </Row>
                    <Row>
                        <Button className="mb-4" variant="success" value={this.props.info.guid} onClick={(e) => this.reqActivity(e, "value")} >Register for this event</Button>
                        <Button variant="primary" onClick={this.goHome} >Go Home</Button>
                        
                    </Row>
                </div>
            </div>

        </>);
    }
}