import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import Axios from 'axios';

export function UserEventCard(props) {
    const [EventData, setEventData] = useState([]);

    useEffect(() => {
        const userGuid = localStorage.getItem("guid")
        Axios.get(`/api/activitylog/getuserparticipations?userGuid=${userGuid}`, {
        }).then((response) => {
            console.log(response);
            setEventData(response.data)
        });
    }, []);

    return (<UserEvents event={EventData} navigate={useNavigate()}></UserEvents>)
}

export default class UserEvents extends Component {
    static displayName = UserEvents.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

    ToEvents = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const eventGuid = event.target.value;
        localStorage.setItem("eventGuid", eventGuid);
        this.props.navigate("/EventDetails")
    }

    render() {
        if (this.props.event.length > 0) {
            return this.props.event.map(value => {
                var statusDate = value.updatedat == null ? value.createdat : value.updatedat;
                let formattedDate = new Date(statusDate).toDateString();
                return (
                    <div>
                        <div className="mb-4">
                            <Row className="d-flex mb-4">
                                <Col xs={4} className="mr-4">
                                    <Card>
                                        <Card.Img variant="top" src="../images/teamwork.jpg" />
                                        <Card.Body>
                                            <Card.Title>Event: {value.eventName}</Card.Title>
                                            <Card.Subtitle>Organization: {value.organizationName}</Card.Subtitle>
                                            <Card.Text>Hours Volunteered: {value.hoursvolunteered} <br /> Status: {value.status} as of {formattedDate} <br/> </Card.Text>
                                            <Button variant="primary" value={value.guid} onClick={this.ToEvents}>EVENT DETAILS</Button>
                                        </Card.Body>
                                    </Card>
                                </Col>
                            </Row>
                        </div>
                    </div>
                );
            });
        }
        else {
            return (<h1>No Events</h1>)
        }
    }

}