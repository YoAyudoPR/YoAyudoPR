import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';


export function OrganizationProfileNavigate(props) {
    const [Data, setData] = useState([]);
    const [EventData, setEventData] = useState([]);
    const [MemberData, setMemberData] = useState([]);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/organization/get?guid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/organization/getevents?guid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setEventData(response.data)
        });
    }, []);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/member/getorganizationmembers?organizationGuid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setMemberData(response.data)
        });
    }, []);

    return (<OrganizationProfile info={Data} event={EventData} member={MemberData} navigate={useNavigate()}></OrganizationProfile>)
}


export default class OrganizationProfile extends Component {
    static displayName = OrganizationProfile.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

    render() {

        <div className="OP-first-div">
            <h1>{this.props.info.name}</h1>
            <Row>
                <Col>
                    <h5>Description</h5>
                    <p>{this.props.info.description}</p>
                </Col>
            </Row>
         </div>

        if (this.props.event.length > 0) {
            return this.props.event.map(value => {
                return (
                    <div>
                        <h2>Events</h2>
                        <div className="mb-4 OP-second-div">
                            <Row className="d-flex justify-content-center mb-4">
                                <Col xs={4} className="mr-4">
                                    <Card style={{ width: '18rem' }}>
                                        <Card.Img variant="top" src="../images/teamwork.jpg" />
                                        <Card.Body>
                                            <Card.Title>{this.props.event.name}</Card.Title>
                                            <Button variant="primary">EVENT DETAILS</Button>
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

        if (this.props.member.length > 0) {
            return this.props.member.map(value => {
                return (
                    <div>
                        <h2>Members</h2>
                        <Row className="d-flex justify-content-center">
                            <Col xs={4}>
                                <Card style={{ width: '18rem' }}>
                                    <Card.Img variant="top" src="../images/profile-pic.jpg" />
                                    <Card.Body>
                                        <Card.Title>{value.userName}</Card.Title>
                                        <Card.Text>{value.roleName}</Card.Text>
                                        <Button variant="primary">LINK PROFILE</Button>
                                    </Card.Body>
                                </Card>
                            </Col>
                        </Row>
                        );
                    </div>
                );
            });
        }
        else {
            return (<h1>No Member</h1>)
        }
    }

}

