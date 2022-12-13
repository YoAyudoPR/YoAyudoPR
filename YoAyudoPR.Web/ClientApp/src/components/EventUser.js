import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';

export function EventUserCard(props) {
    const [UserData, setUserData] = useState([]);

    useEffect(() => {
        const event = JSON.parse(localStorage.getItem("event_data"));
        console.log(event);
        Axios.get(`/api/activitylog/geteventparticipants?eventGuid=${event.guid}`, {
        }).then((response) => {
            console.log(response);
            setUserData(response.data)
        });
    }, []);

    return (<EventUser user={UserData} navigate={useNavigate()}></EventUser>)
}

export default class EventUser extends Component {
    static displayName = EventUser.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

    ToAddHours = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const guid = event.target.value;
        localStorage.setItem("activityLogGuid", guid);
        this.props.navigate("/AddHours")
    }

    render() {
        if (this.props.user.length > 0) {
            return this.props.user.map(value => {
                var statusDate = value.updatedat == null ? value.createdat : value.updatedat;
                let formattedDate = new Date(statusDate).toDateString();
                return (
                    <div>
                        <Row className="d-flex justify-content-center">
                            <Col xs={4}>
                                <Card>
                                    <Card.Img variant="top" src="../images/profile-pic.jpg" />
                                    <Card.Body>
                                        <Card.Title>{value.userName}</Card.Title>
                                        <Card.Text>Hours Volunteered: {value.hoursvolunteered} <br /> Status: {value.status} as of {formattedDate} <br /> </Card.Text>
                                        <Button variant="primary" value={value.guid} onClick={this.ToAddHours}>Add Hours</Button>
                                    </Card.Body>
                                </Card>
                            </Col>
                        </Row>
                    </div>
                );
            });
        }
        else {
            return (<h1 style={{ color : 'white' }}>No Volunteers</h1>)
        }
    }
}