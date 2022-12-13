import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';


export function OrgEventCard(props) {
    const [EventData, setEventData] = useState([]);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/organization/getevents?guid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setEventData(response.data)
        });
    }, []);

    return (<OrganizationEvents event={EventData} navigate={useNavigate()}></OrganizationEvents>)
}

export default class OrganizationEvents extends Component {
    static displayName = OrganizationEvents.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

/*    ToEvents = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const eventGuid = event.target.value;
        localStorage.setItem("eventGuid", eventGuid);
        this.props.navigate("/OrgEventDetails")
    }*/

    eventsDetails = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const event_id = event.target.value;
        Axios.get(`api/event/get?guid=${event_id}`, {
        }).then((response) => {
            console.log(response.data);
            localStorage.setItem("event_data", JSON.stringify(response.data))
            this.props.navigate("/EventDetails")
        });
    }

    render() {
        if (this.props.event.length > 0) {
            return this.props.event.map(value => {
                return (
                    <div>
                        <div className="mb-4 OP-second-div">
                            <Row className="d-flex justify-content-center mb-4">
                                <Col xs={4} className="mr-4">
                                    <Card style={{ width: '18rem' }}>
                                        <Card.Img variant="top" src="../images/teamwork.jpg" />
                                        <Card.Body>
                                            <Card.Title>{value.name}</Card.Title>
                                            <Button variant="primary" value={value.guid} onClick={e => this.eventsDetails(e, "value")}>EVENT DETAILS</Button>
                                        </Card.Body>
                                    </Card>
                                </Col>
                            </Row>
                        </div>
                        );
                    </div>
                );
            });
        }
        else {
            return (<h1>No Events</h1>)
        }
    }

}

